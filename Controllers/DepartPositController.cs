using HRM.Data.Enums;
using HRM.Data.Services.Interfaces;
using HRM.Data.ViewModels;
using HRM.Models;
using Microsoft.AspNetCore.Mvc;

namespace HRM.Controllers
{
    public class DepartPositController : Controller
    {
        private readonly IDepartPositService _service;

        public DepartPositController(IDepartPositService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<JsonResult> GetAllAsync(string type)
        {
            var data = await _service.GetAllAsync(x => x.Type == type);
            return Json(data);
        }
        [HttpGet]
        public async Task<JsonResult> GetById(int Id, string type)
        {
            var data = await _service.GetAllAsync(x => x.Type == type);
            var rs = data.FirstOrDefault(x => x.Id == Id);

            if(rs == null)
            {
                return Json(new { success = false, message = "Không tìm thấy thông tin" });
            }

            return Json(rs);
        }

        [HttpPost]
        public async Task<JsonResult> Create(DepartmentPosition obj)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Hãy nhập đúng thông tin" });
            }
            if (await _service.CheckExits(x => x.Code == obj.Code && x.Type == obj.Type))
            {
                return Json(new { success = false, message = obj.Type + " đã tồn tại" });
            }

            await _service.AddAsync(obj);
            return Json(new { success = true, message = "Thêm mới thành công" });
        }

        [HttpPost]
        public async Task<JsonResult> Edit(int id, DepartmentPosition obj)
        {
            if (!await _service.CheckExits(d => d.Id == id)) return Json(new { success = false, message = "Không tìm thấy phòng ban" });

            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(id, obj);
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
