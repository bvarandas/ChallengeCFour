USE [challengeB3]
GO

/****** Object:  Table [dbo].[tb_Register]    Script Date: 13/05/2023 11:04:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tb_Register](
	[RegisterId] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](100) NULL,
	[Status] [varchar](100) NULL,
	[Date] [datetime] NULL
) ON [PRIMARY]
GO

