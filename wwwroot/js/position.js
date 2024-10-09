var Position;
(function (Position) {
    function openAddOrEditModal(id) {
        if (id == null || id == undefined) {
            resetForm();
            $('#code').prop('readonly', false);
            $('#addOrEditModal').modal('show');
        }
        else {
            $.ajax({
                url: "/DepartPosit/GetById/".concat(id),
                type: 'GET',
                success: function (res) {
                    if (res.success) {
                        fillForm(res.data);
                        $('#code').prop('readonly', true);
                        $('#addOrEditModal').modal('show');
                    }
                    else {
                        console.error(res);
                    }
                },
                error: function (err) {
                    console.error(err);
                }
            });
        }
    }
    Position.openAddOrEditModal = openAddOrEditModal;
    function resetForm() {
        $('#id').val('');
        $('#code').val('');
        $('#name').val('');
        $('#message').html('');
    }
    Position.resetForm = resetForm;
    function fillForm(data) {
        $('#id').val(data.id);
        $('#code').val(data.code);
        $('#name').val(data.name);
    }
    Position.fillForm = fillForm;
    function add() {
        var data = {
            code: $('#code').val(),
            name: $('#name').val(),
            type: 'Chức vụ'
        };
        console.log(data);
        $.ajax({
            url: '/DepartPosit/Create',
            type: 'POST',
            data: data,
            success: function (res) {
                if (res.success) {
                    $('#addOrEditModal').modal('hide');
                    location.reload();
                }
                else {
                    $('#message').html('<div class="alert alert-danger">' + res.message + '</div>');
                    console.log(res);
                }
            },
            error: function (res) {
                $('#message').html('<div class="alert alert-danger">' + 'Đã xảy ra lỗi' + '</div>');
                console.log(res);
            }
        });
    }
    Position.add = add;
    function edit(id) {
        var data = {
            id: $('#id').val(),
            code: $('#code').val(),
            type: 'Chức vụ',
            name: $('#name').val(),
        };
        $.ajax({
            url: "/DepartPosit/Edit/".concat(id),
            type: 'POST',
            data: data,
            success: function (res) {
                if (res.success) {
                    $('#addOrEditModal').modal('hide');
                    location.reload();
                }
                else {
                    $('#message').html('<div class="alert alert-danger">' + res.message + '</div>');
                    console.log(res);
                }
            },
            error: function (res) {
                $('#message').html('<div class="alert alert-danger">' + 'Đã xảy ra lỗi' + '</div>');
                console.log(res);
            }
        });
    }
    Position.edit = edit;
    function save() {
        var id = $('#id').val();
        if (id === null || id === undefined || id == '' || id == 0) {
            add();
        }
        else {
            edit(id);
        }
    }
    Position.save = save;
    function openDeleteModal(id) {
        $('#deleteId').val(id);
        $('#deleteModal').modal('show');
    }
    Position.openDeleteModal = openDeleteModal;
    function remove() {
        var id = $('#deleteId').val();
        console.log(id);
        $.ajax({
            url: "/DepartPosit/Delete/".concat(id),
            type: 'POST',
            data: {},
            success: function (res) {
                if (res.success) {
                    $('#deleteModal').modal('hide');
                    location.reload();
                }
                else {
                    $('#deleteMessage').html('<div class="alert alert-danger">' + res.message + '</div>');
                }
            },
            error: function (res) {
                $('#deleteMessage').html('<div class="alert alert-danger">' + 'Đã xảy ra lỗi' + '</div>');
                console.log(res);
            }
        });
    }
    Position.remove = remove;
})(Position || (Position = {}));
//# sourceMappingURL=position.js.map