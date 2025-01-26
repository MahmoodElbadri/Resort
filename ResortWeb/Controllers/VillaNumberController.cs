using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Resort.Domain.Entities;
using Resort.Infrastructure.Data;
using ResortWeb.ViewModels;

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
            //Ican use the ViewModel
            VillaNumberVM list = new VillaNumberVM()
            {
                VillaList = _db.Villas.ToList().Select(tmp => new SelectListItem
                {
                    Text = tmp.Name,
                    Value = tmp.Id.ToString()
                })
            };
            //you can use this way
            //List<SelectListItem> list = _db.Villas.ToList().Select(tmp => new SelectListItem
            //{
            //    Text = tmp.Name,
            //    Value = tmp.Id.ToString()
            //}).ToList();
            ////u can use with view data
            ////ViewData["Villas"] = list;
            //// i will use viewbag
            //ViewBag.Villas = list;
            return View(list);
        }

        [HttpPost]
        public IActionResult Create(VillaNumberVM villaNumber)
        {
            //var villa = _db.Villas.FirstOrDefault(x => x.Id == villaNumber.VillaNo);
            //if(villa != null) { TempData["error"] = "Villa number already exists!"; return View(villaNumber); }
            //this is one way to check uniqueness
            //bool isUnique;
            //isUnique = _db.VillaNumbers.Any(tmp => tmp.VillaNo == villaNumber.VillaNo);
            //if (isUnique) { TempData["error"] = "Villa number already exists!"; return View(villaNumber); }
            if (ModelState.IsValid)
            {
                _db.VillaNumbers.Add(villaNumber.VillaNumber);
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
            VillaNumberVM villaNumberVM = new()
            {
                VillaNumber = _db.VillaNumbers.FirstOrDefault(x => x.VillaNo == id),
                VillaList = _db.Villas.ToList().Select(tmp => new SelectListItem
                {
                    Text = tmp.Name,
                    Value = tmp.Id.ToString()
                })
            };

            if (villaNumberVM.VillaNumber == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(villaNumberVM);
        }

        [HttpPost]
        public IActionResult Update(VillaNumberVM villaNumberVM)
        {
            if (ModelState.IsValid)
            {
                var villaNumberFromDb = _db.VillaNumbers.AsNoTracking().FirstOrDefault(x => x.VillaNo == villaNumberVM.VillaNumber.VillaNo);

                if (villaNumberFromDb == null)
                {
                    TempData["error"] = "Villa number not found!";
                    return RedirectToAction("Index");
                }

                _db.VillaNumbers.Update(villaNumberVM.VillaNumber);
                _db.SaveChanges();

                TempData["Success"] = "Villa number has been updated successfully!";
                return RedirectToAction("Index");
            }

            TempData["error"] = "Villa number couldn't be updated!";
            villaNumberVM.VillaList = _db.Villas.ToList().Select(tmp => new SelectListItem
            {
                Text = tmp.Name,
                Value = tmp.Id.ToString()
            });
            return View(villaNumberVM);
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
