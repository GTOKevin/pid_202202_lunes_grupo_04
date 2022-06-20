USE master
GO
USE DB_INMOBILIARIA
GO

-- PROCEDURES DE LA TABLA TIPO --


ALTER TABLE PERFIL
ALTER COLUMN tipo_documento VARCHAR(5)
GO
ALTER TABLE PERFIL
ALTER COLUMN genero VARCHAR(5)
GO
ALTER TABLE PERFIL
ALTER COLUMN nacionalidad VARCHAR(5)
GO

-- PROCEDURES DE LA TABLA PERFIL --

CREATE PROC USP_LIST_PERFIL
AS
BEGIN
SELECT * FROM PERFIL
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
,@tipo_documento VARCHAR(5)
,@nro_documento VARCHAR(20)
,@genero VARCHAR(5)
,@nacionalidad VARCHAR(5)
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
,@tipo_documento VARCHAR(5)
,@nro_documento VARCHAR(20)
,@genero VARCHAR(5)
,@nacionalidad VARCHAR(5)
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

CREATE PROC USP_EDIT_CONTRASEÑA
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

CREATE TABLE PERFIL_FILE
(
id_file INT IDENTITY (1,1) PRIMARY KEY,
nombrefile VARCHAR(MAX),
id_perfil INT
FOREIGN KEY (id_perfil) REFERENCES PERFIL(id_perfil)
)
GO

CREATE PROCEDURE USP_FILE_PERFIL    
@id_perfil int,    
@nombrefile varchar(max)     
AS    
   DECLARE @id int 
 if exists(select * from PERFIL_FILE where id_perfil=@id_perfil)    
  BEGIN    
   update PERFIL_FILE set nombrefile=@nombrefile    
       where id_perfil=@id_perfil    
	SET @id = @id_perfil    
  END    
 ELSE    
  BEGIN    
  insert into PERFIL_FILE(nombrefile,id_perfil)VALUES(@nombrefile,@id_perfil)    
   SET @id=SCOPE_IDENTITY()    
  END  
  SELECT @id
GO

CREATE TRIGGER TRG_FILE_PERFIL
ON PERFIL
AFTER INSERT
AS
	declare	@id_perfil int
	set @id_perfil =(select id_perfil from inserted)
	BEGIN
		insert into PERFIL_FILE(nombrefile, id_perfil)
			values('/Assets/img/avatars/default.jpg', @id_perfil);

		SET @id_perfil = SCOPE_IDENTITY();

	END
GO

CREATE PROCEDURE USP_LISTAR_PERFILFILE
@id_file int
as
	if @id_file=0
		BEGIN
			select * from PERFIL_FILE
		END
	else
		BEGIN
			select * from PERFIL_FILE where id_file=@id_file
		END
go

CREATE PROCEDURE USP_LISTAR_USUARIO_PORPERFIL
@id_perfil int
as
	if @id_perfil = 0
		BEGIN
			select us.id_usuario, us.username,us.fecha_registro,us.id_rol,us.id_perfil,us.id_estado  from USUARIO us
		END
	else
		BEGIN
			select us.id_usuario, us.username,us.fecha_registro,us.id_rol,us.id_perfil,us.id_estado  from USUARIO us where id_perfil=@id_perfil
		END
go

CREATE PROC USP_LISTAR_TIPO_PORUNIDAD
@unidad varchar(50)
as
begin
	SELECT * FROM TIPO where unidad = @unidad
end
go

CREATE PROC USP_LISTA_MI_PERFIL
@id_perfil INT
AS
BEGIN
	SELECT P.id_perfil, P.nombres, P.primer_apellido,P.segundo_apellido, P.fecha_nacimiento,T.nombre as 'tipo_documento' , P.nro_documento, T1.nombre as 'genero' ,T2.nombre as 'nacionalidad',P.direccion FROM PERFIL P 
	INNER JOIN TIPO T ON P.tipo_documento = T.id_tipo 
	INNER JOIN TIPO T1 ON P.genero = T1.id_tipo INNER JOIN TIPO T2 ON P.nacionalidad = T2.id_tipo WHERE P.id_perfil = @id_perfil
END
GO

ALTER TABLE PROPIETARIO
ALTER COLUMN nacionalidad varchar(5)
GO
ALTER TABLE PROPIETARIO
ALTER COLUMN tipo_documento varchar(5)
GO

ALTER PROCEDURE USP_PROPIETARIO_REGISTRAR      
 @id_propietario int,      
 @nombres varchar(50),      
 @primer_apellido varchar(30),  
 @segundo_apellido varchar(30),  
 @tipo_documento varchar(5),  
 @nro_documento varchar(20),  
 @nacionalidad varchar(5),
 --FK
 @id_departamento int,
 @id_tipo int
 as      
 declare @id int      
  if @id_propietario=0      
   begin      
    INSERT INTO PROPIETARIO(nombres,primer_apellido,segundo_apellido,tipo_documento,nro_documento,nacionalidad,estado,id_departamento,id_tipo,fecha_registro)      
        VALUES(@nombres,@primer_apellido,@segundo_apellido,@tipo_documento,@nro_documento,@nacionalidad,1,@id_departamento,@id_tipo,GETDATE())          
   end      
  else      
   begin       
    UPDATE PROPIETARIO SET nombres=@nombres,primer_apellido=@primer_apellido,segundo_apellido=@segundo_apellido,
		tipo_documento=@tipo_documento,nro_documento=@nro_documento,nacionalidad=@nacionalidad,
		id_tipo=@id_tipo
       WHERE id_propietario=@id_propietario       
   end      
GO

CREATE PROC [dbo].[USP_SET_ROL_USER]
@id_usuario int,
@id_rol int
AS
DECLARE @id int
BEGIN
	UPDATE USUARIO SET id_rol = @id_rol 
	WHERE id_usuario = @id_usuario
	SET @id = @id_usuario
END
SELECT @id
GO


--Cambios Visitante Registro--

ALTER PROCEDURE [dbo].[USP_LISTAR_VISITAREG]
	  @id_visita_registro int
	  AS
	  if @id_visita_registro =0
	BEGIN
	 Select 
		Vr.id_visita_registro,
		Vr.fecha_ingreso,
		Vr.fecha_salida , 
		s.id_sucursal,
		s.nombre AS 'nombre_sucursal',
		sec.id_sector,
		sec.nombre_sector AS 'nombre_sector',
		t.id_torre,
		t.numero AS 'numero_torre',
		d.id_departamento,
		d.numero AS 'numero_departamento',	
		v.id_visitante,
		v.nro_documento,
		v.nombre +SPACE(1)+ v.apellidos AS 'nombre_visitante' 
	from VISITA_REGISTRO Vr 
		join DEPARTAMENTO d on vr.id_departamento=d.id_departamento 
		join VISITANTE v on vr.id_visitante=v.id_visitante 
		join TORRE t on d.id_torre=t.id_torre 
		join SECTOR sec on t.id_sector=sec.id_sector
		join SUCURSAL s on s.id_sucursal=sec.id_sucursal 
	END
	else
	begin
	 Select 
		Vr.id_visita_registro,
		Vr.fecha_ingreso,
		Vr.fecha_salida , 
		s.id_sucursal,
		s.nombre AS 'nombre_sucursal',
		sec.id_sector,
		sec.nombre_sector AS 'nombre_sector',
		t.id_torre,
		t.numero AS 'numero_torre',
		d.id_departamento,
		d.numero AS 'numero_departamento',	
		v.id_visitante,
		v.nro_documento,
		v.nombre +SPACE(1)+ v.apellidos AS 'nombre_visitante' 
	 from
	  VISITA_REGISTRO Vr 
		join DEPARTAMENTO d on vr.id_departamento=d.id_departamento 
		join VISITANTE v on vr.id_visitante=v.id_visitante 
		join TORRE t on d.id_torre=t.id_torre 
		join SECTOR sec on t.id_sector=sec.id_sector
		join SUCURSAL s on s.id_sucursal=sec.id_sucursal 
	  where vr.id_visita_registro=@id_visita_registro
	 END

GO

  ALTER PROCEDURE [dbo].[SP_VISITANTE_REGISTER]  
@id_visitante int,    
@nombre varchar(30),    
@apellido varchar(100),
@tipo_documento varchar(100),  
@nro_documento varchar(100), 
@genero varchar(100)  

AS    
   DECLARE @id int 
IF EXISTS(select * from VISITANTE where id_visitante=@id_visitante)
	  BEGIN    
	   IF EXISTS(SELECT * FROM VISITANTE  WHERE nro_documento = @nro_documento and id_visitante != @id_visitante)
		   BEGIN
				SET @id = -1
		   END
	   ELSE
		   BEGIN
		   update VISITANTE set nombre=@nombre, apellidos=@apellido,tipo_documento=@tipo_documento,
		   nro_documento=@nro_documento , genero=@genero
		   where id_visitante=@id_visitante
			SET @id = @id_visitante 
		   END
	  END
ELSE
	BEGIN
		IF EXISTS(SELECT * FROM VISITANTE  WHERE nro_documento = @nro_documento)
			BEGIN
				SET @id = -1
			END
		ELSE
			BEGIN
				insert into VISITANTE(nombre,apellidos,tipo_documento,nro_documento,genero)
				VALUES(@nombre,@apellido,@tipo_documento,@nro_documento,@genero)    
				SET @id=SCOPE_IDENTITY()  
			END
	END
SELECT @id
GO

 CREATE PROCEDURE [dbo].[USP_LISTAR_HISTORIAL_VISITA]
	    @id_visitante int
	  AS
	  if @id_visitante = 0
	BEGIN
	 Select 
		Vr.id_visita_registro,
		Vr.fecha_ingreso,
		Vr.fecha_salida , 
		s.id_sucursal,
		s.nombre AS 'nombre_sucursal',
		sec.id_sector,
		sec.nombre_sector AS 'nombre_sector',
		t.id_torre,
		t.numero AS 'numero_torre',
		d.id_departamento,
		d.numero AS 'numero_departamento',	
		v.id_visitante,
		v.nombre AS 'nombre_visitante' 
	from VISITA_REGISTRO Vr 
		join DEPARTAMENTO d on vr.id_departamento=d.id_departamento 
		join VISITANTE v on vr.id_visitante=v.id_visitante 
		join TORRE t on d.id_torre=t.id_torre 
		join SECTOR sec on t.id_sector=sec.id_sector
		join SUCURSAL s on s.id_sucursal=sec.id_sucursal 
		ORDER BY Vr.id_visita_registro DESC;
	END
	else
	begin
	 Select 
		Vr.id_visita_registro,
		Vr.fecha_ingreso,
		Vr.fecha_salida , 
		s.id_sucursal,
		s.nombre AS 'nombre_sucursal',
		sec.id_sector,
		sec.nombre_sector AS 'nombre_sector',
		t.id_torre,
		t.numero AS 'numero_torre',
		d.id_departamento,
		d.numero AS 'numero_departamento',	
		v.id_visitante,
		v.nombre AS 'nombre_visitante' 
	 from
	  VISITA_REGISTRO Vr 
		join DEPARTAMENTO d on vr.id_departamento=d.id_departamento 
		join VISITANTE v on vr.id_visitante=v.id_visitante 
		join TORRE t on d.id_torre=t.id_torre 
		join SECTOR sec on t.id_sector=sec.id_sector
		join SUCURSAL s on s.id_sucursal=sec.id_sucursal 
	  where vr.id_visitante=@id_visitante
	  ORDER BY Vr.id_visita_registro DESC;
	 END
GO

CREATE PROC USP_FILTRO_DNI_VIS
@nro_documento varchar(20)
AS
BEGIN
	IF @nro_documento = ''
		BEGIN
			SELECT V.id_visitante,V.nombre,V.apellidos,V.tipo_documento,
			T.nombre AS 'nombre_tipo',V.nro_documento,V.genero,T2.nombre AS 'nombre_genero',V.fecha_creacion
			FROM VISITANTE V LEFT JOIN TIPO T ON V.tipo_documento = T.id_tipo LEFT JOIN TIPO T2 ON V.genero = T2.id_tipo 
		END
	ELSE
		BEGIN
		SELECT V.id_visitante,V.nombre,V.apellidos,V.tipo_documento,
		T.nombre AS 'nombre_tipo',V.nro_documento,V.genero,T2.nombre AS 'nombre_genero',V.fecha_creacion
		FROM VISITANTE V LEFT JOIN TIPO T ON V.tipo_documento = T.id_tipo LEFT JOIN TIPO T2 ON V.genero = T2.id_tipo  
		WHERE nro_documento = @nro_documento
		END
END
GO

CREATE PROC [dbo].[USP_FILTRO_DNI_VIS_REG]
@nro_documento varchar(20)
AS
BEGIN
	IF EXISTS (SELECT * FROM VISITANTE WHERE nro_documento = @nro_documento)
	BEGIN
	IF EXISTS (SELECT * FROM VISITANTE V INNER JOIN VISITA_REGISTRO VR ON V.id_visitante = VR.id_visitante  WHERE VR.fecha_salida IS NULL AND V.nro_documento = @nro_documento)
		BEGIN
			SELECT -1;
		END
	ELSE
		BEGIN
			SELECT V.id_visitante
			FROM VISITANTE V LEFT JOIN TIPO T ON V.tipo_documento = T.id_tipo LEFT JOIN TIPO T2 ON V.genero = T2.id_tipo  
			WHERE nro_documento = @nro_documento
		END
	END
	ELSE
	BEGIN
		SELECT -2
	END
END
GO

CREATE PROC [dbo].[USP_FILTRO_VISITANTES_ACT]
@nro_documento varchar(20),
@estado INT
AS
BEGIN
	IF @estado = 1
	BEGIN
	Select 
		Vr.id_visita_registro,Vr.fecha_ingreso,Vr.fecha_salida , s.id_sucursal,
		s.nombre AS 'nombre_sucursal',sec.id_sector,sec.nombre_sector AS 'nombre_sector',
		t.id_torre,t.numero AS 'numero_torre',d.id_departamento,d.numero AS 'numero_departamento',	
		v.id_visitante,v.nro_documento,v.nombre +SPACE(1)+ v.apellidos AS 'nombre_visitante' 
	 from
	  VISITA_REGISTRO Vr join DEPARTAMENTO d on vr.id_departamento=d.id_departamento 
		join VISITANTE v on vr.id_visitante=v.id_visitante 
		join TORRE t on d.id_torre=t.id_torre 
		join SECTOR sec on t.id_sector=sec.id_sector
		join SUCURSAL s on s.id_sucursal=sec.id_sucursal 
		 where v.nro_documento = CASE WHEN @nro_documento = '' THEN V.nro_documento ELSE @nro_documento END
	END
	IF @estado = 2
	BEGIN
	Select 
		Vr.id_visita_registro,Vr.fecha_ingreso,Vr.fecha_salida , s.id_sucursal,
		s.nombre AS 'nombre_sucursal',sec.id_sector,sec.nombre_sector AS 'nombre_sector',
		t.id_torre,t.numero AS 'numero_torre',d.id_departamento,d.numero AS 'numero_departamento',	
		v.id_visitante,v.nro_documento,v.nombre +SPACE(1)+ v.apellidos AS 'nombre_visitante' 
	 from
	  VISITA_REGISTRO Vr join DEPARTAMENTO d on vr.id_departamento=d.id_departamento 
		join VISITANTE v on vr.id_visitante=v.id_visitante 
		join TORRE t on d.id_torre=t.id_torre 
		join SECTOR sec on t.id_sector=sec.id_sector
		join SUCURSAL s on s.id_sucursal=sec.id_sucursal 
		 where v.nro_documento = CASE WHEN @nro_documento = '' THEN V.nro_documento ELSE @nro_documento END
		 AND VR.fecha_salida IS NULL
	END
	IF @estado = 3
	BEGIN
	Select 
		Vr.id_visita_registro,Vr.fecha_ingreso,Vr.fecha_salida , s.id_sucursal,
		s.nombre AS 'nombre_sucursal',sec.id_sector,sec.nombre_sector AS 'nombre_sector',
		t.id_torre,t.numero AS 'numero_torre',d.id_departamento,d.numero AS 'numero_departamento',	
		v.id_visitante,v.nro_documento,v.nombre +SPACE(1)+ v.apellidos AS 'nombre_visitante' 
	 from
	  VISITA_REGISTRO Vr join DEPARTAMENTO d on vr.id_departamento=d.id_departamento 
		join VISITANTE v on vr.id_visitante=v.id_visitante 
		join TORRE t on d.id_torre=t.id_torre 
		join SECTOR sec on t.id_sector=sec.id_sector
		join SUCURSAL s on s.id_sucursal=sec.id_sucursal 
		 where v.nro_documento = CASE WHEN @nro_documento = '' THEN V.nro_documento ELSE @nro_documento END
		 AND VR.fecha_salida IS NOT NULL
	END
END
GO

CREATE PROC USP_FILTER_DEPARTAMENTOS
@id_sucursal INT = 0,
@id_sector INT = 0,
@id_torre INT = 0,
@numero INT = 0
AS
BEGIN
	select A.*,B.numero as 'numero_torre',C.id_sector, C.nombre_sector,D.id_sucursal,D.nombre as 'nombre_sucursal'
	from DEPARTAMENTO A 
	INNER JOIN TORRE B ON A.id_torre = B.id_torre
	INNER JOIN SECTOR C ON B.id_sector=C.id_sector
	INNER JOIN SUCURSAL D ON C.id_sucursal=D.id_sucursal
	WHERE D.id_sucursal = CASE WHEN @id_sucursal = 0 THEN D.id_sucursal ELSE @id_sucursal END
	AND C.id_sector = CASE WHEN @id_sector = 0 THEN C.id_sector ELSE @id_sector END
	AND B.id_torre = CASE WHEN @id_torre = 0 THEN B.id_torre ELSE @id_torre END
	AND A.numero = CASE WHEN @numero = 0 THEN A.numero ELSE @numero END
END
GO

CREATE PROCEDURE [dbo].[SP_USER_REGISTER_USUARIO]    
@id_usuario int,    
@username varchar(50),
@clave varchar(50),
@id_rol int,
@id_estado int
AS    
   DECLARE @id int 
 if NOT exists(select * from USUARIO where username=@username)    
  BEGIN    
   insert into USUARIO(username,clave,id_rol,id_estado)VALUES(@username,@clave,4,3)    
   SET @id=SCOPE_IDENTITY()    
  END
 ELSE
 BEGIN
  SET @id = -1
 END
SELECT @id