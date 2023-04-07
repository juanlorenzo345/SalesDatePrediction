using ConnectionManagement.Data;
using System.Data;
using SalesDatePrediction.Api.DTOs;
using static NuGet.Packaging.PackagingConstants;
using SalesDatePrediction.Api.Task.Filter;

namespace SalesDatePrediction.Api.Orders
{

    public class ShippersTask
    {
        public string ConectionString { get; set; }
        public ShippersTask(string conectionString)
        {
            ConectionString = conectionString;
        }

        public async Task<List<ShippersDto>> GetShippers()
        {
            List<ShippersDto> result = new();
            Connection db = new(ConectionString);
            db.TiempoEsperaComando = 0;
            try
            {
                db.TiempoEsperaComando = 0;
                var reader = await db.EjecutarReaderAsync("GetShippers", CommandType.StoredProcedure);
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        var Dto = new ShippersDto();

                        Dto.ShipperID = Convert.ToInt32(reader["ShipperID"]);
                        if (!Convert.IsDBNull(reader["CompanyName"])) { Dto.CompanyName = reader["CompanyName"].ToString(); }

                        result.Add(Dto);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Se genero un error al consultar los registros: " + ex.Message);
            }
            finally { }
            if (db != null)
            {
                db.Dispose();
            }

            return result;
        }
    }

}
