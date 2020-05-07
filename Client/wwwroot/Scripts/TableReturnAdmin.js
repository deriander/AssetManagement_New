$(document).ready(function () {
    $.fn.DataTable.ext.errMode = 'none';
    $('#returnTable').DataTable({
        "ajax": {
            url: "/Return/LoadReturnAdmin",
            type: "GET",
            dataType: "json",
            dataSrc: ""
        },
        //"columnDefs": [
        //    { "orderable": true, "targets": 3 },
        //    { "searchable": true, "targets": 3 }
        //],
        "scrollX": true,
        "columns": [
            { "data": "fullname" },
            {
                "data": "return_Date", "render": function (data) {
                    if (data === null) {
                        return "Not Returned Yet";
                    }
                    else {
                        return moment(data).format('DD/MM/YYYY, hh:mm a');
                    }
                }
            },
            { "data": "condition" },
            { "data": "status" },
            { "data": "brand" },
            { "data": "cpu" },
            { "data": "gpu" },
            { "data": "ram" },
            { "data": "display" },
            { "data": "storage" },
            { "data": "os" }
        ]
    });
});