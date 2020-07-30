CREATE TABLE [dbo].[Employees]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[EmployeeId] INT NOT NULL,
	[EmployeeName] NCHAR(90) NOT NULL,
	[EmployeeType] NCHAR(40) NOT NULL,
	RatePerHour FLOAT NOT NULL,
)
