using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaEntidad;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_Cliente
    {
        public List<Cliente> Listar()
        {
            List<Cliente> lista = new List<Cliente>();

            try
            {
                using (SqlConnection oConexiones = new SqlConnection(Conexiones.cn))
                {
                    string query = "select clie_id, clie_nombre, clie_apellido, clie_correo, clie_clave, clie_restablecer  from Cliente";

                    SqlCommand cmd = new SqlCommand(query, oConexiones);
                    cmd.CommandType = CommandType.Text;

                    oConexiones.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new Cliente()
                                {
                                    clie_id = Convert.ToInt32(dr["clie_id"]),
                                    clie_nombre = dr["clie_nombre"].ToString(),
                                    clie_apellido = dr["clie_apellido"].ToString(),
                                    clie_correo = dr["clie_correo"].ToString(),
                                    clie_clave = dr["clie_clave"].ToString(),
                                    clie_restablecer = Convert.ToBoolean(dr["clie_restablecer"]),
                                    
                                }
                                );
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Cliente>();
            }
            return lista;
        }
        public int Registrar(Cliente obj, out string Mensaje)
        {
            int idautogenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexiones = new SqlConnection(Conexiones.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarCliente", oConexiones);
                    cmd.Parameters.AddWithValue("Nombres", obj.clie_nombre);
                    cmd.Parameters.AddWithValue("Apellidos", obj.clie_apellido);
                    cmd.Parameters.AddWithValue("Correo", obj.clie_correo);
                    cmd.Parameters.AddWithValue("Clave", obj.clie_clave);
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

        public bool Editar(Cliente obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexiones.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarCliente", oConexion);
                    cmd.Parameters.AddWithValue("IdCliente", obj.clie_id);
                    cmd.Parameters.AddWithValue("Nombres", obj.clie_nombre);
                    cmd.Parameters.AddWithValue("Apellidos", obj.clie_apellido);
                    cmd.Parameters.AddWithValue("Correo", obj.clie_correo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
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

        public bool Eliminar(int id, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexiones.cn))
                {
                    SqlCommand cmd = new SqlCommand("delete top (1) from Cliente where IdCliente = @id", oConexion);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();
                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }

        public bool CambiarClave(int idCliente, string nuevaclave, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexiones.cn))
                {
                    SqlCommand cmd = new SqlCommand("update Cliente set clie_clave = @nuevaclave , reestablecer = 0 where idCliente = @id", oConexion);
                    cmd.Parameters.AddWithValue("@id", idCliente);
                    cmd.Parameters.AddWithValue("@nuevaClave", nuevaclave);
                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();
                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }

        public bool ReestablecerClave(int idCliente, string clave, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexiones.cn))
                {
                    SqlCommand cmd = new SqlCommand("update Cliente set clie_clave = @clave , reestablecer = 1 where idCliente = @id", oConexion);
                    cmd.Parameters.AddWithValue("@id", idCliente);
                    cmd.Parameters.AddWithValue("@clave", clave);
                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();
                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false;
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
