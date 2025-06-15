
CREATE TABLE Employees (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100),
    Gender NVARCHAR(10),
    IsActive BIT,
    Department NVARCHAR(50)
);

🪜 Step-by-Step:

1. Create ASP.NET Web Forms Project

1. Open Visual Studio


2. Click on Create a new project


3. Search for ASP.NET Web Application (.NET Framework) → Select it → Click Next


4. Project name: WebFormsCRUD → Location: Any → Click Create


5. In the template window:

Choose Web Forms

Uncheck Host in the cloud

Click Create
