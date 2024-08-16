﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneOtomasyonu.Service.Extensions
{
    public static class LoggedInUserExtensions
    {
        public static int GetLoggedInUserId(this ClaimsPrincipal principal)
        {
            return int.Parse(principal.FindFirstValue(ClaimTypes.NameIdentifier));
        }
        public static string GetLoggedInUserName(this ClaimsPrincipal principal)
        {
            return (principal.FindFirstValue(ClaimTypes.Name));
        }
    }
}
