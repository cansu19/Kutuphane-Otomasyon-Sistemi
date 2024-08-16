using KutuphaneOtomasyonu.Data.Repositories.Abstractions;
using KutuphaneOtomasyonu.Data.Repositories.Concretes;
using KutuphaneOtomasyonu.Data.UnitOfWorks;
using KutuphaneOtomasyonu.Service.Services.Abstractions;
using KutuphaneOtomasyonu.Service.Services.Concretes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneOtomasyonu.Service.Extensions
{
    public static class ServiceLayerExtensions
    {
        public static IServiceCollection LoadServiceLayerExtension(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddScoped<IKitapService, KitapService>();
            services.AddScoped<IKitaplikService, KitaplikService>();
            services.AddScoped<IOkunanKitaplarService, OkunanKitaplarService>();
            services.AddScoped<IAppUserService, AppUserService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAutoMapper(assembly);
            return services;
        }
    }
}
