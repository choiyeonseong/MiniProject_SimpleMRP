USE [MRP]
GO
/****** Object:  Table [dbo].[Process]    Script Date: 2021-07-01 오후 2:01:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Process](
	[PrcIdx] [int] IDENTITY(1,1) NOT NULL,
	[SchIdx] [int] NOT NULL,
	[PrcCD] [char](14) NOT NULL,
	[PrcDate] [date] NOT NULL,
	[PrcLoadTime] [int] NULL,
	[PrcStartTime] [time](7) NULL,
	[PrcEndTime] [time](7) NULL,
	[PrcFacilityID] [char](8) NULL,
	[PrcResult] [bigint] NOT NULL,
	[RegDate] [datetime] NULL,
	[RegID] [varchar](20) NULL,
	[ModDate] [datetime] NULL,
	[ModID] [varchar](20) NULL,
 CONSTRAINT [PK_Process] PRIMARY KEY CLUSTERED 
(
	[PrcIdx] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Schedules]    Script Date: 2021-07-01 오후 2:01:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schedules](
	[SchIdx] [int] IDENTITY(1,1) NOT NULL,
	[PlantCode] [char](8) NULL,
	[SchDate] [date] NOT NULL,
	[SchLoadTime] [int] NOT NULL,
	[SchStartTime] [time](7) NULL,
	[SchEndTime] [time](7) NULL,
	[SchFacilityID] [char](8) NULL,
	[SchAmount] [int] NULL,
	[RegDate] [datetime] NULL,
	[RegID] [varchar](20) NULL,
	[ModDate] [datetime] NULL,
	[ModID] [varchar](20) NULL,
 CONSTRAINT [PK_Schedules] PRIMARY KEY CLUSTERED 
(
	[SchIdx] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Settings]    Script Date: 2021-07-01 오후 2:01:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Settings](
	[BasicCode] [char](8) NOT NULL,
	[CodeName] [nvarchar](100) NOT NULL,
	[CodeDesc] [nvarchar](max) NULL,
	[RegDate] [datetime] NULL,
	[RedID] [varchar](20) NULL,
	[ModDate] [datetime] NULL,
	[ModID] [varchar](20) NULL,
 CONSTRAINT [PK_Settings] PRIMARY KEY CLUSTERED 
(
	[BasicCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Process] ON 

INSERT [dbo].[Process] ([PrcIdx], [SchIdx], [PrcCD], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegDate], [RegID], [ModDate], [ModID]) VALUES (10, 5, N'PRC20210630001', CAST(N'2021-06-30' AS Date), 5, CAST(N'07:00:00' AS Time), CAST(N'13:00:00' AS Time), N'FAC10002', 0, CAST(N'2021-06-30T11:33:36.247' AS DateTime), N'MRP', CAST(N'2021-06-30T11:37:43.000' AS DateTime), N'SYS')
INSERT [dbo].[Process] ([PrcIdx], [SchIdx], [PrcCD], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegDate], [RegID], [ModDate], [ModID]) VALUES (11, 5, N'PRC20210630002', CAST(N'2021-06-30' AS Date), 5, CAST(N'07:00:00' AS Time), CAST(N'13:00:00' AS Time), N'FAC10002', 0, CAST(N'2021-06-30T11:40:13.917' AS DateTime), N'MRP', CAST(N'2021-07-01T10:15:17.000' AS DateTime), N'SYS')
SET IDENTITY_INSERT [dbo].[Process] OFF
GO
SET IDENTITY_INSERT [dbo].[Schedules] ON 

INSERT [dbo].[Schedules] ([SchIdx], [PlantCode], [SchDate], [SchLoadTime], [SchStartTime], [SchEndTime], [SchFacilityID], [SchAmount], [RegDate], [RegID], [ModDate], [ModID]) VALUES (1, N'PC010002', CAST(N'2021-06-24' AS Date), 10, CAST(N'10:00:00' AS Time), CAST(N'17:00:00' AS Time), N'FAC10002', 21, CAST(N'2021-06-24T18:00:00.000' AS DateTime), N'SYS', CAST(N'2021-06-25T11:48:25.427' AS DateTime), N'MRP')
INSERT [dbo].[Schedules] ([SchIdx], [PlantCode], [SchDate], [SchLoadTime], [SchStartTime], [SchEndTime], [SchFacilityID], [SchAmount], [RegDate], [RegID], [ModDate], [ModID]) VALUES (2, N'PC010001', CAST(N'2021-06-25' AS Date), 20, CAST(N'10:00:00' AS Time), CAST(N'15:00:00' AS Time), N'FAC10001', 40, CAST(N'2021-06-25T12:39:29.250' AS DateTime), N'MRP', CAST(N'2021-06-28T09:17:58.023' AS DateTime), N'MRP')
INSERT [dbo].[Schedules] ([SchIdx], [PlantCode], [SchDate], [SchLoadTime], [SchStartTime], [SchEndTime], [SchFacilityID], [SchAmount], [RegDate], [RegID], [ModDate], [ModID]) VALUES (3, N'PC010002', CAST(N'2021-06-28' AS Date), 10, CAST(N'10:00:00' AS Time), CAST(N'15:00:00' AS Time), N'FAC10002', 40, CAST(N'2021-06-25T14:26:22.613' AS DateTime), N'MRP', CAST(N'2021-06-28T14:00:05.610' AS DateTime), N'MRP')
INSERT [dbo].[Schedules] ([SchIdx], [PlantCode], [SchDate], [SchLoadTime], [SchStartTime], [SchEndTime], [SchFacilityID], [SchAmount], [RegDate], [RegID], [ModDate], [ModID]) VALUES (4, N'PC010004', CAST(N'2021-06-25' AS Date), 20, CAST(N'10:00:00' AS Time), CAST(N'15:00:00' AS Time), N'FAC10002', 35, CAST(N'2021-06-28T09:18:01.940' AS DateTime), N'MRP', CAST(N'2021-06-28T09:27:07.353' AS DateTime), N'MRP')
INSERT [dbo].[Schedules] ([SchIdx], [PlantCode], [SchDate], [SchLoadTime], [SchStartTime], [SchEndTime], [SchFacilityID], [SchAmount], [RegDate], [RegID], [ModDate], [ModID]) VALUES (5, N'PC010002', CAST(N'2021-06-30' AS Date), 5, CAST(N'07:00:00' AS Time), CAST(N'13:00:00' AS Time), N'FAC10001', 12, CAST(N'2021-06-28T09:18:33.210' AS DateTime), N'MRP', CAST(N'2021-06-30T09:41:33.513' AS DateTime), N'MRP')
INSERT [dbo].[Schedules] ([SchIdx], [PlantCode], [SchDate], [SchLoadTime], [SchStartTime], [SchEndTime], [SchFacilityID], [SchAmount], [RegDate], [RegID], [ModDate], [ModID]) VALUES (6, N'PC010001', CAST(N'2021-06-27' AS Date), 20, NULL, NULL, N'FAC10001', 5, CAST(N'2021-06-28T09:26:50.057' AS DateTime), N'MRP', CAST(N'2021-06-28T11:52:02.090' AS DateTime), N'MRP')
SET IDENTITY_INSERT [dbo].[Schedules] OFF
GO
INSERT [dbo].[Settings] ([BasicCode], [CodeName], [CodeDesc], [RegDate], [RedID], [ModDate], [ModID]) VALUES (N'FAC10001', N'설비1', N'생산설비1
확인요', CAST(N'2021-06-24T14:07:32.580' AS DateTime), N'MRP', NULL, NULL)
INSERT [dbo].[Settings] ([BasicCode], [CodeName], [CodeDesc], [RegDate], [RedID], [ModDate], [ModID]) VALUES (N'FAC10002', N'설비2', N'생산설비2', CAST(N'2021-06-24T14:09:03.830' AS DateTime), N'MRP', NULL, NULL)
INSERT [dbo].[Settings] ([BasicCode], [CodeName], [CodeDesc], [RegDate], [RedID], [ModDate], [ModID]) VALUES (N'PC010001', N'수원', N'수원공장(코드)', CAST(N'2021-06-24T11:22:10.000' AS DateTime), N'SYS', NULL, NULL)
INSERT [dbo].[Settings] ([BasicCode], [CodeName], [CodeDesc], [RegDate], [RedID], [ModDate], [ModID]) VALUES (N'PC010002', N'부산', N'부산공장 코드명', CAST(N'2021-06-24T13:58:21.163' AS DateTime), N'MRP', CAST(N'2021-06-25T11:11:20.967' AS DateTime), NULL)
INSERT [dbo].[Settings] ([BasicCode], [CodeName], [CodeDesc], [RegDate], [RedID], [ModDate], [ModID]) VALUES (N'PC010004', N'대전', N'대전공장', CAST(N'2021-06-24T13:57:27.950' AS DateTime), N'MRP', NULL, NULL)
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UK_Process_PrcCD]    Script Date: 2021-07-01 오후 2:01:58 ******/
ALTER TABLE [dbo].[Process] ADD  CONSTRAINT [UK_Process_PrcCD] UNIQUE NONCLUSTERED 
(
	[PrcCD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Process]  WITH NOCHECK ADD  CONSTRAINT [FK_Process_Schedules] FOREIGN KEY([SchIdx])
REFERENCES [dbo].[Schedules] ([SchIdx])
GO
ALTER TABLE [dbo].[Process] NOCHECK CONSTRAINT [FK_Process_Schedules]
GO
ALTER TABLE [dbo].[Process]  WITH NOCHECK ADD  CONSTRAINT [FK_Process_Settings] FOREIGN KEY([PrcFacilityID])
REFERENCES [dbo].[Settings] ([BasicCode])
GO
ALTER TABLE [dbo].[Process] NOCHECK CONSTRAINT [FK_Process_Settings]
GO
ALTER TABLE [dbo].[Schedules]  WITH NOCHECK ADD  CONSTRAINT [FK_Schedules_Settings] FOREIGN KEY([PlantCode])
REFERENCES [dbo].[Settings] ([BasicCode])
GO
ALTER TABLE [dbo].[Schedules] NOCHECK CONSTRAINT [FK_Schedules_Settings]
GO
ALTER TABLE [dbo].[Schedules]  WITH NOCHECK ADD  CONSTRAINT [FK_Schedules_Settings1] FOREIGN KEY([SchFacilityID])
REFERENCES [dbo].[Settings] ([BasicCode])
GO
ALTER TABLE [dbo].[Schedules] NOCHECK CONSTRAINT [FK_Schedules_Settings1]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'작성일' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Process', @level2type=N'COLUMN',@level2name=N'RegDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'작성자' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Process', @level2type=N'COLUMN',@level2name=N'RegID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'수정일' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Process', @level2type=N'COLUMN',@level2name=N'ModDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'수정자' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Process', @level2type=N'COLUMN',@level2name=N'ModID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'공정계획순번' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Schedules', @level2type=N'COLUMN',@level2name=N'SchIdx'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'공장코드' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Schedules', @level2type=N'COLUMN',@level2name=N'PlantCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'공정계획일' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Schedules', @level2type=N'COLUMN',@level2name=N'SchDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'로드타임(초)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Schedules', @level2type=N'COLUMN',@level2name=N'SchLoadTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'시작시간(계획)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Schedules', @level2type=N'COLUMN',@level2name=N'SchStartTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'종료시간(계획)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Schedules', @level2type=N'COLUMN',@level2name=N'SchEndTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'생산설비ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Schedules', @level2type=N'COLUMN',@level2name=N'SchFacilityID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'목표수량(계획)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Schedules', @level2type=N'COLUMN',@level2name=N'SchAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'작성일' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Schedules', @level2type=N'COLUMN',@level2name=N'RegDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'작성자' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Schedules', @level2type=N'COLUMN',@level2name=N'RegID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'수정일' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Schedules', @level2type=N'COLUMN',@level2name=N'ModDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'수정자' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Schedules', @level2type=N'COLUMN',@level2name=N'ModID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'코드' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Settings', @level2type=N'COLUMN',@level2name=N'BasicCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'코드명' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Settings', @level2type=N'COLUMN',@level2name=N'CodeName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'코드설명' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Settings', @level2type=N'COLUMN',@level2name=N'CodeDesc'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'작성일' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Settings', @level2type=N'COLUMN',@level2name=N'RegDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'작성자' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Settings', @level2type=N'COLUMN',@level2name=N'RedID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'수정일' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Settings', @level2type=N'COLUMN',@level2name=N'ModDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'수정자' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Settings', @level2type=N'COLUMN',@level2name=N'ModID'
GO
