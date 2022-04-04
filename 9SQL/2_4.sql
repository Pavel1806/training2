--1
SELECT (SELECT CompanyName FROM Suppliers WHERE Products.SupplierID = Suppliers.SupplierID) AS CompanyName , ProductName ,UnitsInStock
FROM Products
WHERE UnitsInStock IN (0)


--2
SELECT (SELECT CONCAT(LastName,' ', FirstName) FROM Employees WHERE Employees.EmployeeID = Orders.EmployeeID) AS LastName, COUNT(EmployeeID) AS Quantity
FROM Orders
GROUP BY EmployeeID
HAVING COUNT(*) > 150

--3
SELECT CompanyName
FROM Customers
WHERE NOT EXISTS (SELECT CustomerID FROM Orders WHERE Customers.CustomerID = Orders.CustomerID)
