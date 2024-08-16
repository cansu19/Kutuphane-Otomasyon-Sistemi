using KutuphaneOtomasyonu.Data.Context;
using KutuphaneOtomasyonu.Data.Repositories.Abstractions;
using KutuphaneOtomasyonu.Data.Repositories.Concretes;
using KutuphaneOtomasyonu.Data.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneOtomasyonu.Data.Extensions
{
    public static class DataLayerExtensions
    {
        public static IServiceCollection LoadDataLayerExtension(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            return services;
        }
    }
}
