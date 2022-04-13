CREATE PROCEDURE SP_USUARIO_OBTENER_LOGIN
@userName varchar(30)
as
		select TOP 1 * from USUARIO where username=@userName
GO


CREATE PROCEDURE USP_PERFIL_LIST_USUARIO
@id_perfil int
as
	select * from PERFIL where id_perfil=@id_perfil
go


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





--TRIGGERS

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
	