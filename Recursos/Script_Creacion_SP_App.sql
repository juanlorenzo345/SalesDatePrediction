-- =============================================
-- Author:		Juan Lorenzo Mejia
-- Create date: 07-04-2023
-- Description:	Script para crear los procedimientos almacenados que usa la APP
-- =============================================

USE [StoreSample]
GO
/****** Object:  StoredProcedure [dbo].[AddNewOrder]    Script Date: 07/04/2023 18:23:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddNewOrder]
(
	@CustId INT = NULL,
	@EmpId INT = NULL,
	@OrderId INT = NULL,
	@ShipperId INT = NULL,
	@ShipName VARCHAR(80) = NULL,
	@ShipAddress VARCHAR(120) = NULL,
	@ShipCity VARCHAR(30) = NULL,
	@OrderDate DATETIME = NULL,
	@RequiredDate DATETIME = NULL,
	@ShippedDate DATETIME = NULL,
	@Freight MONEY = NULL,
	@ShipCountry NVARCHAR(30) = NULL,
	@Productid INT = NULL,
	@UnitPrice MONEY = NULL,
	@Qty SMALLINT = NULL,
	@Discount DECIMAL = NULL,

	@resultado INT = 0 OUT
	
)

AS
	
	BEGIN
							
		INSERT INTO Sales.Orders (Empid, CustID, Shipperid, Shipname, Shipaddress, Shipcity, Orderdate, Requireddate, Shippeddate, Freight, Shipcountry)
		VALUES (@EmpId, @CustId, @ShipperId, @ShipName, @ShipAddress, @ShipCity, @OrderDate, @RequiredDate, @ShippedDate, @Freight, @ShipCountry)

		-- Obtener el ID de la última orden insertada
		SET @orderId = SCOPE_IDENTITY()

		-- Insertar un producto en la tabla OrderDetails para la orden creada anteriormente
		INSERT INTO Sales.OrderDetails (Orderid, Productid, Unitprice, Qty, Discount)
		VALUES (@orderId, @Productid, @UnitPrice, @Qty, @Discount)

		SET @resultado = @orderId

	EXEC dbo.GetOrdersByIdClient -- int
						
	END
GO
/****** Object:  StoredProcedure [dbo].[GetCustomerOrders]    Script Date: 07/04/2023 18:23:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetCustomerOrders]
	

AS


BEGIN
	SET NOCOUNT ON;
	SELECT 
		c.custid,
		c.companyname AS 'CustomerName', 
		MAX(o.OrderDate) AS 'LastOrderDate',
		DATEADD(day, AVG(DATEDIFF(day, o.OrderDate, o2.OrderDate)), MAX(o.OrderDate)) AS 'NextPredictedOrder'
	FROM 
		Sales.Orders o
		INNER JOIN Sales.Customers c ON c.custid = o.custid
		OUTER APPLY (
			SELECT TOP 1 OrderDate
			FROM Sales.Orders
			WHERE custid = o.custid AND OrderDate > o.OrderDate
			ORDER BY OrderDate ASC
		) o2
	GROUP BY c.custid,c.companyname

END
GO
/****** Object:  StoredProcedure [dbo].[GetEmployees]    Script Date: 07/04/2023 18:23:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetEmployees]
	

AS


BEGIN
	SET NOCOUNT ON;

	SELECT Empid, CONCAT(firstname, ' ', lastname) as FullName
	FROM HR.Employees

END
GO
/****** Object:  StoredProcedure [dbo].[GetOrdersByIdClient]    Script Date: 07/04/2023 18:23:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetOrdersByIdClient]
 @custid INT = NULL
AS


BEGIN
	SET NOCOUNT ON;
SELECT OrderID, RequiredDate, ShippedDate, ShipName, ShipAddress, ShipCity, c.CompanyName
	FROM Sales.Orders INNER JOIN Sales.Customers AS c ON c.custid = Orders.custid
	WHERE ((Orders.custid= @custid) OR ( @custid IS NULL ))

END
GO
/****** Object:  StoredProcedure [dbo].[GetProducts]    Script Date: 07/04/2023 18:23:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetProducts]
	

AS


BEGIN
	SET NOCOUNT ON;

	SELECT ProductId, ProductName 
	FROM Production.Products 

END
GO
/****** Object:  StoredProcedure [dbo].[GetShippers]    Script Date: 07/04/2023 18:23:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetShippers]
	

AS


BEGIN
	SET NOCOUNT ON;

	SELECT ShipperID, CompanyName
	FROM Sales.Shippers

END
GO
