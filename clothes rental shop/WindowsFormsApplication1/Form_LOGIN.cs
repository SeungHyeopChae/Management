using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form_LOGIN : Form
    {
        DataTable mytable;
        bool check_login = false;
        public Form_LOGIN()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (DataRow mydataRow in mytable.Rows)
            {
                string id = mydataRow["ID"].ToString();
                string pw = mydataRow["PASSWORD"].ToString();
                string role = mydataRow["ROLE"].ToString();
                if (id == textBox1.Text)
                {
                    if (pw == textBox2.Text)
                    {
                        if (role == "MANAGER")
                        {
                            this.Hide();
                            Form_MAN Form2 = new Form_MAN();
                            Form2.ShowDialog();
                            this.Close();
                            check_login = true;
                            //textBox1.Text = "";
                            //textBox2.Text = "";
                            return;
                        }
                        else if (role == "STAFF")
                        {
                            this.Hide();
                            Form_STA Form3 = new Form_STA(id);
                            Form3.ShowDialog();
                            this.Close();
                            check_login = true;
                            //textBox1.Text = "";
                            //textBox2.Text = "";
                            return;
                        }
                        else
                        {
                            this.Hide();
                            Form_CUS Form4 = new Form_CUS(id);
                            Form4.ShowDialog();
                            this.Close();
                            check_login = true;
                            //textBox1.Text = "";
                            //textBox2.Text = "";
                            return;
                        }
                    }
                }
            }
            if (check_login == false)
            {
                MessageBox.Show("아이디 또는 비밀번호가 틀렸습니다.");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            humanTableAdapter1.Fill(dataSet11.HUMAN);
            mytable = dataSet11.Tables["HUMAN"];
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form_SIGN Form5 = new Form_SIGN();
            Form5.ShowDialog();
            humanTableAdapter1.Fill(dataSet11.HUMAN);
            mytable = dataSet11.Tables["HUMAN"];
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form_FOR Form6 = new Form_FOR();
            Form6.ShowDialog();
        }

        private void Form_LOGIN_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                foreach (DataRow mydataRow in mytable.Rows)
                {
                    string id = mydataRow["ID"].ToString();
                    string pw = mydataRow["PASSWORD"].ToString();
                    string role = mydataRow["ROLE"].ToString();
                    if (id == textBox1.Text)
                    {
                        if (pw == textBox2.Text)
                        {
                            if (role == "MANAGER")
                            {
                                this.Hide();
                                Form_MAN Form2 = new Form_MAN();
                                Form2.ShowDialog();
                                this.Close();
                                check_login = true;
                                //textBox1.Text = "";
                                //textBox2.Text = "";
                                return;
                            }
                            else if (role == "STAFF")
                            {
                                this.Hide();
                                Form_STA Form3 = new Form_STA(id);
                                Form3.ShowDialog();
                                this.Close();
                                check_login = true;
                                //textBox1.Text = "";
                                //textBox2.Text = "";
                                return;
                            }
                            else
                            {
                                this.Hide();
                                Form_CUS Form4 = new Form_CUS(id);
                                Form4.ShowDialog();
                                this.Close();
                                check_login = true;
                                //textBox1.Text = "";
                                //textBox2.Text = "";
                                return;
                            }
                        }
                    }
                }
                if (check_login == false)
                {
                    MessageBox.Show("아이디 또는 비밀번호가 틀렸습니다.");
                }
            }
            else
            {
                return;
            }
        }
    }
}
