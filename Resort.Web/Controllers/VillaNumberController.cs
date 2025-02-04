using Microsoft.AspNetCore.Mvc;
using Resort.Infrastructure.Data;
using Resort.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Resort.Web.ViewModels;

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
            var villaNumbers = _db.VillaNumbers.Include(tmp=>tmp.Villa).ToList();
            return View(villaNumbers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            VillaNumberVM villas = new VillaNumberVM()
            {
                VillaList = _db.Villas.Select(i => new SelectListItem
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
                _db.VillaNumbers.Add(villaNumber);
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
            VillaNumberVM villaNumber = new VillaNumberVM()
            {
                VillaList = _db.Villas.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }).ToList(),
                VillaNumber = _db.VillaNumbers.FirstOrDefault(v => v.Villa_Number == id)
            };
            if(villaNumber.VillaNumber == null)
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
                _db.VillaNumbers.Update(villa.VillaNumber);
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
            VillaNumberVM villaNumber = new VillaNumberVM()
            {
                VillaList = _db.Villas.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }).ToList(),
                VillaNumber = _db.VillaNumbers.FirstOrDefault(v => v.Villa_Number == id)
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
            var villaNumber = _db.VillaNumbers
                .FirstOrDefault(tmp=>tmp.Villa_Number == villa.VillaNumber.Villa_Number);
            if(villaNumber is not null)
            {
                _db.VillaNumbers.Remove(villaNumber);
                _db.SaveChanges();
                TempData["success"] = "Villa deleted successfully";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Villa has not been deleted";
            return View(villa);
        }
    }
}
