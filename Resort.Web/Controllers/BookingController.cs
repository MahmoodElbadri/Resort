using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resort.Application.Common.Interfaces;
using Resort.Application.Utility;
using Resort.Domain.Entities;
using System.Security.Claims;

namespace Resort.Web.Controllers;

public class BookingController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public BookingController(IUnitOfWork unitOfWork)
    {
        this._unitOfWork = unitOfWork;
    }

    [Authorize]
    public IActionResult FinalizeBooking(int villaId, DateOnly checkInDate, int nights)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
        ApplicationUser user = _unitOfWork.User.Get(tmp=>tmp.Id == userId);
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
        return RedirectToAction(nameof(BookingConfirmation), new {bookingId = booking.Id});
    }

    [Authorize]
    public  IActionResult BookingConfirmation(int bookingId)
    {
        return View(bookingId);
    }
}
