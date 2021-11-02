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

    public partial class Form_MYP : Form
    {
        DataTable cust;
        DataTable humt;
        string CUSID;
        bool checkPW;
        public Form_MYP(string id)
        {
            InitializeComponent();
            CUSID = id;
        }

        private void Form_MYP_Load(object sender, EventArgs e)
        {

            customerTableAdapter1.Fill(dataSet11.CUSTOMER);
            cust = dataSet11.Tables["CUSTOMER"];
            humanTableAdapter1.Fill(dataSet11.HUMAN);
            humt = dataSet11.Tables["HUMAN"];

            foreach (DataRow mydataRow in cust.Rows)
            {
                if (mydataRow["CUS_ID"].ToString() == CUSID)
                {
                    label8.Text = CUSID;
                    textBox4.Text = mydataRow["CUS_NAME"].ToString();
                    textBox5.Text = mydataRow["CUS_CALL"].ToString();
                    textBox6.Text = mydataRow["CUS_MAIL"].ToString();
                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == textBox3.Text)
            {
                label7.Text = "비밀번호 일치";
                label7.ForeColor = Color.Green;
                checkPW = true;
            }
            else
            {
                label7.Text = "비밀번호 일치하지않음";
                label7.ForeColor = Color.Red;
                checkPW = false;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == textBox3.Text)
            {
                label7.Text = "비밀번호 일치";
                label7.ForeColor = Color.Green;
                checkPW = true;
            }
            else
            {
                label7.Text = "비밀번호 일치하지않음";
                label7.ForeColor = Color.Red;
                checkPW = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkPW == true && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "")
            {
                foreach (DataRow custRow in cust.Rows)
                {
                    if (custRow["CUS_ID"].ToString() == CUSID)
                    {
                        custRow["CUS_NAME"] = textBox4.Text;
                        custRow["CUS_CALL"] = textBox5.Text;
                        custRow["CUS_MAIL"] = textBox6.Text;
                    }
                }
                foreach (DataRow humtRow in humt.Rows)
                {
                    if (humtRow["ID"].ToString() == CUSID)
                    {
                        humtRow["PASSWORD"] = textBox3.Text;
                    }

                }
                int numOfHuman = humanTableAdapter1.Update(dataSet11.HUMAN);
                int numOfCustomer = customerTableAdapter1.Update(dataSet11.CUSTOMER);
                if (numOfHuman < 1 && numOfCustomer < 1)
                    MessageBox.Show("수정 실패");
                else
                    MessageBox.Show("수정 완료");
                this.Close();
            }
        }       
    }
}
