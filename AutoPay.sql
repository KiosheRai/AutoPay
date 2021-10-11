CREATE DATABASE AutoPay

USE AutoPay
GO

DROP TABLE Paycheck;
DROP TABLE Bonus;
DROP TABLE Decrease;
DROP TABLE WorkDay;
DROP TABLE Child;
DROP TABLE Driver;

CREATE TABLE Driver(
	id int identity(1,1),
	FIO nchar(40) not null,
	rate float not null,
	experience int not null,
	children int not null,
	CONSTRAINT Prim_ID_Driver PRIMARY KEY (id),
)  
GO

CREATE TABLE Child(
	id int identity(1,1),
	driver int not null,
	FIO nchar(40) not null,
	birthday date not null,
	CONSTRAINT Fore_Child_Driver FOREIGN KEY (driver) REFERENCES Driver([id])
)  
GO

CREATE TABLE WorkDay(
	driver int not null,
	datee date not null,
	shiftt int not null,
	daytype nchar(10) not null,
	CONSTRAINT Fore_WorkDay_Driver FOREIGN KEY (driver) REFERENCES Driver([id])
)  
GO

CREATE TABLE Bonus
(
id int primary key identity(1,1) not null,
datee date not null,
summa decimal not null
)
GO

CREATE TABLE Decrease
(
id int primary key identity(1,1) not null,
datee date not null,
summa decimal not null
)
GO

CREATE TABLE Paycheck
(
id int primary key identity(1,1) not null,
driver int not null, 
starte date not null,
ende date not null,
summa decimal not null,
decrease int not null,
bonus int not null,
CONSTRAINT Fore_Paycheck_Driver FOREIGN KEY (driver) REFERENCES Driver([id]),
CONSTRAINT Fore_Paycheck_Decrease FOREIGN KEY (decrease) REFERENCES decrease([id]),
CONSTRAINT Fore_Paycheck_Bonus FOREIGN KEY (bonus) REFERENCES bonus([id])
)
GO

INSERT INTO Driver(FIO, rate, experience, children) 
values
('Åãîğ Ëåòîâ', 2, 2, 1),
('Òèìîé Ğóäîëüôîâè÷', 1, 2, 0),
('Åâãåíèé Ñòàøèíñêèé', 1, 2, 1),
('Çüìèöåğ Âèøí¸â', 20, 20, 2),
('Àííà Çåëåíñêàÿ', 5, 2, 1),
('Àííî Õèäıàêè', 20, 20, 0),
('Éêêî Òàğî', 10, 20, 0),
('Êàäçèìà Ãåíèé', 13, 21, 0)
GO

INSERT INTO Child(driver, FIO, birthday)
values
(1, 'Ïåòğîâñêèé Àíäğåé', '17-07-2003'),
(3, 'Ñêğåáåö Òàòüÿíà', '09-03-2003'),
(4, 'Àëôèìîâ Àğñıí', '29-03-2003'),
(5, 'Ëÿõ Àğò¸ì', '10-04-2003'),
(4, 'Ñèäîğåíêî Äàğüÿ', '20-08-2003')
GO

INSERT INTO WorkDay(driver, datee, shiftt, daytype)
values
(1, '07-10-2021', 8, 'Âûõîäíîé'),
(1, '06-10-2021', 8, 'Âûõîäíîé'),
(1, '05-10-2021', 8, 'Âûõîäíîé'),
(1, '30-10-2021', 8, 'Âûõîäíîé'),
(3, '06-10-2021', 2, 'Âûõîäíîé'),
(4, '07-10-2021', 3, 'Âûõîäíîé')
GO

insert into Bonus(datee, summa)
values
(GETDATE(), 200)
go

insert into Decrease(datee,summa)
values
(GETDATE(), 100)
go

insert into Paycheck(driver, starte, ende, summa, decrease, bonus) values (1, '01-10-2021', '10-10-2021', 200, 1, 1)
GO

create trigger trig_1
on Driver
instead of delete
as
if exists (select * from Deleted Driver join Child on Driver.id = Child.driver)
begin 
delete from Child where driver in (select driver from deleted)
begin
delete from Driver where id in (select id from deleted)
end
end
GO

SET DATEFORMAT DMY;
GO



delete Driver where id = 6
select count(*) from Child where driver = 10

select * from Driver
select * from Child
select * from WorkDay

select getDate()

SELECT GETDATE ( )

select * from WorkDay where CONVERT(date,GETDATE()) like datee and driver = 2

SELECT * FROM Driver where FIO like '%Åãîğ%'

SELECT MAX(id) FROM Bonus

select sum(shiftt) from WorkDay where driver = 1 and datee > '1-10-2021' and datee < '10-10-2021' 

