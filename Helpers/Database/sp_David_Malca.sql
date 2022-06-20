--LIST
CREATE proc USP_LIST_MOVIMIENTO
@idmovi int
as
if @idmovi =0 
  begin
  select * from MOVIMIENTO
  end
  else
  begin
    select * from MOVIMIENTO where id_movimiento = @idmovi

  end
  GO
  --CREATE
ALTER TABLE INCIDENTE
ADD Estado BIT
GO

  ALTER Proc [dbo].[USP_INCIDENTE_CREAR]  
@descripcion varchar(200), @nombre_reportado varchar(100),@tipodocumento varchar(1),@nro_documento varchar(20),  
@id_departamento int , @idusuario int 
As  
Begin  
Insert dbo.INCIDENTE(fecha_incidente,descripcion,nombre_reportado,tipo_documento,nro_documento,fecha_registro,id_departamento,id_usuario,Estado)  
Values (GETDATE(),@descripcion,@nombre_reportado,@tipodocumento,@nro_documento,GETDATE(),@id_departamento,@idusuario,0)  
End 
go
  
CREATE proc USP_INSERT_MOVIMIENTO
@idmovi int , @idpropietario int , @idtipo int 
as
  declare @id int
  if exists(select * from MOVIMIENTO where id_movimiento = @idmovi )
  begin
     update MOVIMIENTO set id_propietario = @idmovi , id_tipo = @idtipo
	 where id_movimiento = @idmovi
	 set @id = @idmovi
	 end
	 else
	 begin
  insert into MOVIMIENTO(id_propietario,id_tipo) values(@idpropietario,@idtipo)
  set @id = SCOPE_IDENTITY()
  end
  select @id
  GO
  --VISITANTE
  --LISTAR
 CREATE PROCEDURE SP_VISITANTE_LISTAR    
@id_visitante int    
as    
 if @id_visitante=0    
  BEGIN    
   select A.id_visitante,A.nombre,A.apellidos,A.tipo_documento,  
   (select B.nombre from TIPO B WHERE B.id_tipo=A.tipo_documento)AS 'nombre_tipo',  
 A.nro_documento,  
 A.genero,  
   (select B.nombre from TIPO B WHERE B.id_tipo=A.genero)AS 'nombre_genero',  
   
 a.fecha_creacion from VISITANTE A    
  END    
 else    
  BEGIN    
    select A.id_visitante,A.nombre,A.apellidos,A.tipo_documento,  
   (select B.nombre from TIPO B WHERE B.id_tipo=A.tipo_documento)AS 'nombre_tipo',  
	A.nro_documento,  
	A.genero,  
   (select B.nombre from TIPO B WHERE B.id_tipo=A.genero)AS 'nombre_genero',  
   
	 a.fecha_creacion from VISITANTE A where A.id_visitante=@id_visitante    
  END 
GO

--REGISTRO
CREATE PROCEDURE SP_VISITANTE_REGISTER  
@id_visitante int,    
@nombre varchar(30),    
@apellido varchar(100),
@tipo_documento varchar(100),  
@nro_documento varchar(100), 
@genero varchar(100)  

AS    
   DECLARE @id int 
 if exists(select * from VISITANTE where id_visitante=@id_visitante)    
  BEGIN    
   update VISITANTE set nombre=@nombre, apellidos=@apellido,tipo_documento=@tipo_documento,
   nro_documento=@nro_documento , genero=@genero
   where id_visitante=@id_visitante
	SET @id = @id_visitante    
  END    
 ELSE    
  BEGIN    
  insert into VISITANTE(nombre,apellidos,tipo_documento,nro_documento,genero)VALUES(@nombre,@apellido,@tipo_documento,@nro_documento,
  @genero)    
   SET @id=SCOPE_IDENTITY()    
  END  
  SELECT @id
  GO

  --VISITA REGISTRO
  --LIST
  CREATE PROCEDURE SP_VISITAREGISTRO_LISTAR
@id_visita_registro int
as
	if @id_visita_registro=0
		BEGIN
			select * from VISITA_REGISTRO
		END
	else
		BEGIN
			select * from VISITA_REGISTRO where id_visita_registro=@id_visita_registro
		END
		GO
--REGISTRO
CREATE PROCEDURE SP_VISITAREGISTER_REGISTER
@id_visita_registro int,    
@id_departamento int  ,
@id_visitante int 
 
AS    
   DECLARE @id int 
 if exists(select * from VISITA_REGISTRO where id_visita_registro=@id_visita_registro)    
  BEGIN    
   update VISITA_REGISTRO  set id_departamento=@id_departamento,id_visitante=@id_visitante  
       where id_visita_registro=@id_visita_registro    
	SET @id = @id_visita_registro    
  END    
 ELSE    
  BEGIN    
  insert into VISITA_REGISTRO(id_departamento,id_visitante)VALUES(@id_departamento,@id_visitante)    
   SET @id=SCOPE_IDENTITY()    
  END  
  SELECT @id
  GO

  --VISITA DE REGISTRO
  CREATE PROCEDURE USP_LISTAR_VISITAREG
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
		v.nombre AS 'nombre_visitante' 
	from VISITA_REGISTRO Vr 
		join DEPARTAMENTO d on vr.id_departamento=d.id_departamento 
		join VISITANTE v on vr.id_visitante=v.id_visitante 
		join TORRE t on d.id_torre=t.id_torre 
		join SECTOR sec on t.id_sector=sec.id_sector
		join SUCURSAL s on s.id_sucursal=s.id_sucursal 
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
		join SUCURSAL s on s.id_sucursal=s.id_sucursal 
	  where vr.id_visita_registro=@id_visita_registro
	 END
	GO

----CREAR
create PROCEDURE USP_VISITAREG_REGISTER 
 @id_visita_registro int,      
 @fecha_ingreso datetime,      
 @fecha_salida datetime,  
 @id_departamento int,  
 @id_visitante int
 as      
 declare @id int      
  if @id_visita_registro=0      
   begin      
    INSERT INTO VISITA_REGISTRO(fecha_ingreso,fecha_salida,id_departamento,id_visitante)      
        VALUES(@fecha_ingreso,@fecha_salida,@id_departamento,@id_visitante)      
    set @id=SCOPE_IDENTITY()      
   end      
  else      
   begin       
    UPDATE VISITA_REGISTRO SET fecha_ingreso=@fecha_ingreso,fecha_salida=@fecha_salida,id_departamento=@id_departamento,
	id_visitante=@id_visitante
       WHERE id_visita_registro=@id_visita_registro      
    set @id=@id_visita_registro    
   end      
   select @id   


