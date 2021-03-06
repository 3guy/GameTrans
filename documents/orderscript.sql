USE [order]
GO
/****** Object:  Table [dbo].[T_UserModule]    Script Date: 11/09/2015 22:33:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[T_UserModule](
	[ID] [bigint] identity(1, 1) NOT NULL,
	[UserName] [varchar](128) NOT NULL,
	[ModuleID] [bigint] NOT NULL,
 CONSTRAINT [PK_UserModule] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[T_User]    Script Date: 11/09/2015 22:33:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_User](
	[UserName] [varchar](128) NOT NULL,
	[RealName] [nvarchar](512) NOT NULL,
	[password] [varchar](512) NOT NULL,
	[HandPhone] [varchar](11) NULL,
	[TelePhone] [varchar](11) NULL,
	[SystemRole] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[CreateTime] [datetime] NULL,
	[CreateUser] [varchar](128) NOT NULL,
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[T_Product]    Script Date: 11/09/2015 22:33:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_Product](
	[Id] [bigint] identity(1, 1) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[OrderID] [bigint] default(0) not null,
	[GameID] [bigint] NOT NULL,
	[Category] [nvarchar](64) NOT NULL,
	[Client] [nvarchar](128) NOT NULL,
	[Domain] [nvarchar](128) NULL,
	[Amount] [bigint] NOT NULL,
	[Description] [nvarchar](512) NULL,
	[OriginalPrice] [decimal](18, 2) NULL,
 CONSTRAINT [PK_Table_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户端' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Product', @level2type=N'COLUMN',@level2name=N'Client'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服务器' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Product', @level2type=N'COLUMN',@level2name=N'Domain'
GO
/****** Object:  Table [dbo].[T_Player]    Script Date: 11/09/2015 22:33:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_Player](
	[Id] [bigint] identity(1, 1) NOT NULL,
	[PlayerName] [nvarchar](128) NOT NULL,
	[RoleName] [nvarchar](128) NOT NULL,
	[Rank] [int] NOT NULL,
	[PreviousGoldAccount] [bigint] NOT NULL,
	[laterGoldAccount] [bigint] NOT NULL,
	[TradeTax] [decimal](18, 2) NOT NULL,
	[ProductTax] [decimal](18, 2) NOT NULL,
	[EquipmentName] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_Table_PlayerCharge] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_OrderStateHistory]    Script Date: 11/09/2015 22:33:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_OrderStateHistory](
	[ID] [bigint] identity(1, 1) NOT NULL,
	[PreviousState] int default(0) not NULL,
	[CurrentState] int default(0) not NULL,
	[CreateTime] [datetime] not NULL,
	[Creater] [nvarchar](128) NULL,
 CONSTRAINT [PK_T_OrderState] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_OrderDetail]    Script Date: 11/09/2015 22:33:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_OrderDetail](
	[ID] [bigint] identity(1, 1) NOT NULL,
	[OrderID] [bigint] default(0) not null,
	[ProductName] [nvarchar](128) NOT NULL,
	[Numbers] [int] NOT NULL,
	[UnitPrice] [decimal](18, 2) NOT NULL,
	[TotalPrice] [decimal](18, 2) NOT NULL,
	[PayTime] [datetime] NOT NULL,
	[PayAccount] [nvarchar](128) NOT NULL,
	[GoldAccount] [bigint] NULL,
	[PayDeviceAccount] [nvarchar](128) NULL,
	[Pay4PlayerId] [bigint] NULL,
 CONSTRAINT [PK_Table_Order_Detail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'购买数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_OrderDetail', @level2type=N'COLUMN',@level2name=N'Numbers'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'挂单元宝数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_OrderDetail', @level2type=N'COLUMN',@level2name=N'GoldAccount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'对应游戏玩家player表id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_OrderDetail', @level2type=N'COLUMN',@level2name=N'Pay4PlayerId'
GO
/****** Object:  Table [dbo].[T_OrderBase]    Script Date: 11/09/2015 22:33:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_OrderBase](
	[ID] [bigint] identity(1, 1) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[TotalPrice] [decimal](18, 2) NOT NULL,
	[State] [int] default(0) NOT NULL,
	[Source] [text] NOT NULL,
	[PayerName] [varchar](128) NOT NULL,
	[Comments] [ntext] NULL,
	[TransferAccountNumber] [nvarchar](128) NULL,
	[TransferredAmount] [decimal](18, 2) NULL,
	[TransferAccountTime] [datetime] NULL,
	[TransferMethod] [nvarchar](128) NULL,
	[BeneficiaryAccountNo] [nvarchar](128) NULL,
	[CreaterUser] [nvarchar](128) NULL,
 CONSTRAINT [PK_T_OrderBase] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'交易猫/ 手动下单，登录user name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_OrderBase', @level2type=N'COLUMN',@level2name=N'Source'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'充值人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_OrderBase', @level2type=N'COLUMN',@level2name=N'PayerName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'转账订单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_OrderBase', @level2type=N'COLUMN',@level2name=N'TransferAccountNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_OrderBase', @level2type=N'COLUMN',@level2name=N'CreaterUser'
GO
/****** Object:  Table [dbo].[T_Module]    Script Date: 11/09/2015 22:33:45 ******/
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
/****** Object:  Table [dbo].[T_Game]    Script Date: 11/09/2015 22:33:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_Game](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [varchar](512) NOT NULL,
	[state] int default(0) not null,
	[createtime] [datetime] NOT NULL,
	[CreateUser] [varchar](128) NOT NULL,
	[comment] [varchar](500) NULL,
 CONSTRAINT [PK_game] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[T_FaceValue]    Script Date: 11/09/2015 22:33:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_FaceValue](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Price] [int] NOT NULL,
 CONSTRAINT [PK_facevalue] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_Balance]    Script Date: 11/09/2015 22:33:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_Balance](
	[Id] [bigint] identity(1, 1) NOT NULL,
	[AppId] [nvarchar](128) NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[UserName] [nvarchar](128) NOT NULL,
	[OperationTime] [datetime] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_AccountRate]    Script Date: 11/09/2015 22:33:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_AccountRate](
	[Id] [bigint] identity(1, 1) NOT NULL,
	[AppId] [nvarchar](128) NOT NULL,
	[Rate] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_T_Rate] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'汇率' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_AccountRate', @level2type=N'COLUMN',@level2name=N'Rate'
GO
/****** Object:  Table [dbo].[T_Account]    Script Date: 11/09/2015 22:33:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_Account](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[AppId] [nvarchar](128) NOT NULL,
	[Balance] [decimal](18, 2) NOT NULL,
	[State] [int] NOT NULL,
	[Comments] [nvarchar](512) NULL,
 CONSTRAINT [PK_inventory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'余额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Account', @level2type=N'COLUMN',@level2name=N'Balance'
GO
/****** Object:  Table [dbo].[T_CheckCodeStream]    Script Date: 11/13/2015 15:19:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[T_CheckCodeStream](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](128) NULL,
	[Mobile] [varchar](11) NULL,
	[Code] [varchar](32) NULL,
	[Type] [int] NOT NULL,
	[RecordDate] [datetime] NOT NULL,
	[ExpireDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.T_CheckCodeStream] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Default [DF__T_Invento__money__45BE5BA9]    Script Date: 11/09/2015 22:33:45 ******/
ALTER TABLE [dbo].[T_Account] ADD  CONSTRAINT [DF__T_Invento__money__45BE5BA9]  DEFAULT ((0.00)) FOR [Balance]
GO
/****** Object:  Default [DF__T_Invento__state__46B27FE2]    Script Date: 11/09/2015 22:33:45 ******/
ALTER TABLE [dbo].[T_Account] ADD  CONSTRAINT [DF__T_Invento__state__46B27FE2]  DEFAULT ((0)) FOR [State]
GO
/****** Object:  Default [DF__T_Game__createti__339FAB6E]    Script Date: 11/09/2015 22:33:45 ******/
ALTER TABLE [dbo].[T_Game] ADD  CONSTRAINT [DF__T_Game__createti__339FAB6E]  DEFAULT (getdate()) FOR [createtime]
GO
/****** Object:  Default [DF__T_User__SystemRo__57DD0BE4]    Script Date: 11/09/2015 22:33:45 ******/
ALTER TABLE [dbo].[T_User] ADD  CONSTRAINT [DF__T_User__SystemRo__57DD0BE4]  DEFAULT ((0)) FOR [SystemRole]
GO
/****** Object:  Default [DF__T_User__Status__58D1301D]    Script Date: 11/09/2015 22:33:45 ******/
ALTER TABLE [dbo].[T_User] ADD  CONSTRAINT [DF__T_User__Status__58D1301D]  DEFAULT ((0)) FOR [Status]
GO
/****** Object:  Default [DF__T_User__CreateTi__59C55456]    Script Date: 11/09/2015 22:33:45 ******/
ALTER TABLE [dbo].[T_User] ADD  CONSTRAINT [DF__T_User__CreateTi__59C55456]  DEFAULT (getdate()) FOR [CreateTime]
GO

