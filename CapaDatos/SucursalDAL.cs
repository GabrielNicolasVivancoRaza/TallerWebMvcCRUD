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
    public class SucursalDAL : CadenaDAL
    {



        public List<SucursalCLS> listarSucursales()
        {
            List<SucursalCLS> lista = new List<SucursalCLS>();

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspListarSucursal", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                SucursalCLS sucursal = new SucursalCLS
                                {
                                    idSucursal = dr.GetInt32(0),
                                    nombre = dr.GetString(1),
                                    direccion = dr.GetString(2)
                                };

                                lista.Add(sucursal);
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

        public List<SucursalCLS> filtrarSucursales(SucursalCLS obj)
        {
            List<SucursalCLS> lista = new List<SucursalCLS>();

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspFiltrarSucursal", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nombresucursal", (object)obj.nombre ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@direccion", (object)obj.direccion ?? DBNull.Value);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                SucursalCLS sucursal = new SucursalCLS
                                {
                                    idSucursal = dr.GetInt32(0),
                                    nombre = dr.GetString(1),
                                    direccion = dr.GetString(2)
                                };

                                lista.Add(sucursal);
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

        public int GuardarSucursal(SucursalCLS obj)
        {
            int rpta = 0;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Sucursal (NOMBRE, DIRECCION, BHABILITADO) VALUES (@nombre, @direccion, 1);", cn))
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

        public SucursalCLS recuperarSucursal(int idSucursal)
        {
            SucursalCLS oSucursalCLS = null;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT IIDSUCURSAL, NOMBRE, DIRECCION FROM Sucursal WHERE BHABILITADO = 1 AND IIDSUCURSAL = @idSucursal", cn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@idSucursal", idSucursal);
                        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleResult);

                        if (dr.Read())
                        {
                            oSucursalCLS = new SucursalCLS
                            {
                                idSucursal = dr.GetInt32(0),
                                nombre = dr.IsDBNull(1) ? "" : dr.GetString(1),
                                direccion = dr.IsDBNull(2) ? "" : dr.GetString(2)
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    oSucursalCLS = null;
                }
            }
            return oSucursalCLS;
        }

        public int GuardarCambiosSucursal(SucursalCLS obj)
        {
            int rpta = 0;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE Sucursal SET NOMBRE = @nombre, DIRECCION = @direccion WHERE IIDSUCURSAL = @idSucursal", cn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                        cmd.Parameters.AddWithValue("@direccion", obj.direccion);
                        cmd.Parameters.AddWithValue("@idSucursal", obj.idSucursal);

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
        public int EliminarSucursal(int id)
        {
            int rpta = 0;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Sucursal WHERE IIDSUCURSAL = @id", cn))
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
