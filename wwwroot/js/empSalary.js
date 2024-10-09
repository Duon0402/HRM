var Salary;
(function (Salary) {
    function openAddOrEditModal(id) {
        if (id === undefined || id === null) {
            resetForm();
            $('#code').prop('readonly', false);
            $('#addOrEditModal').modal('show');
        }
        else {
            $.ajax({
                url: "/EmpSalary/GetById/".concat(id),
                type: 'GET',
                success: function (res) {
                    if (res.success) {
                        console.log(res.data);
                        fillForm(res.data);
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
    Salary.openAddOrEditModal = openAddOrEditModal;
    function resetForm() {
        $('#id').val('');
        $('#empId').val('');
        $('#salary').val('');
        $('#startDate').val('');
        $('#endDate').val('');
        $('#message').html('');
    }
    Salary.resetForm = resetForm;
    function fillForm(data) {
        $('#id').val(data.id);
        $('#empId').val(data.employeeId);
        $('#salary').val(data.salary);
        $('#startDate').val(formatDate(data.startDate));
        $('#endDate').val(formatDate(data.endDate));
    }
    Salary.fillForm = fillForm;
    function formatDate(date) {
        var dateFormat = date.split("T")[0];
        return dateFormat;
    }
    Salary.formatDate = formatDate;
    function add() {
        var data = {
            employeeId: $('#empId').val(),
            salary: $('#salary').val(),
            startDate: $('#startDate').val(),
            endDate: $('#endDate').val(),
        };
        console.log(data);
        $.ajax({
            url: '/EmpSalary/Create',
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
    Salary.add = add;
    function edit(id) {
        var data = {
            id: $('#id').val(),
            employeeId: $('#empId').val(),
            salary: $('#salary').val(),
            startDate: $('#startDate').val(),
            endDate: $('#endDate').val(),
        };
        $.ajax({
            url: "/EmpSalary/Edit/".concat(id),
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
    Salary.edit = edit;
    function save() {
        var id = $('#id').val();
        if (id === null || id === undefined || id == '' || id == 0) {
            add();
        }
        else {
            edit(id);
        }
    }
    Salary.save = save;
    function openDeleteModal(id) {
        $('#deleteId').val(id);
        $('#deleteModal').modal('show');
    }
    Salary.openDeleteModal = openDeleteModal;
    function remove() {
        var id = $('#deleteId').val();
        console.log(id);
        $.ajax({
            url: "/EmpSalary/Delete/".concat(id),
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
    Salary.remove = remove;
})(Salary || (Salary = {}));
//# sourceMappingURL=empSalary.js.map