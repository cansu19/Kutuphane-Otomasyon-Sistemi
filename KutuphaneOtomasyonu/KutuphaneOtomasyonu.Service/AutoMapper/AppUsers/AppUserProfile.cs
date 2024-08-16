using AutoMapper;
using KutuphaneOtomasyonu.Entity.Dtos.AppUsers;
using KutuphaneOtomasyonu.Entity.Dtos.OkunanKitaplars;
using KutuphaneOtomasyonu.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneOtomasyonu.Service.AutoMapper.AppUsers
{
    public class AppUserProfile : Profile
    {
        public AppUserProfile() 
        {
            CreateMap<AppUserDto, AppUser>().ReverseMap();
        }
    }
}
