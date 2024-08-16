using KutuphaneOtomasyonu.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneOtomasyonu.Entity.Entities
{
    public class OkunanKitaplar:EntityBase
    {
        [Key]
        public int Id { get; set; }
        public string? Gorus { get; set; }
        public int KitapId { get; set; }
        public Kitap Kitap { get; set; }
        public int AppUserId { get; set; } //role=öğrenci
        public AppUser AppUser { get; set; }
    }
}
