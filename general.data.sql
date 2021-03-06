USE [General]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 2019/5/23 23:37:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[IsMenu] [bit] NOT NULL CONSTRAINT [DF_Category_IsMenu]  DEFAULT ((1)),
	[SysResource] [nvarchar](200) NULL,
	[ResourceId] [nvarchar](200) NULL,
	[FatherResource] [nvarchar](200) NULL,
	[FatherId] [nvarchar](200) NULL,
	[Controller] [nvarchar](200) NULL,
	[Action] [nvarchar](200) NULL,
	[CssClass] [nvarchar](50) NULL,
	[Sort] [int] NOT NULL CONSTRAINT [DF_Category_Sort]  DEFAULT ((0)),
	[IsDisabled] [bit] NOT NULL CONSTRAINT [DF_Category_IsDisabled]  DEFAULT ((0)),
	[RouteName] [varchar](50) NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SysPermission]    Script Date: 2019/5/23 23:37:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysPermission](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[Role] [int] NOT NULL,
 CONSTRAINT [PK_SysPermission] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SysRole]    Script Date: 2019/5/23 23:37:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[Descrption] [nvarchar](500) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_SysRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SysUser]    Script Date: 2019/5/23 23:37:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SysUser](
	[Name] [nvarchar](50) NOT NULL,
	[Account] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](200) NOT NULL,
	[Email] [nvarchar](150) NULL,
	[MobilePhone] [nvarchar](50) NULL,
	[Salt] [nvarchar](max) NOT NULL,
	[Sex] [varchar](2) NULL,
	[LoginFailedNum] [int] NOT NULL,
	[UserGuid] [uniqueidentifier] NOT NULL,
	[LoginLock] [bit] NOT NULL CONSTRAINT [DF_SysUser_LoginLock]  DEFAULT ((0)),
	[LastLoginTime] [datetime] NOT NULL,
	[AllowLoginTime] [datetime] NULL,
	[Enable] [bit] NOT NULL CONSTRAINT [DF_SysUser_Enable]  DEFAULT ((0)),
	[IsDeleted] [bit] NOT NULL CONSTRAINT [DF_SysUser_IsDeleted]  DEFAULT ((0)),
 CONSTRAINT [PK_SysUser_1] PRIMARY KEY CLUSTERED 
(
	[UserGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SysUserLoginLog]    Script Date: 2019/5/23 23:37:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysUserLoginLog](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[IpAddress] [nvarchar](50) NOT NULL,
	[LoginTime] [datetime] NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_SysUserLoginLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SysUserToken]    Script Date: 2019/5/23 23:37:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysUserToken](
	[Id] [uniqueidentifier] NOT NULL,
	[SysUserId] [uniqueidentifier] NOT NULL,
	[ExpireTime] [datetime] NOT NULL,
 CONSTRAINT [PK_SysUserToken] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([Id], [Name], [IsMenu], [SysResource], [ResourceId], [FatherResource], [FatherId], [Controller], [Action], [CssClass], [Sort], [IsDisabled], [RouteName]) VALUES (3, N'系统管理', 1, N'General.Mvc.Areas.Admin.Controllers.SystemManageController', N'0313551edcd115ae48eb7a6374b2c14c', NULL, N'', NULL, NULL, N'menu-icon fa fa-desktop', 100, 0, NULL)
INSERT [dbo].[Category] ([Id], [Name], [IsMenu], [SysResource], [ResourceId], [FatherResource], [FatherId], [Controller], [Action], [CssClass], [Sort], [IsDisabled], [RouteName]) VALUES (4, N'系统用户', 1, N'General.Mvc.Areas.Admin.Controllers.UserController.Index', N'59445392d10bf073474af443b05f41ed', N'General.Mvc.Areas.Admin.Controllers.SystemManageController', N'0313551edcd115ae48eb7a6374b2c14c', N'User', N'Index', N'menu-icon fa fa-caret-right', 0, 0, NULL)
SET IDENTITY_INSERT [dbo].[Category] OFF
INSERT [dbo].[SysUser] ([Name], [Account], [Password], [Email], [MobilePhone], [Salt], [Sex], [LoginFailedNum], [UserGuid], [LoginLock], [LastLoginTime], [AllowLoginTime], [Enable], [IsDeleted]) VALUES (N'谢汉冰', N'xiehanbing', N'8a7729b0bfb8d40b450ea1eb7344c357', NULL, NULL, N'cb9e39fa-aa3c-4a87-8a9b-fd1563c0a788', NULL, 0, N'973ee36b-8869-4060-a6e0-4a8e5c424da8', 0, CAST(N'2019-05-23 23:26:12.150' AS DateTime), NULL, 0, 0)
INSERT [dbo].[SysUserLoginLog] ([Id], [UserId], [IpAddress], [LoginTime], [Message]) VALUES (N'10555558-2088-42b8-a233-1762c7b7cdd1', N'973ee36b-8869-4060-a6e0-4a8e5c424da8', N'', CAST(N'2019-05-23 23:13:34.617' AS DateTime), N'登录成功')
INSERT [dbo].[SysUserLoginLog] ([Id], [UserId], [IpAddress], [LoginTime], [Message]) VALUES (N'594a5537-71d0-4e32-a3cf-214b388745ac', N'973ee36b-8869-4060-a6e0-4a8e5c424da8', N'', CAST(N'2019-05-23 23:00:09.567' AS DateTime), N'登录成功')
INSERT [dbo].[SysUserLoginLog] ([Id], [UserId], [IpAddress], [LoginTime], [Message]) VALUES (N'dfcfbbae-fa1f-4a7d-9918-26832acf2b01', N'973ee36b-8869-4060-a6e0-4a8e5c424da8', N'', CAST(N'2019-05-19 21:02:34.017' AS DateTime), N'登录成功')
INSERT [dbo].[SysUserLoginLog] ([Id], [UserId], [IpAddress], [LoginTime], [Message]) VALUES (N'5b32d907-e10d-4c72-bdf5-3bbd796a74a6', N'973ee36b-8869-4060-a6e0-4a8e5c424da8', N'', CAST(N'2019-05-19 19:25:05.687' AS DateTime), N'登录成功')
INSERT [dbo].[SysUserLoginLog] ([Id], [UserId], [IpAddress], [LoginTime], [Message]) VALUES (N'aff1a58e-5aa1-4b8c-bf20-670899b6c662', N'973ee36b-8869-4060-a6e0-4a8e5c424da8', N'', CAST(N'2019-05-23 22:51:57.790' AS DateTime), N'登录密码错误')
INSERT [dbo].[SysUserLoginLog] ([Id], [UserId], [IpAddress], [LoginTime], [Message]) VALUES (N'2be627c9-de07-4bfb-b184-841f41af51ff', N'973ee36b-8869-4060-a6e0-4a8e5c424da8', N'', CAST(N'2019-05-19 17:56:29.107' AS DateTime), N'登录成功')
INSERT [dbo].[SysUserLoginLog] ([Id], [UserId], [IpAddress], [LoginTime], [Message]) VALUES (N'9440f11e-ccd9-4adb-b061-87b5d8481c69', N'973ee36b-8869-4060-a6e0-4a8e5c424da8', N'', CAST(N'2019-05-19 17:54:35.700' AS DateTime), N'登录成功')
INSERT [dbo].[SysUserLoginLog] ([Id], [UserId], [IpAddress], [LoginTime], [Message]) VALUES (N'df00782b-e6c5-48fb-a95c-87d712325fd4', N'973ee36b-8869-4060-a6e0-4a8e5c424da8', N'', CAST(N'2019-05-19 19:27:37.603' AS DateTime), N'登录成功')
INSERT [dbo].[SysUserLoginLog] ([Id], [UserId], [IpAddress], [LoginTime], [Message]) VALUES (N'8f4f070c-4b14-4cf0-9674-95ac601de06d', N'973ee36b-8869-4060-a6e0-4a8e5c424da8', N'', CAST(N'2019-05-23 22:59:29.123' AS DateTime), N'登录密码错误')
INSERT [dbo].[SysUserLoginLog] ([Id], [UserId], [IpAddress], [LoginTime], [Message]) VALUES (N'd67ec7cf-95c8-4333-b42b-b9d1859f4b43', N'973ee36b-8869-4060-a6e0-4a8e5c424da8', N'', CAST(N'2019-05-19 18:08:01.210' AS DateTime), N'登录成功')
INSERT [dbo].[SysUserLoginLog] ([Id], [UserId], [IpAddress], [LoginTime], [Message]) VALUES (N'4cc4fafc-a7be-4233-a11d-be133e3af017', N'973ee36b-8869-4060-a6e0-4a8e5c424da8', N'', CAST(N'2019-05-23 22:56:41.090' AS DateTime), N'登录密码错误')
INSERT [dbo].[SysUserLoginLog] ([Id], [UserId], [IpAddress], [LoginTime], [Message]) VALUES (N'93c7ccd2-a545-47dd-a50e-cfa59e53e0d2', N'973ee36b-8869-4060-a6e0-4a8e5c424da8', N'', CAST(N'2019-05-19 17:58:11.310' AS DateTime), N'登录成功')
INSERT [dbo].[SysUserLoginLog] ([Id], [UserId], [IpAddress], [LoginTime], [Message]) VALUES (N'3dada10a-0ba0-40e9-8854-e2df13eabd96', N'973ee36b-8869-4060-a6e0-4a8e5c424da8', N'', CAST(N'2019-05-23 23:26:12.157' AS DateTime), N'登录成功')
INSERT [dbo].[SysUserLoginLog] ([Id], [UserId], [IpAddress], [LoginTime], [Message]) VALUES (N'f4b7f663-2547-43d3-a35b-ea09953f37e5', N'973ee36b-8869-4060-a6e0-4a8e5c424da8', N'', CAST(N'2019-05-23 22:56:19.097' AS DateTime), N'登录密码错误')
INSERT [dbo].[SysUserLoginLog] ([Id], [UserId], [IpAddress], [LoginTime], [Message]) VALUES (N'a892f032-3101-4773-a0d5-ede967984015', N'973ee36b-8869-4060-a6e0-4a8e5c424da8', N'', CAST(N'2019-05-23 22:57:09.133' AS DateTime), N'登录密码错误')
INSERT [dbo].[SysUserLoginLog] ([Id], [UserId], [IpAddress], [LoginTime], [Message]) VALUES (N'8031b4ec-45c0-48c5-8460-f24fbec7a3a2', N'973ee36b-8869-4060-a6e0-4a8e5c424da8', N'', CAST(N'2019-05-19 17:41:54.743' AS DateTime), N'登录密码错误')
INSERT [dbo].[SysUserLoginLog] ([Id], [UserId], [IpAddress], [LoginTime], [Message]) VALUES (N'177f0d96-b516-4aa3-a6cd-f6aa432c7eb3', N'973ee36b-8869-4060-a6e0-4a8e5c424da8', N'', CAST(N'2019-05-19 21:10:30.077' AS DateTime), N'登录成功')
INSERT [dbo].[SysUserLoginLog] ([Id], [UserId], [IpAddress], [LoginTime], [Message]) VALUES (N'd41bcb23-d8e0-42cf-a1a2-fde62778c407', N'973ee36b-8869-4060-a6e0-4a8e5c424da8', N'', CAST(N'2019-05-19 17:50:57.957' AS DateTime), N'登录密码错误')
INSERT [dbo].[SysUserToken] ([Id], [SysUserId], [ExpireTime]) VALUES (N'60064851-02e4-4cb4-8eff-2807fab22f3d', N'973ee36b-8869-4060-a6e0-4a8e5c424da8', CAST(N'2019-05-19 19:08:01.210' AS DateTime))
INSERT [dbo].[SysUserToken] ([Id], [SysUserId], [ExpireTime]) VALUES (N'391d50a1-5c82-4d08-80ae-548245084d1c', N'973ee36b-8869-4060-a6e0-4a8e5c424da8', CAST(N'2019-05-19 22:10:30.077' AS DateTime))
INSERT [dbo].[SysUserToken] ([Id], [SysUserId], [ExpireTime]) VALUES (N'b278eef7-9bca-4889-91a8-81692ce3e062', N'973ee36b-8869-4060-a6e0-4a8e5c424da8', CAST(N'2019-05-24 00:13:34.617' AS DateTime))
INSERT [dbo].[SysUserToken] ([Id], [SysUserId], [ExpireTime]) VALUES (N'5ce8e62b-7f32-441c-b17b-8dfc80a1b658', N'973ee36b-8869-4060-a6e0-4a8e5c424da8', CAST(N'2019-05-24 00:00:09.567' AS DateTime))
INSERT [dbo].[SysUserToken] ([Id], [SysUserId], [ExpireTime]) VALUES (N'54af6d10-cb34-4b80-899e-aa67f749890e', N'973ee36b-8869-4060-a6e0-4a8e5c424da8', CAST(N'2019-05-19 18:58:11.310' AS DateTime))
INSERT [dbo].[SysUserToken] ([Id], [SysUserId], [ExpireTime]) VALUES (N'48410b85-478e-4965-b001-cdeebd2f12ae', N'973ee36b-8869-4060-a6e0-4a8e5c424da8', CAST(N'2019-05-19 18:54:35.700' AS DateTime))
INSERT [dbo].[SysUserToken] ([Id], [SysUserId], [ExpireTime]) VALUES (N'91b71195-25f3-45ea-b12d-e910bf969d18', N'973ee36b-8869-4060-a6e0-4a8e5c424da8', CAST(N'2019-05-24 00:26:12.157' AS DateTime))
INSERT [dbo].[SysUserToken] ([Id], [SysUserId], [ExpireTime]) VALUES (N'376defbc-d2a5-42c0-b1bd-eb955edfe0fc', N'973ee36b-8869-4060-a6e0-4a8e5c424da8', CAST(N'2019-05-19 20:25:05.687' AS DateTime))
INSERT [dbo].[SysUserToken] ([Id], [SysUserId], [ExpireTime]) VALUES (N'84a4a6ce-1e9f-44b6-8731-f04b62e06cef', N'973ee36b-8869-4060-a6e0-4a8e5c424da8', CAST(N'2019-05-19 22:02:34.017' AS DateTime))
INSERT [dbo].[SysUserToken] ([Id], [SysUserId], [ExpireTime]) VALUES (N'879b34f5-223a-415c-900f-f4408748331b', N'973ee36b-8869-4060-a6e0-4a8e5c424da8', CAST(N'2019-05-19 20:27:37.603' AS DateTime))
INSERT [dbo].[SysUserToken] ([Id], [SysUserId], [ExpireTime]) VALUES (N'c7f4a085-83f3-4e50-b56c-fb5832ba5b5a', N'973ee36b-8869-4060-a6e0-4a8e5c424da8', CAST(N'2019-05-19 18:56:29.110' AS DateTime))
ALTER TABLE [dbo].[SysRole] ADD  CONSTRAINT [DF_SysRole_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[SysPermission]  WITH CHECK ADD  CONSTRAINT [FK_SysPermission_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([Id])
GO
ALTER TABLE [dbo].[SysPermission] CHECK CONSTRAINT [FK_SysPermission_Category]
GO
ALTER TABLE [dbo].[SysPermission]  WITH CHECK ADD  CONSTRAINT [FK_SysPermission_SysRole] FOREIGN KEY([Role])
REFERENCES [dbo].[SysRole] ([Id])
GO
ALTER TABLE [dbo].[SysPermission] CHECK CONSTRAINT [FK_SysPermission_SysRole]
GO
ALTER TABLE [dbo].[SysUserLoginLog]  WITH CHECK ADD  CONSTRAINT [FK_SysUserLoginLog_SysUser] FOREIGN KEY([UserId])
REFERENCES [dbo].[SysUser] ([UserGuid])
GO
ALTER TABLE [dbo].[SysUserLoginLog] CHECK CONSTRAINT [FK_SysUserLoginLog_SysUser]
GO
ALTER TABLE [dbo].[SysUserToken]  WITH CHECK ADD  CONSTRAINT [FK_SysUserToken_SysUser] FOREIGN KEY([SysUserId])
REFERENCES [dbo].[SysUser] ([UserGuid])
GO
ALTER TABLE [dbo].[SysUserToken] CHECK CONSTRAINT [FK_SysUserToken_SysUser]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'自增id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Category', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Category', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否是菜单' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Category', @level2type=N'COLUMN',@level2name=N'IsMenu'
GO
