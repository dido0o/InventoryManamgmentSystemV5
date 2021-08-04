-----------------------------------------------Initializing database tables-------------------------------------------
-------------------------- «—ÌŒ »œ«Ì… «·⁄„· ⁄·Ï ﬁÊ«⁄œ «·»Ì«‰«  26/„«—”/2021-------------------------------------

--Create table Stores (id int primary key NOT NULL IDENTITY(1,1),
--StoreId int UNIQUE NOT NULL,
--StoreName nvarchar(30) NOT NULL UNIQUE,
--StoreLocation nvarchar(20) NOT NULL,
--StoreDetails nvarchar(150) )

--insert into Stores values (101, 'InitialStore', 'InitialLocation',
--'This is a dummy data, and this is the initialization store; in this field we can write any other details')


--insert into Stores values (102, '2ndInitialStore', '2ndInitialLocation',
--'This is a dummy data, and this is the 2ndinitialization store; in this field we can write any other details')


--insert into Stores values (103, '3rdInitialStore', '3rdInitialLocation',
--'This is a dummy data, and this is the 3rdinitialization store; in this field we can write any other details')


--insert into Stores values (104, '4thInitialStore', '4thInitialLocation',
--'This is a dummy data, and this is the 4thinitialization store; in this field we can write any other details')

select * from Stores

----------------------------------------------------------------------------------------------------------------------

--Create table Items (id int primary key not null identity(1,1), ISerialNum nvarchar(200) Unique not null,
--ItemName nvarchar(50) not null ,ItemBrand nvarchar (50) not null, ItemStatus nvarchar (20) not null, 
--IStatusID int not null, FOREIGN KEY (IStatusID) REFERENCES IStatus(StatusID))

--insert into Items values ('Initial serial num', 'Initial Item', 'initial brand', 'initial status', '0')

--insert into Items values ('1Initial serial num', '1Initial Item', '1initial brand', '1initial status', 0)

--insert into Items values ('3Initial serial num', '1Initial Item11', '11initial brand', '11initial status', 0)

--insert into Items values ('init', 'init', 'initbrand', 'initstatus', 0)

select * from Items

----------------------------------------------------------------------------------------------------------------------

--Create table Agency (Id int not null identity(1,1) unique, AgencyID int primary key not null,
--AgencyName nvarchar (30) unique not null)

select * from Agency

----------------------------------------------------------------------------------------------------------------------

--create table VUsers (id int identity(1,1) not null, UserID int primary key, UserName nvarchar(40) not null,
--Agency nvarchar(30) not null, AgencyId int not null, foreign key (AgencyId) REFERENCES Agency(AgencyID))

--insert into VUsers values (00,'initial user','Initial agency',1)

select * from VUsers

--drop table VUsers
----------------------------------------------------------------------------------------------------------------------

--Create table IStatus (id int identity(1,1) unique, Status nvarchar(25) not null unique, StatusID int primary key not null)

--insert into IStatus values ('initial status', 0)

select * from IStatus

--drop table IStatus
----------------------------------------------------------------------------------------------------------------------
-------------------------------------------------------Sub tables-----------------------------------------------------

--create table AgencyUserMapping (id int identity(1,1), AgencyId int not null, AgencyName nvarchar(30) not null,
--UserId int not null, UserName nvarchar(40) not null, Foreign key (AgencyId) references Agency(AgencyID),
--Foreign key (UserId) references VUsers(UserID))

--insert into AgencyUserMapping values (0,'initial Agency', 0, 'intial user')

select * from AgencyUserMapping

--drop table AgencyUserMapping

----------------------------------------------------------------------------------------------------------------------

Create table UsersItems (operationId int identity(1,1)primary key, exchangeID int not null, ISerialNum nvarchar(200) not null,
UserId int not null, currentDateTime datetime not null, foreign key (exchangeID) references Items(id),
foreign key (UserId) references VUsers(UserID)) 

insert into UsersItems values (1002,'initial serial num', 0, GETDATE())
  
insert into UsersItems values (3,'1Initial serial num', 0, GETDATE())

select * from UsersItems

drop table UsersItems

----------------------------------------------------------------------------------------------------------------------

  --create table StoreItems (id int not null identity(1,1), StoreId int not null, StoreName nvarchar(30) not null,
  --ISerialNum nvarchar(200) not null unique, ItemName nvarchar(50) not null, foreign key (StoreId) references Stores(StoreId),
  --foreign key (ISerialNum) references Items(ISerialNum))

  select * from StoreItems

  --insert into StoreItems values (101,'InitialStore', 'Initial serial num', 'Initial Item')
  
  --insert into StoreItems values (102,'2ndInitialStore', '1Initial serial num', '1Initial Item')

  --drop table StoreItems

  ----------------------------------------------------------------------------------------------------------------------

--create table ItemHistory (id int identity(1,1), ISerialNum nvarchar(200) not null unique, CurrentOwnerID int not null,
--CurrentStatusID int not null, CurrentDateTime DateTime not null, Notes nvarchar(400),
--foreign key (ISerialNum) references Items(ISerialNum), foreign key (CurrentOwnerID) references VUsers (UserID),
--foreign key (CurrentStatusID) references IStatus(StatusID))

--select * from ItemHistory

--insert into ItemHistory values ('Initial serial num', 0, 0, CURRENT_TIMESTAMP, 'initial Notes')

--drop table ItemHistory

----------------------------------------------------------------------------------------------------------------------

  --create table IBrand (BrandID int primary key, BrandName nvarchar(40) not null unique)
  --insert into IBrand values (0, 'Inintial Brand')
  --insert into IBrand values (1, '2nd Initial Brand')
  --insert into IBrand values (2, '3rd Inintial Brand')

  select * from IBrand

  --drop table IBrand

-------------------------- «—ÌŒ «·«ﬂ „«· «·„»œ∆Ì ·ﬁÊ«⁄œ «·»Ì«‰«  15/«»—Ì·/2021-------------------------------------