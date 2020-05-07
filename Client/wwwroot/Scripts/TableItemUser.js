$(document).ready(function () {
    $.fn.DataTable.ext.errMode = 'none';
    $('#itemTable').DataTable({
        "ajax": {
            url: "/Item/LoadItemUser",
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
            { "data": "brand" },
            { "data": "cpu" },
            { "data": "gpu" },
            { "data": "ram" },
            { "data": "display" },
            { "data": "storage" },
            { "data": "os" },
                        {
                            "data": null, "render": function (data, type, row) {
                                return '<button type="button" class="btn btn-success" id="BorrowBtn" data-toggle="tooltip" data-placement="top" title="Borrow" onclick="return BorrowItem(' + row.id + ')">Borrow</button>';
                            }
                        }
        ]
    });
});