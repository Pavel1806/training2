USE Northwind_Extended_v1_1
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmployeeCreditCards]') AND type in (N'U'))

BEGIN
CREATE TABLE EmployeeCreditCards
(
  Id INT PRIMARY KEY IDENTITY,
  NumberCard INT,
  ExpirationDate DATETIME,
  OwnersName VARCHAR(10),
  EmployeeId INT REFERENCES Employees(EmployeeID)
)


INSERT EmployeeCreditCards VALUES ('12345678','01/02/2025',(SELECT FirstName FROM Employees WHERE EmployeeID = 1 ), 1)
INSERT EmployeeCreditCards VALUES ('12345679','01/02/2024',(SELECT FirstName FROM Employees WHERE EmployeeID = 2 ), 2)
INSERT EmployeeCreditCards VALUES ('12345688','05/02/2025',(SELECT FirstName FROM Employees WHERE EmployeeID = 3 ), 3)
INSERT EmployeeCreditCards VALUES ('12345689','07/02/2024',(SELECT FirstName FROM Employees WHERE EmployeeID = 4 ), 4)
INSERT EmployeeCreditCards VALUES ('12345699','01/03/2025',(SELECT FirstName FROM Employees WHERE EmployeeID = 5 ), 5)
INSERT EmployeeCreditCards VALUES ('11345679','01/04/2024',(SELECT FirstName FROM Employees WHERE EmployeeID = 6 ), 6)
INSERT EmployeeCreditCards VALUES ('11345688','01/05/2025',(SELECT FirstName FROM Employees WHERE EmployeeID = 7 ), 7)
INSERT EmployeeCreditCards VALUES ('11345689','01/06/2024',(SELECT FirstName FROM Employees WHERE EmployeeID = 8 ), 8)
INSERT EmployeeCreditCards VALUES ('11345680','01/06/2026',(SELECT FirstName FROM Employees WHERE EmployeeID = 9 ), 9)
END
