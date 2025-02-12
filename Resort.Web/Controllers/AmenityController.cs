﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Resort.Application.Common.Interfaces;
using Resort.Domain.Entities;

namespace Resort.Web.Controllers
{
    public class AmenityController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AmenityController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var amenities = _unitOfWork.Amenity.GetAll(includeProperties: "Villa");
            return View(amenities);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var villas = _unitOfWork.Villa.GetAll();
            //var villaList = villas.Select(i => new SelectListItem
            //{
            //    Text = i.Name,
            //    Value = i.Id.ToString()
            //}).ToList();

            //ViewBag.Villa = villaList;
            //use this instead of above it's simpler and more efficient and more readable
            //and more maintainable and more secure ane more efficient again and again
            ViewBag.Villa = new SelectList(villas, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Amenity amenity)
        {
            if(amenity.VillaId == 0)
            {
                ModelState.AddModelError("VillaId", "Villa is required");
                ViewBag.Villa = new SelectList(_unitOfWork.Villa.GetAll(),"Id","Name");
                TempData["error"] = "Amenity has not been created";
                return View(amenity);
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Amenity.Add(amenity);
                _unitOfWork.Save();
                TempData["success"] = "Amenity created successfully";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Amenity has not been created";
            return View(amenity);
        }
    }
}
