/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [id]
      ,[AgencyId]
      ,[AgencyName]
      ,[UserId]
      ,[UserName]
  FROM [StorageManagmentDB].[dbo].[AgencyUserMapping]

  insert into AgencyUserMapping values (0,'initial Agency', 0, 'intial user')
  select * from Agency
  select * from VUsers
  select * from AgencyUserMapping
  delete  from AgencyUserMapping where UserId = 0

  select * from Items
  select * from VUsers

  Create table UserItems (id int identity(1,1), ISerialNum nvarchar(200) not null unique, ItemName nvarchar (50) not null,
  UserId int not null, UserName nvarchar (40) not null, foreign key (ISerialNum) references Items(ISerialNum),
  foreign key (UserId) references VUsers(UserID))


  select * from UserItems

  insert into UserItems values ('Initial serial num', 'initial Item', 0, 'Initial username')
  
  insert into UserItems values ('1Initial serial num', 'initial Item', 0, 'Initial username')

    drop table UserItems

	
select * from Stores
select * from Items

  create table StoreItems (id int not null identity(1,1), StoreId int not null, StoreName nvarchar(30) not null,
  ISerialNum nvarchar(200) not null unique, ItemName nvarchar(50) not null, foreign key (StoreId) references Stores(StoreId),
  foreign key (ISerialNum) references Items(ISerialNum))

  select * from StoreItems

  insert into StoreItems values (101,'InitialStore', 'Initial serial num', 'Initial Item')
  
  insert into StoreItems values (102,'2ndInitialStore', '1Initial serial num', '1Initial Item')