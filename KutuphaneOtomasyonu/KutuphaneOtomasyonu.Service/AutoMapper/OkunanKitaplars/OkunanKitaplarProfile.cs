using AutoMapper;
using KutuphaneOtomasyonu.Entity.Dtos.AppUsers;
using KutuphaneOtomasyonu.Entity.Dtos.Kitapliks;
using KutuphaneOtomasyonu.Entity.Dtos.Kitaps;
using KutuphaneOtomasyonu.Entity.Dtos.OkunanKitaplars;
using KutuphaneOtomasyonu.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneOtomasyonu.Service.AutoMapper.OkunanKitaplars
{
    public class OkunanKitaplarProfile : Profile
    {
        public OkunanKitaplarProfile()
        {
            CreateMap<OkunanKitaplarDto, OkunanKitaplar>().ReverseMap();
            CreateMap<OkunanKitaplarDto, KitapDto>().ReverseMap();
            CreateMap<OkunanKitaplarDto, AppUserDto>().ReverseMap();
            CreateMap<OkunanKitaplarAddDto, OkunanKitaplar>().ReverseMap();
            CreateMap<OkunanKitaplarAddDto, OkunanKitaplarDto>().ReverseMap();
            CreateMap<OkunanKitaplarUpdateDto, OkunanKitaplar>().ReverseMap();
        }
        
    }
}
