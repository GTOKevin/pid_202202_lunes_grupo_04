using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Helpers
{
   public static class EncryptMD5
    {
        public static string Encrypt(string cadena)
        {
            string hash = "seguridad sabuesos";
            byte[] data = UTF8Encoding.UTF8.GetBytes(cadena);


            //Protocolo MD5
            MD5 mD5 = MD5.Create();
            TripleDES tripleDES = TripleDES.Create();


            tripleDES.Key = mD5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            tripleDES.Mode = CipherMode.ECB;


            ICryptoTransform transform = tripleDES.CreateEncryptor();
            byte[] result = transform.TransformFinalBlock(data, 0, data.Length);

            return Convert.ToBase64String(result);
        }

        public static string Decrypt(string msjEn)
        {
            string hash = "seguridad sabuesos";
            byte[] data = Convert.FromBase64String(msjEn);


            //Protocolo MD5
            MD5 mD5 = MD5.Create();
            TripleDES tripleDES = TripleDES.Create();


            tripleDES.Key = mD5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            tripleDES.Mode = CipherMode.ECB;


            ICryptoTransform transform = tripleDES.CreateDecryptor();
            byte[] result = transform.TransformFinalBlock(data, 0, data.Length);

            return UTF8Encoding.UTF8.GetString(result);
        }
    }
}
