USE MinDatabase;


CREATE TABLE Caliper (
    CaliberID INT IDENTITY(1,1) NOT NULL,
    ItemNumber INT NULL,
    CaliperType NVARCHAR(50) NULL,
    CONSTRAINT PK_Caliper PRIMARY KEY (CaliberID)
);


CREATE TABLE Employee(
    EmployeeID INT IDENTITY(1,1) NOT NULL,
    CONSTRAINT PK_Employee PRIMARY KEY (EmployeeID)
);


CREATE TABLE FinalControl (
    FinalControlID INT IDENTITY(1,1) NOT NULL,
    CaliberID INT NOT NULL,
    EmployeeID INT NOT NULL,
    [Date] DATETIME2 NULL,
    Result BIT NULL,
    Comment NVARCHAR(255) NULL,
    Waste INT NULL,
    Export INT NULL,
    CONSTRAINT PK_FinalControl PRIMARY KEY (FinalControlID),
    CONSTRAINT FK_FinalControl_Caliper
        FOREIGN KEY (CaliberID) REFERENCES Caliper(CaliberID),
    CONSTRAINT FK_FinalControl_Employee
        FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID)
);


CREATE TABLE StartControl (
    StartControlID INT IDENTITY(1,1) NOT NULL,
    CaliberID INT NOT NULL,
    EmployeeID INT NOT NULL,
    [Date] DATETIME2 NULL,
    CONSTRAINT PK_StartControl PRIMARY KEY (StartControlID),
    CONSTRAINT FK_StartControl_Caliper
        FOREIGN KEY (CaliberID) REFERENCES Caliper(CaliberID),
    CONSTRAINT FK_StartControl_Employee
        FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID)
);