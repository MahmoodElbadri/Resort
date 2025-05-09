﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Resort.Application.Common.Interfaces;
using Resort.Application.Utility;
using Resort.Domain.Entities;

namespace Resort.Web.Controllers;

public class AmenityController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public AmenityController(IUnitOfWork unitOfWork)
    {
        this._unitOfWork = unitOfWork;
    }

    [Authorize(Roles = SD.Role_Admin)]
    [Authorize(Roles = SD.Role_Customer)]
    [HttpGet]
    public IActionResult Index()
    {
        var amenities = _unitOfWork.Amenity.GetAll(includeProperties: "Villa");
        return View(amenities);
    }

    [Authorize(Roles = SD.Role_Admin)]
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

    [Authorize(Roles = SD.Role_Admin)]
    [HttpPost]
    public IActionResult Create(Amenity amenity)
    {
        if (amenity.VillaId == 0)
        {
            ModelState.AddModelError("VillaId", "Villa is required");
            ViewBag.Villa = new SelectList(_unitOfWork.Villa.GetAll(), "Id", "Name");
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

    [Authorize(Roles = SD.Role_Admin)]
    [HttpGet]
    public IActionResult Update(int id)
    {
        var amenity = _unitOfWork.Amenity.Get(tmp => tmp.Id == id);
        ViewBag.Villa = new SelectList(_unitOfWork.Villa.GetAll(), "Id", "Name");
        return View(amenity);
    }

    [Authorize(Roles = SD.Role_Admin)]
    [HttpPost]
    public IActionResult Update(Amenity amenity)
    {
        if(amenity.VillaId == 0)
        {
            ModelState.AddModelError("VillaId", "Villa is required");
            TempData["error"] = "Amenity has not been updated";
            return View(amenity);
        }
        if (ModelState.IsValid)
        {
            _unitOfWork.Amenity.Update(amenity);
            _unitOfWork.Save();
            TempData["success"] = "Amenity updated successfully";
            return RedirectToAction("Index");
        }
        return View(amenity);
    }

    [Authorize(Roles = SD.Role_Admin)]
    [HttpGet]
    public IActionResult Delete(int id)
    {
        var amenity = _unitOfWork.Amenity.Get(tmp => tmp.Id == id);
        return View(amenity);
    }

    [Authorize(Roles = SD.Role_Admin)]
    [HttpPost]
    public IActionResult Delete(Amenity amenity)
    {
        _unitOfWork.Amenity.Remove(amenity);
        _unitOfWork.Save();
        TempData["success"] = "Amenity deleted successfully";
        return RedirectToAction("Index");
    }
}
