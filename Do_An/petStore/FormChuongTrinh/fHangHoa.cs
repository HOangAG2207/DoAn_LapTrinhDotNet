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
    public partial class fHangHoa : Form
    {
        ConnectData dataHangHoa = new ConnectData();
        bool capNhat = false;
        string mahh = "";
        public fHangHoa()
        {
            InitializeComponent();
        }

        private void fHangHoa_Load(object sender, EventArgs e)
        {
            // Kết nối với csdl
            dataHangHoa.OpenConnection();
            // đổ dữ liệu lên datagridview
            LayDuLieu_HangHoa();
            LayDuLieu_HangHoa(cboLoai, "SELECT * FROM LOAIHH", "TENLOAI", "MALOAI");
            LayDuLieu_HangHoa(cboNhaCungCap, "SELECT * FROM NHACUNGCAP", "TENNCC", "MANCC");

            // Khi click vào dgvNhanVien thì hiển thị dữ liệu của dòng được chọn lên các control
            txtMaHH.DataBindings.Clear();
            txtTenHH.DataBindings.Clear();
            cboLoai.DataBindings.Clear();
            cboLoai.Text = "";
            txtSoLuong.DataBindings.Clear();
            txtDonGiaBan.DataBindings.Clear();
            txtDonGiaNhap.DataBindings.Clear();
            cboNhaCungCap.DataBindings.Clear();
            cboNhaCungCap.Text = "";
            txtMoTa.DataBindings.Clear();

            txtMaHH.DataBindings.Add("Text", dgvHangHoa.DataSource, "MAHH", false, DataSourceUpdateMode.Never);
            txtTenHH.DataBindings.Add("Text", dgvHangHoa.DataSource, "TENHH", false, DataSourceUpdateMode.Never);
            cboLoai.DataBindings.Add("SelectedValue", dgvHangHoa.DataSource, "MALOAI", false, DataSourceUpdateMode.Never);
            txtSoLuong.DataBindings.Add("Text", dgvHangHoa.DataSource, "SOLUONG", false, DataSourceUpdateMode.Never);
            txtDonGiaBan.DataBindings.Add("Text", dgvHangHoa.DataSource, "DONGIABAN", false, DataSourceUpdateMode.Never);
            txtDonGiaNhap.DataBindings.Add("Text", dgvHangHoa.DataSource, "DONGIANHAP", false, DataSourceUpdateMode.Never);
            cboNhaCungCap.DataBindings.Add("SelectedValue", dgvHangHoa.DataSource, "MANCC", false, DataSourceUpdateMode.Never);
            txtMoTa.DataBindings.Add("Text", dgvHangHoa.DataSource, "MOTA", false, DataSourceUpdateMode.Never);
            // Việt hóa tiêu đề dgvHangHoa
            dgvHangHoa.Columns["MAHH"].HeaderText = "Mã hàng hóa";
            dgvHangHoa.Columns["TENHH"].HeaderText = "Tên hàng hóa";
            dgvHangHoa.Columns["MALOAI"].HeaderText = "Loại hàng hóa";
            dgvHangHoa.Columns["SOLUONG"].HeaderText = "Số lượng";
            dgvHangHoa.Columns["DONGIABAN"].HeaderText = "Đơn giá nhập";
            dgvHangHoa.Columns["DONGIANHAP"].HeaderText = "Đơn giá bán";
            dgvHangHoa.Columns["MANCC"].HeaderText = "Nhà cung cấp";
            dgvHangHoa.Columns["MOTA"].HeaderText = "Mô tả";
            dgvHangHoa.Columns["ANH"].HeaderText = "Ảnh";

            // Làm sáng nút Thêm mới, Sửa và Xóa
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            //Làm mờ nút lưu và bỏ qua
            btnLuu.Enabled = false;
            btnHuyBo.Enabled = false;
            // làm mờ các trường nhập dữ liệu
            //NHANVIEN
            txtMaHH.Enabled = false;
            txtTenHH.Enabled = false;
            cboLoai.Enabled = false;
            txtSoLuong.Enabled = false;
            txtDonGiaBan.Enabled = false;
            txtDonGiaNhap.Enabled = false;
            cboNhaCungCap.Enabled = false;
            txtMoTa.Enabled = false;
        }
        #region button của dgvNhanVien
        // Sự kiện Click của nút Thêm:
        private void btnThem_Click(object sender, EventArgs e)
        {
            capNhat = false;
            // làm trống các trường nhập dữ liệu
            txtMaHH.Clear();
            txtTenHH.Clear();
            cboLoai.Text = "";
            txtSoLuong.Clear();
            txtDonGiaBan.Clear();
            txtDonGiaNhap.Clear();
            cboNhaCungCap.Text = "";
            txtMoTa.Clear();
            txtMaHH.Focus();
            // làm mờ nút Thêm, Sửa, Xóa
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            // làm sáng các trường nhập dữ liệu
            txtMaHH.Enabled = true;
            txtTenHH.Enabled = true;
            cboLoai.Enabled = true;
            txtSoLuong.Enabled = true;
            txtDonGiaBan.Enabled = true;
            txtDonGiaNhap.Enabled = true;
            cboNhaCungCap.Enabled = true;
            txtMoTa.Enabled = true;
            //làm sáng nút Lưu và Bỏ qua
            btnLuu.Enabled = true;
            btnHuyBo.Enabled = true;
        }
        // Sự kiện nút sửa
        private void btnSua_Click(object sender, EventArgs e)
        {
            // Đánh dấu là Cập nhật
            capNhat = true;
            mahh = txtMaHH.Text;

            // Làm mờ nút Thêm mới, Sửa và Xóa
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            // Làm sáng nút Lưu và Bỏ qua
            btnLuu.Enabled = true;
            btnHuyBo.Enabled = true;

            // làm sáng các trường nhập dữ liệu
            txtMaHH.Enabled = true;
            txtTenHH.Enabled = true;
            cboLoai.Enabled = true;
            txtSoLuong.Enabled = true;
            txtDonGiaBan.Enabled = true;
            txtDonGiaNhap.Enabled = true;
            cboNhaCungCap.Enabled = true;
            txtMoTa.Enabled = true;
        }
        // Sự kiện nút Xóa
        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult kq;
            kq = MessageBox.Show("Bạn có muốn xóa Hàng hóa " + txtTenHH.Text + " không?", "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
            {
                string sql = @"DELETE FROM HANGHOA WHERE MAHH = @mhh";
                SqlCommand cmd = new SqlCommand(sql);
                cmd.Parameters.Add("@mhh", SqlDbType.NVarChar).Value = txtMaHH.Text;
                dataHangHoa.Update(cmd);

                // Tải lại form
                fHangHoa_Load(sender, e);
            }
        }
        // Sự kiện nút Hủy bỏ
        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            fHangHoa_Load(sender, e);
        }
        // Sự kiện nút Lưu
        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu (Các trường không được rỗng, mã sách không trùng)
            if (txtMaHH.Text.Trim() == "")
                MessageBox.Show("Mã hàng hóa không được bỏ trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (txtTenHH.Text.Trim() == "")
                MessageBox.Show("Tên hàng hóa không được bỏ trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                bool error = false;
                DataGridViewRow currentRow = dgvHangHoa.CurrentRow;
                foreach (DataGridViewRow row in dgvHangHoa.Rows)
                {
                    if (capNhat && row == currentRow) continue;
                    if (row.Cells["MAHH"].Value.ToString() == txtMaHH.Text)
                        error = true;
                }

                if (error)
                    MessageBox.Show("Mã hàng hóa " + txtMaHH.Text + " đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    // Lưu các thông tin trên các control vào CSDL
                    try
                    {
                        if (capNhat)
                        {
                            string sql = @"UPDATE   HANGHOA
                                           SET      MAHH = @mahh,
                                                    TENHH = @tenhh,
                                                    MALOAI = @maloai,
                                                    SOLUONG = @sl,
                                                    DONGIABAN = @dgb,
                                                    DONGIANHAP = @dgn,
                                                    MANCC = @ncc,
                                                    MOTA = @mt
                                           WHERE    MAHH = @mahhcu";
                            SqlCommand cmd = new SqlCommand(sql);
                            cmd.Parameters.Add("@mahh", SqlDbType.VarChar).Value = txtMaHH.Text;
                            cmd.Parameters.Add("@tenhh", SqlDbType.NVarChar).Value = txtTenHH.Text;
                            cmd.Parameters.Add("@maloai", SqlDbType.VarChar).Value = cboLoai.SelectedValue.ToString(); ;
                            cmd.Parameters.Add("@sl", SqlDbType.TinyInt).Value = txtSoLuong.Text;
                            cmd.Parameters.Add("@dgb", SqlDbType.SmallMoney).Value = txtDonGiaBan.Text;
                            cmd.Parameters.Add("@dgn", SqlDbType.SmallMoney).Value = txtDonGiaNhap.Text;
                            cmd.Parameters.Add("@ncc", SqlDbType.VarChar).Value = cboNhaCungCap.SelectedValue.ToString(); ;
                            cmd.Parameters.Add("@mt", SqlDbType.NText).Value = txtMoTa.Text;
                            cmd.Parameters.Add("@mahhcu", SqlDbType.VarChar).Value = mahh;
                            dataHangHoa.Update(cmd);
                        }
                        else
                        {
                            string sql = @"INSERT INTO HANGHOA (MAHH, TENHH, MALOAI, SOLUONG, DONGIABAN, DONGIANHAP, MANCC, MOTA)
                                           VALUES(@mahh, @tenhh, @maloai, @sl, @dgb, @dgn, @ncc, @mt)";
                            SqlCommand cmd = new SqlCommand(sql);
                            cmd.Parameters.Add("@mahh", SqlDbType.VarChar).Value = txtMaHH.Text;
                            cmd.Parameters.Add("@tenhh", SqlDbType.NVarChar).Value = txtTenHH.Text;
                            cmd.Parameters.Add("@maloai", SqlDbType.VarChar).Value = cboLoai.SelectedValue.ToString(); ;
                            cmd.Parameters.Add("@sl", SqlDbType.TinyInt).Value = txtSoLuong.Text;
                            cmd.Parameters.Add("@dgb", SqlDbType.SmallMoney).Value = txtDonGiaBan.Text;
                            cmd.Parameters.Add("@dgn", SqlDbType.SmallMoney).Value = txtDonGiaNhap.Text;
                            cmd.Parameters.Add("@ncc", SqlDbType.VarChar).Value = cboNhaCungCap.SelectedValue.ToString(); ;
                            cmd.Parameters.Add("@mt", SqlDbType.NText).Value = txtMoTa.Text;
                            dataHangHoa.Update(cmd);
                        }

                        // Tải lại form
                        fHangHoa_Load(sender, e);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        #endregion
        #region Lấy dữ liệu
        // lấy dữ liệu nhân viên
        public void LayDuLieu_HangHoa()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM HANGHOA");
            dataHangHoa.Fill(cmd);
            BindingSource binding = new BindingSource();
            binding.DataSource = dataHangHoa;
            dgvHangHoa.DataSource = binding;
        }
        // Lấy dữ liệu vào các ComboBox:
        public void LayDuLieu_HangHoa(ComboBox comboBox, string data, string display, string value)
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
        public void LayDuLieu_TimKiem(string TuKhoa)
        {
            SqlCommand cmd = new SqlCommand(@"SELECT * 
                                            FROM HANGHOA
                                            WHERE MAHH LIKE N'%" + TuKhoa + "%'" +
                                            " or TENHH LIKE N'%" + TuKhoa + "%'" +
                                            " or SOLUONG LIKE N'%" + TuKhoa + "%'" +
                                            " or DONGIANHAP LIKE N'%" + TuKhoa + "%'" +
                                            " or DONGIABAN LIKE N'%" + TuKhoa + "%'" +
                                            " or MANCC LIKE N'%" + TuKhoa + "%'" +
                                            " or MOTA LIKE N'%" + TuKhoa + "%'");
            dataHangHoa.Fill(cmd);
            BindingSource binding = new BindingSource();
            binding.DataSource = dataHangHoa;
            dgvHangHoa.DataSource = binding;
        }

        #endregion

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            LayDuLieu_TimKiem(txtTimKiem.Text);
        }
    }
}
