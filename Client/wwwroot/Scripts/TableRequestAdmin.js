$(document).ready(function () {
    $.fn.DataTable.ext.errMode = 'none';
    $('#requestTable').DataTable({
        "ajax": {
            url: "/Request/LoadRequest",
            type: "GET",
            dataType: "json",
            dataSrc: ""
        },
        //"columnDefs": [
        //    { "orderable": true, "targets": 3 },
        //    { "searchable": true, "targets": 3 }
        //],
        "columns": [
            { "data": "fullname" },
            {
                "data": "request_Date", "render": function (data) {
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
            {
                "data": "status_Approval", "render": function (data, type, row) {
                    if (data === "Approved") {
                        return '<button type="button" class="btn btn-success" id="GetByIdBtn" data-toggle="tooltip" data-placement="top" title="Add" onclick="return GetById(' + row.id + ')">Add Item</button>';
                    }
                    else {
                        return '-'
                    }

                }
            },
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