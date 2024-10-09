namespace Salary {
    export interface EmployeeSalary {
        id?: number;
        employeeId: number;
        salary: number;
        startDate: string;
        endDate: string;
    }
    export function openAddOrEditModal(id: number | null): void {
        if (id === undefined || id === null) {
            resetForm();
            $('#code').prop('readonly', false);
            $('#addOrEditModal').modal('show');
        } else {
            $.ajax({
                url: `/EmpSalary/GetById/${id}`,
                type: 'GET',
                success: function (res: { success: boolean; data: EmployeeSalary }) {
                    if (res.success) {
                        console.log(res.data);
                        fillForm(res.data);
                        $('#addOrEditModal').modal('show');
                    } else {
                        console.error(res);
                    }
                },
                error: function (err: any) {
                    console.error(err);
                }
            });
        }
    }
    export function resetForm(): void {
        $('#id').val('');
        $('#empId').val('');
        $('#salary').val('');
        $('#startDate').val('');
        $('#endDate').val('');
        $('#message').html('');
    }
    export function fillForm(data: EmployeeSalary): void {
        $('#id').val(data.id);
        $('#empId').val(data.employeeId);
        $('#salary').val(data.salary);
        $('#startDate').val(formatDate(data.startDate));
        $('#endDate').val(formatDate(data.endDate));
    }
    export function formatDate(date: any): string {
        var dateFormat = date.split("T")[0];
        return dateFormat;
    }
    export function add(): void {
        var data = {
            employeeId: $('#empId').val(),
            salary: $('#salary').val(),
            startDate: $('#startDate').val(),
            endDate: $('#endDate').val(),
        }
        console.log(data);
        $.ajax({
            url: '/EmpSalary/Create',
            type: 'POST',
            data: data,
            success: function (res) {
                if (res.success) {
                    $('#addOrEditModal').modal('hide');
                    location.reload();
                } else {
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
    export function edit(id: any): void {
        var data = {
            id: $('#id').val(),
            employeeId: $('#empId').val(),
            salary: $('#salary').val(),
            startDate: $('#startDate').val(),
            endDate: $('#endDate').val(),
        }
        $.ajax({
            url: `/EmpSalary/Edit/${id}`,
            type: 'POST',
            data: data,
            success: function (res) {
                if (res.success) {
                    $('#addOrEditModal').modal('hide');
                    location.reload();
                } else {
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
    export function save(): void {
        var id = $('#id').val();
        if (id === null || id === undefined || id == '' || id == 0) {
            add();
        }
        else {
            edit(id);
        }
    }
    export function openDeleteModal(id: number): void {
        $('#deleteId').val(id);
        $('#deleteModal').modal('show');
    }
    export function remove() {
        var id = $('#deleteId').val();
        console.log(id);
        $.ajax({
            url: `/EmpSalary/Delete/${id}`,
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
        })
    }
}

