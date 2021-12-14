create database QLPetShop
go 
use QLPetShop
go
--Tao bang NHANVIEN
create table NHANVIEN(
	MANV varchar(5),
	TENNV Nvarchar(50) not null,
	CCCD varchar(12) not null,
	GIOITINH bit not null,
	NGAYSINH date,
	SDT varchar(12) not null,
	ANH image,

	primary key(MANV)
)
go
-- Tao bang Nguoi dung he thong
create table TAIKHOAN(
	TENDANGNHAP varchar(32) not null,
	MATKHAU varchar(12) not null,
	QUYENHAN varchar(10) not null,
	GHICHU Nvarchar (30),
	MANV varchar(5), 

	primary key(TENDANGNHAP),
	foreign key(MANV) references NHANVIEN(MANV)
)
go
--Tao bang nha cung cap hang hoa
create table NHACUNGCAP(
	MANCC varchar(5),
	TENNCC Nvarchar(30) not null,
	SDT varchar(12) not null,
	DIACHI Ntext,

	primary key(MaNCC)
)
go 
--Tao bang hang hoa
create table LOAIHH(
	MALOAI varchar(5),
	TENLOAI Nvarchar(50) not null,

	primary key(MALOAI)
)
go
--Tao bang hang hoa
create table HANGHOA(
	MAHH varchar(5),
	TENHH Nvarchar(30) not null,
	MALOAI varchar(5) not null,
	SOLUONG tinyint,
	DONGIANHAP smallmoney,
	DONGIABAN smallmoney,
	MANCC varchar(5),
	MOTA Ntext,
	ANH image,

	primary key(MAHH),
	foreign key (MALOAI) references LOAIHH(MALOAI),
	foreign key (MANCC) references NHACUNGCAP(MANCC)
)
go
--Tao bang khach hang
create table KHACHHANG(
	MAKH varchar(5),
	TENKH Nvarchar(30) not null,
	GIOITINH bit not null,
	SDT varchar(12) not null,
	DIACHI Ntext,

	primary key(MaKH)
)
go
-- Tao bang hoa don ban va chi tiet HD
create table HOADONBAN(
	MaHDban varchar(9),
	MANV varchar(5),
	MaKH varchar(5),
	NgayLap datetime not null,
	ThanhTien smallmoney

	primary key(MaHDban),
	foreign key(MANV) references NHANVIEN(MANV),
	foreign key(MaKH) references KHACHHANG(MaKH)
)
go
create table HOADONBAN_CHITIET(
	MaHDban varchar(9),
	MaHH varchar(5),
	SoLuong tinyint,
	DGban smallmoney,
	ThanhTien smallmoney

	foreign key(MaHDban) references HOADONBAN(MaHDban),
	foreign key(MaHH) references HANGHOA(MaHH)
)
go
--Tao bang hoa don nhap va chi tiet HD
create table HOADONNHAP(
	MaHDnhap varchar(9),
	MANV varchar(5),
	MaNCC varchar(5),
	NgayLap datetime not null,
	ThanhTien smallmoney

	primary key(MaHDnhap),
	foreign key(MANV) references NHANVIEN(MANV),
	foreign key(MaNCC) references NHACUNGCAP(MaNCC)
)
go
create table HOADONNHAP_CHITIET(
	MaHDnhap varchar(9),
	MaHH varchar(5),
	SoLuong tinyint,
	DGnhap smallmoney,
	ThanhTien smallmoney

	foreign key(MaHDnhap) references HOADONBAN(MaHDban),
	foreign key(MaHH) references HANGHOA(MaHH)
)
-- insert values to table NHANVIEN
insert into NHANVIEN (MANV, TENNV, CCCD, GIOITINH, NGAYSINH, SDT)
values ('ad1', N'Quản lý', '123456789', 0, '2021-12-07', '0123456789')
insert into NHANVIEN (MANV, TENNV, CCCD, GIOITINH, NGAYSINH, SDT)
values ('ad2', N'Quản lý 2', '12345678', 1, '2021-12-07', '0123456789')
insert into NHANVIEN (MANV, TENNV, CCCD, GIOITINH, NGAYSINH, SDT)
values ('nv001', N'Nguyễn Huy Hoàng', '123456780', 0, '1997-07-22', '0123456780')
insert into NHANVIEN (MANV, TENNV, CCCD, GIOITINH, NGAYSINH, SDT)
values ('nv002', N'Huỳnh Nguyễn Thái An', '123456700', 0, '2001-07-11', '0123456700')
insert into NHANVIEN (MANV, TENNV, CCCD, GIOITINH, NGAYSINH, SDT)
values ('nv003', N'Nguyễn Thị Bích Trâm', '123456000', 1, '2001-09-16', '0123456000')
--insert values to table TAIKHOAN
insert into TAIKHOAN
values ('admin', '123', 'ad', N'Quản Lý','ad1')
insert into TAIKHOAN
values ('huyhoang', '123', 'ad', N'Nhân viên','nv001')
insert into TAIKHOAN
values ('anhuynh', '123', 'nv', N'Nhân viên','nv002')
insert into TAIKHOAN
values ('bichtram', '123', 'nv', N'Nhân viên','nv003')
--insert values to table LOAIHH
insert into LOAIHH
values ('TAC', N'Thức ăn cho Chó')
insert into LOAIHH
values ('TAM', N'Thức ăn cho Mèo')
insert into LOAIHH
values ('QAC', N'Quần áo cho Chó')
insert into LOAIHH
values ('QAM', N'Quần áo cho Mèo')
insert into LOAIHH
values ('VS', N'Dụng cụ vệ sinh')
insert into LOAIHH
values ('PKC', N'Phụ kiện và đồ chơi cho Chó')
insert into LOAIHH
values ('PKM', N'Phụ kiện và đồ chơi cho Mèo')
insert into LOAIHH
values ('YTT', N'Vật dụng Y tế và Thuốc')
insert into LOAIHH
values ('KH', N'Vật dụng khác')
