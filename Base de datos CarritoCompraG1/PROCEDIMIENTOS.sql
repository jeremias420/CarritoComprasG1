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
------------------------------------------------------------------


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
alter procedure sp_EliminarCategoria(
@IdCategoria int,
@Mensaje varchar(500) output,
@Resultado int output
)
as
begin
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM producto 
					inner join categoria on cate_id = prod_cate_id
					where prod_cate_id = @IdCategoria)
	begin
    	delete top (1) from categoria where cate_id = @IdCategoria
    	SET @Resultado = 1
	end
	else
    	set @Mensaje = 'La categoria se encuentra relacionada a un producto'
end
SELECT * FROM producto
---------------------------------------------------------------


--REGISTRAR MARCA
alter procedure sp_RegistrarMarca(
@Descripcion varchar(100),
@Activo bit,
@Mensaje varchar(100) output,
@Resultado bit output

)
as
begin
	SET @Resultado = 0
		if not exists (select * from marca where marc_descripcion = @Descripcion)
begin
insert into marca(marc_descripcion, marc_activo)
values
(@Descripcion,@Activo)

	set @Resultado = scope_identity()
end
	else
	set @Mensaje = 'La marca ya existe'
end

--EDITAR MARCA
alter procedure sp_EditarMarca(
@IdMarca int,
@Descripcion varchar(100),
@Activo bit,
@Mensaje varchar(100) output,
@Resultado bit output
)
as
begin
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM marca WHERE marc_descripcion = @Descripcion and marc_id != @IdMarca)
	begin
    	update top (1) marca set
        	marc_descripcion = @Descripcion,
        	marc_activo = @Activo
    	where marc_id = @IdMarca
   	 
    	SET @Resultado = 1
	end
	else
    	set @Mensaje = 'La marca ya existe'
end

--SP ELIMINAR MARCA
create procedure sp_EliminarMarca(
@IdMarca int,
@Mensaje varchar(500) output,
@Resultado bit output
)
as
begin
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM producto 
					inner join marca on marc_id = prod_marc_id
					where prod_marc_id = @IdMarca)
	begin
    	delete top (1) from marca where marc_id = @IdMarca
    	SET @Resultado = 1
	end
	else
    	set @Mensaje = 'La marca se encuentra relacionada a un producto'
end

-------------------------------------------------------------------------

--REGISTRAR PRODUCTO
create procedure sp_RegistrarProducto(
@Nombre varchar(100),
@Descripcion varchar(100),
@IdMarca varchar(100),
@IdCategoria varchar(100),
@Precio decimal (10,2),
@Stock int,
@Activo bit,
@Mensaje varchar(100) output,
@Resultado bit output

)
as
begin
	SET @Resultado = 0
		if not exists (select * from producto where prod_descripcion = @Descripcion)
begin
insert into producto(prod_nombre, prod_descripcion, prod_marc_id, prod_cate_id, prod_precio, prod_stock, prod_activo)
values
(@Nombre,@Descripcion,@IdMarca,@IdCategoria,@Precio,@Stock,@Activo)

	set @Resultado = scope_identity()
end
	else
	set @Mensaje = 'El producto ya existe'
end


--EDITAR PRODUCTO
create procedure sp_EditarProducto(
@IdProducto int,
@Nombre varchar(100),
@Descripcion varchar(100),
@IdMarca varchar(100),
@IdCategoria varchar(100),
@Precio decimal (10,2),
@Stock int,
@Activo bit,
@Mensaje varchar(100) output,
@Resultado bit output

)
as
begin
	SET @Resultado = 0
	if not exists (select * from producto where prod_nombre = @Nombre and prod_id = @IdProducto)
	begin

    	update producto set
        	prod_nombre = @Nombre,
			prod_descripcion = @Descripcion,
			prod_marc_id = @IdMarca,
			prod_cate_id = @IdCategoria,
			prod_precio = @Precio,
			prod_stock = @Stock,
			prod_activo = @Activo
    	where prod_id = @IdProducto
   	 
    	SET @Resultado = 1
	end
	else
    	set @Mensaje = 'El producto ya existe'
end

--ELIMINAR PRODUCTO
create procedure sp_EliminarProducto(
@IdProducto int,
@Mensaje varchar(500) output,
@Resultado bit output
)
as
begin
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM detalle_venta 
					inner join producto on prod_id = deta_prod_id
					where prod_id = @IdProducto)
	begin
    	delete top (1) from producto where prod_id = @IdProducto
    	SET @Resultado = 1
	end
	else
    	set @Mensaje = 'El producto se encuentra relacionado a una venta'
end

select prod_id, prod_nombre, prod_descripcion, prod_marc_id, marc_descripcion, prod_cate_id, cate_descripcion, prod_precio, prod_stock, prod_rutaImagen, prod_nombreImagen, prod_activo from producto
inner join marca on marc_id = prod_marc_id
inner join categoria on cate_id = prod_cate_id



--REGISTRAR CLIENTE
create procedure sp_RegistrarCliente(
@Nombre varchar(100),
@Descripcion varchar(100),
@Correo varchar(100),
@Clave varchar(100),
@Mensaje varchar(100) output,
@Resultado bit output

)
as
begin
	SET @Resultado = 0
		if not exists (select * from CLIENTE where clie_correo = @Correo)
begin
insert into CLIENTE(clie_nombre, clie_apellidos, clie_correo, clie_clave, clie_reestablecer)
values
(@Nombre,@Apellidos,@Correo,@Clave,0)

	set @Resultado = scope_identity()
end
	else
	set @Mensaje = 'El correo del usuario ya existe'
end
