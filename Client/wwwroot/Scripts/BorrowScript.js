function MyTableReload() {
    var rtable = $('#borrowTable').DataTable({
        ajax: "data.json"
    });

    rtable.ajax.reload();
}

function AcceptApproval1(id) {
    var Model = new Object();
    Model.Id = id;
    $.ajax({
        type: 'PUT',
        url: '/Borrow/AcceptApproval1/' + id,
        data: Model
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

function AcceptApproval2(id, item_id, email) {
    var Model = new Object();
    Model.Id = id;
    Model.Item_Id = item_id;
    Model.Email = email;
    $.ajax({
        type: 'PUT',
        url: '/Borrow/AcceptApproval2/',
        data: Model
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
    var Model = new Object();
    Model.Id = id;
    $.ajax({
        type: 'PUT',
        url: '/Borrow/DeclineApproval/' + id,
        data: Model
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