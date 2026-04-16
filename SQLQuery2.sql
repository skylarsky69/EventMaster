USE [db_ac8236_eventmasterdb]
GO

IF OBJECT_ID(N'[dbo].[FK_Tickets_Orders_OrderId]', 'F') IS NOT NULL ALTER TABLE [dbo].[Tickets] DROP CONSTRAINT [FK_Tickets_Orders_OrderId]
GO
IF OBJECT_ID(N'[dbo].[FK_Tickets_Events_EventId]', 'F') IS NOT NULL ALTER TABLE [dbo].[Tickets] DROP CONSTRAINT [FK_Tickets_Events_EventId]
GO
IF OBJECT_ID(N'[dbo].[FK_Orders_AspNetUsers_UserId]', 'F') IS NOT NULL ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK_Orders_AspNetUsers_UserId]
GO
IF OBJECT_ID(N'[dbo].[FK_Events_Venues_VenueId]', 'F') IS NOT NULL ALTER TABLE [dbo].[Events] DROP CONSTRAINT [FK_Events_Venues_VenueId]
GO
IF OBJECT_ID(N'[dbo].[FK_Events_Categories_CategoryId]', 'F') IS NOT NULL ALTER TABLE [dbo].[Events] DROP CONSTRAINT [FK_Events_Categories_CategoryId]
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetUserTokens_AspNetUsers_UserId]', 'F') IS NOT NULL ALTER TABLE [dbo].[AspNetUserTokens] DROP CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetUserRoles_AspNetUsers_UserId]', 'F') IS NOT NULL ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetUserRoles_AspNetRoles_RoleId]', 'F') IS NOT NULL ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetUserLogins_AspNetUsers_UserId]', 'F') IS NOT NULL ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetUserClaims_AspNetUsers_UserId]', 'F') IS NOT NULL ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetRoleClaims_AspNetRoles_RoleId]', 'F') IS NOT NULL ALTER TABLE [dbo].[AspNetRoleClaims] DROP CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Venues]') AND type in (N'U')) DROP TABLE [dbo].[Venues]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tickets]') AND type in (N'U')) DROP TABLE [dbo].[Tickets]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Orders]') AND type in (N'U')) DROP TABLE [dbo].[Orders]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Events]') AND type in (N'U')) DROP TABLE [dbo].[Events]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContactMessages]') AND type in (N'U')) DROP TABLE [dbo].[ContactMessages]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Categories]') AND type in (N'U')) DROP TABLE [dbo].[Categories]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserTokens]') AND type in (N'U')) DROP TABLE [dbo].[AspNetUserTokens]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUsers]') AND type in (N'U')) DROP TABLE [dbo].[AspNetUsers]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]') AND type in (N'U')) DROP TABLE [dbo].[AspNetUserRoles]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]') AND type in (N'U')) DROP TABLE [dbo].[AspNetUserLogins]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserClaims]') AND type in (N'U')) DROP TABLE [dbo].[AspNetUserClaims]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetRoles]') AND type in (N'U')) DROP TABLE [dbo].[AspNetRoles]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetRoleClaims]') AND type in (N'U')) DROP TABLE [dbo].[AspNetRoleClaims]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[__EFMigrationsHistory]') AND type in (N'U')) DROP TABLE [dbo].[__EFMigrationsHistory]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL DEFAULT N'',
	[LastName] [nvarchar](50) NOT NULL DEFAULT N'',
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactMessages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Subject] [nvarchar](100) NOT NULL,
	[Message] [nvarchar](2000) NOT NULL,
	[SentOn] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ContactMessages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Events](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](1000) NOT NULL,
	[StartDate] [datetime2](7) NOT NULL,
	[ImageUrl] [nvarchar](max) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[VenueId] [int] NOT NULL,
 CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderDate] [datetime2](7) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tickets](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[EventId] [int] NOT NULL,
	[OrderId] [int] NULL,
 CONSTRAINT [PK_Tickets] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Venues](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Address] [nvarchar](200) NOT NULL,
	[Capacity] [int] NOT NULL,
 CONSTRAINT [PK_Venues] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'00000000000000_CreateIdentitySchema', N'8.0.24')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20260328222931_InitialCreate', N'8.0.24')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20260328233903_AddContactMessages', N'8.0.24')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20260403200734_FixedSeedingData', N'8.0.24')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'1c758d31-2138-4f46-afab-46c3a9292564', N'User', N'USER', NULL)
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'24900f88-4456-48ba-b688-4ae2fb86c77a', N'Administrator', N'ADMINISTRATOR', NULL)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'3ca846ad-5b26-45f5-8918-b927acf094bc', N'24900f88-4456-48ba-b688-4ae2fb86c77a')
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName]) VALUES (N'3ca846ad-5b26-45f5-8918-b927acf094bc', N'admin@eventmaster.com', N'ADMIN@EVENTMASTER.COM', N'admin@eventmaster.com', N'ADMIN@EVENTMASTER.COM', 1, N'AQAAAAIAAYagAAAAEA53JMoPahMenBJbZgbuSI8CDG9SRzZhRydxFhDGoshUSJKlbbFNNTDDs/zwFD2paQ==', N'S27ZYPY3QRKD534RM7WJTY6RVAYTLFSS', N'37749cfb-d968-42d3-8d7a-f1db0796635e', NULL, 0, 0, NULL, 1, 0, N'Главeн', N'Администратор')
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName]) VALUES (N'849503eb-c2b6-4de2-ba44-b7e55c6a16e9', N'nikolaipenev70@gmail.com', N'NIKOLAIPENEV70@GMAIL.COM', N'nikolaipenev70@gmail.com', N'NIKOLAIPENEV70@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEPX1pbq4fsfFSodZxUIbo8VG7ObbHmdKOnjozrfbTlBG+hJnFQzf/8yTiKWRTlIBag==', N'5E54TLPYYXRKEXDCYLC3474CWO3VESW3', N'c16e7453-a683-4891-935d-8cfc63fd0278', NULL, 0, 0, NULL, 1, 0, N'', N'')
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName]) VALUES (N'9107141b-3f3b-402d-9aad-7794d92a3572', N'nikolaipenev81@gmail.com', N'NIKOLAIPENEV81@GMAIL.COM', N'nikolaipenev81@gmail.com', N'NIKOLAIPENEV81@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEChi55CoqWpO1bu4TpEoK8fQnUOHkfvtZIS/lO3G8vCfEYtDeRDnTUONEpXApcV2UA==', N'G7RWCSIHMU7IL32USFI4VGFX2DMXDUOL', N'166b73e3-34d6-4d47-bc91-c996bfa3e7a2', NULL, 0, 0, NULL, 1, 0, N'', N'')
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 
GO
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (1, N'Концерт')
GO
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (2, N'Театър')
GO
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (3, N'Спорт')
GO
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (4, N'Фестивал')
GO
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (5, N'Семинар')
GO
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (101, N'Музика')
GO
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (102, N'Театър')
GO
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (103, N'Спорт')
GO
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (104, N'Фестивали')
GO
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (106, N'Комедия')
GO
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (107, N'Изложба')
GO
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Events] ON 
GO
INSERT [dbo].[Events] ([Id], [Title], [Description], [StartDate], [ImageUrl], [CategoryId], [VenueId]) VALUES (3, N'E-SPORTS BALKAN ARENA', N'Най-големият гейминг турнир на Балканите! Гледайте на живо сблъсъка на титаните в CS:GO и League of Legends. Очакват ви огромни екрани, невероятна атмосфера, косплей шоу и срещи с най-известните стриймъри. Подгответе се за епично шоу!', CAST(N'2026-07-15T18:30:00.0000000' AS DateTime2), N'https://images.unsplash.com/photo-1542751371-adc38448a05e?auto=format&fit=crop&w=1200&q=80', 3, 101)
GO
INSERT [dbo].[Events] ([Id], [Title], [Description], [StartDate], [ImageUrl], [CategoryId], [VenueId]) VALUES (4, N'ЛЕБЕДОВО ЕЗЕРО - Класически Балет', N'Насладете се на грацията и красотата на класическия балет. "Лебедово езеро" оживява на сцената с впечатляваща хореография, пищни костюми и вечната музика на Чайковски, изпълнена от симфоничен оркестър на живо. Една вълшебна вечер за ценителите на изкуството.', CAST(N'2026-10-19T19:30:00.0000000' AS DateTime2), N'https://images.unsplash.com/photo-1508807526345-15e9b5f4eaff?auto=format&fit=crop&w=1200&q=80', 102, 102)
GO
INSERT [dbo].[Events] ([Id], [Title], [Description], [StartDate], [ImageUrl], [CategoryId], [VenueId]) VALUES (5, N'Епичен Рок Фестивал 2026', N'Най-големите рок банди на Балканите се събират за една незабравима вечер, изпълнена с мощни китарни рифове и невероятна енергия. Не пропускайте събитието на годината с над 10 часа музика на живо!', CAST(N'2027-04-15T20:00:00.0000000' AS DateTime2), N'https://images.unsplash.com/photo-1459749411175-04bf5292ceea?q=80&w=1000&auto=format&fit=crop', 101, 104)
GO
INSERT [dbo].[Events] ([Id], [Title], [Description], [StartDate], [ImageUrl], [CategoryId], [VenueId]) VALUES (6, N'Виенска Филхармония под звездите', N'Насладете се на вечните класики на Моцарт и Бетховен, изпълнени от световноизвестната Виенска филхармония в уникална атмосфера на открито. Едно елегантно преживяване за ценителите на изкуството.', CAST(N'2026-07-30T19:30:00.0000000' AS DateTime2), N'https://images.unsplash.com/photo-1465847899084-d164df4dedc6?q=80&w=1000&auto=format&fit=crop', 101, 105)
GO
INSERT [dbo].[Events] ([Id], [Title], [Description], [StartDate], [ImageUrl], [CategoryId], [VenueId]) VALUES (7, N'Стендъп Комеди Спешъл', N'Пригответе се за вечер, изпълнена със смях до сълзи! Най-добрите комедианти от страната се събират на една сцена, за да ви представят най-новите си и абсолютно нецензурирани шеги.', CAST(N'2026-10-16T18:30:00.0000000' AS DateTime2), N'https://images.unsplash.com/photo-1585699324551-f6c309eedeca?q=80&w=1000&auto=format&fit=crop', 106, 102)
GO
INSERT [dbo].[Events] ([Id], [Title], [Description], [StartDate], [ImageUrl], [CategoryId], [VenueId]) VALUES (8, N'Future Tech Summit 2026', N'Най-иновативните умове в сферата на изкуствения интелект и блокчейн технологиите споделят своите визии за бъдещето. Включва нетуъркинг сесии, уъркшопи и ексклузивни демонстрации на живо.', CAST(N'2026-12-17T19:00:00.0000000' AS DateTime2), N'https://images.unsplash.com/photo-1540575467063-178a50c2df87?q=80&w=1000&auto=format&fit=crop', 5, 101)
GO
INSERT [dbo].[Events] ([Id], [Title], [Description], [StartDate], [ImageUrl], [CategoryId], [VenueId]) VALUES (9, N'Джаз Вечер: Магията на Саксофона', N'Потопете се в дълбоките и чувствени ритми на съвременния джаз. Една интимна вечер с авторска музика, специални коктейли и невероятна акустика, която ще ви пренесе в друго измерение.', CAST(N'2026-05-30T20:30:00.0000000' AS DateTime2), N'https://images.unsplash.com/photo-1511192336575-5a79af67a629?q=80&w=1000&auto=format&fit=crop', 101, 101)
GO
INSERT [dbo].[Events] ([Id], [Title], [Description], [StartDate], [ImageUrl], [CategoryId], [VenueId]) VALUES (10, N'MMA Fight Night: Битката на Титаните', N'Зрелищни двубои, адреналин и безкомпромисни битки в октагона. Най-очакваното бойно събитие за годината с участието на международни шампиони в тежка категория.', CAST(N'2027-01-21T20:30:00.0000000' AS DateTime2), N'https://images.unsplash.com/photo-1599566150163-29194dcaad36?q=80&w=1000&auto=format&fit=crop', 3, 101)
GO
INSERT [dbo].[Events] ([Id], [Title], [Description], [StartDate], [ImageUrl], [CategoryId], [VenueId]) VALUES (11, N'Изложба "Неонови Сънища"', N'Интерактивна визуална изложба, която съчетава модерно изкуство, светлинни инсталации и добавена реалност. Едно истинско пътешествие за сетивата, което променя представата за реалност.', CAST(N'2026-06-12T17:30:00.0000000' AS DateTime2), N'https://images.unsplash.com/photo-1508344928928-7165b67de128?q=80&w=1000&auto=format&fit=crop', 107, 104)
GO
INSERT [dbo].[Events] ([Id], [Title], [Description], [StartDate], [ImageUrl], [CategoryId], [VenueId]) VALUES (12, N'Midnight Neon Rave', N'Топ световни DJ-и, масивно лазерно шоу и хиляди фенове, танцуващи до зори. Подгответе се за най-мощното EDM парти на сезона, което ще взриви залата!', CAST(N'2026-08-15T20:40:00.0000000' AS DateTime2), N'https://images.unsplash.com/photo-1516450360452-9312f5e86fc7?q=80&w=1000&auto=format&fit=crop', 101, 101)
GO
SET IDENTITY_INSERT [dbo].[Events] OFF
GO
SET IDENTITY_INSERT [dbo].[Venues] ON 
GO
INSERT [dbo].[Venues] ([Id], [Name], [Address], [Capacity]) VALUES (101, N'Арена София', N'бул. Асен Йорданов 1', 1000)
GO
INSERT [dbo].[Venues] ([Id], [Name], [Address], [Capacity]) VALUES (102, N'Народен Театър', N'ул. Дякон Игнатий 5', 0)
GO
INSERT [dbo].[Venues] ([Id], [Name], [Address], [Capacity]) VALUES (104, N'Фестивален и конгресен център - Варна', N'бул. Сливница №2, 9000 ВАРНА', 500)
GO
INSERT [dbo].[Venues] ([Id], [Name], [Address], [Capacity]) VALUES (105, N'Спортна зала "Васил Левски" ПАЗАРДЖИК', N' пл. "В. Левски" № 5', 300)
GO
SET IDENTITY_INSERT [dbo].[Venues] OFF
GO

CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims] ([RoleId] ASC)
GO
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles] ([NormalizedName] ASC) WHERE ([NormalizedName] IS NOT NULL)
GO
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims] ([UserId] ASC)
GO
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins] ([UserId] ASC)
GO
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles] ([RoleId] ASC)
GO
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers] ([NormalizedEmail] ASC)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers] ([NormalizedUserName] ASC) WHERE ([NormalizedUserName] IS NOT NULL)
GO
CREATE NONCLUSTERED INDEX [IX_Events_CategoryId] ON [dbo].[Events] ([CategoryId] ASC)
GO
CREATE NONCLUSTERED INDEX [IX_Events_VenueId] ON [dbo].[Events] ([VenueId] ASC)
GO
CREATE NONCLUSTERED INDEX [IX_Orders_UserId] ON [dbo].[Orders] ([UserId] ASC)
GO
CREATE NONCLUSTERED INDEX [IX_Tickets_EventId] ON [dbo].[Tickets] ([EventId] ASC)
GO
CREATE NONCLUSTERED INDEX [IX_Tickets_OrderId] ON [dbo].[Tickets] ([OrderId] ASC)
GO

ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Events]  WITH CHECK ADD  CONSTRAINT [FK_Events_Categories_CategoryId] FOREIGN KEY([CategoryId]) REFERENCES [dbo].[Categories] ([Id]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Events] CHECK CONSTRAINT [FK_Events_Categories_CategoryId]
GO
ALTER TABLE [dbo].[Events]  WITH CHECK ADD  CONSTRAINT [FK_Events_Venues_VenueId] FOREIGN KEY([VenueId]) REFERENCES [dbo].[Venues] ([Id]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Events] CHECK CONSTRAINT [FK_Events_Venues_VenueId]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_AspNetUsers_UserId] FOREIGN KEY([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD  CONSTRAINT [FK_Tickets_Events_EventId] FOREIGN KEY([EventId]) REFERENCES [dbo].[Events] ([Id]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Tickets] CHECK CONSTRAINT [FK_Tickets_Events_EventId]
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD  CONSTRAINT [FK_Tickets_Orders_OrderId] FOREIGN KEY([OrderId]) REFERENCES [dbo].[Orders] ([Id])
GO
ALTER TABLE [dbo].[Tickets] CHECK CONSTRAINT [FK_Tickets_Orders_OrderId]
GO