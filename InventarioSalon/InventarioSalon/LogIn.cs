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
using System.Runtime.InteropServices;

namespace InventarioSalon
{
    public partial class LogIn : Form
    {
        DataBase db = new DataBase();
        Decrypt decrypt = new Decrypt();
        private bool isDragging = false;
        private Point lastCursorPosition;

        public LogIn()
        {
            InitializeComponent();
        }

        private Color hoverBorderColor = Color.Red; 
        private bool isMouseOverPictureBox = false;

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

        private void txtUser_Enter(object sender, EventArgs e)
        {
            if(txtUser.Text == "Usuario")
            {
                txtUser.Text = "";
                txtUser.ForeColor = Color.Black;
            }
            panel1.BackColor = Color.LightBlue;

        }

        private void txtUser_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUser.Text))
            {
                txtUser.Text = "Usuario";
                txtUser.ForeColor = Color.Silver;
            }
            panel1.BackColor = Color.DimGray;
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (txtPassword.Text == "Contraseña")
            {
                txtPassword.Text = "";
                txtPassword.ForeColor = Color.Black;
                txtPassword.PasswordChar = '*';
            }
            panel2.BackColor = Color.LightBlue;

        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                txtPassword.Text = "Contraseña";
                txtPassword.ForeColor = Color.Silver;
                txtPassword.PasswordChar = '\0';
            }
            panel2.BackColor = Color.DimGray;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void LogIn_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                lastCursorPosition = Cursor.Position;
            }
        }

        private void LogIn_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point cursorDiff = new Point(Cursor.Position.X - lastCursorPosition.X, Cursor.Position.Y - lastCursorPosition.Y);
                Location = new Point(Location.X + cursorDiff.X, Location.Y + cursorDiff.Y);
                lastCursorPosition = Cursor.Position;
            }
        }

        private void LogIn_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }

    }
}
