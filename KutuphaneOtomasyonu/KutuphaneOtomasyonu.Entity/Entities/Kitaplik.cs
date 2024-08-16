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
    public class Kitaplik : EntityBase
    {
        [Key]
        public int Id { get; set; }
        public string Tur { get; set; }
        public ICollection<Kitap>Kitaps { get; set; }

    }
}
