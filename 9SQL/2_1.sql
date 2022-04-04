--1
SELECT SUM(UnitPrice * Quantity - Discount) AS Totals
FROM [Order Details]

--2
SELECT (COUNT(*) - COUNT(ShippedDate)) AS UndeliveredOrders
FROM Orders

--3
SELECT COUNT (DISTINCT CustomerID) AS CustomerID
FROM Orders
