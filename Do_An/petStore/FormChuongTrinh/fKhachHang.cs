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
    public partial class fKhachHang : Form
    {
        ConnectData datakhachhang = new ConnectData();
        bool capNhat = false;
        string makh = "";
        public fKhachHang()
        {
            InitializeComponent();
        }
        // Form Load
        private void fKhachHang_Load(object sender, EventArgs e)
        {
            // kết nối đến csdl
            datakhachhang.OpenConnection();
            // Đổ dữ liệu lên datagridview
            LayDuLieu_KhachHang();
            // Khi click vào dgvNhanVien thì hiển thị dữ liệu của dòng được chọn lên các control
            txtMaKhachHang.DataBindings.Clear();
            txtTenKhachHang.DataBindings.Clear();
            chkbGioiTinh.DataBindings.Clear();
            txtDiaChi.DataBindings.Clear();
            txtSDT.DataBindings.Clear();

            txtMaKhachHang.DataBindings.Add("Text", dgvKhachHang.DataSource, "MAKH", false, DataSourceUpdateMode.Never);
            txtTenKhachHang.DataBindings.Add("Text", dgvKhachHang.DataSource, "TENKH", false, DataSourceUpdateMode.Never);
            chkbGioiTinh.DataBindings.Add("Checked", dgvKhachHang.DataSource, "GIOITINH", false, DataSourceUpdateMode.Never);
            txtDiaChi.DataBindings.Add("Text", dgvKhachHang.DataSource, "DIACHI", false, DataSourceUpdateMode.Never);
            txtSDT.DataBindings.Add("Text", dgvKhachHang.DataSource, "SDT", false, DataSourceUpdateMode.Never);
            // Việt hóa tiêu đề dgvKhachHang
            dgvKhachHang.Columns["MAKH"].HeaderText = "Mã khách hàng";
            dgvKhachHang.Columns["TENKH"].HeaderText = "Tên khách hàng";
            dgvKhachHang.Columns["GIOITINH"].HeaderText = "Giới tính";
            dgvKhachHang.Columns["DIACHI"].HeaderText = "Địa chỉ";
            dgvKhachHang.Columns["SDT"].HeaderText = "Số điện thoại";
            // Làm sáng nút Thêm mới, Sửa và Xóa
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            //Làm mờ nút lưu và bỏ qua
            btnLuu.Enabled = false;
            btnHuyBo.Enabled = false;
            // làm mờ các trường nhập dữ liệu
            txtMaKhachHang.Enabled = false;
            txtTenKhachHang.Enabled = false;
            chkbGioiTinh.Enabled = false;
            txtDiaChi.Enabled = false;
            txtSDT.Enabled = false;
        }

        #region Lấy dữ liệu
        public void LayDuLieu_KhachHang()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM KHACHHANG");
            datakhachhang.Fill(cmd);
            BindingSource binding1 = new BindingSource();
            binding1.DataSource = datakhachhang;
            dgvKhachHang.DataSource = binding1;
        }
        // Lấy dữ liệu theo từ khóa
        public void LayDuLieu_TimKiem(string TuKhoa)
        {
            SqlCommand cmd = new SqlCommand(@"SELECT * 
                                            FROM KHACHHANG
                                            WHERE MAKH LIKE N'%" + TuKhoa + "%'" +
                                            " or TENKH LIKE N'%" + TuKhoa + "%'" +
                                            " or SDT LIKE N'%" + TuKhoa + "%'" +
                                            " or DIACHI LIKE N'%" + TuKhoa + "%'");
            datakhachhang.Fill(cmd);
            BindingSource binding = new BindingSource();
            binding.DataSource = datakhachhang;
            dgvKhachHang.DataSource = binding;
        }
        #endregion
        private void pbTimKiem_Click(object sender, EventArgs e)
        {
            LayDuLieu_TimKiem(txtTimKiem.Text);
        }
        #region sự kiện của các nút
        // nút thêm mới
        private void btnThem_Click(object sender, EventArgs e)
        {
            capNhat = false;
            // làm trống các trường nhập dữ liệu
            txtMaKhachHang.Clear();
            txtTenKhachHang.Clear();
            chkbGioiTinh.Checked = false;
            txtDiaChi.Clear();
            txtSDT.Clear();
            txtMaKhachHang.Focus();
            // làm mờ nút Thêm, Sửa, Xóa
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            //làm sáng nút Lưu và Bỏ qua
            btnLuu.Enabled = true;
            btnHuyBo.Enabled = true;
            // làm sáng các trường nhập dữ liệu
            txtMaKhachHang.Enabled = true;
            txtTenKhachHang.Enabled = true;
            chkbGioiTinh.Enabled = true;
            txtDiaChi.Enabled = true;
            txtSDT.Enabled = true;
        }
        // nút sửa
        private void btnSua_Click(object sender, EventArgs e)
        {
            // Đánh dấu là Cập nhật
            capNhat = true;
            makh = txtMaKhachHang.Text;

            // Làm mờ nút Thêm mới, Sửa và Xóa
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            // Làm sáng nút Lưu và Bỏ qua
            btnLuu.Enabled = true;
            btnHuyBo.Enabled = true;

            // làm sáng các trường nhập dữ liệu
            txtMaKhachHang.Enabled = true;
            txtTenKhachHang.Enabled = true;
            chkbGioiTinh.Enabled = true;
            txtDiaChi.Enabled = true;
            txtSDT.Enabled = true;
        }
        // nút xóa
        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult kq;
            kq = MessageBox.Show("Bạn có muốn xóa khách hàng có mã là" + txtMaKhachHang.Text + " không?", "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
            {
                string sql = @"DELETE FROM KHACHHANG WHERE MAKH = @kh";
                SqlCommand cmd = new SqlCommand(sql);
                cmd.Parameters.Add("@kh", SqlDbType.NVarChar).Value = txtMaKhachHang.Text;
                datakhachhang.Update(cmd);

                // Tải lại form
                fKhachHang_Load(sender, e);
            }
        }
        // nút hủy bỏ
        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            fKhachHang_Load(sender, e);
        }
        // nút lưu vào csdl
        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu (Các trường không được rỗng, mã sách không trùng)
            if (txtMaKhachHang.Text.Trim() == "")
                MessageBox.Show("Mã khách hàng không được bỏ trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (txtTenKhachHang.Text.Trim() == "")
                MessageBox.Show("Tên khách hàng được bỏ trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                bool error = false;
                DataGridViewRow currentRow = dgvKhachHang.CurrentRow;
                foreach (DataGridViewRow row in dgvKhachHang.Rows)
                {
                    if (capNhat && row == currentRow) continue;
                    if (row.Cells["MAKH"].Value.ToString() == txtMaKhachHang.Text)
                        error = true;
                }

                if (error)
                    MessageBox.Show("Mã khách hàng " + txtMaKhachHang.Text + " đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    // Lưu các thông tin trên các control vào CSDL
                    try
                    {
                        if (capNhat)
                        {
                            string sql = @"UPDATE   KHACHHANG
                                           SET      MAKH = @maKH,
                                                    TENKH = @tenKH,
                                                    GIOITINH = @gt,
                                                    DIACHI = @dc,
                                                    SDT = @sdt
                                           WHERE    MAKH = @maKHcu";
                            SqlCommand cmd = new SqlCommand(sql);
                            cmd.Parameters.Add("@maKH", SqlDbType.VarChar).Value = txtMaKhachHang.Text;
                            cmd.Parameters.Add("@tenKH", SqlDbType.NVarChar).Value = txtTenKhachHang.Text;
                            cmd.Parameters.Add("@gt", SqlDbType.Bit).Value = chkbGioiTinh.Checked ? 1 : 0; ;
                            cmd.Parameters.Add("@dc", SqlDbType.NVarChar).Value = txtDiaChi.Text;
                            cmd.Parameters.Add("@sdt", SqlDbType.NVarChar).Value = txtSDT.Text;
                            cmd.Parameters.Add("@maKHcu", SqlDbType.VarChar).Value = makh;
                            datakhachhang.Update(cmd);
                        }
                        else
                        {
                            string sql = @"INSERT INTO KHACHHANG (MAKH, TENKH, GIOITINH, DIACHI, SDT)
                                           VALUES(@maKH, @tenKH, @gt, @dc, @sdt)";
                            SqlCommand cmd = new SqlCommand(sql);
                            cmd.Parameters.Add("@maKH", SqlDbType.VarChar).Value = txtMaKhachHang.Text;
                            cmd.Parameters.Add("@tenKH", SqlDbType.NVarChar).Value = txtTenKhachHang.Text;
                            cmd.Parameters.Add("@gt", SqlDbType.Bit).Value = chkbGioiTinh.Checked ? 1 : 0; ;
                            cmd.Parameters.Add("@dc", SqlDbType.NVarChar).Value = txtDiaChi.Text;
                            cmd.Parameters.Add("@sdt", SqlDbType.NVarChar).Value = txtSDT.Text;
                            datakhachhang.Update(cmd);
                        }

                        // Tải lại form
                        fKhachHang_Load(sender, e);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            #endregion
        }
    }
}
