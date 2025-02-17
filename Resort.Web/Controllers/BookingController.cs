using Microsoft.AspNetCore.Mvc;
using Resort.Application.Common.Interfaces;
using Resort.Domain.Entities;

namespace Resort.Web.Controllers;

public class BookingController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public BookingController(IUnitOfWork unitOfWork)
    {
        this._unitOfWork = unitOfWork;
    }
    public IActionResult FinalizeBooking(int villaId, DateOnly checkInDate,int nights)
    {
        var booking = new Booking
        {
            VillaId = villaId,
            Villa = _unitOfWork.Villa.Get(tmp=>tmp.Id==villaId,includeProperties:"VillaAmenity"),
            CheckInDate = checkInDate,
            Nights = nights,
            CheckOutDate = checkInDate.AddDays(nights)
        };
        return View(booking);
    }
}
