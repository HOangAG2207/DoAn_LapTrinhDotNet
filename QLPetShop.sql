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
	DONGIANHAP money,
	DONGIABAN money,
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
	MAHDBAN varchar(20),
	MANV varchar(5),
	MAKH varchar(5),
	NGAYLAP datetime not null,
	THANHTIEN money

	primary key(MAHDBAN),
	foreign key(MANV) references NHANVIEN(MANV),
	foreign key(MAKH) references KHACHHANG(MAKH)
)
go
create table HOADONBAN_CHITIET(
	STT smallint,
	MaHDban varchar(20),
	MaHH varchar(5),
	SoLuong tinyint,
	DGban money,
	ThanhTien money

	foreign key(MaHDban) references HOADONBAN(MaHDban),
	foreign key(MaHH) references HANGHOA(MaHH)
)
go
--Tao bang hoa don nhap va chi tiet HD
create table HOADONNHAP(
	MaHDnhap varchar(20),
	MANV varchar(5),
	MaNCC varchar(5),
	NgayLap datetime not null,
	ThanhTien money

	primary key(MaHDnhap),
	foreign key(MANV) references NHANVIEN(MANV),
	foreign key(MaNCC) references NHACUNGCAP(MaNCC)
)
go
create table HOADONNHAP_CHITIET(
	MaHDnhap varchar(20),
	MaHH varchar(5),
	SoLuong tinyint,
	DGnhap money,
	ThanhTien money

	foreign key(MaHDnhap) references HOADONBAN(MaHDban),
	foreign key(MaHH) references HANGHOA(MaHH)
)
-- insert values to table NHANVIEN
insert into NHANVIEN (MANV, TENNV, CCCD, GIOITINH, NGAYSINH, SDT)
values ('ad1', N'Quản lý', '123456789', 0, '2021-12-07', '0123456789')
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
--insert values to table NHACUNGCAP
insert into NHACUNGCAP
values ('NCC01', N'Pet Food', '0123456', N'Long Xuyên')
insert into NHACUNGCAP
values ('NCC02', N'Pet Cloth', '0123457', N'TPHCM')
insert into NHACUNGCAP
values ('NCC03', N'Pet Nội Thất', '0123458', N'Long Xuyên')
insert into NHACUNGCAP
values ('NCC04', N'Pet Phụ kiện', '0123459', N'Cần Thơ')
insert into NHACUNGCAP
values ('NCC05', N'Pet Vệ Sinh', '0123451', N'Long Xuyên')
insert into NHACUNGCAP
values ('NCC06', N'Pet Khác', '0123452', N'Long Xuyên')
--insert values to table KHACHHANG
insert into KHACHHANG
values('KH01', N'Hoàng', 0, '01111111', N'Long Xuyên')
insert into KHACHHANG
values('KH02', N'Thanh', 0, '02222222', N'Long Xuyên')
insert into KHACHHANG
values('KH03', N'Khanh', 0, '03333333', N'Long Xuyên')
insert into KHACHHANG
values('KH04', N'An', 0, '04444444', N'Long Xuyên')
insert into KHACHHANG
values('KH05', N'Trâm', 1, '05555555', N'Long Xuyên')