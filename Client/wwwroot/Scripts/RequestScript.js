function MyTableReload() {
    var rtable = $('#requestTable').DataTable({
        ajax: "data.json"
    });

    rtable.ajax.reload();
}

function ShowModal() {
    $('#myModal').modal('show');
    $('#Brand').val('');
    $('#Cpu').val('');
    $('#Gpu').val('');
    $('#Ram').val('');
    $('#Display').val('');
    $('#Storage').val('');
    $('#Os').val('');
}

function AcceptApproval1(id) {
    var Req = new Object();
    Req.Name = id;
    $.ajax({
        type: 'PUT',
        url: '/Request/AcceptApproval1/' + id,
        data: Req
    }).then((result) => {
        if (result.statusCode === 200) {
            Swal.fire({
                position: 'center',
                type: 'success',
                title: 'Approval Accept Successfully',
                timer: 5000
            }).then(function () {
                MyTableReload();
            });
        } else {
            Swal.fire('Error', 'Failed', 'error');
            MyTableReload();
        }
    });
}

function AcceptApproval2(id) {
    var Req = new Object();
    Req.Id = id;
    $.ajax({
        type: 'PUT',
        url: '/Request/AcceptApproval2/' + id,
        data: Req
    }).then((result) => {
        if (result.statusCode === 200) {
            Swal.fire({
                position: 'center',
                type: 'success',
                title: 'Approval Accept Successfully',
                timer: 5000
            }).then(function () {
                MyTableReload();
            });
        } else {
            Swal.fire('Error', 'Failed', 'error');
            MyTableReload();
        }
    });
}

function DeclineApproval(id) {
    var Req = new Object();
    Req.Id = id;
    $.ajax({
        type: 'PUT',
        url: '/Request/DeclineApproval/' + id,
        data: Req
    }).then((result) => {
        if (result.statusCode === 200) {
            Swal.fire({
                position: 'center',
                type: 'success',
                title: 'Approval Decline Successfully',
                timer: 5000
            }).then(function () {
                MyTableReload();
            });
        } else {
            Swal.fire('Error', 'Failed', 'error');
            MyTableReload();
        }
    });
}

function GetById(id) {
    $.ajax({
        url: "/Request/GetById/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (obj) {
            $('#Id').val(obj[0].id);
            $('#Brand').val(obj[0].brand);
            $('#Cpu').val(obj[0].cpu);
            $('#Gpu').val(obj[0].gpu);
            $('#Ram').val(obj[0].ram);
            $('#Display').val(obj[0].display);
            $('#Storage').val(obj[0].storage);
            $('#Os').val(obj[0].os);
            $('#Email').val(obj[0].email);
            $('#Fullname').val(obj[0].fullname);
            $('#myModal').modal('show');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function AddItemRequest() {
    var Req = new Object();
    Req.Id = $('#Id').val();
    Req.Brand = $('#Brand').val();
    Req.Cpu = $('#Cpu').val();
    Req.Gpu = $('#Gpu').val();
    Req.Ram = $('#Ram').val();
    Req.Display = $('#Display').val();
    Req.Storage = $('#Storage').val();
    Req.Os = $('#Os').val();
    Req.Email = $('#Email').val();
    Req.Fullname = $('#Fullname').val();
    $.ajax({
        type: 'POST',
        url: '/Request/AddItemRequest/',
        data: Req
    }).then((result) => {
        if (result.statusCode === 201) {
            Swal.fire({
                position: 'center',
                type: 'success',
                title: 'Request Item Added Successfully',
                timer: 5000
            }).then(function () {
                MyTableReload();
            });
        } else {
            Swal.fire('Error', 'Failed to Input', 'error');
            MyTableReload();
        }
    });
}

function AddRequest() {
    var Req = new Object();
    Req.Brand = $('#Brand').val();
    Req.Cpu = $('#Cpu').val();
    Req.Gpu = $('#Gpu').val();
    Req.Ram = $('#Ram').val();
    Req.Display = $('#Display').val();
    Req.Storage = $('#Storage').val();
    Req.Os = $('#Os').val();
    $.ajax({
        type: 'POST',
        url: '/Request/AddRequest/',
        data: Req
    }).then((result) => {
        if (result.statusCode === 201) {
            Swal.fire({
                position: 'center',
                type: 'success',
                title: 'Request Added Successfully',
                timer: 5000
            }).then(function () {
                MyTableReload();
            });
        } else {
            Swal.fire('Error', 'Failed to Input', 'error');
            MyTableReload();
        }
    });
}