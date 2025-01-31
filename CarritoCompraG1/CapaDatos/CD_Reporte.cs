﻿using System;
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
    public class CD_Reporte
    {

        public List<Reporte> Ventas(string fechainicio, string fechafin, string idtransaccion)
        {
            List<Reporte> lista = new List<Reporte>();

            try
            {
                using (SqlConnection oConexiones = new SqlConnection(Conexiones.cn))
                {

                    SqlCommand cmd = new SqlCommand("sp_ReporteVentas", oConexiones);
                    cmd.Parameters.AddWithValue("fechainicio", fechainicio);
                    cmd.Parameters.AddWithValue("fechafin", fechafin);
                    cmd.Parameters.AddWithValue("idtransaccion", idtransaccion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexiones.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new Reporte()
                                {
                                    FechaVenta = dr["FechaVenta"].ToString(),
                                    Cliente = dr["Cliente"].ToString(),
                                    Producto = dr["Producto"].ToString(),
                                    Precio = Convert.ToDecimal(dr["Precio"], new CultureInfo("es-AR")),
                                    Cantidad = Convert.ToInt32(dr["Cantidad"].ToString()),
                                    Total = Convert.ToDecimal(dr["Total"], new CultureInfo("es-AR")),
                                    IdTransaccion = dr["IdTransaccion"].ToString(),
                                });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Reporte>();
            }
            return lista;
        }
        public DashBoard verDashBoard()
        {
            DashBoard objeto = new DashBoard();

            try
            {
                using (SqlConnection oConexiones = new SqlConnection(Conexiones.cn))
                {

                    SqlCommand cmd = new SqlCommand("sp_ReporteDashboard", oConexiones);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexiones.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            objeto = new DashBoard()
                            {
                                TotalCliente = Convert.ToInt32(dr["TotalCliente"]),
                                TotalVenta = Convert.ToInt32(dr["TotalVenta"]),
                                TotalProducto = Convert.ToInt32(dr["TotalProducto"]),
                            };
                        }
                    }
                }
            }
            catch
            {
                objeto = new DashBoard();
            }
            return objeto;
        }
    }
}
