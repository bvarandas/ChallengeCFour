/*
USE [master];
GO

IF NOT EXISTS (SELECT * FROM sys.sql_logins WHERE name = 'newuser')
BEGIN
    CREATE LOGIN [newuser] WITH PASSWORD = 'Mssql2@19', CHECK_POLICY = OFF;
    ALTER SERVER ROLE [sysadmin] ADD MEMBER [newuser];
END
GO
*/
Create database ChallengeCrf
GO
USE [ChallengeCrf]
GO

CREATE TABLE [dbo].[StoredEvent](
	[Id] [uniqueidentifier] NOT NULL,
	[AggregateId] [int] NOT NULL,
	[Data] [nvarchar](max) NULL,
	[Action] [varchar](100) NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[User] [nvarchar](max) NULL,
 CONSTRAINT [PK_StoredEvent] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

USE [challengeCrf]
GO

/****** Object:  Table [dbo].[tb_CashFlow]    Script Date: 30/07/2023 12:07:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tb_CashFlow](
	[CashFlowId] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](100) NOT NULL,
	[Amount] [float] NOT NULL,
	[Entry] [char](1) NOT NULL,
	[Date] [datetime] NOT NULL
) ON [PRIMARY]
GO



