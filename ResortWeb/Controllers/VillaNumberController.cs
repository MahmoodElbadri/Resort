using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resort.Domain.Entities;
using Resort.Infrastructure.Data;

namespace ResortWeb.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly ApplicationDbContext _db;

        public VillaNumberController(ApplicationDbContext db)
        {
            this._db = db;
        }
        public IActionResult Index()
        {
            var villaNumbers = _db.VillaNumbers.Include(x => x.Villa).ToList();
            return View(villaNumbers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(VillaNumber villaNumber)
        {
            if (ModelState.IsValid)
            {
                _db.VillaNumbers.Add(villaNumber);
                _db.SaveChanges();
                TempData["Success"] = "Villa number has been created successfully!";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Villa number couldn't be created!";
            return View(villaNumber);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            if (id < 0) { return RedirectToAction("Error", "Home"); }
            var villaNumber = _db.VillaNumbers.Find(id);
            if(villaNumber == null) {
                return RedirectToAction("Error", "Home");
            }
            return View(villaNumber);
        }

        [HttpPost]
        public IActionResult Update(VillaNumber villaNumber)
        {
            if (ModelState.IsValid) {
                _db.Update(villaNumber);
                _db.SaveChanges();
                TempData["Success"] = "Villa number has been updated successfully!";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Villa number couldn't be updated!";
            return View(villaNumber);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id < 0) { return RedirectToAction("Error", "Home"); }
            var villaNumber = _db.VillaNumbers.Find(id);
            if (villaNumber == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(villaNumber);
        }

        [HttpPost]
        public IActionResult Delete(VillaNumber villaNumber)
        {
            var existingVillaNumber = _db.VillaNumbers.Find(villaNumber.VillaNo);
            if (existingVillaNumber == null)
            {
                TempData["error"] = "Villa number couldn't be deleted!";
                return RedirectToAction("Error", "Home");
            }
            _db.VillaNumbers.Remove(existingVillaNumber);
            _db.SaveChanges();
            TempData["Success"] = "Villa number has been deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}
