using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resort.Application.Common.Interfaces;
using Resort.Application.Utility;
using Resort.Domain.Entities;
using System.Security.Claims;
using Stripe.Checkout;
using Stripe;

namespace Resort.Web.Controllers;

public class BookingController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public BookingController(IUnitOfWork unitOfWork)
    {
        this._unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        return View();
    }


    [Authorize]
    public IActionResult FinalizeBooking(int villaId, DateOnly checkInDate, int nights)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
        ApplicationUser user = _unitOfWork.User.Get(tmp => tmp.Id == userId);
        var booking = new Booking
        {
            VillaId = villaId,
            Villa = _unitOfWork.Villa.Get(tmp => tmp.Id == villaId, includeProperties: "VillaAmenity"),
            CheckInDate = checkInDate,
            Nights = nights,
            CheckOutDate = checkInDate.AddDays(nights),
            UserId = userId,
            Phone = user.PhoneNumber,
            Name = user.Name,
            Email = user.Email,

        };
        booking.TotalCost = booking.Villa.Price * nights;
        return View(booking);
    }

    [HttpPost]
    [Authorize]
    public IActionResult FinalizeBooking(Booking booking)
    {
        var villa = _unitOfWork.Villa.Get(tmp => tmp.Id == booking.VillaId);
        booking.TotalCost = villa.Price * booking.Nights;
        booking.Status = SD.StatusPending;
        booking.BookingDate = DateTime.Now;

        _unitOfWork.Booking.Add(booking);
        _unitOfWork.Save();

        var domain = Request.Scheme + "://" + Request.Host.Value + "/";
        var options = new SessionCreateOptions()
        {

            Mode = "payment",
            SuccessUrl = domain + $"Booking/BookingConfirmation?bookingId={booking.Id}",
            CancelUrl = domain + $"Booking/FinalizeBooking?villaId={villa.Id}" +
            $"&checkInDate={booking.CheckInDate}&nights={booking.Nights}",
            LineItems = new List<SessionLineItemOptions>(),
        };

        options.LineItems.Add(new SessionLineItemOptions
        {
            PriceData = new SessionLineItemPriceDataOptions
            {
                UnitAmount = (long)(booking.TotalCost * 100),
                Currency = "usd",
                ProductData = new SessionLineItemPriceDataProductDataOptions
                {
                    Name = villa.Name,
                    Description = villa.Description,
                },
            },
            Quantity = 1,
        });

        var service = new SessionService();
        Session session = service.Create(options);

        _unitOfWork.Booking.UpdateStripePaymentId(booking.Id, session.Id, session.PaymentIntentId);
        _unitOfWork.Save();

        Response.Headers.Add("Location", session.Url);
        return new StatusCodeResult(303);
    }

    [Authorize]
    public IActionResult BookingConfirmation(int bookingId)
    {
        var bookingFromDb = _unitOfWork.Booking.Get(tmp => tmp.Id == bookingId, includeProperties: "Villa,User");
        if(bookingFromDb.Status == SD.StatusPending)
        {
            var service = new SessionService();
            Session session = service.Get(bookingFromDb.StripeSessionId);
            if (session.PaymentStatus.ToLower() == "paid")
            {
                _unitOfWork.Booking.UpdateStatus(bookingFromDb.Id, SD.StatusApproved);
                _unitOfWork.Booking.UpdateStripePaymentId(bookingFromDb.Id, session.Id, session.PaymentIntentId);
                _unitOfWork.Save();
            }
        }
        return View(bookingId);
    }

    #region APICALLS

    [HttpGet]
    [Authorize]
    public IActionResult GetAll()
    {
        IEnumerable<Booking> bookings;
        if (User.IsInRole(SD.Role_Admin))
        {
            bookings = _unitOfWork.Booking.GetAll(includeProperties: "Villa,User");
        }
        else
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            bookings = _unitOfWork.Booking.GetAll(u => u.UserId == userId, includeProperties: "Villa,User");
        }
        return Json(new {data = bookings});
    }

    #endregion
}
