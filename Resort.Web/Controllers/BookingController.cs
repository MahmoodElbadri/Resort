﻿using Microsoft.AspNetCore.Authorization;
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

    [Authorize]
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

        var villaNumbersList = _unitOfWork.VillaNumber.GetAll().ToList();
        var bookedVillas = _unitOfWork.Booking.GetAll
            (tmp => tmp.Status == SD.StatusApproved || tmp.Status == SD.StatusCheckedIn).ToList();
            int roomAvailable =
                SD.VillaRoomsAvailable_Count(villa.Id, villaNumbersList, booking.CheckInDate, booking.Nights, bookedVillas);
            
        if(roomAvailable == 0)
        {
            return RedirectToAction(nameof(FinalizeBooking), 
                new { villaId = booking.VillaId, checkInDate = booking.CheckInDate, nights = booking.Nights });
        }

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
                    Images = new List<string>
                    {
                        "https://image.jimcdn.com/app/cms/image/transf/dimension=930x10000:format=jpg/path/sb47433d1bebe9902/image/i3bee918e12c0b83d/version/1552606045/image.jpg"
                    },
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
    public IActionResult BookingDetails(int bookingId)
    {
        var bookingFromDb = _unitOfWork.Booking.Get(tmp => tmp.Id == bookingId, includeProperties: "Villa,User");
        if (bookingFromDb.VillaNumber == 0 && bookingFromDb.Status == SD.StatusApproved)
        {
            var availableVillaNumber = AssignAvailableVillaNumberByVilla(bookingFromDb.VillaId);
            bookingFromDb.VillaNumbers = _unitOfWork.VillaNumber.GetAll(tmp => tmp.VillaId == bookingFromDb.VillaId &&
            availableVillaNumber.Any(x => x == tmp.Villa_Number)).ToList();
        }
        return View(bookingFromDb);
    }

    [Authorize(Roles = SD.Role_Admin)]
    [HttpPost]
    public IActionResult CheckIn(Booking booking)
    {
        _unitOfWork.Booking.UpdateStatus(booking.Id, SD.StatusCheckedIn, booking.VillaNumber);
        _unitOfWork.Save();
        TempData["success"] = "Booking updated successfully";
        return RedirectToAction(nameof(BookingDetails), new { bookingId = booking.Id });
    }
    [Authorize(Roles = SD.Role_Admin)]
    [HttpPost]
    public IActionResult CheckOut(Booking booking)
    {
        _unitOfWork.Booking.UpdateStatus(booking.Id, SD.StatusCompleted, booking.VillaNumber);
        _unitOfWork.Save();
        TempData["success"] = "Booking Completed successfully";
        return RedirectToAction(nameof(BookingDetails), new { bookingId = booking.Id });
    }
    [Authorize(Roles = SD.Role_Admin)]
    [HttpPost]
    public IActionResult CancelBooking  (Booking booking)
    {
        _unitOfWork.Booking.UpdateStatus(booking.Id, SD.StatusCancelled, 0);
        _unitOfWork.Save();
        TempData["success"] = "Booking Cancelled successfully";
        return RedirectToAction(nameof(BookingDetails), new { bookingId = booking.Id });
    }

    [Authorize]
    public IActionResult BookingConfirmation(int bookingId)
    {
        var bookingFromDb = _unitOfWork.Booking.Get(tmp => tmp.Id == bookingId, includeProperties: "Villa,User");
        if (bookingFromDb.Status == SD.StatusPending)
        {
            var service = new SessionService();
            Session session = service.Get(bookingFromDb.StripeSessionId);
            if (session.PaymentStatus.ToLower() == "paid")
            {
                _unitOfWork.Booking.UpdateStatus(bookingFromDb.Id, SD.StatusApproved, 0);
                _unitOfWork.Booking.UpdateStripePaymentId(bookingFromDb.Id, session.Id, session.PaymentIntentId);
                _unitOfWork.Save();
            }
        }
        return View(bookingId);
    }

    private List<int> AssignAvailableVillaNumberByVilla(int villaId)
    {
        List<int> availableVillaNumbers = new List<int>();
        var villaNumber = _unitOfWork.VillaNumber.GetAll(tmp => tmp.VillaId == villaId);
        var checkedInVilla = _unitOfWork.Booking.GetAll(tmp => tmp.VillaId == villaId && tmp.Status == SD.StatusCheckedIn)
            .Select(tmp => tmp.VillaNumber);
        foreach (var villa in villaNumber)
        {
            if (!checkedInVilla.Contains(villa.Villa_Number))
            {
                availableVillaNumbers.Add(villa.Villa_Number);
            }
        }
        return availableVillaNumbers;
    }

    #region API_CALLS

    [HttpGet]
    [Authorize]
    public IActionResult GetAll(string status)
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
        if (!string.IsNullOrEmpty(status))
        {
            bookings = bookings.Where(tmp => string.Equals(tmp.Status, status, StringComparison.OrdinalIgnoreCase));
        }
        return Json(new { data = bookings });
    }

    #endregion
}
