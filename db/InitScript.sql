

CREATE TABLE dbo.Users
(
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Name VARCHAR(MAX) NOT NULL,
    Email VARCHAR(MAX) NOT NULL,
    Password VARCHAR(MAX) NOT NULL,
    RoleId int
);

CREATE TABLE dbo.Authors
(
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Name VARCHAR(MAX),
    Gender VARCHAR(10),
    DateOfBirth DATE
)

ALTER TABLE dbo.Authors
ADD PRIMARY KEY(Id);

CREATE TABLE dbo.Books
(
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Title VARCHAR(MAX) NOT NULL,
    Description VARCHAR(MAX) DEFAULT '',
    AuthorId INT NOT NULL FOREIGN KEY REFERENCES Authors(Id),
    ISBN VARCHAR(MAX),
    GENRE VARCHAR(100),
);


CREATE TABLE dbo.UserRoles
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    RoleName VARCHAR(50) NOT NULL
)

ALTER TABLE dbo.Users 
ADD RoleId int FOREIGN KEY REFERENCES dbo.UserRoles(Id);

ALTER TABLE dbo.Users
DROP COLUMN ROLE;


INSERT INTO dbo.UserRoles(RoleName)
VALUES('Admin')

INSERT INTO dbo.UserRoles(RoleName)
VALUES('User')

INSERT INTO dbo.Users (Name,Email,Password,RoleId)
VALUES('John Doe', 'john.doe@email.com','Password1',1)

INSERT INTO dbo.Users (Name,Email,Password,RoleId)
VALUES('John Doe', 'jane.doe@email.com','Password2',2)


INSERT INTO dbo.Authors(Name, Gender, DateOfBirth)
VALUES('JRR Martin','Male','01-09-1948')
INSERT INTO dbo.Authors(Name, Gender, DateOfBirth)
VALUES('JRR Tolkein','Male','01-03-1892')
INSERT INTO dbo.Authors(Name, Gender, DateOfBirth)
VALUES('JK Rowling','Female','01-09-1976')

INSERT INTO dbo.Books (Title,[Description],AuthorId,ISBN,GENRE)
VALUES('Harry Potter','Book about adeventures of harry potter',3,'890ASASAS123','Fantasy');
INSERT INTO dbo.Books (Title,[Description],AuthorId,ISBN,GENRE)
VALUES('Lord of the rings','Book about lord of the rings',2,'ASELKASAS123','Fantasy');
INSERT INTO dbo.Books (Title,[Description],AuthorId,ISBN,GENRE)
VALUES('Song of Ice and Fire','Book about westrose',1,'PDEHKASAS123','Fantasy');