using AutoMapper;
using KutuphaneOtomasyonu.Data.UnitOfWorks;
using KutuphaneOtomasyonu.Entity.Dtos.Kitaps;
using KutuphaneOtomasyonu.Entity.Dtos.OkunanKitaplars;
using KutuphaneOtomasyonu.Entity.Entities;
using KutuphaneOtomasyonu.Service.Services.Abstractions;
using KutuphaneOtomasyonu.Service.Services.Concretes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;

namespace KutuphaneOtomasyonu.Web.Areas.Ogretmen.Controllers
{
    [Area("Ogretmen")]
    [Authorize(Roles = "Ogretmen")]
    public class OgretmenController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IKitapService kitapService;
        private readonly IKitaplikService kitaplikService;
        private readonly IOkunanKitaplarService okunanKitaplarService;
        private IMapper mapper;

        public OgretmenController(IKitapService kitapservice, IUnitOfWork unitOfWork, IKitaplikService kitaplikservice, IMapper mapper, IOkunanKitaplarService okunanKitaplarService)
        {
            this.kitapService = kitapservice;
            this.kitaplikService = kitaplikservice;
            this.mapper = mapper;
            this.okunanKitaplarService = okunanKitaplarService;
            this.unitOfWork= unitOfWork;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            var kitap = await kitapService.GetAllKitapAsync();
            return View(kitap.ToPagedList(page, 5));
        }
        [HttpGet]
        public async Task<IActionResult> Add(int page = 1)
        {
            var kitapliks = await kitaplikService.GetAllKitaplikAsync();         

            return View(new KitapAddDto { Kitapliks = kitapliks });
        }
        [HttpPost]
        public async Task<IActionResult> Add(KitapAddDto kitapAddDto)
        {
            var existingRecords = await unitOfWork.GetRepository<Kitap>()
               .GetAllAsync(x => x.Ad == kitapAddDto.Ad && x.KitaplikId == kitapAddDto.KitaplikId);

            if (existingRecords.Any())
            {
                TempData["Message"] = "Bu kitabı daha önce eklenmiş.";
                TempData["MessageType"] = "danger";
                return RedirectToAction("Add", "Ogretmen", new { Area = "Ogretmen" });
            }
            try
            {
                await kitapService.CreateKitapAsync(kitapAddDto);
                TempData["Message"] = "Kitap başarıyla eklendi.";
                TempData["MessageType"] = "success";
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Kitap ekleme işlemi başarısız oldu ";
                TempData["MessageType"] = "danger";
            }

            var kitapliks = await kitaplikService.GetAllKitaplikAsync();
           

            return View(new KitapAddDto { Kitapliks = kitapliks });
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var kitapliks = await kitaplikService.GetAllKitaplikAsync();
            var kitapDto = await kitapService.GetKitapById(id); // KitapDto al
            var kitapUpdateDto = mapper.Map<KitapUpdateDto>(kitapDto); // KitapDto'dan KitapUpdateDto'ya dönüşüm
            kitapUpdateDto.Kitapliks = kitapliks;
            // Güncelleme formunu görüntüleme
            return View(kitapUpdateDto);
        }
        [HttpPost]
        public async Task<IActionResult> Update(KitapUpdateDto kitapUpdateDto)
        {
            try
            {
                await kitapService.UpdateKitapAsync(kitapUpdateDto);
                TempData["Message"] = "Kitap başarıyla güncellendi.";
                TempData["MessageType"] = "success";
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Kitap güncelleme işlemi başarısız oldu ";
                TempData["MessageType"] = "danger";
            }

            return RedirectToAction("Update", new { id = kitapUpdateDto.Id });
        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await kitapService.DeleteKitapAsync(id);
                TempData["Message"] = "kitap başarıyla silindi.";
                TempData["MessageType"] = "success";
            }
            catch (Exception ex)
            {
                TempData["Message"] = "kitap silme işlemi başarısız oldu ";
                TempData["MessageType"] = "danger";
            }


            return RedirectToAction("Index", "Ogretmen", new { Area = "Ogretmen" });
        }
        public async Task<IActionResult> KitapDetay(int id)
        {
            var kitap= await kitapService.GetKitapById(id);
            var kitapDto = mapper.Map<KitapDto>(kitap);
            return View(kitapDto);
        }
        public async Task<IActionResult> UserDetay(int id, int page = 1)
        {
            var okunanKitaplar = await okunanKitaplarService.GetOkunanKitaplarByAppUserIdAsync(id);
            var okunanKitaplarDtos = mapper.Map<List<OkunanKitaplarDto>>(okunanKitaplar);

            return View(okunanKitaplarDtos.ToPagedList(page, 5));
        }
    }
}
