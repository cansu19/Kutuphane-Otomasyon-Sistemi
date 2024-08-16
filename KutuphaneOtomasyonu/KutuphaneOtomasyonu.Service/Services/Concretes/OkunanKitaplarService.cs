using AutoMapper;
using KutuphaneOtomasyonu.Data.UnitOfWorks;
using KutuphaneOtomasyonu.Entity.Dtos.Kitaps;
using KutuphaneOtomasyonu.Entity.Dtos.OkunanKitaplars;
using KutuphaneOtomasyonu.Entity.Entities;
using KutuphaneOtomasyonu.Service.Extensions;
using KutuphaneOtomasyonu.Service.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneOtomasyonu.Service.Services.Concretes
{
    public class OkunanKitaplarService: IOkunanKitaplarService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ClaimsPrincipal _user;

        public OkunanKitaplarService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            _user = httpContextAccessor.HttpContext.User;
        }

        public async Task CreateOkunanKitaplarAsync(OkunanKitaplarAddDto okunanKitaplarAddDto)
        {
            var appUserId = _user.GetLoggedInUserId();
            var existingRecords = await unitOfWork.GetRepository<OkunanKitaplar>()
                .GetAllAsync(x => x.AppUserId == appUserId && x.KitapId == okunanKitaplarAddDto.KitapId);

            if (existingRecords.Any())
            {             
                throw new InvalidOperationException("Bu kitabı daha önce eklediniz.");
            }

            var okunanKitaplar = new OkunanKitaplar
            {
                Gorus = okunanKitaplarAddDto.Gorus,
                KitapId = okunanKitaplarAddDto.KitapId,
                AppUserId = appUserId,
            };

            await unitOfWork.GetRepository<OkunanKitaplar>().AddAsync(okunanKitaplar);
            await unitOfWork.SaveAsync();
        }


        public async Task<List<OkunanKitaplarDto>> GetAllOkunanKitaplarAsync()
        {
            var okunanKitaplars = await unitOfWork.GetRepository<OkunanKitaplar>().GetAllAsync(x => x.Id != null, x => x.Kitap, x=>x.AppUser);
            var map = mapper.Map<List<OkunanKitaplarDto>>(okunanKitaplars);
            return map;
        }
        public async Task<OkunanKitaplar> GetOkunanKitaplarById(int id)
        {
            var okunanKitaplar = await unitOfWork.GetRepository<OkunanKitaplar>().GetByIdAsync(id);
            return okunanKitaplar;
        }
        public async Task<List<OkunanKitaplarDto>> GetOkunanKitaplarByKitapAsync(int kitapId)

        {
            var okunanKitaplars = await unitOfWork.GetRepository<OkunanKitaplar>().GetAllAsync(x => x.Id != null && x.KitapId == kitapId, x => x.Kitap,  x=>x.AppUser);

            var map = mapper.Map<List<OkunanKitaplarDto>>(okunanKitaplars);

            return map;
        }
        public async Task<List<OkunanKitaplarDto>> GetOkunanKitaplarByAppUserIdAsync(int id)

        {
            var okunanKitaplars = await unitOfWork.GetRepository<OkunanKitaplar>().GetAllAsync(x => x.Id != null && x.AppUserId == id, x => x.Kitap, x => x.AppUser);

            var map = mapper.Map<List<OkunanKitaplarDto>>(okunanKitaplars);

            return map;
        }
        public async Task<List<OkunanKitaplarDto>> GetAllOkunanKitaplarsByAppUserAsync()
        {
            var appUserId = _user.GetLoggedInUserId();
            var okunanKitaplars = await unitOfWork.GetRepository<OkunanKitaplar>()
                                          .GetAllAsync(h => h.AppUserId == appUserId, h => h.Kitap, h => h.AppUser);

            var map = mapper.Map<List<OkunanKitaplarDto>>(okunanKitaplars);

            return map;
        }
        public async Task UpdateOkunanKitaplarAsync(OkunanKitaplarUpdateDto okunanKitaplarUpdateDto)
        {
            var okunanKitaplar = await unitOfWork.GetRepository<OkunanKitaplar>().GetAsync(x => x.Id != null && x.Id == okunanKitaplarUpdateDto.Id, x => x.Kitap, x => x.AppUser);

            okunanKitaplar.Gorus = okunanKitaplarUpdateDto.Gorus;

            await unitOfWork.GetRepository<OkunanKitaplar>().UpdateAsync(okunanKitaplar);
            await unitOfWork.SaveAsync();
        }
        public async Task DeleteKitapAsync(int id)
        {
            var okunanKitaplar = await unitOfWork.GetRepository<OkunanKitaplar>().GetByIdAsync(id);

            await unitOfWork.GetRepository<OkunanKitaplar>().DeleteAsync(okunanKitaplar);
            await unitOfWork.SaveAsync();
        }
    }
}
