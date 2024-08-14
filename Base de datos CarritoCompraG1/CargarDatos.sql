insert into usuario(usua_nombre, usua_apellido, usua_correo, usua_clave)
values
('Juan', 'Rodriguez', 'juanrodriguez@gmail.com', 'juanrodriguez11'),
('Roberto', 'Gutierrez', 'robergutierrez@gmail.com', 'robertoguti99')

select * from usuario

insert into categoria(cate_descripcion)
values
('Accesorios'),
('Indumentaria'),
('Calzado')

select * from categoria

insert into marca(marc_descripcion)
values
('Nike'),
('Adidas'),
('Puma')

select * from marca

insert into departamento(depa_descripcion, depa_prov_id)
values
('Gualeguaychu', 1),
('Boca del bermejo', 2),
('Rio chico', 3)


select * from departamento

insert into provincia(prov_descripcion)
values
('Entre Rios'),
('Chaco'),
('Tucuman')

select * from provincia

insert into ciudad(ciud_descripcion, ciud_prov_id, ciud_depa_id)
values
('Gualeguaychu', 1, 1),
('Las Palmas',2, 2),
('El espinillo',3, 3)

select * from ciudad



