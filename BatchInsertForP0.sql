
--CLEAR THE OLD DATA
DELETE FROM StoreInventories;
DELETE FROM OrderDetails;
DELETE FROM Orders;
DELETE FROM Products;
DELETE FROM Customers;
DELETE FROM Stores;

--INSERT NEW TEST DATA INTO STORES
insert into Stores
values(1, 'Seattle');
insert into Stores
values(2, 'Bellevue');
insert into Stores
values(3, 'Renton');

--INSERT NEW TEST DATA INTO CUSTOMERS
insert into Customers
VALUES(1, 'John', 'Doe', 'JohnDoe@email.com');
insert into Customers
VALUES(2, 'Jane', 'Doe', 'JaneDoe@email.com');
insert into Customers
VALUES(3, 'Don', 'Johnson', 'DonJon@email.com');
insert into Customers
VALUES(4, 'April', 'Showers', 'a_showers@email.com');
insert into Customers
VALUES(5, 'David', 'Smith', 'DaveSmith@email.com');

--INSERT NEW TEST DATA INTO PRODUCTS
INSERT INTO Products
VALUES(1, 'Product 1', 12.99);
INSERT INTO Products
VALUES(2, 'Product 2', 10.99);
INSERT INTO Products
VALUES(3, 'Product 3', 22.99);
INSERT INTO Products
VALUES(4, 'Product 4', 18.99);
INSERT INTO Products
VALUES(5, 'Product 5', 5.99);

--INSERT NEW TEST DATA INTO STOREINVENTORIES
INSERT INTO StoreInventories
VALUES(1, 1, 200);
INSERT INTO StoreInventories
VALUES(1, 2, 200);
INSERT INTO StoreInventories
VALUES(1, 3, 200);
INSERT INTO StoreInventories
VALUES(1, 4, 200);
INSERT INTO StoreInventories
VALUES(1, 5, 200);

--INSERT INTO StoreInventories
--VALUES(2, 1, 200);
INSERT INTO StoreInventories
VALUES(2, 2, 200);
INSERT INTO StoreInventories
VALUES(2, 3, 200);
INSERT INTO StoreInventories
VALUES(2, 4, 200);
INSERT INTO StoreInventories
VALUES(2, 5, 200);
INSERT INTO StoreInventories
VALUES(3, 1, 200);

--INSERT INTO StoreInventories
--VALUES(3, 2, 200);
INSERT INTO StoreInventories
VALUES(3, 3, 200);
INSERT INTO StoreInventories
VALUES(3, 4, 200);
INSERT INTO StoreInventories
VALUES(3, 5, 200);
