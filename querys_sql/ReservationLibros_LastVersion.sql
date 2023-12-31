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
	intStatus INT DEFAULT 1, -- se puede tomar para una tabla de estados (disponible,reservado,da�ado, perdido)
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
	dtimeDateReservation DATETIME NOT NULL,
	dtimeDateReservationEnd DATETIME NOT NULL,
	intStatus INT DEFAULT 1, -- se puede tomar para una tabla de estados (activo, pendiente, cancelado)
	bitIsActive BIT DEFAULT 1, -- saber si esta activo
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
GO

ALTER TABLE TReservations
	ADD CONSTRAINT FK_TReservations_TBooks FOREIGN KEY (idBook)
	REFERENCES TBooks(idBook)
	ON DELETE CASCADE
	ON UPDATE CASCADE
GO
-- (FIN) Add FK in Reservation Table

CREATE TABLE TWaitReservations(
	idWaitReservation INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	idUser INT NOT NULL,
	idBook INT NOT NULL,
	varUserName VARCHAR(100) NOT NULL, -- nombre del usuario sin hacer join
	varBookName VARCHAR(150) NOT NULL, -- nombre del libro sin hacer join
	varPriority CHAR(2) NOT NULL,
	dtimeDateReservation DATETIME NOT NULL,
	dtimeDateReservationEnd DATETIME NOT NULL,
	intStatus INT DEFAULT 1, -- se puede tomar para una tabla de estados (activo, pendiente, cancelado)
	bitIsActive BIT DEFAULT 1, -- saber si esta activo
	dtimeCreatedAt DATETIME NOT NULL DEFAULT SYSDATETIME(),
	dtimeUpdatedAt DATETIME NOT NULL DEFAULT SYSDATETIME(),
	bitIsDeleted BIT DEFAULT 0
)
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

CREATE TRIGGER TRG_Update_TWaitReservations
ON TWaitReservations
FOR UPDATE
AS
BEGIN
	UPDATE TWaitReservations
	SET dtimeUpdatedAt = SYSDATETIME()
	FROM TWaitReservations inner join inserted ON TWaitReservations.idWaitReservation = inserted.idWaitReservation
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
CREATE PROCEDURE SP_GetAll_Books (
	@IdUser INT = 0
)
AS
	SELECT 
	TBooks.idBook, 
	varTitle, 
	varCode, 
	TBooks.intStatus, 
	bitIsAvailable,
	TReservations.dtimeDateReservation,
	CASE WHEN TReservations.idUser = @IdUser THEN 1 ELSE 0 END AS bitReservedByMe,
	CASE WHEN TWaitReservations.idUser = @IdUser THEN 1 ELSE 0 END AS bitWaitReservedByMe,
	TBooks.dtimeCreatedAt
	FROM TBooks 
	left join (
		SELECT * FROM TReservations
		WHERE TReservations.bitIsActive = 1
		AND TReservations.BitIsDeleted = 0
	) TReservations on TBooks.idBook = TReservations.idBook
	left join (
		SELECT idUser, idBook, dtimeDateReservationEnd FROM TWaitReservations
		WHERE TWaitReservations.bitIsActive = 1
		AND TWaitReservations.BitIsDeleted = 0
		AND TWaitReservations.idUser = @IdUser
	) TWaitReservations on TWaitReservations.idBook = TReservations.idBook
	WHERE TBooks.BitIsDeleted = 0
GO

-- BUSCAR LIBROS
CREATE PROCEDURE SP_GetSearch_Books (
	@VarSearch VARCHAR(100),
	@IdUser INT = 0
)
AS
	SELECT 
	TBooks.idBook, 
	varTitle, 
	varCode, 
	TBooks.intStatus, 
	bitIsAvailable,
	TReservations.dtimeDateReservation,
	CASE WHEN TReservations.idUser = @IdUser THEN 1 ELSE 0 END AS bitReservedByMe,
	CASE WHEN TWaitReservations.idUser = @IdUser THEN 1 ELSE 0 END AS bitWaitReservedByMe,
	TBooks.dtimeCreatedAt
	FROM TBooks 
	left join (
		SELECT * FROM TReservations
		WHERE TReservations.bitIsActive = 1
		AND TReservations.BitIsDeleted = 0
	) TReservations on TBooks.idBook = TReservations.idBook
	left join (
		SELECT idUser, idBook, dtimeDateReservationEnd FROM TWaitReservations
		WHERE TWaitReservations.bitIsActive = 1
		AND TWaitReservations.BitIsDeleted = 0
		AND TWaitReservations.idUser = @IdUser
	) TWaitReservations on TWaitReservations.idBook = TReservations.idBook
	WHERE TBooks.bitIsDeleted = 0
	AND (varTitle LIKE '%' + @VarSearch + '%'
	OR varCode LIKE '%' + @VarSearch + '%')
GO

-- -- OBTENER LIBRO POR ID	
CREATE PROCEDURE SP_GetById_Books (
	@IntIdBook INT,
	@IdUser INT = 0
)
AS
	SELECT 
	TBooks.idBook, 
	varTitle, 
	varCode, 
	TBooks.intStatus, 
	bitIsAvailable,
	TReservations.dtimeDateReservation,
	TReservations.dtimeDateReservation,
	CASE WHEN TReservations.idUser = @IdUser THEN 1 ELSE 0 END AS bitReservedByMe,
	CASE WHEN TWaitReservations.idUser = @IdUser THEN 1 ELSE 0 END AS bitWaitReservedByMe,
	TBooks.dtimeCreatedAt
	FROM TBooks 
	left join (
		SELECT * FROM TReservations
		WHERE TReservations.bitIsActive = 1
		AND TReservations.BitIsDeleted = 0
	) TReservations on TBooks.idBook = TReservations.idBook
	left join (
		SELECT idUser, idBook, dtimeDateReservationEnd FROM TWaitReservations
		WHERE TWaitReservations.bitIsActive = 1
		AND TWaitReservations.BitIsDeleted = 0
		AND TWaitReservations.idUser = @IdUser
	) TWaitReservations on TWaitReservations.idBook = TReservations.idBook
	WHERE TBooks.IdBook = @IntIdBook
	AND TBooks.BitIsDeleted = 0
GO

-- CRONJOB (liberacion de libros, reserva de forma autom�tica)
CREATE PROCEDURE SP_CronJob
AS
	DECLARE @todayDate DATE = GETDATE();
	DECLARE @countWait INT = 0;

	-- obtener cantidad de reservas en cola
	SELECT DISTINCT @countWait = COUNT(1) FROM TWaitReservations
	WHERE dtimeDateReservation = @todayDate

	-- verificar si hay o no reservas en cola
	IF @countWait > 0
		BEGIN
			-- pasar los datos de TWaitReservations a TReservations
			INSERT INTO TReservations
				(idUser, idBook, varUserName, varBookName, dtimeDateReservation, dtimeDateReservationEnd)
			SELECT 
			idUser,
			idBook,
			varUserName,
			varBookName,
			dtimeDateReservation,
			dtimeDateReservationEnd
			FROM
			TWaitReservations
			WHERE bitIsActive = 1
			AND dtimeDateReservation = @todayDate
			AND BitIsDeleted = 0

			-- en TReservations actualizar bitIsActive a false
			UPDATE TReservations
			SET bitIsActive = 0
			WHERE dtimeDateReservation < @todayDate
			AND bitIsActive = 1
			AND BitIsDeleted = 0

			-- en TWaitReservations actualizar varPriority
			UPDATE TWaitReservations
			SET varPriority = 
				CASE 
					WHEN varPriority = 'P2' THEN 'P1'
					WHEN varPriority = 'P3' THEN 'P2'
				END
			WHERE dtimeDateReservation > @todayDate
			AND bitIsActive = 1
			AND BitIsDeleted = 0

			-- en TWaitReservations actualizar bitIsActive a false (si era P1)
			UPDATE TWaitReservations
			SET bitIsActive = 0
			WHERE dtimeDateReservation <= @todayDate
			AND varPriority = 'P1'
			AND bitIsActive = 1
			AND BitIsDeleted = 0

			-- actualizar libros (habilitado)
			UPDATE TBooks
			SET bitIsAvailable = 1
			WHERE bitIsAvailable = 0
			AND BitIsDeleted = 0

			SELECT 'Se han actualizado los datos correctamente'  as message, 1 as status
		END
	ELSE
		BEGIN
			SELECT 'Los datos ya estan actualizados' as message, 0 as status
		END
GO
-- (FIN) CREAR STORED PROCEDURE

EXECUTE SP_GetAll_Books @IdUser = 1
GO

EXECUTE SP_GetSearch_Books @VarSearch = 'prueba', @IdUser = 2
GO

EXECUTE SP_GetById_Books @IntIdBook = 2, @IdUser = 1
GO

-- ejecutar cronjob
EXECUTE SP_CronJob
GO

SELECT * FROM TBooks
GO

