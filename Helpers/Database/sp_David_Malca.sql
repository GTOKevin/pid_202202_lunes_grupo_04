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
			select * from VISITANTE
		END
	else
		BEGIN
			select * from VISITANTE where id_visitante=@id_visitante
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


