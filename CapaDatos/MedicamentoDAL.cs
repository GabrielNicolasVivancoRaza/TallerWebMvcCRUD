using CapaDatos;
using CapaEntidad;
using System.Data.SqlClient;
using System.Data;

public class MedicamentoDAL : CadenaDAL
{
    public List<MedicamentoCLS> ListarMedicamentos()
    {
        List<MedicamentoCLS> lista = new List<MedicamentoCLS>();
        using (SqlConnection cn = new SqlConnection(cadena))
        {
            try
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("uspListarMedicamento", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new MedicamentoCLS
                            {
                                IdMedicamento = dr.GetInt32(dr.GetOrdinal("IIDMEDICAMENTO")),
                                Nombre = dr.GetString(dr.GetOrdinal("NOMBREMEDICAMENTO")),
                                IdLaboratorio = 0, // No está en la consulta, asignamos 0 o null
                                NombreLaboratorio = dr.GetString(dr.GetOrdinal("NOMBRELABOTARIO")), // CORREGIDO
                                IdTipoMedicamento = 0, // No está en la consulta, asignamos 0 o null
                                NombreTipoMedicamento = dr.GetString(dr.GetOrdinal("NOMBRETIPO")),
                                Habilitado = true
                            });


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
}