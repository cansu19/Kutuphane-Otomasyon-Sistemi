using AutoMapper;
using KutuphaneOtomasyonu.Data.UnitOfWorks;
using KutuphaneOtomasyonu.Entity.Dtos.Kitapliks;
using KutuphaneOtomasyonu.Entity.Dtos.Kitaps;
using KutuphaneOtomasyonu.Entity.Dtos.OkunanKitaplars;
using KutuphaneOtomasyonu.Entity.Entities;
using KutuphaneOtomasyonu.Service.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;

namespace KutuphaneOtomasyonu.Web.Areas.Ogretmen.Controllers
{
    [Area("Ogretmen")]
    [Authorize(Roles = "Ogretmen")]
    public class KitaplikController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IKitapService kitapService;
        private readonly IKitaplikService kitaplikService;
        private readonly IOkunanKitaplarService okunanKitaplarService;
        private IMapper mapper;
        public KitaplikController( IKitaplikService kitaplikservice, IUnitOfWork unitOfWork, IMapper mapper, IKitapService kitapService, IOkunanKitaplarService okunanKitaplarService)
        {
            this.kitaplikService = kitaplikservice;
            this.kitapService = kitapService;
            this.okunanKitaplarService = okunanKitaplarService;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            var kitaplik = await kitaplikService.GetAllKitaplikAsync();
            return View(kitaplik.ToPagedList(page,5));
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(KitaplikAddDto kitaplikAddDto)
        {
            var existingRecords = await unitOfWork.GetRepository<Kitaplik>()
                .GetAllAsync(x => x.Tur == kitaplikAddDto.Tur);

            if (existingRecords.Any())
            {
                TempData["Message"] = "Bu kitaplık daha önce eklendi.";
                TempData["MessageType"] = "danger";
                return RedirectToAction("Add", "Kitaplik", new { Area = "Ogretmen" });
            }

            try
            {
                await kitaplikService.CreateKitaplikAsync(kitaplikAddDto);
                TempData["Message"] = "Kitaplık başarıyla eklendi.";
                TempData["MessageType"] = "success";
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Kitaplık ekleme işlemi başarısız oldu.";
                TempData["MessageType"] = "danger";
            }

            return RedirectToAction("Index", "Kitaplik", new { Area = "Ogretmen" });
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var kitaplik = await kitaplikService.GetKitaplikById(id);
            return View(new KitaplikUpdateDto() { Id = kitaplik.Id, Tur = kitaplik.Tur });
        }
        [HttpPost]
        public async Task<IActionResult> Update(KitaplikUpdateDto kitaplikUpdateDto)
        {
            try
            {
                await kitaplikService.UpdateKitaplikAsync(kitaplikUpdateDto);
                TempData["Message"] = "Kitaplık başarıyla güncellendi.";
                TempData["MessageType"] = "success";
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Kitaplık güncelleme işlemi başarısız oldu ";
                TempData["MessageType"] = "danger";
            }
            return RedirectToAction("Index", "Kitaplik", new { Area = "Ogretmen" });
        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await kitaplikService.DeleteKitaplikAsync(id);
                TempData["Message"] = "Kitaplık başarıyla silindi.";
                TempData["MessageType"] = "success";
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Kitaplık silme işlemi başarısız oldu ";
                TempData["MessageType"] = "danger";
            }

            return RedirectToAction("Index", "Kitaplik", new { Area = "Ogretmen" });
        }
        public async Task<IActionResult> KitaplikDetay(int id, int page = 1)
        {
            var kitap = await kitapService.GetKitapByKitaplikAsync(id);
            var kitapDto = mapper.Map<List<KitapDto>>(kitap);
            return View(kitapDto.ToPagedList(page, 5));
        }
        public async Task<IActionResult> KitapGorus(int id, int page=1)
        {
            var okunanKitaplar = await okunanKitaplarService.GetOkunanKitaplarByKitapAsync(id);
            var okunanKitaplarDto = mapper.Map<List<OkunanKitaplarDto>>(okunanKitaplar);
            return View(okunanKitaplarDto.ToPagedList(page, 5));
        }
    }
}
