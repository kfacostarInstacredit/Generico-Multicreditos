using InstAPIGeneric.Model;
using InstAPIGeneric.Model.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace InstAPIGeneric.BL.BD
{
    /// <summary> Clase que define la ejecución de funciones debase de datos SQL server </summary>
    public class SQLBD
    {
        /// <summary> Método que retorna el resultado de procesar una consulta base de datos </summary>
        /// <param name="conexion">conexion del motor de SQL</param>
        /// <param name="procedimiento">caracteristicas del procedimiento almacenado a ejecutar</param>
        /// <param name="parametros">Valor de los parametros a ejecutar</param>
        /// <returns>Configuracion de un resultado del proceso</returns>
        public ResultadoDTO ResutlDataTable(Catalogo_Conexiones conexion, Catalogo_Procedimientos_Funciones procedimiento, List<Parametro> parametros)
        {
            DataTable dt = new DataTable();
            ResultadoDTO res = new ResultadoDTO();

            try
            {
                using (SqlConnection sqlcon = new SqlConnection(conexion.ConnectionString))
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand(procedimiento.Nombre, sqlcon))
                        {
                            foreach (var item in parametros)
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue(item.Nombre, item.Valor);
                            }
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                da.Fill(dt);
                            }

                            res.Nombre = procedimiento.ValorMostar;
                            if (dt.Rows.Count > 0)
                            {
                                switch (dt.Rows.Count)
                                {
                                    case 1:
                                        {
                                            res.Valor = dt.Rows[0].ItemArray;
                                            break;
                                        }
                                    default:
                                        {
                                            foreach (DataRow row in dt.Rows)
                                            {
                                                res.Valores.Add(row.ItemArray);
                                            }
                                            break;
                                        }
                                }
                            }
                            res.Codigo = 0;
                            res.Mensaje = "Finalizada con exito";
                        }
                    }
                    catch (Exception ex1) {
                        sqlcon.Close();
                        throw new Exception(ex1.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                res.Codigo = 1;
                res.Mensaje = ex.Message;
                res.Nombre = procedimiento.ValorMostar;
            }

            return res;
        }
    }
}
