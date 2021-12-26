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
    public partial class fShowHoaDonBan : Form
    {
        // Khai báo biến toàn cục
        ConnectData datahoadonban = new ConnectData();
        public fShowHoaDonBan()
        {
            InitializeComponent();
        }

        private void fShowHoaDonBan_Load(object sender, EventArgs e)
        {
            // kết nối với csdl
            datahoadonban.OpenConnection();
            // Đổ dữ liệu lên datagridview
            LayDuLieu_HoaDon();
            // Việt hóa tiêu đề dgvNhanVien
            dgvHoaDonBan.Columns["MAHDBAN"].HeaderText = "MÃ HD";
            dgvHoaDonBan.Columns["MANV"].HeaderText = "MÃ NV";
            dgvHoaDonBan.Columns["MAKH"].HeaderText = "MÃ KH";
            dgvHoaDonBan.Columns["NGAYLAP"].HeaderText = "NGÀY LẬP";
            dgvHoaDonBan.Columns["THANHTIEN"].HeaderText = "TỔNG TIỀN";
        }
        public void LayDuLieu_HoaDon()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM HOADONBAN");
            datahoadonban.Fill(cmd);
            BindingSource binding1 = new BindingSource();
            binding1.DataSource = datahoadonban;
            dgvHoaDonBan.DataSource = binding1;
            bindingNavigator1.BindingSource = binding1;
        }
        public void LayDuLieu_ChiTietHoaDon()
        {
            ConnectData data = new ConnectData();
            data.OpenConnection();
            SqlCommand cmd = new SqlCommand("SELECT TENHH FROM HOADONBAN_CHITIET ct, HANGHOA hh WHERE ct.MaHH = hh.MAHH");
            data.Fill(cmd);
            BindingSource binding1 = new BindingSource();
            binding1.DataSource = data;
            dgvChiTiet.DataSource = binding1;
            bindingNavigator2.BindingSource = binding1;
        }
        public void LayDuLieu_ChiTietHoaDon(string mahdb)
        {
            ConnectData data = new ConnectData();
            data.OpenConnection();
            SqlCommand cmd = new SqlCommand(@"SELECT a.MaHH, b.TENHH, a.SoLuong, a.DGban, a.ThanhTien 
                                              FROM HOADONBAN_CHITIET AS a, HANGHOA AS b
                                              WHERE MaHDban = @mahd AND a.MaHH = b.MAHH");
            cmd.Parameters.Add("@mahd", SqlDbType.VarChar).Value = mahdb;
            data.Fill(cmd);
            BindingSource binding1 = new BindingSource();
            binding1.DataSource = data;
            dgvChiTiet.DataSource = binding1;
            bindingNavigator2.BindingSource = binding1;
        }

        private void dgvHoaDonBan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            string mahd = dgvHoaDonBan.SelectedRows[0].Cells["MAHDBAN"].Value.ToString(); 
            LayDuLieu_ChiTietHoaDon(mahd);
            dgvChiTiet.Columns["MaHH"].HeaderText = "Mã hàng";
            dgvChiTiet.Columns["TenHH"].HeaderText = "Tên hàng";
            dgvChiTiet.Columns["SoLuong"].HeaderText = "Số lượng";
            dgvChiTiet.Columns["DGban"].HeaderText = "Đơn giá";
            dgvChiTiet.Columns["ThanhTien"].HeaderText = "Thành tiền";
        }
    }
}
