USE Northwind_Extended_v1_3

EXEC sp_rename 'Region', 'Regions';

ALTER TABLE Customers
ADD DateFoundation DATETIME NULL