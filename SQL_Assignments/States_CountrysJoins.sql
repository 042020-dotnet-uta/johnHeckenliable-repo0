

USE JoinTestDb;

--Cross Join
select * from Countrys
cross join States;

--Inner Join
select * from States as s
inner join Countrys as c
on s.CountryId = c.CountryId;

--Left Outer Join
select * from States as s
left outer join Countrys as c
on s.CountryId = c.CountryId;  

--Right Outer Join
select * from States as s
right outer join Countrys as c
on s.CountryId = c.CountryId

--Full Outer Join
select * from States as s
full outer join Countrys as c
on s.CountryId = c.CountryId