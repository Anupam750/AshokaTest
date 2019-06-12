USE [master]
GO
/****** Object:  Database [AshokaTest]    Script Date: 6/13/2019 12:39:18 AM ******/
CREATE DATABASE [AshokaTest]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AshokaTest', FILENAME = N'E:\InstalledSoftwares\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\AshokaTest.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'AshokaTest_log', FILENAME = N'E:\InstalledSoftwares\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\AshokaTest_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [AshokaTest] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AshokaTest].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AshokaTest] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AshokaTest] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AshokaTest] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AshokaTest] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AshokaTest] SET ARITHABORT OFF 
GO
ALTER DATABASE [AshokaTest] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AshokaTest] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AshokaTest] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AshokaTest] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AshokaTest] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AshokaTest] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AshokaTest] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AshokaTest] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AshokaTest] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AshokaTest] SET  DISABLE_BROKER 
GO
ALTER DATABASE [AshokaTest] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AshokaTest] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AshokaTest] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AshokaTest] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AshokaTest] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AshokaTest] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AshokaTest] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AshokaTest] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [AshokaTest] SET  MULTI_USER 
GO
ALTER DATABASE [AshokaTest] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AshokaTest] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AshokaTest] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AshokaTest] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [AshokaTest] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [AshokaTest] SET QUERY_STORE = OFF
GO
USE [AshokaTest]
GO
/****** Object:  Table [dbo].[Courses]    Script Date: 6/13/2019 12:39:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Courses](
	[CourseID] [int] IDENTITY(1,1) NOT NULL,
	[Category] [nvarchar](10) NOT NULL,
	[CousreCode] [nvarchar](20) NOT NULL,
	[CourseName] [nvarchar](50) NOT NULL,
	[Capicity] [int] NOT NULL,
 CONSTRAINT [PK_Courses] PRIMARY KEY CLUSTERED 
(
	[CourseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rules]    Script Date: 6/13/2019 12:39:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rules](
	[Category] [nvarchar](10) NOT NULL,
	[Min] [int] NOT NULL,
	[Max] [int] NOT NULL,
	[Capicity] [int] NULL,
 CONSTRAINT [PK_Rules] PRIMARY KEY CLUSTERED 
(
	[Category] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 6/13/2019 12:39:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[StudentID] [int] IDENTITY(1,1) NOT NULL,
	[StudentName] [nvarchar](50) NOT NULL,
	[StudentEmail] [nvarchar](50) NOT NULL,
	[StudentPassword] [nvarchar](20) NOT NULL,
	[Status] [nvarchar](10) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[StudentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentCourseMapping]    Script Date: 6/13/2019 12:39:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentCourseMapping](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StudentID] [int] NOT NULL,
	[CourseCode] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_StudentCourseMapping] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Courses] ON 

INSERT [dbo].[Courses] ([CourseID], [Category], [CousreCode], [CourseName], [Capicity]) VALUES (1, N'FC', N'C1', N'Cousre 1', 20)
INSERT [dbo].[Courses] ([CourseID], [Category], [CousreCode], [CourseName], [Capicity]) VALUES (2, N'FC', N'C2', N'Cousre 2', 20)
INSERT [dbo].[Courses] ([CourseID], [Category], [CousreCode], [CourseName], [Capicity]) VALUES (3, N'FC', N'C3', N'Cousre 3', 20)
INSERT [dbo].[Courses] ([CourseID], [Category], [CousreCode], [CourseName], [Capicity]) VALUES (4, N'FC', N'C4', N'Cousre 4', 20)
INSERT [dbo].[Courses] ([CourseID], [Category], [CousreCode], [CourseName], [Capicity]) VALUES (5, N'CT', N'C5', N'Cousre 5', 20)
INSERT [dbo].[Courses] ([CourseID], [Category], [CousreCode], [CourseName], [Capicity]) VALUES (6, N'CT', N'C6', N'Cousre 6', 20)
INSERT [dbo].[Courses] ([CourseID], [Category], [CousreCode], [CourseName], [Capicity]) VALUES (7, N'CT', N'C7', N'Cousre 6', 20)
INSERT [dbo].[Courses] ([CourseID], [Category], [CousreCode], [CourseName], [Capicity]) VALUES (8, N'CT', N'C8', N'Cousre 7', 20)
INSERT [dbo].[Courses] ([CourseID], [Category], [CousreCode], [CourseName], [Capicity]) VALUES (9, N'MM', N'C9', N'Cousre 8', 20)
INSERT [dbo].[Courses] ([CourseID], [Category], [CousreCode], [CourseName], [Capicity]) VALUES (10, N'MM', N'C10', N'Cousre 9', 20)
INSERT [dbo].[Courses] ([CourseID], [Category], [CousreCode], [CourseName], [Capicity]) VALUES (11, N'MM', N'C11', N'Cousre 10', 20)
INSERT [dbo].[Courses] ([CourseID], [Category], [CousreCode], [CourseName], [Capicity]) VALUES (12, N'MM', N'C12', N'Cousre 11', 20)
SET IDENTITY_INSERT [dbo].[Courses] OFF
INSERT [dbo].[Rules] ([Category], [Min], [Max], [Capicity]) VALUES (N'CT', 1, 1, 20)
INSERT [dbo].[Rules] ([Category], [Min], [Max], [Capicity]) VALUES (N'FC', 1, 3, 2)
INSERT [dbo].[Rules] ([Category], [Min], [Max], [Capicity]) VALUES (N'MM', 2, 2, 5)
INSERT [dbo].[Rules] ([Category], [Min], [Max], [Capicity]) VALUES (N'Overall', 3, 3, 3)
SET IDENTITY_INSERT [dbo].[Student] ON 

INSERT [dbo].[Student] ([StudentID], [StudentName], [StudentEmail], [StudentPassword], [Status], [IsActive]) VALUES (1, N'Anupam Singh', N'anupamsinghjadoun@gmail.com', N'123456789', N'Confirmed', 0)
INSERT [dbo].[Student] ([StudentID], [StudentName], [StudentEmail], [StudentPassword], [Status], [IsActive]) VALUES (2, N'Sohan Singh', N'sohan@gmail.com', N'789845445', N'Confirmed', 0)
INSERT [dbo].[Student] ([StudentID], [StudentName], [StudentEmail], [StudentPassword], [Status], [IsActive]) VALUES (3, N'Ramesh Singh', N'ramesh@gmail.com', N'78965412', N'Waiting', 0)
INSERT [dbo].[Student] ([StudentID], [StudentName], [StudentEmail], [StudentPassword], [Status], [IsActive]) VALUES (4, N'Sudhir Sharma', N'sudhir@gmail.com', N'78965412', N'Confirmed', 0)
SET IDENTITY_INSERT [dbo].[Student] OFF
SET IDENTITY_INSERT [dbo].[StudentCourseMapping] ON 

INSERT [dbo].[StudentCourseMapping] ([ID], [StudentID], [CourseCode]) VALUES (1, 1, N'C5')
INSERT [dbo].[StudentCourseMapping] ([ID], [StudentID], [CourseCode]) VALUES (2, 2, N'C1')
INSERT [dbo].[StudentCourseMapping] ([ID], [StudentID], [CourseCode]) VALUES (3, 2, N'C2')
INSERT [dbo].[StudentCourseMapping] ([ID], [StudentID], [CourseCode]) VALUES (4, 2, N'C3')
INSERT [dbo].[StudentCourseMapping] ([ID], [StudentID], [CourseCode]) VALUES (5, 3, N'C1')
INSERT [dbo].[StudentCourseMapping] ([ID], [StudentID], [CourseCode]) VALUES (6, 3, N'C2')
INSERT [dbo].[StudentCourseMapping] ([ID], [StudentID], [CourseCode]) VALUES (7, 3, N'C3')
INSERT [dbo].[StudentCourseMapping] ([ID], [StudentID], [CourseCode]) VALUES (10, 4, N'C5')
SET IDENTITY_INSERT [dbo].[StudentCourseMapping] OFF
ALTER TABLE [dbo].[Courses]  WITH CHECK ADD FOREIGN KEY([Category])
REFERENCES [dbo].[Rules] ([Category])
GO
USE [master]
GO
ALTER DATABASE [AshokaTest] SET  READ_WRITE 
GO
