﻿using HRM.Data.Services;
using HRM.Data.Services.Interfaces;
using HRM.Data.ViewModels;
using HRM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRM.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _service;
        private readonly IDepartPositService _departPositService;

        public DepartmentController(IDepartmentService service, IDepartPositService departPositService)
        {
            _service = service;
            _departPositService = departPositService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _departPositService.GetAllAsync();
            var departRs = new DepartmentVM()
            {
                Departments = data.Where(x => x.Type == "Phòng ban")
            };
            return View("Index", departRs);
        }

        //[HttpPost]
        //public async Task<JsonResult> Create(Department department)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (await _service.CheckExits(d => d.Code == department.Code))
        //        {
        //            return Json(new { success = false, message = "Mã phòng ban đã tồn tài" });
        //        }
        //        var data = new Department()
        //        {
        //            Code = department.Code,
        //            Name = department.Name,
        //            CreateBy = department.CreateBy,
        //            CreateAt = DateTime.Now
        //        };

        //        await _service.AddAsync(data);
        //        return Json(new { success = true, message = "Thêm mới thành công" });
        //    }
        //    else
        //    {
        //        return Json(new { success = false, message = "Hãy nhập đủ thông tin" });
        //    }
        //}

        //[HttpPost]
        //public async Task<JsonResult> Edit(int id, Department department)
        //{
        //    if (!await _service.CheckExits(d => d.Id == id)) return Json(new { success = false, message = "Không tìm thấy phòng ban" });

        //    if(ModelState.IsValid)
        //    {
        //        await _service.UpdateAsync(id, department);
        //        return Json(new { success = true, message = "Chỉnh sửa thành công" });
        //    }
        //    else
        //    {
        //        return Json(new { success = false, message = "Hãy nhập đủ thông tin" });
        //    }
        //}

        //[HttpPost]
        //public async Task<JsonResult> Delete(int id)
        //{
        //    if (!await _service.CheckExits(d => d.Id == id)) return Json(new { success = false, message = "Không tìm thấy phòng ban" });
        //    try
        //    {
        //        await _service.DeleteAsync(id);
        //        return Json(new { success = true, message = "Xóa thành công" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, message = "Xóa không thành công" });
        //    }
        //}
    }
}
