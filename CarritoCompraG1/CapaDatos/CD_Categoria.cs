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
    public class CD_Categoria {

        public List<Categoria> Listar()
        {
            List<Categoria> lista = new List<Categoria>();

            try
            {
                using (SqlConnection oConexiones = new SqlConnection(Conexiones.cn))
                {
                    string query = "select cate_id,cate_descripcion,cate_activo from categoria";


                    SqlCommand cmd = new SqlCommand(query, oConexiones);
                    cmd.CommandType = CommandType.Text;

                    oConexiones.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Categoria()
                                {
                                    cate_id = Convert.ToInt32(dr["cate_id"]),
                                    cate_descripcion = dr["cate_descripcion"].ToString(),
                                    cate_activo = Convert.ToBoolean(dr["cate_activo"])
                            }
                                );
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Categoria>();
            }
            return lista;
        }

        public int Registrar(Categoria obj, out string Mensaje)
        {
            int idautogenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexiones = new SqlConnection(Conexiones.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarCategoria", oConexiones);
                    cmd.Parameters.AddWithValue("Descripcion", obj.cate_descripcion);
                    cmd.Parameters.AddWithValue("Activo", obj.cate_activo);
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

        public bool Editar(Categoria obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexiones = new SqlConnection(Conexiones.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarCategoria", oConexiones);
                    cmd.Parameters.AddWithValue("Idcategoria", obj.cate_id);
                    cmd.Parameters.AddWithValue("Descripcion", obj.cate_descripcion);
                    cmd.Parameters.AddWithValue("Activo", obj.cate_activo);
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
                    SqlCommand cmd = new SqlCommand("sp_EliminarCategoria", oConexion);
                    cmd.Parameters.AddWithValue("Idcategoria", id);
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
