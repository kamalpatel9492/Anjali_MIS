USE [DB_A157D8_AnjaliMIS]
GO

/****** Object:  StoredProcedure [dbo].[PP_INV_Invoice_SelectForPrint]    Script Date: 31-10-2018 10:07:39 AM ******/
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

--   [dbo].[PP_INV_Invoice_SelectForPrint] @InvoiceID = 1

Create PROCEDURE [dbo].[PP_INV_Invoice_SelectForPrint]

		@InvoiceID 		int

AS

SET NOCOUNT ON;

SELECT  
		[dbo].[INV_Invoice].[InvoiceID],
		[dbo].[INV_Invoice].[InvoiceNo],
		[dbo].[INV_Invoice].[InvoiceDate],
		[dbo].[INV_Invoice].[PONo],
		[dbo].[INV_Invoice].[CompanyID],
		[dbo].[SYS_Company].[CompanyName],
		REPLACE([dbo].[SYS_Company].[LogoPath],'~',[dbo].[GetDomainURL]()) as LogoPath,
		[dbo].[SYS_Company].[Address] as CompanyAddress,
		[dbo].[SYS_Company].[GSTIN] as CompanyGSTIN,
		[dbo].[SYS_Company].[Phone],
		[dbo].[LOC_City].[CityName] as ComapanyCity,
		[dbo].[INV_Invoice].[PartyID],
		[dbo].[MST_Party].[PartyName],
		[dbo].[MST_Party].[Mobile],
		[dbo].[MST_Party].[GSTIN] as PartyGSTIN,
		[dbo].[MST_Party].[Address] as PartyAddress,
		[PartyCountryID].[CountryName],
		[PartyStateID].[StateName],
		[PartyCityID].[CityName],
		[CGST_ACC_Tax].[Percentage] AS [CGST_Percentage],
		[dbo].[INV_Invoice].[CGSTAmount],
		[SGST_ACC_Tax].[Percentage] AS [SGST_Percentage],
		[dbo].[INV_Invoice].[SGSTAmount],
		[IGST_ACC_Tax].[Percentage] AS [IGST_Percentage],
		[dbo].[INV_Invoice].[IGSTAmount],
		[dbo].[INV_Invoice].[TotalAmount],
		[dbo].[INV_Item].[ItemID],
		[dbo].[INV_Item].[ItemName],
		[dbo].[INV_InvoiceItem].[Quantity],
		[dbo].[INV_InvoiceItem].[PricePerUnit]
FROM  [dbo].[INV_Invoice]

INNER JOIN [dbo].[INV_InvoiceItem]
ON [dbo].[INV_InvoiceItem].[InvoiceID] = [dbo].[INV_Invoice].[InvoiceID]
INNER JOIN [dbo].[INV_Item]
ON [dbo].[INV_Item].[ItemID] = [dbo].[INV_InvoiceItem].[ItemID]
INNER JOIN [dbo].[MST_Party]
ON [dbo].[INV_Invoice].[PartyID] = [dbo].[MST_Party].[PartyID]
INNER JOIN [dbo].[LOC_Country] as PartyCountryID
ON PartyCountryID.[CountryID] = [dbo].[MST_Party].[CountryID]
INNER JOIN [dbo].[LOC_State] as PartyStateID
ON PartyStateID.[StateID] = [dbo].[MST_Party].[StateID]
INNER JOIN [dbo].[LOC_City] as PartyCityID
ON PartyCityID.[CityID] = [dbo].[MST_Party].[CityID]
INNER JOIN [dbo].[SYS_Company]
ON [dbo].[INV_Invoice].[CompanyID] = [dbo].[SYS_Company].[CompanyID]
INNER JOIN [dbo].[LOC_City]
ON [dbo].[LOC_City].[CityID] = [dbo].[SYS_Company].[CityID]

LEFT OUTER JOIN [dbo].[ACC_Tax] AS [CGST_ACC_Tax]
ON [dbo].[INV_Invoice].[CGST] = [CGST_ACC_Tax].[TaxID]
LEFT OUTER JOIN [dbo].[ACC_Tax] AS [SGST_ACC_Tax]
ON [dbo].[INV_Invoice].[SGST] = [SGST_ACC_Tax].[TaxID]
LEFT OUTER JOIN [dbo].[ACC_Tax] AS [IGST_ACC_Tax]
ON [dbo].[INV_Invoice].[IGST] = [IGST_ACC_Tax].[TaxID]

WHERE [dbo].[INV_Invoice].[InvoiceID] = @InvoiceID



GO


