
USE JoinTestDb;

Create Table Countrys(
	CountryId int NOT NULL PRIMARY KEY IDENTITY(0, 1),
	CountryName Nvarchar (50));

Create Table States(
	StateId int NOT NULL PRIMARY KEY IDENTITY(0, 1),
	StateName NVARCHAR(50),
	CountryId int FOREIGN KEY REFERENCES Countrys (CountryId));

insert into Countrys
	values
		('USA'),
		('Germany'),
		('Mexico'),
		('Australia'),
		('Canada'),
		('UK');

INSERT INTO States
VALUES
	('Utah',
		(select CountryId from Countrys where CountryName = 'USA')),
	('Oregon',
		(select CountryId from Countrys where CountryName = 'USA')),
	('Florida',
		(select CountryId from Countrys where CountryName = 'USA'));
	
INSERT INTO States
VALUES
	('Chiapas',
		(select CountryId from Countrys where CountryName = 'Mexico')),
	('Durango',
		(select CountryId from Countrys where CountryName = 'Mexico')),
	('Oaxaca',
		(select CountryId from Countrys where CountryName = 'Mexico'));
		
INSERT INTO States
VALUES
	('Victoria',
		(select CountryId from Countrys where CountryName = 'Australia')),
	('Tasmania',
		(select CountryId from Countrys where CountryName = 'Australia')),
	('Queensland',
		(select CountryId from Countrys where CountryName = 'Australia'));
			
INSERT INTO States
VALUES
	('Bavaria',
		(select CountryId from Countrys where CountryName = 'Germany')),
	('Hamburg',
		(select CountryId from Countrys where CountryName = 'Germany')),
	('Hesse',
		(select CountryId from Countrys where CountryName = 'Germany'));

INSERT INTO States (StateName)
VALUES('Hunan');
INSERT INTO States (StateName)
VALUES('Qinghai');