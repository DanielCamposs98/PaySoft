

GO
CREATE PROCEDURE SP_InsertarSalon
@Salon VARCHAR(30)
AS
DECLARE @Estado VARCHAR(30)
SET @Estado= 'ACTIVO'

IF EXISTS(SELECT Nombre FROM SALON WHERE Nombre= @Salon)
	RAISERROR('Ya existe un salon con este nombre, intenta de nuevo',16,1)
ELSE
	INSERT INTO SALON VALUES (@Salon,@Estado)


GO
CREATE PROC SP_InsertarMesa
@Nombre VARCHAR(30),
@Salon int
AS
DECLARE @Estado_Vida VARCHAR(30)
DECLARE @Estado_Disponibilidad VARCHAR(30)
SET @Estado_Vida='ACTIVO'
SET @Estado_Disponibilidad='LIBRE'

IF EXISTS(SELECT Nombre FROM MESA WHERE Nombre = @Nombre AND Nombre <>'NULO')
	RAISERROR('Ya existe una mesa con ese nombre, intenta de nuevo',16,1)
ELSE
	INSERT INTO MESA VALUES(@Nombre,@Salon,@Estado_Vida,@Estado_Disponibilidad)

GO
CREATE PROC SP_MostrarSalon
@Buscar VARCHAR(30)
AS
SELECT * FROM SALON WHERE Nombre LIKE '%' + @Buscar + '%'

GO
CREATE PROC SP_MostrarMesasSalon
@ID_Salon int
AS
SELECT M.* ,P.* FROM MESA M INNER JOIN SALON S ON S.ID_Salon = M.Salon
CROSS JOIN PROPIEDAD_MESA P
WHERE M.Salon = @ID_Salon

GO

CREATE PROC SP_MostrarIdSalonRegistrado
@salon as VARCHAR(30)
AS
SELECT ID_Salon FROM SALON WHERE Nombre= @salon