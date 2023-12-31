USE [Hunt]
GO
/****** Object:  Table [dbo].[Animal]    Script Date: 28.11.2023 11:13:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Animal](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](150) NOT NULL,
	[DifficultyId] [int] NOT NULL,
	[Image] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Animal] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Difficulty]    Script Date: 28.11.2023 11:13:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Difficulty](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK_Difficulty] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Gender]    Script Date: 28.11.2023 11:13:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gender](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_Gender] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[House]    Script Date: 28.11.2023 11:13:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[House](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Cost] [decimal](18, 0) NOT NULL,
	[Description] [nvarchar](150) NOT NULL,
	[Image] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_House] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Hunting]    Script Date: 28.11.2023 11:13:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hunting](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[AnimalId] [int] NOT NULL,
	[Cost] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_Hunting] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HuntingReport]    Script Date: 28.11.2023 11:13:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HuntingReport](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[HouseId] [int] NOT NULL,
	[HuntingId] [int] NOT NULL,
	[ClientId] [int] NOT NULL,
 CONSTRAINT [PK_HuntingReport] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 28.11.2023 11:13:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 28.11.2023 11:13:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[Surname] [nvarchar](20) NOT NULL,
	[Patronymic] [nvarchar](20) NULL,
	[Birthday] [date] NOT NULL,
	[RoleId] [int] NOT NULL,
	[GenderId] [int] NOT NULL,
	[Email] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Login] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Animal] ON 

INSERT [dbo].[Animal] ([id], [Name], [Description], [DifficultyId], [Image]) VALUES (2, N'Олень', N'Это олень', 1, N'1.jpg')
SET IDENTITY_INSERT [dbo].[Animal] OFF
GO
SET IDENTITY_INSERT [dbo].[Difficulty] ON 

INSERT [dbo].[Difficulty] ([id], [Name], [Description]) VALUES (1, N'Очень сложно', N'есть шанс умереть')
SET IDENTITY_INSERT [dbo].[Difficulty] OFF
GO
SET IDENTITY_INSERT [dbo].[Gender] ON 

INSERT [dbo].[Gender] ([id], [Name]) VALUES (1, N'м')
INSERT [dbo].[Gender] ([id], [Name]) VALUES (2, N'ж')
SET IDENTITY_INSERT [dbo].[Gender] OFF
GO
SET IDENTITY_INSERT [dbo].[House] ON 

INSERT [dbo].[House] ([id], [Name], [Cost], [Description], [Image]) VALUES (1, N'Двушка', CAST(1200 AS Decimal(18, 0)), N'Две комнаты', N'1.jpg')
SET IDENTITY_INSERT [dbo].[House] OFF
GO
SET IDENTITY_INSERT [dbo].[Hunting] ON 

INSERT [dbo].[Hunting] ([id], [Name], [AnimalId], [Cost]) VALUES (2, N'Охота на лося', 2, CAST(3000 AS Decimal(18, 0)))
SET IDENTITY_INSERT [dbo].[Hunting] OFF
GO
SET IDENTITY_INSERT [dbo].[HuntingReport] ON 

INSERT [dbo].[HuntingReport] ([id], [HouseId], [HuntingId], [ClientId]) VALUES (7, 1, 2, 2)
SET IDENTITY_INSERT [dbo].[HuntingReport] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([id], [Name]) VALUES (1, N'Модератор')
INSERT [dbo].[Role] ([id], [Name]) VALUES (2, N'Клиент')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([id], [Name], [Surname], [Patronymic], [Birthday], [RoleId], [GenderId], [Email], [Password], [Login]) VALUES (2, N'Паша', N'Молодец', NULL, CAST(N'2003-02-09' AS Date), 2, 2, N'mail@mail.ru', N'ased', N'ased')
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[Animal]  WITH CHECK ADD  CONSTRAINT [FK_Animal_Difficulty] FOREIGN KEY([DifficultyId])
REFERENCES [dbo].[Difficulty] ([id])
GO
ALTER TABLE [dbo].[Animal] CHECK CONSTRAINT [FK_Animal_Difficulty]
GO
ALTER TABLE [dbo].[Hunting]  WITH CHECK ADD  CONSTRAINT [FK_Hunting_Animal] FOREIGN KEY([AnimalId])
REFERENCES [dbo].[Animal] ([id])
GO
ALTER TABLE [dbo].[Hunting] CHECK CONSTRAINT [FK_Hunting_Animal]
GO
ALTER TABLE [dbo].[HuntingReport]  WITH CHECK ADD  CONSTRAINT [FK_HuntingReport_House] FOREIGN KEY([HouseId])
REFERENCES [dbo].[House] ([id])
GO
ALTER TABLE [dbo].[HuntingReport] CHECK CONSTRAINT [FK_HuntingReport_House]
GO
ALTER TABLE [dbo].[HuntingReport]  WITH CHECK ADD  CONSTRAINT [FK_HuntingReport_Hunting] FOREIGN KEY([HuntingId])
REFERENCES [dbo].[Hunting] ([id])
GO
ALTER TABLE [dbo].[HuntingReport] CHECK CONSTRAINT [FK_HuntingReport_Hunting]
GO
ALTER TABLE [dbo].[HuntingReport]  WITH CHECK ADD  CONSTRAINT [FK_HuntingReport_User] FOREIGN KEY([ClientId])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[HuntingReport] CHECK CONSTRAINT [FK_HuntingReport_User]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Gender] FOREIGN KEY([GenderId])
REFERENCES [dbo].[Gender] ([id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Gender]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO
