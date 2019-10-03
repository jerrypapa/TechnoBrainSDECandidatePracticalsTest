SELECT C.[CarMaker],C.[CarModel], SUM(C.[SalePriceInDollar]) AS [Total Sale] FROM [dbo].[CarSales] C
WHERE C.[SaleDate] >= DATEADD(month,-1,GETDATE())
GROUP BY C.[CarMaker],C.[CarModel];