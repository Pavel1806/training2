SELECT OrderID, CAST (ShippedDate AS nvarchar) , ShipVia
FROM Orders
WHERE ShippedDate >= '06-05-1998' AND ShipVia >= 2


SELECT OrderID, CASE WHEN ShippedDate IS NULL THEN 'Not Shipped' END ShippedDate
FROM Orders
WHERE ShippedDate IS NULL


SELECT OrderID AS 'ORDER ID' , 
CASE 
WHEN ShippedDate IS NULL THEN 'Not Shipped' ELSE CONVERT(nvarchar, ShippedDate, 0) END 'Shipped Date'
FROM Orders 
WHERE ShippedDate > '01-05-1998' OR ShippedDate IS NULL



