CREATE TABLE [dbo].[EmployeeCreditCards]
(
	[Id] INT NOT NULL PRIMARY KEY,
	NumberCard INT,
	ExpirationDate DATETIME,
	OwnersName VARCHAR(30),
	EmployeeId INT REFERENCES Employees(EmployeeID) 
)
