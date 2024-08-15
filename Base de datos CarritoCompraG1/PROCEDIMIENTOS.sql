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

--SP REGISTRAR CATEGORIA
create proc sp_RegistrarCategoria(
@Descripcion varchar(100),
@Activo bit,
@Mensaje varchar(100) output,
@Resultado int output

)
as
begin
	SET @Resultado = 0
		if not exists (select * from categoria where cate_descripcion = @Descripcion)
begin
insert into categoria (cate_descripcion, cate_activo)
values
(@Descripcion,@Activo)

	set @Resultado = scope_identity()
end
	else
	set @Mensaje = 'La categoria ya existe'
end

--SP EDITAR CATEGORIA
create proc sp_EditarCategoria(
@IdCategoria int,
@Descripcion varchar(100),
@Activo bit,
@Mensaje varchar(100) output,
@Resultado int output
)
as
begin
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM categoria WHERE cate_descripcion = @Descripcion and cate_id != @IdCategoria)
	begin
    	update top (1) categoria set
        	cate_descripcion = @Descripcion,
        	cate_activo = @Activo
    	where cate_id = @IdCategoria
   	 
    	SET @Resultado = 1
	end
	else
    	set @Mensaje = 'La categoria ya existe'
end

--SP ELIMINAR CATEGORIA
create proc sp_EliminarCategoria(
@IdCategoria int,
@Mensaje varchar(500) output,
@Resultado int output
)
as
begin
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM producto p
	inner join categoria c on c.cate_id = p.cate_id
	where p.cate_id = @IdCategoria)
	begin
    	delete top (1) from categoria where cate_id = @IdCategoria
    	SET @Resultado = 1
	end
	else
    	set @Mensaje = 'La categoria se encuentra relacionada a un producto'
end
