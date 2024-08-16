using KutuphaneOtomasyonu.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneOtomasyonu.Entity.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public List<OkunanKitaplar> OkunanKitaplars { get; set; }
    }
}
