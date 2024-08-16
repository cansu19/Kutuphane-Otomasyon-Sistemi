using KutuphaneOtomasyonu.Entity.Dtos.Kitapliks;
using KutuphaneOtomasyonu.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneOtomasyonu.Service.Services.Abstractions
{
    public interface IKitaplikService
    {
        Task<List<KitaplikDto>> GetAllKitaplikAsync();
        Task CreateKitaplikAsync(KitaplikAddDto kitaplikAddDto);
        Task<Kitaplik> GetKitaplikById(int id);
        Task<string> UpdateKitaplikAsync(KitaplikUpdateDto kitaplikUpdateDto);
        Task DeleteKitaplikAsync(int id);
    }
}
