using HRM.Data.Services;
using HRM.Data.ViewModels;
using HRM.Models;
using Microsoft.AspNetCore.Mvc;

namespace HRM.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _service;

        public DepartmentController(IDepartmentService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var departRs = new DepartmentVM()
            {
                Departments = await _service.GetAllAsync()
            };
            return View("Index", departRs);
        }

        [HttpPost]
        public async Task<JsonResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
                if (await _service.CheckExits(d => d.Code == department.Code))
                {
                    return Json(new { success = false, message = "Mã phòng ban đã tồn tài" });
                }
                var data = new Department()
                {
                    Code = department.Code,
                    Name = department.Name,
                    CreateBy = department.CreateBy,
                    CreateAt = DateTime.Now
                };

                await _service.AddAsync(data);
                return Json(new { success = true, message = "Thêm mới thành công" });
            }
            else
            {
                return Json(new { success = false, message = "Hãy nhập đủ thông tin" });
            }
        }

        [HttpPost]
        public async Task<JsonResult> Edit(int id, Department department)
        {
            if (!await _service.CheckExits(d => d.Id == id)) return Json(new { success = false, message = "Không tìm thấy phòng ban" });

            if(ModelState.IsValid)
            {
                await _service.UpdateAsync(id, department);
                return Json(new { success = true, message = "Chỉnh sửa thành công" });
            }
            else
            {
                return Json(new { success = false, message = "Hãy nhập đủ thông tin" });
            }
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            if (!await _service.CheckExits(d => d.Id == id)) return Json(new { success = false, message = "Không tìm thấy phòng ban" });
            try
            {
                await _service.DeleteAsync(id);
                return Json(new { success = true, message = "Xóa thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Xóa không thành công" });
            }
        }
    }
}
