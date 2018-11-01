USE [DB_A157D8_AnjaliMIS]
GO

/****** Object:  UserDefinedFunction [dbo].[GetDomainURL]    Script Date: 01-11-2018 9:28:05 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[GetDomainURL]()
RETURNS varchar(100)
AS
BEGIN
	RETURN 'http://localhost:3547'

END

GO


