using HRM.Data;
using HRM.Data.Services.Interfaces;
using HRM.Data.ViewModels;
using HRM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HRM.Controllers
{
    public class EmpSalaryController : Controller
    {
        private readonly IEmpSalaryService _service;
        private readonly IEmployeeService _empService;
        private readonly DataContext _context;

        public EmpSalaryController(IEmpSalaryService service, IEmployeeService empService,
            DataContext context)
        {
            _service = service;
            _empService = empService;
            _context = context;
        }
        public async Task<IActionResult> Index(string? empCode, DateTime? startDate, DateTime? endDate)
        {
            ViewData["EmpCodeList"] = new SelectList(_context.Employees.Where(x => !x.IsDeleted), "Id", "Code");

            var query = _context.EmpSalaries.Where(x => !x.IsDeleted).Include(e => e.Employee).AsQueryable();

            if (!string.IsNullOrWhiteSpace(empCode)) { query = query.Where(d => d.Employee!.Code == empCode); }
            if (startDate.HasValue)
            {
                query = query.Where(s => s.StartDate >= startDate.Value);
            }
            if (endDate.HasValue)
            {
                query = query.Where(s => s.EndDate <= endDate.Value);
            }
            var rsVM = new EmpSalaryVM()
            {
                EmpSalaries = await query.ToListAsync()
            };

            return View(rsVM);
        }

        [HttpPost]
        public async Task<JsonResult> Create(EmpSalary empSalary)
        {
            if (!ModelState.IsValid) return Json(new { success = false, message = "Hãy nhập đủ thông tin" });

            if (!await _empService.CheckExits(e => e.Id == empSalary.EmployeeId))
                return Json(new { success = false, message = "Không tìm thấy nhân viên" });

            if (empSalary.Salary < 0)
                return Json(new { success = false, message = "Lương phải lớn hơn 0" });

            if (!await _service.ValidateDate(empSalary))
                return Json(new { success = false, message = "Khoảng thời gian không hợp lệ hoặc bị trùng lặp." });

            await _service.AddAsync(empSalary);
            return Json(new { success = true, message = "Thêm mới thành công" });
        }


        [HttpPost]
        public async Task<JsonResult> Edit(int id, EmpSalary empSalary)
        {
            if (!await _service.CheckExits(s => s.Id == id))
                return Json(new { success = false, message = "Không tìm thấy thông tin" });

            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Hãy nhập đủ thông tin" });

            if (empSalary.Salary < 0)
                return Json(new { success = false, message = "Lương phải lớn hơn 0" });

            if (!await _empService.CheckExits(e => e.Id == empSalary.EmployeeId))
                return Json(new { success = false, message = "Không tìm thấy nhân viên" });

            if (!await _service.ValidateDate(empSalary))
                return Json(new { success = false, message = "Khoảng thời gian không hợp lệ hoặc đã tồn tại" });

            await _service.UpdateAsync(id, empSalary);
            return Json(new { success = true, message = "Sửa thành công" });
        }


        [HttpGet]
        public async Task<JsonResult> GetById(int id)
        {
            var rs = await _service.GetByIdAsync(id);
            if (rs != null) return Json(new { success = true, data = rs });
            return Json(new { success = false, message = "Không tìm thấy thông tin" });
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            if(!await _service.CheckExits(s => s.Id == id)) return Json(new { success = false, message = "Không tìm thấy thông tin" });
            await _service.DeleteAsync(id);
            return Json(new { success = true, message = "Xóa thành công" });
        }
    }
}
