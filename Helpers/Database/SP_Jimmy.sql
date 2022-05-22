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


------LISTAR SERVICIO EN EL RECIBO ----------------

CREATE PROCEDURE USP_RECIBO_LISTAR_SERVICIO
@id_recibo int  
as  
 if @id_recibo=0  
  begin  
   select R.*,S.nombre as 'nombre_servicio' from RECIBO R INNER JOIN SERVICIO S ON R.id_servicio=S.id_servicio
  end  
 else  
  begin   
    select R.*,S.nombre as 'nombre_servicio' from RECIBO R INNER JOIN SERVICIO S ON R.id_servicio=S.id_servicio 
	where R.id_recibo=@id_recibo
  end  
GO

-----------------------     SERVICIO     ----------------------------------

------LISTAR SERVICIO ----------------
CREATE PROCEDURE SP_SERVICIO_LISTAR 
@id_servicio int  
as  
 if @id_servicio=0  
  begin  
   select 
		s.id_servicio, 
		ti.id_tipo,
		ti.nombre AS 'nombre_tipo',
		su.id_sucursal,
		su.nombre AS 'nombre_sucursal',
		sec.id_sector,
		sec.nombre_sector AS 'nombre_sector',
		t.id_torre,
		t.numero AS 'numero_torre',
		d.id_departamento,
		d.numero AS 'numero_departamento',		
		s.nombre 
   from SERVICIO s 

   join DEPARTAMENTO d on s.id_departamento=d.id_departamento 
		join TORRE t on d.id_torre=t.id_torre 
		join SECTOR sec on t.id_sector=sec.id_sector
		join SUCURSAL su on sec.id_sucursal=su.id_sucursal 	
		   join TIPO ti on s.id_tipo=ti.id_tipo
  end  
 else  
  begin   
    select 
	s.id_servicio, 
		ti.id_tipo,
		ti.nombre AS 'nombre_tipo',
		su.id_sucursal,
		su.nombre AS 'nombre_sucursal',
		sec.id_sector,
		sec.nombre_sector AS 'nombre_sector',
		t.id_torre,
		t.numero AS 'numero_torre',
		d.id_departamento,
		d.numero AS 'numero_departamento',		
		s.nombre 
   from SERVICIO s 
   join DEPARTAMENTO d on s.id_departamento=d.id_departamento 
		join TORRE t on d.id_torre=t.id_torre 
		join SECTOR sec on t.id_sector=sec.id_sector
		join SUCURSAL su on sec.id_sucursal=su.id_sucursal 
		join TIPO ti on s.id_tipo=ti.id_tipo
		where s.id_servicio=@id_servicio
  end  

GO




------REGISTRAR SERVICIO ----------------
CREATE PROCEDURE SP_SERVICIO_REGISTER    
@id_servicio int,
@id_tipo int,
@id_departamento int,
@nombre varchar(50)    
 
AS    
   DECLARE @id int
   IF @id_servicio=0
  BEGIN    
  INSERT INTO SERVICIO(id_tipo,id_departamento,nombre)
  VALUES(@id_tipo,@id_departamento,@nombre)    
   SET @id=SCOPE_IDENTITY()    
  END  
   
 ELSE    
  BEGIN    
   UPDATE SERVICIO SET id_tipo=@id_tipo,id_departamento=@id_departamento, nombre=@nombre    
       WHERE id_servicio=@id_servicio    
	SET @id = @id_servicio    
  END  
  SELECT @id
GO

