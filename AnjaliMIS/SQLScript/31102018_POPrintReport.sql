USE [DB_A157D8_AnjaliMIS]
GO

/****** Object:  StoredProcedure [dbo].[PP_INV_PurchaseOrder_SelectForPrint]    Script Date: 31-10-2018 12:05:19 AM ******/
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

--   [dbo].[PP_INV_PurchaseOrder_SelectForPrint] @PurchaseOrderID = 1

CREATE PROCEDURE [dbo].[PP_INV_PurchaseOrder_SelectForPrint]

		@PurchaseOrderID 		int

AS

SET NOCOUNT ON;

SELECT  
		[dbo].[INV_PurchaseOrder].[PurchaseOrderID],
		[dbo].[INV_PurchaseOrder].[PONo],
		[dbo].[INV_PurchaseOrder].[PODate],
		[dbo].[INV_PurchaseOrder].[CompanyID],
		[dbo].[SYS_Company].[CompanyName],
		[dbo].[SYS_Company].[Address] as CompanyAddress,
		[dbo].[SYS_Company].[GSTIN] as CompanyGSTIN,
		[dbo].[SYS_Company].[Phone],
		[dbo].[LOC_City].[CityName] as ComapanyCity,
		[dbo].[INV_PurchaseOrder].[SellerPartyID],
		[dbo].[MST_Party].[PartyName],
		[dbo].[MST_Party].[Mobile],
		[dbo].[MST_Party].[GSTIN] as PartyGSTIN,
		[dbo].[MST_Party].[Address] as PartyAddress,
		[PartyCountryID].[CountryName],
		[PartyStateID].[StateName],
		[PartyCityID].[CityName],
		[CGST_ACC_Tax].[Percentage] AS [CGSTPercentage],
		[dbo].[INV_PurchaseOrder].[CGSTAmount],
		[SGST_ACC_Tax].[Percentage] AS [SGSTPercentage],
		[dbo].[INV_PurchaseOrder].[SGSTAmount],
		[IGST_ACC_Tax].[Percentage] AS [IGSTPercentage],
		[dbo].[INV_PurchaseOrder].[IGSTAmount],
		[dbo].[INV_PurchaseOrder].[TotalAmount],
		[dbo].[INV_Item].[ItemID],
		[dbo].[INV_Item].[ItemName],
		[dbo].[INV_PurchaseOrderItem].[OrderedQuantity],
		[dbo].[INV_PurchaseOrderItem].[PuchasePrice]
FROM  [dbo].[INV_PurchaseOrder]

INNER JOIN [dbo].[INV_PurchaseOrderItem]
ON [dbo].[INV_PurchaseOrderItem].[PurchaseOrderID] = [dbo].[INV_PurchaseOrder].[PurchaseOrderID]
INNER JOIN [dbo].[INV_Item]
ON [dbo].[INV_Item].[ItemID] = [dbo].[INV_PurchaseOrderItem].[ItemID]
INNER JOIN [dbo].[SYS_Company]
ON [dbo].[INV_PurchaseOrder].[CompanyID] = [dbo].[SYS_Company].[CompanyID]
INNER JOIN [dbo].[LOC_City]
ON [dbo].[LOC_City].[CityID] = [dbo].[SYS_Company].[CityID]
INNER JOIN [dbo].[MST_Party]
ON [dbo].[INV_PurchaseOrder].[SellerPartyID] = [dbo].[MST_Party].[PartyID]
INNER JOIN [dbo].[LOC_Country] as PartyCountryID
ON PartyCountryID.[CountryID] = [dbo].[MST_Party].[CountryID]
INNER JOIN [dbo].[LOC_State] as PartyStateID
ON PartyStateID.[StateID] = [dbo].[MST_Party].[StateID]
INNER JOIN [dbo].[LOC_City] as PartyCityID
ON PartyCityID.[CityID] = [dbo].[MST_Party].[CityID]

LEFT OUTER JOIN [dbo].[ACC_Tax] AS [CGST_ACC_Tax]
ON [dbo].[INV_PurchaseOrder].[CGST] = [CGST_ACC_Tax].[TaxID]
LEFT OUTER JOIN [dbo].[ACC_Tax] AS [SGST_ACC_Tax]
ON [dbo].[INV_PurchaseOrder].[SGST] = [SGST_ACC_Tax].[TaxID]
LEFT OUTER JOIN [dbo].[ACC_Tax] AS [IGST_ACC_Tax]
ON [dbo].[INV_PurchaseOrder].[IGST] = [IGST_ACC_Tax].[TaxID]


WHERE [dbo].[INV_PurchaseOrder].[PurchaseOrderID] = @PurchaseOrderID



GO


