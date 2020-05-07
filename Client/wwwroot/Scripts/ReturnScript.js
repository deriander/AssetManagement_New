function MyTableReload() {
    var rtable = $('#returnTable').DataTable({
        ajax: "data.json"
    });

    rtable.ajax.reload();
}

function ShowModal(item_id) {
    $('#myModal').modal('show');
    $('#Id').val(item_id);
    $('#Condition').val('');
}

function ReturnItem() {
    var Model = new Object();
    Model.Item_Id = $('#Id').val();
    Model.Condition = $('#Condition').val();
    $.ajax({
        type: 'PUT',
        url: '/Return/ReturnItem/',
        data: Model
    }).then((result) => {
        if (result.statusCode === 200) {
            Swal.fire({
                position: 'center',
                type: 'success',
                title: 'Return Item Successfully',
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