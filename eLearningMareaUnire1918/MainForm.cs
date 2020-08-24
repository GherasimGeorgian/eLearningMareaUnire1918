using MaterialSkin;
using System;
using MaterialSkin.Controls;
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
using System.Globalization;
using System.Drawing.Drawing2D;

namespace eLearningMareaUnire1918
{
    public partial class MainForm : MaterialSkin.Controls.MaterialForm
    {
        MaterialSkinManager materialSkinManager;
        public MainForm()
        {
            InitializeComponent();
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Green400, Primary.Green400, Primary.Green400, Accent.Green400, TextShade.WHITE);
        }
        
        public static string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\eLearning1918.mdf;Integrated Security=True;Connect Timeout=30";
       /// <summary>
       /// 1 - datele au fost incarcate in baza de date
       /// 0 - datele nu au fost incarcate in baza de date
       /// </summary>
       int LoadDataBase = Properties.Settings.Default.loadDataBase;

        private void MainForm_Load(object sender, EventArgs e)
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", Directory.GetCurrentDirectory());
            if (LoadDataBase == 0)
            {
                sterge();
                Initializare();
                Properties.Settings.Default.loadDataBase = 1;
                Properties.Settings.Default.Save();
            }
        }
        /// <summary>
        /// Stergem inrefistrarile din baza de date eLearning1918 respectiv dint tablele:Utilizatori,Itemi si Evaluari
        /// </summary>
        private void sterge()
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from Utilizatori", con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            cmd.CommandText = "TRUNCATE TABLE Utilizatori;";


            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("delete from Itemi", con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            cmd.CommandText = "TRUNCATE TABLE Itemi;";
            cmd.ExecuteNonQuery();

            cmd = new SqlCommand("delete from Evaluari", con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            cmd.CommandText = "TRUNCATE TABLE Evaluari;";
            cmd.ExecuteNonQuery();
            con.Close();
        }
        /// <summary>
        /// Datele necesare aplicatiei sunt preluate din fisierul date.txt si incarcate in baza de date
        /// </summary>
        private static void Initializare()
        {
            StreamReader sr = new StreamReader(Application.StartupPath + @"\..\..\date.txt");
            string sir;
            List<string> cities = new List<string>();
            while ((sir = sr.ReadLine()) != null)
            {
                cities.Add(sir);
            }
            char[] split = { ';' };
            string a = "";
            int i;
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd;
            con.Open();


            var textArray = File.ReadAllLines(Application.StartupPath + @"\..\..\date.txt");
            var indexOfUtilizatori = Array.IndexOf(textArray, "Utilizatori:");
            var indexOfItemi = Array.IndexOf(textArray, "Itemi:");
            for (i = indexOfUtilizatori + 1; i < indexOfItemi; i++)
            {
                a = cities[i].ToString();
                string[] siruri = a.Split(split);

                cmd = new SqlCommand("insert into Utilizatori(numeprenumeutilizator,parolautilizator,emailutilizator,clasautilizator) values(@npu,@pu,@eu,@cu)", con);
                cmd.Parameters.AddWithValue("npu", siruri[0].Trim());
                cmd.Parameters.AddWithValue("pu", siruri[1].Trim());
                cmd.Parameters.AddWithValue("eu", siruri[2].Trim());
                cmd.Parameters.AddWithValue("cu", siruri[3].Trim());
                cmd.ExecuteNonQuery();
            }
            int tipitem;
            var indexOfItemi2 = Array.IndexOf(textArray, "Itemi:");
            var indexOfEvaluari = Array.IndexOf(textArray, "Evaluari:");
            for (i = indexOfItemi2 + 1; i < indexOfEvaluari; i++)
            {
                a = cities[i].ToString();
                string[] siruri = a.Split(split);
                cmd = new SqlCommand("insert into Itemi(tipitem,enuntitem,raspuns1item,raspuns2item,raspuns3item,raspuns4item,raspunscorectitem) values(@ti,@ei,@r1,@r2,@r3,@r4,@rs)", con);
                tipitem = int.Parse(siruri[0]);
                cmd.Parameters.AddWithValue("ti", tipitem);
                cmd.Parameters.AddWithValue("ei", siruri[1].Trim());
                cmd.Parameters.AddWithValue("r1", siruri[2].Trim());
                cmd.Parameters.AddWithValue("r2", siruri[3].Trim());
                cmd.Parameters.AddWithValue("r3", siruri[4].Trim());
                cmd.Parameters.AddWithValue("r4", siruri[5].Trim());
                cmd.Parameters.AddWithValue("rs", siruri[6].Trim());
                cmd.ExecuteNonQuery();
            }



            int nota;
            DateTime dt1;
            IFormatProvider culture = new CultureInfo("en-US", true);
            var indexOfEvaluari2 = Array.IndexOf(textArray, "Evaluari:");
            for (i = indexOfEvaluari2 + 1; i < cities.Count; i++)
            {
                a = cities[i].ToString();
                string[] siruri = a.Split(split);
                nota = int.Parse(siruri[2]);
                string d1 = siruri[1];
                dt1 = Convert.ToDateTime(d1, CultureInfo.InvariantCulture);


                cmd = new SqlCommand("select idutilizator from utilizatori where idutilizator=@idelev", con);
                cmd.Parameters.AddWithValue("idelev", siruri[0].Trim());
                int IdUtilizator = Convert.ToInt32(cmd.ExecuteScalar());

                cmd = new SqlCommand("insert into Evaluari(idelev,dataevaluare,notaevaluare) values(@ie,@de,@ne)", con);
                cmd.Parameters.AddWithValue("ie", siruri[0].Trim());
                cmd.Parameters.AddWithValue("de", dt1);
                cmd.Parameters.AddWithValue("ne", nota);
                cmd.ExecuteNonQuery();
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            eLearning1918_start w = new eLearning1918_start();
            this.Hide();
            w.Show();
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            this.TopMost = true;
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
        }

        private void checkBoxTM_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxTM.Checked) {
                this.TopMost = true;
                checkBoxTM.Text = "UnTopMost";
            } else {
                this.TopMost = false;
                checkBoxTM.Text = "TopMost";
            }
        }

        private void btnRed_Click(object sender, EventArgs e)
        {
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Red400, Primary.Red400, Primary.Red400, Accent.Red400, TextShade.WHITE);
        }

        private void btnPurple_Click(object sender, EventArgs e)
        {
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Purple400, Primary.Purple400, Primary.Purple400, Accent.Purple400, TextShade.WHITE);
        }

        private void btnOrange_Click(object sender, EventArgs e)
        {
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Orange400, Primary.Orange400, Primary.Orange400, Accent.Orange400, TextShade.WHITE);
        }

        private void btnBlue_Click(object sender, EventArgs e)
        {
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Blue400, Primary.Blue400, Primary.Blue400, Accent.Blue400, TextShade.WHITE);
        }

        private void btnPink_Click(object sender, EventArgs e)
        {
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Pink400, Primary.Pink400, Primary.Pink400, Accent.Pink400, TextShade.WHITE);
        }

        private void btnGreen_Click(object sender, EventArgs e)
        {
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Green400, Primary.Green400, Primary.Green400, Accent.Green400, TextShade.WHITE);
        }

        private void checkBokTheme_CheckedChanged(object sender, EventArgs e)
        {
            if (lblTheme.Text == "dark")
            {
                materialSkinManager = MaterialSkinManager.Instance;
                materialSkinManager.AddFormToManage(this);
                materialSkinManager.Theme = MaterialSkinManager.Themes.DARK; 
                checkBokTheme.Text = "light";
                lblTheme.Text = "light";
            }
            else
            {
                materialSkinManager = MaterialSkinManager.Instance;
                materialSkinManager.AddFormToManage(this);
                materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
                checkBokTheme.Text = "dark";
                lblTheme.Text = "dark";
            }
        }

        private void materialRaisedButton4_Click(object sender, EventArgs e)
        {
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
           
        }

        private void materialRaisedButton3_Click(object sender, EventArgs e)
        {
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
        }
    }
}
