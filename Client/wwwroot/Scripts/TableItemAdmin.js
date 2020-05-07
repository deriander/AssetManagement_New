$(document).ready(function () {
    $.fn.DataTable.ext.errMode = 'none';
    $('#itemTable').DataTable({
        "ajax": {
            url: "/Item/LoadItemAdmin",
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
            {
                "data": "status", "render": function (data) {
                    if(data === true){
                        return "Available";
                    }
                    else {
                        return "Not Available";
                    }
                }
            },
            {
                "data": "create_Date", "render": function (data) {
                    return moment(data).format('DD/MM/YYYY, hh:mm a');
                }
            },
            {
                "data": "update_Date", "render": function (data) {
                    var dateupdate = "Not Updated Yet";
                    if(data === null){
                        return dateupdate;
                    }
                    else {
                        return moment(data).format('DD/MM/YYYY, hh:mm a');
                    }                    
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
                "data": null, "render": function (data, type, row) {
                    return '<button type="button" class="btn btn-warning" id="EditBtn" data-toggle="tooltip" data-placement="top" title="Edit" onclick="return GetById(' + row.id + ')"><i class="mdi mdi-pencil"></i></button> <button type="button" class="btn btn-danger" id="DeleteBtn" data-toggle="tooltip" data-placement="top" title="Delete" onclick="return Delete(' + row.id + ')"><i class="mdi mdi-delete"></i></button>';
                }
            }
        ]
    });
});