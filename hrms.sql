USE [master]
GO
/****** Object:  Database [HRMS]    Script Date: 07/07/2024 4:57:21 PM ******/
CREATE DATABASE [HRMS]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'HRMS', FILENAME = N'C:\DATA\HRMS.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'HRMS_log', FILENAME = N'C:\DATA\HRMS_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [HRMS] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [HRMS].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [HRMS] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [HRMS] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [HRMS] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [HRMS] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [HRMS] SET ARITHABORT OFF 
GO
ALTER DATABASE [HRMS] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [HRMS] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [HRMS] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [HRMS] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [HRMS] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [HRMS] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [HRMS] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [HRMS] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [HRMS] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [HRMS] SET  DISABLE_BROKER 
GO
ALTER DATABASE [HRMS] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [HRMS] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [HRMS] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [HRMS] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [HRMS] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [HRMS] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [HRMS] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [HRMS] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [HRMS] SET  MULTI_USER 
GO
ALTER DATABASE [HRMS] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [HRMS] SET DB_CHAINING OFF 
GO
ALTER DATABASE [HRMS] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [HRMS] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [HRMS] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [HRMS] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [HRMS] SET QUERY_STORE = ON
GO
ALTER DATABASE [HRMS] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [HRMS]
GO
/****** Object:  Table [dbo].[Admin]    Script Date: 07/07/2024 4:57:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admin](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](30) NULL,
	[Password] [nvarchar](30) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AfterUniversity]    Script Date: 07/07/2024 4:57:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AfterUniversity](
	[EmployeeCode] [varchar](30) NOT NULL,
	[SpecializedMaster] [nvarchar](50) NULL,
	[TrainingPlaceMaster] [nvarchar](50) NULL,
	[DegreeYearMaster] [nvarchar](10) NULL,
	[SpecializedDoctorate] [nvarchar](50) NULL,
	[TrainingPlaceDoctorate] [nvarchar](50) NULL,
	[DegreeYearDoctorate] [nvarchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bonus]    Script Date: 07/07/2024 4:57:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bonus](
	[EmployeeCode] [varchar](30) NOT NULL,
	[BonusDate] [date] NULL,
	[Reason] [nvarchar](max) NULL,
	[BonusMoney] [float] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contract]    Script Date: 07/07/2024 4:57:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contract](
	[ContractCode] [varchar](30) NOT NULL,
	[ContractType] [nvarchar](50) NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
	[Note] [nvarchar](max) NULL,
 CONSTRAINT [PK_Contract] PRIMARY KEY CLUSTERED 
(
	[ContractCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 07/07/2024 4:57:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[DepartmentCode] [varchar](30) NOT NULL,
	[DepartmentName] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](50) NULL,
	[DepartmentPhoneNumber] [varchar](11) NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[DepartmentCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Discipline]    Script Date: 07/07/2024 4:57:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Discipline](
	[EmployeeCode] [varchar](30) NOT NULL,
	[DisciplineDate] [date] NULL,
	[Reason] [nvarchar](max) NULL,
	[DisciplineMoney] [float] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EducationLevel]    Script Date: 07/07/2024 4:57:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EducationLevel](
	[EducationLevelCode] [varchar](30) NOT NULL,
	[EducationLevelName] [nvarchar](max) NOT NULL,
	[TierCoefficient] [float] NULL,
 CONSTRAINT [PK_EducationLevel] PRIMARY KEY CLUSTERED 
(
	[EducationLevelCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EducationLevelUpdate]    Script Date: 07/07/2024 4:57:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EducationLevelUpdate](
	[UpdateCode] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeCode] [varchar](30) NOT NULL,
	[UpdateDay] [date] NOT NULL,
	[PreviousEducationLevelCode] [varchar](30) NOT NULL,
	[EducationLevelUpdateCode] [varchar](30) NOT NULL,
 CONSTRAINT [PK_EducationLevelUpdate] PRIMARY KEY CLUSTERED 
(
	[UpdateCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 07/07/2024 4:57:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[EmployeeCode] [varchar](30) NOT NULL,
	[Username] [nvarchar](30) NULL,
	[Password] [nvarchar](30) NULL,
	[FullName] [nvarchar](50) NULL,
	[Birthday] [date] NULL,
	[Hometown] [nvarchar](100) NULL,
	[Image] [nvarchar](max) NULL,
	[Gender] [int] NULL,
	[Ethnic] [nvarchar](10) NULL,
	[PhoneNumber] [varchar](11) NULL,
	[EmployeePositionCode] [varchar](30) NULL,
	[Status] [bit] NOT NULL,
	[DepartmentCode] [varchar](30) NULL,
	[ContractCode] [varchar](30) NULL,
	[SpecializedCode] [varchar](30) NULL,
	[EducationLevelCode] [varchar](30) NULL,
	[IdentityCard] [varchar](50) NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[EmployeeCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeePosition]    Script Date: 07/07/2024 4:57:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeePosition](
	[EmployeePositionCode] [varchar](30) NOT NULL,
	[PositionName] [nvarchar](50) NOT NULL,
	[HSPC] [float] NULL,
 CONSTRAINT [PK_EmployeePosition] PRIMARY KEY CLUSTERED 
(
	[EmployeePositionCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeRotation]    Script Date: 07/07/2024 4:57:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeRotation](
	[EmployeeCode] [varchar](30) NOT NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[RotationDate] [date] NOT NULL,
	[RotationReason] [nvarchar](max) NULL,
	[DepartmentRotation] [varchar](30) NULL,
	[IncomingDepartment] [varchar](30) NULL,
 CONSTRAINT [PK_EmployeeRotation] PRIMARY KEY CLUSTERED 
(
	[EmployeeCode] ASC,
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ForeignLanguage]    Script Date: 07/07/2024 4:57:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ForeignLanguage](
	[EmployeeCode] [varchar](30) NOT NULL,
	[ForeignLanguageName] [nvarchar](50) NULL,
	[Level] [nvarchar](30) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuitJob]    Script Date: 07/07/2024 4:57:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuitJob](
	[EmployeeCode] [varchar](30) NOT NULL,
	[Reason] [nvarchar](max) NULL,
	[QuitJobDate] [date] NOT NULL,
 CONSTRAINT [PK_QuitJob] PRIMARY KEY CLUSTERED 
(
	[EmployeeCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Salary]    Script Date: 07/07/2024 4:57:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Salary](
	[EmployeeCode] [varchar](30) NOT NULL,
	[MinimumSalary] [float] NULL,
	[SalaryCoefficient] [float] NULL,
	[SocialInsurance] [float] NULL,
	[HealthInsurance] [float] NULL,
	[UnemploymentInsurance] [float] NULL,
	[Allowance] [float] NULL,
	[IncomeTax] [float] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SalaryDetail]    Script Date: 07/07/2024 4:57:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalaryDetail](
	[EmployeeCode] [varchar](30) NOT NULL,
	[BasicSalary] [float] NULL,
	[SocialInsurance] [float] NULL,
	[HealthInsurance] [float] NULL,
	[UnemploymentInsurance] [float] NULL,
	[Allowance] [float] NULL,
	[IncomeTax] [float] NULL,
	[BonusMoney] [float] NULL,
	[DisciplineMoney] [float] NULL,
	[PayDay] [date] NULL,
	[TotalSalary] [float] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SalaryUpdate]    Script Date: 07/07/2024 4:57:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalaryUpdate](
	[EmployeeCode] [varchar](30) NOT NULL,
	[CurrentSalary] [float] NULL,
	[SalaryAfterUpdate] [float] NULL,
	[SalaryCoefficient] [float] NULL,
	[SocialInsurance] [float] NULL,
	[HealthInsurance] [float] NULL,
	[UnemploymentInsurance] [float] NULL,
	[Allowance] [float] NULL,
	[IncomeTax] [float] NULL,
	[UpdateDay] [date] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ScientificResearchTopics]    Script Date: 07/07/2024 4:57:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScientificResearchTopics](
	[EmployeeCode] [varchar](30) NOT NULL,
	[ScientificResearchTopicName] [nvarchar](200) NULL,
	[YearOfBegin] [nvarchar](10) NULL,
	[YearOfComplete] [nvarchar](10) NULL,
	[LevelTopic] [nvarchar](50) NULL,
	[ResponsibilityInTheTopic] [nvarchar](100) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ScientificWorks]    Script Date: 07/07/2024 4:57:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScientificWorks](
	[EmployeeCode] [varchar](30) NOT NULL,
	[ScientificWorksName] [nvarchar](200) NULL,
	[Year] [nvarchar](10) NULL,
	[MagazineName] [nvarchar](200) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Specialized]    Script Date: 07/07/2024 4:57:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Specialized](
	[SpecializedCode] [varchar](30) NOT NULL,
	[SpecializedName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Specialized] PRIMARY KEY CLUSTERED 
(
	[SpecializedCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UnitUsed]    Script Date: 07/07/2024 4:57:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UnitUsed](
	[UnitUsedName] [nvarchar](200) NULL,
	[SchoolName] [nvarchar](200) NULL,
	[Address] [nvarchar](200) NULL,
	[PhoneNumber] [nvarchar](20) NULL,
	[Email] [nvarchar](200) NULL,
	[SalaryIncreasePeriod] [nvarchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[University]    Script Date: 07/07/2024 4:57:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[University](
	[EmployeeCode] [varchar](30) NOT NULL,
	[UniversityName] [nvarchar](50) NULL,
	[TrainingCountry] [nvarchar](50) NULL,
	[GraduateYear] [nvarchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkingProcess]    Script Date: 07/07/2024 4:57:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkingProcess](
	[EmployeeCode] [varchar](30) NOT NULL,
	[WorkPlace] [nvarchar](100) NULL,
	[WorkUndertake] [nvarchar](200) NULL,
	[Time] [nvarchar](20) NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Admin] ON 

INSERT [dbo].[Admin] ([ID], [Username], [Password]) VALUES (1, N'admin', N'admin')
SET IDENTITY_INSERT [dbo].[Admin] OFF
GO
INSERT [dbo].[Contract] ([ContractCode], [ContractType], [StartDate], [EndDate], [Note]) VALUES (N'0001', N'Nhân viên chính thức', CAST(N'2020-12-20' AS Date), CAST(N'2024-12-20' AS Date), N'abc')
INSERT [dbo].[Contract] ([ContractCode], [ContractType], [StartDate], [EndDate], [Note]) VALUES (N'0002', N'Thử việc', CAST(N'2020-12-20' AS Date), CAST(N'2024-12-20' AS Date), N'abcd')
GO
INSERT [dbo].[Department] ([DepartmentCode], [DepartmentName], [Address], [DepartmentPhoneNumber]) VALUES (N'cntt', N'Công nghệ thông tin', N'Lầu 1 nhà H', N'0826625621')
INSERT [dbo].[Department] ([DepartmentCode], [DepartmentName], [Address], [DepartmentPhoneNumber]) VALUES (N'daotao', N'Đào tạo', N'Lầu 2 nhà A', N'029348472')
INSERT [dbo].[Department] ([DepartmentCode], [DepartmentName], [Address], [DepartmentPhoneNumber]) VALUES (N'ketoan', N'Kế toán', N'Lầu 3 nhà D', N'089372732')
INSERT [dbo].[Department] ([DepartmentCode], [DepartmentName], [Address], [DepartmentPhoneNumber]) VALUES (N'xaydung', N'Xây dựng', N'phòng A1.1 nhà A', N'0832983422')
GO
INSERT [dbo].[EducationLevel] ([EducationLevelCode], [EducationLevelName], [TierCoefficient]) VALUES (N'gs', N'Giáo sư', 6.2)
INSERT [dbo].[EducationLevel] ([EducationLevelCode], [EducationLevelName], [TierCoefficient]) VALUES (N'ks', N'Kỹ sư', 2.34)
INSERT [dbo].[EducationLevel] ([EducationLevelCode], [EducationLevelName], [TierCoefficient]) VALUES (N'pgs', N'Phó giáo sư', 4.4)
INSERT [dbo].[EducationLevel] ([EducationLevelCode], [EducationLevelName], [TierCoefficient]) VALUES (N'ths', N'Thạc sỹ', 2.67)
INSERT [dbo].[EducationLevel] ([EducationLevelCode], [EducationLevelName], [TierCoefficient]) VALUES (N'ts', N'Tiến sỹ', 3)
GO
INSERT [dbo].[Employee] ([EmployeeCode], [Username], [Password], [FullName], [Birthday], [Hometown], [Image], [Gender], [Ethnic], [PhoneNumber], [EmployeePositionCode], [Status], [DepartmentCode], [ContractCode], [SpecializedCode], [EducationLevelCode], [IdentityCard]) VALUES (N'1', N'thientran', N'123456', N'Trần Văn Thiện', CAST(N'1970-06-20' AS Date), N'Cà Mau', N'C:\Users\ADMIN\Downloads\anh-meo-2.jpg', 1, N'Kinh', N'0123456789', N'tp', 1, N'cntt', N'0001', N'cntt', N'ths', N'123456789')
INSERT [dbo].[Employee] ([EmployeeCode], [Username], [Password], [FullName], [Birthday], [Hometown], [Image], [Gender], [Ethnic], [PhoneNumber], [EmployeePositionCode], [Status], [DepartmentCode], [ContractCode], [SpecializedCode], [EducationLevelCode], [IdentityCard]) VALUES (N'2', N'chithanh', N'123456789', N'Nguyễn Chí Thành', CAST(N'2002-02-02' AS Date), N'Cà Mau', N'C:\Users\ADMIN\Downloads\8.jpg', 0, N'Kinh', N'123456789', N'tp', 1, N'ketoan', N'0001', N'cntt', N'ths', N'123456789')
GO
INSERT [dbo].[EmployeePosition] ([EmployeePositionCode], [PositionName], [HSPC]) VALUES (N'nv', N'Nhân viên', 0)
INSERT [dbo].[EmployeePosition] ([EmployeePositionCode], [PositionName], [HSPC]) VALUES (N'pp', N'Phó phòng, Phó khoa', 0.35)
INSERT [dbo].[EmployeePosition] ([EmployeePositionCode], [PositionName], [HSPC]) VALUES (N'tbm', N'Trưởng bộ môn', 0.25)
INSERT [dbo].[EmployeePosition] ([EmployeePositionCode], [PositionName], [HSPC]) VALUES (N'tp', N'Trưởng phòng, Trưởng khoa', 0.45)
GO
INSERT [dbo].[Salary] ([EmployeeCode], [MinimumSalary], [SalaryCoefficient], [SocialInsurance], [HealthInsurance], [UnemploymentInsurance], [Allowance], [IncomeTax]) VALUES (N'1', 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[Salary] ([EmployeeCode], [MinimumSalary], [SalaryCoefficient], [SocialInsurance], [HealthInsurance], [UnemploymentInsurance], [Allowance], [IncomeTax]) VALUES (N'2', 5000000, 4, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[SalaryDetail] ([EmployeeCode], [BasicSalary], [SocialInsurance], [HealthInsurance], [UnemploymentInsurance], [Allowance], [IncomeTax], [BonusMoney], [DisciplineMoney], [PayDay], [TotalSalary]) VALUES (N'1', 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2024-07-07' AS Date), 0)
INSERT [dbo].[SalaryDetail] ([EmployeeCode], [BasicSalary], [SocialInsurance], [HealthInsurance], [UnemploymentInsurance], [Allowance], [IncomeTax], [BonusMoney], [DisciplineMoney], [PayDay], [TotalSalary]) VALUES (N'2', 5000000, 0, 0, 0, 0, 0, 0, 0, CAST(N'2024-07-07' AS Date), 0)
GO
INSERT [dbo].[SalaryUpdate] ([EmployeeCode], [CurrentSalary], [SalaryAfterUpdate], [SalaryCoefficient], [SocialInsurance], [HealthInsurance], [UnemploymentInsurance], [Allowance], [IncomeTax], [UpdateDay]) VALUES (N'1', 0, 0, 0, 0, 0, 0, 0, 0, CAST(N'2024-07-07' AS Date))
INSERT [dbo].[SalaryUpdate] ([EmployeeCode], [CurrentSalary], [SalaryAfterUpdate], [SalaryCoefficient], [SocialInsurance], [HealthInsurance], [UnemploymentInsurance], [Allowance], [IncomeTax], [UpdateDay]) VALUES (N'2', 5000000, 5000000, 4, 0, 0, 0, 0, 0, CAST(N'2024-07-07' AS Date))
GO
INSERT [dbo].[Specialized] ([SpecializedCode], [SpecializedName]) VALUES (N'ck', N'Cơ khí')
INSERT [dbo].[Specialized] ([SpecializedCode], [SpecializedName]) VALUES (N'cntt', N'Công nghệ thông tin')
INSERT [dbo].[Specialized] ([SpecializedCode], [SpecializedName]) VALUES (N'cth', N'Chính trị học')
INSERT [dbo].[Specialized] ([SpecializedCode], [SpecializedName]) VALUES (N'dientu', N'Điện tử')
INSERT [dbo].[Specialized] ([SpecializedCode], [SpecializedName]) VALUES (N'hoahoc', N'Hóa học')
INSERT [dbo].[Specialized] ([SpecializedCode], [SpecializedName]) VALUES (N'kt', N'Kế toán')
INSERT [dbo].[Specialized] ([SpecializedCode], [SpecializedName]) VALUES (N'nl', N'Nhiệt lạnh')
INSERT [dbo].[Specialized] ([SpecializedCode], [SpecializedName]) VALUES (N'sinhhoc', N'Sinh học')
INSERT [dbo].[Specialized] ([SpecializedCode], [SpecializedName]) VALUES (N'toan', N'Toán')
GO
INSERT [dbo].[UnitUsed] ([UnitUsedName], [SchoolName], [Address], [PhoneNumber], [Email], [SalaryIncreasePeriod]) VALUES (N'Đơn vị xxxxxxxxxxxx', N'Trường xxxxxxxxxxxx', N'xxxxxxxxxxxxxxxxxx', N'xxxxxxx', N'xxxxx@gmail.COM', N'2 năm')
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_FK_EducationLevelUpdate_Employee]    Script Date: 07/07/2024 4:57:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_FK_EducationLevelUpdate_Employee] ON [dbo].[EducationLevelUpdate]
(
	[EmployeeCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_FK__Employee__ContractCode]    Script Date: 07/07/2024 4:57:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_FK__Employee__ContractCode] ON [dbo].[Employee]
(
	[ContractCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_FK__Employee__DepartmentCode]    Script Date: 07/07/2024 4:57:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_FK__Employee__DepartmentCode] ON [dbo].[Employee]
(
	[DepartmentCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_FK__Employee__SpecializedCode]    Script Date: 07/07/2024 4:57:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_FK__Employee__SpecializedCode] ON [dbo].[Employee]
(
	[SpecializedCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_FK_Employee_EducationLevel]    Script Date: 07/07/2024 4:57:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_FK_Employee_EducationLevel] ON [dbo].[Employee]
(
	[EducationLevelCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_FK_Employee_EmployeePosition]    Script Date: 07/07/2024 4:57:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_FK_Employee_EmployeePosition] ON [dbo].[Employee]
(
	[EmployeePositionCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AfterUniversity]  WITH CHECK ADD  CONSTRAINT [FK_AfterUniversity_Employee] FOREIGN KEY([EmployeeCode])
REFERENCES [dbo].[Employee] ([EmployeeCode])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AfterUniversity] CHECK CONSTRAINT [FK_AfterUniversity_Employee]
GO
ALTER TABLE [dbo].[Bonus]  WITH CHECK ADD  CONSTRAINT [FK_Bonus_Employee] FOREIGN KEY([EmployeeCode])
REFERENCES [dbo].[Employee] ([EmployeeCode])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Bonus] CHECK CONSTRAINT [FK_Bonus_Employee]
GO
ALTER TABLE [dbo].[Discipline]  WITH CHECK ADD  CONSTRAINT [FK_Discipline_Employee] FOREIGN KEY([EmployeeCode])
REFERENCES [dbo].[Employee] ([EmployeeCode])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Discipline] CHECK CONSTRAINT [FK_Discipline_Employee]
GO
ALTER TABLE [dbo].[EducationLevelUpdate]  WITH CHECK ADD  CONSTRAINT [FK_EducationLevelUpdate_Employee] FOREIGN KEY([EmployeeCode])
REFERENCES [dbo].[Employee] ([EmployeeCode])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[EducationLevelUpdate] CHECK CONSTRAINT [FK_EducationLevelUpdate_Employee]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK__Employee__ContractCode] FOREIGN KEY([ContractCode])
REFERENCES [dbo].[Contract] ([ContractCode])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK__Employee__ContractCode]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK__Employee__DepartmentCode] FOREIGN KEY([DepartmentCode])
REFERENCES [dbo].[Department] ([DepartmentCode])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK__Employee__DepartmentCode]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_EducationLevel] FOREIGN KEY([EducationLevelCode])
REFERENCES [dbo].[EducationLevel] ([EducationLevelCode])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_EducationLevel]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_EmployeePosition] FOREIGN KEY([EmployeePositionCode])
REFERENCES [dbo].[EmployeePosition] ([EmployeePositionCode])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_EmployeePosition]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Specialized] FOREIGN KEY([SpecializedCode])
REFERENCES [dbo].[Specialized] ([SpecializedCode])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Specialized]
GO
ALTER TABLE [dbo].[EmployeeRotation]  WITH CHECK ADD  CONSTRAINT [FK__EmployeeRotation__EmployeeCode] FOREIGN KEY([EmployeeCode])
REFERENCES [dbo].[Employee] ([EmployeeCode])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[EmployeeRotation] CHECK CONSTRAINT [FK__EmployeeRotation__EmployeeCode]
GO
ALTER TABLE [dbo].[ForeignLanguage]  WITH CHECK ADD  CONSTRAINT [FK_ForeignLanguage_Employee] FOREIGN KEY([EmployeeCode])
REFERENCES [dbo].[Employee] ([EmployeeCode])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ForeignLanguage] CHECK CONSTRAINT [FK_ForeignLanguage_Employee]
GO
ALTER TABLE [dbo].[QuitJob]  WITH CHECK ADD  CONSTRAINT [FK__QuitJob__EmployeeCode] FOREIGN KEY([EmployeeCode])
REFERENCES [dbo].[Employee] ([EmployeeCode])
GO
ALTER TABLE [dbo].[QuitJob] CHECK CONSTRAINT [FK__QuitJob__EmployeeCode]
GO
ALTER TABLE [dbo].[Salary]  WITH CHECK ADD  CONSTRAINT [FK_Salary_Employee] FOREIGN KEY([EmployeeCode])
REFERENCES [dbo].[Employee] ([EmployeeCode])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Salary] CHECK CONSTRAINT [FK_Salary_Employee]
GO
ALTER TABLE [dbo].[SalaryDetail]  WITH CHECK ADD  CONSTRAINT [FK_SalaryDetail_Employee] FOREIGN KEY([EmployeeCode])
REFERENCES [dbo].[Employee] ([EmployeeCode])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SalaryDetail] CHECK CONSTRAINT [FK_SalaryDetail_Employee]
GO
ALTER TABLE [dbo].[SalaryUpdate]  WITH CHECK ADD  CONSTRAINT [FK_SalaryUpdate_Employee] FOREIGN KEY([EmployeeCode])
REFERENCES [dbo].[Employee] ([EmployeeCode])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SalaryUpdate] CHECK CONSTRAINT [FK_SalaryUpdate_Employee]
GO
ALTER TABLE [dbo].[ScientificResearchTopics]  WITH CHECK ADD  CONSTRAINT [FK_ScientificResearchTopics_Employee] FOREIGN KEY([EmployeeCode])
REFERENCES [dbo].[Employee] ([EmployeeCode])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ScientificResearchTopics] CHECK CONSTRAINT [FK_ScientificResearchTopics_Employee]
GO
ALTER TABLE [dbo].[ScientificWorks]  WITH CHECK ADD  CONSTRAINT [FK_ScientificWorks_Employee] FOREIGN KEY([EmployeeCode])
REFERENCES [dbo].[Employee] ([EmployeeCode])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ScientificWorks] CHECK CONSTRAINT [FK_ScientificWorks_Employee]
GO
ALTER TABLE [dbo].[University]  WITH CHECK ADD  CONSTRAINT [FK_University_Employee] FOREIGN KEY([EmployeeCode])
REFERENCES [dbo].[Employee] ([EmployeeCode])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[University] CHECK CONSTRAINT [FK_University_Employee]
GO
ALTER TABLE [dbo].[WorkingProcess]  WITH CHECK ADD  CONSTRAINT [FK_WorkingProcess_Employee] FOREIGN KEY([EmployeeCode])
REFERENCES [dbo].[Employee] ([EmployeeCode])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[WorkingProcess] CHECK CONSTRAINT [FK_WorkingProcess_Employee]
GO
USE [master]
GO
ALTER DATABASE [HRMS] SET  READ_WRITE 
GO
