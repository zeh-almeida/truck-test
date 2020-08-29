USE [master]
GO

IF DB_ID('project-database') IS NOT NULL
  SET NOEXEC ON
  CREATE DATABASE [project-database];
GO