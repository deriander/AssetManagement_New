$(document).ready(function () {
    $.fn.DataTable.ext.errMode = 'none';
    $('#requestTable').DataTable({
        "ajax": {
            url: "/Request/LoadRequestApp2",
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
                "data": "request_Date", "render": function (data) {
                    return moment(data).format('DD/MM/YYYY, hh:mm a');
                }
            },
            { "data": "brand" },
            { "data": "cpu" },
            { "data": "gpu" },
            { "data": "ram" },
            { "data": "display" },
            { "data": "storage" },
            { "data": "os" },
            {
                "data": "status_Approval", "render": function (data, type, row) {
                    if (data === "Waiting") {
                        return '<button type="button" class="btn btn-success" id="AccApp2Btn" data-toggle="tooltip" data-placement="top" title="Edit" onclick="return AcceptApproval2(' + row.id + ')">Approve</button> <button type="button" class="btn btn-danger" id="DecAppBtn" data-toggle="tooltip" data-placement="top" title="Delete" onclick="return DeclineApproval(' + row.id + ')">Decline</button>';
                    }
                    else {
                        return '-'
                    }

                }
            }
        ]
    });
});