using KutuphaneOtomasyonu.Entity.Dtos.AppUsers;
using KutuphaneOtomasyonu.Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KutuphaneOtomasyonu.Web.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterOgrenci(AppUserRegisterDto appUserRegisterDto)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    UserName = appUserRegisterDto.Username
                };

                var result = await _userManager.CreateAsync(user, appUserRegisterDto.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Ogrenci");
                    TempData["Message"] = "Kayıt başarılı. Lütfen giriş yapınız.";
                    TempData["MessageType"] = "success";
                    return RedirectToAction("Index", "Login");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("Hatalı", error.Description);
                }
            }

            TempData["Message"] = "Kayıt başarısız. Lütfen bilgilerinizi kontrol ediniz.";
            TempData["MessageType"] = "danger";
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RegisterOgretmen(AppUserRegisterDto appUserRegisterDto)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    UserName = appUserRegisterDto.Username
                };

                var result = await _userManager.CreateAsync(user, appUserRegisterDto.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Ogretmen");
                    TempData["Message"] = "Kayıt başarılı. Lütfen giriş yapınız.";
                    TempData["MessageType"] = "success";
                    return RedirectToAction("Index", "Login");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("Hatalı", error.Description);
                }
            }

            TempData["Message"] = "Kayıt başarısız. Lütfen bilgilerinizi kontrol ediniz.";
            TempData["MessageType"] = "danger";
            return View("Index");
        }
    }
}
