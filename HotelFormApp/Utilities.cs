using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HotelFormApp
{
   public static class Utilities
    {
        public static bool IsEmpty(string[] arr)
        {
            foreach (var ar in arr)
            {
                if (string.IsNullOrWhiteSpace(ar))
                {
                    return false;
                }

            }
            return true;
        }
            public static string HasHed(this string Pas)
        {
            byte[] newByte = new UTF8Encoding().GetBytes(Pas);
            var HashMe =new SHA256Managed().ComputeHash(newByte);
            string Password = new UTF8Encoding().GetString(HashMe);
            return Password;
        }
    }
}
