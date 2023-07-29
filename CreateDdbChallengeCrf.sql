Create database ChallengeB3

USE [challengeB3]
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

USE [challengeB3]
GO
/****** Object:  Table [dbo].[tb_Register]    Script Date: 15/05/2023 09:34:21 ******/
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
SET IDENTITY_INSERT [dbo].[tb_Register] ON 
GO
INSERT [dbo].[tb_Register] ([RegisterId], [Description], [Status], [Date]) VALUES (24, N'PS VITA OLED', N'Em conserto', CAST(N'2023-05-12T03:00:00.000' AS DateTime))
GO
INSERT [dbo].[tb_Register] ([RegisterId], [Description], [Status], [Date]) VALUES (2, N'Sega Saturn 3', N'Em conserto', CAST(N'2023-02-16T03:00:00.000' AS DateTime))
GO
INSERT [dbo].[tb_Register] ([RegisterId], [Description], [Status], [Date]) VALUES (3, N'Xbox Series X', N'Reparado', CAST(N'2023-01-13T03:00:00.000' AS DateTime))
GO
INSERT [dbo].[tb_Register] ([RegisterId], [Description], [Status], [Date]) VALUES (17, N'Xbox One S', N'Reparado', CAST(N'2023-05-14T03:00:00.000' AS DateTime))
GO
INSERT [dbo].[tb_Register] ([RegisterId], [Description], [Status], [Date]) VALUES (19, N'NIntendo Switch Pro', N'Com Defeito', CAST(N'2023-05-15T03:00:00.000' AS DateTime))
GO
INSERT [dbo].[tb_Register] ([RegisterId], [Description], [Status], [Date]) VALUES (22, N'Atari 2600', N'Reparado', CAST(N'2023-05-05T03:00:00.000' AS DateTime))
GO
INSERT [dbo].[tb_Register] ([RegisterId], [Description], [Status], [Date]) VALUES (7, N'Playstation 4 Slim', N'Reparado', CAST(N'2023-01-10T03:00:00.000' AS DateTime))
GO
INSERT [dbo].[tb_Register] ([RegisterId], [Description], [Status], [Date]) VALUES (14, N'Playstation One', N'Reparado', CAST(N'2021-02-13T03:00:00.000' AS DateTime))
GO
INSERT [dbo].[tb_Register] ([RegisterId], [Description], [Status], [Date]) VALUES (21, N'Playstation 5 ', N'Reparado', CAST(N'2023-05-14T03:00:00.000' AS DateTime))
GO
INSERT [dbo].[tb_Register] ([RegisterId], [Description], [Status], [Date]) VALUES (23, N'Xbox One S', N'Em conserto', CAST(N'2023-04-04T03:00:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[tb_Register] OFF
GO
