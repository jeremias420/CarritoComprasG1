using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Producto{


            private CN_Producto objCapaDato = new CN_Producto();

            public List<Producto> Listar()
            {
                return objCapaDato.Listar();
            }

            public int Registrar(Producto obj, out string Mensaje)
            {
                Mensaje = string.Empty;

                 if (string.IsNullOrEmpty(obj.prod_nombre) || string.IsNullOrWhiteSpace(obj.prod_nombre))
                {
                    Mensaje = "El nombre del producto no puede ser vacio";
                }
                else if (string.IsNullOrEmpty(obj.prod_descripcion) || string.IsNullOrWhiteSpace(obj.prod_descripcion))
                {
                Mensaje = "La descripcion del no puede ser vacio";
                }
                else if (obj.oMarca.marc_id == 0)
                {
                Mensaje = "Debe seleccionar una marca ";
                }
                else if (obj.oCategoria.cate_id == 0)
                {
                Mensaje = "Debe seleccionar una categoria";
                }
                else if (obj.prod_precio == 0)
                {
                Mensaje = "Debe ingresar el precio del producto";
                }
                else if (obj.prod_stock == 0)
                {
                Mensaje = "Debe ingresar el stock del producto";
                }


            if (string.IsNullOrEmpty(Mensaje))
                {
                    return objCapaDato.Registrar(obj, out Mensaje);
                }
                else
                {
                    return 0;
                }
            }

            public bool Editar(Producto obj, out string Mensaje)
            {
                Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.prod_nombre) || string.IsNullOrWhiteSpace(obj.prod_nombre))
            {
                Mensaje = "El nombre del producto no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.prod_descripcion) || string.IsNullOrWhiteSpace(obj.prod_descripcion))
            {
                Mensaje = "La descripcion del no puede ser vacio";
            }
            else if (obj.oMarca.marc_id == 0)
            {
                Mensaje = "Debe seleccionar una marca ";
            }
            else if (obj.oCategoria.cate_id == 0)
            {
                Mensaje = "Debe seleccionar una categoria";
            }
            else if (obj.prod_precio == 0)
            {
                Mensaje = "Debe ingresar el precio del producto";
            }
            else if (obj.prod_stock == 0)
            {
                Mensaje = "Debe ingresar el stock del producto";
            }

            if (string.IsNullOrEmpty(Mensaje))
                {
                    return objCapaDato.Editar(obj, out Mensaje);
                }
                else
                {
                    return false;
                }
            }
            public bool Eliminar(int id, out string Mensaje)
            {
                return objCapaDato.Eliminar(id, out Mensaje);
            }

        public bool GuardarDatosImagen(Producto obj, out string mensaje){

            return objCapaDato.GuardarDatosImagen(obj, out mensaje);

        }

        }
  }
