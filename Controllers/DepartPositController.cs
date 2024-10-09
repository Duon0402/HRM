using AspNetCoreHero.ToastNotification.Abstractions;
using HRM.Data.Services.Interfaces;
using HRM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRM.Controllers
{
    [Authorize]
    public class DepartPositController : Controller
    {
        private readonly IDepartPositService _service;
        private readonly INotyfService _notyf;

        public DepartPositController(IDepartPositService service, INotyfService notyf)
        {
            _service = service;
            _notyf = notyf;
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
        public async Task<JsonResult> GetById(int Id)
        {
            var rs = await _service.GetByIdAsync(Id);

            if(rs == null)
            {
                return Json(new { success = false, message = "Không tìm thấy thông tin" });
            }
            return Json( new { success = true, data = rs });
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
                _notyf.Warning(obj.Type + " đã tồn tại", 3);
                return Json(new { success = false, message = obj.Type + " đã tồn tại" });
            }

            await _service.AddAsync(obj);
            _notyf.Success("Thêm mới thành công", 3);
            return Json(new { success = true, message = "Thêm mới thành công" });
        }

        [HttpPost]
        public async Task<JsonResult> Edit(int id, DepartmentPosition obj)
        {
            if (!await _service.CheckExits(d => d.Id == id))
            {
                _notyf.Warning("Không tìm thấy thông tin", 3);
                return Json(new { success = false, message = "Không tìm thấy thông tin" });
            }

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
                _notyf.Success("Xóa thành công", 3);
                return Json(new { success = true, message = "Xóa thành công" });
            }
            catch
            {
                return Json(new { success = false, message = "Xóa không thành công" });
            }
        }
    }
}
