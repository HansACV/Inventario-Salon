using InventarioSalon.AppData;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventarioSalon
{
    public partial class LogIn : Form
    {
        DataBase db = new DataBase();
        Decrypt decrypt = new Decrypt();

        public LogIn()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string Password = decrypt.EncryptText(txtPassword.Text);
                MySqlCommand command = new MySqlCommand("select count(*) from DC_USER where User = @User and Pasword = @Pasword");
                command.Parameters.AddWithValue("User", txtUser.Text);
                command.Parameters.AddWithValue("Pasword", Password);
                int validador = (int)db.RunQueryForSingleValue(command, 0);
                if (validador == 0)
                {
                    string quepedo = "no que pedo";
                }
                else
                {
                    string quepedo = "que pedo";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {

        }

        private void LogIn_Load(object sender, EventArgs e)
        {

        }
    }
}
