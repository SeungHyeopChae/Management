using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;

namespace WindowsFormsApplication1
{
    public partial class Form_STA : Form
    {
        DataTable rent;
        DataTable cust;
        DataTable clot;
        DataTable payt;
        DataTable blat;
        DataTable rest;
        DataTable latt;

        string rental_time;
        string STAID;
        string cusid;
        string latefee;
        public Form_STA(string id)
        {
            InitializeComponent();
            STAID = id;
        }
        void MailSetting( string usermail, string clothes)
        {
            MailMessage message = new MailMessage();
            message.To.Add(usermail);
            message.From = new MailAddress("swisscom2021@gmail.com", "test", System.Text.Encoding.UTF8);
            MailAddress bcc = new MailAddress("swisscom2021@gmail.com");//참조 메일계정
            message.Bcc.Add(bcc);
            message.Subject = "고객님께";
            message.SubjectEncoding = UTF8Encoding.UTF8;
            message.Body = "고객님께서 예약하신 "+ clothes +"이(가) 반납 되었습니다. 대여 하시러 오시기 바랍니다.";
            message.BodyEncoding = UTF8Encoding.UTF8;
            message.IsBodyHtml = true; //메일내용이 HTML형식임
            message.Priority = MailPriority.High; //중요도 높음
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure; //메일 배달 실패시 알림
            //Attachment attFile = new Attachment("d\\image1.jpg");//첨부파일

            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com"; //SMTP(발송)서버 도메인
            client.Port = 587; //25, SMTP서버 포트
            client.EnableSsl = true; //SSL 사용
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("swisscom2021@gmail.com", "letsbee119");//보내는 사람 메일 서버접속계정, 암호, Anonymous이용시 생략
            client.Send(message);

            message.Dispose();
        }




        private void Form_STA_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            Form_LOGIN Form1 = new Form_LOGIN();
            Form1.ShowDialog();
            this.Close();
        }

        private void Form_STA_Load(object sender, EventArgs e)
        {
            // TODO: 이 코드는 데이터를 'dataSet11.CATEGORY' 테이블에 로드합니다. 필요한 경우 이 코드를 이동하거나 제거할 수 있습니다.
            this.cATEGORYTableAdapter.Fill(this.dataSet11.CATEGORY);
            // TODO: 이 코드는 데이터를 'dataSet1.DataTable1' 테이블에 로드합니다. 필요한 경우 이 코드를 이동하거나 제거할 수 있습니다.
            this.dataTable1TableAdapter.FillTHREE(this.dataSet11.DataTable1);
            // TODO: 이 코드는 데이터를 'dataSet11.DataTable2' 테이블에 로드합니다. 필요한 경우 이 코드를 이동하거나 제거할 수 있습니다.
            this.dataTable2TableAdapter.FillFOUR(this.dataSet11.DataTable2);

            // TODO: 이 코드는 데이터를 'dataSet11.DataTable1' 테이블에 로드합니다. 필요한 경우 이 코드를 이동하거나 제거할 수 있습니다.
            this.dataTable1TableAdapter.FillTHREE(this.dataSet11.DataTable1);
            // TODO: 이 코드는 데이터를 'dataSet1.RENTAL' 테이블에 로드합니다. 필요한 경우 이 코드를 이동하거나 제거할 수 있습니다.
            this.rENTALTableAdapter.Fill(this.dataSet11.RENTAL);
            // TODO: 이 코드는 데이터를 'dataSet1.CLOTHES' 테이블에 로드합니다. 필요한 경우 이 코드를 이동하거나 제거할 수 있습니다.
            this.cLOTHESTableAdapter.Fill(this.dataSet11.CLOTHES);
            // TODO: 이 코드는 데이터를 'dataSet1.CUSTOMER' 테이블에 로드합니다. 필요한 경우 이 코드를 이동하거나 제거할 수 있습니다.
            this.cUSTOMERTableAdapter.Fill(this.dataSet11.CUSTOMER);

            rENTALTableAdapter.Fill(dataSet11.RENTAL);
            rent = dataSet11.Tables["RENTAL"];

            cUSTOMERTableAdapter.Fill(dataSet11.CUSTOMER);
            cust = dataSet11.Tables["CUSTOMER"];

            cLOTHESTableAdapter.Fill(dataSet11.CLOTHES);
            clot = dataSet11.Tables["CLOTHES"];

            payTableAdapter1.Fill(dataSet11.PAY);
            payt = dataSet11.Tables["PAY"];

            blacklistTableAdapter1.Fill(dataSet11.BLACKLIST);
            blat = dataSet11.Tables["BLACKLIST"];

            reservationTableAdapter1.Fill(dataSet11.RESERVATION);
            rest = dataSet11.Tables["RESERVATION"];

            latefeeTableAdapter1.Fill(dataSet11.LATEFEE);
            latt = dataSet11.Tables["LATEFEE"];

            oracleConnection1.Open();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cUSTOMERBindingSource.Filter != null)
            {
                cUSTOMERBindingSource.RemoveFilter();
                textBox1.Text = "";
                button1.Text = "검색";
            }
            else
            {
                cUSTOMERBindingSource.Filter = string.Format("CUS_NAME like '%{0}%'", textBox1.Text );
                button1.Text = "검색 해제";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (cLOTHESBindingSource.Filter != null)
            {
                cLOTHESBindingSource.RemoveFilter();
                textBox2.Text = "";
                button2.Text = "검색";
            }
            else
            {
                cLOTHESBindingSource.Filter = string.Format("CLO_NAME like '%{0}%'", textBox2.Text );
                button2.Text = "검색 해제";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {      
            //대여
                foreach (DataRow mydataRow in clot.Rows)
                {
                    string CLONO = mydataRow["CLO_ID"].ToString();
                    string CLOFG = mydataRow["CLO_RENTAL"].ToString();
                    string cgrade = mydataRow["GRADE"].ToString();

                    
                    
                    
                    if (CLONO == Convert.ToString(dataGridView2.CurrentRow.Cells[0].Value))
                    {                    
                        if (CLOFG == "0")
                        {
                            foreach (DataRow cusRow in cust.Rows)
                            {
                                if (cusRow["CUS_ID"].ToString() == Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value))
                                {
                                    if (Convert.ToInt32(cusRow["CUS_FEE"]) > 0)
                                    {

                                        MessageBox.Show("연체료가 있습니다.");
                                        return;
                                    }
                                }
                                foreach (DataRow blackRow in blat.Rows)
                                {
                                    if (Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value) == blackRow["CUS_ID"].ToString())
                                    {
                                        if (cgrade != "D")
                                        {
                                            MessageBox.Show("블랙리스트는 제일 낮은 등급의 용품만 빌릴 수 있습니다.");
                                            return;
                                        }
                                    }
                                }
                               
                            }
                            int i = 100;
                            foreach (DataRow myresRow in rest.Rows)
                            {
                                if (Convert.ToInt32(mydataRow["CLO_RESERVATION"]) != 0)
                                {
                                    if(i > Convert.ToInt32(myresRow["RES_NO"]))
                                    {
                                        i = Convert.ToInt32(myresRow["RES_NO"]);
                                    }
                                }
                            }
                            for (int j = rest.Rows.Count - 1; j >= 0; j--)
                            {
                                DataRow dr = rest.Rows[j];
                                if (Convert.ToInt32(dr["RES_NO"]) == i)
                                {
                                    if (dr["CUS_ID"].ToString() == Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value))
                                    {
                                        mydataRow["CLO_RESERVATION"] = Convert.ToInt32(mydataRow["CLO_RESERVATION"]) - 1;
                                        dr.Delete();
                                    }
                                    else
                                    {
                                        MessageBox.Show("예약이 이미 있습니다");
                                        return;
                                    }
                                }
                            }
                            

                            string current_time;
                            oracleCommand1.CommandText = "SELECT TO_CHAR(SYSDATE, 'YYYY-MM-DD') FROM DUAL";
                            current_time = Convert.ToString(oracleCommand1.ExecuteScalar());

                            oracleCommand2.CommandText = "SELECT RENT_SQ.nextval FROM DUAL";
                            string RENNO = Convert.ToString(oracleCommand2.ExecuteScalar());
                            


                            DataRow rentaldata = rent.NewRow();
                            rentaldata["REN_NO"] = RENNO;
                            rentaldata["REN_TIME"] = current_time;
                            rentaldata["CUS_ID"] = Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value);
                            rentaldata["CLO_ID"] = Convert.ToString(dataGridView2.CurrentRow.Cells[0].Value);
                            rentaldata["STA_ID"] = STAID;
                            rentaldata["GRADE"] = cgrade;
                            rent.Rows.Add(rentaldata);

                            
                            


                            mydataRow["CLO_RENTAL"] = "1";
                            //mydataRow["CLO_RENTAL"] = Convert.ToInt32(mydataRow["CLO_RENTAL"]) - 1;

                            if (MessageBox.Show("대여 하시겠습니까?", "대여확인", MessageBoxButtons.YesNo) == DialogResult.No)
                            {
                                return;
                            }

                            int CLOofRows = cLOTHESTableAdapter.Update(dataSet11.CLOTHES);
                            int RENTofRows = rENTALTableAdapter.Update(dataSet11.RENTAL);
                            int resOfRows = reservationTableAdapter1.Update(dataSet11.RESERVATION);
                            dataTable1TableAdapter.FillTHREE(dataSet11.DataTable1);
                            dataTable2TableAdapter.FillFOUR(dataSet11.DataTable2);
                            if (RENTofRows > 0 && CLOofRows > 0)
                            {
                                MessageBox.Show("대여 성공");
                                return;
                            }
                            else
                            {
                                MessageBox.Show("대여 실패");
                                return;
                            }
                        }
                        else
                            MessageBox.Show("대여 확인 해주세요");
                    }
                }
            
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (DataRow mydataRow in cust.Rows)
            {
                string id = mydataRow["CUS_ID"].ToString(); 
                int fee = Convert.ToInt32(mydataRow["CUS_FEE"]);
                if (textBox5.Text == "")
                {
                    MessageBox.Show("벌금을 입력해주세요.");
                    return;
                }
                int textfee = Convert.ToInt32(textBox5.Text);
                if (id == Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value))
                {
                    if ((fee - textfee) < 0)
                    {
                        MessageBox.Show("벌금을 다시 확인 하세요");
                    }
                    else
                    {
                        mydataRow["CUS_FEE"] = Convert.ToString(fee - textfee);

                        oracleCommand2.CommandText = "SELECT LAT_SQ.nextval FROM DUAL";
                        string LATNO = Convert.ToString(oracleCommand2.ExecuteScalar());

                        oracleCommand1.CommandText = "SELECT TO_CHAR(SYSDATE, 'YYYY-MM-DD') FROM DUAL";
                        string LATtime = Convert.ToString(oracleCommand1.ExecuteScalar());

                        DataRow dr = latt.NewRow();
                        dr["LAT_NO"] = LATNO;
                        dr["CUS_ID"] = Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value);
                        dr["LAT_TIME"] = LATtime;
                        dr["LAT_FEE"] = textBox5.Text;
                        latt.Rows.Add(dr);


                        int latofRows = latefeeTableAdapter1.Update(dataSet11.LATEFEE);
                        int numOfRows = cUSTOMERTableAdapter.Update(dataSet11.CUSTOMER);
                        if (numOfRows > 0 && latofRows > 0)
                            MessageBox.Show("벌금 수납 되었습니다.");
                        else
                            MessageBox.Show("에러 발생");
                    }
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataTable1BindingSource.Filter != null)
            {
                dataTable1BindingSource.RemoveFilter();
                textBox1.Text = "";
                button6.Text = "검색";
            }
            else
            {
                dataTable1BindingSource.Filter = "CUS_ID = '" + textBox3.Text + "'";
                button6.Text = "검색 해제";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //반납

            string return_time;
            
            oracleCommand1.CommandText = "SELECT TO_CHAR(SYSDATE, 'YYYY-MM-DD') FROM DUAL";
            return_time = Convert.ToString(oracleCommand1.ExecuteScalar());
            
            foreach (DataRow mydataRow in rent.Rows)
            {
                string RENNOM = mydataRow["REN_NO"].ToString();
                if (RENNOM == Convert.ToString(dataGridView3.CurrentRow.Cells[0].Value))
                {
                    mydataRow["RET_TIME"] = return_time;
                    mydataRow["RET_ID"] = STAID;
                    rental_time = mydataRow["REN_TIME"].ToString();
                    cusid = mydataRow["CUS_ID"].ToString();
                }
            }
            //에약 낮추기
            int i = 100;
            foreach (DataRow mydataofRow in clot.Rows)
            {
                foreach (DataRow myresRow in rest.Rows)
                {
                    if (Convert.ToInt32(mydataofRow["CLO_RESERVATION"]) != 0)
                    {
                        if (i > Convert.ToInt32(myresRow["RES_NO"]))
                        {
                            i = Convert.ToInt32(myresRow["RES_NO"]);
                        }
                    }
                }             
            }
            //예약 메일 보내기
            
            for (int j = rest.Rows.Count - 1; j >= 0; j--)
            {
                DataRow dr = rest.Rows[j];
                if (Convert.ToInt32(dr["RES_NO"]) == i)
                {
                    foreach (DataRow cusofRows in cust.Rows)
                    {
                        if (cusofRows["CUS_ID"].ToString() == dr["CUS_ID"].ToString())
                        {
                            string cloOfid = dr["CLO_ID"].ToString();
                            foreach(DataRow cloofRows in clot.Rows)
                            {
                                if (cloOfid == cloofRows["CLO_ID"].ToString())
                                {
                                    MessageBox.Show("메일을 보내는중입니다.");
                                    MailSetting(cusofRows["CUS_MAIL"].ToString(),cloofRows["CLO_NAME"].ToString());
                                    break;
                                }
                            }
                            
                            
                        }
                    }
                }
            }
            

            foreach (DataRow mydataRow in clot.Rows)
            {
                string CLONO = mydataRow["CLO_ID"].ToString();
                string CLOFG = mydataRow["CLO_RENTAL"].ToString();
                string GRADE = mydataRow["GRADE"].ToString();
                if (CLONO == Convert.ToString(dataGridView3.CurrentRow.Cells[3].Value))
                {
                    if (CLOFG == "1")
                    {
                        mydataRow["CLO_RENTAL"] = "0";
                        
                        // 날짜 비교
                        int result = DateTime.Compare(Convert.ToDateTime(rental_time).AddDays(Convert.ToInt32(mydataRow["CLO_RETURNDAY"])), Convert.ToDateTime(return_time));
                        if (result < 0)
                        {//2번째가 더 클때
                            //textBox4.Text = "빌린날 + 기본기간 < 반납날";
                            TimeSpan t1 = Convert.ToDateTime(rental_time).AddDays(Convert.ToInt32(mydataRow["CLO_RETURNDAY"])) - Convert.ToDateTime(return_time);
                            int t2 = t1.Days;
                            t2 = -1 * t2;
                            
                            

                            foreach (DataRow mypayRow in payt.Rows)
                            {
                                if (GRADE == mypayRow["GRADE"].ToString())
                                {
                                    latefee = mypayRow["LATEFEE"].ToString(); 
                                }
                            }

                            foreach (DataRow mycusRow in cust.Rows)
                            {
                                if (mycusRow["CUS_ID"].ToString() == cusid)
                                {
                                    mycusRow["CUS_FEE"] = t2 * Convert.ToInt32(latefee);
                                }
                            }
                        }
                        int CLOofRows = cLOTHESTableAdapter.Update(dataSet11.CLOTHES);
                        int RENTofRows = rENTALTableAdapter.Update(dataSet11.RENTAL);
                        int CUSofRows = cUSTOMERTableAdapter.Update(dataSet11.CUSTOMER);
                        dataTable1TableAdapter.FillTHREE(dataSet11.DataTable1);
                        dataTable2TableAdapter.FillFOUR(dataSet11.DataTable2);
                        if (RENTofRows > 0 && CLOofRows > 0)
                        {
                            MessageBox.Show("반납 성공");
                            return;
                        }

                        else
                        {
                            MessageBox.Show("반납 실패");
                            return;
                        }
                    }
                    else
                        MessageBox.Show("대여 확인 해주세요");
                }
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataTable2BindingSource.Filter != null)
            {
                dataTable2BindingSource.RemoveFilter();
                dataTable2BindingSource.Filter = "CLO_ID = '" + Convert.ToString(dataGridView2.CurrentRow.Cells[0].Value) + "'";
            }
            else
            dataTable2BindingSource.Filter = "CLO_ID = '" + Convert.ToString(dataGridView2.CurrentRow.Cells[0].Value) + "'";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            dataTable2BindingSource.RemoveFilter();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int i = 100;
            foreach (DataRow mydataRow in clot.Rows)
            {
                string CLONO = mydataRow["CLO_ID"].ToString();




                if (rest.Rows.Count > 0)
                {
                    if (CLONO == Convert.ToString(dataGridView4.CurrentRow.Cells[2].Value))
                    {
                        for (int j = rest.Rows.Count - 1; j >= 0; j--)
                        {
                            DataRow dr = rest.Rows[j];
                            if (Convert.ToString(dr["RES_NO"]) == Convert.ToString(dataGridView4.CurrentRow.Cells[0].Value))
                            {
                                if (dr["CUS_ID"].ToString() == Convert.ToString(dataGridView4.CurrentRow.Cells[1].Value))
                                {
                                    mydataRow["CLO_RESERVATION"] = Convert.ToInt32(mydataRow["CLO_RESERVATION"]) - 1;
                                    dr.Delete();
                                }
                            }
                        }
                        int resOfRows = reservationTableAdapter1.Update(dataSet11.RESERVATION);
                        int CLOofRows = cLOTHESTableAdapter.Update(dataSet11.CLOTHES);
                        dataTable2TableAdapter.FillFOUR(dataSet11.DataTable2);
                        if (resOfRows > 0 && CLOofRows > 0)
                        {
                            MessageBox.Show("삭제 완료");
                            return;
                        }
                        else
                        {
                            MessageBox.Show("삭제 실패");
                            return;
                        }

                    }

                }
                else
                {
                    MessageBox.Show("실패");
                    return;
                }
            }
        }
    }
}
