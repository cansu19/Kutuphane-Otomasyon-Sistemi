using AutoMapper;
using KutuphaneOtomasyonu.Entity.Dtos.OkunanKitaplars;
using KutuphaneOtomasyonu.Service.Services.Abstractions;
using KutuphaneOtomasyonu.Service.Services.Concretes;
using Microsoft.AspNetCore.Mvc;

namespace KutuphaneOtomasyonu.Web.ViewComponents.OkunanKitaplarViewComponent
{
    public class OkunanKitaplarViewComponent : ViewComponent
    {
        private readonly IOkunanKitaplarService okunanKitaplarService;
        private IMapper mapper;

        public OkunanKitaplarViewComponent(IOkunanKitaplarService okunanKitaplarService, IMapper mapper)
        {
                this.okunanKitaplarService = okunanKitaplarService;
            this.mapper = mapper;

        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var okunanKitaplar = await okunanKitaplarService.GetAllOkunanKitaplarsByAppUserAsync();
            var okunankitaplarDto = mapper.Map<List<OkunanKitaplarDto>>(okunanKitaplar);
            return View(okunankitaplarDto);
        }

    }
}
