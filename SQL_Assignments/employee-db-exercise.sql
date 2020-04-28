--Create the Employee Database
create database EmployeeDB;

--USE the Employee Database
USE EmployeeDB;
--Create the Department Table
Create Table Department (
	DeptId int NOT NULL PRIMARY KEY IDENTITY(0, 1),
	Name NVARCHAR (50),
	Location Nvarchar (50));
--Create the Employee Table
Create Table Employee(
	EmployeeId int NOT NULL PRIMARY KEY IDENTITY(0, 1),
	FirstName NVARCHAR (50),
	LastName Nvarchar (50),
	SSN CHAR(12),
	DeptId int NOT NULL FOREIGN KEY REFERENCES Department(DeptId));
--Create the EmployeeDetails Table
Create Table EmployeeDetails(
	DetailsId int NOT NULL PRIMARY KEY IDENTITY(0, 1),
	EmployeeId int NOT NULL FOREIGN KEY REFERENCES Employee (EmployeeId),
	Salary DECIMAL(16,4),
	Address1 Nvarchar (50),
	Address2 Nvarchar (50),
	City Nvarchar (50),
	State char (2),
	Country NVARCHAR(25));
--Insert data into the Department Table
INSERT INTO Department
VALUES ('Marketing', 'Tacoma');
INSERT INTO Department
VALUES ('Sales', 'Seattle');
INSERT INTO Department
VALUES ('Development', 'Bellevue');
--Insert Data into the Employee Table
INSERT INTO Employee
VALUES('Susan', 'Johnson', '555-555-1234', 
(SELECT DeptId from Department where Name = 'Marketing'));
INSERT INTO Employee
VALUES('Dan', 'Jackson', '555-555-6789', 
(SELECT DeptId from Department where Name = 'Sales'));
INSERT INTO Employee
VALUES('Tom', 'Jones', '555-555-3456', 
(SELECT DeptId from Department where Name = 'Development'));
--Insert Data into the EmployeeDetails Table
INSERT INTO EmployeeDetails
VALUES(0, 50000.00, '','', 'Seattle', 'WA', 'USA' );
INSERT INTO EmployeeDetails
VALUES(1, 65000.00,'', '', 'Kenewick', 'WA', 'USA' );
INSERT INTO EmployeeDetails
VALUES(2, 44000.00, '', '', 'Olympia', 'WA', 'USA' );
--Insert Tina Smith into the Employee table
INSERT INTO Employee
VALUES('Tina', 'Smith', '555-555-9876', 
(SELECT DeptId from Department where Name = 'Marketing'));
--Insert the Details for Tina Smith
INSERT INTO EmployeeDetails (EmployeeId, Salary, City, State, Country)
VALUES(3, 50000.00, 'Seattle', 'WA', 'USA');

--List all employees in Marketing
SELECT * 
from Employee
WHERE DeptId = (SELECT DeptId from Department WHERE Name = 'Marketing');
--Report total salaries in Marketing
SELECT SUM(Salary) as 'Marketing Salary Totals' 
From EmployeeDetails 
Join Employee On Employee.EmployeeId = EmployeeDetails.EmployeeId 
Join Department on Employee.DeptID = Department.DeptId 
Where Department.Name = 'Marketing';
--Report total employees by department
SELECT Department.Name as 'Department', COUNT(Employee.EmployeeId) as 'Employee Count'
FROM Employee
JOIN Department on Department.DeptId = Employee.DeptId
Group By (Department.Name);
--update salary for Tina Smith to 90000.00
UPDATE EmployeeDetails
SET Salary = 90000.00
Where EmployeeId = 
(SELECT EmployeeId from Employee WHERE FirstName = 'Tina' and LastName = 'Smith');