using AutoMapper;
using KutuphaneOtomasyonu.Entity.Dtos.Kitaps;
using KutuphaneOtomasyonu.Entity.Dtos.OkunanKitaplars;
using KutuphaneOtomasyonu.Service.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;

namespace KutuphaneOtomasyonu.Web.Areas.Ogrenci.Controllers
{
    [Area("Ogrenci")]
    [Authorize(Roles = "Ogrenci")]
    public class KitaplikController : Controller
    {
        private readonly IKitapService kitapService;
        private readonly IKitaplikService kitaplikService;
        private readonly IOkunanKitaplarService okunanKitaplarService;
        private IMapper mapper;
        public KitaplikController(IKitaplikService kitaplikservice, IMapper mapper, IKitapService kitapService, IOkunanKitaplarService okunanKitaplarService)
        {
            this.kitaplikService = kitaplikservice;
            this.kitapService = kitapService;
            this.okunanKitaplarService = okunanKitaplarService;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            var kitaplik = await kitaplikService.GetAllKitaplikAsync();
            return View(kitaplik.ToPagedList(page, 5));
        }
        public async Task<IActionResult> KitaplikDetay(int id, int page = 1)
        {
            var kitap = await kitapService.GetKitapByKitaplikAsync(id);
            var kitapDto = mapper.Map<List<KitapDto>>(kitap);
            return View(kitapDto.ToPagedList(page, 5));
        }
        public async Task<IActionResult> KitapGorus(int id, int page = 1)
        {
            var okunanKitaplar = await okunanKitaplarService.GetOkunanKitaplarByKitapAsync(id);
            var okunanKitaplarDto = mapper.Map<List<OkunanKitaplarDto>>(okunanKitaplar);
            return View(okunanKitaplarDto.ToPagedList(page, 5));
        }
    }
}
