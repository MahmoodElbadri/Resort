using Microsoft.AspNetCore.Mvc;
using Resort.Infrastructure.Data;

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
            _db.Villas.Add(villa);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
