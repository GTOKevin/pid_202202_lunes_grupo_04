-------------------------------------------------------------------------------------------------------------

------------------------    RECIBO       -----------------------------------
CREATE PROCEDURE SP_RECIBO_LISTAR 
@id_recibo int  
as  
 if @id_recibo=0  
  begin  
   select 
		r.id_recibo, 
		su.id_sucursal,
		su.nombre AS 'nombre_sucursal',
		sec.id_sector,
		sec.nombre_sector AS 'nombre_sector',
		t.id_torre,
		t.numero AS 'numero_torre',
		d.id_departamento,
		d.numero AS 'numero_departamento',	
		ser.id_servicio,
		ser.nombre AS 'nombre_servicio',
		r.monto,
		r.fecha_pago,
		fecha_vencimiento	 
   from RECIBO r
   		join SERVICIO ser on r.id_servicio=ser.id_servicio
	    join DEPARTAMENTO d on ser.id_departamento=d.id_departamento 
		join TORRE t on d.id_torre=t.id_torre 
		join SECTOR sec on t.id_sector=sec.id_sector
		join SUCURSAL su on sec.id_sucursal=su.id_sucursal

  end  
 else  
  begin   
   select 
   r.id_recibo, 
		su.id_sucursal,
		su.nombre AS 'nombre_sucursal',
		sec.id_sector,
		sec.nombre_sector AS 'nombre_sector',
		t.id_torre,
		t.numero AS 'numero_torre',
		d.id_departamento,
		d.numero AS 'numero_departamento',	
		ser.id_servicio,
		ser.nombre AS 'nombre_servicio',
		r.monto,
		r.fecha_pago,
		fecha_vencimiento	 
   from RECIBO r
   		join SERVICIO ser on r.id_servicio=ser.id_servicio
	    join DEPARTAMENTO d on ser.id_departamento=d.id_departamento 
		join TORRE t on d.id_torre=t.id_torre 
		join SECTOR sec on t.id_sector=sec.id_sector
		join SUCURSAL su on sec.id_sucursal=su.id_sucursal
		where r.id_recibo=@id_recibo
  end  

GO

-----------------------------------RECIBO REGISTRAR---------------------------------
ALTER PROCEDURE SP_RECIBO_REGISTER    
@id_recibo int,    
@id_servicio int,    
@monto decimal,
@fecha_pago datetime,
@fecha_vencimiento datetime
 
AS    
   DECLARE @id int 
 if exists(select * from RECIBO where id_recibo=@id_recibo)    
  BEGIN    
   update RECIBO set id_servicio=@id_servicio,monto=@monto, fecha_pago=@fecha_pago,fecha_vencimiento=@fecha_vencimiento
       where id_recibo=@id_recibo    
	SET @id = @id_recibo    
  END    
 ELSE    
  BEGIN    
  insert into RECIBO(id_servicio,monto,fecha_pago,fecha_vencimiento)VALUES(@id_servicio,@monto,@fecha_pago,@fecha_vencimiento)    
   SET @id=SCOPE_IDENTITY()    
  END  
  SELECT @id
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

