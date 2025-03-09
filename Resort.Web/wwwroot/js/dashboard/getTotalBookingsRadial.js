$(document).ready(function () {
    loadTotalBookingsRadialChart();
});

function loadTotalBookingsRadialChart() {
    $(".chart-spinner").show(); 

    $.ajax({
        url: '/Dashboard/GetBookingRadialChartData',
        type: 'GET',
        dataType: 'JSON',
        success: function (data) {
            document.querySelector("#spanTotalBookingCount").innerHTML = data.spanTotalBookingCount;

            var sectionCurrentCount = document.createElement("span");
            if (data.hasRatioIncreased) {
                sectionCurrentCount.className = "text-success";
                sectionCurrentCount.innerHTML = '<i class="bi bi-arrow-up-right-circle-fill me-1"></i> <span>'
                    + data.countInCurrentMonth + '</span>';
            }
            else {
                sectionCurrentCount.className = "text-danger";
                sectionCurrentCount.innerHTML = '<i class="bi bi-arrow-down-right-circle-fill me-1"></i> <span>'
                    + data.countInCurrentMonth + '</span>';
            }

            document.querySelector("#sectionBookingCount").append(sectionCurrentCount);
            document.querySelector("#sectionBookingCount").append(" since last month");

            $(".chart-spinner").hide(); 
        },
        error: function () {
            console.error("Error fetching chart data.");
            $(".chart-spinner").hide(); 
        }
    });
}
