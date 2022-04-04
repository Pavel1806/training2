--1
SELECT DISTINCT CONCAT(FirstName,' ',LastName), Region.RegionDescription
FROM EmployeeTerritories 
JOIN Employees ON EmployeeTerritories.EmployeeID = Employees.EmployeeID
JOIN Territories ON EmployeeTerritories.TerritoryID = Territories.TerritoryID
JOIN Region ON Territories.RegionID = Region.RegionID
WHERE Region.RegionDescription = 'Western'

--2
SELECT Customers.CompanyName, COUNT(Orders.CustomerID) AS NumberOrders
FROM Orders RIGHT JOIN Customers ON Orders.CustomerID = Customers.CustomerID
GROUP BY Customers.CompanyName
ORDER BY NumberOrders