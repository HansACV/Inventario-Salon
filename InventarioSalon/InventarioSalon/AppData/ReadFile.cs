using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioSalon.AppData
{
    internal class ReadFile
    {
        public string ip, bd, id, pass;
        Decrypt decrypt = new Decrypt();

        public ReadFile() 
        {
            string[] arrStrInfo = Read();
            ip = decrypt.DecryptText(arrStrInfo[0]);
            bd = decrypt.DecryptText(arrStrInfo[1]);
            id = decrypt.DecryptText(arrStrInfo[2]);
            pass = decrypt.DecryptText(arrStrInfo[3]);
        }

        public String[] Read()
        {
            // Ruta de archivo principal
            const string strRuta = ".\\SetUpDB.ini";

            string[] StrData = new string[4];

            System.IO.StreamReader sr = new System.IO.StreamReader(strRuta);

            StrData[0] = sr.ReadLine();
            StrData[1] = sr.ReadLine();
            StrData[2] = sr.ReadLine();
            StrData[3] = sr.ReadLine();

            sr.Close();
            return StrData;
        }

    }
}
