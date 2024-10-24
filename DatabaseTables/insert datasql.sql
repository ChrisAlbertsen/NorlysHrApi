INSERT INTO employeeMgmt.Office(OfficeId, OccupancyLimit)
VALUES 
('Aalborg',2),
('Silkeborg',3),
('Esbjerg',4)


SELECT *
FROM employeeMgmt.Office


INSERT INTO employeeMgmt.Employee(FirstName, LastName, BirthDate, FkOfficeId)
VALUES
('Frodo', 'Baggins', '1905-01-01','Aalborg'),
('Samwise', 'Gamgee', '1960-01-01', 'Silkeborg'),
('Samwise', 'Gamgee', '1960-01-01', 'Esbjerg'),
('Samwise', 'Gamgee', '1960-01-01', 'Esbjerg'),
('Samwise', 'Gamgee', '1960-01-01', 'Esbjerg')


SELECT * 
FROM employeeMgmt.Employee