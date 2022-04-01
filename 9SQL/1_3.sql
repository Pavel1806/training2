SELECT DISTINCT OrderID
FROM DBO.[Order Details]
WHERE Quantity BETWEEN  3 AND 10


SELECT CustomerID, Country
FROM Customers
WHERE Country BETWEEN 'b' AND 'h'
ORDER BY Country



SELECT CustomerID, Country
FROM Customers
WHERE Country > 'b' AND Country < 'h'
ORDER BY Country


SELECT ProductName
FROM Products
WHERE ProductName LIKE 'cho_olade'