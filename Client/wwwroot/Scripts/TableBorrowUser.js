$(document).ready(function () {
    $.fn.DataTable.ext.errMode = 'none';
    $('#borrowTable').DataTable({
        "ajax": {
            url: "/Borrow/LoadBorrowById",
            type: "GET",
            dataType: "json",
            dataSrc: ""
        },
        //"columnDefs": [
        //    { "orderable": true, "targets": 3 },
        //    { "searchable": true, "targets": 3 }
        //],
        "columns": [
            {
                "data": "borrow_Date", "render": function (data) {
                    return moment(data).format('MMMM Do YYYY, h:mm:ss a');
                }
            },
            {
                "data": "approval_1", "render": function (data) {
                    if (data === true) {
                        return "Yes";
                    }
                    else {
                        return "-";
                    }
                }
            },
            {
                "data": "approval_2", "render": function (data) {
                    if (data === true) {
                        return "Yes";
                    }
                    else {
                        return "-";
                    }
                }
            },
            { "data": "status_Approval" },
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