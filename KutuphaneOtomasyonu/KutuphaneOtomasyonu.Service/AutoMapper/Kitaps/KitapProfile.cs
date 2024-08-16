using AutoMapper;
using KutuphaneOtomasyonu.Entity.Dtos.Kitaps;
using KutuphaneOtomasyonu.Entity.Entities;

public class KitapProfile : Profile
{
    public KitapProfile()
    {  
        CreateMap<KitapDto, KitapUpdateDto>().ReverseMap();
        CreateMap<KitapDto, Kitap>().ReverseMap();
        CreateMap<KitapAddDto, Kitap>().ReverseMap();
        CreateMap<KitapUpdateDto, Kitap>().ReverseMap();
    }
}
