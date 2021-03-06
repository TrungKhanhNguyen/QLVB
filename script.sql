USE [master]
GO
/****** Object:  Database [QLVB]    Script Date: 2/7/2020 9:25:09 AM ******/
CREATE DATABASE [QLVB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QLVB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\QLVB.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'QLVB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\QLVB_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [QLVB] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QLVB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QLVB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QLVB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QLVB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QLVB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QLVB] SET ARITHABORT OFF 
GO
ALTER DATABASE [QLVB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [QLVB] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [QLVB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QLVB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QLVB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QLVB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QLVB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QLVB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QLVB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QLVB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QLVB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [QLVB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QLVB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QLVB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QLVB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QLVB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QLVB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QLVB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QLVB] SET RECOVERY FULL 
GO
ALTER DATABASE [QLVB] SET  MULTI_USER 
GO
ALTER DATABASE [QLVB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QLVB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QLVB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QLVB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'QLVB', N'ON'
GO
USE [QLVB]
GO
/****** Object:  StoredProcedure [dbo].[danhsachCVden]    Script Date: 2/7/2020 9:25:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[danhsachCVden]
	
AS
BEGIN 
	SELECT
       * FROM tblCVden
END

GO
/****** Object:  StoredProcedure [dbo].[danhsachCVdi]    Script Date: 2/7/2020 9:25:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[danhsachCVdi]
	
AS
BEGIN 
	SELECT
       * FROM tblCVdi
END

GO
/****** Object:  StoredProcedure [dbo].[DELETECVden]    Script Date: 2/7/2020 9:25:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DELETECVden]
@socongvan nvarchar(50)=NULL
AS
BEGIN
	DELETE tblCVden WHERE socongvan =@socongvan
END

GO
/****** Object:  StoredProcedure [dbo].[DELETECVdi]    Script Date: 2/7/2020 9:25:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DELETECVdi]
@socongvan nvarchar(50)=NULL
AS
BEGIN
	DELETE tblCVdi WHERE socongvan =@socongvan
END

GO
/****** Object:  StoredProcedure [dbo].[THEMCVden]    Script Date: 2/7/2020 9:25:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[THEMCVden]
	--@maTB int=NULL,
	
	@ngaythang DATETIME=NULL,
	@socongvan NVARCHAR(50)= NULL,
	@noidung NVARCHAR(MAX)=NULL,
	@noigui NVARCHAR(500)=NULL,
	@nguoiky NVARCHAR(50)=NULL,
	@ngaychidao DATETIME=NULL,
	@ykienchidao NVARCHAR(MAX)=NULL,
	@ghichu NVARCHAR(MAX)=NULL,
	@anhscan NVARCHAR(MAX)=NULL
AS
BEGIN
INSERT INTO tblCVden(ngaythang,socongvan,noidung,noigui,nguoiky,ngaychidao,ykienchidao,ghichu,anhscan)
		VALUES (@ngaythang,@socongvan,@noidung,@noigui,@nguoiky,@ngaychidao,@ykienchidao,@ghichu,@anhscan)
END

GO
/****** Object:  StoredProcedure [dbo].[THEMCVdi]    Script Date: 2/7/2020 9:25:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[THEMCVdi]
	--@maTB int=NULL,
	
	@socongvan NVARCHAR(50)= NULL,
	@ngaygui DATETIME=NULL,
	@noidung NVARCHAR(MAX)=NULL,
	@noinhan NVARCHAR(500)=NULL,
	@ghichu NVARCHAR(MAX)=NULL,
	@anhscan NVARCHAR(MAX)=NULL
AS
BEGIN
INSERT INTO tblCVdi(socongvan,ngaygui,noidung,noinhan,ghichu,anhscan)
		VALUES (@socongvan,@ngaygui,@noidung,@noinhan,@ghichu,@anhscan)
END

GO
/****** Object:  StoredProcedure [dbo].[timkiemCVden]    Script Date: 2/7/2020 9:25:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[timkiemCVden] 
	-- Add the parameters for the stored procedure here
	@searchText NVARCHAR(200)=NULL,
	@searchType NVARCHAR(200)= NULL
AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	IF (@searchText is null)
		begin
			select * from tblCVden 
		end
	ELSE IF (@searchType is null)
		begin
			select * from tblCVden where noidung LIKE '%' + @searchText + '%'
		end
	ELSE 
		begin
			select * from tblCVden where socongvan LIKE '%' + @searchText + '%'
		end

GO
/****** Object:  StoredProcedure [dbo].[timkiemCVdi]    Script Date: 2/7/2020 9:25:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[timkiemCVdi] 
	-- Add the parameters for the stored procedure here
	@searchText NVARCHAR(200)=NULL,
	@searchType NVARCHAR(200)= NULL
AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	IF (@searchText is null)
		begin
			select * from tblCVdi 
		end
	ELSE IF (@searchType is null)
		begin
			select * from tblCVdi where noidung LIKE '%' + @searchText + '%'
		end
	ELSE 
		begin
			select * from tblCVdi where socongvan LIKE '%' + @searchText + '%'
		end

GO
/****** Object:  StoredProcedure [dbo].[UPDATECVden]    Script Date: 2/7/2020 9:25:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[UPDATECVden]
	
	@ngaythang DATETIME=NULL,
	@socongvan NVARCHAR(50)= NULL,
	@noidung NVARCHAR(MAX)=NULL,
	@noigui NVARCHAR(500)=NULL,
	@nguoiky NVARCHAR(50)=NULL,
	@ngaychidao DATETIME=NULL,
	@ykienchidao NVARCHAR(MAX)=NULL,
	@ghichu NVARCHAR(MAX)=NULL,
	@anhscan NVARCHAR(MAX)=NULL
AS
BEGIN
	UPDATE tblCVden SET ngaythang=@ngaythang,noidung=@noidung,
	noigui=@noigui,nguoiky=@nguoiky,ngaychidao=@ngaychidao,ykienchidao=@ykienchidao,ghichu=@ghichu,anhscan=@anhscan WHERE socongvan=@socongvan
END

GO
/****** Object:  StoredProcedure [dbo].[UPDATECVdi]    Script Date: 2/7/2020 9:25:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[UPDATECVdi]
	
	@ngaygui DATETIME=NULL,
	@socongvan NVARCHAR(50)= NULL,
	@noidung NVARCHAR(MAX)=NULL,
	@noinhan NVARCHAR(500)=NULL,
	@ghichu NVARCHAR(MAX)=NULL,
	@anhscan NVARCHAR(MAX)=NULL
AS
BEGIN
	UPDATE tblCVdi SET ngaygui=@ngaygui,noidung=@noidung,
	noinhan=@noinhan,ghichu=@ghichu,anhscan=@anhscan WHERE socongvan=@socongvan
END

GO
/****** Object:  Table [dbo].[CategoryVB]    Script Date: 2/7/2020 9:25:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoryVB](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NameCate] [nvarchar](200) NULL,
	[Active] [bit] NULL,
 CONSTRAINT [PK_CategoryVB] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NhanVien]    Script Date: 2/7/2020 9:25:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhanVien](
	[maVB] [float] NULL,
	[ngayden] [nvarchar](255) NULL,
	[nguoinhap] [nvarchar](255) NULL,
	[ngayduyet] [nvarchar](255) NULL,
	[noigui] [nvarchar](255) NULL,
	[noinhan] [datetime] NULL,
	[noidung] [nvarchar](255) NULL,
	[ykienchidao] [nvarchar](255) NULL,
	[ghichu] [nvarchar](255) NULL,
	[anhscan] [nvarchar](255) NULL,
	[tenVB] [nvarchar](255) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblCVden]    Script Date: 2/7/2020 9:25:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCVden](
	[STT] [int] IDENTITY(1,1) NOT NULL,
	[ngaythang] [datetime] NULL,
	[socongvan] [nvarchar](50) NULL,
	[noidung] [nvarchar](max) NULL,
	[noigui] [nvarchar](500) NULL,
	[nguoiky] [nvarchar](50) NULL,
	[ngaychidao] [datetime] NULL,
	[ykienchidao] [nvarchar](max) NULL,
	[ghichu] [nvarchar](max) NULL,
	[anhscan] [nvarchar](max) NULL,
	[ngayhethan] [datetime] NULL,
	[Daxuly] [bit] NULL,
	[nguoixuly] [nvarchar](50) NULL,
	[CategoryId] [nvarchar](50) NULL,
 CONSTRAINT [PK_tblCVden] PRIMARY KEY CLUSTERED 
(
	[STT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblCVdi]    Script Date: 2/7/2020 9:25:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCVdi](
	[STT] [int] IDENTITY(1,1) NOT NULL,
	[socongvan] [nvarchar](50) NOT NULL,
	[ngaygui] [datetime] NULL,
	[noidung] [nvarchar](max) NULL,
	[noinhan] [nvarchar](500) NULL,
	[ghichu] [nvarchar](max) NULL,
	[anhscan] [nvarchar](max) NULL,
 CONSTRAINT [PK_tblCVdi] PRIMARY KEY CLUSTERED 
(
	[STT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblLogin]    Script Date: 2/7/2020 9:25:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblLogin](
	[userName] [nvarchar](50) NOT NULL,
	[passWord] [nvarchar](50) NOT NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NULL,
 CONSTRAINT [PK_tblLogin] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[NhanVien] ([maVB], [ngayden], [nguoinhap], [ngayduyet], [noigui], [noinhan], [noidung], [ykienchidao], [ghichu], [anhscan], [tenVB]) VALUES (1, N'3504/BCA-QĐ-A70', N'dfadfads', N'Tổng cục An ninh', N'sdfff', CAST(0x0000A6FE00000000 AS DateTime), N'fdsafasdf', N'fdsaf', N'fdsaf', N'fdsaf', N'fdsaf')
INSERT [dbo].[NhanVien] ([maVB], [ngayden], [nguoinhap], [ngayduyet], [noigui], [noinhan], [noidung], [ykienchidao], [ghichu], [anhscan], [tenVB]) VALUES (2, N'fdsafasdf', NULL, NULL, N'Nguyễn Thị Hương', NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[tblCVdi] ON 

INSERT [dbo].[tblCVdi] ([STT], [socongvan], [ngaygui], [noidung], [noinhan], [ghichu], [anhscan]) VALUES (2, N'QĐ/1234', CAST(0x0000A713010ADDF8 AS DateTime), N'Cử cán bộ học lớp An ninh mạng', N'Tổng cục An ninh - BCA', N'không có ghi chú', N'E:\Lập trình C#')
INSERT [dbo].[tblCVdi] ([STT], [socongvan], [ngaygui], [noidung], [noinhan], [ghichu], [anhscan]) VALUES (3, N'AD/3423', CAST(0x0000A713010D79D1 AS DateTime), N'Đi học lớp Cơ sở dữ liệu', N'A73', N'Không có', N'E:\Lập trình C#')
INSERT [dbo].[tblCVdi] ([STT], [socongvan], [ngaygui], [noidung], [noinhan], [ghichu], [anhscan]) VALUES (4, N'aa', CAST(0x0000982E00000000 AS DateTime), N'aa', N'aaa', N'aaa', N'C:\Users\Public\Pictures\Sample Pictures')
INSERT [dbo].[tblCVdi] ([STT], [socongvan], [ngaygui], [noidung], [noinhan], [ghichu], [anhscan]) VALUES (5, N'bbb', CAST(0x0000982E00000000 AS DateTime), N'bb', N'bbb', N'bbbb', N'C:\Users\Public\Pictures\Sample Pictures')
SET IDENTITY_INSERT [dbo].[tblCVdi] OFF
SET IDENTITY_INSERT [dbo].[tblLogin] ON 

INSERT [dbo].[tblLogin] ([userName], [passWord], [id], [RoleId]) VALUES (N'huongnt', N'508df4cb2f4d8f80519256258cfb975f', 1, 1)
INSERT [dbo].[tblLogin] ([userName], [passWord], [id], [RoleId]) VALUES (N'admin', N'e10adc3949ba59abbe56e057f20f883e', 2, 2)
INSERT [dbo].[tblLogin] ([userName], [passWord], [id], [RoleId]) VALUES (N'viewer', N'e10adc3949ba59abbe56e057f20f883e', 6, 1)
SET IDENTITY_INSERT [dbo].[tblLogin] OFF
ALTER TABLE [dbo].[CategoryVB] ADD  CONSTRAINT [DF_CategoryVB_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[tblCVdi] ADD  CONSTRAINT [DF_tblCVdi_nguoinhap]  DEFAULT (N'Nguyễn Thị Hương') FOR [ngaygui]
GO
USE [master]
GO
ALTER DATABASE [QLVB] SET  READ_WRITE 
GO
