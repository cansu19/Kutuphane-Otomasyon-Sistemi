using KutuphaneOtomasyonu.Entity.Dtos.AppUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneOtomasyonu.Service.Services.Abstractions
{
    public interface IAppUserService
    {
        Task<List<AppUserDto>> GetAllAppUserAsync();
    }
}
