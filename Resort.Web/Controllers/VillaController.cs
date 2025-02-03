using Microsoft.AspNetCore.Mvc;
using Resort.Infrastructure.Data;
using Resort.Domain.Entities;

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
                return NotFound();
            }
            return View(villa);
        }
    }
}
