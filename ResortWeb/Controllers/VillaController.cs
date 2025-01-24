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
            _db.Add(villa);
            _db.SaveChanges();
            return RedirectToAction("Index","Villa");
        }
    }
}
