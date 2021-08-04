-----------------------------------------------Initializing database tables-------------------------------------------
-------------------------- «—ÌŒ »œ«Ì… «·⁄„· ⁄·Ï «·«’œ«— «·À«·À „‰ ﬁÊ«⁄œ «·»Ì«‰«  26/„«ÌÊ/2021---------------------------

---------------------------------------------------All Tables---------------------------------------------------------

use InventoryManagmentSystem

select * from Agency order by 1
select * from IBrand order by 1
select * from IStatus order by 1
select * from Items order by 1
select * from IType order by 1
select * from LLocation order by 1
select * from Stores order by 1
select * from VUsers order by 1

select * from AgencyUserMapping order by 1
select * from ItemHistory order by 1
select * from StoreItems order by 1
select * from UsersItems order by 1
select * from IMovingItem order by 1
select * from IReturnItem order by 1

-- Initially ItemHistory Table contains all the history of the Items, but UsersItems will contain the current assosiation
-- means it will contain the current user for specific Item
--alter table TableName drop constraint FK__ItemHisto__Curre__1CBC4616
--select * from sys.objects where type_desc not like 'SYSTEM_TABLE' and type_desc not like 'INTERNAL_TABLE'
--select * from sys.objects where type_desc not in ('INTERNAL_TABLE', 'SYSTEM_TABLE','SERVICE_QUEUE')
--ALTER TABLE IMovingItem NOCHECK CONSTRAINT CurrentStatusId
--ALTER TABLE IMovingItem CHECK CONSTRAINT CurrentStatusId
--alter table IMovingItem drop constraint FK__IMovingIt__curre__41EDCAC5 

--alter table IMovingItem Add CurrentStatusId int;

--alter table IMovingItem ADD Constraint CurrentStatusId foreign key (CurrentStatusId) references IStatus (StatusID)

----------------------------------------------------------------------------------------------------------------------

--Create table Stores (StoreId int Primary key,
--StoreName nvarchar(30) NOT NULL UNIQUE,
--StoreLocationId int NOT NULL,
--StoreDetails nvarchar(150), 
--foreign key (StoreLocationId) references LLocation(LocationId))

--insert into Stores values (101, 'InitialStoreName', 1,
--'This is a dummy data, and this is the initialization store; in this field we can write any other details')

--insert into Stores values (102, '2nd InitialStoreName', 3,
--'This is a dummy data, and this is the 2nd initialization store; in this field we can write any other details')

--insert into Stores values (103, '3rd InitialStoreName', 4,
--'This is a dummy data, and this is the 3rd initialization store; in this field we can write any other details')

--insert into Stores values (000, 'Main Store (Default)', 1002,
--'This is the default store, and its located in tichnecal Support Store')

select * from Stores order by 1

--drop table Stores

----------------------------------------------------------------------------------------------------------------------

--Create table Items (ItemId int primary key identity(1,1),
--ISerialNum nvarchar(200) Unique not null,
--ItemTypeId int not null, 
--ItemBrandId int not null, 
--IStatusID int not null,
--FOREIGN KEY (ItemBrandId) REFERENCES IBrand(BrandID),
--FOREIGN KEY (IStatusID) REFERENCES IStatus(StatusID),
--Foreign key (ItemTypeId) references IType(TypeId))

--insert into Items values ('Initial serial num', 1, 0, 0)
--insert into Items values ('1st Initial serial num', 3, 0, 0)
--insert into Items values ('2nd Initial serial num', 4, 2, 102)
--insert into Items values ('3rd Initial serial num', 3, 2, 101)

--alter table Items drop constraint FK__Items__IStatusID__02FC7413
--alter table Items add constraint  itemId foreign key references IStatus(StatusId)

--delete from Items where ItemId in (2,5,9,14,1002)

--alter table UsersItems add constraint CurrentStatusId foreign key (CurrentStatusId) references IStatus (StatusID)

--alter table Items add IStore int 

--alter table Items add constraint IStore foreign key (IStore) references Stores (StoreId)

select * from Items order by 1

--drop table Items

----------------------------------------------------------------------------------------------------------------------

--Create table Agency (AgencyID int primary key,
--AgencyName nvarchar (30) unique not null)

--insert into Agency values (000, 'Initial Agency')
--insert into Agency values (101, '1st Initial Agency')
--insert into Agency values (102, '2nd Initial Agency')
--insert into Agency values (1, 'Technical Support')

select * from Agency order by 1

--drop table Agency
----------------------------------------------------------------------------------------------------------------------

--create table VUsers (UserID int primary key, 
--UserName nvarchar(40) not null,
--AgencyId int not null, 
--foreign key (AgencyId) REFERENCES Agency(AgencyID))

--insert into VUsers values (00,'initial user', 0)
--insert into VUsers values (101,'1st initial user', 101)
--insert into VUsers values (102,'2nd initial user', 102)
--insert into VUsers values (1,'Store Userser', 1)

select * from VUsers order by 1

--drop table VUsers
----------------------------------------------------------------------------------------------------------------------

--Create table IStatus (StatusID int primary key, 
--StatusName nvarchar(25) not null unique)

--insert into IStatus values (0, 'Initial Status')
--insert into IStatus values (101, '1st Initial Status')
--insert into IStatus values (102, '2nd Initial Status')

select * from IStatus order by 1

--drop table IStatus

----------------------------------------------------------------------------------------------------------------------
--create table IType (TypeId int primary key identity(1,1),
--ITypeName nvarchar(30) unique not null)

--insert into IType values ('initial type')
--insert into IType values ('1st initial type')
--insert into IType values ('3rd initial type')

select * from IType order by 1

--drop table IType
----------------------------------------------------------------------------------------------------------------------
--create table LLocation (LocationId int primary key,
--LocationName nvarchar(50) Unique not null)

--insert into LLocation values (1, 'Initial Location')
--insert into LLocation values (2, '2ndInitial Location')
--insert into LLocation values (3, '3rdInitial Location')
--insert into LLocation values (4, 'Main Locatin')

select * from llocation order by 1

--drop table LLocation

----------------------------------------------------------------------------------------------------------------------

--create table IBrand (BrandID int primary key, 
--BrandName nvarchar(40) not null unique)

--insert into IBrand values (0, 'Inintial Brand')
--insert into IBrand values (1, '2nd Initial Brand')
--insert into IBrand values (2, '3rd Inintial Brand')

select * from IBrand order by 1

--drop table IBrand

----------------------------------------------------------------------------------------------------------------------
-------------------------------------------------------Sub tables-----------------------------------------------------

--create table AgencyUserMapping (RecordId int identity(1,1) primary key, 
--AgencyId int not null,
--UserId int not null Unique,
--Foreign key (AgencyId) references Agency(AgencyID),
--Foreign key (UserId) references VUsers(UserID))

--insert into AgencyUserMapping values (0,0)
--insert into AgencyUserMapping values (101,102)
--insert into AgencyUserMapping values (102,101)

select * from AgencyUserMapping

--drop table AgencyUserMapping

----------------------------------------------------------------------------------------------------------------------

--Create table UsersItems (RecordId int identity(1,1)primary key,
--UserId int not null, 
--ItemId int not null unique,
--currentDateTime datetime not null, 
--foreign key (ItemId) references Items(Itemid),
--foreign key (UserId) references VUsers(UserID)) 
--alter table UsersItems add CurrentStatusId int default

--insert into UsersItems values (101, 2, GETDATE())
--insert into UsersItems values (102, 5, GETDATE())
--insert into UsersItems values (102, 9, GETDATE())
--insert into UsersItems values (0, 14, GETDATE())

select * from UsersItems

--drop table UsersItems

----------------------------------------------------------------------------------------------------------------------

--create table StoreItems (RecordId int identity(1,1) Primary key, 
--StoreId int not null,
--ItemId int not null unique,
--foreign key (StoreId) references Stores(StoreId),
--foreign key (ItemId) references Items(ItemId))

--insert into StoreItems values (101, 2)
--insert into StoreItems values (102,9)
--insert into StoreItems values (103,14)

select * from StoreItems order by 1

--drop table StoreItems

----------------------------------------------------------------------------------------------------------------------

--create table ItemHistory (RecordId int identity(1,1) primary key, 
--ItemId int not null unique, 
--CurrentOwnerID int not null,
--CurrentStatusID int not null, 
--CurrentDateTime DateTime not null, 
--Notes nvarchar(400),
--foreign key (ItemId) references Items(ItemId), 
--foreign key (CurrentOwnerID) references VUsers (UserID),
--foreign key (CurrentStatusID) references IStatus(StatusID))


--insert into ItemHistory values (14, 0, 0, GETDATE(), 'initial Notes')
--insert into ItemHistory values (9, 101, 0, GETDATE(), 'initial Notes')
--insert into ItemHistory values (5, 102, 0, GETDATE(), '3rd initial Notes')

select * from ItemHistory order by 1

--drop table ItemHistory

----------------------------------------------------------------------------------------------------------------------
--Create table IMovingItem (RecordId int identity(1,1) primary key,
--ItemId int not null unique,
--previousUserId int not null Default 1,
--currentUserId int not null,
--currentStatusId int not null,
--curretnDateTime datetime not null,
--notes nvarchar (400),
--foreign key (ItemId) references Items(ItemId),
--foreign key (previousUserId) references Vusers(UserId),
--foreign key (currentStatusId) references IStatus(StatusID),
--foreign key (currentUserId) references Vusers(UserId))

--insert into IMovingItem values (2,1,1,0, GETDATE(), 'This is the inital record and the owner is the Main Store')

select * from IMovingItem order by 1

--drop table IMovingItem
----------------------------Adding new table called "IReturnItem"-------------------------------------
--Tuseday 15 june 2021

--create table IReturnItem (RecordId int Identity(1,1) primary key,
--ItemId int not null,
--LastUserId int not null,
--IStatusID int not null,
--CurrentDateTime datetime not null,
--foreign key (ItemId) references Items(ItemId),
--foreign key (LastUserId) references VUsers(UserID),
--foreign key (IStatusID) references Istatus(StatusID))


select * from IReturnItem

--drop table IReturnItem

----------------------------Adding new table called "ReportingSystem"-------------------------------------
--Monday 05 july 2021

--create table ReportingSystem (RecordId int Identity(1,1) primary key,
--ItemId int,
--foreign key (ItemId) references Items(ItemId),
--UserId int,
--foreign key (UserId) references VUsers(UserId),
--AgencyId int,
--foreign key (AgencyId) references Agency(AgencyId),
--StoreId int,
--foreign key (StoreId) references Stores(StoreId),
--BrandId int,
--foreign key (BrandId) references IBrand(BrandId),
--StatusId int,
--foreign key (StatusId) references IStatus(StatusId),
--TypeId int,
--foreign key (TypeId) references IType(TypeId),
--LocationId int,
--foreign key (LocationId) references LLocation(LocationId)
--)

select * from ReportingSystem

--drop table ReportingSystem

------------------------------------------Database History--------------------------------------------
--Tuseday 01/06/2021 All the database is dropped and recreated due to some enhancments, and this is the sixth time doing th
--Monday 05/07/2021 New (Reports) table will be added to the system, all the reports will be genrated from this table



