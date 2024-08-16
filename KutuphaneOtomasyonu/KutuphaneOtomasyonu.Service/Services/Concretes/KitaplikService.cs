using AutoMapper;
using KutuphaneOtomasyonu.Data.UnitOfWorks;
using KutuphaneOtomasyonu.Entity.Dtos.Kitapliks;
using KutuphaneOtomasyonu.Entity.Entities;
using KutuphaneOtomasyonu.Service.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneOtomasyonu.Service.Services.Concretes
{
    public class KitaplikService : IKitaplikService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public KitaplikService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<List<KitaplikDto>> GetAllKitaplikAsync()
        {
            var kitapliks = await unitOfWork.GetRepository<Kitaplik>().GetAllAsync(x => x.Id != null);
            var map = mapper.Map<List<KitaplikDto>>(kitapliks);

            return map;
        }
        public async Task CreateKitaplikAsync(KitaplikAddDto kitaplikAddDto)
        {
            var kitaplik = new Kitaplik
            {
                Tur=kitaplikAddDto.Tur,
            };
            await unitOfWork.GetRepository<Kitaplik>().AddAsync(kitaplik);
            await unitOfWork.SaveAsync();

        }
        public async Task<Kitaplik> GetKitaplikById(int id)
        {
            var kitaplik = await unitOfWork.GetRepository<Kitaplik>().GetByIdAsync(id);
            return kitaplik;
        }
        public async Task<string> UpdateKitaplikAsync(KitaplikUpdateDto kitaplikUpdateDto)
        {
            var kitaplik = await unitOfWork.GetRepository<Kitaplik>().GetAsync(x => x.Id != null && x.Id == kitaplikUpdateDto.Id);
            kitaplik.Tur = kitaplikUpdateDto.Tur;

            await unitOfWork.GetRepository<Kitaplik>().UpdateAsync(kitaplik);
            await unitOfWork.SaveAsync();
            return kitaplik.Tur;
        }

        public async Task DeleteKitaplikAsync(int id)
        {
            var kitaplik = await unitOfWork.GetRepository<Kitaplik>().GetByIdAsync(id);

            await unitOfWork.GetRepository<Kitaplik>().DeleteAsync(kitaplik);
            await unitOfWork.SaveAsync();
        }
       
    }
}
