USE [EmailCounts]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[vwAllEmails_Trace]
AS
SELECT * FROM AllEmails E
WHERE NOT EXISTS 
(
--Only select where doesn't exist in "Exclude" table
SELECT * FROM Exclusions Ex 
WHERE Ex.FullAddress = E.SenderAddress 
OR Ex.Domain = E.SenderDomain
)
AND EXISTS 
(
--Only select where does exist in "Capture" table
SELECT EmailAddress 
FROM RecipientsToCapture C 
WHERE C.EmailAddress = E.RecipientAddress
)
 

GO