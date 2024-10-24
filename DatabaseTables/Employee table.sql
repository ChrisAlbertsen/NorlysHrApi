CREATE TABLE employeeMgmt.Employee (
	employeeId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	FirstName VARCHAR(70) NOT NULL,
	LastName VARCHAR(50) NOT NULL CHECK (LastName NOT LIKE '% %'),
	BirthDate DATE NOT NULL CHECK (BirthDate > '1900-01-01' AND BirthDate < GETDATE()),
	FkOfficeId VARCHAR(100) NOT NULL FOREIGN KEY REFERENCES employeeMgmt.Office(officeId),

	CONSTRAINT CHK_occupancy_limit CHECK 
		(employeeMgmt.getOfficeEmployeeCount(FkOfficeId) <= employeeMgmt.getOccupanyLimit(FkOfficeId))
	);