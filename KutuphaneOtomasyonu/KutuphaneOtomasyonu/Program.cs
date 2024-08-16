using KutuphaneOtomasyonu.Data.Context;
using KutuphaneOtomasyonu.Data.Extensions;
using KutuphaneOtomasyonu.Service.Extensions;
using KutuphaneOtomasyonu.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.LoadDataLayerExtension(builder.Configuration);
builder.Services.LoadServiceLayerExtension();

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddIdentity<AppUser,AppRole>().AddEntityFrameworkStores<AppDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    // Area routes
    endpoints.MapAreaControllerRoute(
        name: "ogretmen",
        areaName: "Ogretmen",
        pattern: "Ogretmen/{controller=Ogretmen}/{action=Index}/{id?}");

    endpoints.MapAreaControllerRoute(
        name: "ogrenci",
        areaName: "Ogrenci",
        pattern: "Ogrenci/{controller=Ogrenci}/{action=Index}/{id?}");

    // Default route
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Register}/{action=Index}/{id?}");

    // General area route
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
});

app.Run();
