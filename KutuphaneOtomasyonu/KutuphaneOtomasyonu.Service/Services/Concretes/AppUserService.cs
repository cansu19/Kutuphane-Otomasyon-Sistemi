using AutoMapper;
using KutuphaneOtomasyonu.Data.UnitOfWorks;
using KutuphaneOtomasyonu.Entity.Dtos.AppUsers;
using KutuphaneOtomasyonu.Entity.Dtos.Kitaps;
using KutuphaneOtomasyonu.Entity.Entities;
using KutuphaneOtomasyonu.Service.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneOtomasyonu.Service.Services.Concretes
{
    public class AppUserService :IAppUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<AppUser> userManager;
        private readonly ClaimsPrincipal _user;

        public AppUserService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper, UserManager<AppUser> userManager) 
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            _user = httpContextAccessor.HttpContext.User;
            this.userManager = userManager;
        }

        public async Task<List<AppUserDto>> GetAllAppUserAsync()
        {
            var appUsers = await userManager.Users.ToListAsync();
            var ogrenciUsers = new List<AppUser>();

            foreach (var user in appUsers)
            {
                if (await userManager.IsInRoleAsync(user, "Ogrenci"))
                {
                    ogrenciUsers.Add(user);
                }
            }
            var map = mapper.Map<List<AppUserDto>>(ogrenciUsers);

            return map;
        }
    }
}
