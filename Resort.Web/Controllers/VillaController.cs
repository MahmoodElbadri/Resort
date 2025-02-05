using Microsoft.AspNetCore.Mvc;
using Resort.Infrastructure.Data;
using Resort.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Resort.Application.Common.Interfaces;

namespace Resort.Web.Controllers;

public class VillaController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public VillaController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
    {
        this._unitOfWork = unitOfWork;
        this._webHostEnvironment = webHostEnvironment;
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
            if (villa.Image != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(villa.Image.FileName);
                string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "Images/VillaImages");

                // Ensure directory exists
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string filePath = Path.Combine(folderPath, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    villa.Image.CopyTo(fileStream);
                }

                // Store correct relative URL for the image (not full server path)
                villa.ImageUrl = "/Images/VillaImages/" + fileName;
            }
            else
            {
                villa.ImageUrl = "/Images/placeholder.jpg";  // Make sure placeholder is in wwwroot
            }


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
        if (villa == null)
        {
            return RedirectToAction("Error", "Home");
        }
        return View(villa);
    }

    [HttpPost]
    public IActionResult Update(Villa villa)
    {
        if (ModelState.IsValid)
        {
            if (ModelState.IsValid)
            {
                if (villa.Image != null)
                {

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(villa.Image.FileName);
                    string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "Images/VillaImages");

                    string filePath = Path.Combine(folderPath, fileName);

                    if (!string.IsNullOrEmpty(villa.ImageUrl))
                    {
                        string oldFilePath = Path.Combine(folderPath, Path.GetFileName(villa.ImageUrl));
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        villa.Image.CopyTo(fileStream);
                    }

                    // Store correct relative URL for the image (not full server path)
                    villa.ImageUrl = "/Images/VillaImages/" + fileName;
                }
            }
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
        var villa = _unitOfWork.Villa.Get(v => v.Id == id);
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
            if (!string.IsNullOrEmpty(villa.ImageUrl))
            {
                string oldFilePath = Path.Combine(_webHostEnvironment.WebRootPath, existedVilla.ImageUrl.TrimStart('/'));
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }

            _unitOfWork.Villa.Remove(existedVilla);
            _unitOfWork.Save();
            TempData["success"] = "Villa deleted successfully";
            return RedirectToAction("Index");
        }
        TempData["error"] = "Villa has not been deleted";
        return View();
    }
}
