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

namespace petStore.FormChuongTrinh
{
    public partial class fHoaDonBan : Form
    {
        public fHoaDonBan()
        {
            InitializeComponent();
        }

        private void fHoaDonBan_Load(object sender, EventArgs e)
        {
            // lấy dữ liệu đổ vào combobox TENHH
            LayDuLieu_HoaDon(cboTenHH, "SELECT * FROM HANGHOA", "MAHH", "MAHH");

            txtMaHD.Clear();
            txtNhanVien.Clear();
            txtTongTien.Text = "0";
            cboMaKH.Text = "";
            txtTenKH.Clear();
            txtDiaChi.Clear();
            txtSDT.Clear();

            cboTenHH.Text = "";
            txtTenHH.Clear();
            txtSoLuong.Text = "0";
            txtThanhTien.Text = "0";
            txtDGB.Text = "0";
            //Làm mờ 1 vài trường dữ liệu
            txtTenHH.Enabled = false;
            cboTenHH.Enabled = false;
            txtSoLuong.Enabled = false;
            dtpHoaDon.Enabled = false;
            cboMaKH.Enabled = false;
            txtMaHD.Enabled = false;
            txtTenKH.Enabled = false;
            txtNhanVien.Enabled = false;
            txtTongTien.Enabled = false;
            txtDGB.Enabled = false;
            txtThanhTien.Enabled = false;
            txtDiaChi.Enabled = false;
            txtSDT.Enabled = false;
            // làm sáng button
            btnTaoHoaDon.Enabled = true;
            //làm mờ button
            btnHuyThem.Enabled = false;
            btnThem.Enabled = false;
            btnXoa.Enabled = false;

            btnLuu.Enabled = false;
            btnThanhToan.Enabled = false;
        }
        //Hàm tạo khóa có dạng: TientoNgaythangnam_giophutgiay
        public static string CreateKey(string tiento)
        {
            string key = tiento;
            string[] partsDay;
            partsDay = DateTime.Now.ToShortDateString().Split('/');
            //Ví dụ 07/08/2009
            string d = String.Format("{0}{1}{2}", partsDay[1], partsDay[0], partsDay[2]);
            key = key + d;
            string[] partsTime;
            partsTime = DateTime.Now.ToLongTimeString().Split(':');
            //Ví dụ 7:08:03 PM hoặc 7:08:03 AM
            if (partsTime[2].Substring(3, 2) == "PM")
                partsTime[0] = ConvertTimeTo24(partsTime[0]);
            if (partsTime[2].Substring(3, 2) == "AM")
                if (partsTime[0].Length == 1)
                    partsTime[0] = "0" + partsTime[0];
            //Xóa ký tự trắng và PM hoặc AM
            partsTime[2] = partsTime[2].Remove(2, 3);
            string t;
            t = String.Format("_{0}{1}{2}", partsTime[0], partsTime[1], partsTime[2]);
            key = key + t;
            return key;
        }
        //Chuyển đổi từ PM sang dạng 24h
        public static string ConvertTimeTo24(string hour)
        {
            string h = "";
            switch (hour)
            {
                case "1":
                    h = "13";
                    break;
                case "2":
                    h = "14";
                    break;
                case "3":
                    h = "15";
                    break;
                case "4":
                    h = "16";
                    break;
                case "5":
                    h = "17";
                    break;
                case "6":
                    h = "18";
                    break;
                case "7":
                    h = "19";
                    break;
                case "8":
                    h = "20";
                    break;
                case "9":
                    h = "21";
                    break;
                case "10":
                    h = "22";
                    break;
                case "11":
                    h = "23";
                    break;
                case "12":
                    h = "0";
                    break;
            }
            return h;
        }


        #region các Sự kiện khi của các nút
        private void btnThem_Click(object sender, EventArgs e)
        {

        }
        private void btnTaoHoaDon_Click(object sender, EventArgs e)
        {   // lấy dữ liệu đổ vào combobox MaKH
            LayDuLieu_HoaDon(cboMaKH, "SELECT * FROM KHACHHANG", "MAKH", "MAKH");
            
            dtpHoaDon.Value = DateTime.Now;
            txtMaHD.Text = CreateKey("HDB");

            txtNhanVien.Text = fQuanLyChinh.HovaTen;
            txtTongTien.Text = "0";
            cboMaKH.Text = "";
            txtTenKH.Clear();
            txtDiaChi.Clear();
            txtSDT.Clear();

            cboTenHH.Text = "";
            txtSoLuong.Text = "0";
            txtThanhTien.Text = "0";
            txtDGB.Text = "0";

            // làm mờ các button
            btnTaoHoaDon.Enabled = false;
            // Enable các trường nhập dữ liệu
            dtpHoaDon.Enabled = true;
            cboMaKH.Enabled = true;
            cboTenHH.Enabled = true;
            txtSoLuong.Enabled = true;
            // làm sáng lại các button
            btnHuyThem.Enabled = true;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;

            btnLuu.Enabled = true;
            btnThanhToan.Enabled = true;
        }
        private void btnHuyThem_Click(object sender, EventArgs e)
        {
            ConnectData data = new ConnectData();
            data.OpenConnection();
            double sl, slcon, slxoa;
            if (MessageBox.Show("Bạn có chắc chắn muốn hủy Hóa đơn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sql = "SELECT MAHH,SOLUONG FROM HOADONBAN_CHITIET WHERE MAHDBAN = N'" + txtMaHD.Text + "'";
                DataTable tblHang = GetDataToTable(sql);
                for (int hang = 0; hang <= tblHang.Rows.Count - 1; hang++)
                {
                    // Cập nhật lại số lượng cho các mặt hàng
                    sl = Convert.ToDouble(LayMienDuLieu("SELECT SOLUONG FROM HANGHOA WHERE MAHH = N'" + tblHang.Rows[hang][0].ToString() + "'"));
                    slxoa = Convert.ToDouble(tblHang.Rows[hang][1].ToString());
                    slcon = sl + slxoa;
                    sql = "UPDATE HANGHOA SET SOLUONG =" + slcon + " WHERE MAHH= N'" + tblHang.Rows[hang][0].ToString() + "'";
                    SqlCommand cmd = new SqlCommand(sql);
                    data.Update(cmd);
                }

                //Xóa chi tiết hóa đơn
                sql = "DELETE HOADONBAN_CHITIET WHERE MAHDBAN=N'" + txtMaHD.Text + "'";
                SqlCommand cmd1 = new SqlCommand(sql);
                data.Update(cmd1);

                //Xóa hóa đơn
                sql = "DELETE HOADONBAN WHERE MAHDBAN=N'" + txtMaHD.Text + "'";
                SqlCommand cmd2 = new SqlCommand(sql);
                data.Update(cmd2);
            }
            fHoaDonBan_Load(sender, e);
        }
        #endregion
        #region Lấy dữ liệu
        // Lấy dữ liệu vào các ComboBox:
        public void LayDuLieu_HoaDon(ComboBox comboBox, string data, string display, string value)
        {
            ConnectData table = new ConnectData();
            table.OpenConnection();
            string sql = data;
            SqlCommand command = new SqlCommand(sql);
            table.Fill(command);
            comboBox.DataSource = table;
            comboBox.DisplayMember = display;
            comboBox.ValueMember = value;
        }
        // Lấy dữ liệu vào các TextBox:
        public static string LayMienDuLieu(string sql)
        {
            ConnectData data = new ConnectData();
            data.OpenConnection();
            string ma = "";
            SqlCommand cmd = new SqlCommand(sql, data.connection);
            data.Fill(cmd);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
                ma = reader.GetValue(0).ToString();
            reader.Close();
            return ma;
        }
        //Lấy dữ liệu vào bảng
        public static DataTable GetDataToTable(string sql)
        {
            ConnectData data = new ConnectData();
            data.OpenConnection();
            SqlDataAdapter dap = new SqlDataAdapter(sql, data.connection); //Định nghĩa đối tượng thuộc lớp SqlDataAdapter
            //Khai báo đối tượng table thuộc lớp DataTable
            DataTable table = new DataTable();
            dap.Fill(table); //Đổ kết quả từ câu lệnh sql vào table
            return table;
        }
        #endregion
        #region Các sự kiện
        private void cboMaKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            if (cboMaKH.Text == "")
            {
                txtTenKH.Text = "";
                txtDiaChi.Text = "";
                txtSDT.Text = "";
            }
            //Khi chọn Mã khách hàng thì các thông tin của khách hàng sẽ hiện ra
            str = "Select TENKH from KHACHHANG where MAKH = N'" + cboMaKH.SelectedValue + "'";
            txtTenKH.Text = LayMienDuLieu(str);
            str = "Select DIACHI from KHACHHANG where MAKH = N'" + cboMaKH.SelectedValue + "'";
            txtDiaChi.Text = LayMienDuLieu(str);
            str = "Select SDT from KHACHHANG where MAKH = N'" + cboMaKH.SelectedValue + "'";
            txtSDT.Text = LayMienDuLieu(str);
        }
        private void cboTenHH_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            if (cboTenHH.Text == "")
            {
                txtTenHH.Text = "";
                txtDGB.Text = "";
            }
            // Khi chọn mã hàng thì các thông tin về hàng hiện ra
            str = "SELECT TENHH FROM HANGHOA WHERE MAHH =N'" + cboTenHH.SelectedValue + "'";
            txtTenHH.Text = LayMienDuLieu(str);
            str = "SELECT DonGiaBan FROM HANGHOA WHERE MAHH =N'" + cboTenHH.SelectedValue + "'";
            txtDGB.Text = LayMienDuLieu(str);
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            //Khi thay đổi số lượng thì thực hiện tính lại thành tiền
            double tt, sl, dg;
            if (txtSoLuong.Text == "")
                sl = 0;
            else
                sl = Convert.ToDouble(txtSoLuong.Text);
            if (txtDGB.Text == "")
                dg = 0;
            else
                dg = Convert.ToDouble(txtDGB.Text);
            tt = sl * dg;
            txtThanhTien.Text = tt.ToString();
        }
        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else e.Handled = true;
        }
        #endregion

    }
}
