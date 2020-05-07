$(document).ready(function () {
    $.fn.DataTable.ext.errMode = 'none';
    $('#returnTable').DataTable({
        "ajax": {
            url: "/Return/LoadReturnUser",
            type: "GET",
            dataType: "json",
            dataSrc: ""
        },
        //"columnDefs": [
        //    { "orderable": true, "targets": 3 },
        //    { "searchable": true, "targets": 3 }
        //],
        "order": [[1, "desc"]],
        "scrollX": true,
        "columns": [
            {
                "data": "status", "render": function (data, type, row) {
                    if (data === "Done") {
                        return "-"
                    }
                    else {
                        return '<button type="button" class="btn btn-success" id="ReturnBtn" data-toggle="tooltip" data-placement="top" title="Return" onclick="return ShowModal(' + row.item_Id + ')">Return</button>';
                    }                 
                }
            },
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