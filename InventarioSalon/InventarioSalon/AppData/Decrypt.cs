using InventarioSalon.AppData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace InventarioSalon.AppData
{
    internal class Decrypt
    {
        TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
        MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
        string myKey = "QuePedoCachorros";
        

        public Decrypt()
        {
            
        }

        public string DecryptText(string texto)
        {
            string Text = "";
            if ((texto.Trim() == ""))
            {
                Text = "";
            }
            else
            {
                des.Key = hashmd5.ComputeHash(new UnicodeEncoding().GetBytes(myKey));
                des.Mode = CipherMode.ECB;
                ICryptoTransform desencrypta = des.CreateDecryptor();
                byte[] buff = Convert.FromBase64String(texto);
                Text = UnicodeEncoding.ASCII.GetString(desencrypta.TransformFinalBlock(buff, 0, buff.Length));
            }

            return Text;
        }

        public string EncryptText(string Text)
        {
            if (Text.Trim() == "")
            {
                return "";
            }
            else
            {
                des.Key = hashmd5.ComputeHash((new UnicodeEncoding()).GetBytes(myKey));
                des.Mode = CipherMode.ECB;
                ICryptoTransform encrypt = des.CreateEncryptor();
                byte[] buff = UnicodeEncoding.ASCII.GetBytes(Text);
                return Convert.ToBase64String(encrypt.TransformFinalBlock(buff, 0, buff.Length));
            }
        }

       


    }
}

