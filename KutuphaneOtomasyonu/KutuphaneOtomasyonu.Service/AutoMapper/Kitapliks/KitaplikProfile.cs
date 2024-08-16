using AutoMapper;
using KutuphaneOtomasyonu.Entity.Dtos.Kitapliks;
using KutuphaneOtomasyonu.Entity.Dtos.Kitaps;
using KutuphaneOtomasyonu.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneOtomasyonu.Service.AutoMapper.Kitapliks
{
    public class KitaplikProfile : Profile
    {
        public KitaplikProfile()
        {
            CreateMap<KitaplikDto, Kitaplik>().ReverseMap();
            CreateMap<KitaplikDto, KitapDto>().ReverseMap();
            CreateMap<KitaplikAddDto, Kitaplik>().ReverseMap();
            CreateMap<KitaplikUpdateDto, Kitaplik>().ReverseMap();

        }
    }
}
