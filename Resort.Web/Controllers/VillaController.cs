using Microsoft.AspNetCore.Mvc;
using Resort.Infrastructure.Data;
using Resort.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Resort.Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly ApplicationDbContext _db;
        public VillaController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var villas = _db.Villas.ToList();
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
                _db.Villas.Add(villa);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
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
                return RedirectToAction("Index");
            }
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
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
