using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Resort.Application.Common.Interfaces;
using Resort.Web.Models;
using Resort.Web.ViewModels;

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
    public IActionResult Index(HomeVM homeVM)
    {
        homeVM.VillaList = _unitOfWork.Villa.GetAll(includeProperties: "VillaAmenity");
        foreach (var villa in homeVM.VillaList)
        {
            if (villa.Id % 2 == 0)
            {
                villa.IsAvailable = false;
            }
        }
        return View(homeVM);
    }
    
    public IActionResult GetVillasByDate(int nights, DateOnly CheckInDate)
    {
        var villaList = _unitOfWork.Villa.GetAll(includeProperties: "VillaAmenity").ToList();
        foreach (var villa in villaList)
        {
            if (villa.Id % 2 == 0)
            {
                villa.IsAvailable = false;
            }
        }
        HomeVM homeVM = new()
        {
            CheckInDate = CheckInDate,
            Nights = nights,
            VillaList = villaList
        };
        return PartialView("_VillaList",homeVM);
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
