using KutuphaneOtomasyonu.Entity.Dtos.AppUsers;
using KutuphaneOtomasyonu.Entity.Dtos.Kitaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneOtomasyonu.Entity.Dtos.OkunanKitaplars
{
    public class OkunanKitaplarAddDto
    {
        public int Id { get; set; }
        public string Gorus { get; set; }
        public int KitapId { get; set; }
        public int AppUserId { get; set; }

        public IList<AppUserDto> AppUsers { get; set; }
        public IList<KitapDto> Kitaps { get; set; }
    }
}
