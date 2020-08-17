using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;
namespace eLearningMareaUnire1918
{
    public partial class eLearning1918_start : Form
    {
        public eLearning1918_start()
        {
            InitializeComponent();
        }
        public static string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\eLearning1918.mdf;Integrated Security=True;Connect Timeout=30";
        bool playing = true;
        int counter = 0;
        DataTable dtIntrebariGenerate = new DataTable();
        private void eLearning1918_start_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = "Aceasta platforma eLearnig este dedicata Marii Uniri de la 1918.La 1 decembrie 2018 se implinesc 100 de ani de la Unirea Transilvaniei, Banatului, Crisanei si Maramuresului cu Romania";
            timer1.Start();
            if (textBox2.PasswordChar == '*')
            {

                textBox2.PasswordChar = '\0';
            }
            else
            {
                textBox2.PasswordChar = '*';
            }
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            if (!playing)
            {
                button2.Text = "Auto";
                timer1.Start();
                playing = true;
            }
            else
            {
                button2.Text = "Manual";
                playing = false;
                timer1.Stop();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            counter++;
            string[] files = Directory.GetFiles(Application.StartupPath + @"\..\..\imaginislideshow\", "*.jpg*");
            if (counter > files.Length - 1)
            { counter = 0; }
            pictureBox1.Image = Image.FromFile(files[counter]);
            progressBar1.Maximum = files.Length - 1;
            progressBar1.Step = 1;
            progressBar1.Value = counter;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button2.Text == "Manual")
            {
                string[] files = Directory.GetFiles(Application.StartupPath + @"\..\..\imaginislideshow\", "*.jpg*");

                if (counter < files.Length - 1)
                    counter++;
                pictureBox1.Image = Image.FromFile(files[counter]);
                progressBar1.Maximum = files.Length - 1;
                progressBar1.Step = 1;
                progressBar1.Value = counter;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button2.Text == "Manual")
            {
                if (counter > 0)
                    counter--;

                string[] files = Directory.GetFiles(Application.StartupPath + @"\..\..\imaginislideshow\", "*.jpg*");
                pictureBox1.Image = Image.FromFile(files[counter]);
                progressBar1.Maximum = files.Length - 1;
                progressBar1.Step = 1;
                progressBar1.Value = counter;
            }
        }
        public void GenereazaIntrebari()
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            DataTable dt2 = new DataTable();
            Random rnd = new Random();
            SqlDataAdapter sda1 = new SqlDataAdapter("SELECT COUNT(TipItem) FROM Itemi GROUP BY TipItem", con);
            sda1.Fill(dt2);
            int tip1 = Convert.ToInt32(dt2.Rows[0][0].ToString());
            int tip2 = Convert.ToInt32(dt2.Rows[1][0].ToString());
            int tip3 = Convert.ToInt32(dt2.Rows[2][0].ToString());
            int tip4 = Convert.ToInt32(dt2.Rows[3][0].ToString());
            HashSet<int> numbers = new HashSet<int>();

            while (numbers.Count <= 10)
            {
                numbers.Add(rnd.Next(tip1 + tip2 + tip3 + 1, tip1 + tip2 + tip3+ tip4));
                numbers.Add(rnd.Next(tip1 + tip2 + 1, tip1 + tip2 + tip3));
                numbers.Add(rnd.Next(1, tip1));
                numbers.Add(rnd.Next(tip1+1, tip1+tip2));

            }

            foreach (int tipitem in numbers)
            {
                SqlDataAdapter sda3 = new SqlDataAdapter("SELECT EnuntItem,TipItem,RaspunsCorectItem,Raspuns1Item,Raspuns2Item,Raspuns3Item,Raspuns4Item FROM Itemi WHERE IdItem = '" + tipitem + "'", con);
                sda3.Fill(dtIntrebariGenerate);
            }
            con.Close();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constr);
            SqlDataAdapter sda = new SqlDataAdapter("select count(*) from utilizatori where emailutilizator='" + textBox1.Text + "' and parolautilizator='" + textBox2.Text + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                GenereazaIntrebari();
                this.Hide();
                eLearning1918_Elev formElev = new eLearning1918_Elev(dtIntrebariGenerate);
                formElev.Show();

            }
            else
            {
                textBox1.Clear();
                textBox2.Clear();
                MessageBox.Show("Eroare de autentificare!");
            }
        }
    }
}
