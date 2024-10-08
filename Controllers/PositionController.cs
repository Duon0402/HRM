using HRM.Data.Services.Interfaces;
using HRM.Data.ViewModels;
using HRM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRM.Controllers
{
    [Authorize]
    public class PositionController : Controller
    {
        private readonly IPositionService _service;
        private readonly IDepartPositService _departPositService;

        public PositionController(IPositionService service, IDepartPositService departPositService)
        {
            _service = service;
            _departPositService = departPositService;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _departPositService.GetAllAsync();
            var results = new PositionVM()
            {
                Positions = data.Where(x => x.Type == "Chức vụ")
            };
            return View("Index", results);
        }
        //[HttpPost]
        //public async Task<JsonResult> Create(Position position)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (await _service.CheckExits(d => d.Code == position.Code))
        //        {
        //            return Json(new { success = false, message = "Mã chức vụ đã tồn tài" });
        //        }
        //        var data = new Position()
        //        {
        //            Code = position.Code,
        //            Name = position.Name,
        //            CreateBy = position.CreateBy,
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
        //public async Task<JsonResult> Edit(int id, Position position)
        //{
        //    if (!await _service.CheckExits(d => d.Id == id)) return Json(new { success = false, message = "Không tìm thấy chức vụ" });

        //    if (ModelState.IsValid)
        //    {
        //        await _service.UpdateAsync(id, position);
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
        //    if (!await _service.CheckExits(d => d.Id == id)) return Json(new { success = false, message = "Không tìm thấy chức vụ" });
        //    try
        //    {
        //        await _service.DeleteAsync(id);
        //        return Json(new { success = true, message = "Xóa thành công" });
        //    }
        //    catch
        //    {
        //        return Json(new { success = false, message = "Xóa không thành công" });
        //    }
        //}
    }
}
