/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [Id]
      ,[FullName]
      ,[Email]
      ,[Phone]
      ,[Address]
  FROM [ContactifyDb].[dbo].[Contacts]