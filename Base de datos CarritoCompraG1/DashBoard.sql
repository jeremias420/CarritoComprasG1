select * from producto

create procedure sp_ReporteDashboard
as
begin

select

(select count(*) from cliente) [TotalCliente],
(select isnull(sum(deta_cantidad), 0) from detalle_venta) [TotalVenta],
(select count(*) from producto) [TotalProducto]

end

exec sp_ReporteDashboard

select * from venta

create procedure sp_ReporteVentas(
@fechainicio varchar(10),
@fechafin varchar(10),
@idtransaccion varchar(50)
)
as
begin

set dateformat dmy;

select convert(char (10),vent_fechaVenta,103)[FechaVenta], concat(clie_nombre,' ' ,clie_apellido)[Cliente], prod_nombre[Producto], prod_precio, deta_cantidad, deta_total, vent_tran_id from detalle_venta
inner join producto on prod_id = deta_prod_id
inner join venta on vent_id = deta_vent_id
inner join cliente on clie_id = vent_clie_id
where convert (date, vent_fechaVenta) between @fechainicio and @fechafin

and vent_tran_id = iif(@idtransaccion = '', vent_tran_id,@idtransaccion)

end