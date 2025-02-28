using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class LaboratorioDAL:CadenaDAL
    {

        public List<LaboratorioCLS> listarLaboratorios()
        {
            List<LaboratorioCLS> lista = new List<LaboratorioCLS>();

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspListarLaboratorio", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                lista.Add(new LaboratorioCLS
                                {
                                    idLaboratorio = dr.GetInt32(0),
                                    nombre = dr.IsDBNull(1) ? null : dr.GetString(1),
                                    direccion = dr.IsDBNull(2) ? null : dr.GetString(2),
                                    
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en listarLaboratorios: {ex.Message}");
                    lista = new List<LaboratorioCLS>(); 
                }
            }
            return lista;
        }


        public List<LaboratorioCLS> filtrarLaboratorios(LaboratorioCLS obj)
        {
            List<LaboratorioCLS> lista = new List<LaboratorioCLS>();

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspFiltrarLaboratorio", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nombre", (object)obj.nombre ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@direccion", (object)obj.direccion ?? DBNull.Value);
                        

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                lista.Add(new LaboratorioCLS
                                {
                                    idLaboratorio = dr.GetInt32(0),
                                    nombre = dr.IsDBNull(1) ? null : dr.GetString(1),
                                    direccion = dr.IsDBNull(2) ? null : dr.GetString(2),
                                    
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en filtrarLaboratorios: {ex.Message}");
                    lista = new List<LaboratorioCLS>(); // Devolver lista vacía en vez de null
                }
            }
            return lista;
        }
        public int GuardarLaboratorio(LaboratorioCLS obj)
        {
            int rpta = 0;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Laboratorio (NOMBRE, DIRECCION, BHABILITADO) VALUES (@nombre, @direccion, 1);", cn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@nombre", (object)obj.nombre ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@direccion", (object)obj.direccion ?? DBNull.Value);
                        
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

        public LaboratorioCLS recuperarLaboratorio(int idLaboratorio)
        {
            LaboratorioCLS oLaboratorioCLS = null;

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT IIDLABORATORIO, NOMBRE, DIRECCION FROM Laboratorio WHERE BHABILITADO = 1 AND IIDLABORATORIO = @idLaboratorio", cn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@idLaboratorio", idLaboratorio);
                        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleResult);

                        if (dr.Read())
                        {
                            oLaboratorioCLS = new LaboratorioCLS
                            {
                                idLaboratorio = dr.GetInt32(0),
                                nombre = dr.IsDBNull(1) ? "" : dr.GetString(1),
                                direccion = dr.IsDBNull(2) ? "" : dr.GetString(2),                                
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    oLaboratorioCLS = null;
                }
            }
            return oLaboratorioCLS;
        }

        public int GuardarCambiosLaboratorio(LaboratorioCLS obj)
        {
            int rpta = 0;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE Laboratorio SET NOMBRE = @nombre, DIRECCION = @direccion WHERE IIDLABORATORIO = @idLaboratorio", cn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@nombre", (object)obj.nombre ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@direccion", (object)obj.direccion ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@idLaboratorio", obj.idLaboratorio);

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

        public int EliminarLaboratorio(int id)
        {
            int rpta = 0;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Laboratorio WHERE IIDLABORATORIO = @id", cn))
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
