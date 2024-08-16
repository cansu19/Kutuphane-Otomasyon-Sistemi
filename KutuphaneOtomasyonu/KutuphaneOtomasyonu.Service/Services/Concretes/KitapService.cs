using KutuphaneOtomasyonu.Entity.Entities;
using KutuphaneOtomasyonu.Service.Services.Abstractions;
using KutuphaneOtomasyonu.Data.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KutuphaneOtomasyonu.Entity.Dtos.Kitaps;
using AutoMapper;
using KutuphaneOtomasyonu.Entity.Dtos.Kitapliks;
using static System.Net.Mime.MediaTypeNames;

namespace KutuphaneOtomasyonu.Service.Services.Concretes
{
    public class KitapService : IKitapService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public KitapService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task CreateKitapAsync(KitapAddDto kitapAddDto)
        {
            var kitap = new Kitap
            {
                Ad= kitapAddDto.Ad,
                Yazar= kitapAddDto.Yazar,
                Yayınevi=kitapAddDto.Yayınevi,
                Ozet=kitapAddDto.Ozet,
                KitaplikId=kitapAddDto.KitaplikId,
            };
            await unitOfWork.GetRepository<Kitap>().AddAsync(kitap);
            await unitOfWork.SaveAsync();

        }
        public async Task<List<KitapDto>> GetAllKitapAsync()
        {
            var kitaps =await unitOfWork.GetRepository<Kitap>().GetAllAsync(x=> x.Id!= null, x=>x.Kitaplik);
            var map = mapper.Map <List<KitapDto >> (kitaps);
            return map;
        }
        public async Task<Kitap> GetKitapById(int id)
        {
            var kitap = await unitOfWork.GetRepository<Kitap>().GetByIdAsync(id);
            return kitap;
        }
        public async Task<List<KitapDto>> GetKitapByKitaplikAsync(int kitaplikId)

        {
            var kitaps = await unitOfWork.GetRepository<Kitap>().GetAllAsync(x => x.Id != null && x.Kitaplik.Id == kitaplikId, x => x.Kitaplik);

            var map = mapper.Map<List<KitapDto>>(kitaps);

            return map;
        }
        public async Task UpdateKitapAsync(KitapUpdateDto kitapUpdateDto)
        {
            var kitap = await unitOfWork.GetRepository<Kitap>().GetAsync(x => x.Id != null && x.Id == kitapUpdateDto.Id, x => x.Kitaplik);

            kitap.Ad = kitapUpdateDto.Ad;
            kitap.Yazar = kitapUpdateDto.Yazar;
            kitap.Yayınevi = kitapUpdateDto.Yayınevi;
            kitap.Ozet = kitapUpdateDto.Ozet;
            kitap.KitaplikId = kitapUpdateDto.KitaplikId;

            await unitOfWork.GetRepository<Kitap>().UpdateAsync(kitap);
            await unitOfWork.SaveAsync();
        }
        public async Task DeleteKitapAsync(int id)
        {
            var kitap = await unitOfWork.GetRepository<Kitap>().GetByIdAsync(id);

            await unitOfWork.GetRepository<Kitap>().DeleteAsync(kitap);
            await unitOfWork.SaveAsync();
        }
    }
}
