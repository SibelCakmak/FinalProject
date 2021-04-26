using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        // public lerde kullanılan harfler büyük harfle başlamalı
        public static string ProductAdded = "Ürün Eklendi";
        public static string ProductNameInValid = "Ürün İsmi Geçersiz";
        internal static string MaintenanceTime = "Sistem Bakımda";
        internal static string ProductsListed = "Ürünler Listelendi";
        internal static string AuthorizationDenied = "yetkiniz yok";
    }
}
