using Microsoft.AspNetCore.Mvc;
using Resort.Infrastructure.Data;
using Resort.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Resort.Application.Common.Interfaces;

namespace Resort.Web.Controllers;

public class VillaController : Controller
{   
    private readonly IUnitOfWork _unitOfWork;

    public VillaController(IUnitOfWork unitOfWork)
    {
        this._unitOfWork = unitOfWork;
    }
    public IActionResult Index()
    {
        var villas = _unitOfWork.Villa.GetAll();
        return View(villas);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(Villa villa)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.Villa.Add(villa);
            _unitOfWork.Save();
            TempData["success"] = "Villa created successfully";
            return RedirectToAction("Index");
        }
        TempData["error"] = "Villa has not been created";
        return View(villa); 
    }

    [HttpGet]
    public IActionResult Update(int id)
    {
        var villa = _unitOfWork.Villa.Get(v => v.Id == id);
        if(villa == null)
        {
            return RedirectToAction("Error","Home");
        }
        return View(villa);
    }

    [HttpPost]
    public IActionResult Update(Villa villa)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.Villa.Update(villa);
            _unitOfWork.Save();
            TempData["success"] = "Villa updated successfully";
            return RedirectToAction("Index");
        }
        TempData["error"] = "Villa has not been updated";
        return View(villa);
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var villa = _unitOfWork.Villa.Get  (v => v.Id == id);
        if (villa == null)
        {
            return RedirectToAction("Error", "Home");
        }
        return View(villa);
    }

    [HttpPost]
    public IActionResult Delete(Villa villa)
    {
        var existedVilla = _unitOfWork.Villa.Get(v => v.Id == villa.Id);
        if (existedVilla is not null)
        {
            _unitOfWork.Villa.Remove(existedVilla);
            _unitOfWork.Save();
            TempData["success"] = "Villa deleted successfully";
            return RedirectToAction("Index");
        }
        TempData["error"] = "Villa has not been deleted";
        return View();
    }
}
