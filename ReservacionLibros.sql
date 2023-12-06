CREATE DATABASE BooksReservation
GO

USE BooksReservation
GO

CREATE TABLE TUsers(
	IdUser INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	VarFirstName VARCHAR(50) NOT NULL,
	VarLastName VARCHAR(50) NOT NULL,
	VarEmail VARCHAR(100) NOT NULL,
	VarPassword VARCHAR(200) NOT NULL,
	IntStatus INT DEFAULT 1,
	DtimeCreatedAt DATETIME NOT NULL DEFAULT SYSDATETIME(),
	DtimeUpdatedAt DATETIME NOT NULL DEFAULT SYSDATETIME(),
	BitIsDeleted BIT DEFAULT 0
)
GO

CREATE TABLE TBooks(
	IdBook INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	VarTitle VARCHAR(150) NOT NULL,
	VarCode VARCHAR(100) NOT NULL,
	IntStatus INT DEFAULT 1,
	DtimeCreatedAt DATETIME NOT NULL DEFAULT SYSDATETIME(),
	DtimeUpdatedAt DATETIME NOT NULL DEFAULT SYSDATETIME(),
	BitIsDeleted BIT DEFAULT 0
)
GO

CREATE TABLE TReservations(
	IdResevation INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	IdUser INT NOT NULL,
	IdBook INT NOT NULL,
	DtimeDateReservation DATETIME DEFAULT SYSDATETIME(),
	IntStatus INT DEFAULT 1,
	DtimeCreatedAt DATETIME NOT NULL DEFAULT SYSDATETIME(),
	DtimeUpdatedAt DATETIME NOT NULL DEFAULT SYSDATETIME(),
	BitIsDeleted BIT DEFAULT 0
)
GO

-- (INICIO) Add FK in Reservation Table
ALTER TABLE TReservations
	ADD CONSTRAINT FK_TReservations_TUsers FOREIGN KEY (IdUser)
	REFERENCES TUsers(IdUser)
	ON DELETE CASCADE
	ON UPDATE CASCADE

ALTER TABLE TReservations
	ADD CONSTRAINT FK_TReservations_TBooks FOREIGN KEY (IdBook)
	REFERENCES TBooks(IdBook)
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
	SET DtimeUpdatedAt = SYSDATETIME()
	FROM TUsers inner join inserted ON TUsers.IdUser = inserted.IdUser
END
GO


CREATE TRIGGER TRG_Update_TBooks 
ON TBooks
FOR UPDATE
AS
BEGIN
	UPDATE TBooks
	SET DtimeUpdatedAt = SYSDATETIME()
	FROM TBooks inner join inserted ON TBooks.IdBook = inserted.IdBook
END
GO

CREATE TRIGGER TRG_Update_TReservations
ON TReservations
FOR UPDATE
AS
BEGIN
	UPDATE TReservations
	SET DtimeUpdatedAt = SYSDATETIME()
	FROM TReservations inner join inserted ON TReservations.IdResevation = inserted.IdResevation
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
	TBooks.IdBook, 
	VarTitle, 
	VarCode, 
	IntStatus, 
	DtimeDateReservation,
	DtimeCreatedAt,
	CASE WHEN SubTReservations.IdBook IS NOT NULL THEN 1 ELSE 0 END AS IsReserved
	FROM TBooks left join (
		SELECT IdBook, MAX(DtimeDateReservation) as DtimeDateReservation FROM TReservations
		WHERE IntStatus = 1
		AND  BitIsDeleted = 0
		GROUP BY IdBook
	) as SubTReservations on TBooks.IdBook = SubTReservations.IdBook
	WHERE BitIsDeleted = 0
GO

-- BUSCAR LIBROS
CREATE PROCEDURE SP_GetSearch_Books (
	@VarSearch VARCHAR(100)
)
AS
	SELECT 
	TBooks.IdBook, 
	VarTitle, 
	VarCode, 
	IntStatus, 
	DtimeDateReservation,
	DtimeCreatedAt,
	CASE WHEN SubTReservations.IdBook IS NOT NULL THEN 1 ELSE 0 END AS IsReserved
	FROM TBooks left join (
		SELECT IdBook, MAX(DtimeDateReservation) as DtimeDateReservation FROM TReservations
		WHERE IntStatus = 1
		AND  BitIsDeleted = 0
		GROUP BY IdBook
	) as SubTReservations on TBooks.IdBook = SubTReservations.IdBook
	WHERE BitIsDeleted = 0
	AND (VarTitle LIKE '%' + @VarSearch + '%'
	OR VarCode LIKE '%' + @VarSearch + '%')
GO

-- -- OBTENER LIBRO POR ID	
CREATE PROCEDURE SP_GetById_Books (
	@IntIdBook INT
)
AS
	SELECT 
	TBooks.IdBook, 
	VarTitle, 
	VarCode, 
	IntStatus, 
	DtimeDateReservation,
	DtimeCreatedAt,
	CASE WHEN SubTReservations.IdBook IS NOT NULL THEN 1 ELSE 0 END AS IsReserved
	FROM TBooks left join (
		SELECT IdBook, MAX(DtimeDateReservation) as DtimeDateReservation FROM TReservations
		WHERE IntStatus = 1
		AND  BitIsDeleted = 0
		GROUP BY IdBook
	) as SubTReservations on TBooks.IdBook = SubTReservations.IdBook
	WHERE TBooks.IdBook = @IntIdBook
	AND BitIsDeleted = 0
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