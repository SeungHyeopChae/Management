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
    public partial class Form_SIGN : Form
    {
        DataTable signtable;
        DataTable usertable;
        bool checkPW;
        bool checkID;

        public Form_SIGN()
        {
            InitializeComponent();
        }


        private void Form_SIGN_Load(object sender, EventArgs e)
        {
            humanTableAdapter1.Fill(dataSet11.HUMAN);
            usertable = dataSet11.Tables["HUMAN"];
            customerTableAdapter1.Fill(dataSet11.CUSTOMER);
            signtable = dataSet11.Tables["CUSTOMER"];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (DataRow mydataRow in usertable.Rows)
            {
                string check = mydataRow["ID"].ToString();
                if (check == textBox1.Text)
                {                   
                    checkID = false;
                }
                else
                {
                    
                    checkID = true;
                }
            }
            if (checkID == false)
            {
                MessageBox.Show("사용할 수 없는 아이디 입니다.");
            }
            else
                MessageBox.Show("사용할 수 있는 아이디 입니다.");
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (checkID == true && checkPW == true && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "")
            {
                DataRow mynewDataRow = signtable.NewRow();
                mynewDataRow["CUS_ID"] = textBox1.Text;
                mynewDataRow["CUS_NAME"] = textBox4.Text;
                mynewDataRow["CUS_CALL"] = textBox5.Text;
                mynewDataRow["CUS_MAIL"] = textBox6.Text;
                mynewDataRow["CUS_FEE"] = "0";
                signtable.Rows.Add(mynewDataRow);

                DataRow mynewuserRow = usertable.NewRow();
                mynewuserRow["ID"] = textBox1.Text;
                mynewuserRow["PASSWORD"] = textBox3.Text;
                mynewuserRow["ROLE"] = "CUSTOMER";
                usertable.Rows.Add(mynewuserRow);
                int numOfHuman = humanTableAdapter1.Update(dataSet11.HUMAN);
                int numOfCustomer = customerTableAdapter1.Update(dataSet11.CUSTOMER);
                if (numOfHuman < 1 &&numOfCustomer <1)
                    MessageBox.Show("가입 실패");
                else
                    MessageBox.Show("가입 완료");
                this.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            checkID = false;
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
    }
}
