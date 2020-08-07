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
        public static string constr = @"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\eLearning1918.mdf;Integrated Security=True;User Instance=True;Password=1213";
        bool playing = true;
        int counter = 0;
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
    }
}
