using Microsoft.AspNetCore.Mvc;
using Resort.Infrastructure.Data;
using Resort.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Resort.Application.Common.Interfaces;

namespace Resort.Web.Controllers
{
    public class VillaController : Controller
    {   
        private readonly IVillaRepository _villaRepo;

        public VillaController(IVillaRepository villaRepo)
        {
            this._villaRepo = villaRepo;
        }
        public IActionResult Index()
        {
            var villas = _villaRepo.GetAll();
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
                _villaRepo.Add(villa);
                _villaRepo.Save();
                TempData["success"] = "Villa created successfully";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Villa has not been created";
            return View(villa); 
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var villa = _villaRepo.Get(v => v.Id == id);
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
                _villaRepo.Update(villa);
                _villaRepo.Save();
                TempData["success"] = "Villa updated successfully";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Villa has not been updated";
            return View(villa);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var villa = _villaRepo.Get  (v => v.Id == id);
            if (villa == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(villa);
        }

        [HttpPost]
        public IActionResult Delete(Villa villa)
        {
            var existedVilla = _villaRepo.Get(v => v.Id == villa.Id);
            if (existedVilla is not null)
            {
                _villaRepo.Remove(existedVilla);
                _villaRepo.Save();
                TempData["success"] = "Villa deleted successfully";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Villa has not been deleted";
            return View();
        }
    }
}
