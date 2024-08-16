using KutuphaneOtomasyonu.Entity.Dtos.AppUsers;
using KutuphaneOtomasyonu.Entity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KutuphaneOtomasyonu.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(AppUserLoginDto appuserLoginDto)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(appuserLoginDto.Username) || string.IsNullOrEmpty(appuserLoginDto.Password))
                {
                    ModelState.AddModelError("", "Kullanıcı adı ve şifre alanlarını doldurmalısınız.");
                    return View(appuserLoginDto);
                }

                var result = await _signInManager.PasswordSignInAsync(appuserLoginDto.Username, appuserLoginDto.Password, false, true);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(appuserLoginDto.Username);
                    if (user != null)
                    {
                        if (await _userManager.IsInRoleAsync(user, "Ogretmen"))
                        {
                            TempData["Message"] = "Başarıyla giriş yaptınız.";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("Index", "Ogretmen", new { Area = "Ogretmen" });
                        }
                        else if (await _userManager.IsInRoleAsync(user, "Ogrenci"))
                        {
                            TempData["Message"] = "Başarıyla giriş yaptınız.";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("Index", "Ogrenci", new { Area = "Ogrenci" });
                        }
                    }
                    TempData["Message"] = "E-posta adresiniz veya şifreniz yanlıştır.";
                    TempData["MessageType"] = "danger";
                }
                else
                {
                    TempData["Message"] = "E-posta adresiniz veya şifreniz yanlıştır.";
                    TempData["MessageType"] = "danger";
                }
            }
            return View(appuserLoginDto);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData["Message"] = "Başarıyla çıkış yaptınız.";
            TempData["MessageType"] = "success";
            return RedirectToAction("Index", "Login");
        }
    }
}
