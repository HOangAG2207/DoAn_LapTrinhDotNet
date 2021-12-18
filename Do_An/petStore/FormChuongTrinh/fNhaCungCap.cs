﻿using System;
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
    public partial class fNhaCungCap : Form
    {
        ConnectData datanhacungcap = new ConnectData();
        bool capNhat = false;
        string mancc = "";
        public fNhaCungCap()
        {
            InitializeComponent();
        }
        // Form Load
        private void fNhaCungCap_Load(object sender, EventArgs e)
        {
            // kết nối đến csdl
            datanhacungcap.OpenConnection();
            // Đổ dữ liệu lên datagridview
            LayDuLieu_KhachHang();
            // Khi click vào dgvNhanVien thì hiển thị dữ liệu của dòng được chọn lên các control
            txtMaNhaCungCap.DataBindings.Clear();
            txtTenNhaCungCap.DataBindings.Clear();
            txtDiaChi.DataBindings.Clear();
            txtSDT.DataBindings.Clear();

            txtMaNhaCungCap.DataBindings.Add("Text", dgvNhaCungCap.DataSource, "MANCC", false, DataSourceUpdateMode.Never);
            txtTenNhaCungCap.DataBindings.Add("Text", dgvNhaCungCap.DataSource, "TENNCC", false, DataSourceUpdateMode.Never);
            txtDiaChi.DataBindings.Add("Text", dgvNhaCungCap.DataSource, "DIACHI", false, DataSourceUpdateMode.Never);
            txtSDT.DataBindings.Add("Text", dgvNhaCungCap.DataSource, "SDT", false, DataSourceUpdateMode.Never);
            // Việt hóa tiêu đề dgvKhachHang
            dgvNhaCungCap.Columns["MANCC"].HeaderText = "Mã nhà cung cấp";
            dgvNhaCungCap.Columns["TENNCC"].HeaderText = "Tên nhà cung cấp";
            dgvNhaCungCap.Columns["DIACHI"].HeaderText = "Địa chỉ";
            dgvNhaCungCap.Columns["SDT"].HeaderText = "Số điện thoại";
            // Làm sáng nút Thêm mới, Sửa và Xóa
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            //Làm mờ nút lưu và bỏ qua
            btnLuu.Enabled = false;
            btnHuyBo.Enabled = false;
            // làm mờ các trường nhập dữ liệu
            txtMaNhaCungCap.Enabled = false;
            txtTenNhaCungCap.Enabled = false;
            txtDiaChi.Enabled = false;
            txtSDT.Enabled = false;
        }
        #region Lấy dữ liệu
        public void LayDuLieu_KhachHang()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM NHACUNGCAP");
            datanhacungcap.Fill(cmd);
            BindingSource binding = new BindingSource();
            binding.DataSource = datanhacungcap;
            dgvNhaCungCap.DataSource = binding;
        }
        // Lấy dữ liệu theo từ khóa
        public void LayDuLieu_TimKiem(string TuKhoa)
        {
            SqlCommand cmd = new SqlCommand(@"SELECT * 
                                            FROM NHACUNGCAP
                                            WHERE MANCC LIKE N'%" + TuKhoa + "%'" +
                                            " or TENNCC LIKE N'%" + TuKhoa + "%'" +
                                            " or SDT LIKE N'%" + TuKhoa + "%'" +
                                            " or DIACHI LIKE N'%" + TuKhoa + "%'");
            datanhacungcap.Fill(cmd);
            BindingSource binding = new BindingSource();
            binding.DataSource = datanhacungcap;
            dgvNhaCungCap.DataSource = binding;
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
            txtMaNhaCungCap.Clear();
            txtTenNhaCungCap.Clear();
            txtDiaChi.Clear();
            txtSDT.Clear();
            txtMaNhaCungCap.Focus();
            // làm mờ nút Thêm, Sửa, Xóa
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            //làm sáng nút Lưu và Bỏ qua
            btnLuu.Enabled = true;
            btnHuyBo.Enabled = true;
            // làm sáng các trường nhập dữ liệu
            txtMaNhaCungCap.Enabled = true;
            txtTenNhaCungCap.Enabled = true;
            txtDiaChi.Enabled = true;
            txtSDT.Enabled = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            // Đánh dấu là Cập nhật
            capNhat = true;
            mancc = txtMaNhaCungCap.Text;

            // Làm mờ nút Thêm mới, Sửa và Xóa
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            // Làm sáng nút Lưu và Bỏ qua
            btnLuu.Enabled = true;
            btnHuyBo.Enabled = true;

            // làm sáng các trường nhập dữ liệu
            txtMaNhaCungCap.Enabled = true;
            txtTenNhaCungCap.Enabled = true;
            txtDiaChi.Enabled = true;
            txtSDT.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult kq;
            kq = MessageBox.Show("Bạn có muốn xóa Nhà cung cấp có mã là" + txtMaNhaCungCap.Text + " không?", "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
            {
                string sql = @"DELETE FROM NHACUNGCAP WHERE MANCC = @ncc";
                SqlCommand cmd = new SqlCommand(sql);
                cmd.Parameters.Add("@ncc", SqlDbType.NVarChar).Value = txtMaNhaCungCap.Text;
                datanhacungcap.Update(cmd);

                // Tải lại form
                fNhaCungCap_Load(sender, e);
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            fNhaCungCap_Load(sender, e);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu (Các trường không được rỗng, mã sách không trùng)
            if (txtMaNhaCungCap.Text.Trim() == "")
                MessageBox.Show("Mã nhà cung cấp không được bỏ trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (txtTenNhaCungCap.Text.Trim() == "")
                MessageBox.Show("Tên nhà cung cấp được bỏ trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                bool error = false;
                DataGridViewRow currentRow = dgvNhaCungCap.CurrentRow;
                foreach (DataGridViewRow row in dgvNhaCungCap.Rows)
                {
                    if (capNhat && row == currentRow) continue;
                    if (row.Cells["MANCC"].Value.ToString() == txtMaNhaCungCap.Text)
                        error = true;
                }

                if (error)
                    MessageBox.Show("Mã cung cấp " + txtMaNhaCungCap.Text + " đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    // Lưu các thông tin trên các control vào CSDL
                    try
                    {
                        if (capNhat)
                        {
                            string sql = @"UPDATE   NHACUNGCAP
                                           SET      MANCC = @maKH,
                                                    TENNCC = @tenKH,
                                                    DIACHI = @dc,
                                                    SDT = @sdt
                                           WHERE    MANCC = @maNCCcu";
                            SqlCommand cmd = new SqlCommand(sql);
                            cmd.Parameters.Add("@maNCC", SqlDbType.VarChar).Value = txtMaNhaCungCap.Text;
                            cmd.Parameters.Add("@tenNCC", SqlDbType.NVarChar).Value = txtTenNhaCungCap.Text;
                            cmd.Parameters.Add("@dc", SqlDbType.VarChar).Value = txtDiaChi.Text;
                            cmd.Parameters.Add("@sdt", SqlDbType.NText).Value = txtSDT.Text;
                            cmd.Parameters.Add("@maNCCcu", SqlDbType.VarChar).Value = mancc;
                            datanhacungcap.Update(cmd);
                        }
                        else
                        {
                            string sql = @"INSERT INTO NHACUNGCAP (MANCC, TENNCC, DIACHI, SDT)
                                           VALUES(@maNCC, @tenNCC, @dc, @sdt)";
                            SqlCommand cmd = new SqlCommand(sql);
                            cmd.Parameters.Add("@maNCC", SqlDbType.VarChar).Value = txtMaNhaCungCap.Text;
                            cmd.Parameters.Add("@tenNCC", SqlDbType.NVarChar).Value = txtTenNhaCungCap.Text;
                            cmd.Parameters.Add("@dc", SqlDbType.VarChar).Value = txtDiaChi.Text;
                            cmd.Parameters.Add("@sdt", SqlDbType.NText).Value = txtSDT.Text;
                            datanhacungcap.Update(cmd);
                        }

                        // Tải lại form
                        fNhaCungCap_Load(sender, e);
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