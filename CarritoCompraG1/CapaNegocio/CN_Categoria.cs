﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Categoria{

        private CN_Categoria objCapaDato = new CN_Categoria();

        public List<Categoria> Listar()
        {
            return objCapaDato.Listar();
        }

        public int Registrar(Categoria obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.cate_descripcion) || string.IsNullOrWhiteSpace(obj.cate_descripcion))
            {
                Mensaje = "La descripciom no puede estar vacío";
            }
 

            if (string.IsNullOrEmpty(Mensaje))
            {

                if (respuesta)
                {
                    return objCapaDato.Registrar(obj, out Mensaje);
                }
                else
                {
                    return 0;
                }


            }
            else
            {
                return 0;
            }
        }

        public bool Editar(Categoria obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.cate_descripcion) || string.IsNullOrWhiteSpace(obj.cate_descripcion))
            {
                Mensaje = "La descripcion no puede estar vacío";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDato.Editar(obj, out Mensaje);
            }
            else
            {
                return false;
            }

            public bool Eliminar(int id, out string Mensaje)
            {
                return objCapaDato.Eliminar(id, out Mensaje);
            }
        }

    }
}
