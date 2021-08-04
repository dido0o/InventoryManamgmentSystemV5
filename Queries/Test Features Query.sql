/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [id]
      ,[UserID]
      ,[UserName]
      ,[Agency]
      ,[AgencyId]
  FROM [StorageManagmentDB].[dbo].[VUsers]


  Create table FetureTest (ID int Primary key, UserID int unique not null, UserName nvarchar(40) not null, AgencyID int not null,
  AgencyName nvarchar (30) not null, foreign key(UserID) references VUsers(UserID), foreign key (AgencyID) references Agency(AgencyId), 
  foreign key (AgencyName) references Agency(AgencyName))

  insert into FetureTest (AgencyName) select AgencyName from Agency
  
  select * from FetureTest

  drop table FetureTest

  select * from IStatus

  
  --0554117569
  