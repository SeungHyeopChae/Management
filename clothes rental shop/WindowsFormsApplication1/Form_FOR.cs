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
    public partial class Form_FOR : Form
    {
        DataTable fortable;
        bool forpw;
        bool forid;

        public Form_FOR()
        {
            InitializeComponent();
        }

        private void Form_FOR_Load(object sender, EventArgs e)
        {
            customerTableAdapter1.Fill(dataSet11.CUSTOMER);
            fortable = dataSet11.Tables["CUSTOMER"];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (DataRow mydataRow in fortable.Rows)
            {
                string check_name = mydataRow["CUS_NAME"].ToString();
                string check_call = mydataRow["CUS_CALL"].ToString();
                if (check_name == textBox1.Text)
                {
                    if (check_call == textBox2.Text)
                    {
                        forid = true;
                        string check_id = mydataRow["CUS_ID"].ToString();
                        MessageBox.Show("아이디는" + check_id + "입니다.");
                        return;
                    }
                    else
                        forid = false;
                }
                else
                    forid = false;
            }
            if (forid == false)
            {
                MessageBox.Show("일치하는 정보가 없습니다.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (DataRow mydataRow in fortable.Rows)
            {
                string check_id2 = mydataRow["CUS_ID"].ToString();
                string check_name2 = mydataRow["CUS_NAME"].ToString();
                string check_call2 = mydataRow["CUS_CALL"].ToString();
                if (check_id2 == textBox3.Text)
                {
                    if (check_name2 == textBox4.Text)
                    {
                        if (check_call2 == textBox5.Text)
                        {
                            forpw = true;
                            string check_pw = mydataRow["CUS_ID"].ToString();
                            MessageBox.Show("비밀번호는" + check_pw + "입니다.");
                            return;
                        }
                        else
                            forpw = false;
                    }
                    else
                        forpw = false;
                }
                else
                    forpw = false;
            }
            if (forpw == false)
            {
                MessageBox.Show("일치하는 정보가 없습니다.");
            }
        }
    }
}
