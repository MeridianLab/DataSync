GO
/****** Object:  UserDefinedFunction [dbo].[ParsePatientDay]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION [dbo].[ParsePatientDay] (@address varchar(20))
RETURNS varchar(1)
AS
BEGIN

declare @ret as char
set @ret = '0'

if(len(@address)<>0)
BEGIN
if substring(@address, 1, 1)='M'
	set @ret = '1'
	
if substring(@address, 1, 1)='T'
	set @ret = '2'
END

return @ret
END
GO
/****** Object:  UserDefinedFunction [dbo].[ParsePatientShift]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION [dbo].[ParsePatientShift] (@address varchar(20))
RETURNS varchar(1)
AS
BEGIN

declare @ret as char
set @ret = '0'

if(len(@address)=2)
BEGIN
set @ret = substring(@address, 2, 1)
END

if @ret<>'0' AND @ret<>'1' AND @ret<>'2'  AND @ret<>'3' AND @ret<>'4'
set @ret = '0'

return @ret
END

GO
/****** Object:  Table [dbo].[BambergTestSequence]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BambergTestSequence](
	[test] [int] NOT NULL,
	[seq] [int] NULL,
	[text] [varchar](40) NULL,
	[decimal] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Test]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Test](
	[RecordKey] [int] NOT NULL,
	[CopiaKey] [int] NULL,
	[LOINC] [int] NULL,
	[Name] [varchar](30) NULL,
	[abbrev] [varchar](20) NULL,
	[units] [varchar](15) NULL,
	[posneg] [bit] NOT NULL,
	[major] [char](1) NULL,
	[ProcessingStatus] [varchar](25) NULL
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[BambergTestSequenceView]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[BambergTestSequenceView]
AS
SELECT     dbo.BambergTestSequence.*, dbo.Test.RecordKey AS Expr1, dbo.Test.Name AS Expr2
FROM         dbo.Test INNER JOIN
                      dbo.BambergTestSequence ON dbo.Test.RecordKey = dbo.BambergTestSequence.test
GO
/****** Object:  Table [dbo].[BridgeLog]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BridgeLog](
	[ID] [uniqueidentifier] NOT NULL,
	[StartDateTime] [datetime] NULL,
	[EndDateTime] [datetime] NULL,
	[Status] [char](10) NULL,
	[DescLog] [text] NULL,
	[LogLevel] [varchar](15) NULL,
 CONSTRAINT [PK_BridgeLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[CompleteBridge]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE VIEW [dbo].[CompleteBridge]
AS
SELECT   CURRENT_TIMESTAMP AS Now,
MAX(EndDateTime) AS LastCompleteDateTime
FROM         dbo.BridgeLog
WHERE     (UPPER(LTRIM(RTRIM(Status))) = 'COMPLETE')
GO
/****** Object:  Table [dbo].[ReportCardSequence]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReportCardSequence](
	[test] [int] NOT NULL,
	[seq] [int] NOT NULL,
	[decimal] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ReportCardSequenceView]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE VIEW [dbo].[ReportCardSequenceView]
AS
SELECT ReportCardSequence.*, Test.RecordKey, Test.Name
FROM Test INNER JOIN ReportCardSequence
    ON Test.RecordKey = ReportCardSequence.test
GO
/****** Object:  Table [dbo].[Bloodtest]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bloodtest](
	[RECORDKEY] [int] NOT NULL,
	[PATIKEY] [int] NULL,
	[DRAWDATE] [datetime] NULL,
	[LOCKEY] [int] NULL,
	[PROVKEY] [int] NULL,
	[SID] [varchar](20) NULL,
	[requisitionKey] [int] NULL,
	[requisitionNumber] [varchar](20) NULL,
	[LOCKEY_ORIG] [int] NULL,
	[CREATE_DATE] [datetime] NOT NULL,
	[ProcessingStatus] [varchar](25) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[location]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[location](
	[recordKey] [int] NOT NULL,
	[name] [varchar](60) NULL,
	[CopiaName] [varchar](60) NULL,
	[abbrev] [varchar](30) NULL,
	[abbrev2] [varchar](30) NULL,
	[abbrevManual] [varchar](30) NULL,
	[addr1] [varchar](25) NULL,
	[addr2] [varchar](25) NULL,
	[zip] [varchar](10) NULL,
	[phone1] [varchar](10) NULL,
	[phone2] [varchar](40) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrdPanel]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrdPanel](
	[RecordKey] [int] NOT NULL,
	[done] [bit] NOT NULL,
	[apprdate] [datetime] NULL,
	[ProcessingStatus] [varchar](25) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patient]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patient](
	[RecordKey] [int] NULL,
	[lastname] [varchar](30) NULL,
	[firstname] [varchar](30) NULL,
	[DOB] [datetime] NULL,
	[Age] [int] NULL,
	[SSN] [varchar](10) NULL,
	[address1] [varchar](30) NULL,
	[address2] [varchar](30) NULL,
	[zip] [varchar](9) NULL,
	[phone1] [varchar](20) NULL,
	[phone2] [varchar](20) NULL,
	[sex] [varchar](2) NULL,
	[provkey] [int] NULL,
	[customaccno] [varchar](25) NULL,
	[middleinit] [varchar](2) NULL,
	[LOCKEY] [int] NULL,
	[LOCKEY2] [int] NULL,
	[LOCKEY3] [int] NULL,
	[LocName] [varchar](150) NULL,
	[prvdrLastname] [varchar](150) NULL,
	[prvdrFirstname] [varchar](150) NULL,
	[day] [int] NULL,
	[shift] [int] NULL,
	[status] [varchar](10) NULL,
	[intStatus] [int] NULL,
	[modality] [varchar](10) NULL,
	[intModality] [int] NULL,
	[createStamp] [datetime] NULL,
	[updateStamp] [numeric](19, 0) NULL,
	[updateDate] [datetime] NULL,
	[LOCKEY_ORIG] [int] NULL,
	[ProcessingStatus] [varchar](25) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Provider]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Provider](
	[recordKey] [int] NOT NULL,
	[lastName] [varchar](30) NULL,
	[firstName] [varchar](30) NULL,
	[fullName] [varchar](50) NULL,
	[degree] [varchar](10) NULL,
	[ssn] [varchar](20) NULL,
	[locKey] [int] NULL,
	[matchLoc] [bit] NOT NULL,
	[addr1] [varchar](25) NULL,
	[addr2] [varchar](25) NULL,
	[phone1] [varchar](20) NULL,
	[updateStamp] [numeric](19, 0) NULL,
	[updateDate] [datetime] NULL,
	[LOCKEY_ORIG] [int] NULL,
	[ProcessingStatus] [varchar](25) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TestResults]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestResults](
	[RecordKey] [int] NOT NULL,
	[numresult] [float] NULL,
	[textresult] [text] NULL,
	[rundate] [datetime] NULL,
	[testkey] [int] NULL,
	[sid] [varchar](20) NULL,
	[blookey] [int] NULL,
	[patikey] [int] NULL,
	[orderingPhysicianKey] [int] NULL,
	[prefix] [char](2) NULL,
	[status] [int] NULL,
	[Ordpkey] [int] NULL,
	[labTestDescription] [varchar](100) NULL,
	[requisitionKey] [int] NULL,
	[ProcessingStatus] [varchar](25) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[tblcomplete]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[tblcomplete]
AS
SELECT     LTRIM(RTRIM(dbo.Patient.lastname)) + ', ' + LTRIM(RTRIM(dbo.Patient.firstname)) AS [Patient Name], CONVERT(varchar, dbo.Patient.customaccno) 
                      AS [Chart Num], dbo.Patient.sex, LTRIM(RTRIM(dbo.provider.lastname)) + ', ' + LTRIM(RTRIM(dbo.provider.firstname)) AS Provider, 
                      dbo.Bloodtest.DRAWDATE AS [Draw Date], LTRIM(RTRIM(dbo.Test.Name)) AS Test, CONVERT(varchar, dbo.TestResults.textresult) AS [Reported As], 
                      dbo.TestResults.numresult AS [Num Res], dbo.location.name AS [Draw Location], CONVERT(varchar, dbo.Bloodtest.SID) AS [Sample ID]
FROM         dbo.Bloodtest INNER JOIN
                      dbo.TestResults ON dbo.Bloodtest.RECORDKEY = dbo.TestResults.blookey INNER JOIN
                      dbo.Test ON dbo.TestResults.testkey = dbo.Test.RecordKey INNER JOIN
                      dbo.Patient ON dbo.TestResults.patikey = dbo.Patient.RecordKey INNER JOIN
                      dbo.provider ON dbo.provider.recordkey = dbo.Patient.provkey INNER JOIN
                      dbo.location ON dbo.provider.lockey = dbo.location.recordKey INNER JOIN
                      dbo.OrdPanel ON dbo.TestResults.Ordpkey = dbo.OrdPanel.RecordKey
WHERE     (dbo.OrdPanel.done = 1) AND (dbo.TestResults.numresult <> - 999.99) OR
                      (dbo.Test.Name = 'Major')
GO
/****** Object:  View [dbo].[tblComplete_new]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[tblComplete_new]
AS
SELECT     LTRIM(RTRIM(dbo.Patient.lastname)) + ', ' + LTRIM(RTRIM(dbo.Patient.firstname)) AS [Patient Name], CONVERT(varchar, dbo.Patient.customaccno) 
                      AS [Chart Num], dbo.Patient.sex, LTRIM(RTRIM(dbo.provider.lastname)) + ', ' + LTRIM(RTRIM(dbo.provider.firstname)) AS Provider, 
                      dbo.Bloodtest.DRAWDATE AS [Draw Date], LTRIM(RTRIM(dbo.Test.Name)) AS Test, CONVERT(varchar, dbo.TestResults.textresult) AS [Reported As], 
                      dbo.TestResults.numresult AS [Num Res], dbo.location.name AS [Draw Location], CONVERT(varchar, dbo.Bloodtest.SID) AS [Sample ID]
FROM         dbo.Bloodtest INNER JOIN
                      dbo.TestResults ON dbo.Bloodtest.RECORDKEY = dbo.TestResults.blookey INNER JOIN
                      dbo.Test ON dbo.TestResults.testkey = dbo.Test.RecordKey INNER JOIN
                      dbo.Patient ON dbo.TestResults.patikey = dbo.Patient.RecordKey INNER JOIN
                      dbo.provider ON dbo.provider.recordkey = dbo.Patient.provkey INNER JOIN
                      dbo.location ON dbo.provider.lockey = dbo.location.recordKey INNER JOIN
                      dbo.OrdPanel ON dbo.TestResults.Ordpkey = dbo.OrdPanel.RecordKey
WHERE     (dbo.OrdPanel.done = 1)
GO
/****** Object:  View [dbo].[tblcomplete_old]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[tblcomplete_old]
AS
SELECT     LTRIM(RTRIM(dbo.Patient.lastname)) + ', ' + LTRIM(RTRIM(dbo.Patient.firstname)) AS [Patient Name], CONVERT(varchar, dbo.Patient.customaccno) 
                      AS [Chart Num], dbo.Patient.sex, LTRIM(RTRIM(dbo.provider.lastname)) + ', ' + LTRIM(RTRIM(dbo.provider.firstname)) AS Provider, 
                      dbo.Bloodtest.DRAWDATE AS [Draw Date], LTRIM(RTRIM(dbo.Test.Name)) AS Test, CONVERT(varchar, dbo.TestResults.textresult) AS [Reported As], 
                      dbo.TestResults.numresult AS [Num Res], dbo.location.name AS [Draw Location], CONVERT(varchar, dbo.Bloodtest.SID) AS [Sample ID]
FROM         dbo.Bloodtest INNER JOIN
                      dbo.TestResults ON dbo.Bloodtest.RECORDKEY = dbo.TestResults.blookey INNER JOIN
                      dbo.Test ON dbo.TestResults.testkey = dbo.Test.RecordKey INNER JOIN
                      dbo.Patient ON dbo.TestResults.patikey = dbo.Patient.RecordKey INNER JOIN
                      dbo.provider ON dbo.provider.recordkey = dbo.Patient.provkey INNER JOIN
                      dbo.location ON dbo.provider.lockey = dbo.location.recordKey
GO
/****** Object:  View [dbo].[TestResultsMajorView]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[TestResultsMajorView]
AS
SELECT     dbo.TestResults.RecordKey, dbo.TestResults.numresult, dbo.TestResults.textresult, dbo.TestResults.rundate, dbo.Bloodtest.DRAWDATE, 
                      dbo.TestResults.patikey, dbo.TestResults.testkey
FROM         dbo.TestResults INNER JOIN
                      dbo.TestResults TestResultsA ON dbo.TestResults.blookey = TestResultsA.blookey AND dbo.TestResults.patikey = TestResultsA.patikey AND 
                      TestResultsA.testkey = 109 INNER JOIN
                      dbo.Bloodtest ON dbo.TestResults.blookey = dbo.Bloodtest.RECORDKEY INNER JOIN
                      dbo.OrdPanel ON dbo.TestResults.Ordpkey = dbo.OrdPanel.RecordKey INNER JOIN
                      dbo.Test ON dbo.TestResults.testkey = dbo.Test.RecordKey
WHERE     (dbo.OrdPanel.done = 1) AND (dbo.TestResults.numresult <> - 999.99) OR
                      (dbo.Test.Name = 'Major')
GO
/****** Object:  Table [dbo].[TestSequence]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestSequence](
	[test] [int] NOT NULL,
	[seq] [int] NULL,
	[text] [varchar](40) NULL,
	[decimal] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[TestSequenceView]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE VIEW [dbo].[TestSequenceView]
AS
SELECT TestSequence.*, Test.RecordKey, Test.Name
FROM Test INNER JOIN
    TestSequence ON Test.RecordKey = TestSequence.test
GO
/****** Object:  View [dbo].[V_PatientFullName]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[V_PatientFullName]
AS
SELECT     TOP 100 PERCENT RecordKey, lastname, firstname, RTRIM(LTRIM(lastname)) + ', ' + RTRIM(LTRIM(firstname)) AS myfullname, ssn, dob, sex, 
                      customaccno
FROM         dbo.Patient
WHERE     (LEFT(lastname, 1) <> '(')
ORDER BY lastname
GO
/****** Object:  Table [dbo].[tblPatientUIDs]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblPatientUIDs](
	[RecordKey] [int] IDENTITY(1,1) NOT NULL,
	[PatientKey] [int] NOT NULL,
	[PatientUID] [char](50) NULL
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[viewPatients]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[viewPatients]
AS
SELECT     dbo.Patient.*, dbo.tblPatientUIDs.PatientUID AS PatientUID
FROM         dbo.Patient LEFT OUTER JOIN
                      dbo.tblPatientUIDs ON dbo.Patient.RecordKey = dbo.tblPatientUIDs.PatientKey
GO
/****** Object:  Table [dbo].[BridgeConfig]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BridgeConfig](
	[NAME] [varchar](50) NOT NULL,
	[VALUE] [varchar](2500) NULL,
	[CREATE_DATE] [datetime] NOT NULL,
	[MODIFIED_DATE] [datetime] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BridgeLogDetail]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BridgeLogDetail](
	[Id] [uniqueidentifier] NOT NULL,
	[BridgeLogId] [uniqueidentifier] NOT NULL,
	[StepName] [varchar](200) NOT NULL,
	[Message] [varchar](1000) NULL,
	[Create_Date] [datetime] NOT NULL,
	[ProjectName] [varchar](100) NULL,
	[LogLevel] [varchar](15) NULL,
 CONSTRAINT [PK_BridgeLogDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BridgeLogTblCounts]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BridgeLogTblCounts](
	[Id] [uniqueidentifier] NOT NULL,
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[COPIA_location]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[COPIA_location](
	[recordKey] [int] NOT NULL,
	[copiaKey] [int] NULL,
	[harvestKey] [int] NULL,
	[name] [varchar](60) NULL,
	[abbrev] [varchar](30) NULL,
	[abbrev2] [varchar](30) NULL,
	[addr1] [varchar](25) NULL,
	[addr2] [varchar](25) NULL,
	[zip] [varchar](10) NULL,
	[phone1] [varchar](10) NULL,
	[phone2] [varchar](40) NULL,
	[host] [varchar](25) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[copiaAdmissionType]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[copiaAdmissionType](
	[admissionTypeKey] [int] NOT NULL,
	[createStamp] [numeric](19, 0) NOT NULL,
	[hl7Code] [varchar](20) NULL,
	[isActive] [bit] NOT NULL,
	[name] [varchar](20) NULL,
	[updateStamp] [numeric](19, 0) NOT NULL,
	[updateVersion] [numeric](19, 0) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[copiaPatientType]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[copiaPatientType](
	[createStamp] [numeric](19, 0) NOT NULL,
	[hl7Code] [varchar](20) NULL,
	[isActive] [bit] NOT NULL,
	[name] [varchar](20) NULL,
	[patientTypeKey] [int] NOT NULL,
	[updateStamp] [numeric](19, 0) NOT NULL,
	[updateVersion] [numeric](19, 0) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ImportDay]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ImportDay](
	[ImportDayCount] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SYNC_JSON_DRIVER]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblLocationProviderRelationship_Working]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblLocationProviderRelationship_Working](
	[locationKey] [int] NULL,
	[providerKey] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblReportStore]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblReportStore](
	[RptID] [int] IDENTITY(1,1) NOT NULL,
	[RptBinData] [binary](50) NULL,
	[RptDateRun] [datetime] NULL,
	[RptDateRequest] [datetime] NULL,
	[UserID] [int] NULL,
	[ReportID] [int] NULL,
 CONSTRAINT [PK_tblReportStore] PRIMARY KEY CLUSTERED 
(
	[RptID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblTestResultsText]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblTestResultsText](
	[RecordKey] [int] IDENTITY(1,1) NOT NULL,
	[TestResultKey] [int] NOT NULL,
	[TestResultDesc] [char](256) NULL,
 CONSTRAINT [PK_tblTestResultsText] PRIMARY KEY CLUSTERED 
(
	[RecordKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblWebAccessLog]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblWebAccessLog](
	[AccessID] [int] IDENTITY(1,1) NOT NULL,
	[AccessDateTime] [datetime] NOT NULL,
	[AccessType] [char](10) NOT NULL,
	[AccessDesc] [ntext] NOT NULL,
	[AccessUserID] [int] NOT NULL,
	[RemoteAddr] [char](50) NOT NULL,
	[RemoteHost] [char](255) NULL,
	[SessionID] [bigint] NULL,
 CONSTRAINT [PK_tblWebAccessLog] PRIMARY KEY CLUSTERED 
(
	[AccessID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblWebLocationReportRelationships]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblWebLocationReportRelationships](
	[RelationshipID] [int] IDENTITY(1,1) NOT NULL,
	[LocationID] [int] NOT NULL,
	[ReportID] [int] NOT NULL,
	[ParamPatientsDefFilter] [char](10) NULL,
 CONSTRAINT [PK_tblWebLocationReportRelationships] PRIMARY KEY CLUSTERED 
(
	[RelationshipID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblWebReports]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblWebReports](
	[ReportID] [int] IDENTITY(1,5) NOT NULL,
	[ReportName] [char](255) NOT NULL,
	[ReportWWWPath] [ntext] NOT NULL,
	[ReportDesc] [ntext] NOT NULL,
	[ParamLocation] [int] NULL,
	[ParamLocationHidden] [bit] NULL,
	[ParamLocationUsed] [bit] NULL,
	[ParamProviders] [int] NULL,
	[ParamProvidersHidden] [bit] NULL,
	[ParamProvidersUsed] [bit] NULL,
	[ParamPatients] [int] NULL,
	[ParamPatientsHidden] [bit] NULL,
	[ParamPatientsUsed] [bit] NULL,
	[ParamPatientsFormat] [char](10) NULL,
	[ParamPatientsDefFilter] [char](10) NULL,
	[ParamPatientsBy] [char](10) NULL,
	[ParamPatientsLimitToModal] [bit] NULL,
	[ParamPatientsLimitToDefFilter] [bit] NULL,
	[ParamDateRange] [int] NULL,
	[ParamDateRangeHidden] [bit] NULL,
	[ParamDateRangeUsed] [bit] NULL,
	[ParamDateRangeSingle] [bit] NULL,
	[ParamDateIsString] [bit] NULL,
	[ParamDefDateRange] [int] NULL,
	[ParamDefDateRangeUnit] [char](1) NULL,
	[UseOldStyle] [bit] NULL,
	[PreprocessHook] [char](255) NULL,
	[ViewHook] [char](255) NULL,
 CONSTRAINT [PK_tblWebReports] PRIMARY KEY CLUSTERED 
(
	[ReportID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblWebSubreports]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblWebSubreports](
	[SubreportID] [int] IDENTITY(1,1) NOT NULL,
	[SubreportName] [char](100) NOT NULL,
	[ReportID] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblWebSuperLocationRelationships]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblWebSuperLocationRelationships](
	[RelationshipID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[LocationID] [int] NOT NULL,
 CONSTRAINT [PK_tblWebSuperLocationRelationships] PRIMARY KEY CLUSTERED 
(
	[RelationshipID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblWebUsers]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblWebUsers](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [char](10) NULL,
	[ProviderID] [int] NULL,
	[Password] [char](10) NULL,
	[LimitToProvider] [bit] NULL,
	[IsSuperUser] [bit] NULL,
	[PasswordResetReq] [bit] NULL,
 CONSTRAINT [PK_tblWebUsers] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TestHarvest]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestHarvest](
	[RecordKey] [int] NOT NULL,
	[Name] [varchar](30) NULL,
	[abbrev] [varchar](20) NULL,
	[CopiaDescription] [varchar](37) NULL,
	[units] [varchar](15) NULL,
	[posneg] [bit] NOT NULL,
	[major] [char](1) NOT NULL,
	[groupNumber] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TestLegacy]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestLegacy](
	[RecordKey] [int] NOT NULL,
	[Name] [varchar](30) NULL,
	[abbrev] [varchar](12) NULL,
	[units] [varchar](15) NULL,
	[posneg] [bit] NOT NULL,
	[major] [char](1) NOT NULL,
	[groupNumber] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TestResults_Note]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestResults_Note](
	[resultNoteKey] [int] NOT NULL,
	[resultKey] [int] NOT NULL,
	[note] [varchar](1000) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TestSeqExceptions]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestSeqExceptions](
	[test] [int] NOT NULL,
	[seq] [int] NULL,
	[text] [varchar](40) NULL,
	[decimal] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Bloodtest] ADD  DEFAULT (getdate()) FOR [CREATE_DATE]
GO
ALTER TABLE [dbo].[Bloodtest] ADD  DEFAULT ('New') FOR [ProcessingStatus]
GO
ALTER TABLE [dbo].[BridgeConfig] ADD  CONSTRAINT [DF_BridgeConfig_CREATE_DATE]  DEFAULT (getdate()) FOR [CREATE_DATE]
GO
ALTER TABLE [dbo].[BridgeConfig] ADD  CONSTRAINT [DF_BridgeConfig_MODIFIED_DATE]  DEFAULT (getdate()) FOR [MODIFIED_DATE]
GO
ALTER TABLE [dbo].[BridgeLogDetail] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[BridgeLogTblCounts] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[OrdPanel] ADD  DEFAULT ('New') FOR [ProcessingStatus]
GO
ALTER TABLE [dbo].[Patient] ADD  DEFAULT ('New') FOR [ProcessingStatus]
GO
ALTER TABLE [dbo].[Provider] ADD  DEFAULT ('New') FOR [ProcessingStatus]
GO
ALTER TABLE [dbo].[SYNC_JSON_DRIVER] ADD  CONSTRAINT [DF_Table_1_recordKey]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[SYNC_JSON_DRIVER] ADD  CONSTRAINT [DF_SYNC_JSON_DRIVER_IS_LOCKED]  DEFAULT ((0)) FOR [IS_LOCKED]
GO
ALTER TABLE [dbo].[SYNC_JSON_DRIVER] ADD  CONSTRAINT [DF_SYNC_JSON_DRIVER_CREATE_DATE]  DEFAULT (getdate()) FOR [CREATE_DATE]
GO
ALTER TABLE [dbo].[SYNC_JSON_DRIVER] ADD  CONSTRAINT [DF_SYNC_JSON_DRIVER_MODIFIED_DATE]  DEFAULT (getdate()) FOR [MODIFIED_DATE]
GO
ALTER TABLE [dbo].[tblWebReports] ADD  CONSTRAINT [DF_tblWebReports_ParamPatientsFormat]  DEFAULT ('FULL') FOR [ParamPatientsFormat]
GO
ALTER TABLE [dbo].[tblWebReports] ADD  CONSTRAINT [DF_tblWebReports_ParamPatientsDefFilter]  DEFAULT ('h') FOR [ParamPatientsDefFilter]
GO
ALTER TABLE [dbo].[tblWebReports] ADD  CONSTRAINT [DF_tblWebReports_ParamPatientsBy]  DEFAULT ('PROV') FOR [ParamPatientsBy]
GO
ALTER TABLE [dbo].[tblWebReports] ADD  CONSTRAINT [DF_tblWebReports_ParamPatientsLimitToModal]  DEFAULT ((0)) FOR [ParamPatientsLimitToModal]
GO
ALTER TABLE [dbo].[tblWebReports] ADD  CONSTRAINT [DF_tblWebReports_ParamPatientsLimitToDefFilter]  DEFAULT ((0)) FOR [ParamPatientsLimitToDefFilter]
GO
ALTER TABLE [dbo].[tblWebReports] ADD  CONSTRAINT [DF_tblWebReports_ParamDateIsString]  DEFAULT ((0)) FOR [ParamDateIsString]
GO
ALTER TABLE [dbo].[tblWebReports] ADD  CONSTRAINT [DF_tblWebReports_ParamDefDateRangeUnit]  DEFAULT ('m') FOR [ParamDefDateRangeUnit]
GO
ALTER TABLE [dbo].[tblWebUsers] ADD  CONSTRAINT [DF_tblWebUsers_LimitToProvider]  DEFAULT ((0)) FOR [LimitToProvider]
GO
ALTER TABLE [dbo].[tblWebUsers] ADD  CONSTRAINT [DF_tblWebUsers_IsSuperUser]  DEFAULT ((0)) FOR [IsSuperUser]
GO
ALTER TABLE [dbo].[Test] ADD  DEFAULT ('N') FOR [major]
GO
ALTER TABLE [dbo].[Test] ADD  DEFAULT ('New') FOR [ProcessingStatus]
GO
ALTER TABLE [dbo].[TestResults] ADD  DEFAULT ('New') FOR [ProcessingStatus]
GO
/****** Object:  StoredProcedure [dbo].[BridgeFailCompare]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[BridgeFailCompare] AS
GO
/****** Object:  StoredProcedure [dbo].[BridgeFailureCompare]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[BridgeFailureCompare] AS
GO
/****** Object:  StoredProcedure [dbo].[BridgeFailureMail]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[BridgeFailureMail] AS 
EXEC master.dbo.xp_startmail
EXEC master.dbo.xp_sendmail 'bmcarthur@concentric-us.com; jeremy@concentric-us.com; jon@concentric-us.com', 'TEST 3 - HarvestSQL bridge has not completed in more than 4 hours'
EXEC master.dbo.sp_processmail
EXEC master.dbo.xp_stopmail
GO
/****** Object:  StoredProcedure [dbo].[GetLastCompleteDate]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetLastCompleteDate] AS
SELECT MAX(EndDateTime) as LastCompleteDateTime 
FROM Bridgelog 
WHERE UPPER(LTRIM(RTRIM(Bridgelog.status))) = 'COMPLETE'
GO
/****** Object:  StoredProcedure [dbo].[ind_rebuild]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[ind_rebuild]
AS
DECLARE @TableName sysname
DECLARE cur_reindex CURSOR FOR
SELECT table_name 
  FROM information_schema.tables 
  WHERE table_type = 'base table'
OPEN cur_reindex
FETCH NEXT FROM cur_reindex INTO @TableName
WHILE @@FETCH_STATUS = 0
BEGIN 
  PRINT 'Reindexing ' + @TableName + ' table'
  DBCC DBREINDEX (@TableName, ' ', 80)
  FETCH NEXT FROM cur_reindex INTO @TableName
END
CLOSE cur_reindex
DEALLOCATE cur_reindex
GO
/****** Object:  StoredProcedure [dbo].[Junk]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[Junk] AS
/*
-- Old Proc Modifications by James Fuller 4-28-2003
SELECT MAX(EndDateTime) as LastCompleteDateTime 
FROM Bridgelog 
WHERE UPPER(LTRIM(RTRIM(Bridgelog.status))) = 'COMPLETE'
-- End of old Proc Modifications by James Fuller 4-28-2003
*/
DECLARE @ReturnValue AS INT
SET @ReturnValue = 0
IF (SELECT DATEDIFF(Hour,MAX(EndDateTime), getdate()) AS no_of_Hours
FROM Bridgelog 
WHERE UPPER(LTRIM(RTRIM(Bridgelog.status))) = 'COMPLETE')> 5
SET @ReturnValue = 1
EXEC HarvestSQL.dbo.BridgeFailureMail
-- Returns 0 if we are good within the number of hours
-- Otherwise, returns 1
RETURN @ReturnValue
GO
/****** Object:  StoredProcedure [dbo].[PatientResultsSP]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[PatientResultsSP] @PatientID int AS
SELECT TestSequenceView.*, TestResultsMajorView.*, SUBSTRING(textresult, 1, 200) As textresultstr
FROM TestSequenceView LEFT OUTER JOIN
    TestResultsMajorView ON 
    TestResultsMajorView.Testkey = TestSequenceView.RecordKey AND
     TestResultsMajorView.patikey = @PatientID
ORDER BY TestSequenceView.Seq
GO
/****** Object:  StoredProcedure [dbo].[ReportCardResultsSP]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[ReportCardResultsSP] @PatientID int AS
SELECT ReportCardSequenceView.*, TestResultsMajorView.*, SUBSTRING(textresult, 1, 200) As textresultstr
FROM ReportCardSequenceView INNER JOIN
    TestResultsMajorView ON 
    TestResultsMajorView.Testkey = ReportCardSequenceView.RecordKey AND
     TestResultsMajorView.patikey = @PatientID
ORDER BY ReportCardSequenceView.Seq
GO
/****** Object:  StoredProcedure [dbo].[SP_ResetProviderLocationKey]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[SP_ResetProviderLocationKey] ( @LocAbbrev varchar(10), @LocName varchar(150))
as

IF(@LocName IS NOT NULL)
BEGIN
-- Update existing Provider lockey
UPDATE PROVIDER_Worker
SET PROVIDER_Worker.lockey = l.recordkey
FROM Location l, PROVIDER_Worker p
WHERE substring(p.lastname, 0, (charindex(')', p.lastname) + 1)) = '' + @LocAbbrev + '' AND l.name = '' + @LocName + ''

-- Delete if lockey is still null, location didn't exist
DELETE PROVIDER_Worker WHERE substring(lastname, 0, (charindex(')', lastname) + 1)) = '' + @LocAbbrev + '' AND lockey IS NULL

END
IF(@LocName IS  NULL)
BEGIN
-- Update existing Provider lockey
UPDATE PROVIDER_Worker
SET PROVIDER_Worker.lockey = l.recordkey
FROM Location l, PROVIDER_Worker p
WHERE substring(p.lastname, 0, (charindex(')', p.lastname) + 1)) = '' + @LocAbbrev + ''

-- Delete if lockey is still null, location didn't exist
DELETE PROVIDER_Worker WHERE substring(lastname, 0, (charindex(')', lastname) + 1)) = '' + @LocAbbrev + '' AND lockey IS NULL

END
GO
/****** Object:  StoredProcedure [dbo].[spAppendPatientUID]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[spAppendPatientUID] AS
INSERT INTO dbo.tblPatientUIDs ( PatientKey, PatientUID )
SELECT dbo.Patient.RecordKey, (LTrim(RTrim(dbo.Patient.lastname))+LTrim(RTrim(dbo.Patient.firstname))+LTrim(RTrim(dbo.Patient.RecordKey))) AS PatientUID
FROM dbo.Patient
WHERE  dbo.Patient.RecordKey NOT IN ( Select dbo.tblPatientUIDs.PatientKey FROM dbo.tblPatientUIDs )
GO
/****** Object:  StoredProcedure [dbo].[spMakeHepResults]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[spMakeHepResults] AS
/*This is to be removed*/
UPDATE dbo.TestResults SET dbo.TestResults.textresult = 'POS'  
WHERE RecordKey In  ( 
	SELECT dbo.TestResults.RecordKey
	FROM dbo.TestResults INNER JOIN dbo.OrdPanel ON dbo.TestResults.Ordpkey = dbo.OrdPanel.RecordKey
	WHERE (dbo.TestResults.numresult >= 11) AND (dbo.TestResults.testkey = 33) AND (dbo.OrdPanel.done = 1)
)
UPDATE dbo.TestResults SET dbo.TestResults.textresult = 'NEG'  
WHERE RecordKey In  ( 
	SELECT dbo.TestResults.RecordKey
	FROM dbo.TestResults INNER JOIN dbo.OrdPanel ON dbo.TestResults.Ordpkey = dbo.OrdPanel.RecordKey
	WHERE (((dbo.TestResults.numresult)>=0) AND ((dbo.TestResults.numresult)<=9) AND ((dbo.TestResults.testkey)=33)) AND (dbo.OrdPanel.done = 1)
)
UPDATE dbo.TestResults SET dbo.TestResults.textresult = 'REP'  
WHERE RecordKey In  ( 
	SELECT dbo.TestResults.RecordKey
	FROM dbo.TestResults INNER JOIN dbo.OrdPanel ON dbo.TestResults.Ordpkey = dbo.OrdPanel.RecordKey
	WHERE (((dbo.TestResults.numresult)<11) AND ((dbo.TestResults.numresult)>9) AND ((dbo.TestResults.testkey)=33)) AND (dbo.OrdPanel.done = 1)
)
UPDATE dbo.TestResults SET dbo.TestResults.textresult = 'POS'  
WHERE RecordKey In  ( 
	SELECT dbo.TestResults.RecordKey
	FROM dbo.TestResults INNER JOIN dbo.OrdPanel ON dbo.TestResults.Ordpkey = dbo.OrdPanel.RecordKey
	WHERE (((dbo.TestResults.numresult)=1) AND ((dbo.TestResults.testkey)=32)) AND (dbo.OrdPanel.done = 1)
)
UPDATE dbo.TestResults SET dbo.TestResults.textresult = 'NEG'  
WHERE RecordKey In  ( 
	SELECT dbo.TestResults.RecordKey
	FROM dbo.TestResults INNER JOIN dbo.OrdPanel ON dbo.TestResults.Ordpkey = dbo.OrdPanel.RecordKey
	WHERE (((dbo.TestResults.numresult)=0) AND ((dbo.TestResults.testkey)=32)) AND (dbo.OrdPanel.done = 1)
)
UPDATE dbo.TestResults SET dbo.TestResults.textresult = 'POS'  
WHERE RecordKey In  ( 
	SELECT dbo.TestResults.RecordKey
	FROM dbo.TestResults INNER JOIN dbo.OrdPanel ON dbo.TestResults.Ordpkey = dbo.OrdPanel.RecordKey
	WHERE (((dbo.TestResults.numresult)=1) AND ((dbo.TestResults.testkey)=34)) AND (dbo.OrdPanel.done = 1)
)
UPDATE dbo.TestResults SET dbo.TestResults.textresult = 'NEG'  
WHERE RecordKey In  ( 
	SELECT dbo.TestResults.RecordKey
	FROM dbo.TestResults INNER JOIN dbo.OrdPanel ON dbo.TestResults.Ordpkey = dbo.OrdPanel.RecordKey
	WHERE (((dbo.TestResults.numresult)=0) AND ((dbo.TestResults.testkey)=34)) AND (dbo.OrdPanel.done = 1)
)
/* New table
tblTestResultsText
Fields:
RecordKey
TestResultKey
TestResultDesc
*/
/*
--Inserts--
This only puts an entry in the dbo.tblTestResultsText for a TestResultKey for hep tests...  
alter TestResults.Testkey clause to change that to include other tests
The updating takes care of the adding the testresultdesc
*/
INSERT INTO dbo.tblTestResultsText (TestResultKey) 
	SELECT RecordKey from dbo.TestResults WHERE RecordKey NOT IN (SELECT TestResultKey FROM dbo.tblTestResultsText) AND dbo.TestResults.testkey IN (33, 32, 34)
/*
--Updates--
 */
UPDATE dbo.tblTestResultsText SET dbo.tblTestResultsText.TestResultDesc = 'POS'  
WHERE TestResultKey In  ( 
	SELECT dbo.TestResults.RecordKey
	FROM dbo.TestResults INNER JOIN dbo.OrdPanel ON dbo.TestResults.Ordpkey = dbo.OrdPanel.RecordKey
	WHERE (dbo.TestResults.numresult >= 11) AND (dbo.TestResults.testkey = 33) AND (dbo.OrdPanel.done = 1)
	) 
AND TestResultKey In (
	Select TestResultKey FROM dbo.tblTestResultsText
	)
UPDATE dbo.tblTestResultsText SET dbo.tblTestResultsText.TestResultDesc = 'NEG'  
WHERE TestResultKey In  ( 
	SELECT dbo.TestResults.RecordKey
	FROM dbo.TestResults INNER JOIN dbo.OrdPanel ON dbo.TestResults.Ordpkey = dbo.OrdPanel.RecordKey
	WHERE (((dbo.TestResults.numresult)>=0) AND ((dbo.TestResults.numresult)<=9) AND ((dbo.TestResults.testkey)=33)) AND (dbo.OrdPanel.done = 1)
	) 
AND TestResultKey In (
	Select TestResultKey FROM dbo.tblTestResultsText
	)
UPDATE dbo.tblTestResultsText SET dbo.tblTestResultsText.TestResultDesc = 'REP'  
WHERE TestResultKey In  ( 
	SELECT dbo.TestResults.RecordKey
	FROM dbo.TestResults INNER JOIN dbo.OrdPanel ON dbo.TestResults.Ordpkey = dbo.OrdPanel.RecordKey
	WHERE (((dbo.TestResults.numresult)<11) AND ((dbo.TestResults.numresult)>9) AND ((dbo.TestResults.testkey)=33)) AND (dbo.OrdPanel.done = 1)
	) 
AND TestResultKey In (
	Select TestResultKey FROM dbo.tblTestResultsText
	)
UPDATE dbo.tblTestResultsText SET dbo.tblTestResultsText.TestResultDesc = 'POS'  
WHERE TestResultKey In  ( 
	SELECT dbo.TestResults.RecordKey
	FROM dbo.TestResults INNER JOIN dbo.OrdPanel ON dbo.TestResults.Ordpkey = dbo.OrdPanel.RecordKey
	WHERE (((dbo.TestResults.numresult)=1) AND ((dbo.TestResults.testkey)=32)) AND (dbo.OrdPanel.done = 1)
	) 
AND TestResultKey In (
	Select TestResultKey FROM dbo.tblTestResultsText
	)
UPDATE dbo.tblTestResultsText SET dbo.tblTestResultsText.TestResultDesc = 'NEG'  
WHERE TestResultKey In  ( 
	SELECT dbo.TestResults.RecordKey
	FROM dbo.TestResults INNER JOIN dbo.OrdPanel ON dbo.TestResults.Ordpkey = dbo.OrdPanel.RecordKey
	WHERE (((dbo.TestResults.numresult)=0) AND ((dbo.TestResults.testkey)=32)) AND (dbo.OrdPanel.done = 1)
	) 
AND TestResultKey In (
	Select TestResultKey FROM dbo.tblTestResultsText
	)
UPDATE dbo.tblTestResultsText SET dbo.tblTestResultsText.TestResultDesc = 'POS'  
WHERE TestResultKey In  ( 
	SELECT dbo.TestResults.RecordKey
	FROM dbo.TestResults INNER JOIN dbo.OrdPanel ON dbo.TestResults.Ordpkey = dbo.OrdPanel.RecordKey
	WHERE (((dbo.TestResults.numresult)=1) AND ((dbo.TestResults.testkey)=34)) AND (dbo.OrdPanel.done = 1)
	) 
AND TestResultKey In (
	Select TestResultKey FROM dbo.tblTestResultsText
	)
UPDATE dbo.tblTestResultsText SET dbo.tblTestResultsText.TestResultDesc = 'NEG'  
WHERE TestResultKey In  ( 
	SELECT dbo.TestResults.RecordKey
	FROM dbo.TestResults INNER JOIN dbo.OrdPanel ON dbo.TestResults.Ordpkey = dbo.OrdPanel.RecordKey
	WHERE (((dbo.TestResults.numresult)=0) AND ((dbo.TestResults.testkey)=34)) AND (dbo.OrdPanel.done = 1)
	) 
AND TestResultKey In (
	Select TestResultKey FROM dbo.tblTestResultsText
	)
GO
/****** Object:  StoredProcedure [dbo].[StatusCheck]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE  PROCEDURE [dbo].[StatusCheck] AS
SELECT id AS IDofFubar
FROM BridgeLog
WHERE ID IN (SELECT TOP 8 ID FROM BridgeLog ORDER BY ID DESC)
AND ID NOT IN (SELECT MAX(ID) FROM BridgeLog)
AND Status <> 'Complete' 
ORDER BY ID DESC
IF @@Rowcount <> 0
	EXEC master.dbo.xp_sendmail 'alert@concentric-us.com', 'There has been a Harvest Bridge Failure.  Please check Bridge Log.'
GO
/****** Object:  StoredProcedure [dbo].[uspBridge_UpdateTestResults_FlattenNotes]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[uspBridge_UpdateTestResults_FlattenNotes] AS

update dbo.TestResults
set TextResult = dbo.Bridge_TestResults_FlattenNote(RecordKey)
where RecordKey in 
	(select resultkey
	from dbo.testresults_note (NOLOCK)
	group by resultkey)

/*update TR
set TR.TextResult = dbo.Bridge_TestResults_FlattenNote(TR.RecordKey)
from dbo.TestResults TR inner join
	dbo.TestResults_Note Notes
	on tr.RecordKey = Notes.resultKey
*/
GO
/****** Object:  StoredProcedure [dbo].[ZapEm]    Script Date: 11/19/2023 12:34:01 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  Stored Procedure dbo.ZapEm    Script Date: 11/20/2002 6:29:48 AM ******/
CREATE PROCEDURE [dbo].[ZapEm] AS
delete from test
delete from testResults
Delete from ordpanel
Delete From panel
Delete From BloodTest
Delete From Patient
Delete From Location
Delete From Provider
GO
