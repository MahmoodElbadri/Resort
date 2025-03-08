using Microsoft.AspNetCore.Mvc;
using Resort.Application.Common.Interfaces;
using Resort.Application.Utility;
using Resort.Web.ViewModels;

namespace Resort.Web.Controllers;

public class DashboardController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    static int previousMonth = DateTime.Now.Month == 1 ? 12 : DateTime.Now.Month - 1;
    readonly DateTime PreviousMonthStartDate = new (DateTime.Now.Year, previousMonth, 1);
    readonly DateTime CurrentMonthStartDate = new (DateTime.Now.Year, DateTime.Now.Month, 1);

    public DashboardController(IUnitOfWork unitOfWork)
    {
        this._unitOfWork = unitOfWork;
    }
    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> GetBookingRadialChartData()
    {
        var totalBookings = _unitOfWork.Booking.GetAll(tmp => tmp.Status != SD.StatusCancelled
        || tmp.Status == SD.StatusPending);

        var countByCurrentMonth = totalBookings.Count(tmp => tmp.BookingDate >= CurrentMonthStartDate &&
        tmp.BookingDate <= DateTime.Now.Date);
        var countByPreviousMonth = totalBookings.Count(tmp => tmp.BookingDate >= PreviousMonthStartDate &&
        tmp.BookingDate <= CurrentMonthStartDate);

        RadialBarChartVM radialBarChartVM = new();

        int increaseDecreaseIssue = 100;

        if (countByPreviousMonth > 0)
        {
            increaseDecreaseIssue =Convert.ToInt32 ((countByCurrentMonth - countByPreviousMonth) / countByPreviousMonth) * 100;
        }

        radialBarChartVM.Series = new int[] { increaseDecreaseIssue };
        radialBarChartVM.TotalCount = totalBookings.Count();
        radialBarChartVM.CountInCurrentMonth = countByCurrentMonth;
        radialBarChartVM.HasRatioIncreased = CurrentMonthStartDate > PreviousMonthStartDate;

        return Json(radialBarChartVM);
    }
}
