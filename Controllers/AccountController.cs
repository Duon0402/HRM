using HRM.Data;
using HRM.Data.ViewModels;
using HRM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRM.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly DataContext _context;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, DataContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginVM());
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }
            var user = await _userManager.FindByNameAsync(loginVM.Username);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
                    var reusult = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (reusult.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                TempData["Error"] = "Sai tên đăng nhập hoặc mật khẩu";
                return View(loginVM);
            }
            TempData["Error"] = "Sai tên đăng nhập hoặc mật khẩu";
            return View(loginVM);
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterVM());
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);

            var user = await _userManager.FindByNameAsync(registerVM.Username);

            if (user != null)
            {
                TempData["error"] = "Tên đăng nhập đã được sử dụng";
                return View(registerVM);
            }

            var newUser = new AppUser()
            {
                UserName = registerVM.Username
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);
            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, AppUserRole.User);
            }
            return View("Login");
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCurrentUserInfo()
        {
            if (User.Identity.IsAuthenticated)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                var roles = await _userManager.GetRolesAsync(currentUser);

                var profileVM = new ProfileVM
                {
                    Username = currentUser.UserName,
                    Roles = roles
                };

                return View(profileVM);
            }

            return RedirectToAction("Login");
        }
    }
}
