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

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Error()
    {
        return View();
    }
}
