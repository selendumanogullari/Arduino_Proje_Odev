using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace arduino_serial_comm_sql
{
    public partial class Form1 : Form
    {
        int button_click_counter=0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String kullanici_adi = textBox1.Text;
            String sifre = textBox2.Text;

            if (kullanici_adi.Equals("admin") && sifre.Equals("admin"))
            {
                MessageBox.Show("Giriş başarılı");
                this.Hide();
                Form2 frm2 = new Form2();
                frm2.Show();
            }
            else if (kullanici_adi.Equals("admin") && !sifre.Equals("admin"))
            {
                button_click_counter++;
                MessageBox.Show("Hatalı Şifre\nHATALI GİRİŞ DENEMESİ!!!\nKALAN GİRİŞ HAKKINIZ "+
                                                            Convert.ToString(3 - button_click_counter) );
            }
            else if (!kullanici_adi.Equals("admin") && sifre.Equals("admin"))
            {
                button_click_counter++;
                MessageBox.Show("Hatalı Kullanıcı Adı\nHATALI GİRİŞ DENEMESİ!!!\nKALAN GİRİŞ HAKKINIZ " +
                                                            Convert.ToString(3 - button_click_counter));
                
            }
            else if(!kullanici_adi.Equals("admin") && !sifre.Equals("admin")){
                button_click_counter++;
                MessageBox.Show("Hatalı Kullanıcı Adı ve Şifre\nHATALI GİRİŞ DENEMESİ!!!\nKALAN GİRİŞ HAKKINIZ " +
                                                            Convert.ToString(3 - button_click_counter));
                
            }
            
            if (button_click_counter == 3)
            {
                Application.Exit();
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
