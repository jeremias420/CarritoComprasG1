--SP REGISTRAR USUARIO
create proc sp_RegistrarUsuario(
@Nombres varchar(100),
@Apellidos varchar(100),
@Correo varchar(100),
@Clave varchar(100),
@Activo bit,
@Mensaje varchar(500) output,
@Resultado int output
)
as
begin
	SET @Resultado = 0
		if not exists (select * from usuario where usua_correo = @Correo)
begin
insert into usuario(usua_nombre, usua_apellido, usua_correo, usua_clave, usua_activo)
values
(@Nombres, @Apellidos, @Correo, @Clave, @Activo)

	set @Resultado = scope_identity()
end
	else
	set @Mensaje = 'El correo del usuario ya existe'
end


--SP EDITAR USUARIO
create proc sp_EditarUsuario(
	@IdUsuario int,
	@Nombres varchar(100),
	@Apellidos varchar(100),
	@Correo varchar(100),
	@Activo bit,
	@Mensaje varchar(500) output,
	@Resultado bit output
)
as
begin
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM usuario WHERE usua_correo = @Correo and usua_id != @IdUsuario)
	begin
    	update top (1) USUARIO set
        	usua_nombre = @Nombres,
        	usua_apellido = @Apellidos,
        	usua_correo = @Correo,
        	usua_activo = @Activo
    	where usua_id = @IdUsuario
   	 
    	SET @Resultado = 1
	end
	else
    	set @Mensaje = 'El correo del usuario ya existe'
end
