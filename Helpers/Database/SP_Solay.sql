Create Proc USP_DEPARTAMENTO_FILE_LISTAR  
AS  
BEGIN  
Select * from DEPARTAMENTO_FILE  
END  
GO

Create Proc USP_DEPARTAMENTO_FILE_CREAR  
@url_imagen varchar(300), @id_departamento int  
As  
Begin  
Insert dbo.DEPARTAMENTO_FILE(url_imagen,id_departamento)  
Values (@url_imagen,@id_departamento)  
End  
GO

CREATE Proc USP_DEPARTAMENTO_FILE_ACTUALIZAR  
@id_departamento_file int,@url_imagen varchar, @fecha_creacion datetime, @id_departamento int  
As  
Begin   
Update dbo.DEPARTAMENTO_FILE set url_imagen=@url_imagen,fecha_creacion=@fecha_creacion, id_departamento=@id_departamento  
Where id_departamento_file=@id_departamento_file  
End
GO

Create Proc USP_DEPARTAMENTO_LISTAR  
As  
Begin Select D.id_departamento,D.piso,D.numero,D.metros_cuadrado,D.dormitorio,  
D.banio,D.fecha_creacion,D.fecha_actualizacion  
From dbo.DEPARTAMENTO D INNER JOIN dbo.TORRE T  
On D.id_torre=T.id_torre  
INNER JOIN dbo.USUARIO U On D.id_usuario=U.id_usuario  
End  
GO

CREATE Proc USP_DEPARTAMENTO_CREAR  
@piso numeric,@numero numeric,@metros_cuadrado numeric,@dormitorio numeric,  
@banio numeric,@id_torre int  
As  
Begin  
Insert dbo.DEPARTAMENTO(piso,numero,metros_cuadrado,dormitorio,banio,id_torre,fecha_actualizacion)  
Values (@piso,@numero,@metros_cuadrado,@dormitorio,@banio,@id_torre,GETDATE())  
End  
GO	

CREATE Proc [dbo].[USP_DEPARTAMENTO_ACTUALIZAR]  
@id_departamento int,@piso numeric,@numero numeric,@metros_cuadrado numeric,@dormitorio numeric,  
@banio numeric,@fecha_creacion datetime,@id_torre int,@id_usuario int  
As  
Begin   
Update dbo.DEPARTAMENTO set piso=@piso,numero=@numero,metros_cuadrado=@metros_cuadrado,dormitorio=@dormitorio,  
banio=@banio,fecha_creacion=@fecha_creacion, id_torre=@id_torre,id_usuario=@id_usuario  
Where id_departamento=@id_departamento  
End
GO

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