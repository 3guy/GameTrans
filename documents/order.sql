USE [Order]
GO
/****** Object:  Table [dbo].[game]    Script Date: 10/27/2015 23:06:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[game](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [varchar](500) NULL,
	[createtime] [datetime] NULL,
	[comment] [varchar](500) NULL,
 CONSTRAINT [PK_game] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[user]    Script Date: 10/27/2015 23:06:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[user](
	[userId] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [varchar](500) NOT NULL,
	[password] [varchar](500) NOT NULL,
	[registertime] [datetime] NULL,
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[sysdiagrams]    Script Date: 10/27/2015 23:06:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[sysdiagrams](
	[name] [nvarchar](128) NOT NULL,
	[principal_id] [int] NOT NULL,
	[diagram_id] [int] IDENTITY(1,1) NOT NULL,
	[version] [int] NULL,
	[definition] [varbinary](max) NULL,
 CONSTRAINT [PK_sysdiagrams] PRIMARY KEY CLUSTERED 
(
	[diagram_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  UserDefinedFunction [dbo].[str_getnum]    Script Date: 10/27/2015 23:06:37 ******/
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
/****** Object:  UserDefinedFunction [dbo].[str_get]    Script Date: 10/27/2015 23:06:37 ******/
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
/****** Object:  Table [dbo].[role]    Script Date: 10/27/2015 23:06:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[role](
	[roleId] [bigint] NOT NULL,
	[name] [varchar](50) NOT NULL,
	[description] [nvarchar](500) NULL,
 CONSTRAINT [PK_role] PRIMARY KEY CLUSTERED 
(
	[roleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[processOrder]    Script Date: 10/27/2015 23:06:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[processOrder](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[userid] [bigint] NULL,
	[orderid] [bigint] NULL,
	[pdid] [bigint] NULL,
	[money] [decimal](18, 3) NULL,
	[price] [decimal](18, 3) NULL,
	[note] [varchar](max) NULL,
	[datetime] [datetime] NULL,
 CONSTRAINT [PK_processOrders] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[platform]    Script Date: 10/27/2015 23:06:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[platform](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[user] [varchar](500) NULL,
	[password] [varchar](500) NULL,
	[platform] [varchar](50) NULL,
 CONSTRAINT [PK_platform] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[order]    Script Date: 10/27/2015 23:06:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[order](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[orderid] [varchar](500) NULL,
	[details] [ntext] NULL,
	[state] [varchar](50) NULL,
	[commissions] [decimal](18, 3) NULL,
	[userId] [bigint] NOT NULL,
	[createtime] [datetime] NULL,
	[comments] [text] NULL,
 CONSTRAINT [PK_order] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'等待充值 正在充值 充值成功 取消充值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'order', @level2type=N'COLUMN',@level2name=N'state'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单导入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'order', @level2type=N'COLUMN',@level2name=N'createtime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'order'
GO
/****** Object:  Table [dbo].[inventory]    Script Date: 10/27/2015 23:06:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[inventory](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[appid] [bigint] NOT NULL,
	[money] [decimal](18, 3) NULL,
	[state] [varchar](500) NULL,
	[soldtime] [datetime] NULL,
 CONSTRAINT [PK_inventory] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[user_to_role]    Script Date: 10/27/2015 23:06:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user_to_role](
	[userId] [bigint] NOT NULL,
	[roleId] [bigint] NOT NULL,
 CONSTRAINT [PK_user_to_role_1] PRIMARY KEY CLUSTERED 
(
	[userId] ASC,
	[roleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[commission]    Script Date: 10/27/2015 23:06:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[commission](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[gameid] [bigint] NOT NULL,
	[price] [decimal](18, 3) NULL,
	[type] [varchar](500) NULL,
	[time] [datetime] NULL,
	[comment] [varchar](500) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'提成单价' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'commission', @level2type=N'COLUMN',@level2name=N'price'
GO
/****** Object:  Table [dbo].[facevalue]    Script Date: 10/27/2015 23:06:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[facevalue](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[inventoryid] [bigint] NOT NULL,
	[gamename] [varchar](500) NULL,
	[price] [decimal](18, 3) NOT NULL,
	[value] [decimal](18, 3) NOT NULL,
 CONSTRAINT [PK_facevalue] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Default [DF_commission_type]    Script Date: 10/27/2015 23:06:36 ******/
ALTER TABLE [dbo].[commission] ADD  CONSTRAINT [DF_commission_type]  DEFAULT ('其它') FOR [type]
GO
/****** Object:  Default [DF_commission_time]    Script Date: 10/27/2015 23:06:36 ******/
ALTER TABLE [dbo].[commission] ADD  CONSTRAINT [DF_commission_time]  DEFAULT (getdate()) FOR [time]
GO
/****** Object:  Default [DF_facevalue_price]    Script Date: 10/27/2015 23:06:36 ******/
ALTER TABLE [dbo].[facevalue] ADD  CONSTRAINT [DF_facevalue_price]  DEFAULT ((0)) FOR [price]
GO
/****** Object:  Default [DF_facevalue_value]    Script Date: 10/27/2015 23:06:36 ******/
ALTER TABLE [dbo].[facevalue] ADD  CONSTRAINT [DF_facevalue_value]  DEFAULT ((0)) FOR [value]
GO
/****** Object:  Default [DF_game_time]    Script Date: 10/27/2015 23:06:36 ******/
ALTER TABLE [dbo].[game] ADD  CONSTRAINT [DF_game_time]  DEFAULT (getdate()) FOR [createtime]
GO
/****** Object:  Default [DF_inventory_money]    Script Date: 10/27/2015 23:06:36 ******/
ALTER TABLE [dbo].[inventory] ADD  CONSTRAINT [DF_inventory_money]  DEFAULT ((0)) FOR [money]
GO
/****** Object:  Default [DF_inventory_state]    Script Date: 10/27/2015 23:06:36 ******/
ALTER TABLE [dbo].[inventory] ADD  CONSTRAINT [DF_inventory_state]  DEFAULT ('启用') FOR [state]
GO
/****** Object:  Default [DF_inventory_time]    Script Date: 10/27/2015 23:06:36 ******/
ALTER TABLE [dbo].[inventory] ADD  CONSTRAINT [DF_inventory_time]  DEFAULT (getdate()) FOR [soldtime]
GO
/****** Object:  Default [DF_order_state]    Script Date: 10/27/2015 23:06:36 ******/
ALTER TABLE [dbo].[order] ADD  CONSTRAINT [DF_order_state]  DEFAULT ('等待充值') FOR [state]
GO
/****** Object:  Default [DF_order_commissions]    Script Date: 10/27/2015 23:06:36 ******/
ALTER TABLE [dbo].[order] ADD  CONSTRAINT [DF_order_commissions]  DEFAULT ((0.5)) FOR [commissions]
GO
/****** Object:  Default [DF_order_time]    Script Date: 10/27/2015 23:06:36 ******/
ALTER TABLE [dbo].[order] ADD  CONSTRAINT [DF_order_time]  DEFAULT (getdate()) FOR [createtime]
GO
/****** Object:  Default [DF_user_time]    Script Date: 10/27/2015 23:06:36 ******/
ALTER TABLE [dbo].[user] ADD  CONSTRAINT [DF_user_time]  DEFAULT (getdate()) FOR [registertime]
GO
/****** Object:  ForeignKey [FK_commission_game]    Script Date: 10/27/2015 23:06:36 ******/
ALTER TABLE [dbo].[commission]  WITH CHECK ADD  CONSTRAINT [FK_commission_game] FOREIGN KEY([gameid])
REFERENCES [dbo].[game] ([id])
GO
ALTER TABLE [dbo].[commission] CHECK CONSTRAINT [FK_commission_game]
GO
/****** Object:  ForeignKey [FK_facevalue_inventory]    Script Date: 10/27/2015 23:06:36 ******/
ALTER TABLE [dbo].[facevalue]  WITH CHECK ADD  CONSTRAINT [FK_facevalue_inventory] FOREIGN KEY([inventoryid])
REFERENCES [dbo].[inventory] ([id])
GO
ALTER TABLE [dbo].[facevalue] CHECK CONSTRAINT [FK_facevalue_inventory]
GO
/****** Object:  ForeignKey [FK_inventory_game]    Script Date: 10/27/2015 23:06:36 ******/
ALTER TABLE [dbo].[inventory]  WITH CHECK ADD  CONSTRAINT [FK_inventory_game] FOREIGN KEY([appid])
REFERENCES [dbo].[game] ([id])
GO
ALTER TABLE [dbo].[inventory] CHECK CONSTRAINT [FK_inventory_game]
GO
/****** Object:  ForeignKey [FK_order_user]    Script Date: 10/27/2015 23:06:36 ******/
ALTER TABLE [dbo].[order]  WITH CHECK ADD  CONSTRAINT [FK_order_user] FOREIGN KEY([userId])
REFERENCES [dbo].[user] ([userId])
GO
ALTER TABLE [dbo].[order] CHECK CONSTRAINT [FK_order_user]
GO
/****** Object:  ForeignKey [FK_user_to_role__role]    Script Date: 10/27/2015 23:06:36 ******/
ALTER TABLE [dbo].[user_to_role]  WITH CHECK ADD  CONSTRAINT [FK_user_to_role__role] FOREIGN KEY([roleId])
REFERENCES [dbo].[role] ([roleId])
GO
ALTER TABLE [dbo].[user_to_role] CHECK CONSTRAINT [FK_user_to_role__role]
GO
/****** Object:  ForeignKey [FK_user_to_role_role]    Script Date: 10/27/2015 23:06:36 ******/
ALTER TABLE [dbo].[user_to_role]  WITH CHECK ADD  CONSTRAINT [FK_user_to_role_role] FOREIGN KEY([userId])
REFERENCES [dbo].[user] ([userId])
GO
ALTER TABLE [dbo].[user_to_role] CHECK CONSTRAINT [FK_user_to_role_role]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'userId mapping to user table id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'user_to_role', @level2type=N'CONSTRAINT',@level2name=N'FK_user_to_role_role'
GO
