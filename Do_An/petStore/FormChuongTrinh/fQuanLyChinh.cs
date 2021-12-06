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

namespace petStore
{
    public partial class fQuanLyChinh : Form
    {
        public fQuanLyChinh(string userName)
        {
            InitializeComponent();
            label2.Text = userName;
        }
        #region Biến toàn cục
        bool flag = false;
        bool maximum = false;
        FormChuongTrinh.fTaiKhoanVaNhanVien TKvaNV = null;
        FormChuongTrinh.fAbout about = null;
        #endregion
        private void fQuanLyChinh_Load(object sender, EventArgs e)
        {
            //pnlLeft.Width = 220;
        }
        #region Thao tác với Menu
        private void mnuNhanVien_Click(object sender, EventArgs e)
        {
            if (TKvaNV == null || TKvaNV.IsDisposed)
            {
                TKvaNV = new FormChuongTrinh.fTaiKhoanVaNhanVien(); TKvaNV.MdiParent = this;
                
                TKvaNV.Show();
            }
            else
                TKvaNV.Activate();
        }
        private void mnuThongTinPM_Click(object sender, EventArgs e)
        {
            if (about == null || about.IsDisposed)
            {
                about = new FormChuongTrinh.fAbout(); about.MdiParent = this;

                about.Show();
            }
            else
                about.Activate();
        }
        #endregion
        #region Thao tác với Form
        // button ẩn hiện panel Trái
        private void btnPnlShowHide_Click(object sender, EventArgs e)
        {
            if (flag)
            {
                pnlLeft.Width = 220;
            }
            else
            {
                pnlLeft.Width = 40;
            }
            flag = !flag;
        }
        //button Thoát Form
        private void vbtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //button Maximum Form
        private void vbtnMaximum_Click(object sender, EventArgs e)
        {
            if (!maximum)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
            maximum = !maximum;
        }
        //button Minimize Form
        private void vbtnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        //button Đăng xuất
        private void vbtnDangXuat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        
    }
}
