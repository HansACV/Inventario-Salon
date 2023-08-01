using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioSalon.AppData
{
    internal class Connection
    {
        private static Connection _connection = null;
        private ReadFile ReadFile = new ReadFile();
        private string server;
        private string DataBase;
        public string User;
        public string Password;
        private MySqlConnection connection = null;

        public static Connection Instance { 
            get { 
                if (_connection == null)
                    _connection = new Connection();
                return _connection; 
            } 
        }

        private Connection()
        {
            server = ReadFile.ip;
            DataBase = ReadFile.bd;
            User = ReadFile.id;
            Password = ReadFile.pass;
        }

        //Metodo que obtiene la conexion de mysql
        // 
        public MySqlConnection ConnectionDB()
        {
            if(connection == null)
            {
                string CadenaDeConexion = "Server=" + server + ";Database=" + DataBase + ";user=" + User + ";password=" + Password + ";SslMode=VerifyFull;";
                connection = new MySqlConnection(CadenaDeConexion);
            }
            
            return connection;
        }



    }
}
