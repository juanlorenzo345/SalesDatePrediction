using ConnectionManagement.Data;
using System.Data;
using SalesDatePrediction.Api.DTOs;
using static NuGet.Packaging.PackagingConstants;
using SalesDatePrediction.Api.Task.Filter;
using SalesDatePrediction.Api.Model;

namespace SalesDatePrediction.Api.Orders
{

    public class OrdersTask
    {
        private DBContext dbContext;

        public string ConectionString { get; set; }
        public OrdersTask(string conectionString)
        {
            ConectionString = conectionString;
        }

        public OrdersTask(DBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ClientOrderDTO>> GetOrdersByIdClient(int custid)
        {
            List<ClientOrderDTO> result = new();
            Connection db = new(ConectionString);
            db.TiempoEsperaComando = 0;
            try
            {
                db.SQLParametros.Clear();
                db.TiempoEsperaComando = 0;
                if (!Convert.IsDBNull(custid) && custid != 0) { db.SQLParametros.Add("@custid", SqlDbType.Int).Value = custid; }
                var reader = await db.EjecutarReaderAsync("GetOrdersByIdClient", CommandType.StoredProcedure);
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        var Dto = new ClientOrderDTO();

                        Dto.OrderId = Convert.ToInt32(reader["OrderID"]);
                        if (!Convert.IsDBNull(reader["RequiredDate"])) { Dto.RequiredDate = DateTime.Parse(reader["RequiredDate"].ToString()); }
                        if (!Convert.IsDBNull(reader["ShippedDate"])) { Dto.ShippedDate = DateTime.Parse(reader["ShippedDate"].ToString()); }
                        if (!Convert.IsDBNull(reader["ShipName"])) { Dto.ShipName = reader["ShipName"].ToString(); }
                        if (!Convert.IsDBNull(reader["ShipAddress"])) { Dto.ShipAddress = reader["ShipAddress"].ToString(); }
                        if (!Convert.IsDBNull(reader["ShipCity"])) { Dto.ShipCity = reader["ShipCity"].ToString(); }
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

        public async Task<List<ClientOrderDTO>> AddNewOrder(OrderDto order)
        {
            List<ClientOrderDTO> result = new();
            Connection db = new(ConectionString);
            db.TiempoEsperaComando = 0;
            try
            {
                db.SQLParametros.Clear();
                db.TiempoEsperaComando = 0;
                if (!Convert.IsDBNull(order.EmpId)) { db.SQLParametros.Add("@EmpId", SqlDbType.Int).Value = order.EmpId; }
                if (!Convert.IsDBNull(order.CustId)) { db.SQLParametros.Add("@CustId", SqlDbType.Int).Value = order.CustId; }
                if (!Convert.IsDBNull(order.OrderId)) { db.SQLParametros.Add("@OrderId", SqlDbType.Int).Value = order.OrderId; }
                if (!Convert.IsDBNull(order.ShipperId)) { db.SQLParametros.Add("@ShipperId", SqlDbType.Int).Value = order.ShipperId; }
                if (!string.IsNullOrWhiteSpace(order.ShipName)) { db.SQLParametros.Add("@ShipName", SqlDbType.VarChar, 100).Value = order.ShipName; }
                if (!string.IsNullOrWhiteSpace(order.ShipAddress)) { db.SQLParametros.Add("@ShipAddress", SqlDbType.VarChar, 100).Value = order.ShipAddress; }
                if (!string.IsNullOrWhiteSpace(order.ShipCity)) { db.SQLParametros.Add("@ShipCity", SqlDbType.VarChar, 100).Value = order.ShipCity; }
                if (!Convert.IsDBNull(order.OrderDate)) { db.SQLParametros.Add("@OrderDate", SqlDbType.DateTime).Value = order.OrderDate; }
                if (!Convert.IsDBNull(order.RequiredDate)) { db.SQLParametros.Add("@RequiredDate", SqlDbType.DateTime).Value = order.RequiredDate; }
                if (!Convert.IsDBNull(order.ShippedDate)) { db.SQLParametros.Add("@ShippedDate", SqlDbType.DateTime).Value = order.ShippedDate; }
                if (!Convert.IsDBNull(order.Freight)) { db.SQLParametros.Add("@Freight", SqlDbType.Money).Value = order.Freight; }
                if (!string.IsNullOrWhiteSpace(order.ShipCountry)) { db.SQLParametros.Add("@ShipCountry", SqlDbType.VarChar, 100).Value = order.ShipCountry; }

                if (!Convert.IsDBNull(order.ProductId)) { db.SQLParametros.Add("@Productid", SqlDbType.Int).Value = order.ProductId; }
                if (!Convert.IsDBNull(order.UnitPrice)) { db.SQLParametros.Add("@UnitPrice", SqlDbType.Money).Value = order.UnitPrice; }
                if (!Convert.IsDBNull(order.Qty)) { db.SQLParametros.Add("@Qty", SqlDbType.Int).Value = order.Qty; }
                if (!Convert.IsDBNull(order.Discount)) { db.SQLParametros.Add("@Discount", SqlDbType.Decimal).Value = order.Discount; }

                var reader = await db.EjecutarReaderAsync("AddNewOrder", CommandType.StoredProcedure);
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        var Dto = new ClientOrderDTO();

                        Dto.OrderId = Convert.ToInt32(reader["OrderID"]);
                        if (!Convert.IsDBNull(reader["RequiredDate"])) { Dto.RequiredDate = DateTime.Parse(reader["RequiredDate"].ToString()); }
                        if (!Convert.IsDBNull(reader["ShippedDate"])) { Dto.ShippedDate = DateTime.Parse(reader["ShippedDate"].ToString()); }
                        if (!Convert.IsDBNull(reader["ShipName"])) { Dto.ShipName = reader["ShipName"].ToString(); }
                        if (!Convert.IsDBNull(reader["ShipAddress"])) { Dto.ShipAddress = reader["ShipAddress"].ToString(); }
                        if (!Convert.IsDBNull(reader["ShipCity"])) { Dto.ShipCity = reader["ShipCity"].ToString(); }

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
