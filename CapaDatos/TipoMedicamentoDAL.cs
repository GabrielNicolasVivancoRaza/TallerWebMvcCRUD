using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class TipoMedicamentoDAL : CadenaDAL
    {
        public List<TipoMedicamentoCLS> listarTipoMedicamento()
        {
            List<TipoMedicamentoCLS> lista = new List<TipoMedicamentoCLS>();

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspListarTipoMedicamento", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                TipoMedicamentoCLS tipoMedicamento = new TipoMedicamentoCLS
                                {
                                    idTipoMedicamento = dr.GetInt32(0),
                                    nombre = dr.IsDBNull(1) ? string.Empty : dr.GetString(1),
                                    descripcion = dr.IsDBNull(2) ? string.Empty : dr.GetString(2)
                                };
                                lista.Add(tipoMedicamento);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    lista = null;
                    throw;
                }
            }
            return lista;
        }

        public List<TipoMedicamentoCLS> filtrarTipoMedicamento(TipoMedicamentoCLS obj)
        {
            List<TipoMedicamentoCLS> lista = new List<TipoMedicamentoCLS>();

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspFiltrarTipoMedicamento", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nombretipomedicamento", (object)obj.nombre ?? "");

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                TipoMedicamentoCLS tipoMedicamento = new TipoMedicamentoCLS
                                {
                                    idTipoMedicamento = dr.GetInt32(0),
                                    nombre = dr.GetString(1),
                                    descripcion = dr.IsDBNull(2) ? "" : dr.GetString(2)
                                };
                                lista.Add(tipoMedicamento);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    lista = null;
                    throw;
                }
            }
            return lista;
        }

        public int GuardarTipoMedicamento(TipoMedicamentoCLS obj)
        {
            int rpta = 0;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO TipoMedicamento (NOMBRE, DESCRIPCION, BHABILITADO) VALUES (@nombre, @descripcion, 1);", cn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@nombre", (object)obj.nombre ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@descripcion", (object)obj.descripcion ?? DBNull.Value);
                        rpta = cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception)
                {
                    rpta = 0;
                    throw;
                }
            }
            return rpta;
        }

        public TipoMedicamentoCLS recuperarTipoMedicamento(int idTipoMedicamento)
        {
            TipoMedicamentoCLS oTipoMedicamentoCLS = null;

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT IIDTIPOMEDICAMENTO, NOMBRE, DESCRIPCION FROM TipoMedicamento WHERE BHABILITADO = 1 AND IIDTIPOMEDICAMENTO = @iidtipomedicamento", cn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@iidtipomedicamento", idTipoMedicamento);
                        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleResult);

                        if (dr.Read()) // ← Esto revisa si hay datos antes de intentar leer
                        {
                            oTipoMedicamentoCLS = new TipoMedicamentoCLS
                            {
                                idTipoMedicamento = dr.GetInt32(0),
                                nombre = dr.IsDBNull(1) ? "" : dr.GetString(1),
                                descripcion = dr.IsDBNull(2) ? "" : dr.GetString(2)
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    oTipoMedicamentoCLS = null;
                }
            }
            return oTipoMedicamentoCLS;
        }


        public int GuardarCambiosTipoMedicamento(TipoMedicamentoCLS obj)
        {
            int rpta = 0;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE TipoMedicamento SET NOMBRE = @nombre, DESCRIPCION = @descripcion WHERE IIDTIPOMEDICAMENTO = @idTipoMedicamento", cn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                        cmd.Parameters.AddWithValue("@descripcion", obj.descripcion);
                        cmd.Parameters.AddWithValue("@idTipoMedicamento", obj.idTipoMedicamento);

                        rpta = cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception)
                {
                    rpta = 0;
                    throw;
                }
            }
            return rpta;
        }
        public int EliminarTipoMedicamento(int id)
        {
            int rpta = 0;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Medicamento WHERE IIDTIPOMEDICAMENTO = @id; DELETE FROM TipoMedicamento WHERE IIDTIPOMEDICAMENTO = @id;", cn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@id", id);
                        rpta = cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception)
                {
                    rpta = 0;
                    throw;
                }
            }
            return rpta;
        }

    }
}