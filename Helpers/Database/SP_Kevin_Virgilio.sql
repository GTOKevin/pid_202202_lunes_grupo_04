CREATE PROCEDURE SP_USUARIO_OBTENER_NAME
@userName varchar(30)
as
	select * from USUARIO WHERE username=@userName
go
