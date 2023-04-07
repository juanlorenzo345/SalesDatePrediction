using ConnectionManagement.Data;
using System.Data;
using SalesDatePrediction.Api.DTOs;
using static NuGet.Packaging.PackagingConstants;
using SalesDatePrediction.Api.Task.Filter;

namespace SalesDatePrediction.Api.Orders
{

    public class ProductsTask
    {
        public string ConectionString { get; set; }
        public ProductsTask(string conectionString)
        {
            ConectionString = conectionString;
        }

        public async Task<List<ProductsDto>> GetProducts()
        {
            List<ProductsDto> result = new();
            Connection db = new(ConectionString);
            db.TiempoEsperaComando = 0;
            try
            {
                db.TiempoEsperaComando = 0;
                var reader = await db.EjecutarReaderAsync("GetProducts", CommandType.StoredProcedure);
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        var Dto = new ProductsDto();

                        Dto.ProductId = Convert.ToInt32(reader["ProductId"]);
                        if (!Convert.IsDBNull(reader["ProductName"])) { Dto.ProductName = reader["ProductName"].ToString(); }

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
