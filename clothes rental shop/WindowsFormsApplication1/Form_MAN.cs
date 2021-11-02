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
    public partial class Form_MAN : Form
    {
        DataTable top10;
        DataTable rent;
        DataTable clot;
        DataTable rest;
        DataTable payt;
        DataTable latt;
        DataTable cust;
        int i = 0;
        int monthofnum = 0;
        string removeid;
        int yearofRow = 2019;
        public Form_MAN()
        {
            InitializeComponent();
            //clsForm = log;
        }

        private void Form_Man_Load(object sender, EventArgs e)
        {
            // TODO: 이 코드는 데이터를 'dataSet11.HUMAN' 테이블에 로드합니다. 필요한 경우 이 코드를 이동하거나 제거할 수 있습니다.
            this.hUMANTableAdapter.Fill(this.dataSet11.HUMAN);
            // TODO: 이 코드는 데이터를 'dataSet11.RENTAL' 테이블에 로드합니다. 필요한 경우 이 코드를 이동하거나 제거할 수 있습니다.
            this.rENTALTableAdapter.Fill(this.dataSet11.RENTAL);
            // TODO: 이 코드는 데이터를 'dataSet11.RENTAL' 테이블에 로드합니다. 필요한 경우 이 코드를 이동하거나 제거할 수 있습니다.
            this.rENTALTableAdapter.Fill(this.dataSet11.RENTAL);
            // TODO: 이 코드는 데이터를 'dataSet11.BLACKLIST' 테이블에 로드합니다. 필요한 경우 이 코드를 이동하거나 제거할 수 있습니다.
            this.bLACKLISTTableAdapter.Fill(this.dataSet11.BLACKLIST);
            // TODO: 이 코드는 데이터를 'dataSet11.CUSTOMER' 테이블에 로드합니다. 필요한 경우 이 코드를 이동하거나 제거할 수 있습니다.
            this.cUSTOMERTableAdapter.Fill(this.dataSet11.CUSTOMER);
            // TODO: 이 코드는 데이터를 'dataSet11.CATEGORY' 테이블에 로드합니다. 필요한 경우 이 코드를 이동하거나 제거할 수 있습니다.
            this.cATEGORYTableAdapter.Fill(this.dataSet11.CATEGORY);
            // TODO: 이 코드는 데이터를 'dataSet11.PAY' 테이블에 로드합니다. 필요한 경우 이 코드를 이동하거나 제거할 수 있습니다.
            this.pAYTableAdapter.Fill(this.dataSet11.PAY);
            // TODO: 이 코드는 데이터를 'dataSet11.CLOTHES' 테이블에 로드합니다. 필요한 경우 이 코드를 이동하거나 제거할 수 있습니다.
            this.cLOTHESTableAdapter.Fill(this.dataSet11.CLOTHES);

            rENTALTableAdapter.Fill(dataSet11.RENTAL);
            rent = dataSet11.Tables["RENTAL"];

            cLOTHESTableAdapter.Fill(dataSet11.CLOTHES);
            clot = dataSet11.Tables["CLOTHES"];

            reservationTableAdapter1.Fill(dataSet11.RESERVATION);
            rest = dataSet11.Tables["RESERVATION"];

            pAYTableAdapter.Fill(dataSet11.PAY);
            payt = dataSet11.Tables["PAY"];

            latefeeTableAdapter1.Fill(dataSet11.LATEFEE);
            latt = dataSet11.Tables["LATEFEE"];

            cUSTOMERTableAdapter.Fill(dataSet11.CUSTOMER);
            cust = dataSet11.Tables["CUSTOMER"];

            oracleConnection1.Open();
            //monthCalendar1.MaxSelectionCount = 31;

            DateTime today = DateTime.Now.Date;
            DateTime first = today.AddDays(1 - today.Day);
            DateTime last = first.AddMonths(1).AddDays(-1);
            chart3.Series[0].Points.Clear();
            for (int i = 0; i < last.Day; i++)
            {
                oracleCommand5.CommandText = "SELECT GRADE, COUNT(*) as co FROM RENTAL WHERE :aa = REN_TIME GROUP BY GRADE";
                oracleCommand5.Parameters[0].Value = today.AddDays(1 - today.Day + i).ToString("yyyy-MM-dd");
                OracleDataReader drd = oracleCommand5.ExecuteReader();
                int sum1 = 0;
                while (drd.Read())
                {
                    string payfee;
                    foreach (DataRow payOfROws in payt.Rows)
                    {
                        if (drd["GRADE"].ToString() == payOfROws["GRADE"].ToString())
                        {
                            payfee = payOfROws["FEE"].ToString();
                            //MessageBox.Show(Convert.ToString(Convert.ToInt32(payfee) * Convert.ToInt32(drd["co"])));
                            sum1 += Convert.ToInt32(payfee) * Convert.ToInt32(drd["co"]);
                        }
                    }
                }
                foreach (DataRow latofROws in latt.Rows)
                {
                    if (latofROws["LAT_TIME"].ToString() == today.AddDays(1 - today.Day + i).ToString("yyyy-MM-dd"))
                    {
                        sum1 += Convert.ToInt32(latofROws["LAT_FEE"]);
                    }
                }
                chart3.Series[0].Points.AddXY(today.AddDays(1 - today.Day + i).ToString("yyyy-MM-dd"), sum1);
            }
            chart4.Series[0].Points.Clear();

            for (int i = 1; i < 8; i++)
            {
                oracleCommand6.CommandText = "SELECT GRADE, COUNT(*) as co FROM RENTAL WHERE :aa = TO_CHAR(TO_DATE(REN_TIME,'yyyy-MM-dd'),'D' )GROUP BY GRADE";
                oracleCommand6.Parameters[0].Value = i;
                OracleDataReader drd = oracleCommand6.ExecuteReader();
                int sum1 = 0;
                while (drd.Read())
                {
                    string payfee;
                    foreach (DataRow payOfROws in payt.Rows)
                    {
                        if (drd["GRADE"].ToString() == payOfROws["GRADE"].ToString())
                        {
                            payfee = payOfROws["FEE"].ToString();
                            //MessageBox.Show(Convert.ToString(Convert.ToInt32(payfee) * Convert.ToInt32(drd["co"])));
                            sum1 += Convert.ToInt32(payfee) * Convert.ToInt32(drd["co"]);
                        }
                    }
                }
                string strDay = "";
                switch (i)
                {
                    case 1:
                        strDay = "일";
                        break;
                    case 2:
                        strDay = "월";
                        break;
                    case 3:
                        strDay = "화";
                        break;
                    case 4:
                        strDay = "수";
                        break;
                    case 5:
                        strDay = "목";
                        break;
                    case 6:
                        strDay = "금";
                        break;
                    case 7:
                        strDay = "토";
                        break;
                }
                foreach (DataRow latofROws in latt.Rows)
                {
                    string latDay = "";
                    switch(Convert.ToDateTime(latofROws["LAT_TIME"]).DayOfWeek)
                    {
                        case DayOfWeek.Sunday:
                            latDay = "일";
                            break;
                        case DayOfWeek.Monday:
                            latDay = "월";
                            break;
                        case DayOfWeek.Tuesday:
                            latDay = "화";
                            break;
                        case DayOfWeek.Wednesday:
                            latDay = "수";
                            break;
                        case DayOfWeek.Thursday:
                            latDay = "목";
                            break;
                        case DayOfWeek.Friday:
                            latDay = "금";
                            break;
                        case DayOfWeek.Saturday:
                            latDay = "토";
                            break;
                    }
                    if (latDay == strDay)
                    {
                        sum1 += Convert.ToInt32(latofROws["LAT_FEE"]);
                    }
                }
                
                chart4.Series[0].Points.AddXY(strDay, sum1);
            }

            chart5.Series[0].Points.Clear();
            for (int i = 1; i < 13; i++)
            {
                oracleCommand7.CommandText = "SELECT GRADE, COUNT(*) as co FROM RENTAL WHERE :aa = TO_CHAR(TO_DATE(REN_TIME,'yyyy-MM-dd'),'MM' )GROUP BY GRADE";
                oracleCommand7.Parameters[0].Value = i.ToString();
                OracleDataReader drd = oracleCommand7.ExecuteReader();
                int sum1 = 0;
                while (drd.Read())
                {
                    string payfee;
                    foreach (DataRow payOfROws in payt.Rows)
                    {
                        if (drd["GRADE"].ToString() == payOfROws["GRADE"].ToString())
                        {
                            payfee = payOfROws["FEE"].ToString();
                            //MessageBox.Show(Convert.ToString(Convert.ToInt32(payfee) * Convert.ToInt32(drd["co"])));
                            sum1 += Convert.ToInt32(payfee) * Convert.ToInt32(drd["co"]);
                        }
                    }
                }
                foreach (DataRow latofROws in latt.Rows)
                {
                    if (Convert.ToDateTime(latofROws["LAT_TIME"]).ToString("MM") == i.ToString() )
                    {
                        sum1 += Convert.ToInt32(latofROws["LAT_FEE"]);
                    }
                }
                chart5.Series[0].Points.AddXY(i.ToString() + "월", sum1);
            }

            chart7.Series[0].Points.Clear();
            foreach (DataRow myLATofRow in clot.Rows)
            {
                string CLONO = myLATofRow["CLO_ID"].ToString();

                int sum = 0;
                int latfee = 0;
                foreach (DataRow mydataRow in rent.Rows)
                {
                    string rental_time = mydataRow["REN_TIME"].ToString();
                    string cusid = mydataRow["CUS_ID"].ToString();
                    string return_time = mydataRow["RET_TIME"].ToString();
                    string cloid = mydataRow["CLO_ID"].ToString();
                    string retgrade = mydataRow["GRADE"].ToString();
                    string latefee = "";

                    if (cloid == CLONO)//렌탈기록에 있을때
                    {
                        if (return_time != "")//반납되었을때
                        {
                            // 날짜 비교
                            int result = DateTime.Compare(Convert.ToDateTime(rental_time).AddDays(Convert.ToInt32(myLATofRow["CLO_RETURNDAY"])), Convert.ToDateTime(return_time));
                            if (result < 0)//1번째 보다 2번째가 더 클때, 연체되었을 때
                            {
                                //"빌린날 + 기본기간 < 반납날";
                                TimeSpan t1 = Convert.ToDateTime(rental_time).AddDays(Convert.ToInt32(myLATofRow["CLO_RETURNDAY"])) - Convert.ToDateTime(return_time);
                                int t2 = t1.Days;
                                t2 = -1 * t2;
                                foreach (DataRow mypayRow in payt.Rows)
                                {
                                    if (retgrade == mypayRow["GRADE"].ToString())
                                    {
                                        latefee = mypayRow["LATEFEE"].ToString();
                                        sum += Convert.ToInt32(mypayRow["FEE"]);//대여료 받아오기
                                    }
                                }
                                foreach (DataRow mycusRow in cust.Rows)
                                {
                                    if (mycusRow["CUS_ID"].ToString() == cusid)
                                    {
                                        latfee = t2 * Convert.ToInt32(latefee);
                                    }
                                }
                                foreach (DataRow mylatRow in latt.Rows)
                                {
                                    if (mylatRow["CUS_ID"].ToString() == mydataRow["CUS_ID"].ToString())
                                    {
                                        if (Convert.ToInt32(mylatRow["LAT_FEE"]) == latfee)
                                        {
                                            sum += latfee;
                                        }
                                    }
                                }
                            }
                            else//연체되지 않았을때
                            {
                                foreach (DataRow mypayRow in payt.Rows)
                                {
                                    if (retgrade == mypayRow["GRADE"].ToString())
                                    {
                                        sum += Convert.ToInt32(mypayRow["FEE"]);//대여료 받아오기
                                    }
                                }
                            }

                        }
                        else//반납 되지 않았을때
                        {
                            foreach (DataRow mypayRow in payt.Rows)
                            {
                                if (retgrade == mypayRow["GRADE"].ToString())
                                {
                                    sum += Convert.ToInt32(mypayRow["FEE"]);
                                }
                            }
                        }
                    }

                }
                chart7.Series[0].Points.AddXY(myLATofRow["CLO_NAME"].ToString(), sum);

            }


        }

        private void Form_MAN_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            Form_LOGIN Form1 = new Form_LOGIN();
            Form1.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cLOTHESBindingSource.AddNew();
            oracleCommand1.CommandText = "SELECT CLO_SQ.nextval FROM DUAL";
            string CLOID = Convert.ToString(oracleCommand1.ExecuteScalar());
            dataGridView1.CurrentRow.Cells[0].Value = CLOID.ToString();
            dataGridView1.CurrentRow.Cells[7].Value = "0";
            dataGridView1.CurrentRow.Cells[8].Value = 0;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            cLOTHESBindingSource.RemoveCurrent();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            this.cLOTHESBindingSource.EndEdit();
            this.pAYBindingSource.EndEdit();
            this.bLACKLISTBindingSource.EndEdit();
            this.hUMANBindingSource.EndEdit();
            int cot = this.cLOTHESTableAdapter.Update(this.dataSet11.CLOTHES);
            if (cot > 0)
            {
                foreach (DataRow mycloRow in clot.Rows)
                {
                    foreach (DataRow myrentRow in rent.Rows)
                    {
                        if (mycloRow["CLO_ID"].ToString() == myrentRow["CLO_ID"].ToString())
                        {
                            if (mycloRow["GRADE"].ToString() != myrentRow["GRADE"].ToString())
                            {
                                myrentRow["GRADE"] = mycloRow["GRADE"].ToString();
                            }
                        }
                    }
                }
            }
            int set = this.reservationTableAdapter1.Update(this.dataSet11.RESERVATION);
            int pet = this.pAYTableAdapter.Update(this.dataSet11.PAY);
            int bet = this.bLACKLISTTableAdapter.Update(this.dataSet11.BLACKLIST);
            int hut = this.hUMANTableAdapter.Update(this.dataSet11.HUMAN);
            int ret = this.rENTALTableAdapter.Update(this.dataSet11.RENTAL);

            if (cot > 0 || pet > 0 || bet > 0 || ret > 0 || hut > 0 || set > 0)
                MessageBox.Show("Update successful");
            else
                MessageBox.Show("Update failed");
            //try
            //{

            //}
            //catch (System.Exception ex)
            //{
            //    MessageBox.Show("오류발생");
            //}
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bLACKLISTBindingSource.AddNew();
            dataGridView5.CurrentRow.Cells[0].Value = Convert.ToString(dataGridView4.CurrentRow.Cells[0].Value);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            bLACKLISTBindingSource.RemoveCurrent();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value));
            rENTALBindingSource.Filter = "CLO_ID  = '" + Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value) + "'";
        }





        private void button9_Click(object sender, EventArgs e)
        {
            rENTALBindingSource.RemoveFilter();
            this.cLOTHESTableAdapter.Fill(this.dataSet11.CLOTHES);
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception != null)
            {
                MessageBox.Show("입력을 취소 합니다.");
            }
        }

        private void dataGridView5_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception != null)
            {
                MessageBox.Show("입력을 취소 합니다.");
            }
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            hUMANBindingSource.AddNew();
            dataGridView3.CurrentRow.Cells[2].Value = "MANAGER";
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            hUMANBindingSource.AddNew();
            dataGridView3.CurrentRow.Cells[2].Value = "STAFF";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (Convert.ToString(dataGridView3.CurrentRow.Cells[2].Value) == "CUSTOMER")
            {
                i = 0;
                //예약이 있으면 예약 내역 삭제와 용품의 예약 번호 -1
                foreach (DataRow reserOfRows in rest.Rows)
                {
                    if (Convert.ToString(dataGridView3.CurrentRow.Cells[0].Value) == reserOfRows["CUS_ID"].ToString())
                    {
                        removeid = reserOfRows["CLO_ID"].ToString();
                        i++;
                    }
                    foreach (DataRow cloOfROws in clot.Rows)
                    {
                        if (cloOfROws["CLO_ID"].ToString() == removeid)
                        {
                            if (Convert.ToInt32(cloOfROws["CLO_RESERVATION"]) > 0)
                            {
                                cloOfROws["CLO_RESERVATION"] = Convert.ToInt32(cloOfROws["CLO_RESERVATION"]) - i;
                            }
                        }
                    }
                    reserOfRows.Delete();
                }
            }
            hUMANBindingSource.RemoveCurrent();

        }

        private void dataGridView3_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception != null)
            {
                MessageBox.Show("입력을 취소 합니다.");
            }
        }

        

        
        

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            MessageBox.Show(Convert.ToString(GetWeekOfMonth(Convert.ToDateTime(dateTimePicker1.Value.ToString("yyyy-MM-dd")))));
            chart1.Series[0].Points.Clear();
            oracleCommand3.CommandText = "SELECT CLO_NAME, co FROM (SELECT CLO_ID, COUNT(CLO_ID) as co FROM RENTAL WHERE To_CHAR(To_DATE(REN_TIME,'YYYY-MM-DD'),'W') = :aa GROUP BY CLO_ID ORDER BY co DESC) REN, CLOTHES WHERE ROWNUM<=3 AND REN.CLO_ID = CLOTHES.CLO_ID";
            oracleCommand3.Parameters[0].Value = Convert.ToString(GetWeekOfMonth(Convert.ToDateTime(dateTimePicker1.Value.ToString("yyyy-MM-dd"))));
            OracleDataReader rd = oracleCommand3.ExecuteReader();
            while (rd.Read())
            {
                chart1.Series[0].Points.AddXY(rd[0], rd[1]);
            }
        }
        public static int GetWeekOfMonth(DateTime dt)
        {
            DateTime now = dt;
            int basisWeekOfDay = (now.Day - 1) % 7;
            int thisWeek = (int)now.DayOfWeek;
            double val = Math.Ceiling((double)now.Day / 7);
            if (basisWeekOfDay > thisWeek) val++;
            return Convert.ToInt32(val);
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            chart2.Series[0].Points.Clear();
            oracleCommand4.CommandText = "SELECT GRADE, COUNT(*) as co FROM RENTAL WHERE :aa = REN_TIME GROUP BY GRADE";
            oracleCommand4.Parameters[0].Value = dateTimePicker2.Value.ToString("yyyy-MM-dd");

            OracleDataReader drd = oracleCommand4.ExecuteReader();
            int sum = 0;
            while (drd.Read())
            {
                string payfee;

                foreach (DataRow payOfROws in payt.Rows)
                {
                    if (drd["GRADE"].ToString() == payOfROws["GRADE"].ToString())
                    {
                        payfee = payOfROws["FEE"].ToString();
                        //MessageBox.Show(Convert.ToString(Convert.ToInt32(payfee) * Convert.ToInt32(drd["co"])));
                        sum += Convert.ToInt32(payfee) * Convert.ToInt32(drd["co"]);
                        foreach (DataRow rentoFROWs in rent.Rows)
                        {
                            if (rentoFROWs["REN_TIME"].ToString() == dateTimePicker2.Value.ToString("yyyy-MM-dd"))
                            {
                                foreach (DataRow clooFROWs in clot.Rows)
                                {
                                    if (rentoFROWs["CLO_ID"].ToString() == clooFROWs["CLO_ID"].ToString())
                                    {
                                        listBox1.Items.Add("대여료 : " + clooFROWs["CLO_NAME"].ToString() + " " + payfee);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            foreach (DataRow latofROws in latt.Rows)
            {
                if (latofROws["LAT_TIME"].ToString() == dateTimePicker2.Value.ToString("yyyy-MM-dd"))
                {
                    sum += Convert.ToInt32(latofROws["LAT_FEE"]);
                    foreach (DataRow cusoFROWs in cust.Rows)
                    {
                        if (latofROws["CUS_ID"].ToString() == cusoFROWs["CUS_ID"].ToString())
                        {
                            listBox1.Items.Add("벌금 : " + cusoFROWs["CUS_NAME"].ToString() + " " + latofROws["LAT_FEE"].ToString());
                        }
                    }
                }
            }
            chart2.Series[0].Points.AddXY(dateTimePicker2.Value.ToString("yyyy-MM-dd"), sum);

        }

        private void button10_Click(object sender, EventArgs e)
        {

            MessageBox.Show(DateTime.Now.AddMonths(--monthofnum).Date.ToString("yyyy-MM"));
            DateTime today = DateTime.Now.AddMonths(monthofnum).Date;
            DateTime first = today.AddDays(1 - today.Day);
            DateTime last = first.AddMonths(1).AddDays(-1);
            chart3.Series[0].Points.Clear();
            for (int i = 0; i < last.Day; i++)
            {
                oracleCommand5.CommandText = "SELECT GRADE, COUNT(*) as co FROM RENTAL WHERE :aa = REN_TIME GROUP BY GRADE";
                oracleCommand5.Parameters[0].Value = today.AddDays(1 - today.Day + i).ToString("yyyy-MM-dd");
                OracleDataReader drd = oracleCommand5.ExecuteReader();
                int sum1 = 0;
                while (drd.Read())
                {
                    string payfee;
                    foreach (DataRow payOfROws in payt.Rows)
                    {
                        if (drd["GRADE"].ToString() == payOfROws["GRADE"].ToString())
                        {
                            payfee = payOfROws["FEE"].ToString();
                            //MessageBox.Show(Convert.ToString(Convert.ToInt32(payfee) * Convert.ToInt32(drd["co"])));
                            sum1 += Convert.ToInt32(payfee) * Convert.ToInt32(drd["co"]);
                        }
                    }
                }
                foreach (DataRow latofROws in latt.Rows)
                {
                    if (latofROws["LAT_TIME"].ToString() == today.AddDays(1 - today.Day + i).ToString("yyyy-MM-dd"))
                    {
                        sum1 += Convert.ToInt32(latofROws["LAT_FEE"]);
                    }
                }
                chart3.Series[0].Points.AddXY(today.AddDays(1 - today.Day + i).ToString("yyyy-MM-dd"), sum1);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {

            MessageBox.Show(DateTime.Now.AddMonths(++monthofnum).Date.ToString("yyyy-MM"));
            DateTime today = DateTime.Now.AddMonths(monthofnum).Date;

            DateTime first = today.AddDays(1 - today.Day);
            DateTime last = first.AddMonths(1).AddDays(-1);
            chart3.Series[0].Points.Clear();
            for (int i = 0; i < last.Day; i++)
            {
                oracleCommand5.CommandText = "SELECT GRADE, COUNT(*) as co FROM RENTAL WHERE :aa = REN_TIME GROUP BY GRADE";
                oracleCommand5.Parameters[0].Value = today.AddDays(1 - today.Day + i).ToString("yyyy-MM-dd");
                OracleDataReader drdr = oracleCommand5.ExecuteReader();
                int sum1 = 0;
                while (drdr.Read())
                {
                    string payfee;
                    foreach (DataRow payOfROws in payt.Rows)
                    {
                        if (drdr["GRADE"].ToString() == payOfROws["GRADE"].ToString())
                        {
                            payfee = payOfROws["FEE"].ToString();
                            //MessageBox.Show(Convert.ToString(Convert.ToInt32(payfee) * Convert.ToInt32(drd["co"])));
                            sum1 += Convert.ToInt32(payfee) * Convert.ToInt32(drdr["co"]);
                        }
                    }
                }
                foreach (DataRow latofROws in latt.Rows)
                {
                    if (latofROws["LAT_TIME"].ToString() == today.AddDays(1 - today.Day + i).ToString("yyyy-MM-dd"))
                    {
                        sum1 += Convert.ToInt32(latofROws["LAT_FEE"]);
                    }
                }
                chart3.Series[0].Points.AddXY(today.AddDays(1 - today.Day + i).ToString("yyyy-MM-dd"), sum1);
            }

        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            oracleCommand2.CommandText = "SELECT CLO_NAME, co FROM (SELECT CLO_ID, COUNT(CLO_ID) as co FROM RENTAL WHERE :aa <= REN_TIME AND REN_TIME <= :bb GROUP BY CLO_ID ORDER BY co DESC) REN, CLOTHES WHERE ROWNUM<=3 AND REN.CLO_ID = CLOTHES.CLO_ID";
            oracleCommand2.Parameters[0].Value = dateTimePicker3.Value.ToString("yyyy/MM/dd");
            oracleCommand2.Parameters[1].Value = dateTimePicker3.Value.ToString("yyyy/MM/dd");
            OracleDataReader rd = oracleCommand2.ExecuteReader();
            while (rd.Read())
            {
                chart1.Series[0].Points.AddXY(rd[0], rd[1]);
            }
        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            oracleCommand2.CommandText = "SELECT CLO_NAME, co FROM (SELECT CLO_ID, COUNT(CLO_ID) as co FROM RENTAL WHERE :aa <= REN_TIME AND REN_TIME <= :bb GROUP BY CLO_ID ORDER BY co DESC) REN, CLOTHES WHERE ROWNUM<=3 AND REN.CLO_ID = CLOTHES.CLO_ID";
            oracleCommand2.Parameters[0].Value = dateTimePicker4.Value.ToString("yyyy-MM") + "-01";
            //MessageBox.Show(comboBox1.SelectedItem.ToString() + "-01");
            oracleCommand2.Parameters[1].Value = dateTimePicker4.Value.ToString("yyyy-MM") + "-31";
            //MessageBox.Show(comboBox1.SelectedItem.ToString() + "-31");
            OracleDataReader rd = oracleCommand2.ExecuteReader();
            while (rd.Read())
            {
                chart1.Series[0].Points.AddXY(rd[0], rd[1]);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            chart5.Series[0].Points.Clear();
            yearofRow -= 1;
            for (int i = 1; i < 13; i++)
            {
                oracleCommand7.CommandText = "SELECT GRADE, COUNT(*) as co FROM RENTAL WHERE :aa = TO_CHAR(TO_DATE(REN_TIME,'yyyy-MM-dd'),'yyyy-MM' )GROUP BY GRADE";
                oracleCommand7.Parameters[0].Value = yearofRow.ToString()+"-"+ i.ToString();
                OracleDataReader drd = oracleCommand7.ExecuteReader();
                int sum1 = 0;
                while (drd.Read())
                {
                    string payfee;
                    foreach (DataRow payOfROws in payt.Rows)
                    {
                        if (drd["GRADE"].ToString() == payOfROws["GRADE"].ToString())
                        {
                            payfee = payOfROws["FEE"].ToString();
                            //MessageBox.Show(Convert.ToString(Convert.ToInt32(payfee) * Convert.ToInt32(drd["co"])));
                            sum1 += Convert.ToInt32(payfee) * Convert.ToInt32(drd["co"]);
                        }
                    }
                }
                foreach (DataRow latofROws in latt.Rows)
                {
                    if (Convert.ToDateTime(latofROws["LAT_TIME"]).ToString("yyyy-MM") == yearofRow.ToString() + "-" + i.ToString())
                    {
                        sum1 += Convert.ToInt32(latofROws["LAT_FEE"]);
                    }
                }
                chart5.Series[0].Points.AddXY(yearofRow.ToString() + "년" + i.ToString() + "월", sum1);
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            chart5.Series[0].Points.Clear();
            yearofRow += 1;
            for (int i = 1; i < 13; i++)
            {
                oracleCommand7.CommandText = "SELECT GRADE, COUNT(*) as co FROM RENTAL WHERE :aa = TO_CHAR(TO_DATE(REN_TIME,'yyyy-MM-dd'),'yyyy-MM' )GROUP BY GRADE";
                oracleCommand7.Parameters[0].Value = yearofRow.ToString() +"-"+ i.ToString();
                OracleDataReader drd = oracleCommand7.ExecuteReader();
                int sum1 = 0;
                while (drd.Read())
                {
                    string payfee;
                    foreach (DataRow payOfROws in payt.Rows)
                    {
                        if (drd["GRADE"].ToString() == payOfROws["GRADE"].ToString())
                        {
                            payfee = payOfROws["FEE"].ToString();
                            //MessageBox.Show(Convert.ToString(Convert.ToInt32(payfee) * Convert.ToInt32(drd["co"])));
                            sum1 += Convert.ToInt32(payfee) * Convert.ToInt32(drd["co"]);
                        }
                    }
                }
                foreach (DataRow latofROws in latt.Rows)
                {
                    if (Convert.ToDateTime(latofROws["LAT_TIME"]).ToString("yyyy-MM") == yearofRow.ToString() + "-" + i.ToString())
                    {
                        sum1 += Convert.ToInt32(latofROws["LAT_FEE"]);
                    }
                }
                chart5.Series[0].Points.AddXY(yearofRow.ToString()+"년"+ i.ToString() + "월", sum1);
            }

        }
    }

}
