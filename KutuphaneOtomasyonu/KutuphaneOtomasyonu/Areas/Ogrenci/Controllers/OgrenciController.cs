using AutoMapper;
using KutuphaneOtomasyonu.Entity.Dtos.Kitaps;
using KutuphaneOtomasyonu.Entity.Dtos.OkunanKitaplars;
using KutuphaneOtomasyonu.Entity.Entities;
using KutuphaneOtomasyonu.Service.Extensions;
using KutuphaneOtomasyonu.Service.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList.Extensions;

namespace KutuphaneOtomasyonu.Web.Areas.Ogrenci.Controllers
{
    [Area("Ogrenci")]
    [Authorize(Roles = "Ogrenci")]
    public class OgrenciController : Controller
    {
        private readonly IKitapService kitapService;
        private readonly IKitaplikService kitaplikService;
        private readonly IOkunanKitaplarService okunanKitaplarService;
        private IMapper mapper;

        public OgrenciController(IMapper mapper, IKitaplikService kitaplikService, IOkunanKitaplarService okunanKitaplarService, IKitapService kitapService)
        {
            this.mapper = mapper;
            this.kitaplikService = kitaplikService;
            this.okunanKitaplarService = okunanKitaplarService;
            this.kitapService = kitapService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var okunanKitaplars = await okunanKitaplarService.GetAllOkunanKitaplarsByAppUserAsync();
            var okunankitaplarDto = mapper.Map<List<OkunanKitaplarDto>>(okunanKitaplars);

            return View(okunankitaplarDto.ToPagedList(page, 5));
        }

        [HttpGet]
        public async Task<IActionResult> Add(int page = 1)
        {
            var kitaps = await kitapService.GetAllKitapAsync();
            var kitapDtos = mapper.Map<List<KitapDto>>(kitaps);

            return View(kitapDtos.ToPagedList(page, 5));
        }

        [HttpPost]
        public async Task<IActionResult> Add(OkunanKitaplarAddDto okunanKitaplarAddDto)
        {
            try
            {
                await okunanKitaplarService.CreateOkunanKitaplarAsync(okunanKitaplarAddDto);
                TempData["Message"] = "Kitap başarıyla eklendi.";
                TempData["MessageType"] = "success";
            }
            catch (InvalidOperationException ex)
            {
                TempData["Message"] = ex.Message;
                TempData["MessageType"] = "warning";
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Kitap ekleme işlemi başarısız oldu.";
                TempData["MessageType"] = "danger";
            }

            return RedirectToAction("Index", "Ogrenci");
        }

        [HttpGet]
        public async Task<IActionResult> AddGorus(int id)
        {
            var okunanKitaplar = await okunanKitaplarService.GetOkunanKitaplarById(id);
            var okunanKitaplarUpdateDto = mapper.Map<OkunanKitaplarUpdateDto>(okunanKitaplar);

            return View(okunanKitaplarUpdateDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddGorus(OkunanKitaplarUpdateDto okunanKitaplarUpdateDto)
        {
            try
            {
                await okunanKitaplarService.UpdateOkunanKitaplarAsync(okunanKitaplarUpdateDto);
                TempData["Message"] = "Görüş başarıyla eklendi.";
                TempData["MessageType"] = "success";
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Görüş ekleme işlemi başarısız oldu.";
                TempData["MessageType"] = "danger";
            }

            return RedirectToAction("AddGorus", new { id = okunanKitaplarUpdateDto.Id });
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await okunanKitaplarService.DeleteKitapAsync(id);
                TempData["Message"] = "Kitap başarıyla silindi.";
                TempData["MessageType"] = "success";
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Kitap silme işlemi başarısız oldu.";
                TempData["MessageType"] = "danger";
            }

            return RedirectToAction("Index", "Ogrenci", new { Area = "Ogrenci" });
        }

        public async Task<IActionResult> KitapDetay(int id)
        {
            var kitap = await kitapService.GetKitapById(id);
            var kitapDto = mapper.Map<KitapDto>(kitap);
            return View(kitapDto);
        }
    }
}
