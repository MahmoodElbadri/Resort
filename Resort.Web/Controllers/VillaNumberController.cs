using Microsoft.AspNetCore.Mvc;
using Resort.Infrastructure.Data;
using Resort.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Resort.Web.Controllers
{
    public class VillaNumberController : Controller
    {   
        private readonly ApplicationDbContext _db;
        public VillaNumberController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var villaNumbers = _db.VillaNumbers.ToList();
            return View(villaNumbers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var villas = _db.Villas.ToList().Select(tmp => new SelectListItem
            {
                Text = tmp.Name,
                Value = tmp.Id.ToString()
            });
            ViewBag.villas = villas;
            return View();
        }
        [HttpPost]
        public IActionResult Create(VillaNumber villa)
        {
            ModelState.Remove("Villa");
            if (ModelState.IsValid)
            {
                _db.VillaNumbers.Add(villa);
                _db.SaveChanges();
                TempData["success"] = "Villa number created successfully";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Villa number has not been created";
            return View(villa); 
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var villa = _db.Villas.FirstOrDefault(v => v.Id == id);
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
                _db.Villas.Update(villa);
                _db.SaveChanges();
                TempData["success"] = "Villa updated successfully";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Villa has not been updated";
            return View(villa);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var villa = _db.Villas.FirstOrDefault(v => v.Id == id);
            if (villa == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(villa);
        }

        [HttpPost]
        public IActionResult Delete(Villa villa)
        {
            var existedVilla = _db.Villas.AsNoTracking().FirstOrDefault(v => v.Id == villa.Id);
            if (existedVilla is not null)
            {
                _db.Villas.Remove(villa);
                _db.SaveChanges();
                TempData["success"] = "Villa deleted successfully";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Villa has not been deleted";
            return View();
        }
    }
}
