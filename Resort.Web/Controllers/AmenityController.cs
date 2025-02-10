using Microsoft.AspNetCore.Mvc;
using Resort.Application.Common.Interfaces;

namespace Resort.Web.Controllers
{
    public class AmenityController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AmenityController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var amenities = _unitOfWork.Amenity.GetAll(includeProperties: "Villa");
            return View(amenities);
        }
    }
}
