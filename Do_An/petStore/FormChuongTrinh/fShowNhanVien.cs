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
    public partial class fShowNhanVien : Form
    {
        // Khai báo biến toàn cục
        ConnectData datanhanvien = new ConnectData();
        public fShowNhanVien()
        {
            InitializeComponent();
        }

        private void fShowNhanVien_Load(object sender, EventArgs e)
        {
            // kết nối với csdl
            datanhanvien.OpenConnection();
            // Đổ dữ liệu lên datagridview
            LayDuLieu_NhanVien();
            // Việt hóa tiêu đề dgvNhanVien
            dgvNhanVien.Columns["MANV"].HeaderText = "Mã nhân viên";
            dgvNhanVien.Columns["TENNV"].HeaderText = "Tên nhân viên";
            dgvNhanVien.Columns["CCCD"].HeaderText = "Căn cước công dân";
            dgvNhanVien.Columns["GIOITINH"].HeaderText = "Giới tính";
            dgvNhanVien.Columns["NGAYSINH"].HeaderText = "Ngày sinh";
            dgvNhanVien.Columns["SDT"].HeaderText = "Số điện thoại";
            dgvNhanVien.Columns["ANH"].Visible = false;
            
            dgvNhanVien.Columns["NGAYSINH"].DefaultCellStyle.Format = "dd/MM/yyyy";
        }
        public void LayDuLieu_NhanVien()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM NHANVIEN");
            datanhanvien.Fill(cmd);
            BindingSource binding1 = new BindingSource();
            binding1.DataSource = datanhanvien;
            dgvNhanVien.DataSource = binding1;
        }
        public void LayDuLieu_TimKiem(string TuKhoa)
        {
            SqlCommand cmd = new SqlCommand(@"SELECT * 
                                            FROM NHANVIEN
                                            WHERE MANV LIKE N'%" + TuKhoa + "%'" +
                                           " or TENNV LIKE N'%" + TuKhoa + "%'" +
                                           " or CCCD LIKE N'%" + TuKhoa + "%'" +
                                           " or SDT LIKE N'%" + TuKhoa + "%'" +
                                           " or NGAYSINh LIKE N'%" + TuKhoa + "%'");
            datanhanvien.Fill(cmd);
            BindingSource binding = new BindingSource();
            binding.DataSource = datanhanvien;
            dgvNhanVien.DataSource = binding;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            LayDuLieu_TimKiem(txtTimKiem.Text);
        }
    }
}
