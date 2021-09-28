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
	FIO nchar(30) not null,
	rate float not null,
	experience int not null,
	children int not null,
	CONSTRAINT Prim_ID_Driver PRIMARY KEY (id),
)  
GO

CREATE TABLE Child(
	driver int not null,
	FIO nchar(30) not null,
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
id int primary key not null,
datee date not null,
summa decimal not null
)
GO

CREATE TABLE Decrease
(
id int primary key not null,
datee date not null,
summa decimal not null
)
GO

CREATE TABLE Paycheck
(
id int primary key not null,
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
('Егор Летов', 2, 2, 0),
('Тимой Рудольфович', 1, 2, 0),
('Евгений Сташинский', 1, 2, 0),
('Зьмицер Вишнёв', 20, 20, 0),
('Анна Зеленская', 5, 2, 0),
('Анно Хидэаки', 20, 20, 0),
('Йкко Таро', 10, 20, 0),
('Кадзима Гений', 13, 21, 0)
GO

delete Driver where id = 1

select * from Driver