using ConnectionManagement.Data;
using System.Data;
using SalesDatePrediction.Api.DTOs;


namespace SalesDatePrediction.Api.Prediction
{

    public class SalesPredictionTask
    {
        public string ConectionString { get; set; }
        public SalesPredictionTask(string conectionString)
        {
            ConectionString = conectionString;
        }

        public async Task<List<CustomerOrderDto>> GetCustomerOrders()
        {
            List<CustomerOrderDto> result = new List<CustomerOrderDto>();
            Connection db = new Connection(ConectionString);
            db.TiempoEsperaComando = 0;
            try
            {
                db.SQLParametros.Clear();
                db.TiempoEsperaComando = 0;
                var reader = await db.EjecutarReaderAsync("GetCustomerOrders", CommandType.StoredProcedure);
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        var Dto = new CustomerOrderDto();
                        Dto.CustId = Convert.ToInt32(reader["CustId"]);
                        if (!Convert.IsDBNull(reader["CustomerName"])) { Dto.CustomerName = reader["CustomerName"].ToString(); }
                        if (!Convert.IsDBNull(reader["LastOrderDate"])) { Dto.LastOrderDate = DateTime.Parse(reader["LastOrderDate"].ToString()); }
                        if (!Convert.IsDBNull(reader["NextPredictedOrder"])) { Dto.NextPredictedOrder = DateTime.Parse(reader["NextPredictedOrder"].ToString()); }

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
