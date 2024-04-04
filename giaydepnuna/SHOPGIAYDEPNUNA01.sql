Create database GIAYDEPNUNADOTNET

CREATE TABLE NHANVIEN(
IDNV NVARCHAR(15) NOT NULL PRIMARY KEY,
HOTEN NVARCHAR(40),
DCHI NVARCHAR(50),
SDT NVARCHAR(20),
NGSINH SMALLDATETIME,
NGVL SMALLDATETIME,
CHUCVU NVARCHAR(30),
)
insert into nhanvien 
values('NV01','Nguyen Van A','Bac Giang','0339393167','2-2-2003','1-3-2023','Nhan Vien'),
	('NV02','Nguyen Van B','Ha Noi','0339393167','2-5-2003','7-3-2023','Nhan Vien')
insert into nhanvien values('NV03','Nguyen Van C','Ha Noi','0339393167','5-5-2003','7-7-2023','Nhan Vien')
insert into nhanvien values('NV04','Nguyen Thị D','Thai Nguyen','0339393167','5-5-2003','7-7-2023','Nhan Vien')

select*from nhanvien
update nhanvien set hoten='huong',
dchi='bacging',sdt='0989876765',ngsinh='1-1-2022',ngvl='8-8-2023',chucvu='nv' where idnv=''


create table NGUOIDUNG(
IDNV NVARCHAR(15) NOT NULL FOREIGN KEY REFERENCES NHANVIEN(IDNV),
ACCOUNT NVARCHAR(30) NOT NULL,
PRIMARY KEY(IDNV, ACCOUNT),
PASSWORK NVARCHAR(30))

insert into nguoidung
values('NV01','huong','123'),
		('NV02','huong','111')
SELECT ACCOUNT,PASSWORK from nguoidung
drop table luong
CREATE TABLE LUONG(
IDNV NVARCHAR(15) NOT NULL FOREIGN KEY REFERENCES NHANVIEN(IDNV),
NGAYCONG INT,
THUONG float,
THANG INT,
primary key(idnv))
insert into luong values('NV01','26','0','1'),
						('NV02','22','0','1')
insert into luong values('NV04','26','0','2')

select idnv,ngaycong,thang,thuong,ngaycong*300000+thuong as luong from luong
update luong set ngaycong='25',thuong='200000',thang='1' where idnv='nv04'
CREATE TABLE NHACUNGCAP(
IDNCC NVARCHAR(15) NOT NULL PRIMARY KEY,
TENNCC NVARCHAR(40),
DCHI NVARCHAR(50),
SDT NVARCHAR(20),
GHICHU NTEXT)
insert into nhacungcap values('NCC01','Nguyen Thi A','Thai Nguyen','0987676564','null'),
							('NCC02','Nguyen Thi B','Bac Giang','0987676564','null')
							select* from nhacungcap where tenNCC like '%B%'
							delete nhacungcap where idncc='NCC02'
delete nhacungcap where idncc='NCC01'
CREATE TABLE GIAYDEP(
IDSP NVARCHAR(15) NOT NULL PRIMARY KEY,
TENSP NVARCHAR(40),
SOLUONG INT,
LOAI NVARCHAR(25),
KICHCO INT,
GHICHU NTEXT,
GIA MONEY,
IDNCC NVARCHAR(15) FOREIGN KEY REFERENCES NHACUNGCAP(IDNCC)) 
insert into giaydep values('SP01','Nike','100','L1','38','Null','800000','NCC01'),
							('SP02','Nike','100','L2','38','Null','500000','NCC01'),
							('SP03','Nike','100','L3','38','Null','200000','NCC01')
						delete giaydep where idsp=''

CREATE TABLE HOADON(
IDHD NVARCHAR(15) NOT NULL PRIMARY KEY,
IDNV NVARCHAR(15) FOREIGN KEY REFERENCES NHANVIEN(IDNV),
NGLAP SMALLDATETIME,
TRIGIA MONEY)
insert into hoadon values('HD01','NV01','5-9-2023','2000000'),
						('HD02','NV02','4-9-2023','1000000')
drop table cthoadon
CREATE TABLE CTHOADON(
IDHD NVARCHAR(15) NOT NULL FOREIGN KEY REFERENCES HOADON(IDHD),
IDSP NVARCHAR(15) NOT NULL FOREIGN KEY REFERENCES GIAYDEP(IDSP),
PRIMARY KEY(IDHD, IDSP),
SOLUONG INT,
THANHTIEN MONEY)
insert into CTHOADON values('HD01','SP01','5','4000000'),
							('HD02','SP02','5','3000000')
		drop table 	PHIEUNHAPKHO		

CREATE TABLE PHIEUNHAPKHO(

IDPNK NVARCHAR(15) NOT NULL FOREIGN KEY REFERENCES CTPHIEUNHAPKHO(IDPNK),
IDNV nvarchar(15) FOREIGN KEY REFERENCES nhanvien(IDNV))
insert into phieunhapkho values('PN03','NV01')
insert into phieunhapkho values('PN02','NV01')
					
	
CREATE TABLE CTPHIEUNHAPKHO(
IDPNK NVARCHAR(15) NOT NULL PRIMARY KEY,
IDSP NVARCHAR(15) NOT NULL FOREIGN KEY REFERENCES GIAYDEP(IDSP),
NGLAP datetime,
SOLUONG INT,
DONGIA float,
IDNCC nvarchar(15) not null foreign key references nhacungcap(idncc))
insert into ctphieunhapkho values('PN03','SP01','1-1-2023','100','200000','ncc01')
select*from CTPHIEUNHAPKHO
insert into CTPHIEUNHAPKHO values ('PN02','SP02','2-2-2023','50','100000','ncc01')
update ctphieunhapkho set idsp='SP01',nglap='7-8-2023',soluong='20',dongia='300000',idncc='ncc01' where idpnk='PN03'

SELECT ctPHIEUNHAPKHO.IDPNK,CTPHIEUNHAPKHO.nglap, CTPHIEUNHAPKHO .soluong * CTPHIEUNHAPKHO.dongia AS thanhtien
FROM PHIEUNHAPKHO,CTPHIEUNHAPKHO,giaydep
where CTPHIEUNHAPKHO.IDPNK=PHIEUNHAPKHO.IDPNK
and CTPHIEUNHAPKHO.IDSP = giaydep.IDSP 

select ctphieunhapkho.idpnk,ctphieunhapkho.nglap,ctphieunhapkho.soluong*ctphieunhapkho.dongia as Thanhtien
from ctphieunhapkho

 






