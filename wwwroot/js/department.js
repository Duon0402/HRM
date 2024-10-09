var Department;
(function (Department) {
    function openAddOrEditModal(id) {
        if (id == null || id == undefined || id == 0) {
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
    Department.openAddOrEditModal = openAddOrEditModal;
    function resetForm() {
        $('#id').val('');
        $('#code').val('');
        $('#name').val('');
        $('#message').html('');
    }
    Department.resetForm = resetForm;
    function fillForm(data) {
        $('#id').val(data.id);
        $('#code').val(data.code);
        $('#name').val(data.name);
    }
    Department.fillForm = fillForm;
    function add() {
        var data = {
            code: $('#code').val(),
            name: $('#name').val(),
            type: 'Phòng ban'
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
    Department.add = add;
    function edit(id) {
        var data = {
            id: $('#id').val(),
            code: $('#code').val(),
            type: 'Phòng ban',
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
    Department.edit = edit;
    function save() {
        var id = $('#id').val();
        if (id === null || id === undefined || id == '' || id == 0) {
            add();
        }
        else {
            edit(id);
        }
    }
    Department.save = save;
    function openDeleteModal(id) {
        $('#deleteId').val(id);
        $('#deleteModal').modal('show');
    }
    Department.openDeleteModal = openDeleteModal;
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
    Department.remove = remove;
})(Department || (Department = {}));
//# sourceMappingURL=department.js.map