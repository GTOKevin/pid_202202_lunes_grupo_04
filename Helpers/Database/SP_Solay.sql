

Create Proc USP_DEPARTAMENTO_FILE_CREAR  
@url_imagen varchar(max), 
@id_departamento int  
As  
Begin  
Insert dbo.DEPARTAMENTO_FILE(url_imagen,id_departamento)  
Values (@url_imagen,@id_departamento)  
End  
GO
--
Create Proc USP_INCIDENTE_LISTAR  
AS  
BEGIN  
Select * from INCIDENTE  
END  
GO



CREATE Proc [dbo].[USP_INCIDENTE_CREAR]  
@descripcion varchar(200), @nombre_reportado varchar(100),@tipodocumento varchar(1),@nro_documento varchar(20),  
@id_departamento int  
As  
Begin  
Insert dbo.INCIDENTE(fecha_incidente,descripcion,nombre_reportado,tipo_documento,nro_documento,id_departamento)  
Values (GETDATE(),@descripcion,@nombre_reportado,@tipodocumento,@nro_documento,@id_departamento)  
End  
GO

CREATE Proc [dbo].[USP_INCIDENTE_ACTUALIZAR]  
@id_incidente int,@fecha_incidente datetime, @descripcion varchar(200),@nombre_reportado varchar(100),@tipo_documento varchar(1),  
@nro_documento varchar(20),@id_departamento int,@id_usuario int  
As  
Begin   
Update dbo.INCIDENTE set @fecha_incidente=@fecha_incidente,descripcion=@descripcion,nombre_reportado=@nombre_reportado,tipo_documento=@tipo_documento,  
nro_documento=@nro_documento,id_departamento=@id_departamento,id_usuario=@id_usuario  
Where id_incidente=@id_incidente  
End
GO


CREATE PROCEDURE SP_TIPO_LISTAR
@id_tipo int
as
	if @id_tipo=0
		BEGIN
			select * from TIPO
		END
	else
		BEGIN
			select * from TIPO where id_tipo=@id_tipo
		END
Go

CREATE PROCEDURE SP_CREAR_TIPO
@id_tipo int, @nombre varchar(50), @unidad varchar(20)  
AS    
   DECLARE @id int 
 if exists(select * from TIPO where id_tipo=@id_tipo)    
  BEGIN    
   update TIPO set nombre=@nombre, unidad=@unidad  
       where id_tipo=@id_tipo   
	SET @id = @id_tipo    
  END    
 ELSE    
  BEGIN    
  insert into TIPO(nombre,unidad)VALUES(@nombre,@unidad)    
   SET @id=SCOPE_IDENTITY()    
  END  
  SELECT @id
  go


  CREATE PROCEDURE SP_ROL_LISTAR
@id_rol int
as
	if @id_rol=0
		BEGIN
			select * from ROL
		END
	else
		BEGIN
			select * from ROL where id_rol=@id_rol
		END
Go

CREATE PROCEDURE SP_CREAR_ROL  
@id_rol int, 
@nombre varchar(50), 
@descripcion varchar(50)    
AS      
   DECLARE @id int   
  if not exists(select * from ROL where id_rol=@id_rol)      
	  BEGIN         
	  insert into ROL(nombre,descripcion)VALUES(@nombre,@descripcion)      
	   SET @id=SCOPE_IDENTITY()      
	  END    
  SELECT @id 
GO

Create Proc USP_REGISTRARROL_VAL
@id_rol int,
@nombre varchar(50),
@descripcion varchar(50)
AS
DECLARE @id int
if exists (select * from ROL where nombre = @nombre)
BEGIN
SET @id = -1
END
ELSE 
begin 
if exists (select * from ROL where id_rol= @id_rol)
BEGIN
UPDATE ROL SET nombre=@nombre, descripcion=@descripcion where id_rol=@id_rol
SET @id=@id_rol
END
ELSE
INSERT INTO ROL (nombre,descripcion)
VALUES (@nombre,@descripcion)
SET @id = SCOPE_IDENTITY()
END
SELECT @id
go

create procedure USP_INSERTARDEPARTMENTOFILE
@id_departamento int,
@url_imagen varchar(max)
AS
DECLARE @id int
BEGIN
Insert into DEPARTAMENTO_FILE ( id_departamento, url_imagen, fecha_creacion) 
values (@id_departamento, @url_imagen, GETDATE())
SET @id=SCOPE_IDENTITY();
END
select @id
GO




Create Proc USP_DEPARTAMENTO_FILE_LISTAR 
@id_departamento_file int
AS
IF @id_departamento_file=0
BEGIN  
Select * from DEPARTAMENTO_FILE  
END
ELSE
BEGIN
SELECT * FROM DEPARTAMENTO_FILE WHERE id_departamento_file=@id_departamento_file
END
GO

Create Proc USP_LISTARDEPARTAMENTOFILEPORIDDEPARTAMENTO
@id_departamento int 
AS
BEGIN
SELECT * FROM DEPARTAMENTO_FILE
WHERE id_departamento= @id_departamento
END 
Go


--Historial de incidente
CREATE PROC USP_INCIDENTE_HISTORIAL_LISTAR 
@id_incidente_historial int
AS
 if @id_incidente_historial=0      
BEGIN
select * from INCIDENTE_HISTORIAL
  end      
 else      
  begin       
   select * from INCIDENTE_HISTORIAL
   where id_incidente_historial = @id_incidente_historial
END
GO
--
CREATE PROCEDURE USP_INCIDENTE_HISTORIAL_CREAR
@acciones varchar(300),@idincidente int
AS
  BEGIN
   INSERT INTO INCIDENTE_HISTORIAL(acciones,fecha_historial,id_incidente) VALUES(@acciones,GETDATE(),@idincidente);
  END
GO