using KutuphaneOtomasyonu.Entity.Dtos.OkunanKitaplars;
using KutuphaneOtomasyonu.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneOtomasyonu.Service.Services.Abstractions
{
    public interface IOkunanKitaplarService
    {
        Task CreateOkunanKitaplarAsync(OkunanKitaplarAddDto okunanKitaplarAddDto);
        Task<List<OkunanKitaplarDto>> GetAllOkunanKitaplarAsync();
        Task<OkunanKitaplar> GetOkunanKitaplarById(int id);
        Task<List<OkunanKitaplarDto>> GetOkunanKitaplarByAppUserIdAsync(int id);
        Task<List<OkunanKitaplarDto>> GetAllOkunanKitaplarsByAppUserAsync();
        Task<List<OkunanKitaplarDto>> GetOkunanKitaplarByKitapAsync(int kitapId);
        Task UpdateOkunanKitaplarAsync(OkunanKitaplarUpdateDto okunanKitaplarUpdateDto);
        Task DeleteKitapAsync(int id);
    }
}
