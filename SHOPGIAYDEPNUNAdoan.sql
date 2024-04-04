create database DOANVEGIAYDEP
use DOANVEGIAYDEP
CREATE TABLE NHANVIEN(
IDNV NVARCHAR(50) NOT NULL PRIMARY KEY,
HOTEN NVARCHAR(50),
DCHI NVARCHAR(50),
SDT NVARCHAR(20),
NGSINH SMALLDATETIME,
NGVL SMALLDATETIME,
CHUCVU NVARCHAR(50),
)
insert into nhanvien
values('NV01','Nguyen Van A','Bac Giang','0339393167','2-2-2003','1-3-2023','Nhan Vien'),
	('NV02','Nguyen Van B','Ha Noi','0339393167','2-5-2003','7-3-2023','Nhan Vien')

select*from nhanvien
update nhanvien set hoten='huong',
dchi='bacging',sdt='0989876765',ngsinh='1-1-2022',ngvl='8-8-2023',chucvu='nv' where idnv=''


create table NGUOIDUNG(

ACCOUNT NVARCHAR(50) NOT NULL PRIMARY KEY,
PASSWORK NVARCHAR(50))
insert into nguoidung values('huong','123'),('ling','123')


create table Quanly(
ACCOUNT NVARCHAR(50) primary key,
PASSWORK NVARCHAR(50))
insert into quanly values('admin','123')
insert into quanly values('quanly','123')
update quanly set passwork='111' where account='admin'
select*from quanly

SELECT ACCOUNT,PASSWORK from nguoidung

CREATE TABLE LUONG(
IDNV NVARCHAR(50) NOT NULL FOREIGN KEY REFERENCES NHANVIEN(IDNV),
NGCONG INT,
LUONG FLOAT,
THUONG FLOAT,
THANG INT)
insert into luong values('NV01','26','5200000','300000','1'),
						('NV02','22','4200000','300000','1')

CREATE TABLE NHACUNGCAP(
IDNCC NVARCHAR(50) NOT NULL PRIMARY KEY,
TENNCC NVARCHAR(50),
DCHI NVARCHAR(50),
SDT NVARCHAR(20),
GHICHU NTEXT)
insert into nhacungcap values('NCC01','Nguyen Thi A','Thai Nguyen','0987676564','null'),
							('NCC02','Nguyen Thi B','Bac Giang','0987676564','null')
							select* from nhacungcap where tenNCC like '%B%'

CREATE TABLE GIAYDEP(
IDSP NVARCHAR(50) NOT NULL PRIMARY KEY,
TENSP NVARCHAR(50),
SOLUONG INT,
LOAI NVARCHAR(50),
KICHCO INT,
GHICHU NTEXT,
GIA FLOAT,
IDNCC NVARCHAR(50) FOREIGN KEY REFERENCES NHACUNGCAP(IDNCC)) 
delete giaydep where idsp='SP01'
insert into giaydep values('SP01','Nike','100','L1','38','Null','800000','NCC01'),
							('SP02','Nike','100','L2','38','Null','500000','NCC01'),
							('SP03','Nike','100','L3','38','Null','200000','NCC01')
							select*from giaydep
update giaydep set tensp='Jodan',soluong='50',loai='L1',kichco='40',ghichu='Null',gia='1000000',idncc='NCC02' where idsp='SP02'
select *from giaydep where idsp  like '%3%'

CREATE TABLE HOADON(
IDHD NVARCHAR(50) NOT NULL PRIMARY KEY,
IDNV NVARCHAR(50) FOREIGN KEY REFERENCES NHANVIEN(IDNV),
NGLAP SMALLDATETIME,
THANHTIEN FLOAT)
insert into hoadon values('HD01','NV01','5-9-2023','2000000'),
						('HD02','NV02','4-9-2023','1000000')
select*from cthoadon

CREATE TABLE CTHOADON(
IDHD NVARCHAR(50) NOT NULL FOREIGN KEY REFERENCES HOADON(IDHD),
IDSP NVARCHAR(50) NOT NULL FOREIGN KEY REFERENCES GIAYDEP(IDSP),
PRIMARY KEY(IDHD),
SOLUONG INT,
DONGIA FlOAT)

--ALTER TABLE CTHOADON
--ADD THANHTIEN FLOAT;


insert into CTHOADON values('HD01','SP01','5','4000000',''),
							('HD02','SP02','5','3000000','')
					
SELECT CTHOADON.IDSP , GIAYDEP.TENSP,CTHOADON.SoLuong, GIAYDEP.GIA,CTHOADON.THANHTIEN 
FROM CTHOADON, GIAYDEP
WHERE CTHOADON.IDHD = N'{0}'
AND CTHOADON.IDSP = GIAYDEP.IDSP


CREATE TABLE PHIEUNHAPKHO(
IDPNK NVARCHAR(50) NOT NULL PRIMARY KEY,
NGLAP SMALLDATETIME,
IDNV nvarchar(50) FOREIGN KEY REFERENCES nhanvien(IDNV),
IDNCC nvarchar(50) not null foreign key references nhacungcap(idncc),
TONGTIENNHAP float)
insert into phieunhapkho values('PN03','1-1-2023','NV01','NCC01',''),
							('PN01','1-1-2023','NV01','NCC01',''),
							('PN02','1-1-2023','NV01','NCC02','')

	
CREATE TABLE CTPHIEUNHAPKHO(
IDPNK NVARCHAR(50) NOT NULL FOREIGN KEY REFERENCES PHIEUNHAPKHO(IDPNK),
IDSP NVARCHAR(50) NOT NULL FOREIGN KEY REFERENCES GIAYDEP(IDSP),
Primary key(idpnk),
SOLUONG INT,
DONGIA float)

INSERT INTO CTPHIEUNHAPKHO(IDPNK, IDSP, SOLUONG, DONGIA) VALUES ('PN03','sp03','100',2000)
insert into ctphieunhapkho values('PN01','SP01','100','200000')
insert into ctphieunhapkho values('PN02','SP01','100','200000')
select*from ctphieunhapkho
SELECT CTPHIEUNHAPKHO.IDSP , GIAYDEP.TENSP,CTPHIEUNHAPKHO.SOLUONG, GIAYDEP.GIA
FROM CTPHIEUNHAPKHO, GIAYDEP
WHERE CTPHIEUNHAPKHO.IDPNK = N'{0}'
AND CTPHIEUNHAPKHO.IDSP = GIAYDEP.IDSP

SELECT CTPHIEUNHAPKHO.IDSP , GIAYDEP.TENSP,CTPHIEUNHAPKHO.SOLUONG, GIAYDEP.GIA 
FROM CTPHIEUNHAPKHO , GIAYDEP 
WHERE CTPHIEUNHAPKHO.IDPNK = N'{0}'
AND CTPHIEUNHAPKHO.IDSP = GIAYDEP.IDSP 
N'{0}'
select*from phieunhapkho
	update phieunhapkho set tongtiennhap=(select sum(soluong*dongia)
	from ctphieunhapkho where ctphieunhapkho.idpnk=phieunhapkho.idpnk
	group by ctphieunhapkho.idpnk)


select*,soluong*dongia as ThanhTien from ctPHIEUNHAPKHO
update ctphieunhapkho set IDPNK='PN02',IDSP='SP01',SOLUONG=3,DONGIA=400000 where idctpn='ct01'

SELECT PHIEUNHAPKHO.IDPNK, nglap,CTPHIEUNHAPKHO .soluong * CTPHIEUNHAPKHO.dongia AS thanhtien
FROM PHIEUNHAPKHO,CTPHIEUNHAPKHO,giaydep
where CTPHIEUNHAPKHO.IDPNK=PHIEUNHAPKHO.IDPNK
and CTPHIEUNHAPKHO.IDSP = giaydep.IDSP ;


SELECT a.IDSP, b.TENSP, a.SoLuong, b.GIA,a.DONGIA
FROM CTHOADON AS a, GIAYDEP AS b 
WHERE a.IDHD = 'HD01' AND a.IDSP=b.IDSP

Select HOTEN from NHANVIEN where IDNV ='NV01'