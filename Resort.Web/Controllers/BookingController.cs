using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resort.Application.Common.Interfaces;
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
}
