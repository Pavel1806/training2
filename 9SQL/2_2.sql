--1
SELECT YEAR(OrderDate) AS Year, COUNT(ShipVia) AS Total
FROM Orders
GROUP BY YEAR(OrderDate)

-- ïðîâåðêà
SELECT COUNT(ShipVia) AS Total
FROM Orders

--2
SELECT (SELECT CONCAT(FirstName,' ',LastName) FROM Employees WHERE Employees.EmployeeID = Orders.EmployeeID) AS Seller, COUNT(EmployeeID) AS Amount
FROM Orders
GROUP BY EmployeeID

--3
SELECT EmployeeID, CustomerID, COUNT(CustomerID) AS TOTAL
FROM Orders
WHERE YEAR(OrderDate) = 1998
GROUP BY EmployeeID, CustomerID
ORDER BY EmployeeID

--4
SELECT CompanyName AS Buyer, CONCAT(FirstName,' ',LastName) AS Seller, Customers.City
FROM Customers, Employees 
WHERE Customers.City = Employees.City

--5 ÍÅÄÎÄÅËÀÍÎ
SELECT City, COUNT(City)
FROM Customers
GROUP BY City
HAVING COUNT(City) > 1