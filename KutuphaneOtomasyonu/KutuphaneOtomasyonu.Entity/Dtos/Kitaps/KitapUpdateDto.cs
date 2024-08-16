using KutuphaneOtomasyonu.Entity.Dtos.Kitapliks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneOtomasyonu.Entity.Dtos.Kitaps
{
    public class KitapUpdateDto
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Yazar { get; set; }
        public string Yayınevi { get; set; }
        public string Ozet { get; set; }
        public int KitaplikId { get; set; }

        public IList<KitaplikDto> Kitapliks { get; set; }
    }
}
