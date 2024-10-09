namespace Employee {
    export interface Employee {
        id?: number;
        code: string;
        name: string;
        departmentId: number;
        positionId: number;
        dateOfBirth: string;
        gender: string;
        phoneNumber: string;
        email: string;
        address: string;
        identifier: string;
    }
    export function openAddOrEditModal(id: number | null) {
        if (id == null || id == undefined) {
            resetForm();
            $('#code').prop('readonly', false);
            $('#addOrEditModal').modal('show');
        }
        else {
            $.ajax({
                url: `/Employee/GetById/${id}`,
                type: 'GET',
                success: function (res: { success: boolean; data: Employee }) {
                    if (res.success) {
                        console.log(res.data);
                        fillForm(res.data);
                        $('#code').prop('readonly', true);
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
    export function resetForm() {
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
    export function fillForm(data: Employee) {
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
    export function formatDate(date: any): string {
        var dateFormat = date.split("T")[0];
        return dateFormat;
    }
    export function openDeleteModal(id: number): void {
        $('#deleteId').val(id);
        $('#deleteModal').modal('show');
    }
    export function remove() {
        var id = $('#deleteId').val();
        console.log(id);
        $.ajax({
            url: `/Employee/Delete/${id}`,
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
    export function add() {
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
        }
        console.log(data);
        $.ajax({
            url: '/Employee/Create',
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
    export function edit(id: any) {
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
        }
        $.ajax({
            url: `/Employee/Edit/${id}`,
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
    export function save() {
        var id = $('#id').val();
        if (id === null || id === undefined || id == 0 || id == '') {
            add();
        }
        else {
            edit(id);
        }
    }
}