using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using System.Data.SqlClient;

namespace arduino_serial_comm_sql
{
    public partial class Form2 : Form
    {
        string[] ports = SerialPort.GetPortNames();
        string line = null;
        private Thread read_serial=null;
        private Thread db_write = null;
        private Thread db_read = null;
        private SqlConnection con = null;
        private SqlCommand komut = null;
        string sicaklik=null;
        string nem_toprak=null;
        string nem_hava=null;
        string role_durumu=null;
        string up_time=null;
        bool __continue_read_serial=false;
        bool __continue_db_write = false;
        
        //byte[] __buffer= new byte[12];
        //int max_count = 15;
        //int offset = 12;
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            if (serialPort1.IsOpen == true)
            {
                __continue_read_serial = false;
                __continue_db_write = false;
                this.read_serial.Abort();
                serialPort1.Close();
                label4.ForeColor = Color.Red;
                label4.Text = "Bağlantı Kapalı";
            }
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
            foreach (string port in ports)
            {
                comboBox1.Items.Add(port);
                comboBox1.SelectedIndex = 0;
            }
            comboBox2.Items.Add("9600");
            comboBox2.Items.Add("11500");
            comboBox2.SelectedIndex = 0;

            label4.Text = "Bağlanmadı.";

            



        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //try
            //{
                //serialPort1.WriteLine("read");
                label5.Text = line;
                lbl_sicaklik.Text = sicaklik;
                lbl_nem_hava.Text = nem_hava;
                lbl_nem_toprak.Text = nem_toprak;
                lbl_up_date.Text = up_time;
                if (role_durumu.Equals("1"))
                    lbl_role_durum.Text = "Açık";
                else if (role_durumu.Equals("0"))
                    lbl_role_durum.Text = "Kapalı";


            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message); 
            //    timer1.Stop();
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {

            this.read_serial =
                new Thread(new ThreadStart(this.reading_serial));
            this.db_write =
                new Thread(new ThreadStart(this.db_write_task));
            timer1.Start();
            this.db_read =
                new Thread(new ThreadStart(this.db_read_task));
            if (serialPort1.IsOpen == false)
            {
                if (comboBox1.Text == "")
                    return;
                serialPort1.PortName = comboBox1.Text; 
                serialPort1.BaudRate = Convert.ToInt16(comboBox2.Text); 
                try
                {
                    serialPort1.Open(); 
                    label4.ForeColor = Color.Green;
                    label4.Text = "Bağlantı Açık";
                    __continue_read_serial = true;
                    this.read_serial.Start();
                    //Thread read_serial = new Thread(reading_serial);

                    String constring = "server=SELEN\\SQLEXPRESS;database=data;integrated security=true";
                    SqlConnection con = new SqlConnection(constring);
                    con.Open();

                    SqlCommand komut = new SqlCommand();
                    komut.Connection = con;
                    komut.CommandText = "select * from dbo.data_table";
                    SqlDataAdapter adap = new SqlDataAdapter();
                    adap.SelectCommand = komut;
                    DataTable dtx = new DataTable();
                    adap.Fill(dtx);

                    dataGridView1.DataSource = dtx;


                }
                catch (Exception hata)
                {
                    MessageBox.Show("Hata:" + hata.Message);
                }
            }
            else
            {
                label4.Text = "Bağlantı kurulu !!!";
            }
        }
        public void db_read_task()
        {

        }
        public void db_write_task()
        {
            while (true)
            {

                String constring = "server=SELEN\\SQLEXPRESS;database=data;integrated security=true";
                SqlConnection con = new SqlConnection(constring);
                con.Open();

                SqlCommand komut = new SqlCommand();
                komut.Connection = con;
                komut.CommandText = "INSERT INTO dbo.data_table (sicaklik,nem_hava,nem_toprak,role_durum,up_time)" +
                                         "values (" + "'" + sicaklik + "','" + nem_hava + "','" + nem_toprak + "','" + role_durumu + "','" + up_time + "')";
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = komut;
                //DataTable dt = new DataTable();
                //adap.Fill(dt);

                //dataGridView1.DataSource = dt;
                Thread.Sleep(1000);
            }
            
        }
        public void reading_serial()
        {
            while (__continue_read_serial)
            {
                line = serialPort1.ReadLine();
                byte i;
                char pass_char = ' ';
                string[] txt_data;
                txt_data = line.Split(pass_char);
                sicaklik = txt_data[0];
                nem_hava = txt_data[1];
                nem_toprak = txt_data[2];
                role_durumu = txt_data[3];
                up_time = txt_data[4];
                Thread.Sleep(100);
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.db_write.Start();
            this.db_read.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            String constring = "server=SELEN\\SQLEXPRESS;database=data;integrated security=true";
            SqlConnection con = new SqlConnection(constring);
            con.Open();

            SqlCommand komut = new SqlCommand();
            komut.Connection = con;
            komut.CommandText = "select * from dbo.data_table";
            SqlDataAdapter adap = new SqlDataAdapter();
            adap.SelectCommand = komut;
            DataTable dt = new DataTable();
            adap.Fill(dt);

            dataGridView1.DataSource = dt;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            __continue_db_write = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            __continue_db_write = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }
    }
}
