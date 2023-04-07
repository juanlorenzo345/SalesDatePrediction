using ConnectionManagement.Data;
using System.Data;
using SalesDatePrediction.Api.DTOs;
using static NuGet.Packaging.PackagingConstants;
using SalesDatePrediction.Api.Task.Filter;

namespace SalesDatePrediction.Api.Orders
{

    public class EmployeesTask
    {
        public string ConectionString { get; set; }
        public EmployeesTask(string conectionString)
        {
            ConectionString = conectionString;
        }

        public async Task<List<EmployeesDto>> GetEmployees()
        {
            List<EmployeesDto> result = new();
            Connection db = new(ConectionString);
            db.TiempoEsperaComando = 0;
            try
            {
                db.TiempoEsperaComando = 0;
                var reader = await db.EjecutarReaderAsync("GetEmployees", CommandType.StoredProcedure);
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        var Dto = new EmployeesDto();
                        Dto.Empid = Convert.ToInt32(reader["Empid"]);
                        if (!Convert.IsDBNull(reader["FullName"])) { Dto.FullName = reader["FullName"].ToString(); }

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
