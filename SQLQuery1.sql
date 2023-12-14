create database ProductInventoryDB
use ProductInventoryDB

create table Products
(
 ProductId int primary key,
 ProductName nvarchar(100),
 Price float,
 Quantity int,
 MfDate date,
 ExpDate date
)



	select * from Products




