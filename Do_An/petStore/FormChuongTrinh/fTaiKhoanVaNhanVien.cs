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
        bool capNhat1 = false;
        bool capNhat2 = false;
        string maNV = "";
        string user = "";
        public fTaiKhoanVaNhanVien()
        {
            InitializeComponent();
        }
        // Form load
        private void fTaiKhoanVaNhanVien_Load(object sender, EventArgs e)
        {
            // Kết nối với csdl
            dataTable1.OpenConnection();
            dataTable2.OpenConnection();
            // đổ dữ liệu lên datagridview
            LayDuLieu_NhanVien();
            LayDuLieu_TaiKhoan();
            LayDuLieu_TaiKhoan(cboMaNV, "SELECT * FROM NHANVIEN", "MANV", "MANV");
            LayDuLieu_TaiKhoan(cboQuyen, "SELECT * FROM TAIKHOAN", "QUYENHAN", "QUYENHAN");

            // Khi click vào dgvNhanVien thì hiển thị dữ liệu của dòng được chọn lên các control
            txtMaNV.DataBindings.Clear();
            txtTenNV.DataBindings.Clear();
            txtCCCD.DataBindings.Clear();
            chkbNu.DataBindings.Clear();
            dtpNgaySinh.DataBindings.Clear();
            txtSDT.DataBindings.Clear();

            txtMaNV.DataBindings.Add("Text", dgvNhanVien.DataSource, "MANV", false, DataSourceUpdateMode.Never);
            txtTenNV.DataBindings.Add("Text", dgvNhanVien.DataSource, "TENNV", false, DataSourceUpdateMode.Never);
            txtCCCD.DataBindings.Add("Text", dgvNhanVien.DataSource, "CCCD", false, DataSourceUpdateMode.Never);
            chkbNu.DataBindings.Add("Checked", dgvNhanVien.DataSource, "GIOITINH", false, DataSourceUpdateMode.Never);
            dtpNgaySinh.DataBindings.Add("Value", dgvNhanVien.DataSource, "NGAYSINH", false, DataSourceUpdateMode.Never);
            txtSDT.DataBindings.Add("Text", dgvNhanVien.DataSource, "SDT", false, DataSourceUpdateMode.Never);
            // Việt hóa tiêu đề dgvNhanVien
            dgvNhanVien.Columns["MANV"].HeaderText = "Mã nhân viên";
            dgvNhanVien.Columns["TENNV"].HeaderText = "Tên nhân viên";
            dgvNhanVien.Columns["CCCD"].HeaderText = "Căn cước công dân";
            dgvNhanVien.Columns["GIOITINH"].HeaderText = "Giới tính";
            dgvNhanVien.Columns["NGAYSINH"].HeaderText = "Ngày sinh";
            dgvNhanVien.Columns["SDT"].HeaderText = "Số điện thoại";
            dgvNhanVien.Columns["ANH"].HeaderText = "Ảnh";

            // Khi click vào dgvTaiKhoan thì hiển thị dữ liệu của dòng được chọn lên các control
            cboMaNV.DataBindings.Clear();
            txtUser.DataBindings.Clear();
            txtPass.DataBindings.Clear();
            cboQuyen.DataBindings.Clear();
            txtGhiChu.DataBindings.Clear();
            cboMaNV.DataBindings.Add("SelectedValue", dgvTaiKhoan.DataSource, "MANV", false, DataSourceUpdateMode.Never);
            txtUser.DataBindings.Add("Text", dgvTaiKhoan.DataSource, "TENDANGNHAP", false, DataSourceUpdateMode.Never);
            txtPass.DataBindings.Add("Text", dgvTaiKhoan.DataSource, "MATKHAU", false, DataSourceUpdateMode.Never);
            cboQuyen.DataBindings.Add("SelectedValue", dgvTaiKhoan.DataSource, "QUYENHAN", false, DataSourceUpdateMode.Never);
            txtGhiChu.DataBindings.Add("Text", dgvTaiKhoan.DataSource, "GHICHU", false, DataSourceUpdateMode.Never);
            // Việt hóa tiêu đề dgvTaiKhoan
            dgvTaiKhoan.Columns["MANV"].HeaderText = "Mã nhân viên";
            dgvTaiKhoan.Columns["TENDANGNHAP"].HeaderText = "Tên đăng nhập";
            dgvTaiKhoan.Columns["MATKHAU"].HeaderText = "Mật khẩu";
            dgvTaiKhoan.Columns["QUYENHAN"].HeaderText = "Quyền hạn";
            dgvTaiKhoan.Columns["GHICHU"].HeaderText = "Ghi chú";

            // Làm sáng nút Thêm mới, Sửa và Xóa
            btnThem1.Enabled = true;
            btnSua1.Enabled = true;
            btnXoa1.Enabled = true;
            btnThem2.Enabled = true;
            btnSua2.Enabled = true;
            btnXoa2.Enabled = true;
            //Làm mờ nút lưu và bỏ qua
            btnLuu1.Enabled = false;
            btnHuyBo1.Enabled = false;
            btnLuu2.Enabled = false;
            btnHuyBo2.Enabled = false;

            // làm mờ các trường nhập dữ liệu
            //NHANVIEN
            txtMaNV.Enabled = false;
            txtTenNV.Enabled = false;
            txtCCCD.Enabled = false;
            chkbNu.Enabled = false;
            dtpNgaySinh.Enabled = false;
            txtSDT.Enabled = false;
            //TAIKHOAN
            cboMaNV.Enabled = false;
            txtUser.Enabled = false;
            txtPass.Enabled = false;
            cboQuyen.Enabled = false;
            txtGhiChu.Enabled = false;
        }
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
            SqlCommand cmd = new SqlCommand("SELECT * FROM TAIKHOAN");
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
        // Định dạng lại mật khẩu trong dgvTaiKhoan để tăng tính bảo mật
        private void dgvTaiKhoan_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvTaiKhoan.Columns[e.ColumnIndex].Name == "MATKHAU")
            {
                e.Value = "••••••••••";
            }
        }
        /*Hàm kiểm tra dữ liệu trên DataGridView:
        public bool KiemTra(string columnName)
        {
            foreach (DataGridViewRow row in dgvTaiKhoan.Rows)
            {
                string value = row.Cells[columnName].Value.ToString();
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Giá trị của ô không được rỗng!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }*/
        #region button của dgvNhanVien
        // Sự kiện Click của nút Thêm:
        private void btnThem1_Click(object sender, EventArgs e)
        {
            capNhat1 = false;
            // làm trống các trường nhập dữ liệu
            txtMaNV.Clear();
            txtTenNV.Clear();
            txtCCCD.Clear();
            chkbNu.Checked = false;
            dtpNgaySinh.Value = DateTime.Today;
            txtSDT.Text = "";
            txtMaNV.Focus();
            // làm mờ nút Thêm, Sửa, Xóa
            btnThem1.Enabled = false;
            btnSua1.Enabled = false;
            btnXoa1.Enabled = false;
            // làm sáng các trường nhập dữ liệu
            txtMaNV.Enabled = true;
            txtTenNV.Enabled = true;
            txtCCCD.Enabled = true;
            chkbNu.Enabled = true;
            dtpNgaySinh.Enabled = true;
            txtSDT.Enabled = true;
            //làm sáng nút Lưu và Bỏ qua
            btnLuu1.Enabled = true;
            btnHuyBo1.Enabled = true;
        }
        // Sự kiện Click của nút Xóa:
        private void btnSua1_Click(object sender, EventArgs e)
        {
            // Đánh dấu là Cập nhật
            capNhat1 = true;
            maNV = txtMaNV.Text;

            // Làm mờ nút Thêm mới, Sửa và Xóa
            btnThem1.Enabled = false;
            btnSua1.Enabled = false;
            btnXoa1.Enabled = false;

            // Làm sáng nút Lưu và Bỏ qua
            btnLuu1.Enabled = true;
            btnHuyBo1.Enabled = true;

            // làm sáng các trường nhập dữ liệu
            txtMaNV.Enabled = true;
            txtTenNV.Enabled = true;
            txtCCCD.Enabled = true;
            chkbNu.Enabled = true;
            dtpNgaySinh.Enabled = true;
            txtSDT.Enabled = true;
        }
        // Sự kiện Click của nút Xóa:
        private void btnXoa1_Click(object sender, EventArgs e)
        {
            DialogResult kq;
            kq = MessageBox.Show("Bạn có muốn xóa Mã nhân viên " + txtMaNV.Text + " không?", "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
            {
                string sql = @"DELETE FROM NHANVIEN WHERE MANV = @manv";
                SqlCommand cmd = new SqlCommand(sql);
                cmd.Parameters.Add("@manv", SqlDbType.NVarChar, 5).Value = txtMaNV.Text;
                dataTable1.Update(cmd);

                // Tải lại form
                fTaiKhoanVaNhanVien_Load(sender, e);
            }
        }
        // Sự kiện Click của nút Lưu dữ liệu:
        private void btnLuu1_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu (Các trường không được rỗng, mã sách không trùng)
            if (txtMaNV.Text.Trim() == "")
                MessageBox.Show("Mã nhân viên không được bỏ trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (txtTenNV.Text.Trim() == "")
                MessageBox.Show("Tên nhân viên không được bỏ trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                bool error = false;
                DataGridViewRow currentRow = dgvNhanVien.CurrentRow;
                foreach (DataGridViewRow row in dgvNhanVien.Rows)
                {
                    if (capNhat1 && row == currentRow) continue;
                    if (row.Cells["MANV"].Value.ToString() == txtMaNV.Text)
                        error = true;
                }

                if (error)
                    MessageBox.Show("Mã nhân viên " + txtMaNV.Text + " đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    // Lưu các thông tin trên các control vào CSDL
                    try
                    {
                        if (capNhat1)
                        {
                            string sql = @"UPDATE   NHANVIEN
                                           SET      MANV = @manv,
                                                    TENNV = @tennv,
                                                    CCCD = @cccd,
                                                    GIOITINH = @gioitinh,
                                                    NGAYSINH = @ngaysinh,
                                                    SDT = @sdt
                                           WHERE    MANV = @manvcu";
                            SqlCommand cmd = new SqlCommand(sql);
                            cmd.Parameters.Add("@manv", SqlDbType.VarChar).Value = txtMaNV.Text;
                            cmd.Parameters.Add("@tennv", SqlDbType.NVarChar).Value = txtTenNV.Text;
                            cmd.Parameters.Add("@cccd", SqlDbType.VarChar).Value = txtCCCD.Text;
                            cmd.Parameters.Add("@gioitinh", SqlDbType.Bit).Value = chkbNu.Checked ? 1 : 0;
                            cmd.Parameters.Add("@ngaysinh", SqlDbType.Date).Value = dtpNgaySinh.Value;
                            cmd.Parameters.Add("@sdt", SqlDbType.NVarChar, 10).Value = txtSDT.Text;
                            cmd.Parameters.Add("@manvcu", SqlDbType.VarChar).Value = maNV;
                            dataTable1.Update(cmd);
                        }
                        else
                        {
                            string sql = @"INSERT INTO NHANVIEN (MANV, TENNV, CCCD, GIOITINH, NGAYSINH, SDT)
                                           VALUES(@manv, @tennv, @cccd, @gioitinh, @ngaysinh, @sdt)";
                            SqlCommand cmd = new SqlCommand(sql);
                            cmd.Parameters.Add("@manv", SqlDbType.VarChar).Value = txtMaNV.Text;
                            cmd.Parameters.Add("@tennv", SqlDbType.NVarChar).Value = txtTenNV.Text;
                            cmd.Parameters.Add("@cccd", SqlDbType.VarChar).Value = txtCCCD.Text;
                            cmd.Parameters.Add("@gioitinh", SqlDbType.Bit).Value = chkbNu.Checked ? 1 : 0;
                            cmd.Parameters.Add("@ngaysinh", SqlDbType.Date).Value = dtpNgaySinh.Value;
                            cmd.Parameters.Add("@sdt", SqlDbType.NVarChar, 10).Value = txtSDT.Text;
                            dataTable1.Update(cmd);
                        }

                        // Tải lại form
                        fTaiKhoanVaNhanVien_Load(sender, e);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        private void btnHuyBo1_Click(object sender, EventArgs e)
        {
            fTaiKhoanVaNhanVien_Load(sender, e);
        }
        #endregion
        #region button của dgvTaiKhoan
        private void btnThem2_Click(object sender, EventArgs e)
        {
            capNhat2 = false;
            // làm trống các trường nhập dữ liệu
            cboMaNV.Text = "";
            txtUser.Clear();
            txtPass.Clear();
            cboQuyen.Text = "";
            txtGhiChu.Clear();
            cboMaNV.Focus();
            // làm mờ nút Thêm, Sửa, Xóa
            btnThem2.Enabled = false;
            btnSua2.Enabled = false;
            btnXoa2.Enabled = false;
            // làm sáng các trường nhập dữ liệu
            cboMaNV.Enabled = true;
            txtUser.Enabled = true;
            txtPass.Enabled = true;
            cboQuyen.Enabled = true;
            txtGhiChu.Enabled = true;
            //làm sáng nút Lưu và Bỏ qua
            btnLuu2.Enabled = true;
            btnHuyBo2.Enabled = true;
        }

        private void btnSua2_Click(object sender, EventArgs e)
        {
            // Đánh dấu là Cập nhật
            capNhat2 = true;
            user = txtUser.Text;

            // Làm mờ nút Thêm mới, Sửa và Xóa
            btnThem2.Enabled = false;
            btnSua2.Enabled = false;
            btnXoa2.Enabled = false;

            // Làm sáng nút Lưu và Bỏ qua
            btnLuu2.Enabled = true;
            btnHuyBo2.Enabled = true;

            // làm sáng các trường nhập dữ liệu
            cboMaNV.Enabled = true;
            txtUser.Enabled = true;
            txtPass.Enabled = true;
            cboQuyen.Enabled = true;
            txtGhiChu.Enabled = true;
        }

        private void btnXoa2_Click(object sender, EventArgs e)
        {
            DialogResult kq;
            kq = MessageBox.Show("Bạn có muốn xóa Tài khoản " + txtUser.Text + " không?", "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
            {
                string sql = @"DELETE FROM TAIKHOAN WHERE TENDANGNHAP = @tendangnhap";
                SqlCommand cmd = new SqlCommand(sql);
                cmd.Parameters.Add("@tendangnhap", SqlDbType.NVarChar).Value = txtUser.Text;
                dataTable2.Update(cmd);

                // Tải lại form
                fTaiKhoanVaNhanVien_Load(sender, e);
            }
        }

        private void btnLuu2_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu (Các trường không được rỗng, mã sách không trùng)
            if (txtUser.Text.Trim() == "")
                MessageBox.Show("Tên đăng nhập không được bỏ trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (txtPass.Text.Trim() == "")
                MessageBox.Show("Mật khẩu không được bỏ trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                bool error = false;
                DataGridViewRow currentRow = dgvTaiKhoan.CurrentRow;
                foreach (DataGridViewRow row in dgvTaiKhoan.Rows)
                {
                    if (capNhat2 && row == currentRow) continue;
                    if (row.Cells["TENDANGNHAP"].Value.ToString() == txtMaNV.Text)
                        error = true;
                }

                if (error)
                    MessageBox.Show("Tên đăng nhập " + txtUser.Text + " đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    // Lưu các thông tin trên các control vào CSDL
                    try
                    {
                        if (capNhat2)
                        {
                            string sql = @"UPDATE   TAIKHOAN
                                           SET      MANV = @manv,
                                                    TENDANGNHAP = @tendangnhap,
                                                    MATKHAU = @matkhau,
                                                    QUYENHAN = @quyenhan,
                                                    GHICHU = @ghichu
                                           WHERE    TENDANGNHAP = @tendangnhapcu";
                            SqlCommand cmd = new SqlCommand(sql);
                            cmd.Parameters.Add("@manv", SqlDbType.VarChar).Value = cboMaNV.SelectedValue.ToString();
                            cmd.Parameters.Add("@tendangnhap", SqlDbType.VarChar).Value = txtUser.Text;
                            cmd.Parameters.Add("@matkhau", SqlDbType.VarChar).Value = txtPass.Text;
                            cmd.Parameters.Add("@quyenhan", SqlDbType.VarChar).Value = cboQuyen.SelectedValue.ToString();
                            cmd.Parameters.Add("@ghichu", SqlDbType.NVarChar).Value = txtGhiChu.Text;
                            cmd.Parameters.Add("@tendangnhapcu", SqlDbType.VarChar).Value = user;
                            dataTable2.Update(cmd);
                        }
                        else
                        {
                            string sql = @"INSERT INTO TAIKHOAN (MANV, TENDANGNHAP, MATKHAU, QUYENHAN, GHICHU)
                                           VALUES(@manv, @tendangnhap, @matkhau, @quyenhan, @ghichu)";
                            SqlCommand cmd = new SqlCommand(sql);
                            cmd.Parameters.Add("@manv", SqlDbType.VarChar).Value = cboMaNV.SelectedValue.ToString();
                            cmd.Parameters.Add("@tendangnhap", SqlDbType.VarChar).Value = txtUser.Text;
                            cmd.Parameters.Add("@matkhau", SqlDbType.VarChar).Value = txtPass.Text;
                            cmd.Parameters.Add("@quyenhan", SqlDbType.VarChar).Value = cboQuyen.SelectedValue.ToString();
                            cmd.Parameters.Add("@ghichu", SqlDbType.NVarChar).Value = txtGhiChu.Text;
                            dataTable2.Update(cmd);
                        }

                        // Tải lại form
                        fTaiKhoanVaNhanVien_Load(sender, e);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        private void btnHuyBo2_Click(object sender, EventArgs e)
        {
            fTaiKhoanVaNhanVien_Load(sender, e);
        }
        #endregion
    }
}
