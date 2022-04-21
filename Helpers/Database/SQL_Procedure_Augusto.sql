USE master
GO
USE DB_INMOBILIARIA
GO

-- PROCEDURES DE LA TABLA TIPO --

CREATE PROC USP_CREAR_TIPO
(
@nombre VARCHAR(50),
@unidad VARCHAR(20)
)
AS
BEGIN
INSERT INTO TIPO (nombre, unidad)
VALUES (@nombre, @unidad)
END
GO

CREATE PROC USP_EDITAR_TIPO
(
@id_tipo int,
@nombre VARCHAR(50),
@unidad VARCHAR(20)
)
AS
BEGIN
UPDATE TIPO SET nombre = @nombre, unidad =@unidad 
WHERE id_tipo = @id_tipo
END
GO

CREATE PROC USP_ELIMINAR_TIPO
(
@id_tipo int
)
AS
BEGIN
DELETE FROM TIPO WHERE id_tipo = @id_tipo
END
GO


-- PROCEDURES DE LA TABLA PERFIL --

CREATE PROC USP_LIST_PERFIL
AS
BEGIN
SELECT * FROM PERFIL
END
GO

CREATE PROC USP_CREATE_PERFIL
(@nombres VARCHAR(50)
,@primer_apellido VARCHAR(30)
,@segundo_apellido VARCHAR(30)
,@fecha_nacimiento DATETIME
,@tipo_documento VARCHAR(1)
,@nro_documento VARCHAR(20)
,@genero VARCHAR(1)
,@nacionalidad VARCHAR(1)
,@direccion VARCHAR(200)
)
AS
BEGIN
INSERT INTO PERFIL (nombres,primer_apellido,segundo_apellido,fecha_nacimiento
,tipo_documento,nro_documento,genero,nacionalidad,direccion)
VALUES (@nombres,@primer_apellido,@segundo_apellido, @fecha_nacimiento
,@tipo_documento,@nro_documento,@genero,@nacionalidad,@direccion)
END
GO

CREATE PROC USP_UPDATE_PERFIL
(@id_perfil INT
,@nombres VARCHAR(50)
,@primer_apellido VARCHAR(30)
,@segundo_apellido VARCHAR(30)
,@fecha_nacimiento DATETIME
,@tipo_documento VARCHAR(1)
,@nro_documento VARCHAR(20)
,@genero VARCHAR(1)
,@nacionalidad VARCHAR(1)
,@direccion VARCHAR(200)
)
AS
BEGIN
UPDATE PERFIL SET nombres=@nombres, primer_apellido=@primer_apellido, segundo_apellido=@segundo_apellido,
fecha_nacimiento=@fecha_nacimiento, tipo_documento=@tipo_documento, nro_documento = @nro_documento,
genero = @genero, nacionalidad = @nacionalidad, direccion = @direccion
WHERE id_perfil = @id_perfil
END
GO

CREATE PROC USP_DELETE_PERFIL
(
@id_perfil INT
)
AS
BEGIN
DELETE FROM PERFIL WHERE id_perfil = @id_perfil
END
GO


CREATE PROCEDURE USP_LIST_USUARIO
@id_usuario int
AS
	if @id_usuario=0
		BEGIN
			SELECT US.id_usuario, US.username, US.fecha_registro, R.nombre AS 'nombre_rol',us.id_perfil,us.id_rol,us.id_estado , p.nombres+SPACE(1)+p.primer_apellido+SPACE(1)+p.segundo_apellido as 'nombre_perfil', es.nombre as 'nombre_estado' FROM USUARIO US 
			INNER JOIN ESTADO ES ON ES.id_estado = US.id_estado INNER JOIN ROL R ON R.id_rol = US.id_rol INNER JOIN PERFIL P ON P.id_perfil = US.id_perfil
		END
	else
		BEGIN
			SELECT US.id_usuario, US.username, US.fecha_registro, R.nombre AS 'nombre_rol',us.id_perfil,us.id_rol,us.id_estado , p.nombres+SPACE(1)+p.primer_apellido+SPACE(1)+p.segundo_apellido as 'nombre_perfil', es.nombre as 'nombre_estado' FROM USUARIO US 
			INNER JOIN ESTADO ES ON ES.id_estado = US.id_estado INNER JOIN ROL R ON R.id_rol = US.id_rol INNER JOIN PERFIL P ON P.id_perfil = US.id_perfil where US.id_usuario=@id_usuario
		END

GO

CREATE PROCEDURE USP_USUARIO_INSERTADO
@username varchar(30),
@clave varchar(50)
as
	if not exists(select * from USUARIO where username=@username)
		BEGIN
			if(select COUNT(id_usuario) from USUARIO)=0
				BEGIN
					insert into USUARIO(username,clave,id_rol,id_estado)
					values(@username,@clave,1,1)
				END
			else
				BEGIN
					insert into USUARIO(username,clave,id_rol,id_estado)
					values(@username,@clave,4,3)
					SELECT us.id_usuario, us.id_perfil FROM USUARIO us where us.id_perfil = SCOPE_IDENTITY()
				END
		 END
GO

CREATE PROC USP_LISTAR_PERFIL
@id_perfil int
AS
BEGIN
if @id_perfil=0
		BEGIN
			SELECT * FROM PERFIL 
		END
	else
		BEGIN
			SELECT * FROM PERFIL p where p.id_perfil=@id_perfil
		END

END
GO

CREATE PROCEDURE USP_PERFIL_REGISTER    
(@id_perfil INT
,@nombres VARCHAR(50)
,@primer_apellido VARCHAR(30)
,@segundo_apellido VARCHAR(30)
,@fecha_nacimiento DATETIME
,@tipo_documento VARCHAR(1)
,@nro_documento VARCHAR(20)
,@genero VARCHAR(1)
,@nacionalidad VARCHAR(1)
,@direccion VARCHAR(200))
AS    
   DECLARE @id int 
 if exists(select * from PERFIL where id_perfil=@id_perfil)    
  BEGIN    
   UPDATE PERFIL SET nombres=@nombres, primer_apellido=@primer_apellido, segundo_apellido=@segundo_apellido,
	fecha_nacimiento=@fecha_nacimiento, tipo_documento=@tipo_documento, nro_documento = @nro_documento,
	genero = @genero, nacionalidad = @nacionalidad, direccion = @direccion
	WHERE id_perfil = @id_perfil 
	SET @id = @id_perfil    
  END    
 ELSE    
  BEGIN    
  INSERT INTO PERFIL (nombres,primer_apellido,segundo_apellido,fecha_nacimiento
	,tipo_documento,nro_documento,genero,nacionalidad,direccion)
	VALUES (@nombres,@primer_apellido,@segundo_apellido, @fecha_nacimiento
	,@tipo_documento,@nro_documento,@genero,@nacionalidad,@direccion) 
   SET @id=SCOPE_IDENTITY()    
  END  
  SELECT @id
GO



CREATE PROCEDURE USP_PERFIL_EDIT_US  
(@id_perfil INT
,@nombres VARCHAR(50)
,@primer_apellido VARCHAR(30)
,@segundo_apellido VARCHAR(30)
,@fecha_nacimiento DATETIME
,@tipo_documento VARCHAR(1)
,@nro_documento VARCHAR(20)
,@genero VARCHAR(1)
,@nacionalidad VARCHAR(1)
,@direccion VARCHAR(200))
AS    
   DECLARE @id int 
 if not exists(select * from PERFIL where nro_documento = @nro_documento)  
  BEGIN
    UPDATE PERFIL SET nombres=@nombres, primer_apellido=@primer_apellido, segundo_apellido=@segundo_apellido,
	fecha_nacimiento=@fecha_nacimiento, tipo_documento=@tipo_documento, nro_documento = @nro_documento,
	genero = @genero, nacionalidad = @nacionalidad, direccion = @direccion
	WHERE id_perfil = @id_perfil 
	SET @id = @id_perfil    
  END
  ELSE
  BEGIN
	if exists (select * from PERFIL where nro_documento = @nro_documento and id_perfil = @id_perfil)
	BEGIN
    UPDATE PERFIL SET nombres=@nombres, primer_apellido=@primer_apellido, segundo_apellido=@segundo_apellido,
	fecha_nacimiento=@fecha_nacimiento, tipo_documento=@tipo_documento, nro_documento = @nro_documento,
	genero = @genero, nacionalidad = @nacionalidad, direccion = @direccion
	WHERE id_perfil = @id_perfil 
	SET @id = @id_perfil    
  END
  END
    SELECT @id
GO


CREATE PROCEDURE SP_USER_REGISTER    
@id_usuario int,    
@username varchar(50),
@clave varchar(50),
@id_rol int,
@id_estado int
AS    
   DECLARE @id int 
 if exists(select * from USUARIO where username=@username)    
  BEGIN    
   update USUARIO set username=@username,clave = @clave,id_rol= @id_rol,id_estado = @id_estado
       where id_usuario=@id_usuario    
	SET @id = @id_usuario    
  END    
 ELSE    
  BEGIN    
  insert into USUARIO(username,clave,id_rol,id_estado)VALUES(@username,@clave,4,3)    
   SET @id=SCOPE_IDENTITY()    
  END  
  SELECT @id
GO

CREATE PROC USP_EDIT_CONTRASE�A
@id_usuario int,
@clave varchar(50)
AS
BEGIN
DECLARE @id int
if exists(SELECT * FROM USUARIO US WHERE US.id_usuario = @id_usuario)
	BEGIN
	UPDATE USUARIO SET clave= @clave WHERE id_usuario = @id_usuario
	SET @id = @id_usuario
	END
END
SELECT @id
GO


CREATE PROC USP_EDIT_ESTADOUS
@id_usuario int,
@id_estado int
AS
BEGIN
DECLARE @id int
	if exists (SELECT * FROM USUARIO WHERE id_usuario = @id_usuario)
		BEGIN
		UPDATE USUARIO SET id_estado = @id_estado WHERE id_usuario = @id_usuario
		SET @id = @id_usuario
		END
END
 SELECT @id
GO