var dataTable;


$(document).ready(function () {
    const urlParams = new URLSearchParams(window.location.search);
    const status = urlParams.get('status');
    loadDataTable(status);
});


function loadDataTable(status) {
    $('#tblBookings').DataTable({
        "ajax": {
            "url": "/booking/GetAll?status=" + encodeURIComponent(status || ''),
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "10%" },
            { "data": "name", "width": "15%" },
            { "data": "phone", "width": "15%" },
            { "data": "email", "width": "20%" },
            { "data": "status", "width": "10%" },
            { "data": "checkInDate", "width": "10%" },
            { "data": "nights", "width": "10%" },
            { "data": "totalCost",render: $.fn.dataTable.render.number(',', '.', 2, '$'), "width": "10%" },
            {
                "data": 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                            <a href="/booking/bookingDetails?bookingId=${data}" 
                            class="btn btn-outline-warning mx-2">
                                <i class="bi bi-pencil-square"></i> Details
                            </a>
                            <form action="/booking/Delete/${data}" method="post">
                                <button type="submit" class="btn btn-danger mx-2">
                                    <i class="bi bi-trash"></i> Delete
                                </button>
                            </form>
                        </div>`
                }
            },
        ],
        "paging": true,
        "searching": true,  // Ensure search box is enabled
        "info": true,
        "lengthChange": false,
        "language": {
            "search": "_INPUT_",
            "searchPlaceholder": "Search bookings..."
        },
        "dom": '<"top"f>rt<"bottom"lp><"clear">',
    });
}