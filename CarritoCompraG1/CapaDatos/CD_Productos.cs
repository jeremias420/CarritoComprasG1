using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaEntidad;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace CapaDatos
{
    public class CD_Productos
    {
        public List<Producto> Listar()
        {
            List<Producto> lista = new List<Producto>();

            try
            {
                using (SqlConnection oConexiones = new SqlConnection(Conexiones.cn))
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine("select prod_id, prod_nombre, prod_descripcion, prod_marc_id, marc_descripcion, prod_cate_id, cate_descripcion, prod_precio, prod_stock, prod_rutaImagen, prod_nombreImagen, prod_activo from producto");
                    sb.AppendLine("inner join marca on marc_id = prod_marc_id");
                    sb.AppendLine("inner join categoria on cate_id = prod_cate_id");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), oConexiones);
                    cmd.CommandType = CommandType.Text;

                    oConexiones.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Producto()
                            {
                                prod_id = Convert.ToInt32(dr["prod_id"]),
                                prod_nombre = dr["prod_nombre"].ToString(),
                                prod_descripcion = dr["prod_descripcion"].ToString(),
                                oMarca = new Marca() { marc_id = Convert.ToInt32(dr["prod_marc_id"]), marc_descripcion = dr["marc_descripcion"].ToString() },
                                oCategoria = new Categoria() { cate_id = Convert.ToInt32(dr["prod_cate_id"]), cate_descripcion = dr["cate_descripcion"].ToString() },
                                prod_precio = Convert.ToDecimal(dr["prod_precio"], new CultureInfo("es-AR")),
                                prod_stock = Convert.ToInt32(dr["prod_stock"]),
                                prod_rutaImagen = dr["prod_rutaImagen"].ToString(),
                                prod_nombreImagen = dr["prod_nombreImagen"].ToString(),
                                prod_activo = Convert.ToBoolean(dr["prod_activo"])
                            });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Producto>();
            }
            return lista;
        }

        public int Registrar(Producto obj, out string Mensaje)
        {
            int idautogenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexiones = new SqlConnection(Conexiones.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarProducto", oConexiones);
                    cmd.Parameters.AddWithValue("Nombre", obj.prod_nombre);
                    cmd.Parameters.AddWithValue("Descripcion", obj.prod_descripcion);
                    cmd.Parameters.AddWithValue("IdMarca", obj.oMarca.marc_id);
                    cmd.Parameters.AddWithValue("IdCategoria", obj.oCategoria.cate_id);
                    cmd.Parameters.AddWithValue("Precio", obj.prod_precio);
                    cmd.Parameters.AddWithValue("Stock", obj.prod_stock);
                    cmd.Parameters.AddWithValue("Activo", obj.prod_activo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexiones.Open();

                    cmd.ExecuteNonQuery();

                    idautogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idautogenerado = 0;
                Mensaje = ex.Message;
            }
            return idautogenerado;
        }

        public bool Editar(Producto obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexiones = new SqlConnection(Conexiones.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarProducto", oConexiones);
                    cmd.Parameters.AddWithValue("IdProducto", obj.prod_id);
                    cmd.Parameters.AddWithValue("Nombre", obj.prod_nombre);
                    cmd.Parameters.AddWithValue("Descripcion", obj.prod_descripcion);
                    cmd.Parameters.AddWithValue("IdMarca", obj.oMarca.marc_id);
                    cmd.Parameters.AddWithValue("IdCategoria", obj.oCategoria.cate_id);
                    cmd.Parameters.AddWithValue("Precio", obj.prod_precio);
                    cmd.Parameters.AddWithValue("Stock", obj.prod_stock);
                    cmd.Parameters.AddWithValue("Activo", obj.prod_activo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexiones.Open();

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }

        public bool GuardarDatosImagen(Producto obj, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexiones = new SqlConnection(Conexiones.cn))
                {
                    string query = "update producto set prod_rutaImagen = @rutaimagen, prod_nombreImagen = @nombreimagen where prod_id = @IdProducto";

                    SqlCommand cmd = new SqlCommand("sp_RegistrarProducto", oConexiones);
                    cmd.Parameters.AddWithValue("@rutaimagen", obj.prod_rutaImagen);
                    cmd.Parameters.AddWithValue("@nombreimagen", obj.prod_nombreImagen);
                    cmd.Parameters.AddWithValue("@IdProducto", obj.prod_id);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexiones.Open();

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        resultado = true;
                    }
                    else
                    {
                        mensaje = "No se pudo actualizar la imagen";
                    }
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;
            }
            return resultado;
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexiones.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarProducto", oConexion);
                    cmd.Parameters.AddWithValue("IdProducto", id);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }

    }
}
