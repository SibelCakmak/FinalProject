﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {
        public static void CreatePasswordHash(string password, out byte[] passwordHash, 
            out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;// ALGORİTMA HER KULLANICI İÇİN BİR KEY OLUŞTURUR HER DEFASINDA
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));// STRİNG OLAN ŞİFREYİ
                // BYTA ÇEVİRİYOR.
            }
        }
        // VERİFY : DOĞRULA
        public static bool VerifyPasswordHash(string password, byte[] passwordHash,
             byte[] passwordSalt)
        {
            // doğrulama için passwordsaltı verdik.
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i]!=passwordHash[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}