USE [master]
GO
/****** Object:  Database [QLSVTT]    Script Date: 4/24/2024 7:13:36 PM ******/
CREATE DATABASE [QLSVTT]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QLSVTT', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\QLSVTT.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'QLSVTT_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\QLSVTT_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [QLSVTT] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QLSVTT].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QLSVTT] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QLSVTT] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QLSVTT] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QLSVTT] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QLSVTT] SET ARITHABORT OFF 
GO
ALTER DATABASE [QLSVTT] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [QLSVTT] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QLSVTT] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QLSVTT] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QLSVTT] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QLSVTT] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QLSVTT] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QLSVTT] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QLSVTT] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QLSVTT] SET  ENABLE_BROKER 
GO
ALTER DATABASE [QLSVTT] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QLSVTT] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QLSVTT] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QLSVTT] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QLSVTT] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QLSVTT] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QLSVTT] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QLSVTT] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [QLSVTT] SET  MULTI_USER 
GO
ALTER DATABASE [QLSVTT] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QLSVTT] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QLSVTT] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QLSVTT] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [QLSVTT] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [QLSVTT] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [QLSVTT] SET QUERY_STORE = ON
GO
ALTER DATABASE [QLSVTT] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [QLSVTT]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 4/24/2024 7:13:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[AccountID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NULL,
	[Password] [nvarchar](max) NULL,
	[RoleID] [int] NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK__TaiKhoan__3214EC2738ACB4DD] PRIMARY KEY CLUSTERED 
(
	[AccountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 4/24/2024 7:13:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[EmployeeID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Email] [nvarchar](30) NULL,
	[PhoneNumber] [nvarchar](11) NULL,
	[Address] [nvarchar](50) NULL,
	[CompanyName] [nvarchar](50) NULL,
	[CompanyAddress] [nvarchar](max) NULL,
	[Note] [nvarchar](max) NULL,
 CONSTRAINT [PK_EmployeeID] PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Internship]    Script Date: 4/24/2024 7:13:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Internship](
	[InternShipID] [int] IDENTITY(1,1) NOT NULL,
	[StudentID] [int] NULL,
	[EmployeeID] [int] NULL,
	[TeacherID] [int] NULL,
	[Start_Day] [date] NULL,
	[End_Day] [date] NULL,
 CONSTRAINT [PK__ThucTap__3214EC2771FF90AF] PRIMARY KEY CLUSTERED 
(
	[InternShipID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 4/24/2024 7:13:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[IDRole] [int] NOT NULL,
	[Role] [nvarchar](50) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[IDRole] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Score]    Script Date: 4/24/2024 7:13:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Score](
	[ScoreID] [int] IDENTITY(1,1) NOT NULL,
	[Score1] [decimal](18, 2) NULL,
	[Score2] [decimal](18, 2) NULL,
	[Score3] [decimal](18, 2) NULL,
	[Score4] [decimal](18, 2) NULL,
	[Score5] [decimal](18, 2) NULL,
	[Assessment] [nvarchar](max) NULL,
	[TopicID] [int] NULL,
 CONSTRAINT [PK__Diem__3214EC27CAFB2D41] PRIMARY KEY CLUSTERED 
(
	[ScoreID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 4/24/2024 7:13:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[StudentID] [int] IDENTITY(1,1) NOT NULL,
	[StudentCode] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[FirstName] [nvarchar](50) NULL,
	[Gender] [nvarchar](10) NULL,
	[DateOfBirth] [date] NULL,
	[Email] [nvarchar](50) NULL,
	[PhoneNumber] [nvarchar](11) NULL,
	[Classroom] [nvarchar](30) NULL,
	[GPAScore] [decimal](18, 2) NULL,
	[LetterScore] [nvarchar](5) NULL,
	[Address] [nvarchar](100) NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK__SinhVien__3214EC27B1D3DDDC] PRIMARY KEY CLUSTERED 
(
	[StudentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teacher]    Script Date: 4/24/2024 7:13:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teacher](
	[TeacherID] [int] IDENTITY(1,1) NOT NULL,
	[LastName] [nvarchar](50) NULL,
	[FirstName] [nvarchar](50) NULL,
	[Address] [nvarchar](100) NULL,
	[Gender] [nvarchar](10) NULL,
	[DateOfBirth] [date] NULL,
	[Email] [nvarchar](50) NULL,
	[PhoneNumber] [nvarchar](11) NULL,
	[Office] [nvarchar](50) NULL,
 CONSTRAINT [PK__GiaoVien__3214EC27B141F025] PRIMARY KEY CLUSTERED 
(
	[TeacherID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Topic]    Script Date: 4/24/2024 7:13:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Topic](
	[TopicID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[StudentID] [int] NULL,
	[EmployeeID] [int] NULL,
 CONSTRAINT [PK__DeTai__3214EC275488090C] PRIMARY KEY CLUSTERED 
(
	[TopicID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([AccountID], [Username], [Password], [RoleID], [Status]) VALUES (1, N'leduc', N'481f6cc0511143ccdd7e2d1b1b94faf0a700a8b49cd13922a70b5ae28acaa8c5', 1, 1)
INSERT [dbo].[Account] ([AccountID], [Username], [Password], [RoleID], [Status]) VALUES (2, N'20103100393', N'03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4', 4, 1)
INSERT [dbo].[Account] ([AccountID], [Username], [Password], [RoleID], [Status]) VALUES (4, N'trungmm@gmail.com', N'6b86b273ff34fce19d6b804eff5a3f5747ada4eaa22f1d49c01e52ddb7875b4b', 2, 1)
INSERT [dbo].[Account] ([AccountID], [Username], [Password], [RoleID], [Status]) VALUES (5, N'son@gmail.com', N'6b86b273ff34fce19d6b804eff5a3f5747ada4eaa22f1d49c01e52ddb7875b4b', 3, 1)
INSERT [dbo].[Account] ([AccountID], [Username], [Password], [RoleID], [Status]) VALUES (6, N'20103100308', N'6b86b273ff34fce19d6b804eff5a3f5747ada4eaa22f1d49c01e52ddb7875b4b', 4, 1)
SET IDENTITY_INSERT [dbo].[Account] OFF
GO
SET IDENTITY_INSERT [dbo].[Employee] ON 

INSERT [dbo].[Employee] ([EmployeeID], [Name], [Email], [PhoneNumber], [Address], [CompanyName], [CompanyAddress], [Note]) VALUES (1, N'Nguyễn Văn Khương', N'khuong@gmail.com', N'0654487244', N'Huyện Vụ Bản - Nam Định', N'VNPT HNI', N'Số 75 A/B Đinh Tiên Hoàng, Phường Tràng Tiền, quận Hoàn Kiếm, Hà Nội', N'không có')
INSERT [dbo].[Employee] ([EmployeeID], [Name], [Email], [PhoneNumber], [Address], [CompanyName], [CompanyAddress], [Note]) VALUES (2, N'Huỳnh Công Sơn', N'son@gmail.com', N'0865521285', N'Huyện Kinh Môn - Hải Dương', N'VNPT Technology', N'Số 165 Cầu Giấy - Quan Hoa - Cầu Giấy - Hà Nội', N'không có')
INSERT [dbo].[Employee] ([EmployeeID], [Name], [Email], [PhoneNumber], [Address], [CompanyName], [CompanyAddress], [Note]) VALUES (3, N'Đỗ Quốc Vinh', N'vinh@gmail.com', N'0911007279', N'Bắc Giang', N'VNPT Bắc Giang', N'Bắc Giang', N'không có')
INSERT [dbo].[Employee] ([EmployeeID], [Name], [Email], [PhoneNumber], [Address], [CompanyName], [CompanyAddress], [Note]) VALUES (4, N'Nguyễn Thu Giang', N'giang@gmail.com', N'0912925567', N'Hưng Yên', N'VNPT Hưng Yên', N'Hưng Yên', N'không có')
INSERT [dbo].[Employee] ([EmployeeID], [Name], [Email], [PhoneNumber], [Address], [CompanyName], [CompanyAddress], [Note]) VALUES (5, N'Nguyễn Đức Cường', N'cuong@gmail.com', N'0912866016', N'Thái Nguyên', N'VNPT Thái Nguyên', N'Thái Nguyên', N'không có')
INSERT [dbo].[Employee] ([EmployeeID], [Name], [Email], [PhoneNumber], [Address], [CompanyName], [CompanyAddress], [Note]) VALUES (6, N'Ngô Xuân Trang', N'trang@gmail.com', N'0915134568', N'Điện Biên', N'VNPT Điện Biên', N'Điện Biên', N'không có')
INSERT [dbo].[Employee] ([EmployeeID], [Name], [Email], [PhoneNumber], [Address], [CompanyName], [CompanyAddress], [Note]) VALUES (7, N'Phạm Thúy Nga', N'nga@gmail.com', N'0912900579', N'Hải Dương', N'VNPT Hải Dương', N'Hải Dương', N'không có')
INSERT [dbo].[Employee] ([EmployeeID], [Name], [Email], [PhoneNumber], [Address], [CompanyName], [CompanyAddress], [Note]) VALUES (8, N'Tô Thị Giang Cảnh', N'canh@gmail.com', N'091 2638638', N'Quảng Ninh', N'VNPT Quảng Ninh', N'Quảng Ninh', N'không có')
INSERT [dbo].[Employee] ([EmployeeID], [Name], [Email], [PhoneNumber], [Address], [CompanyName], [CompanyAddress], [Note]) VALUES (9, N'Vũ Đức Long', N'long@gmail.com', N'091 2808893', N'Hà Giang', N'VNPT Hà Giang', N'Hà Giang', N'không có')
INSERT [dbo].[Employee] ([EmployeeID], [Name], [Email], [PhoneNumber], [Address], [CompanyName], [CompanyAddress], [Note]) VALUES (10, N'Nguyễn Thanh Toàn', N'toan@gmail.com', N'0912000858', N'Yên Bái', N'VNPT Yên Bái', N'Yên Bái', N'không có')
SET IDENTITY_INSERT [dbo].[Employee] OFF
GO
SET IDENTITY_INSERT [dbo].[Internship] ON 

INSERT [dbo].[Internship] ([InternShipID], [StudentID], [EmployeeID], [TeacherID], [Start_Day], [End_Day]) VALUES (1, 4, 2, 2, CAST(N'2023-02-23' AS Date), CAST(N'2023-03-24' AS Date))
INSERT [dbo].[Internship] ([InternShipID], [StudentID], [EmployeeID], [TeacherID], [Start_Day], [End_Day]) VALUES (2, 1, 1, 1, CAST(N'2023-02-23' AS Date), CAST(N'2023-03-25' AS Date))
INSERT [dbo].[Internship] ([InternShipID], [StudentID], [EmployeeID], [TeacherID], [Start_Day], [End_Day]) VALUES (3, 4, 2, 2, CAST(N'2023-02-23' AS Date), CAST(N'2023-03-24' AS Date))
INSERT [dbo].[Internship] ([InternShipID], [StudentID], [EmployeeID], [TeacherID], [Start_Day], [End_Day]) VALUES (4, 1, 1, 1, CAST(N'2023-02-23' AS Date), CAST(N'2023-03-25' AS Date))
INSERT [dbo].[Internship] ([InternShipID], [StudentID], [EmployeeID], [TeacherID], [Start_Day], [End_Day]) VALUES (5, 5, 1, 3, CAST(N'2023-02-23' AS Date), CAST(N'2023-03-24' AS Date))
INSERT [dbo].[Internship] ([InternShipID], [StudentID], [EmployeeID], [TeacherID], [Start_Day], [End_Day]) VALUES (6, 10, 2, 4, CAST(N'2023-02-23' AS Date), CAST(N'2023-03-25' AS Date))
INSERT [dbo].[Internship] ([InternShipID], [StudentID], [EmployeeID], [TeacherID], [Start_Day], [End_Day]) VALUES (7, 27, 3, 5, CAST(N'2023-02-23' AS Date), CAST(N'2023-03-24' AS Date))
INSERT [dbo].[Internship] ([InternShipID], [StudentID], [EmployeeID], [TeacherID], [Start_Day], [End_Day]) VALUES (8, 28, 4, 6, CAST(N'2023-02-23' AS Date), CAST(N'2023-03-25' AS Date))
INSERT [dbo].[Internship] ([InternShipID], [StudentID], [EmployeeID], [TeacherID], [Start_Day], [End_Day]) VALUES (9, 29, 3, 7, CAST(N'2023-02-23' AS Date), CAST(N'2023-03-24' AS Date))
INSERT [dbo].[Internship] ([InternShipID], [StudentID], [EmployeeID], [TeacherID], [Start_Day], [End_Day]) VALUES (10, 31, 6, 9, CAST(N'2023-02-23' AS Date), CAST(N'2023-03-24' AS Date))
INSERT [dbo].[Internship] ([InternShipID], [StudentID], [EmployeeID], [TeacherID], [Start_Day], [End_Day]) VALUES (11, 32, 5, 10, CAST(N'2023-02-23' AS Date), CAST(N'2023-03-25' AS Date))
INSERT [dbo].[Internship] ([InternShipID], [StudentID], [EmployeeID], [TeacherID], [Start_Day], [End_Day]) VALUES (12, 33, 5, 2, CAST(N'2023-02-23' AS Date), CAST(N'2023-03-24' AS Date))
INSERT [dbo].[Internship] ([InternShipID], [StudentID], [EmployeeID], [TeacherID], [Start_Day], [End_Day]) VALUES (13, 34, 6, 1, CAST(N'2023-02-23' AS Date), CAST(N'2023-03-25' AS Date))
INSERT [dbo].[Internship] ([InternShipID], [StudentID], [EmployeeID], [TeacherID], [Start_Day], [End_Day]) VALUES (14, 35, 7, 3, CAST(N'2023-02-23' AS Date), CAST(N'2023-03-24' AS Date))
INSERT [dbo].[Internship] ([InternShipID], [StudentID], [EmployeeID], [TeacherID], [Start_Day], [End_Day]) VALUES (15, 36, 7, 4, CAST(N'2023-02-23' AS Date), CAST(N'2023-03-25' AS Date))
INSERT [dbo].[Internship] ([InternShipID], [StudentID], [EmployeeID], [TeacherID], [Start_Day], [End_Day]) VALUES (16, 39, 8, 5, CAST(N'2023-02-23' AS Date), CAST(N'2023-03-24' AS Date))
INSERT [dbo].[Internship] ([InternShipID], [StudentID], [EmployeeID], [TeacherID], [Start_Day], [End_Day]) VALUES (17, 41, 8, 6, CAST(N'2023-02-23' AS Date), CAST(N'2023-03-25' AS Date))
INSERT [dbo].[Internship] ([InternShipID], [StudentID], [EmployeeID], [TeacherID], [Start_Day], [End_Day]) VALUES (18, 43, 9, 7, CAST(N'2023-02-23' AS Date), CAST(N'2023-03-24' AS Date))
INSERT [dbo].[Internship] ([InternShipID], [StudentID], [EmployeeID], [TeacherID], [Start_Day], [End_Day]) VALUES (19, 47, 10, 9, CAST(N'2023-02-23' AS Date), CAST(N'2023-03-24' AS Date))
INSERT [dbo].[Internship] ([InternShipID], [StudentID], [EmployeeID], [TeacherID], [Start_Day], [End_Day]) VALUES (20, 49, 10, 10, CAST(N'2023-02-23' AS Date), CAST(N'2023-03-25' AS Date))
SET IDENTITY_INSERT [dbo].[Internship] OFF
GO
INSERT [dbo].[Roles] ([IDRole], [Role]) VALUES (1, N'Admin')
INSERT [dbo].[Roles] ([IDRole], [Role]) VALUES (2, N'Teacher')
INSERT [dbo].[Roles] ([IDRole], [Role]) VALUES (3, N'Employee')
INSERT [dbo].[Roles] ([IDRole], [Role]) VALUES (4, N'Student')
GO
SET IDENTITY_INSERT [dbo].[Score] ON 

INSERT [dbo].[Score] ([ScoreID], [Score1], [Score2], [Score3], [Score4], [Score5], [Assessment], [TopicID]) VALUES (1, CAST(7.00 AS Decimal(18, 2)), CAST(6.00 AS Decimal(18, 2)), CAST(7.00 AS Decimal(18, 2)), CAST(7.00 AS Decimal(18, 2)), CAST(7.00 AS Decimal(18, 2)), N'Trung Bình', 2)
INSERT [dbo].[Score] ([ScoreID], [Score1], [Score2], [Score3], [Score4], [Score5], [Assessment], [TopicID]) VALUES (2, CAST(8.00 AS Decimal(18, 2)), CAST(7.00 AS Decimal(18, 2)), CAST(6.00 AS Decimal(18, 2)), CAST(7.00 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), N'Khá', 3)
INSERT [dbo].[Score] ([ScoreID], [Score1], [Score2], [Score3], [Score4], [Score5], [Assessment], [TopicID]) VALUES (3, CAST(9.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), N'Giỏi', 4)
INSERT [dbo].[Score] ([ScoreID], [Score1], [Score2], [Score3], [Score4], [Score5], [Assessment], [TopicID]) VALUES (4, CAST(9.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), CAST(8.00 AS Decimal(18, 2)), CAST(8.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), N'Giỏi', 1)
INSERT [dbo].[Score] ([ScoreID], [Score1], [Score2], [Score3], [Score4], [Score5], [Assessment], [TopicID]) VALUES (5, CAST(7.00 AS Decimal(18, 2)), CAST(6.00 AS Decimal(18, 2)), CAST(7.00 AS Decimal(18, 2)), CAST(7.00 AS Decimal(18, 2)), CAST(7.00 AS Decimal(18, 2)), N'Trung Bình', 2)
INSERT [dbo].[Score] ([ScoreID], [Score1], [Score2], [Score3], [Score4], [Score5], [Assessment], [TopicID]) VALUES (6, CAST(8.00 AS Decimal(18, 2)), CAST(7.00 AS Decimal(18, 2)), CAST(6.00 AS Decimal(18, 2)), CAST(7.00 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), N'Khá', 3)
INSERT [dbo].[Score] ([ScoreID], [Score1], [Score2], [Score3], [Score4], [Score5], [Assessment], [TopicID]) VALUES (7, CAST(9.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), N'Giỏi', 4)
INSERT [dbo].[Score] ([ScoreID], [Score1], [Score2], [Score3], [Score4], [Score5], [Assessment], [TopicID]) VALUES (8, CAST(9.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), CAST(8.00 AS Decimal(18, 2)), CAST(8.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), N'Giỏi', 1)
INSERT [dbo].[Score] ([ScoreID], [Score1], [Score2], [Score3], [Score4], [Score5], [Assessment], [TopicID]) VALUES (9, CAST(7.00 AS Decimal(18, 2)), CAST(6.00 AS Decimal(18, 2)), CAST(7.00 AS Decimal(18, 2)), CAST(7.00 AS Decimal(18, 2)), CAST(7.00 AS Decimal(18, 2)), N'Trung Bình', 5)
INSERT [dbo].[Score] ([ScoreID], [Score1], [Score2], [Score3], [Score4], [Score5], [Assessment], [TopicID]) VALUES (10, CAST(8.00 AS Decimal(18, 2)), CAST(7.00 AS Decimal(18, 2)), CAST(6.00 AS Decimal(18, 2)), CAST(7.00 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), N'Khá', 6)
INSERT [dbo].[Score] ([ScoreID], [Score1], [Score2], [Score3], [Score4], [Score5], [Assessment], [TopicID]) VALUES (11, CAST(9.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), N'Giỏi', 7)
INSERT [dbo].[Score] ([ScoreID], [Score1], [Score2], [Score3], [Score4], [Score5], [Assessment], [TopicID]) VALUES (12, CAST(9.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), CAST(8.00 AS Decimal(18, 2)), CAST(8.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), N'Giỏi', 8)
INSERT [dbo].[Score] ([ScoreID], [Score1], [Score2], [Score3], [Score4], [Score5], [Assessment], [TopicID]) VALUES (13, CAST(7.00 AS Decimal(18, 2)), CAST(6.00 AS Decimal(18, 2)), CAST(7.00 AS Decimal(18, 2)), CAST(7.00 AS Decimal(18, 2)), CAST(7.00 AS Decimal(18, 2)), N'Trung Bình', 9)
INSERT [dbo].[Score] ([ScoreID], [Score1], [Score2], [Score3], [Score4], [Score5], [Assessment], [TopicID]) VALUES (14, CAST(8.00 AS Decimal(18, 2)), CAST(7.00 AS Decimal(18, 2)), CAST(6.00 AS Decimal(18, 2)), CAST(7.00 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), N'Khá', 10)
INSERT [dbo].[Score] ([ScoreID], [Score1], [Score2], [Score3], [Score4], [Score5], [Assessment], [TopicID]) VALUES (15, CAST(9.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), N'Giỏi', 11)
INSERT [dbo].[Score] ([ScoreID], [Score1], [Score2], [Score3], [Score4], [Score5], [Assessment], [TopicID]) VALUES (16, CAST(9.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), CAST(8.00 AS Decimal(18, 2)), CAST(8.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), N'Giỏi', 12)
INSERT [dbo].[Score] ([ScoreID], [Score1], [Score2], [Score3], [Score4], [Score5], [Assessment], [TopicID]) VALUES (17, CAST(7.00 AS Decimal(18, 2)), CAST(6.00 AS Decimal(18, 2)), CAST(7.00 AS Decimal(18, 2)), CAST(7.00 AS Decimal(18, 2)), CAST(7.00 AS Decimal(18, 2)), N'Trung Bình', 13)
INSERT [dbo].[Score] ([ScoreID], [Score1], [Score2], [Score3], [Score4], [Score5], [Assessment], [TopicID]) VALUES (18, CAST(8.00 AS Decimal(18, 2)), CAST(7.00 AS Decimal(18, 2)), CAST(6.00 AS Decimal(18, 2)), CAST(7.00 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), N'Khá', 14)
INSERT [dbo].[Score] ([ScoreID], [Score1], [Score2], [Score3], [Score4], [Score5], [Assessment], [TopicID]) VALUES (19, CAST(9.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), N'Giỏi', 15)
INSERT [dbo].[Score] ([ScoreID], [Score1], [Score2], [Score3], [Score4], [Score5], [Assessment], [TopicID]) VALUES (20, CAST(9.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), CAST(8.00 AS Decimal(18, 2)), CAST(8.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), N'Giỏi', 16)
INSERT [dbo].[Score] ([ScoreID], [Score1], [Score2], [Score3], [Score4], [Score5], [Assessment], [TopicID]) VALUES (21, CAST(7.00 AS Decimal(18, 2)), CAST(6.00 AS Decimal(18, 2)), CAST(7.00 AS Decimal(18, 2)), CAST(7.00 AS Decimal(18, 2)), CAST(7.00 AS Decimal(18, 2)), N'Trung Bình', 17)
INSERT [dbo].[Score] ([ScoreID], [Score1], [Score2], [Score3], [Score4], [Score5], [Assessment], [TopicID]) VALUES (22, CAST(8.00 AS Decimal(18, 2)), CAST(7.00 AS Decimal(18, 2)), CAST(6.00 AS Decimal(18, 2)), CAST(7.00 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), N'Khá', 18)
INSERT [dbo].[Score] ([ScoreID], [Score1], [Score2], [Score3], [Score4], [Score5], [Assessment], [TopicID]) VALUES (23, CAST(9.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), N'Giỏi', 19)
INSERT [dbo].[Score] ([ScoreID], [Score1], [Score2], [Score3], [Score4], [Score5], [Assessment], [TopicID]) VALUES (24, CAST(9.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), CAST(8.00 AS Decimal(18, 2)), CAST(8.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), N'Giỏi', 20)
SET IDENTITY_INSERT [dbo].[Score] OFF
GO
SET IDENTITY_INSERT [dbo].[Student] ON 

INSERT [dbo].[Student] ([StudentID], [StudentCode], [LastName], [FirstName], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Classroom], [GPAScore], [LetterScore], [Address], [Status]) VALUES (1, N'20103100393', N'lê anh', N'đức', N'Nam', CAST(N'2002-11-07' AS Date), N'leduc7112002@gmail.com', N'0813250204', N'DHTI14A2CL', CAST(3.53 AS Decimal(18, 2)), N'B+', N'T.p Yên Bái', 1)
INSERT [dbo].[Student] ([StudentID], [StudentCode], [LastName], [FirstName], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Classroom], [GPAScore], [LetterScore], [Address], [Status]) VALUES (2, N'20103100002', N'Nguyễn Hoài', N'Nam', N'Nam', CAST(N'2002-03-26' AS Date), N'nambg2603@gmail.com', N'0855327713', N'DHTI14A1HN', CAST(3.42 AS Decimal(18, 2)), N'B ', N'Tỉnh Bắc Giang.', 1)
INSERT [dbo].[Student] ([StudentID], [StudentCode], [LastName], [FirstName], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Classroom], [GPAScore], [LetterScore], [Address], [Status]) VALUES (3, N'20103100137', N'Nguyễn Thanh', N'Tùng', N'Nam', CAST(N'2002-04-10' AS Date), N'thanhtung02.uneti@gmail.com', N'0898253433', N'DHTI14A2HN', CAST(2.50 AS Decimal(18, 2)), N'C+', N'Tỉnh Bắc Giang.', 1)
INSERT [dbo].[Student] ([StudentID], [StudentCode], [LastName], [FirstName], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Classroom], [GPAScore], [LetterScore], [Address], [Status]) VALUES (4, N'20103100308', N'Thân Thị', N'Hà', N'Nữ', CAST(N'2002-11-04' AS Date), N'hathantth04@gmail.com
', N'0767039217', N'DHTI14A4HN', CAST(3.30 AS Decimal(18, 2)), N'B ', N'Huyện Việt Yên - Tỉnh Bắc Giang', 0)
INSERT [dbo].[Student] ([StudentID], [StudentCode], [LastName], [FirstName], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Classroom], [GPAScore], [LetterScore], [Address], [Status]) VALUES (5, N'20103100954', N'Phạm Văn', N'Phong', N'Nam', CAST(N'2002-09-18' AS Date), N'pvphong.dhti14a12hn@sv.uneti.edu.vn', N'0974507584', N'DHTI14A12HN', CAST(1.20 AS Decimal(18, 2)), N'D', N'TP Bắc Giang, Tỉnh Bắc Giang', 0)
INSERT [dbo].[Student] ([StudentID], [StudentCode], [LastName], [FirstName], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Classroom], [GPAScore], [LetterScore], [Address], [Status]) VALUES (6, N'20103100652', N'Trương Thị', N'Thuận', N'Nữ', CAST(N'2002-06-29' AS Date), N'ttthuan.dhti14a11hn@sv.uneti.edu.vn', N'0398817164', N'DHTI14A11HN', CAST(1.90 AS Decimal(18, 2)), N'D+', N'Huyện Lục Nam, Tỉnh Bắc Giang.', 1)
INSERT [dbo].[Student] ([StudentID], [StudentCode], [LastName], [FirstName], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Classroom], [GPAScore], [LetterScore], [Address], [Status]) VALUES (7, N'20103100235', N'Trần Hoàng', N'Anh', N'Nam', CAST(N'2002-10-06' AS Date), N'hoanganhtran0927@gmail.com', N'0355718846', N'DHTI14A5HN', CAST(3.19 AS Decimal(18, 2)), N'B ', N'Huyện Hiệp Hoà, Tỉnh Bắc Giang', 1)
INSERT [dbo].[Student] ([StudentID], [StudentCode], [LastName], [FirstName], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Classroom], [GPAScore], [LetterScore], [Address], [Status]) VALUES (8, N'20103100695', N'Nguyễn Văn', N'Long', N'Nam', CAST(N'2002-10-08' AS Date), N'nvlongdb@gmail.com', N'0984847610', N'DHTI14A4HN', CAST(3.80 AS Decimal(18, 2)), N'A', N'Huyện Hiệp Hoà, Tỉnh Bắc Giang.', 0)
INSERT [dbo].[Student] ([StudentID], [StudentCode], [LastName], [FirstName], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Classroom], [GPAScore], [LetterScore], [Address], [Status]) VALUES (9, N'20103100427', N'Lương Việt', N'Hoàng', N'Nam', CAST(N'2002-04-08' AS Date), N'lvhoang842002@gmail.com', N'0964952230', N'DHTI14A7HN', CAST(2.56 AS Decimal(18, 2)), N'C+', N'Huyện Việt Yên - Tỉnh Bắc Giang', 0)
INSERT [dbo].[Student] ([StudentID], [StudentCode], [LastName], [FirstName], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Classroom], [GPAScore], [LetterScore], [Address], [Status]) VALUES (10, N'20103100172', N'Trần Văn', N'Đức', N'Nam', CAST(N'2002-09-10' AS Date), N'phamhuyenlinh1407@gmail.com', N'0396955238', N'DHTI14A2HN', CAST(3.04 AS Decimal(18, 2)), N'B ', N'Huyện Lạng Giang,Tỉnh Bắc Giang.', 1)
INSERT [dbo].[Student] ([StudentID], [StudentCode], [LastName], [FirstName], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Classroom], [GPAScore], [LetterScore], [Address], [Status]) VALUES (11, N'20103100643', N'Ngô Thế', N'Thái', N'Nam', CAST(N'2002-01-20' AS Date), N'thai2012002@gmail.com', N'0964178382', N'DHTI14A10HN', CAST(2.97 AS Decimal(18, 2)), N'C+', N'Huyện Việt Yên, Tỉnh Bắc Giang.', 0)
INSERT [dbo].[Student] ([StudentID], [StudentCode], [LastName], [FirstName], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Classroom], [GPAScore], [LetterScore], [Address], [Status]) VALUES (12, N'20103100644', N'Phạm Huyền Diệu', N'Linh', N'Nữ', CAST(N'2002-07-14' AS Date), N'phamhuyenlinh1407@gmail.com', N'0396955238', N'DHTI14A4HN', CAST(2.96 AS Decimal(18, 2)), N'C+', N'huyện Yên Dũng,tỉnh Bắc Giang', 1)
INSERT [dbo].[Student] ([StudentID], [StudentCode], [LastName], [FirstName], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Classroom], [GPAScore], [LetterScore], [Address], [Status]) VALUES (13, N'20103100645', N'Thân Thị Lan', N'Anh', N'Nữ', CAST(N'2002-10-16' AS Date), N'ttlanh.dhti14a11hn@sv.uneti.edu.vn', N'0961153788', N'DHTI14A11HN', CAST(2.94 AS Decimal(18, 2)), N'C+', N'TP Bắc Giang, tỉnh Bắc Giang', 1)
INSERT [dbo].[Student] ([StudentID], [StudentCode], [LastName], [FirstName], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Classroom], [GPAScore], [LetterScore], [Address], [Status]) VALUES (14, N'20103100646', N'Lê Việt', N'Hà', N'Nam', CAST(N'2001-06-26' AS Date), N'lvha.dhti14A11HN@sv.uneti.edu.vn', N'0877336334', N'DHTI14A11HN', CAST(2.89 AS Decimal(18, 2)), N'C+', N'Huyện Tân Yên, tỉnh Bắc Giang', 0)
INSERT [dbo].[Student] ([StudentID], [StudentCode], [LastName], [FirstName], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Classroom], [GPAScore], [LetterScore], [Address], [Status]) VALUES (15, N'20103100647', N'Nguyễn Tuấn', N'Nam', N'Nam', CAST(N'2002-02-24' AS Date), N'bangbang20022003@gmail.com', N'0366013588', N'DHTI14A2HN', CAST(2.87 AS Decimal(18, 2)), N'C+', N'Huyện Tân Yên, Tỉnh Bắc Giang', 0)
INSERT [dbo].[Student] ([StudentID], [StudentCode], [LastName], [FirstName], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Classroom], [GPAScore], [LetterScore], [Address], [Status]) VALUES (16, N'20103100648', N'Trần Duy', N'Hiệp', N'Nam', CAST(N'2002-11-12' AS Date), N'12duyhiep12@gmail.com', N'0388869202', N'DHTI14A8HN', CAST(2.85 AS Decimal(18, 2)), N'C+', N'Sa Long - Thị Trấn Thắng - Hiệp Hòa - Bắc Giang', 1)
INSERT [dbo].[Student] ([StudentID], [StudentCode], [LastName], [FirstName], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Classroom], [GPAScore], [LetterScore], [Address], [Status]) VALUES (17, N'20103100649', N'Nguyễn Đức', N'Đạt', N'Nam', CAST(N'2002-02-22' AS Date), N'nguyenddat.2002@gmail.com', N'0378516595', N'DHTI14A7HN', CAST(2.84 AS Decimal(18, 2)), N'C+', N'thôn Trung Hòa, xã Mai Trung, huyện Hiệp Hòa, tỉnh Bắc Giang,', 1)
INSERT [dbo].[Student] ([StudentID], [StudentCode], [LastName], [FirstName], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Classroom], [GPAScore], [LetterScore], [Address], [Status]) VALUES (18, N'20103100650', N'Nguyễn Trường', N'Giang', N'Nam', CAST(N'2002-08-09' AS Date), N'truonggiangg0908@gmail.com', N'0969013925', N'DHTI14A7HN', CAST(2.65 AS Decimal(18, 2)), N'C+', N'Huyện Yên Dũng,Tỉnh Bắc Giang.', 0)
INSERT [dbo].[Student] ([StudentID], [StudentCode], [LastName], [FirstName], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Classroom], [GPAScore], [LetterScore], [Address], [Status]) VALUES (19, N'20103100651', N'Nguyễn Duy', N'Sơn', N'Nam', CAST(N'2002-04-01' AS Date), N'duyson.nguyends@gmail.com', N'0983958460', N'DHTI14A5HN', CAST(2.57 AS Decimal(18, 2)), N'C+', N'Huyện Lục Nam - Bắc Giang', 0)
INSERT [dbo].[Student] ([StudentID], [StudentCode], [LastName], [FirstName], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Classroom], [GPAScore], [LetterScore], [Address], [Status]) VALUES (20, N'20103100653', N'Lương Tuệ', N'Minh', N'Nam', CAST(N'2002-08-01' AS Date), N'thai2012002@gmail.com', N'0964178382', N'DHMT14A2HN', CAST(2.54 AS Decimal(18, 2)), N'C+', N'Huyện Việt Yên, Tỉnh Bắc Giang.', 0)
INSERT [dbo].[Student] ([StudentID], [StudentCode], [LastName], [FirstName], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Classroom], [GPAScore], [LetterScore], [Address], [Status]) VALUES (21, N'20103100654', N'Đào Thị Hương', N'Giang', N'Nữ', CAST(N'2002-07-31' AS Date), N'hgiang30072002@gmail.com', N'0965796806', N'DHTI14A6HN', CAST(2.51 AS Decimal(18, 2)), N'C+', N'Huyện Hiệp Hoà, Bắc Giang', 1)
INSERT [dbo].[Student] ([StudentID], [StudentCode], [LastName], [FirstName], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Classroom], [GPAScore], [LetterScore], [Address], [Status]) VALUES (22, N'20103100655', N'Phạm Văn', N'Trường', N'Nam', CAST(N'2002-01-03' AS Date), N'pvtruong.dhti14a9hn@sv.uneti.edu.vn', N'0377890096', N'DHTI14A9HN', CAST(2.49 AS Decimal(18, 2)), N'C', N'Huyện Hiệp Hoà,Tỉnh Bắc Giang.', 1)
INSERT [dbo].[Student] ([StudentID], [StudentCode], [LastName], [FirstName], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Classroom], [GPAScore], [LetterScore], [Address], [Status]) VALUES (23, N'20103100656', N'Dương Văn', N'Trung', N'Nam', CAST(N'2002-10-29' AS Date), N'dvtrung.dhti14a12hn@sv.uneti.edu.vn', N'0379357129', N'DHTI14A12HN', CAST(2.42 AS Decimal(18, 2)), N'C', N'Huyện Tân Yên,Tỉnh Bắc Giang.', 0)
INSERT [dbo].[Student] ([StudentID], [StudentCode], [LastName], [FirstName], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Classroom], [GPAScore], [LetterScore], [Address], [Status]) VALUES (24, N'20103100657', N'Nguyễn Ngọc', N'Hưng', N'Nam', CAST(N'2002-02-10' AS Date), N'ngochung234kt@gmail.com', N'0868007325', N'DHTI14A1HN', CAST(2.35 AS Decimal(18, 2)), N'C', N'Huyện Lạng Giang - Bắc Giang', 0)
INSERT [dbo].[Student] ([StudentID], [StudentCode], [LastName], [FirstName], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Classroom], [GPAScore], [LetterScore], [Address], [Status]) VALUES (25, N'20103100658', N'Nguyễn Quang', N'Hạnh', N'Nam', CAST(N'2002-01-03' AS Date), N'nqhanh.dhmt14a1hn@sv.uneti.edu.vn', N'0332167288', N'DHMT14A1HN', CAST(2.31 AS Decimal(18, 2)), N'C', N'Lạng Giang - Bắc Giang', 1)
INSERT [dbo].[Student] ([StudentID], [StudentCode], [LastName], [FirstName], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Classroom], [GPAScore], [LetterScore], [Address], [Status]) VALUES (26, N'20103100659', N'Lê Thị', N'Quỳnh', N'Nữ', CAST(N'2002-10-20' AS Date), N'ltquynh20102002@gmai.cơm', N'0963858032', N'DHTI14A2CL', CAST(3.30 AS Decimal(18, 2)), N'B', N'Huyện Tiên Lữ, Tỉnh Hưng Yên', 1)
INSERT [dbo].[Student] ([StudentID], [StudentCode], [LastName], [FirstName], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Classroom], [GPAScore], [LetterScore], [Address], [Status]) VALUES (27, N'20103100660', N'Hoàng Minh', N'Thản', N'Nam', CAST(N'2002-10-05' AS Date), N'hmthan.dhti14a9hn@sv.uneti.edu.vn', N'0868133967', N'DHTI14A9HN', CAST(3.20 AS Decimal(18, 2)), N'B', N'Huyện Yên Mỹ, Tỉnh Hưng Yên', 0)
INSERT [dbo].[Student] ([StudentID], [StudentCode], [LastName], [FirstName], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Classroom], [GPAScore], [LetterScore], [Address], [Status]) VALUES (28, N'20103100661', N'Phạm Văn', N'Quân', N'Nam', CAST(N'2002-09-01' AS Date), N'phamhathu201@gmail.com', N'0353498956', N'DHTI14A2HN', CAST(3.19 AS Decimal(18, 2)), N'B', N'Huyện Phù Cừ, Tỉnh Hưng Yên.', 0)
INSERT [dbo].[Student] ([StudentID], [StudentCode], [LastName], [FirstName], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Classroom], [GPAScore], [LetterScore], [Address], [Status]) VALUES (29, N'20103100662', N'Đào Duy', N'Hợp', N'Nam', CAST(N'2002-09-11' AS Date), N'ddhop.dhmt14a2hn@sv.uneti.edu.vn', N'0977850206', N'DHMT14A2HN', CAST(3.18 AS Decimal(18, 2)), N'B', N'Ân thi, Hưng yên', 1)
INSERT [dbo].[Student] ([StudentID], [StudentCode], [LastName], [FirstName], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Classroom], [GPAScore], [LetterScore], [Address], [Status]) VALUES (30, N'20103100663', N'Phạm Văn', N'Nam', N'Nam', CAST(N'2002-07-30' AS Date), N'pvnam372@gmail.com', N'0796468988', N'DHTI14A8HN', CAST(3.00 AS Decimal(18, 2)), N'B', N'Huyện Ân Thi -Tỉnh Hưng Yên', 0)
INSERT [dbo].[Student] ([StudentID], [StudentCode], [LastName], [FirstName], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Classroom], [GPAScore], [LetterScore], [Address], [Status]) VALUES (31, N'20103100664', N'Lê Đức', N'Tuấn', N'Nam', CAST(N'2002-07-22' AS Date), N'tuanhypc02@gmail.com', N'0982650467', N'DHTI14A7HN', CAST(2.99 AS Decimal(18, 2)), N'C+', N'Huyện Phù Cừ, Tỉnh Hưng Yên.', 1)
INSERT [dbo].[Student] ([StudentID], [StudentCode], [LastName], [FirstName], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Classroom], [GPAScore], [LetterScore], [Address], [Status]) VALUES (32, N'20103100665', N'Nguyễn Văn', N'Quyền', N'Nam', CAST(N'2002-10-28' AS Date), N'quyen1990leesin@gmail.com', N'0982329844', N'DHTI14A1HN', CAST(2.95 AS Decimal(18, 2)), N'C+', N'TP Hưng Yên, Hưng Yên', 1)
INSERT [dbo].[Student] ([StudentID], [StudentCode], [LastName], [FirstName], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Classroom], [GPAScore], [LetterScore], [Address], [Status]) VALUES (33, N'20103100666', N'Nguyễn Thị Hải', N'Phương', N'Nữ', CAST(N'2001-11-06' AS Date), N'nguyenhaiphuong06112001@gmail.com', N'0825122995', N'DHTI14A4HN', CAST(2.89 AS Decimal(18, 2)), N'C+', N'Huyện Khoái Châu, Tỉnh Hưng Yên.', 0)
INSERT [dbo].[Student] ([StudentID], [StudentCode], [LastName], [FirstName], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Classroom], [GPAScore], [LetterScore], [Address], [Status]) VALUES (34, N'20103100667', N'Trịnh Thanh', N'Toại', N'Nam', CAST(N'2002-04-12' AS Date), N'tttoai.dhti14a9hn@sv.uneti.edu.vn', N'0325452356', N'DHTI14A9HN', CAST(2.73 AS Decimal(18, 2)), N'C+', N'Huyện Văn Lâm,Tỉnh Hưng Yên', 0)
INSERT [dbo].[Student] ([StudentID], [StudentCode], [LastName], [FirstName], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Classroom], [GPAScore], [LetterScore], [Address], [Status]) VALUES (35, N'20103100668', N'Nguyễn Thị', N'Luật', N'Nữ', CAST(N'2001-06-27' AS Date), N'ntluat.dhti14a9hn@sv.uneti.edu.vn', N'0327306082', N'DHTI14A9HN', CAST(2.58 AS Decimal(18, 2)), N'C+', N'Huyện Khoái Châu - Hưng Yên', 1)
INSERT [dbo].[Student] ([StudentID], [StudentCode], [LastName], [FirstName], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Classroom], [GPAScore], [LetterScore], [Address], [Status]) VALUES (36, N'20103100669', N'Vũ Mạnh', N'Cường', N'Nam', CAST(N'2002-11-25' AS Date), N'cuongvumanh24@gmail.com', N'0961664329', N'DHTI14A1HN', CAST(2.53 AS Decimal(18, 2)), N'C+', N'Huyện Yên Mỹ, Tỉnh Hưng Yên', 1)
INSERT [dbo].[Student] ([StudentID], [StudentCode], [LastName], [FirstName], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Classroom], [GPAScore], [LetterScore], [Address], [Status]) VALUES (37, N'20103100670', N'Nguyễn Thúy', N'Quỳnh', N'Nữ', CAST(N'2002-10-25' AS Date), N'quynhthuy25102002@gmail.com', N'0377963235', N'DHTI14A1HN', CAST(2.49 AS Decimal(18, 2)), N'C', N'Huyện Yên Mỹ,Tỉnh Hưng Yên.', 0)
INSERT [dbo].[Student] ([StudentID], [StudentCode], [LastName], [FirstName], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Classroom], [GPAScore], [LetterScore], [Address], [Status]) VALUES (38, N'20103100671', N'Trương Văn', N'Thuân', N'Nam', CAST(N'2001-10-28' AS Date), N'truongvanthuan0210@gmail.com', N'0328554762', N'DHTI14A4HN', CAST(2.38 AS Decimal(18, 2)), N'C', N'Huyện Văn Giang -tỉnh Hưng Yên', 0)
INSERT [dbo].[Student] ([StudentID], [StudentCode], [LastName], [FirstName], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Classroom], [GPAScore], [LetterScore], [Address], [Status]) VALUES (39, N'20103100672', N'Chu Mạnh', N'Tiến', N'Nam', CAST(N'2002-11-16' AS Date), N'tienphaluoi@gmail.com', N'0869393759', N'DHTI14A5HN', CAST(2.38 AS Decimal(18, 2)), N'C', N'văn lâm, Tỉnh Hưng Yên.', 1)
INSERT [dbo].[Student] ([StudentID], [StudentCode], [LastName], [FirstName], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Classroom], [GPAScore], [LetterScore], [Address], [Status]) VALUES (40, N'20103100673', N'Trần Tuấn', N'Nghĩa', N'Nam', CAST(N'2002-12-11' AS Date), N'tuannghiapytn@gmail.com', N'0394000898', N'DHTI14A2HN', CAST(2.78 AS Decimal(18, 2)), N'C+', N'Thị xã Phổ Yên - Tỉnh Thái Nguyên', 0)
INSERT [dbo].[Student] ([StudentID], [StudentCode], [LastName], [FirstName], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Classroom], [GPAScore], [LetterScore], [Address], [Status]) VALUES (41, N'20103100674', N'Lê Anh', N'Minh', N'Nam', CAST(N'2002-11-09' AS Date), N'laminh.dhti14a12hn@sv.uneti.edu.vn', N'0364766020', N'DHTI14A12HN', CAST(3.12 AS Decimal(18, 2)), N'B', N'Huyện Điện Biên, Tỉnh Điện Biên.', 1)
INSERT [dbo].[Student] ([StudentID], [StudentCode], [LastName], [FirstName], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Classroom], [GPAScore], [LetterScore], [Address], [Status]) VALUES (42, N'20103100675', N'Nguyễn Tiến', N'Hưng', N'Nam', CAST(N'2002-04-29' AS Date), N'nthung.dhti14a13hn@sv.uneti.edu.vn', N'0365932101', N'DHTI14A13HN', CAST(3.38 AS Decimal(18, 2)), N'B', N'Tỉnh Hải Dương.', 1)
INSERT [dbo].[Student] ([StudentID], [StudentCode], [LastName], [FirstName], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Classroom], [GPAScore], [LetterScore], [Address], [Status]) VALUES (43, N'20103100676', N'Trần Như', N'Quỳnh', N'Nữ', CAST(N'2002-05-15' AS Date), N'qt448047@gmail.com', N'0971136925', N'DHTI14A6HN', CAST(3.29 AS Decimal(18, 2)), N'B', N'Huyện Kim Thành, Hải Dương', 0)
INSERT [dbo].[Student] ([StudentID], [StudentCode], [LastName], [FirstName], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Classroom], [GPAScore], [LetterScore], [Address], [Status]) VALUES (44, N'20103100677', N'Nguyễn Bảo', N'Ngọc', N'Nữ', CAST(N'2002-08-27' AS Date), N'nbngoc.dhti14a13hn@sv.uneti.edu.vn', N'0372889096', N'DHTI14A13HN', CAST(3.27 AS Decimal(18, 2)), N'B', N'Huyện Tứ Kỳ, Tỉnh Hải Dương.', 0)
INSERT [dbo].[Student] ([StudentID], [StudentCode], [LastName], [FirstName], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Classroom], [GPAScore], [LetterScore], [Address], [Status]) VALUES (45, N'20103100678', N'Bùi Thanh', N'Trà', N'Nam', CAST(N'2002-06-25' AS Date), N'buitra02@gmail.com', N'0369292098', N'DHTI14A6HN', CAST(3.27 AS Decimal(18, 2)), N'B', N'Ninh Giang, Hải Dương', 1)
INSERT [dbo].[Student] ([StudentID], [StudentCode], [LastName], [FirstName], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Classroom], [GPAScore], [LetterScore], [Address], [Status]) VALUES (46, N'20103100679', N'Nguyễn Thị Khánh', N'Linh', N'Nữ', CAST(N'2002-10-26' AS Date), N'klinh26102002@gmail.com', N'0929897139', N'DHMT14A2HN', CAST(3.27 AS Decimal(18, 2)), N'B', N'thành phố Hải Dương, Hải Dương', 1)
INSERT [dbo].[Student] ([StudentID], [StudentCode], [LastName], [FirstName], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Classroom], [GPAScore], [LetterScore], [Address], [Status]) VALUES (47, N'20103100680', N'Nguyễn Thị', N'Hoài', N'Nữ', CAST(N'2002-03-13' AS Date), N'nguyenhoaiglhd13@gmail.com', N'0372880478', N'DHTI14A2HN', CAST(3.26 AS Decimal(18, 2)), N'B', N'Huyện Gia Lộc, Hải Dương', 0)
INSERT [dbo].[Student] ([StudentID], [StudentCode], [LastName], [FirstName], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Classroom], [GPAScore], [LetterScore], [Address], [Status]) VALUES (48, N'20103100681', N'Đỗ Văn', N'Quý', N'Nam', CAST(N'2002-06-10' AS Date), N'doquy10062002@gmail.com', N'0984176412', N'DHTI14A10HN', CAST(3.20 AS Decimal(18, 2)), N'B', N'Huyện kim Thành, Tỉnh Hải Dương', 0)
INSERT [dbo].[Student] ([StudentID], [StudentCode], [LastName], [FirstName], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Classroom], [GPAScore], [LetterScore], [Address], [Status]) VALUES (49, N'20103100682', N'Bùi Đức', N'Huy', N'Nam', CAST(N'2002-11-05' AS Date), N'bdhuy02@gmail.com', N'0981366502', N'DHTI14A3HN', CAST(3.13 AS Decimal(18, 2)), N'B', N'Tp Hải Dương, Tỉnh Hải Dương', 1)
INSERT [dbo].[Student] ([StudentID], [StudentCode], [LastName], [FirstName], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Classroom], [GPAScore], [LetterScore], [Address], [Status]) VALUES (50, N'20103100123', N'Mai Ngọc', N'Đức', N'Nam', CAST(N'2002-08-02' AS Date), N'mnduc@gmail.com', N'0813250111', N'DHTI14A2CL', CAST(3.10 AS Decimal(18, 2)), N'B', N'Hà Nội', 1)
SET IDENTITY_INSERT [dbo].[Student] OFF
GO
SET IDENTITY_INSERT [dbo].[Teacher] ON 

INSERT [dbo].[Teacher] ([TeacherID], [LastName], [FirstName], [Address], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Office]) VALUES (1, N'Mai Mạnh', N'Trừng', N'Cầu Giấy - Hà Nội', N'Nam', CAST(N'1986-02-01' AS Date), N'trungmm@gmail.com', N'0974507584', N'Phó bộ môn')
INSERT [dbo].[Teacher] ([TeacherID], [LastName], [FirstName], [Address], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Office]) VALUES (2, N'Lê Thị Thảo', N'Hiếu', N'Hoàng Mai - Hà Nội', N'Nữ', CAST(N'1988-11-04' AS Date), N'hieultt@gmail.com', N'0898253433', N'Giảng viên')
INSERT [dbo].[Teacher] ([TeacherID], [LastName], [FirstName], [Address], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Office]) VALUES (3, N'Bùi Văn', N'Tân', N'Hai Bà Trưng - Hà Nội', N'Nam', CAST(N'1980-08-12' AS Date), N'tanbv@gmail.com', N'0396955238', N'Trưởng bộ môn')
INSERT [dbo].[Teacher] ([TeacherID], [LastName], [FirstName], [Address], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Office]) VALUES (4, N'Đường Tuấn', N'Hải', N'Cầu Giấy - Hà Nội', N'Nam', CAST(N'1985-04-02' AS Date), N'haidt@gmail.com', N'0912233917', N'Giảng viên')
INSERT [dbo].[Teacher] ([TeacherID], [LastName], [FirstName], [Address], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Office]) VALUES (5, N'Trần Cảnh', N'Dương', N'Cầu Giấy - Hà Nội', N'Nam', CAST(N'1985-05-02' AS Date), N'duongtc@gmail.com', N'0912233918', N'Giảng viên')
INSERT [dbo].[Teacher] ([TeacherID], [LastName], [FirstName], [Address], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Office]) VALUES (6, N'Bùi Văn', N'Công', N'Hai Bà Trưng - Hà Nội', N'Nam', CAST(N'1987-07-12' AS Date), N'congbv@gmail.com', N'0912233919', N'Phó bộ môn')
INSERT [dbo].[Teacher] ([TeacherID], [LastName], [FirstName], [Address], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Office]) VALUES (7, N'Nguyễn Hoàng', N'Chiến', N'Hoàng Mai - Hà Nội', N'Nam', CAST(N'1988-04-02' AS Date), N'chiennh@gmail.com', N'0912233920', N'Trưởng bộ môn')
INSERT [dbo].[Teacher] ([TeacherID], [LastName], [FirstName], [Address], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Office]) VALUES (8, N'Trần Bích ', N'Thảo', N'Hai Bà Trưng - Hà Nội', N'Nữ', CAST(N'1989-04-17' AS Date), N'thaotb@gmail.com', N'0912233921', N'Giảng viên')
INSERT [dbo].[Teacher] ([TeacherID], [LastName], [FirstName], [Address], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Office]) VALUES (9, N'Trần Thị Lan', N'Anh', N'Hoàng Mai - Hà Nội', N'Nữ', CAST(N'1990-11-12' AS Date), N'anhttl@gmail.com', N'0912233922', N'Phó bộ môn')
INSERT [dbo].[Teacher] ([TeacherID], [LastName], [FirstName], [Address], [Gender], [DateOfBirth], [Email], [PhoneNumber], [Office]) VALUES (10, N'Lê Thị Thu', N'Hiền', N'Cầu Giấy - Hà Nội', N'Nữ', CAST(N'1991-12-22' AS Date), N'hienltt@gmail.com', N'0912233923', N'Phó bộ môn')
SET IDENTITY_INSERT [dbo].[Teacher] OFF
GO
SET IDENTITY_INSERT [dbo].[Topic] ON 

INSERT [dbo].[Topic] ([TopicID], [Title], [Description], [StudentID], [EmployeeID]) VALUES (1, N'Sử dụng ASP.Net làm Web bán hàng', N'thêm, sửa, xóa, quản lý, chức năng thanh toán, giỏ hàng', 1, 1)
INSERT [dbo].[Topic] ([TopicID], [Title], [Description], [StudentID], [EmployeeID]) VALUES (2, N'Sử dụng PHP, Mysql làm phần mềm quản lý sinh viên', N'thêm, sửa, xóa, quản lý, chức năng thông kế, báo cáo', 4, 2)
INSERT [dbo].[Topic] ([TopicID], [Title], [Description], [StudentID], [EmployeeID]) VALUES (3, N'Web bán hàng', N'Luận văn tốt nghiệp', 5, 1)
INSERT [dbo].[Topic] ([TopicID], [Title], [Description], [StudentID], [EmployeeID]) VALUES (4, N'Web quản lý', N'Luận văn tốt nghiệp', 10, 2)
INSERT [dbo].[Topic] ([TopicID], [Title], [Description], [StudentID], [EmployeeID]) VALUES (5, N'App bán hàng', N'Luận văn tốt nghiệp', 27, 3)
INSERT [dbo].[Topic] ([TopicID], [Title], [Description], [StudentID], [EmployeeID]) VALUES (6, N'App quản lý', N'Luận văn tốt nghiệp', 28, 4)
INSERT [dbo].[Topic] ([TopicID], [Title], [Description], [StudentID], [EmployeeID]) VALUES (7, N'Trang giới thiệu sản phẩm', N'Luận văn tốt nghiệp', 29, 3)
INSERT [dbo].[Topic] ([TopicID], [Title], [Description], [StudentID], [EmployeeID]) VALUES (8, N'Trang giới thiệu công ty', N'Luận văn tốt nghiệp', 30, 3)
INSERT [dbo].[Topic] ([TopicID], [Title], [Description], [StudentID], [EmployeeID]) VALUES (9, N'Web xem phim', N'Luận văn tốt nghiệp', 31, 6)
INSERT [dbo].[Topic] ([TopicID], [Title], [Description], [StudentID], [EmployeeID]) VALUES (10, N'App bán sách', N'Luận văn tốt nghiệp', 32, 5)
INSERT [dbo].[Topic] ([TopicID], [Title], [Description], [StudentID], [EmployeeID]) VALUES (11, N'App bán thực phẩm', N'Luận văn tốt nghiệp', 33, 5)
INSERT [dbo].[Topic] ([TopicID], [Title], [Description], [StudentID], [EmployeeID]) VALUES (12, N'Web quản lý nhân viên', N'Luận văn tốt nghiệp', 34, 6)
INSERT [dbo].[Topic] ([TopicID], [Title], [Description], [StudentID], [EmployeeID]) VALUES (13, N'Web chấm công', N'Luận văn tốt nghiệp', 35, 7)
INSERT [dbo].[Topic] ([TopicID], [Title], [Description], [StudentID], [EmployeeID]) VALUES (14, N'App chấm công', N'Luận văn tốt nghiệp', 36, 7)
INSERT [dbo].[Topic] ([TopicID], [Title], [Description], [StudentID], [EmployeeID]) VALUES (15, N'App quản lý nhân viên', N'Luận văn tốt nghiệp', 39, 8)
INSERT [dbo].[Topic] ([TopicID], [Title], [Description], [StudentID], [EmployeeID]) VALUES (16, N'App quản lý sinh viên', N'Luận văn tốt nghiệp', 41, 8)
INSERT [dbo].[Topic] ([TopicID], [Title], [Description], [StudentID], [EmployeeID]) VALUES (17, N'Web nghe nhạc', N'Luận văn tốt nghiệp', 43, 9)
INSERT [dbo].[Topic] ([TopicID], [Title], [Description], [StudentID], [EmployeeID]) VALUES (18, N'Web đọc báo', N'Luận văn tốt nghiệp', 44, 9)
INSERT [dbo].[Topic] ([TopicID], [Title], [Description], [StudentID], [EmployeeID]) VALUES (19, N'App nghe nhạc', N'Luận văn tốt nghiệp', 47, 10)
INSERT [dbo].[Topic] ([TopicID], [Title], [Description], [StudentID], [EmployeeID]) VALUES (20, N'App đọc báo', N'Luận văn tốt nghiệp', 49, 10)
SET IDENTITY_INSERT [dbo].[Topic] OFF
GO
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF__TaiKhoan__LoaiTa__49C3F6B7]  DEFAULT ('user') FOR [RoleID]
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_Roles] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([IDRole])
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_Account_Roles]
GO
ALTER TABLE [dbo].[Internship]  WITH CHECK ADD  CONSTRAINT [FK__ThucTap__MaGiaoV__5441852A] FOREIGN KEY([TeacherID])
REFERENCES [dbo].[Teacher] ([TeacherID])
GO
ALTER TABLE [dbo].[Internship] CHECK CONSTRAINT [FK__ThucTap__MaGiaoV__5441852A]
GO
ALTER TABLE [dbo].[Internship]  WITH CHECK ADD  CONSTRAINT [FK__ThucTap__MaSV__52593CB8] FOREIGN KEY([StudentID])
REFERENCES [dbo].[Student] ([StudentID])
GO
ALTER TABLE [dbo].[Internship] CHECK CONSTRAINT [FK__ThucTap__MaSV__52593CB8]
GO
ALTER TABLE [dbo].[Internship]  WITH CHECK ADD  CONSTRAINT [FK_Internship_Employee] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employee] ([EmployeeID])
GO
ALTER TABLE [dbo].[Internship] CHECK CONSTRAINT [FK_Internship_Employee]
GO
ALTER TABLE [dbo].[Score]  WITH CHECK ADD  CONSTRAINT [FK__Diem__MaDeTai__59FA5E80] FOREIGN KEY([TopicID])
REFERENCES [dbo].[Topic] ([TopicID])
GO
ALTER TABLE [dbo].[Score] CHECK CONSTRAINT [FK__Diem__MaDeTai__59FA5E80]
GO
ALTER TABLE [dbo].[Topic]  WITH CHECK ADD  CONSTRAINT [FK__DeTai__MaSV__571DF1D5] FOREIGN KEY([StudentID])
REFERENCES [dbo].[Student] ([StudentID])
GO
ALTER TABLE [dbo].[Topic] CHECK CONSTRAINT [FK__DeTai__MaSV__571DF1D5]
GO
ALTER TABLE [dbo].[Topic]  WITH CHECK ADD  CONSTRAINT [FK_Topic_Employee] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employee] ([EmployeeID])
GO
ALTER TABLE [dbo].[Topic] CHECK CONSTRAINT [FK_Topic_Employee]
GO
USE [master]
GO
ALTER DATABASE [QLSVTT] SET  READ_WRITE 
GO
