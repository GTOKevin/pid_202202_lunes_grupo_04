-------------------------------------------------------------------------------------------------------------

------------------------    RECIBO       -----------------------------------
CREATE PROCEDURE SP_RECIBO_LISTAR
@id_recibo int
as
	if @id_recibo=0
		BEGIN
			select * from RECIBO
		END
	else
		BEGIN
			select * from RECIBO where id_recibo=@id_recibo
		END
go

CREATE PROCEDURE SP_RECIBO_REGISTER    
@id_recibo int,    
@id_servicio int,    
@monto decimal,
@estado bit,
@fecha_pago datetime,
@fecha_vencimiento datetime
 
AS    
   DECLARE @id int 
 if exists(select * from RECIBO where id_recibo=@id_recibo)    
  BEGIN    
   update RECIBO set id_servicio=@id_servicio,monto=@monto,estado=@estado, fecha_pago=@fecha_pago,fecha_vencimiento=@fecha_vencimiento
       where id_recibo=@id_recibo    
	SET @id = @id_recibo    
  END    
 ELSE    
  BEGIN    
  insert into RECIBO(id_servicio,monto,estado,fecha_pago,fecha_vencimiento)VALUES(@id_servicio,@monto,@estado,@fecha_pago,@fecha_vencimiento)    
   SET @id=SCOPE_IDENTITY()    
  END  
  SELECT @id
GO
-----------------------     SERVICIO     ----------------------------------
CREATE PROCEDURE SP_SERVICIO_LISTAR
@id_servicio int
as
	if @id_servicio=0
		BEGIN
			select * from SERVICIO
		END
	else
		BEGIN
			select * from SERVICIO where id_servicio=@id_servicio
		END
go

CREATE PROCEDURE SP_SERVICIO_REGISTER    
@id_servicio int,
@id_tipo int,
@id_departamento int,
@nombre varchar(50)    
 
AS    
   DECLARE @id int 
 if exists(select * from SERVICIO where id_servicio=@id_servicio)    
  BEGIN    
   update SERVICIO set id_tipo=@id_tipo,id_departamento=@id_departamento, nombre=@nombre    
       where id_servicio=@id_servicio    
	SET @id = @id_servicio    
  END    
 ELSE    
  BEGIN    
  insert into SERVICIO(id_tipo,id_departamento,nombre)VALUES(@id_tipo,@id_departamento,@nombre)    
   SET @id=SCOPE_IDENTITY()    
  END  
  SELECT @id
GO