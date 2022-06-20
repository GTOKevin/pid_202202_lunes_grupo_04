-------------------------------------------------------------------------------------------------------------
------------------------- LISTAR RECIBO CON CONDICION ----------
CREATE PROC SP_RECIBO_LISTAR_N  
as  
 select TOP 20 * from RECIBO  order by fecha_registro desc, estado asc 
GO
------------------------    RECIBO       -----------------------------------
ALTER PROCEDURE SP_RECIBO_LISTAR 
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
@monto decimal,  
@id_departamento int,  
@servicio varchar(30),  
@fecha_pago datetime  
   
AS      
 if not exists( select * from RECIBO where id_departamento =@id_departamento and fecha_pago=@fecha_pago and servicio=@servicio)  
  begin  
   insert into RECIBO(monto,id_departamento,servicio,fecha_pago,estado)VALUES(@monto,@id_departamento,@servicio,@fecha_pago,0)         
   select SCOPE_IDENTITY()
   END    

go
----------------------------  DEPARTMANETO - PROPIETARIO LISTAR     ----------------------------------
CREATE PROCEDURE USP_DEPARTAMENTO_PROP_LISTAR   
@id_propietario int  
as  
 if @id_propietario=0  
  BEGIN  
     select D.id_departamento from DEPARTAMENTO D RIGHT join PROPIETARIO P ON D.id_departamento=P.id_departamento
	  where P.id_tipo=1
  END  
 else   
  BEGIN  
        select D.id_departamento from DEPARTAMENTO D RIGHT join PROPIETARIO P ON D.id_departamento=P.id_departamento
	  where P.id_tipo=1 AND P.id_propietario=@id_propietario
  END  
GO

----------------------------------------- RECIBO LISTAR FILTRO ------------------------------------------
create proc sp_recibo_listar_filtro
@dni varchar(20),
@nombre varchar(50),
@servicio varchar(30),
@estado int
as
	if @servicio='' and @estado=3
		begin
			select DISTINCT R.* from RECIBO R INNER JOIN PROPIETARIO P
					 ON R.id_departamento = p.id_departamento
					 where P.nombres like ''+@nombre+'%' 
					 and P.nro_documento like ''+@dni+'%' 
					 and P.id_tipo=1
		end
	else if @servicio='' and @estado<>3
		begin
			select DISTINCT R.* from RECIBO R INNER JOIN PROPIETARIO P
					 ON R.id_departamento = p.id_departamento
					 where P.nombres like @nombre+'%' 
					    and P.nro_documento like @dni+'%' 
						and R.estado=@estado
						and P.id_tipo=1
		end
	else if @estado = 3 and @servicio <> ''
		begin
			 select DISTINCT R.* from RECIBO R INNER JOIN PROPIETARIO P
					 ON R.id_departamento = p.id_departamento
					 where P.nombres like @nombre+'%' 
					    and P.nro_documento like @dni+'%' 
						and R.servicio = @servicio
						and P.id_tipo=1
		end
	else if @servicio <> '' and @estado<>3
			begin
			 select DISTINCT R.* from RECIBO R INNER JOIN PROPIETARIO P
					 ON R.id_departamento = p.id_departamento
					 where P.nombres like @nombre+'%' 
					    and P.nro_documento like @dni+'%' 
						and R.estado=@estado
						and R.servicio = @servicio
						and P.id_tipo=1
		end

GO

------------------------------------------- RECIBO PAGAR ----------------------------------------
create proc sp_recibo_pagar
@id_recibo int,
@estado bit
as
	update recibo set estado=@estado where id_recibo=@id_recibo
go