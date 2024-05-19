CREATE TABLE Users (
    Id INT PRIMARY KEY,
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    UserType NVARCHAR(50)
);

CREATE TABLE Catalog (
    Id INT PRIMARY KEY,
    RoomNumber INT,
    RoomType NVARCHAR(50),
    IsBooked BIT
);

CREATE TABLE State (
    Id INT PRIMARY KEY,
    RoomCatalogId INT,
    Price INT,
    FOREIGN KEY (RoomCatalogId) REFERENCES Catalog(Id)
);

CREATE TABLE Event (
    Id INT PRIMARY KEY,
    UserId INT,
    StateId INT,
    CheckInDate DATETIME,
    CheckOutDate DATETIME,
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    FOREIGN KEY (StateId) REFERENCES State(Id)
);