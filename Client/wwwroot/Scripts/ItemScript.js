function MyTableReload() {
    var rtable = $('#itemTable').DataTable({
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
    $('#AddBtn').show();
    $('#EditBtn').hide();
}

function BorrowItem(id) {
    var Model = new Object();
    $.ajax({
        type: 'POST',
        url: '/Item/BorrowItem/' + id,
        data: Model
    }).then((result) => {
        if (result.statusCode === 201) {
            Swal.fire({
                position: 'center',
                type: 'success',
                title: 'Borrow Successfully',
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

function InsertItem() {
    var Model = new Object();
    Model.Brand = $('#Brand').val();
    Model.Cpu = $('#Cpu').val();
    Model.Gpu = $('#Gpu').val();
    Model.Ram = $('#Ram').val();
    Model.Display = $('#Display').val();
    Model.Storage = $('#Storage').val();
    Model.Os = $('#Os').val();
    $.ajax({
        type: 'POST',
        url: '/Item/InsertOrUpdate/',
        data: Model
    }).then((result) => {
        if (result.statusCode === 201) {
            Swal.fire({
                position: 'center',
                type: 'success',
                title: 'Item Added Successfully',
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

function UpdateItem(id) {
    var Model = new Object();
    Model.Id = $('#Id').val();
    Model.Brand = $('#Brand').val();
    Model.Cpu = $('#Cpu').val();
    Model.Gpu = $('#Gpu').val();
    Model.Ram = $('#Ram').val();
    Model.Display = $('#Display').val();
    Model.Storage = $('#Storage').val();
    Model.Os = $('#Os').val();
    $.ajax({
        type: 'POST',
        url: '/Item/InsertOrUpdate/' + id,
        data: Model
    }).then((result) => {
        if (result.statusCode === 200) {
            Swal.fire({
                position: 'center',
                type: 'success',
                title: 'Item Updated Successfully',
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
        url: "/Item/GetById/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (obj) {
            $('#Id').val(obj.id);
            $('#Brand').val(obj.brand);
            $('#Cpu').val(obj.cpu);
            $('#Gpu').val(obj.gpu);
            $('#Ram').val(obj.ram);
            $('#Display').val(obj.display);
            $('#Storage').val(obj.storage);
            $('#Os').val(obj.os);
            $('#myModal').modal('show');
            $('#AddBtn').hide();
            $('#EditBtn').show();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function Delete(id) {
    var Model = new Object();
    Model.Id = id;
    Swal.fire({
        title: "Are you sure?",
        showCanceButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: '/Item/Delete/' + id,
                type: "PUT",
                data: Model
            }).then((result) => {
                if (result.statusCode === 200) {
                    Swal.fire({
                        position: 'center',
                        type: 'success',
                        title: 'Item Deleted Successfully'
                    }).then((result) => {
                        if (result.value) {
                            MyTableReload();
                        }
                    });
                } else {
                    Swal.fire('Error', 'Failed to Delete', 'error');
                    ShowModal();
                }
            });
        }
    });
}