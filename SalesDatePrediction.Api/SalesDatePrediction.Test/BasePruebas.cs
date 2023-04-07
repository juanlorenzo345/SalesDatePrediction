using Microsoft.EntityFrameworkCore;
using SalesDatePrediction.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDatePrediction.Test
{

    /// <summary>
    /// Clase para crear un contexto en memoria y realizar las pruebas unitarias
    /// </summary>
    public class BasePruebas
    {
        protected DBContext ConstruirContext(string nombreDB)
        {
            var opciones = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(nombreDB).Options;

            var dbContext = new DBContext(opciones);
            return dbContext;
        }

    }
}
