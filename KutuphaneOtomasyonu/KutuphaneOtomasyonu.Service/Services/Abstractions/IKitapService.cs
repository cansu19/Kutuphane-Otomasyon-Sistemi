using KutuphaneOtomasyonu.Entity.Dtos.Kitaps;
using KutuphaneOtomasyonu.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneOtomasyonu.Service.Services.Abstractions
{
    public interface IKitapService
    {
        Task<List<KitapDto>> GetAllKitapAsync();
        Task CreateKitapAsync(KitapAddDto kitapAddDto);
        Task<List<KitapDto>> GetKitapByKitaplikAsync(int kitaplikId);
        Task UpdateKitapAsync(KitapUpdateDto kitapUpdateDto);
        Task<Kitap> GetKitapById(int id);
        Task DeleteKitapAsync(int id);

    }
}
