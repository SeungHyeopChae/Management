using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace WindowsFormsApplication1
{
    public partial class Form_CUS : Form
    {
        DataTable clot;
        DataTable payt;
        DataTable catt;
        DataTable rest;
        DataTable rent;
        DataTable revt;
        DataTable cust;
        string CUSID;
        string CLOID;
        string CLONAME;
        string noret;
        string yesret;
        string revnum;
        string revid;
        public Form_CUS(string id)
        {
            InitializeComponent();
            CUSID = id;
        }

        private void Form_CUS_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            Form_LOGIN Form1 = new Form_LOGIN();
            Form1.ShowDialog();
            this.Close();
        }

        private void Form_CUS_Load(object sender, EventArgs e)
        {
           
            // TODO: 이 코드는 데이터를 'dataSet11.CATEGORY' 테이블에 로드합니다. 필요한 경우 이 코드를 이동하거나 제거할 수 있습니다.
            this.cATEGORYTableAdapter.Fill(this.dataSet11.CATEGORY);

            clothesTableAdapter1.Fill(dataSet11.CLOTHES);
            clot = dataSet11.Tables["CLOTHES"];

            payTableAdapter1.Fill(dataSet11.PAY);
            payt = dataSet11.Tables["PAY"];

            categoryTableAdapter1.Fill(dataSet11.CATEGORY);
            catt = dataSet11.Tables["CATEGORY"];

            reservationTableAdapter1.Fill(dataSet11.RESERVATION);
            rest = dataSet11.Tables["RESERVATION"];

            rentalTableAdapter1.Fill(dataSet11.RENTAL);
            rent = dataSet11.Tables["RENTAL"];

            reviewTableAdapter1.Fill(dataSet11.REVIEW);
            revt = dataSet11.Tables["REVIEW"];

            customerTableAdapter1.Fill(dataSet11.CUSTOMER);
            cust = dataSet11.Tables["CUSTOMER"];

            foreach (DataRow cusTAROW in cust.Rows)
            {
                if (cusTAROW["CUS_ID"].ToString() == CUSID)
                {
                    label9.Text = cusTAROW["CUS_NAME"].ToString() + "님 환영합니다.";
                }
            }

            listBox1.Items.Clear();
            foreach (DataRow mydataRow in clot.Rows)
            {
                listBox1.Items.Add(mydataRow["CLO_BRAND"].ToString() + " " + mydataRow["CLO_NAME"].ToString() + " " + mydataRow["CLO_SIZE"].ToString());
            }          
            foreach (DataRow myRow in rent.Rows)
            {
                string MYID = myRow["CUS_ID"].ToString();
                string CLONOM = myRow["CLO_ID"].ToString();
                
                foreach(DataRow mycloRow in clot.Rows)
                {
                    if( mycloRow["CLO_ID"].ToString() == CLONOM)
                    {
                        CLONAME = mycloRow["CLO_NAME"].ToString();
                    }
                }
                if (MYID == CUSID)
                {
                    if (myRow["RET_TIME"].ToString() == "")
                    {
                        listBox3.Items.Add("대여 : " + myRow["REN_TIME"].ToString() + " 반납 : null"  + " 이름 : " + CLONAME);
                    }
                    else
                        listBox3.Items.Add("대여 : " + myRow["REN_TIME"].ToString() + " 반납 : " + myRow["RET_TIME"].ToString() + " 이름 : " + CLONAME);
                }
            }

            oracleConnection1.Open();
            oracleCommand5.CommandText = "SELECT CLO_NAME, co FROM (SELECT CLO_ID, COUNT(CLO_ID) as co FROM RENTAL GROUP BY CLO_ID ORDER BY co DESC) REN, CLOTHES WHERE ROWNUM<=3 AND REN.CLO_ID = CLOTHES.CLO_ID";
            OracleDataReader rdr = oracleCommand5.ExecuteReader();
            while (rdr.Read())
            {
                chart1.Series[0].Points.AddXY(rdr[0], rdr[1]);
            }

            monthCalendar1.MaxSelectionCount = 31;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            foreach (DataRow mydataRow in clot.Rows)
            {
                
                string search_name = mydataRow["CLO_NAME"].ToString();
                string search_brand = mydataRow["CLO_BRAND"].ToString();
                string search_size = mydataRow["CLO_SIZE"].ToString();
                string search_color = mydataRow["CLO_COLOR"].ToString();
                string catid = mydataRow["CAT_ID"].ToString();
                if (textBox1.Text == "" && textBox2.Text == "" && Convert.ToString(comboBox3.SelectedValue)== "000000" && comboBox2.Text == "" && comboBox3.Text == "")
                {
                    listBox1.Items.Add("검색어를 입력해주세요");
                    return;
                }
                else
                {
                    //5개
                    if (search_name == textBox1.Text && catid == Convert.ToString(comboBox3.SelectedValue) && search_brand == textBox2.Text && search_size == comboBox1.Text && search_color == comboBox2.Text)
                    {
                        listBox1.Items.Add(mydataRow["CLO_BRAND"].ToString() + " " + mydataRow["CLO_NAME"].ToString() + " " + mydataRow["CLO_SIZE"].ToString());
                    }
                        //1개
                    else if (search_name == textBox1.Text && Convert.ToString(comboBox3.SelectedValue)=="000000" && textBox2.Text=="" && comboBox1.Text=="" && comboBox2.Text=="")
                    {
                        listBox1.Items.Add(mydataRow["CLO_BRAND"].ToString() + " " + mydataRow["CLO_NAME"].ToString() + " " + mydataRow["CLO_SIZE"].ToString());
                    }
                    else if ( textBox1.Text=="" && catid == Convert.ToString(comboBox3.SelectedValue) &&  textBox2.Text=="" &&  comboBox1.Text=="" &&  comboBox2.Text=="")
                    {
                        listBox1.Items.Add(mydataRow["CLO_BRAND"].ToString() + " " + mydataRow["CLO_NAME"].ToString() + " " + mydataRow["CLO_SIZE"].ToString());
                    }
                    else if (textBox1.Text == "" && Convert.ToString(comboBox3.SelectedValue) == "000000" && search_brand == textBox2.Text &&  comboBox1.Text=="" &&  comboBox2.Text=="")
                    {
                        listBox1.Items.Add(mydataRow["CLO_BRAND"].ToString() + " " + mydataRow["CLO_NAME"].ToString() + " " + mydataRow["CLO_SIZE"].ToString());
                    }
                    else if (textBox1.Text == "" && Convert.ToString(comboBox3.SelectedValue) == "000000" && textBox2.Text=="" && search_size == comboBox1.Text &&  comboBox2.Text=="")
                    {
                        listBox1.Items.Add(mydataRow["CLO_BRAND"].ToString() + " " + mydataRow["CLO_NAME"].ToString() + " " + mydataRow["CLO_SIZE"].ToString());
                    }
                    else if (textBox1.Text == "" && Convert.ToString(comboBox3.SelectedValue) == "000000" &&  textBox2.Text =="" &&  comboBox1.Text=="" && search_color == comboBox2.Text)
                    {
                        listBox1.Items.Add(mydataRow["CLO_BRAND"].ToString() + " " + mydataRow["CLO_NAME"].ToString() + " " + mydataRow["CLO_SIZE"].ToString());
                    }
                    //4개
                    else if (search_name == textBox1.Text && catid == Convert.ToString(comboBox3.SelectedValue) && search_brand == textBox2.Text && search_size == comboBox1.Text && comboBox2.Text == "")
                    {
                        listBox1.Items.Add(mydataRow["CLO_BRAND"].ToString() + " " + mydataRow["CLO_NAME"].ToString() + " " + mydataRow["CLO_SIZE"].ToString());
                    }
                    else if (search_name == textBox1.Text && catid == Convert.ToString(comboBox3.SelectedValue) && search_brand == textBox2.Text && search_color ==comboBox2.Text && comboBox1.Text == "")
                    {
                        listBox1.Items.Add(mydataRow["CLO_BRAND"].ToString() + " " + mydataRow["CLO_NAME"].ToString() + " " + mydataRow["CLO_SIZE"].ToString());
                    }
                    else if (search_name == textBox1.Text && catid == Convert.ToString(comboBox3.SelectedValue) && search_size == comboBox1.Text && search_color == comboBox2.Text && textBox2.Text == "")
                    {
                        listBox1.Items.Add(mydataRow["CLO_BRAND"].ToString() + " " + mydataRow["CLO_NAME"].ToString() + " " + mydataRow["CLO_SIZE"].ToString());
                    }
                    else if (search_name == textBox1.Text && search_brand == textBox2.Text && search_size == comboBox1.Text && search_color == comboBox2.Text && Convert.ToString(comboBox3.SelectedValue) == "000000")
                    {
                        listBox1.Items.Add(mydataRow["CLO_BRAND"].ToString() + " " + mydataRow["CLO_NAME"].ToString() + " " + mydataRow["CLO_SIZE"].ToString());
                    }
                    else if (catid == Convert.ToString(comboBox3.SelectedValue) && search_brand == textBox2.Text && search_size == comboBox1.Text && search_color == comboBox2.Text && textBox1.Text == "")
                    {
                        listBox1.Items.Add(mydataRow["CLO_BRAND"].ToString() + " " + mydataRow["CLO_NAME"].ToString() + " " + mydataRow["CLO_SIZE"].ToString());
                    }
                    //2개
                    else if (search_name == textBox1.Text && catid == Convert.ToString(comboBox3.SelectedValue) && textBox2.Text == "" && comboBox2.Text == "" && comboBox1.Text == "")
                    {
                        listBox1.Items.Add(mydataRow["CLO_BRAND"].ToString() + " " + mydataRow["CLO_NAME"].ToString() + " " + mydataRow["CLO_SIZE"].ToString());
                    }
                    else if (search_name == textBox1.Text && search_brand == textBox2.Text && Convert.ToString(comboBox3.SelectedValue) == "000000" && comboBox2.Text == "" && comboBox1.Text == "")
                    {
                        listBox1.Items.Add(mydataRow["CLO_BRAND"].ToString() + " " + mydataRow["CLO_NAME"].ToString() + " " + mydataRow["CLO_SIZE"].ToString());
                    }
                    else if (search_name == textBox1.Text && search_size == comboBox1.Text && textBox2.Text == "" && Convert.ToString(comboBox3.SelectedValue) == "000000" && comboBox2.Text == "")
                    {
                        listBox1.Items.Add(mydataRow["CLO_BRAND"].ToString() + " " + mydataRow["CLO_NAME"].ToString() + " " + mydataRow["CLO_SIZE"].ToString());
                    }
                    else if (search_name == textBox1.Text && search_color == comboBox2.Text && textBox2.Text == "" && Convert.ToString(comboBox3.SelectedValue) == "000000" && comboBox1.Text == "")
                    {
                        listBox1.Items.Add(mydataRow["CLO_BRAND"].ToString() + " " + mydataRow["CLO_NAME"].ToString() + " " + mydataRow["CLO_SIZE"].ToString());
                    }
                    else if (catid == Convert.ToString(comboBox3.SelectedValue) && search_brand == textBox2.Text && textBox1.Text == "" && comboBox2.Text == "" && comboBox1.Text == "")
                    {
                        listBox1.Items.Add(mydataRow["CLO_BRAND"].ToString() + " " + mydataRow["CLO_NAME"].ToString() + " " + mydataRow["CLO_SIZE"].ToString());
                    }
                    else if (catid == Convert.ToString(comboBox3.SelectedValue) && search_size == comboBox1.Text && textBox1.Text == "" && textBox2.Text == "" && comboBox2.Text == "")
                    {
                        listBox1.Items.Add(mydataRow["CLO_BRAND"].ToString() + " " + mydataRow["CLO_NAME"].ToString() + " " + mydataRow["CLO_SIZE"].ToString());
                    }
                    else if (catid == Convert.ToString(comboBox3.SelectedValue) && search_color == comboBox2.Text && textBox1.Text == "" && textBox2.Text == "" && comboBox1.Text == "")
                    {
                        listBox1.Items.Add(mydataRow["CLO_BRAND"].ToString() + " " + mydataRow["CLO_NAME"].ToString() + " " + mydataRow["CLO_SIZE"].ToString());
                    }
                    else if (search_brand == textBox2.Text && search_size == comboBox1.Text && textBox1.Text == "" && Convert.ToString(comboBox3.SelectedValue) == "000000" &&comboBox2.Text== "")
                    {
                        listBox1.Items.Add(mydataRow["CLO_BRAND"].ToString() + " " + mydataRow["CLO_NAME"].ToString() + " " + mydataRow["CLO_SIZE"].ToString());
                    }
                    else if (search_brand == textBox2.Text && search_color == comboBox2.Text && textBox1.Text == "" && Convert.ToString(comboBox3.SelectedValue) == "000000" && comboBox1.Text == "")
                    {
                        listBox1.Items.Add(mydataRow["CLO_BRAND"].ToString() + " " + mydataRow["CLO_NAME"].ToString() + " " + mydataRow["CLO_SIZE"].ToString());
                    }
                    else if (search_size == comboBox1.Text && search_color == comboBox2.Text && textBox1.Text == "" && textBox2.Text == "" && Convert.ToString(comboBox3.SelectedValue) == "000000")
                    {
                        listBox1.Items.Add(mydataRow["CLO_BRAND"].ToString() + " " + mydataRow["CLO_NAME"].ToString() + " " + mydataRow["CLO_SIZE"].ToString());
                    }
                    //3개
                    else if (search_name == textBox1.Text && catid == Convert.ToString(comboBox3.SelectedValue) && search_brand == textBox2.Text && search_size ==comboBox1.Text && search_color == comboBox2.Text)
                    {
                        listBox1.Items.Add(mydataRow["CLO_BRAND"].ToString() + " " + mydataRow["CLO_NAME"].ToString() + " " + mydataRow["CLO_SIZE"].ToString());
                    }
                    else if (search_name == textBox1.Text && catid == Convert.ToString(comboBox3.SelectedValue) && textBox2.Text == "" && search_size == comboBox1.Text && search_color == comboBox2.Text)
                    {
                        listBox1.Items.Add(mydataRow["CLO_BRAND"].ToString() + " " + mydataRow["CLO_NAME"].ToString() + " " + mydataRow["CLO_SIZE"].ToString());
                    }
                    else if (search_name == textBox1.Text && catid == Convert.ToString(comboBox3.SelectedValue) && textBox2.Text == "" && comboBox1.Text == "" && search_color == comboBox2.Text)
                    {
                        listBox1.Items.Add(mydataRow["CLO_BRAND"].ToString() + " " + mydataRow["CLO_NAME"].ToString() + " " + mydataRow["CLO_SIZE"].ToString());
                    }
                    else if (search_name == textBox1.Text && Convert.ToString(comboBox3.SelectedValue) == "000000" && search_brand == textBox2.Text && search_size == comboBox1.Text && comboBox2.Text == "")
                    {
                        listBox1.Items.Add(mydataRow["CLO_BRAND"].ToString() + " " + mydataRow["CLO_NAME"].ToString() + " " + mydataRow["CLO_SIZE"].ToString());
                    }
                    else if (search_name == textBox1.Text && Convert.ToString(comboBox3.SelectedValue) == "000000" && search_brand == textBox2.Text && comboBox1.Text == "" && search_color == comboBox2.Text)
                    {
                        listBox1.Items.Add(mydataRow["CLO_BRAND"].ToString() + " " + mydataRow["CLO_NAME"].ToString() + " " + mydataRow["CLO_SIZE"].ToString());
                    }
                    else if (search_name == textBox1.Text && Convert.ToString(comboBox3.SelectedValue) == "000000" && textBox2.Text == "" && search_size == comboBox1.Text && search_color == comboBox2.Text)
                    {
                        listBox1.Items.Add(mydataRow["CLO_BRAND"].ToString() + " " + mydataRow["CLO_NAME"].ToString() + " " + mydataRow["CLO_SIZE"].ToString());
                    }
                    else if (textBox1.Text == "" && catid == Convert.ToString(comboBox3.SelectedValue) && search_brand == textBox2.Text && search_size == comboBox1.Text && comboBox2.Text == "")
                    {
                        listBox1.Items.Add(mydataRow["CLO_BRAND"].ToString() + " " + mydataRow["CLO_NAME"].ToString() + " " + mydataRow["CLO_SIZE"].ToString());
                    }
                    else if (textBox1.Text == "" && catid == Convert.ToString(comboBox3.SelectedValue) && search_brand == textBox2.Text && comboBox1.Text == "" && search_color == comboBox2.Text)
                    {
                        listBox1.Items.Add(mydataRow["CLO_BRAND"].ToString() + " " + mydataRow["CLO_NAME"].ToString() + " " + mydataRow["CLO_SIZE"].ToString());
                    }
                    else if (textBox1.Text == "" && catid == Convert.ToString(comboBox3.SelectedValue) && textBox2.Text == "" && search_size == comboBox1.Text && search_color == comboBox2.Text)
                    {
                        listBox1.Items.Add(mydataRow["CLO_BRAND"].ToString() + " " + mydataRow["CLO_NAME"].ToString() + " " + mydataRow["CLO_SIZE"].ToString());
                    }
                    else if (textBox1.Text == "" && catid == Convert.ToString(comboBox3.SelectedValue) && textBox2.Text == "" && search_size == comboBox1.Text && search_color == comboBox2.Text)
                    {
                        listBox1.Items.Add(mydataRow["CLO_BRAND"].ToString() + " " + mydataRow["CLO_NAME"].ToString() + " " + mydataRow["CLO_SIZE"].ToString());
                    }

                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            textBox1.Text = ""; 
            textBox2.Text = "";
            comboBox1.Text ="";
            comboBox2.Text = "";
            comboBox3.SelectedIndex = 0;
            foreach (DataRow mydataRow in clot.Rows)
            {
                listBox1.Items.Add(mydataRow["CLO_BRAND"].ToString() + " " + mydataRow["CLO_NAME"].ToString() + " " + mydataRow["CLO_SIZE"].ToString());
            }
            //str1.Equals(str2, StringComparison.OrdinalIgnoreCase
            //a.Contains(b)
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
            //예약
            string current_time;
            string time07;
            oracleCommand2.CommandText = "SELECT TO_CHAR(SYSDATE, 'YYYYMMDDHH24MISS') FROM DUAL";
            current_time = Convert.ToString(oracleCommand2.ExecuteScalar());
            time07 = current_time.Substring(0,7);

            foreach (DataRow clothRow in clot.Rows)
            {
                if (clothRow["CLO_RENTAL"].ToString() == "0")
                {
                    MessageBox.Show("대여 가능합니다.");
                    return;
                }
            }

            foreach (DataRow resRow in rest.Rows)
            {
                if (resRow["CUS_ID"].ToString() == CUSID && resRow["CLO_ID"].ToString() == CLOID)
                {
                    MessageBox.Show("예약이 이미 있습니다.");
                    return;
                }

            }
            oracleCommand3.CommandText = "SELECT RES_sq.nextval FROM DUAL";
            string RESNO = Convert.ToString(oracleCommand3.ExecuteScalar());
            DataRow newReservation = rest.NewRow();
            newReservation["RES_NO"] = RESNO;
            newReservation["CUS_ID"] = CUSID;
            newReservation["CLO_ID"] = CLOID;
            newReservation["RES_TIME"] = current_time;
            rest.Rows.Add(newReservation);



            DataRow myRow = clot.Rows.Find(CLOID);
            if (myRow != null)
            {
                
                
                    
                    myRow["CLO_RESERVATION"] = Convert.ToInt32(myRow["CLO_RESERVATION"]) + 1;
                    int RESOfRows = reservationTableAdapter1.Update(dataSet11.RESERVATION);
                    int CLOofRows = clothesTableAdapter1.Update(dataSet11.CLOTHES);
                    if (RESOfRows < 1 && CLOofRows < 1)
                        MessageBox.Show("예약 실패");
                    else
                        MessageBox.Show("예약 성공");
            }
            else
                MessageBox.Show("예약 실패");
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (DataRow mydataRow2 in clot.Rows)
            {
                string search2 = mydataRow2["CLO_BRAND"].ToString() + " " + mydataRow2["CLO_NAME"].ToString() + " " + mydataRow2["CLO_SIZE"].ToString();
                if (search2 == Convert.ToString(listBox1.SelectedItem))
                {
                    CLOID = mydataRow2["CLO_ID"].ToString();
                }
            }
            //상세보기
            richTextBox2.Text = "";
            listBox4.Items.Clear();
            foreach (DataRow mydataRow in clot.Rows)
            {
                string search = mydataRow["CLO_BRAND"].ToString() + " " + mydataRow["CLO_NAME"].ToString() + " " + mydataRow["CLO_SIZE"].ToString();
                if (search == Convert.ToString(listBox1.SelectedItem))
                {
                    //selectid = mydataRow["M_ID"].ToString();
                    string price = mydataRow["GRADE"].ToString();
                    foreach (DataRow mydataRows in payt.Rows)
                    {
                        if (price == mydataRows["GRADE"].ToString())
                        {
                            string check;
                            if (mydataRow["CLO_RENTAL"].ToString() == "0")
                                check = "대여 가능";
                            else
                                check = "대여 불가";
                            richTextBox2.Text = "색깔 : " + mydataRow["CLO_COLOR"].ToString() + "\n" + "대여 여부 : " + check + "\n" + "예약 순위 : " + mydataRow["CLO_RESERVATION"].ToString() + "\n" + "대여기간 : " + mydataRow["CLO_RETURNDAY"].ToString() + "일\n" + "가격 : " + mydataRows["FEE"].ToString() + "\n" + "벌금 : " + mydataRows["LATEFEE"].ToString() +"\n"+"등급 : "+mydataRow["GRADE"].ToString();
                            //CLOID = mydataRow["CLO_ID"].ToString();
                            foreach (DataRow rentalRow in rent.Rows)
                            {
                                if (mydataRow["CLO_ID"].ToString() == rentalRow["CLO_ID"].ToString())
                                {
                                    string ren_no = rentalRow["REN_NO"].ToString();
                                    foreach (DataRow reviewlRow in revt.Rows)
                                    {
                                        if (ren_no == reviewlRow["REN_NO"].ToString())
                                        {
                                            listBox4.Items.Add(reviewlRow["TITLE"].ToString());
                                            revid = reviewlRow["REV_NO"].ToString();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }    
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string a = listBox3.SelectedItem.ToString();
            string b = "null";
            bool c = a.Contains(b);
            if (c == false)
            {
                richTextBox1.Visible = true;
                button7.Visible = true;
                label11.Visible = true;
                textBox3.Visible = true;
            }
            else
            {
                richTextBox1.Visible = false;
                button7.Visible = false;
                label11.Visible = false;
                textBox3.Visible = false;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            foreach (DataRow myRow in rent.Rows)
            {
                string MYID = myRow["CUS_ID"].ToString();
                string CLONOM = myRow["CLO_ID"].ToString();

                foreach (DataRow mycloRow in clot.Rows)
                {
                    if (mycloRow["CLO_ID"].ToString() == CLONOM)
                    {
                        CLONAME = mycloRow["CLO_NAME"].ToString();
                    }
                }

                if (MYID == CUSID)
                {
                    if (myRow["RET_TIME"].ToString() == "")
                    {
                        noret = "대여 : " + myRow["REN_TIME"].ToString() + " 반납 : null" + " 이름 : " + CLONAME;
                        revnum = myRow["REN_NO"].ToString();
                    }
                    else
                    {
                        yesret = "대여 : " + myRow["REN_TIME"].ToString() + " 반납 : " + myRow["RET_TIME"].ToString() + " 이름 : " + CLONAME;
                        revnum = myRow["REN_NO"].ToString();
                    }
                }

                if (yesret == Convert.ToString(listBox3.SelectedItem)||noret == Convert.ToString(listBox3.SelectedItem))
                {
                    foreach (DataRow reviewRows in revt.Rows)
                    {
                        if (reviewRows["REN_NO"].ToString() == myRow["REN_NO"].ToString())
                        {
                            MessageBox.Show("이미 리뷰를 등록했습니다.");
                            textBox3.Text = "";
                            richTextBox1.Text = "";
                            return;
                        }
                    }
                    oracleCommand4.CommandText = "SELECT REV_SQ.nextval FROM DUAL";
                    string REVNO = Convert.ToString(oracleCommand4.ExecuteScalar());

                    DataRow reviewRow = revt.NewRow();
                    reviewRow["REV_NO"] = REVNO;
                    reviewRow["REN_NO"] = revnum;
                    reviewRow["COMMENTS"] = richTextBox1.Text;
                    reviewRow["TITLE"] = textBox3.Text;
                    revt.Rows.Add(reviewRow);
                    int revofRow = reviewTableAdapter1.Update(dataSet11.REVIEW);
                    if (revofRow > 0)
                    {
                        MessageBox.Show("후기 등록 완료");
                        textBox3.Text = "";
                        richTextBox1.Text = "";
                        return;
                    }
                    else
                        MessageBox.Show("후기 등록 실패");
                }
            }
        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (DataRow revRow in revt.Rows)
            {
                if (revid == revRow["REV_NO"].ToString())
                {
                    richTextBox3.Text = revRow["COMMENTS"].ToString();
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            if (checkBox1.Checked == true)
            {
                foreach (DataRow mydataRow in clot.Rows)
                {
                    if (mydataRow["CLO_RENTAL"].ToString() == "0")
                    {
                        listBox1.Items.Add(mydataRow["CLO_BRAND"].ToString() + " " + mydataRow["CLO_NAME"].ToString() + " " + mydataRow["CLO_SIZE"].ToString());
                    }
                }
            }
            else
            {
                foreach (DataRow mydataRow in clot.Rows)
                {
                    listBox1.Items.Add(mydataRow["CLO_BRAND"].ToString() + " " + mydataRow["CLO_NAME"].ToString() + " " + mydataRow["CLO_SIZE"].ToString());
                }
            }
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            if (monthCalendar1.SelectionRange.Start == monthCalendar1.SelectionRange.End)
            {
                MessageBox.Show(monthCalendar1.SelectionRange.Start.ToString("yyyy/MM/dd"));
                //MessageBox.Show(monthCalendar1.SelectionStart.ToShortDateString());
                chart1.Series[0].Points.Clear();
                oracleCommand1.CommandText = "SELECT CLO_NAME, co FROM (SELECT CLO_ID, COUNT(CLO_ID) as co FROM RENTAL WHERE :aa <= REN_TIME AND REN_TIME <= :bb GROUP BY CLO_ID ORDER BY co DESC) REN, CLOTHES WHERE ROWNUM<=3 AND REN.CLO_ID = CLOTHES.CLO_ID";
                oracleCommand1.Parameters[0].Value = monthCalendar1.SelectionRange.Start.ToString("yyyy/MM/dd");
                oracleCommand1.Parameters[1].Value = monthCalendar1.SelectionRange.Start.ToString("yyyy/MM/dd");
                OracleDataReader rd = oracleCommand1.ExecuteReader();
                while (rd.Read())
                {
                    chart1.Series[0].Points.AddXY(rd[0], rd[1]);
                }
            }
            else
            {
                MessageBox.Show(monthCalendar1.SelectionRange.Start.ToString("yyyy/MM/dd") + "~" + monthCalendar1.SelectionRange.End.ToString("yyyy/MM/dd"));
                chart1.Series[0].Points.Clear();
                oracleCommand1.CommandText = "SELECT CLO_NAME, co FROM (SELECT CLO_ID, COUNT(CLO_ID) as co FROM RENTAL WHERE :aa <= REN_TIME AND REN_TIME <= :bb GROUP BY CLO_ID ORDER BY co DESC) REN, CLOTHES WHERE ROWNUM<=3 AND REN.CLO_ID = CLOTHES.CLO_ID";
                oracleCommand1.Parameters[0].Value = monthCalendar1.SelectionRange.Start.ToString("yyyy/MM/dd");
                oracleCommand1.Parameters[1].Value = monthCalendar1.SelectionRange.End.ToString("yyyy/MM/dd");
                OracleDataReader rd = oracleCommand1.ExecuteReader();
                while (rd.Read())
                {
                    chart1.Series[0].Points.AddXY(rd[0], rd[1]);
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form_MYP FormMYP = new Form_MYP(CUSID);
            FormMYP.ShowDialog();
        }
    }
}
