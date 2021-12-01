create database QLPetShop
go 
use QLPetShop
go

-- Tao bang Nguoi dung he thong
create table ACCOUNT(
	MaAC varchar(5),
	TEN Nvarchar(50) not null,
	CCCD varchar(12) not null unique,
	GioiTinh Nvarchar(3) not null check(GioiTinh = N'Nam' or GioiTinh = N'Nữ'),
	NgaySinh date,
	SDT varchar(12) not null,
	userName varchar(32) not null unique,
	passWord varchar(12) not null,
	Anh image,
	LoaiAC varchar(3) not null check(LoaiAC ='ad' or LoaiAC = 'nv'),

	primary key(MaAC)
)
go
--Tao bang hang hoa
create table LOAIHH(
	MaLoai varchar(5),
	TenLoai Nvarchar(30) not null,

	primary key(MaLoai)
)
go
--Tao bang nha cung cap hang hoa
create table NHACUNGCAP(
	MaNCC varchar(5),
	TenNCC Nvarchar(30) not null,
	SDT varchar(12) not null,
	DiaChi Ntext,

	primary key(MaNCC)
)
go 
--Tao bang hang hoa
create table HANGHOA(
	MaHH varchar(5),
	TenHH Nvarchar(30) not null,
	MaLoai varchar(5) not null,
	SoLuong tinyint,
	DonGiaNhap smallmoney,
	DonGiaBan smallmoney not null,
	MaNCC varchar(5),
	DonViTinh Nvarchar(10),
	MoTa Ntext,
	Anh image,

	primary key(MaHH),
	foreign key (MaLoai) references LOAIHH(MaLoai),
	foreign key (MaNCC) references NHACUNGCAP(MaNCC)
)
go
--Tao bang khach hang
create table KHACHHANG(
	MaKH varchar(5),
	TenKH Nvarchar(30) not null,
	GioiTinh Nvarchar(3) not null check(GioiTinh = N'Nam' or GioiTinh = N'Nữ'),
	SDT varchar(12) not null,
	DiaChi Ntext,

	primary key(MaKH)
)
go
-- Tao bang hoa don ban va chi tiet HD
create table HOADONBAN(
	MaHDban varchar(9),
	MaAC varchar(5),
	MaKH varchar(5),
	NgayLap datetime not null,
	ThanhTien smallmoney

	primary key(MaHDban),
	foreign key(MaAC) references ACCOUNT(MaAC),
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
	MaAC varchar(5),
	MaNCC varchar(5),
	NgayLap datetime not null,
	ThanhTien smallmoney

	primary key(MaHDnhap),
	foreign key(MaAC) references ACCOUNT(MaAC),
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