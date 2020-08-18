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
using System.Collections;
namespace eLearningMareaUnire1918
{
    public partial class eLearning1918_Elev : Form
    {
        public eLearning1918_Elev(DataTable tib)
        {
            InitializeComponent();
            dtNew = tib;
        }
        DataTable dtNew;
        public int punctaj = 1;
        public int tipIntrebare = -1;
        public int idintrebare= 0;
        TextBox txtRaspuns = new TextBox();
        RadioButton radioBox1 = new RadioButton();
        RadioButton radioBox2 = new RadioButton();
        RadioButton radioBox3 = new RadioButton();
        RadioButton radioBox4 = new RadioButton();

        CheckBox checkBox1 = new CheckBox();
        CheckBox checkBox2 = new CheckBox();
        CheckBox checkBox3 = new CheckBox();
        CheckBox checkBox4 = new CheckBox();

        RadioButton radioBoxTrue = new RadioButton();
        RadioButton radioBoxFalse = new RadioButton();

        DataTable reportRaspunsuri = new DataTable();

        public static string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\eLearning1918.mdf;Integrated Security=True;Connect Timeout=30";

        private void eLearning1918_Elev_Load(object sender, EventArgs e)
        {
            label1.Text = "Punctaj" + punctaj.ToString();
            button2.Enabled = false;
            button3.Enabled = false;
            radioBox1.BackColor = Color.Transparent;
            radioBox2.BackColor = Color.Transparent;
            radioBox3.BackColor = Color.Transparent;
            radioBox4.BackColor = Color.Transparent;
            checkBox1.BackColor = Color.Transparent;
            checkBox2.BackColor = Color.Transparent;
            checkBox3.BackColor = Color.Transparent;
            checkBox4.BackColor = Color.Transparent;
            radioBoxTrue.BackColor = Color.Transparent;
            radioBoxFalse.BackColor = Color.Transparent;
            reportRaspunsuri.Columns.Add("IdItem", typeof(int));
            reportRaspunsuri.Columns.Add("TipItem", typeof(int));
            reportRaspunsuri.Columns.Add("EnuntItem", typeof(string));
            reportRaspunsuri.Columns.Add("RaspunsElev", typeof(string));
            reportRaspunsuri.Columns.Add("RaspunsCorect", typeof(string));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            genereazaIntrebare(idintrebare);
            idintrebare++;
            button1.Enabled = false;
            button3.Enabled = true;
        }
        public void genereazaIntrebare(int nrintrebare)
        {
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;

            radioBox1.Checked = false;
            radioBox2.Checked = false;
            radioBox3.Checked = false;
            radioBox4.Checked = false;

            radioBoxFalse.Checked = false;
            radioBoxTrue.Checked = false;
            radioBox1.BackColor = Color.Transparent;
            radioBox2.BackColor = Color.Transparent;
            radioBox3.BackColor = Color.Transparent;
            radioBox4.BackColor = Color.Transparent;
            radioBoxTrue.BackColor = Color.Transparent;
            radioBoxFalse.BackColor = Color.Transparent;
            checkBox1.BackColor = Color.Transparent;
            checkBox2.BackColor = Color.Transparent;
            checkBox3.BackColor = Color.Transparent;
            checkBox4.BackColor = Color.Transparent;

            txtRaspuns.Text = "";

            tipIntrebare = Convert.ToInt32(dtNew.Rows[nrintrebare][1].ToString());
            richTextBox1.Text = dtNew.Rows[nrintrebare][0].ToString();
            if (tipIntrebare == 1)
            {
                radioBoxTrue.Visible = false;
                radioBoxFalse.Visible = false;
                radioBox1.Visible = false;
                radioBox2.Visible = false;
                radioBox3.Visible = false;
                radioBox4.Visible = false;

                checkBox1.Visible = false;
                checkBox2.Visible = false;
                checkBox3.Visible = false;
                checkBox4.Visible = false;

                txtRaspuns.Visible = true;
                txtRaspuns.Location = new Point(187, 201);
                this.tabPage1.Controls.Add(txtRaspuns);
            }
            else if (tipIntrebare == 2)
            {
                radioBoxTrue.Visible = false;
                radioBoxFalse.Visible = false;
                txtRaspuns.Visible = false;
                checkBox1.Visible = false;
                checkBox2.Visible = false;
                checkBox3.Visible = false;
                checkBox4.Visible = false;

                radioBox1.Visible = true;
                radioBox2.Visible = true;
                radioBox3.Visible = true;
                radioBox4.Visible = true;

                radioBox1.Text = dtNew.Rows[nrintrebare][3].ToString();
                radioBox2.Text = dtNew.Rows[nrintrebare][4].ToString();
                radioBox3.Text = dtNew.Rows[nrintrebare][5].ToString();
                radioBox4.Text = dtNew.Rows[nrintrebare][6].ToString();
                radioBox1.Location = new Point(187, 150);
                radioBox2.Location = new Point(187, 170);
                radioBox3.Location = new Point(187, 190);
                radioBox4.Location = new Point(187, 210);
                this.tabPage1.Controls.Add(radioBox1);
                this.tabPage1.Controls.Add(radioBox2);
                this.tabPage1.Controls.Add(radioBox3);
                this.tabPage1.Controls.Add(radioBox4);
            }
            else if (tipIntrebare == 3)
            {
                radioBoxTrue.Visible = false;
                radioBoxFalse.Visible = false;
                radioBox1.Visible = false;
                radioBox2.Visible = false;
                radioBox3.Visible = false;
                radioBox4.Visible = false;
                txtRaspuns.Visible = false;

                checkBox1.Visible = true;
                checkBox2.Visible = true;
                checkBox3.Visible = true;
                checkBox4.Visible = true;

                checkBox1.Text = dtNew.Rows[nrintrebare][3].ToString();
                checkBox2.Text = dtNew.Rows[nrintrebare][4].ToString();
                checkBox3.Text = dtNew.Rows[nrintrebare][5].ToString();
                checkBox4.Text = dtNew.Rows[nrintrebare][6].ToString();
                checkBox1.Location = new Point(187, 150);
                checkBox2.Location = new Point(187, 170);
                checkBox3.Location = new Point(187, 190);
                checkBox4.Location = new Point(187, 210);
                this.tabPage1.Controls.Add(checkBox1);
                this.tabPage1.Controls.Add(checkBox2);
                this.tabPage1.Controls.Add(checkBox3);
                this.tabPage1.Controls.Add(checkBox4);
            }
            else if (tipIntrebare == 4)
            {
                radioBox1.Visible = false;
                radioBox2.Visible = false;
                radioBox3.Visible = false;
                radioBox4.Visible = false;
                checkBox1.Visible = false;
                checkBox2.Visible = false;
                checkBox3.Visible = false;
                checkBox4.Visible = false;
                txtRaspuns.Visible = false;
                radioBoxTrue.Visible = true;
                radioBoxFalse.Visible = true;

                radioBoxTrue.Text = "Adevarat";
                radioBoxFalse.Text = "Fals";

                radioBoxTrue.Location = new Point(187, 150);
                radioBoxFalse.Location = new Point(187, 170);

                this.tabPage1.Controls.Add(radioBoxTrue);
                this.tabPage1.Controls.Add(radioBoxFalse);
            }
        }
        public void EndGame()
        {
            if (idintrebare == 9)
            {
                button1.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
                richTextBox1.Visible = false;
                radioBox1.Visible = false;
                radioBox2.Visible = false;
                radioBox3.Visible = false;
                radioBox4.Visible = false;
                checkBox1.Visible = false;
                checkBox2.Visible = false;
                checkBox3.Visible = false;
                checkBox4.Visible = false;
                txtRaspuns.Visible = false;
                radioBoxTrue.Visible = false;
                radioBoxFalse.Visible = false;
                for (int j = 0; j < 5; j++)
                {
                    List<Label> lblIdIntrebare = GenereazaLblIdIntrebare(j);
                    for (int i = 0; i < lblIdIntrebare.Count; i++)
                    {
                        lblIdIntrebare[i].Location = new Point(50 + j*100, 50 + 30 * i);
                        this.tabPage1.Controls.Add(lblIdIntrebare[i]);
                    }
                }
            }
        }
        public List<Label> GenereazaLblIdIntrebare(int col)
        {
            List<Label> lblraport = new List<Label>();
            for (int i = 0; i < reportRaspunsuri.Rows.Count; i++)
            {
                Label label = new Label();
                label.Text = reportRaspunsuri.Rows[i][col].ToString();
                lblraport.Add(label);
            }
            return lblraport;
        }
        private void button2_Click(object sender, EventArgs e)
        {

            EndGame();
            button2.Enabled = false;
            button3.Enabled = true;
            if (idintrebare < 9)
            {
                genereazaIntrebare(idintrebare);
                idintrebare++;
            }
            else
            {
                button2.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            SqlConnection con = new SqlConnection(constr);
            con.Open();

            button2.Enabled = true;
            button3.Enabled = false;
            if (tipIntrebare == 1)
            {
                string raspunsCorect = dtNew.Rows[idintrebare - 1][2].ToString();

                reportRaspunsuri.Rows.Add(Convert.ToInt32(dtNew.Rows[idintrebare - 1][7]), tipIntrebare, dtNew.Rows[idintrebare - 1][0].ToString(), txtRaspuns.Text.ToString(), raspunsCorect);

                if (txtRaspuns.Text.Replace(" ", "").ToLower() == raspunsCorect.ToString().Replace(" ", "").ToLower())
                {
                    punctaj++;
                    label1.Text = "Punctaj " + punctaj.ToString();
                    MessageBox.Show("Ai raspuns corect :)");
                }
                else
                {
                    MessageBox.Show("Raspunsul corect era: " + raspunsCorect);
                }
                
            }
            else if (tipIntrebare == 2)
            {
                int raspunsCorect = Convert.ToInt32(dtNew.Rows[idintrebare - 1][2].ToString());
                
                int raspunstulmeu = -1;
                if (radioBox1.Checked == true)
                {
                    raspunstulmeu = 1;
                }
                else if (radioBox2.Checked == true)
                {
                    raspunstulmeu = 2;
                }
                else if (radioBox3.Checked == true)
                {
                    raspunstulmeu = 3;
                }
                else if (radioBox4.Checked == true)
                {
                    raspunstulmeu = 4;
                }
                reportRaspunsuri.Rows.Add(Convert.ToInt32(dtNew.Rows[idintrebare - 1][7]), tipIntrebare, dtNew.Rows[idintrebare - 1][0].ToString(), raspunstulmeu.ToString(), raspunsCorect);
                if (raspunsCorect == 1)
                {
                    radioBox2.BackColor = Color.Red;
                    radioBox3.BackColor = Color.Red;
                    radioBox4.BackColor = Color.Red;
                    radioBox1.BackColor = Color.Green;
                }
                else if (raspunsCorect == 2)
                {
                    radioBox1.BackColor = Color.Red;
                    radioBox3.BackColor = Color.Red;
                    radioBox4.BackColor = Color.Red;
                    radioBox2.BackColor = Color.Green;
                }
                else if (raspunsCorect == 3)
                {
                    radioBox1.BackColor = Color.Red;
                    radioBox2.BackColor = Color.Red;
                    radioBox4.BackColor = Color.Red;
                    radioBox3.BackColor = Color.Green;
                }
                else if (raspunsCorect == 4)
                {
                    radioBox1.BackColor = Color.Red;
                    radioBox2.BackColor = Color.Red;
                    radioBox3.BackColor = Color.Red;
                    radioBox4.BackColor = Color.Green;
                }
                if (raspunsCorect == raspunstulmeu)
                {
                    punctaj++;
                    label1.Text = "Punctaj " + punctaj.ToString();
                    MessageBox.Show("Ai raspuns corect!");
                }
                else
                {
                    MessageBox.Show("Ai raspuns incorect!");
                }

            }

            else if (tipIntrebare == 3)
            {
                int raspunsCorect = Convert.ToInt32(dtNew.Rows[idintrebare - 1][2].ToString());
                string raspunsulmeu = "";
                if (checkBox1.Checked == true)
                {
                    raspunsulmeu = raspunsulmeu + "1";
                }
                if (checkBox2.Checked == true)
                {
                    raspunsulmeu = raspunsulmeu + "2";
                }
                if (checkBox3.Checked == true)
                {
                    raspunsulmeu = raspunsulmeu + "3";
                }
                if (checkBox4.Checked == true)
                {
                    raspunsulmeu = raspunsulmeu + "4";
                }
                reportRaspunsuri.Rows.Add(Convert.ToInt32(dtNew.Rows[idintrebare - 1][7]), tipIntrebare, dtNew.Rows[idintrebare - 1][0].ToString(), raspunsulmeu, raspunsCorect);
                if (raspunsCorect.ToString().Contains("1"))
                {
                    checkBox1.BackColor = Color.Green;
                }
                else
                {
                    checkBox1.BackColor = Color.Red;
                }
                if (raspunsCorect.ToString().Contains("2"))
                {
                    checkBox2.BackColor = Color.Green;
                }
                else
                {
                    checkBox2.BackColor = Color.Red;
                }
                if (raspunsCorect.ToString().Contains("3"))
                {
                    checkBox3.BackColor = Color.Green;
                }
                else
                {
                    checkBox3.BackColor = Color.Red;
                }
                if (raspunsCorect.ToString().Contains("4"))
                {
                    checkBox4.BackColor = Color.Green;
                }
                else
                {
                    checkBox4.BackColor = Color.Red;
                }
                if (raspunsCorect.ToString() == raspunsulmeu.ToString())
                {
                    punctaj++;
                    label1.Text = "Punctaj " + punctaj.ToString();
                    MessageBox.Show("Ai raspuns corect!");
                }
                else
                {
                    MessageBox.Show("Ai raspuns gresit!");
                }
            }
            else if (tipIntrebare == 4)
            {
                int raspunsCorect = Convert.ToInt32(dtNew.Rows[idintrebare - 1][2].ToString());
                int raspunsulmeu;
                if (radioBoxTrue.Checked == true)
                {
                    raspunsulmeu = 1;
                }
                else
                {
                    raspunsulmeu = 0;
                }
                if (raspunsCorect == 1)
                {
                    radioBoxTrue.BackColor = Color.Green;
                }
                else {
                    radioBoxTrue.BackColor = Color.Red;
                }
                reportRaspunsuri.Rows.Add(Convert.ToInt32(dtNew.Rows[idintrebare - 1][7]), tipIntrebare, dtNew.Rows[idintrebare - 1][0].ToString(), raspunsulmeu.ToString(), raspunsCorect);
                if (raspunsCorect == raspunsulmeu)
                {
                    punctaj++;
                    label1.Text = "Punctaj " + punctaj.ToString();
                    MessageBox.Show("Ai raspuns corect!");
                }
                else
                {
                    MessageBox.Show("Ai raspuns gresit!");
                }
            }
        con.Close();
        }

        private void testeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
        }

        private void carnetDeNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        private void graficNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage3;
        }
    }
}
