USE [Order]
GO
/****** Object:  Table [dbo].[T_Game]    Script Date: 10/27/2015 23:06:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_Game](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [varchar](500) NULL,
	[createtime] [datetime] default(getdate()) NULL,
	[CreateUser] varchar(128) null,
	[comment] [varchar](500) NULL,
 CONSTRAINT [PK_game] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[T_User]    Script Date: 10/27/2015 23:06:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_User](
	[UserName] varchar(128) NOT NULL,
	[RealName] [nvarchar](500) NOT NULL,
	[password] [varchar](500) NOT NULL,
	[HandPhone] [varchar](11) NULL,
	[TelePhone] [varchar](11) NULL,
	[SystemRole] [int] default(0) not null,
	[Status] [int] default(0) not null,
	[CreateTime] [datetime] default(getdate()) NULL,
	[CreateUser] [varchar](128) NOT NULL,
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


/****** Object:  Table [dbo].[T_ProcessOrder]    Script Date: 10/27/2015 23:06:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_ProcessOrder](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[userid] [bigint] NULL,
	[orderid] [bigint] NULL,
	[pdid] [bigint] NULL,
	[money] [decimal](18, 3) default(0.00) NULL,
	[price] [decimal](18, 3) default(0.00) NULL,
	[note] [varchar](max) NULL,
	[datetime] [datetime] default(getdate()) NULL,
 CONSTRAINT [PK_processOrders] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[T_Platform]    Script Date: 10/27/2015 23:06:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_Platform](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[user] [varchar](500) NULL,
	[password] [varchar](500) NULL,
	[Gameplatform] [varchar](50) NULL,
 CONSTRAINT [PK_platform] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[T_Order]    Script Date: 10/27/2015 23:06:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_Order](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[orderCode] [varchar](500) NULL,
	[details] [ntext] NULL,
	[OrderStatus] [int] default(0) NULL,
	[commissions] [decimal](18, 2) NULL,
	[UserName] varchar(128) NOT NULL,
	[createtime] [datetime] default(getdate()) NULL,
	[comments] [text] NULL,
 CONSTRAINT [PK_order] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'等待充值 正在充值 充值成功 取消充值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Order', @level2type=N'COLUMN',@level2name=N'OrderStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单导入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Order', @level2type=N'COLUMN',@level2name=N'createtime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Order'
GO
/****** Object:  Table [dbo].[T_Inventory]    Script Date: 10/27/2015 23:06:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_Inventory](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[appid] [bigint] NOT NULL,
	[money] [decimal](18, 2) default(0.00) NULL,
	[state] int default(0) NULL,
	[soldtime] [datetime] default(getdate()) NULL,
 CONSTRAINT [PK_inventory] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[T_Module]    Script Date: 10/30/2015 20:37:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[T_Module](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](8000) NULL,
	[Name] [nvarchar](64) NULL,
	[Level] [int] NOT NULL,
	[Url] [varchar](128) NULL,
	[ParentID] [int] NOT NULL,
	[SeqNo] [int] NOT NULL,
	[Icon] [varchar](50) NULL,
	[IsDisplay] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.T_Module] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[T_UserModule]    Script Date: 10/27/2015 23:06:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_UserModule](
    [ID] [bigint] not null,
	[UserName] varchar(128) NOT NULL,
	[ModuleID] [bigint] NOT NULL,
 CONSTRAINT [PK_UserModule] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_Commission]    Script Date: 10/27/2015 23:06:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_Commission](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[gameid] [bigint] NOT NULL,
	[price] [decimal](18, 2) NULL,
	[type] [int] default(0) NULL,
	[time] [datetime] default(getdate()) NULL,
	[comment] [varchar](500) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'提成单价' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Commission', @level2type=N'COLUMN',@level2name=N'price'
GO
/****** Object:  Table [dbo].[T_Facevalue]    Script Date: 10/27/2015 23:06:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_FaceValue](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[inventoryid] [bigint] NOT NULL,
	[gamename] [nvarchar](500) NULL,
	[price] [decimal](18, 2) NOT NULL,
	[value] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_facevalue] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO