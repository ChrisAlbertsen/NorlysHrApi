CREATE FUNCTION employeeMgmt.getOfficeEmployeeCount
(
    @office_id VARCHAR(100)
)
RETURNS INT
AS
BEGIN
    DECLARE @employee_count INT

    SELECT @employee_count = COUNT(*)
	FROM employeeMgmt.Employee
	WHERE FkOfficeId = @office_id;

    RETURN @employee_count;
END;

CREATE FUNCTION employeeMgmt.getOccupanyLimit
(
	@office_id VARCHAR(100)
)
RETURNS INT
AS
BEGIN
	DECLARE @office_limit INT

	SELECT @office_limit = OccupancyLimit
	FROM employeeMgmt.Office
	WHERE OfficeId = @office_id

	RETURN @office_limit
END