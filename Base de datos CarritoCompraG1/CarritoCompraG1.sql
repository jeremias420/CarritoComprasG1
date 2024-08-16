create database CarritoCompraG1

GO

use CarritoCompraG1

GO

create table categoria(
cate_id int primary key identity,
cate_descripcion varchar (100),
cate_activo bit default 1,
cate_fechaRegistro datetime default getdate(),
)

go

select * from categoria


create table marca(
marc_id int primary key identity,
marc_descripcion varchar (100),
marc_activo bit default 1,
marc_fechaRegistro datetime default getdate(),
)

go

create table producto(
prod_id int primary key identity,
prod_nombre varchar (255),
prod_descripcion varchar (255),
prod_marc_id int references marca(marc_id),
prod_cate_id int references categoria(cate_id),
prod_precio decimal (10,2) default 0,
prod_stock int,
prod_rutaImagen varchar (100),
prod_nombreImagen varchar (100),
prod_activo bit default 1,
prod_fechaRegistro datetime default getdate(),
)

go

create table cliente(
clie_id int primary key identity,
clie_nombre varchar(100),
clie_apellido varchar(100),
clie_correo varchar(100),
clie_clave varchar(100),
clie_restablecer bit default 1,
clie_fechaRegistro datetime default getdate(),
)

go


create table carrito(
carr_id int primary key identity,
carr_clie_id int references cliente(clie_id),
carr_prod_id int references producto(prod_id),
carr_cantidad int
)

go

create table venta(
vent_id int primary key identity,
vent_clie_id int references cliente(clie_id),
vent_totalProducto int,
vent_montoTotal decimal(10,2),
vent_contacto varchar(50),
vent_ciud_id int,
vent_telefono varchar(50),
vent_direccion varchar(500),
vent_tran_id int,
vent_fechaVenta datetime default getdate()
)

go


create table detalle_venta(
deta_id int primary key identity,
deta_vent_id int references venta(vent_id),
deta_prod_id int references producto(prod_id),
deta_cantidad int,
deta_total decimal(10,2)
)

go

create table usuario(
usua_id int primary key identity,
usua_nombre varchar(100),
usua_apellido varchar(100),
usua_correo varchar(100),
usua_clave varchar(100),
usua_restablecer bit default 1,
usua_activo bit default 1,
usua_fechaRegistro datetime default getdate(),
)

go


create table departamento(
depa_id int primary key identity not null,
depa_descripcion varchar(45) not null,
depa_prov_id int not null
)

go


create table provincia(
prov_id int primary key identity not null,
prov_descripcion varchar(45) not null,
)

go


create table ciudad(
ciud_id int primary key identity not null,
ciud_descripcion varchar(45) not null,
ciud_prov_id int not null,
ciud_depa_id int not null,
)

go


