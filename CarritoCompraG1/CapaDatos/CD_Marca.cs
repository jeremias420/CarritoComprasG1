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
    public class CD_Marca
    {
        public List<Marca> Listar()
        {
            List<Marca> lista = new List<Marca>();

            try
            {
                using (SqlConnection oConexiones = new SqlConnection(Conexiones.cn))
                {
                    string query = "select marc_id,marc_descripcion,marc_activo from marca";


                    SqlCommand cmd = new SqlCommand(query, oConexiones);
                    cmd.CommandType = CommandType.Text;

                    oConexiones.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Marca()
                            {
                                marc_id = Convert.ToInt32(dr["marc_id"]),
                                marc_descripcion = dr["marc_descripcion"].ToString(),
                                marc_activo = Convert.ToBoolean(dr["marc_activo"])
                            });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Marca>();
            }
            return lista;
        }

        public int Registrar(Marca obj, out string Mensaje)
        {
            int idautogenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexiones = new SqlConnection(Conexiones.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarMarca", oConexiones);
                    cmd.Parameters.AddWithValue("Descripcion", obj.marc_descripcion);
                    cmd.Parameters.AddWithValue("Activo", obj.marc_activo);
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

        public bool Editar(Marca obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexiones = new SqlConnection(Conexiones.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarMarca", oConexiones);
                    cmd.Parameters.AddWithValue("IdMarca", obj.marc_id);
                    cmd.Parameters.AddWithValue("Descripcion", obj.marc_descripcion);
                    cmd.Parameters.AddWithValue("Activo", obj.marc_activo);
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

        public bool Eliminar(int id, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexiones.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarMarca", oConexion);
                    cmd.Parameters.AddWithValue("IdMarca", id);
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
