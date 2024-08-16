using KutuphaneOtomasyonu.Models;
using KutuphaneOtomasyonu.Service.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KutuphaneOtomasyonu.Controllers
{
    public class HomeController : Controller
    {
        private readonly IKitapService kitapService;

        public HomeController(IKitapService kitapservice)
        {
            this.kitapService = kitapservice;
        }

        public async Task<IActionResult> Index()
        {
            var kitap = await kitapService.GetAllKitapAsync();
            return View(kitap);
        }

    }
}
