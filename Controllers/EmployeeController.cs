using HRM.Data;
using HRM.Data.Services;
using HRM.Data.Services.Interfaces;
using HRM.Data.ViewModels;
using HRM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HRM.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _service;
        private readonly DataContext _context;
        private readonly IDepartPositService _departPositService;

        public EmployeeController(IEmployeeService service, DataContext context,
            IDepartPositService departPositService)
        {
            _service = service;
            _context = context;
            _departPositService = departPositService;
        }
        public async Task<IActionResult> Index(int? DepartmentId, string? Name)
        {
            ViewData["DepartmentName"] = new SelectList(_context.DepartmentPositions.Where(x => !x.IsDeleted && x.Type == "Phòng ban"), "Id", "Name");
            ViewData["PositionName"] = new SelectList(_context.DepartmentPositions.Where(x => !x.IsDeleted && x.Type == "Chức vụ"), "Id", "Name");

            var query = _context.Employees
                .Where(x => !x.IsDeleted)
                .AsQueryable();

            if (DepartmentId.HasValue)
            {

                query = query.Where(d => d.DepartmentId == DepartmentId.Value);
                var department = _context.DepartmentPositions.Find(DepartmentId);
                ViewBag.SelectedDepart = new
                {
                    DepartmentId,
                    department?.Name
                };
            }

            if (!string.IsNullOrWhiteSpace(Name))
            {
                query = query.Where(d => d.Name.Contains(Name));
                ViewBag.EmployeeName = Name;
            }
            var data = await _departPositService.GetAllAsync();

            var results = new EmployeeVM()
            {
                Employees = await query.ToListAsync(),
                Departments = data.Where(x => x.Type == "Phòng ban"),
                Positions = data.Where(x => x.Type == "Chức vụ")
            };

            return View(results);
        }


        [HttpPost]
        public async Task<JsonResult> Create(Employee employee)
        {
            employee.CreateBy = User.Identity.Name;
            if (ModelState.IsValid)
            {
                if (await _service.CheckExits(d => d.Code == employee.Code))
                {
                    return Json(new { success = false, message = "Mã nhân viên ban đã tồn tài" });
                }

                await _service.AddAsync(employee);
                return Json(new { success = true, message = "Thêm mới thành công" });
            }
            else
            {
                return Json(new { success = false, message = "Hãy nhập đủ thông tin" });
            }
        }

        [HttpPost]
        public async Task<JsonResult> Edit(int id, Employee employee)
        {
            employee.CreateBy = User.Identity.Name;
            if (!await _service.CheckExits(d => d.Id == id)) return Json(new { success = false, message = "Không tìm thấy nhân sự" });

            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(id, employee);
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
            if (!await _service.CheckExits(d => d.Id == id)) return Json(new { success = false, message = "Không tìm thấy nhân viên" });
            try
            {
                await _service.DeleteAsync(id);
                return Json(new { success = true, message = "Xóa thành công" });
            }
            catch
            {
                return Json(new { success = false, message = "Xóa không thành công" });
            }
        }
    }
}
