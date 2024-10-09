namespace Position {
    export interface Position {
        id: number,
        code: string,
        name: string,
        type: string
    }
    export function openAddOrEditModal(id: number | null) {
        if (id == null || id == undefined) {
            resetForm();
            $('#code').prop('readonly', false);
            $('#addOrEditModal').modal('show');
        }
        else {
            $.ajax({
                url: `/DepartPosit/GetById/${id}`,
                type: 'GET',
                success: function (res: { success: boolean; data: Position }) {
                    if (res.success) {
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
        $('#id').val('');
        $('#code').val('');
        $('#name').val('');
        $('#message').html('');
    }
    export function fillForm(data: Position) {
        $('#id').val(data.id);
        $('#code').val(data.code);
        $('#name').val(data.name);
    }

    export function add() {
        var data = {
            code: $('#code').val(),
            name: $('#name').val(),
            type: 'Chức vụ'
        }
        console.log(data);
        $.ajax({
            url: '/DepartPosit/Create',
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
            type: 'Chức vụ',
            name: $('#name').val(),
        }
        $.ajax({
            url: `/DepartPosit/Edit/${id}`,
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
            url: `/DepartPosit/Delete/${id}`,
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
