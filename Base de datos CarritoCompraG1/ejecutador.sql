select usua_id, usua_nombre, usua_apellido, usua_correo, usua_clave, usua_restablecer, usua_activo from usuario

select cate_id, cate_descripcion, cate_activo from categoria

update usuario set usua_activo = 0 where usua_id = 1