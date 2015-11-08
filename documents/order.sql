USE [order]
GO
/****** Object:  Table [dbo].[T_UserModule]    Script Date: 11/08/2015 19:56:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[T_UserModule](
	[ID] [bigint] NOT NULL,
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
/****** Object:  Table [dbo].[T_User]    Script Date: 11/08/2015 19:56:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_User](
	[UserName] [varchar](128) NOT NULL,
	[RealName] [nvarchar](500) NOT NULL,
	[password] [varchar](500) NOT NULL,
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
/****** Object:  Table [dbo].[T_Product]    Script Date: 11/08/2015 19:56:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_Product](
	[Id] [bigint] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[GameID] [bigint] NOT NULL,
	[Category] [nvarchar](100) NOT NULL,
	[Client] [nvarchar](100) NOT NULL,
	[Domain] [nvarchar](100) NULL,
	[Amount] [bigint] NOT NULL,
	[Description] [nchar](10) NULL,
	[OriginalPrice] [decimal](18, 3) NULL,
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
/****** Object:  Table [dbo].[T_PlayerCharge]    Script Date: 11/08/2015 19:56:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_PlayerCharge](
	[Id] [bigint] NOT NULL,
	[PlayerName] [nvarchar](100) NOT NULL,
	[RoleName] [nvarchar](100) NOT NULL,
	[Rank] [int] NOT NULL,
	[PreviousGoldAccount] [nvarchar](100) NOT NULL,
	[laterGoldAccount] [nvarchar](100) NOT NULL,
	[TradeTax] [decimal](18, 3) NOT NULL,
	[ProductTax] [decimal](18, 3) NOT NULL,
	[EquipmentName] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Table_PlayerCharge] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_Platform]    Script Date: 11/08/2015 19:56:31 ******/
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
/****** Object:  Table [dbo].[T_OrderState]    Script Date: 11/08/2015 19:56:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_OrderState](
	[ID] [int] NOT NULL,
	[State] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_T_OrderState] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_Order_Detail]    Script Date: 11/08/2015 19:56:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_Order_Detail](
	[ID] [bigint] NOT NULL,
	[ProductName] [nvarchar](100) NOT NULL,
	[GameID] [bigint] NOT NULL,
	[Numbers] [int] NOT NULL,
	[UnitPrice] [decimal](18, 3) NOT NULL,
	[TotalPrice] [decimal](18, 3) NOT NULL,
	[PayTime] [datetime] NOT NULL,
	[PayAccount] [nvarchar](100) NOT NULL,
	[GoldAccount] [bigint] NULL,
	[PayDeviceAccount] [nvarchar](100) NULL,
	[Pay4PlayerId] [bigint] NULL,
 CONSTRAINT [PK_Table_Order_Detail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'购买数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Order_Detail', @level2type=N'COLUMN',@level2name=N'Numbers'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'挂单元宝数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Order_Detail', @level2type=N'COLUMN',@level2name=N'GoldAccount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'对应游戏玩家player表id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Order_Detail', @level2type=N'COLUMN',@level2name=N'Pay4PlayerId'
GO
/****** Object:  Table [dbo].[T_Order_Base]    Script Date: 11/08/2015 19:56:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_Order_Base](
	[ID] [bigint] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[TotalPrice] [decimal](18, 4) NOT NULL,
	[StateID] [int] NOT NULL,
	[Source] [text] NOT NULL,
	[PayerName] [varchar](128) NOT NULL,
	[Comments] [ntext] NULL,
	[TransferAccountNumber] [nvarchar](100) NULL,
	[TransferredAmount] [decimal](18, 3) NULL,
	[TransferAccountTime] [datetime] NULL,
	[TransferMethod] [nvarchar](100) NULL,
	[BeneficiaryAccountNo] [nvarchar](100) NULL,
 CONSTRAINT [PK_T_Order_Base] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'充值人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Order_Base', @level2type=N'COLUMN',@level2name=N'PayerName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'转账订单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Order_Base', @level2type=N'COLUMN',@level2name=N'TransferAccountNumber'
GO
/****** Object:  Table [dbo].[T_Module]    Script Date: 11/08/2015 19:56:31 ******/
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
/****** Object:  Table [dbo].[T_Inventory]    Script Date: 11/08/2015 19:56:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_Inventory](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[AppId] [nvarchar](100) NOT NULL,
	[Balance] [decimal](18, 3) NOT NULL,
	[State] [int] NOT NULL,
	[LastChargeTime] [datetime] NOT NULL,
	[LastChargePrice] [decimal](18, 3) NOT NULL,
	[Comments] [nvarchar](500) NULL,
 CONSTRAINT [PK_inventory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'余额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Inventory', @level2type=N'COLUMN',@level2name=N'Balance'
GO
/****** Object:  Table [dbo].[T_Game]    Script Date: 11/08/2015 19:56:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_Game](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [varchar](500) NULL,
	[createtime] [datetime] NULL,
	[CreateUser] [varchar](128) NULL,
	[comment] [varchar](500) NULL,
 CONSTRAINT [PK_game] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[T_FaceValue]    Script Date: 11/08/2015 19:56:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_FaceValue](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[AppId] [bigint] NOT NULL,
	[RMB] [decimal](18, 3) NOT NULL,
	[ForeignCurrency] [decimal](18, 3) NOT NULL,
	[Notes] [nvarchar](500) NULL,
 CONSTRAINT [PK_facevalue] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_Commission]    Script Date: 11/08/2015 19:56:31 ******/
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
	[type] [int] NULL,
	[time] [datetime] NULL,
	[comment] [varchar](500) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'提成单价' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Commission', @level2type=N'COLUMN',@level2name=N'price'
GO
/****** Object:  Table [dbo].[T_Balance]    Script Date: 11/08/2015 19:56:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_Balance](
	[Id] [bigint] NOT NULL,
	[AppId] [nvarchar](100) NOT NULL,
	[Incoming] [decimal](18, 3) NOT NULL,
	[Outting] [decimal](18, 3) NOT NULL,
	[User] [nvarchar](100) NOT NULL,
	[OperationTime] [nchar](10) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[str_getnum]    Script Date: 11/08/2015 19:56:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[str_getnum] (@s varchar(5000))
returns char(11) as 
begin
 declare @start int;
 set @start=PATINDEX('%[1][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]%',@s);
  
 if @start=0
  return null;
   
 return substring(@s,@start,11);
end
GO
/****** Object:  UserDefinedFunction [dbo].[str_get]    Script Date: 11/08/2015 19:56:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[str_get](@src varchar(8000), @start varchar(8000),@end varchar(8000))
returns varchar(8000)
as
begin
	declare @sindex int,@eindex int,@rstr varchar(8000)

	set @src=ltrim(@src)
	set @start=ltrim(@start)
	set @end=ltrim(@end)

	set @src=rtrim(@src)
	set @start=rtrim(@start)
	set @end=rtrim(@end)
	if @src is null
	begin
		return ''
	end

	set @sindex=CHARINDEX(@start,@src)
	if @sindex>0
	begin
		set @sindex=@sindex+len(@start)
	end
	else
	begin
		return ''
	end

	set @eindex=CHARINDEX(@end,@src,@sindex)
	if @eindex<=0
	begin
		return ''
	end

	set @rstr=SubString(@src,@sindex,@eindex-@sindex)
	if @rstr is null
	begin
		return ''
	end
    return @rstr
end
GO
/****** Object:  Default [DF__T_Commissi__type__4D5F7D71]    Script Date: 11/08/2015 19:56:31 ******/
ALTER TABLE [dbo].[T_Commission] ADD  DEFAULT ((0)) FOR [type]
GO
/****** Object:  Default [DF__T_Commissi__time__4E53A1AA]    Script Date: 11/08/2015 19:56:31 ******/
ALTER TABLE [dbo].[T_Commission] ADD  DEFAULT (getdate()) FOR [time]
GO
/****** Object:  Default [DF__T_Game__createti__339FAB6E]    Script Date: 11/08/2015 19:56:31 ******/
ALTER TABLE [dbo].[T_Game] ADD  DEFAULT (getdate()) FOR [createtime]
GO
/****** Object:  Default [DF__T_Invento__money__45BE5BA9]    Script Date: 11/08/2015 19:56:31 ******/
ALTER TABLE [dbo].[T_Inventory] ADD  CONSTRAINT [DF__T_Invento__money__45BE5BA9]  DEFAULT ((0.00)) FOR [Balance]
GO
/****** Object:  Default [DF__T_Invento__state__46B27FE2]    Script Date: 11/08/2015 19:56:31 ******/
ALTER TABLE [dbo].[T_Inventory] ADD  CONSTRAINT [DF__T_Invento__state__46B27FE2]  DEFAULT ((0)) FOR [State]
GO
/****** Object:  Default [DF__T_Invento__soldt__47A6A41B]    Script Date: 11/08/2015 19:56:31 ******/
ALTER TABLE [dbo].[T_Inventory] ADD  CONSTRAINT [DF__T_Invento__soldt__47A6A41B]  DEFAULT (getdate()) FOR [LastChargeTime]
GO
/****** Object:  Default [DF__T_User__SystemRo__57DD0BE4]    Script Date: 11/08/2015 19:56:31 ******/
ALTER TABLE [dbo].[T_User] ADD  DEFAULT ((0)) FOR [SystemRole]
GO
/****** Object:  Default [DF__T_User__Status__58D1301D]    Script Date: 11/08/2015 19:56:31 ******/
ALTER TABLE [dbo].[T_User] ADD  DEFAULT ((0)) FOR [Status]
GO
/****** Object:  Default [DF__T_User__CreateTi__59C55456]    Script Date: 11/08/2015 19:56:31 ******/
ALTER TABLE [dbo].[T_User] ADD  DEFAULT (getdate()) FOR [CreateTime]
GO
