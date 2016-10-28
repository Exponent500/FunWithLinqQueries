using LinqExercises.Infrastructure;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace LinqExercises.Controllers
{
    public class SuppliersController : ApiController
    {
        private NORTHWNDEntities _db;

        public SuppliersController()
        {
            _db = new NORTHWNDEntities();
        }

        //GET: api/suppliers/salesAndMarketing
        [HttpGet, Route("api/suppliers/salesAndMarketing"), ResponseType(typeof(IQueryable<Supplier>))]
        public IHttpActionResult GetSalesAndMarketing()
        {
            var resultSet = from supplier in _db.Suppliers
                            where supplier.ContactTitle.Contains("Marketing Manager") || supplier.ContactTitle.Contains("Sales Representative")
                            where supplier.Fax != null
                            select supplier;

            return Ok(resultSet);
            //throw new NotImplementedException("Write a query to return all Suppliers that are marketing managers or sales representatives that have a fax number");
        }

        //GET: api/suppliers/search
        [HttpGet, Route("api/suppliers/search"), ResponseType(typeof(IQueryable<Supplier>))]
        public IHttpActionResult SearchSuppliers(string term)
        {

            var resultSet = _db.Suppliers
                                    .Where(s => s.Address.Contains(term))
                                    .OrderBy(s => s.CompanyName)
                                    .Select(s => s);
            
            /* Linq Query version that for some reason isn't working...*/

            //var resultSet = from supplier in _db.Suppliers
            //                where supplier.Address.Contains(term)
            //                orderby supplier.CompanyName
            //                select supplier;

            return Ok(resultSet);
            // throw new NotImplementedException("Write a query to return all Suppliers containing the 'term' variable in their address. The list should ordered alphabetically by company name.");
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
        }
    }
}
