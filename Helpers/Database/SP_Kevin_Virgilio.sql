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
