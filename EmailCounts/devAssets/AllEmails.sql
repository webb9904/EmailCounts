USE [EmailCounts]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AllEmails](
	[Id] [int] NOT NULL,
	[SentDate] [date] NULL,
	[SentDateTime] [datetime] NULL,
	[SenderAddress] [nvarchar](250) NULL,
	[SenderDomain] [nvarchar](250) NULL,
	[RecipientAddress] [nvarchar](250) NULL,
 CONSTRAINT [PK_AllEmails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO