using Microsoft.AspNetCore.Mvc;
using Resort.Domain.Entities;
using Resort.Infrastructure.Data;

namespace ResortWeb.Controllers
{
    public class VillaController : Controller
    {
        private readonly ApplicationDbContext _db;

        public VillaController(ApplicationDbContext db)
        {
            this._db = db;
        }
        public IActionResult Index()
        {
            var villas = _db.Villas.ToList();
            return View(villas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Villa villa)
        {
            if (villa.Name == villa.Description)
            {
                ModelState.AddModelError("", "Name and Description should be different");
            }
            if (ModelState.IsValid)
            {
                _db.Add(villa);
                _db.SaveChanges();
                TempData["Success"] = "Villa has been created successfully";
                return RedirectToAction("Index", "Villa");
            }
            TempData["error"] = "Villa couldn't be created";
            return View(villa);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            Villa? villa = _db.Villas.FirstOrDefault(x => x.Id == id);
            if (villa == null) { return RedirectToAction("Error","Home"); }
            return View(villa);
        }

        [HttpPost]
        public IActionResult Update(Villa villa)
        {

            if (ModelState.IsValid)
            {
                _db.Villas.Update(villa);
                _db.SaveChanges();
                TempData["Success"] = "Villa has been updated successfully";
                return RedirectToAction("Index", "Villa");
            }
            TempData["error"] = "Villa couldn't be updated";
            return View(villa);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Villa? villa = _db.Villas.FirstOrDefault(x => x.Id == id);
            if (villa == null) { return RedirectToAction("Error", "Home"); }
            return View(villa);
        }

        [HttpPost]
        public IActionResult Delete(Villa villa)
        {
            var existVilla = _db.Villas.FirstOrDefault(x => x.Id == villa.Id);
            if (existVilla is not null)
            {
                _db.Villas.Remove(existVilla);
                _db.SaveChanges();
                TempData["Success"] = "Villa has been deleted successfully";
                return RedirectToAction("Index", "Villa");
            }
            TempData["error"] = "Villa couldn't be Deleted";
            return View();
        }
    }
}
