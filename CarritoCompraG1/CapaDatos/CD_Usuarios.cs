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
    public class CD_Usuarios
    {
        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();

            try
            {
                using (SqlConnection oConexiones = new SqlConnection(Conexiones.cn))
                {
                    string query = "select usua_id, usua_nombre, usua_apellido, usua_correo, usua_clave, usua_restablecer, usua_activo from usuario";

                    SqlCommand cmd = new SqlCommand(query, oConexiones);
                    cmd.CommandType = CommandType.Text;

                    oConexiones.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new Usuario()
                                {
                                    usua_id = Convert.ToInt32(dr["usua_id"]),
                                    usua_nombre = dr["usua_nombre"].ToString(),
                                    usua_apellido = dr["usua_apellido"].ToString(),
                                    usua_correo = dr["usua_correo"].ToString(),
                                    usua_clave = dr["usua_clave"].ToString(),
                                    usua_restablecer = Convert.ToBoolean(dr["usua_restablecer"]),
                                    usua_activo = Convert.ToBoolean(dr["usua_activo"])
                                }
                                );
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Usuario>();
            }
            return lista;
        }
        public int Registrar(Usuario obj, out string Mensaje)
        {
            int idautogenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexiones = new SqlConnection(Conexiones.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarUsuario", oConexiones);
                    cmd.Parameters.AddWithValue("Nombres", obj.usua_nombre);
                    cmd.Parameters.AddWithValue("Apellidos", obj.usua_apellido);
                    cmd.Parameters.AddWithValue("Correo", obj.usua_correo);
                    cmd.Parameters.AddWithValue("Clave", obj.usua_clave);
                    cmd.Parameters.AddWithValue("Activo", obj.usua_activo);
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

        public bool Editar(Usuario obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexiones.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarUsuario", oConexion);
                    cmd.Parameters.AddWithValue("IdUsuario", obj.usua_id);
                    cmd.Parameters.AddWithValue("Nombres", obj.usua_nombre);
                    cmd.Parameters.AddWithValue("Apellidos", obj.usua_apellido);
                    cmd.Parameters.AddWithValue("Correo", obj.usua_correo);
                    cmd.Parameters.AddWithValue("Activo", obj.usua_activo);
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
                    SqlCommand cmd = new SqlCommand("delete top (1) from usuario where IdUsuario = @id", oConexion);
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

        public bool CambiarClave(int idusuario, string nuevaclave, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexiones.cn))
                {
                    SqlCommand cmd = new SqlCommand("update usuario set usua_clave = @nuevaclave , reestablecer = 0 where idusuario = @id", oConexion);
                    cmd.Parameters.AddWithValue("@id", idusuario);
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

        public bool ReestablecerClave(int idusuario, string clave, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexiones.cn))
                {
                    SqlCommand cmd = new SqlCommand("update usuario set usua_clave = @clave , reestablecer = 1 where idusuario = @id", oConexion);
                    cmd.Parameters.AddWithValue("@id", idusuario);
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
