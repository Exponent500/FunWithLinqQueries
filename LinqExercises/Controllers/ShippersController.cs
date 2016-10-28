using LinqExercises.Infrastructure;
using LinqExercises.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace LinqExercises.Controllers
{
    public class ShippersController : ApiController
    {
        private NORTHWNDEntities _db;

        public ShippersController()
        {
            _db = new NORTHWNDEntities();
        }

        //GET: api/shippers/reports/freight
        [HttpGet, Route("api/shippers/reports/freight"), ResponseType(typeof(IQueryable<object>))]
        public IHttpActionResult GetFreightReport()
        {

            List<MyClass> resultSet = (_db.Shippers
                                        .Select(s => new MyClass
                                        {
                                            Shipper = s.ShipperID,
                                            FreightTotals = Math.Round((decimal)(s.Orders.Sum(o => o.Freight)),0)
                                        })
                                        .OrderByDescending(o => o.FreightTotals)
                                        ).ToList();

            // See this blog post for more information about projecting to anonymous objects. https://blogs.msdn.microsoft.com/swiss_dpe_team/2008/01/25/using-your-own-defined-type-in-a-linq-query-expression/

            return Ok(resultSet);
            //throw new NotImplementedException("Write a query to return an array of anonymous objects that have two properties. A Shipper property and the freight totals for that shipper labelled as 'FreightTotals' rounded to the nearest whole number from the Orders table, ordered by FreightTotals in descending order.");
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
        }
    }
}
