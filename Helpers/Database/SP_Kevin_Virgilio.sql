--USUARIO
CREATE PROCEDURE SP_USUARIO_OBTENER_LOGIN
@userName varchar(30)
as
		select TOP 1 * from USUARIO where username=@userName
GO

CREATE PROCEDURE USP_INSERT_USUARIO
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
				END
		 END
GO

--USUARIO TRIGGERS

CREATE TRIGGER TRG_USUARIO
ON USUARIO
AFTER INSERT
AS
	declare	@id_perfil int
	declare @id_usuario int 
	set @id_usuario =(select id_usuario from inserted)
	BEGIN
		insert into PERFIL(nombres)
			values('');

		SET @id_perfil = SCOPE_IDENTITY();

		update USUARIO set id_perfil=@id_perfil where id_usuario=@id_usuario;
	END
GO

--PERFIL
CREATE PROCEDURE USP_PERFIL_LIST_USUARIO
@id_perfil int
as
	select * from PERFIL where id_perfil=@id_perfil
go

--SUCURSAL
CREATE PROCEDURE SP_SUCURSAL_LISTAR
@id_sucursal int
as
	if @id_sucursal=0
		BEGIN
			select * from SUCURSAL
		END
	else
		BEGIN
			select * from SUCURSAL where id_sucursal=@id_sucursal
		END
go

CREATE PROCEDURE SP_SUCURSAL_REGISTER    
@id_sucursal int,    
@nombre varchar(50),    
@descripcion varchar(100)  
 
AS    
   DECLARE @id int 
 if exists(select * from SUCURSAL where id_sucursal=@id_sucursal)    
  BEGIN    
   update SUCURSAL set nombre=@nombre,descripcion=@descripcion    
       where id_sucursal=@id_sucursal    
	SET @id = @id_sucursal    
  END    
 ELSE    
  BEGIN    
  insert into SUCURSAL(nombre,descripcion)VALUES(@nombre,@descripcion)    
   SET @id=SCOPE_IDENTITY()    
  END  
  SELECT @id
GO

--SECTOR
CREATE PROCEDURE USP_SECTOR_LISTAR
@id_sector int
as
	if @id_sector=0
		begin
			select * from SECTOR
		end
	else
		begin 
			select * from SECTOR where id_sector=@id_sector
		end
GO

CREATE PROCEDURE USP_SECTOR_LISTAR_SUC
@id_sector int  
as  
 if @id_sector=0  
  begin  
   select a.*,b.nombre as 'nombre_sucursal' from SECTOR A INNER JOIN SUCURSAL B ON A.id_sucursal=B.id_sucursal
  end  
 else  
  begin   
    select a.*,b.nombre as 'nombre_sucursal' from SECTOR A INNER JOIN SUCURSAL B ON A.id_sucursal=B.id_sucursal 
	where a.id_sector=@id_sector
  end  
GO

CREATE PROCEDURE USP_SECTOR_REGISTRAR  
 @id_sector int,  
 @nombre_sector varchar(50),  
 @id_sucursal int  
 as  
 declare @id int  
  if @id_sector=0  
   begin  
    INSERT INTO SECTOR(nombre_sector,id_sucursal)  
        VALUES(@nombre_sector,@id_sucursal)  
    set @id=SCOPE_IDENTITY()  
   end  
  else  
   begin   
    UPDATE SECTOR SET nombre_sector=@nombre_sector WHERE id_sector=@id_sector  
    set @id=@id_sector  
   end  
   select @id
GO

--TORRE
CREATE PROCEDURE USP_TORRE_LISTAR  
@id_torre int      
as      
 if @id_torre=0      
  begin      
   select a.*,b.nombre_sector,c.nombre as 'nombre_sucursal',c.id_sucursal from TORRE A INNER JOIN SECTOR B ON A.id_sector=B.id_sector   
               INNER JOIN SUCURSAL C ON B.id_sucursal=C.id_sucursal  
  end      
 else      
  begin       
   select a.*,b.nombre_sector,c.nombre as 'nombre_sucursal',c.id_sucursal from TORRE A INNER JOIN SECTOR B ON A.id_sector=B.id_sector   
               INNER JOIN SUCURSAL C ON B.id_sucursal=C.id_sucursal  
 where a.id_torre=@id_torre    
  end      
GO

CREATE PROCEDURE USP_TORRE_REGISTRAR  
 @id_torre int,  
 @numero decimal(10,0),  
 @id_sector int  
 as  
 declare @id int  
  if @id_torre=0  
   begin  
    INSERT INTO TORRE(numero,id_sector)  
        VALUES(@numero,@id_sector)  
    set @id=SCOPE_IDENTITY()  
   end  
  else  
   begin   
    UPDATE TORRE SET numero=@numero WHERE id_torre=@id_torre  
    set @id=@id_torre  
   end  
   select @id
GO

--DEPARTAMENTO

CREATE PROCEDURE USP_DEPARTAMETO_REGISTRAR      
 @id_departamento int,      
 @piso int,      
 @numero int,  
 @metros_cuadrado int,  
 @dormitorio int,  
 @banio int,  
 @id_torre int,
 @id_usuario int  
 as      
 declare @id int      
  if @id_departamento=0      
   begin      
    INSERT INTO DEPARTAMENTO(piso,numero,metros_cuadrado,dormitorio,banio,id_torre,id_usuario)      
        VALUES(@piso,@numero,@metros_cuadrado,@dormitorio,@banio,@id_torre,@id_usuario)      
    set @id=SCOPE_IDENTITY()      
   end      
  else      
   begin       
    UPDATE DEPARTAMENTO SET piso=@piso,numero=@numero,metros_cuadrado=@metros_cuadrado,dormitorio=@dormitorio,  
          banio=@banio,fecha_actualizacion=GETDATE(),id_usuario=@id_usuario  
       WHERE id_departamento=@id_departamento      
    set @id=@id_departamento      
   end      
   select @id   
GO

CREATE PROCEDURE USP_DEPARTAMENTO_LISTAR 
@id_departamento int
as
	if @id_departamento=0
		BEGIN
			select A.*,B.numero as 'numero_torre',C.id_sector, C.nombre_sector,D.id_sucursal,D.nombre as 'nombre_sucursal'
										 from DEPARTAMENTO A INNER JOIN TORRE B ON A.id_torre = B.id_torre
										 INNER JOIN SECTOR C ON B.id_sector=C.id_sector
										 INNER JOIN SUCURSAL D ON C.id_sucursal=D.id_sucursal
		END
	else 
		BEGIN
				select A.*,B.numero as 'numero_torre',C.id_sector, C.nombre_sector,D.id_sucursal,D.nombre as 'nombre_sucursal'
										 from DEPARTAMENTO A INNER JOIN TORRE B ON A.id_torre = B.id_torre
										 INNER JOIN SECTOR C ON B.id_sector=C.id_sector
										 INNER JOIN SUCURSAL D ON C.id_sucursal=D.id_sucursal
										 where A.id_departamento=@id_departamento
		END
GO


--PROPIETARIO-DEPARTAMENTO

CREATE PROCEDURE USP_PROPIETARIO_REGISTRAR      
 @id_propietario int,      
 @nombres varchar(50),      
 @primer_apellido varchar(30),  
 @segundo_apellido varchar(30),  
 @tipo_documento varchar(1),  
 @nro_documento varchar(20),  
 @nacionalidad varchar(1),
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



CREATE PROCEDURE USP_PROPIETARIO_DEP_LISTAR  
 @id_departamento int  
 as  
 if @id_departamento>0  
  begin  
   select A.id_propietario,A.nombres,A.primer_apellido,A.segundo_apellido,A.tipo_documento,	
          (select b.nombre from tipo b where b.id_tipo=a.tipo_documento)as 'nombre_documento',
		  A.nro_documento,
          (select b.nombre from tipo b where b.id_tipo=a.nacionalidad)as 'nombre_nacionalidad',
		  A.nacionalidad,
		  A.fecha_registro,
		  A.estado,
		  A.id_departamento,
		  A.id_tipo,
          (select b.nombre from tipo b where b.id_tipo=a.id_tipo)as 'nombre_tipo'	from PROPIETARIO A  WHERE A.id_departamento=@id_departamento
  end  
GO