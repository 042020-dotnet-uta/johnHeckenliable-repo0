-- basic exercises (Chinook database)

-- 1. list all customers (full names, customer ID, and country) who are not in the US
select CONCAT(FirstName, ' ', LastName) as 'Full Name', customerID, Country 
from Customer
where Country <> 'USA';

-- 2. list all customers from brazil
select * 
from Customer
where Country = 'Brazil';

-- 3. list all sales agents
select * 
from Employee
where Title LIKE 'Sales%';

-- 4. show a list of all countries in billing addresses on invoices.
select distinct BillingCountry 
from Invoice;

-- 5. how many invoices were there in 2009, and what was the sales total for that year?
select COUNT(InvoiceId) as 'Invoice Totals', SUM(Total) as 'Total Sales' 
from Invoice
where YEAR(InvoiceDate) = 2009;

-- 6. how many line items were there for invoice #37?
select count(InvoiceLineId)
from dbo.InvoiceLine
where InvoiceId = 37;

-- 7. how many invoices per country?
select BillingCountry, COUNT(InvoiceId) 
from Invoice
Group By BillingCountry;

-- 8. show total sales per country, ordered by highest sales first.
select BillingCountry, SUM(Total) as 'Total Sales' 
from Invoice
Group By BillingCountry
Order By [Total Sales] DESC;