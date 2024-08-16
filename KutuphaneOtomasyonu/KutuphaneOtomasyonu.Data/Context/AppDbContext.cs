using KutuphaneOtomasyonu.Entity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneOtomasyonu.Data.Context
{
    public class AppDbContext: IdentityDbContext<AppUser,AppRole,int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-OOL85IB\\MSSQLSERVER01;Database=KutuphaneDb;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        public DbSet<Kitap> Kitaps { get; set; }
        public DbSet<Kitaplik> Kitapliks { get; set; }
        public DbSet<OkunanKitaplar> OkunanKitaplars { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
