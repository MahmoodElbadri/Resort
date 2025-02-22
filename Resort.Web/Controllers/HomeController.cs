using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Resort.Application.Common.Interfaces;
using Resort.Web.Models;
using Resort.Web.ViewModels;
using Resort.Application.Utility;

namespace Resort.Web.Controllers;

public class HomeController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public HomeController(IUnitOfWork unitOfWork)
    {
        this._unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        var homeVm = new HomeVM
        {
            CheckInDate = DateOnly.FromDateTime(DateTime.Now),
            Nights = 1,
            VillaList = _unitOfWork.Villa.GetAll(includeProperties: "VillaAmenity"),
        };
        return View(homeVm);
        //var villas = _unitOfWork.Villa.GetAll(includeProperties: "VillaAmenity").ToList();
        //foreach (var villa in villas)
        //{
        //    Console.WriteLine($"Villa: {villa.Name}, Amenities: {villa.VillaAmenity?.Count()}");
        //}
        //return View();
    }

    [HttpPost]
    public IActionResult GetVillasByDate(int nights, DateOnly CheckInDate)
    {
        Thread.Sleep(1500);
        var villaList = _unitOfWork.Villa.GetAll(includeProperties: "VillaAmenity").ToList();
        var villaNumbersList = _unitOfWork.VillaNumber.GetAll().ToList();
        var bookedVillas = _unitOfWork.Booking.GetAll
            (tmp=>tmp.Status ==  SD.StatusApproved || tmp.Status == SD.StatusCheckedIn).ToList();
        foreach (var villa in villaList)
        {
            int roomAvailable =
                SD.VillaRoomsAvailable_Count(villa.Id, villaNumbersList, CheckInDate, nights, bookedVillas);
            villa.IsAvailable = roomAvailable > 0 ? true : false;
        }
        HomeVM homeVM = new()
        {
            CheckInDate = CheckInDate,
            Nights = nights,
            VillaList = villaList
        };
        return PartialView("_VillaList", homeVM);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Error()
    {
        return View();
    }
}
