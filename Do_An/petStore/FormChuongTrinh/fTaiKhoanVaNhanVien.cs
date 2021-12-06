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
    public partial class fTaiKhoanVaNhanVien : Form
    {
        // Khai báo biến toàn cục
        ConnectData dataTable1 = new ConnectData();
        ConnectData dataTable2 = new ConnectData();
        public fTaiKhoanVaNhanVien()
        {
            InitializeComponent();
            dataTable1.OpenConnection();
            dataTable2.OpenConnection();
        }
        #region mouseHover, mouseLeave Button
        private void btnThem1_MouseHover(object sender, EventArgs e)
        {
            btnThem1.ForeColor = Color.Red;
        }
        private void btnThem1_MouseLeave(object sender, EventArgs e)
        {
            btnThem1.ForeColor = Color.Black;
        }
        #endregion
        #region Lấy dữ liệu
        // lấy dữ liệu nhân viên
        public void LayDuLieu_NhanVien()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM NHANVIEN");
            dataTable1.Fill(cmd);
            BindingSource binding1 = new BindingSource();
            binding1.DataSource = dataTable1;
            dgvNhanVien.DataSource = binding1;
        }
        public void LayDuLieu_TaiKhoan()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM ACCOUNT");
            dataTable2.Fill(cmd);
            BindingSource binding2 = new BindingSource();
            binding2.DataSource = dataTable2;
            dgvTaiKhoan.DataSource = binding2;
        }
        // Lấy dữ liệu vào các ComboBox:
        public void LayDuLieu_TaiKhoan(ComboBox comboBox, string data, string display, string value)
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
        #endregion

        private void fTaiKhoanVaNhanVien_Load(object sender, EventArgs e)
        {
            LayDuLieu_NhanVien();
            LayDuLieu_TaiKhoan();
            LayDuLieu_TaiKhoan(cboMaNV, "SELECT * FROM ACCOUNT", "MANV", "MANV");
            LayDuLieu_TaiKhoan(cboQuyen, "SELECT * FROM ACCOUNT", "QUYEN", "QUYEN");
        }
    }
}
