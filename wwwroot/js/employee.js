var Employee;
(function (Employee) {
    function openAddOrEditModal(id) {
        if (id == null || id == undefined) {
            resetForm();
            $('#code').prop('readonly', false);
            $('#addOrEditModal').modal('show');
        }
        else {
            $.ajax({
                url: "/Employee/GetById/".concat(id),
                type: 'GET',
                success: function (res) {
                    if (res.success) {
                        console.log(res.data);
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
    Employee.openAddOrEditModal = openAddOrEditModal;
    function resetForm() {
        $('#code').prop('readonly', false);
        $('#id').val('');
        $('#code').val('');
        $('#name').val('');
        $('#departmentId').val('');
        $('#positionId').val('');
        $('#dateOfBirth').val('');
        $('#gender').val('');
        $('#phoneNumber').val('');
        $('#email').val('');
        $('#address').val('');
        $('#identifier').val('');
        $('#message').html('');
    }
    Employee.resetForm = resetForm;
    function fillForm(data) {
        $('#id').val(data.id);
        $('#code').val(data.code);
        $('#name').val(data.name);
        $('#departmentId').val(data.departmentId);
        $('#positionId').val(data.positionId);
        $('#dateOfBirth').val(formatDate(data.dateOfBirth));
        $('#gender').val(data.gender);
        $('#phoneNumber').val(data.phoneNumber);
        $('#email').val(data.email);
        $('#address').val(data.address);
        $('#identifier').val(data.identifier);
    }
    Employee.fillForm = fillForm;
    function formatDate(date) {
        var dateFormat = date.split("T")[0];
        return dateFormat;
    }
    Employee.formatDate = formatDate;
    function openDeleteModal(id) {
        $('#deleteId').val(id);
        $('#deleteModal').modal('show');
    }
    Employee.openDeleteModal = openDeleteModal;
    function remove() {
        var id = $('#deleteId').val();
        console.log(id);
        $.ajax({
            url: "/Employee/Delete/".concat(id),
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
    Employee.remove = remove;
    function add() {
        var data = {
            code: $('#code').val(),
            name: $('#name').val(),
            deparmentId: $('#deparmentId').val(),
            positionId: $('#positionId').val(),
            dateOfBirth: $('#dateOfBirth').val(),
            gender: $('#gender').val(),
            phoneNumber: $('#phoneNumber').val(),
            email: $('#email').val(),
            address: $('#address').val(),
            identifier: $('#identifier').val(),
        };
        console.log(data);
        $.ajax({
            url: '/Employee/Create',
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
    Employee.add = add;
    function edit(id) {
        var data = {
            id: $('#id').val(),
            code: $('#code').val(),
            name: $('#name').val(),
            deparmentId: $('#deparmentId').val(),
            positionId: $('#positionId').val(),
            dateOfBirth: $('#dateOfBirth').val(),
            gender: $('#gender').val(),
            phoneNumber: $('#phoneNumber').val(),
            email: $('#email').val(),
            address: $('#address').val(),
            identifier: $('#identifier').val(),
        };
        $.ajax({
            url: "/Employee/Edit/".concat(id),
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
    Employee.edit = edit;
    function save() {
        var id = $('#id').val();
        if (id === null || id === undefined || id == 0 || id == '') {
            add();
        }
        else {
            edit(id);
        }
    }
    Employee.save = save;
})(Employee || (Employee = {}));
//# sourceMappingURL=employee.js.map