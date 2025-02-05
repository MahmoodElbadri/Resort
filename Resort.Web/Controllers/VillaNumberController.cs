using Microsoft.AspNetCore.Mvc;
using Resort.Infrastructure.Data;
using Resort.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Resort.Web.ViewModels;
using Resort.Application.Common.Interfaces;

namespace Resort.Web.Controllers;

public class VillaNumberController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public VillaNumberController(IUnitOfWork unitOfWork)
    {
        this._unitOfWork = unitOfWork;
    }
    public IActionResult Index()
    {
        var villaNumbers = _unitOfWork.VillaNumber.GetAll(includeProperties: "Villa");
        return View(villaNumbers);
    }

    [HttpGet]
    public IActionResult Create()
    {
        VillaNumberVM villas = new VillaNumberVM()
        {
            VillaList = _unitOfWork.Villa.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }).ToList()
        };
        //ViewBag.villas = villas;
        return View(villas);
    }
    [HttpPost]
    public IActionResult Create(VillaNumberVM villa)
    {
        ModelState.Remove("Villa");
        var villaNumber = villa.VillaNumber;
        if (ModelState.IsValid)
        {
            _unitOfWork.VillaNumber.Add(villaNumber);
            _unitOfWork.Save();
            TempData["success"] = "Villa number created successfully";
            return RedirectToAction("Index");
        }
        TempData["error"] = "Villa number has not been created";
        return View(villa);
    }

    [HttpGet]
    public IActionResult Update(int id)
    {
        VillaNumberVM villaNumber = new VillaNumberVM()
        {
            VillaList = _unitOfWork.Villa.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }).ToList(),
            VillaNumber = _unitOfWork.VillaNumber.Get(v => v.Villa_Number == id)
        };
        if (villaNumber.VillaNumber == null)
        {
            return RedirectToAction("Error", "Home");
        }
        return View(villaNumber);
    }

    [HttpPost]
    public IActionResult Update(VillaNumberVM villa)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.VillaNumber.Update(villa.VillaNumber);
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
        VillaNumberVM villaNumber = new VillaNumberVM()
        {
            VillaList = _unitOfWork.Villa.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }).ToList(),
            VillaNumber = _unitOfWork.VillaNumber.Get(v => v.Villa_Number == id)
        };
        if (villaNumber.VillaNumber == null)
        {
            return RedirectToAction("Error", "Home");
        }
        return View(villaNumber);
    }

    [HttpPost]
    public IActionResult Delete(VillaNumberVM villa)
    {
        if (villa.VillaNumber == null || villa.VillaNumber.Villa_Number == 0)
        {
            TempData["error"] = "Invalid Villa Number selection.";
            return RedirectToAction(nameof(Index));
        }

        var villaNumber = _unitOfWork.VillaNumber.Get(tmp => tmp.Villa_Number == villa.VillaNumber.Villa_Number);

        if (villaNumber != null)
        {
            _unitOfWork.VillaNumber.Remove(villaNumber);
            _unitOfWork.Save();  // Ensure Save() is called on the correct UnitOfWork
            TempData["success"] = "Villa deleted successfully";
            return RedirectToAction(nameof(Index));
        }

        TempData["error"] = "Villa not found or has already been deleted.";
        return RedirectToAction(nameof(Index));
    }

}
