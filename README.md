
CREATE TABLE Employees (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100),
    Gender NVARCHAR(10),
    IsActive BIT,
    Department NVARCHAR(50)
);

