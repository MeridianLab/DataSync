USE HarvestSQL_Staging

--**********************************************************************************************************
--  Provider section adding column updateStamp and updateDate]
--**********************************************************************************************************
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS
				WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Provider' AND COLUMN_NAME = 'updateStamp')
BEGIN
	ALTER TABLE [dbo].[Provider] ADD
	[updateStamp] numeric(19, 0) NULL, updateDate datetime NULL;
	PRINT 'ADDED COLUMN updateStamp, updateDate to table Provider'
END


--**********************************************************************************************************
--  Patient section adding column updateStamp and updateDate]
--**********************************************************************************************************
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS
				WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Patient' AND COLUMN_NAME = 'updateStamp')
BEGIN
	ALTER TABLE [dbo].[Patient] ADD
	[updateStamp] numeric(19, 0) NULL, updateDate datetime NULL;
	PRINT 'ADDED COLUMN updateStamp, updateDate to table Patient'
END

--**********************************************************************************************************
--  Bloodtest section adding column LOCKEY_ORIG]
--**********************************************************************************************************
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS
				WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Bloodtest' AND COLUMN_NAME = 'LOCKEY_ORIG')
BEGIN
	ALTER TABLE [dbo].[Bloodtest] ADD
	[LOCKEY_ORIG] INT NULL, CREATE_DATE DATETIME NOT NULL DEFAULT(GETDATE())
	PRINT 'ADDED COLUMN LOCKEY_ORIG to table Bloodtest'
END

--**********************************************************************************************************
--  Patient section adding column LOCKEY_ORIG]
--**********************************************************************************************************
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS
				WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Patient' AND COLUMN_NAME = 'LOCKEY_ORIG')
BEGIN
	ALTER TABLE [dbo].[Patient] ADD
	[LOCKEY_ORIG] INT NULL
	PRINT 'ADDED COLUMN LOCKEY_ORIG to table Patient'
END

--**********************************************************************************************************
--  Provider section adding column LOCKEY_ORIG]
--**********************************************************************************************************
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS
				WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Provider' AND COLUMN_NAME = 'LOCKEY_ORIG')
BEGIN
	ALTER TABLE [dbo].[Provider] ADD
	[LOCKEY_ORIG] INT NULL
	PRINT 'ADDED COLUMN LOCKEY_ORIG to table Provider'
END

--**********************************************************************************************************
--  Adding Processing Status Column
--**********************************************************************************************************
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS
				WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'TestResults' AND COLUMN_NAME = 'ProcessingStatus')
BEGIN
ALTER TABLE [dbo].[Bloodtest] ADD [ProcessingStatus] varchar(25) default 'New'
ALTER TABLE [dbo].[OrdPanel] ADD [ProcessingStatus] varchar(25) default 'New'
ALTER TABLE [dbo].[Patient] ADD [ProcessingStatus] varchar(25) default 'New'
ALTER TABLE [dbo].[Provider] ADD [ProcessingStatus] varchar(25) default 'New'
ALTER TABLE [dbo].[Test] ADD [ProcessingStatus] varchar(25) default 'New'
ALTER TABLE [dbo].[TestResults] ADD [ProcessingStatus] varchar(25) default 'New'
	PRINT 'ADDED COLUMN ProcessingStatus to table TestResults'
END

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BridgeLog]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BridgeLog](
	[ID] [uniqueidentifier] NOT NULL,
	[StartDateTime] [datetime] NULL,
	[EndDateTime] [datetime] NULL,
	[Status] [char](10) NULL,
	[DescLog] [text] NULL,
	[LogLevel] varchar(15) null,
	[CurrentStep] [varchar](500) NULL,
 CONSTRAINT [PK_BridgeLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[BridgeLogDetail]    Script Date: 10/27/2023 5:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BridgeLogDetail]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BridgeLogDetail](
	[Id] [uniqueidentifier] NOT NULL DEFAULT(NEWID()),
	[BridgeLogId] [uniqueidentifier] NOT NULL,
	[ProjectName] [varchar](100) NOT NULL,
	[StepName] [varchar](200) NOT NULL,
	[Message] [varchar](1000) NULL,
	[LogLevel] varchar(15) null,
	[Create_Date] [datetime] NOT NULL,
 CONSTRAINT [PK_BridgeLogDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[BridgeLogTblCounts]    Script Date: 10/27/2023 5:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BridgeLogTblCounts]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BridgeLogTblCounts](
	[Id] [uniqueidentifier] NOT NULL DEFAULT(NEWID()),
	[BridgeLogId] [uniqueidentifier] NOT NULL,
	[BloodtestCnt] [int] NOT NULL,
	[locationCnt] [int] NOT NULL,
	[OrdPanelCnt] [int] NOT NULL,
	[PatientCnt] [int] NOT NULL,
	[ProviderCnt] [int] NOT NULL,
	[TestCnt] [int] NOT NULL,
	[TestResultsCnt] [int] NOT NULL,
	[Create_Date] [datetime] NOT NULL,
 CONSTRAINT [PK_BridgeLogTblCounts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO


IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BridgeLog]') AND type in (N'U'))
BEGIN

CREATE TABLE [dbo].[SYNC_JSON_DRIVER](
	[ID] [uniqueidentifier] NOT NULL,
	[NAME] [varchar](150) NOT NULL,
	[JSON_MODEL] [varchar](max) NOT NULL,
	[IS_LOCKED] [bit] NOT NULL,
	[CREATE_DATE] [datetime] NOT NULL,
	[MODIFIED_DATE] [datetime] NOT NULL,
 CONSTRAINT [PK_SYNC_JSON_DRIVER] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

ALTER TABLE [dbo].[SYNC_JSON_DRIVER] ADD  CONSTRAINT [DF_Table_1_recordKey]  DEFAULT (newid()) FOR [ID]

ALTER TABLE [dbo].[SYNC_JSON_DRIVER] ADD  CONSTRAINT [DF_SYNC_JSON_DRIVER_IS_LOCKED]  DEFAULT ((0)) FOR [IS_LOCKED]

ALTER TABLE [dbo].[SYNC_JSON_DRIVER] ADD  CONSTRAINT [DF_SYNC_JSON_DRIVER_CREATE_DATE]  DEFAULT (getdate()) FOR [CREATE_DATE]

ALTER TABLE [dbo].[SYNC_JSON_DRIVER] ADD  CONSTRAINT [DF_SYNC_JSON_DRIVER_MODIFIED_DATE]  DEFAULT (getdate()) FOR [MODIFIED_DATE]


CREATE TABLE [dbo].[BridgeConfig](
	[NAME] [varchar](50) NOT NULL,
	[VALUE] [varchar](2500) NULL,
	[CREATE_DATE] [datetime] NOT NULL,
	[MODIFIED_DATE] [datetime] NOT NULL
) ON [PRIMARY]
ALTER TABLE [dbo].[BridgeConfig] ADD  CONSTRAINT [DF_BridgeConfig_CREATE_DATE]  DEFAULT (getdate()) FOR [CREATE_DATE]
ALTER TABLE [dbo].[BridgeConfig] ADD  CONSTRAINT [DF_BridgeConfig_MODIFIED_DATE]  DEFAULT (getdate()) FOR [MODIFIED_DATE]


END