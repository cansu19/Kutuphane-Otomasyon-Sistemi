using KutuphaneOtomasyonu.Service.Services.Abstractions;
using KutuphaneOtomasyonu.Service.Services.Concretes;
using Microsoft.AspNetCore.Mvc;

namespace KutuphaneOtomasyonu.Web.ViewComponents._LayoutViewComponents
{
    public class _LayoutViewComponent : ViewComponent
    {
        private readonly IAppUserService appUserService;

        public _LayoutViewComponent(IAppUserService appUserService)
        {
            this.appUserService = appUserService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var appUsers = await appUserService.GetAllAppUserAsync();
            return View(appUsers);
        }
    }
}
