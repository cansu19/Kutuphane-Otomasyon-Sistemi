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
    public class Kitap: EntityBase
    {
        [Key]
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Yazar { get; set; }
        public string Yayınevi { get; set; }
        public string Ozet { get; set; }
        public int KitaplikId { get; set; }
        public Kitaplik Kitaplik { get; set; }
        public ICollection <OkunanKitaplar>OkunanKitaplars { get; set; }    
    }
}
