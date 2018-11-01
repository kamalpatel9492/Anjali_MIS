USE [DB_A157D8_AnjaliMIS]
GO

/****** Object:  StoredProcedure [dbo].[PP_INV_Item_SelectItemStockReportByCategoryID]    Script Date: 31-10-2018 12:04:47 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*
==================================
Author      : <Author,,Name>
Create date : <Create Date,,>
Description : <Description,,>
==================================
*/

--   [dbo].[PP_INV_Item_SelectItemStockReportByCategoryID]

CREATE PROCEDURE [dbo].[PP_INV_Item_SelectItemStockReportByCategoryID]
	@CategoryID	int,
	@CompanyID	int

AS

SET NOCOUNT ON;

SELECT  
		[dbo].[INV_Item].[ItemID],
		[dbo].[INV_Category].[CategoryID],
		[dbo].[INV_Category].[CategoryName],
		[dbo].[INV_Item].[CompanyID],
		[dbo].[SYS_Company].[CompanyName],
		REPLACE([dbo].[SYS_Company].[LogoPath],'~',[dbo].[GetDomainURL]()) as LogoPath,
		[dbo].[INV_Item].[ItemName],
		[dbo].[INV_Item].[ItemCode],
		[dbo].[INV_Item].[UnitID],
		[dbo].[INV_Unit].[Unit],
		[dbo].[INV_Item].[IsConfigurable],
		[dbo].[INV_Item].[Quantity],
		[dbo].[INV_Item].[MinStockLimit],
		[dbo].[INV_Item].[RejectedQuantity]
FROM	[dbo].[INV_Item]
INNER JOIN [dbo].[INV_Unit]
ON		[dbo].[INV_Item].[UnitID] = [dbo].[INV_Unit].[UnitID]
INNER JOIN [dbo].[SYS_Company]
ON		[dbo].[INV_Item].[CompanyID] = [dbo].[SYS_Company].[CompanyID]
INNER JOIN [dbo].[INV_Category]
ON		[dbo].[INV_Category].[CategoryID] = [dbo].[INV_Item].[CategoryID]
WHERE	(@CategoryID IS NULL OR [dbo].[INV_Item].[CategoryID] = @CategoryID)
AND		[dbo].[INV_Item].[CompanyID] =  @CompanyID

GO


