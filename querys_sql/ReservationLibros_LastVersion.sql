CREATE DATABASE BooksReservationNew
GO

USE BooksReservationNew
GO

CREATE TABLE TUsers(
	idUser INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	varFirstName VARCHAR(50) NOT NULL,
	varLastName VARCHAR(50) NOT NULL,
	varEmail VARCHAR(100) NOT NULL,
	varPassword VARCHAR(200) NOT NULL,
	intStatus INT DEFAULT 1,
	dtimeCreatedAt DATETIME NOT NULL DEFAULT SYSDATETIME(),
	dtimeUpdatedAt DATETIME NOT NULL DEFAULT SYSDATETIME(),
	bitIsDeleted BIT DEFAULT 0
)
GO

CREATE TABLE TBooks(
	idBook INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	varTitle VARCHAR(150) NOT NULL,
	varCode VARCHAR(100) NOT NULL,
	intStatus INT DEFAULT 1, -- se puede tomar para una tabla de estados (disponible,reservado,dañado, perdido)
	bitIsAvailable BIT DEFAULT 1, -- saber si esta disponible(available) sin hacer join a otra tabla
	dtimeCreatedAt DATETIME NOT NULL DEFAULT SYSDATETIME(),
	dtimeUpdatedAt DATETIME NOT NULL DEFAULT SYSDATETIME(),
	bitIsDeleted BIT DEFAULT 0
)
GO

CREATE TABLE TReservations(
	idResevation INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	idUser INT NOT NULL,
	idBook INT NOT NULL,
	varUserName VARCHAR(100) NOT NULL, -- nombre del usuario sin hacer join
	varBookName VARCHAR(150) NOT NULL, -- nombre del libro sin hacer join
	dtimeDateReservation DATETIME DEFAULT SYSDATETIME(),
	intStatus INT DEFAULT 1,
	dtimeCreatedAt DATETIME NOT NULL DEFAULT SYSDATETIME(),
	dtimeUpdatedAt DATETIME NOT NULL DEFAULT SYSDATETIME(),
	bitIsDeleted BIT DEFAULT 0
)
GO

-- (INICIO) Add FK in Reservation Table
ALTER TABLE TReservations
	ADD CONSTRAINT FK_TReservations_TUsers FOREIGN KEY (idUser)
	REFERENCES TUsers(idUser)
	ON DELETE CASCADE
	ON UPDATE CASCADE

ALTER TABLE TReservations
	ADD CONSTRAINT FK_TReservations_TBooks FOREIGN KEY (idBook)
	REFERENCES TBooks(idBook)
	ON DELETE CASCADE
	ON UPDATE CASCADE
-- (FIN) Add FK in Reservation Table
GO

CREATE TRIGGER TRG_Update_TUsers 
ON TUsers
FOR UPDATE
AS
BEGIN
	UPDATE TUsers
	SET dtimeUpdatedAt = SYSDATETIME()
	FROM TUsers inner join inserted ON TUsers.idUser = inserted.idUser
END
GO


CREATE TRIGGER TRG_Update_TBooks 
ON TBooks
FOR UPDATE
AS
BEGIN
	UPDATE TBooks
	SET dtimeUpdatedAt = SYSDATETIME()
	FROM TBooks inner join inserted ON TBooks.idBook = inserted.idBook
END
GO

CREATE TRIGGER TRG_Update_TReservations
ON TReservations
FOR UPDATE
AS
BEGIN
	UPDATE TReservations
	SET dtimeUpdatedAt = SYSDATETIME()
	FROM TReservations inner join inserted ON TReservations.idResevation = inserted.idResevation
END
GO

INSERT INTO TBooks(VarTitle, VarCode, IntStatus) 
VALUES
('Libro de prueba 1', 'LIBPRUE-1', 1),
('Libro de prueba 2', 'LIBPRUE-2', 1),
('Libro de prueba 3', 'LIBPRUE-3', 1),
('Libro de prueba 4', 'LIBPRUE-4', 1)
GO

-- (INICIO) CREAR STORED PROCEDURE
-- -- OBTENER LOS LIBROS	
CREATE PROCEDURE SP_GetAll_Books
AS
	SELECT 
	TBooks.idBook, 
	varTitle, 
	varCode, 
	TBooks.intStatus, 
	bitIsAvailable,
	dtimeDateReservation,
	TBooks.dtimeCreatedAt
	FROM TBooks 
	left join TReservations on TBooks.idBook = TReservations.idBook
	WHERE TBooks.BitIsDeleted = 0
GO

-- BUSCAR LIBROS
CREATE PROCEDURE SP_GetSearch_Books (
	@VarSearch VARCHAR(100)
)
AS
	SELECT 
	TBooks.idBook, 
	varTitle, 
	varCode, 
	TBooks.intStatus, 
	bitIsAvailable,
	dtimeDateReservation,
	TBooks.dtimeCreatedAt
	FROM TBooks 
	left join TReservations on TBooks.idBook = TReservations.idBook
	WHERE TBooks.bitIsDeleted = 0
	AND (varTitle LIKE '%' + @VarSearch + '%'
	OR varCode LIKE '%' + @VarSearch + '%')
GO

-- -- OBTENER LIBRO POR ID	
CREATE PROCEDURE SP_GetById_Books (
	@IntIdBook INT
)
AS
	SELECT 
	TBooks.idBook, 
	varTitle, 
	varCode, 
	TBooks.intStatus, 
	bitIsAvailable,
	dtimeDateReservation,
	TBooks.dtimeCreatedAt
	FROM TBooks 
	left join TReservations on TBooks.idBook = TReservations.idBook
	WHERE TBooks.IdBook = @IntIdBook
	AND TBooks.BitIsDeleted = 0
GO

-- (FIN) CREAR STORED PROCEDURE

EXECUTE SP_GetAll_Books
GO

EXECUTE SP_GetSearch_Books @VarSearch = 'prueba 2'
GO

EXECUTE SP_GetById_Books @IntIdBook = 4
GO

SELECT * FROM TBooks
GO