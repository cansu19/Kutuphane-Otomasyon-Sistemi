using KutuphaneOtomasyonu.Entity.Dtos.AppUsers;
using KutuphaneOtomasyonu.Entity.Dtos.Kitaps;
using KutuphaneOtomasyonu.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneOtomasyonu.Entity.Dtos.OkunanKitaplars
{
    public class OkunanKitaplarDto
    {
        public int Id { get; set; }
        public string Gorus { get; set; }
        public KitapDto Kitap { get; set; }
        public AppUserDto AppUser { get; set; }
    }
}
