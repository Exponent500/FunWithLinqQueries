using LinqExercises.Infrastructure;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace LinqExercises.Controllers
{
    public class EmployeesController : ApiController
    {
        private NORTHWNDEntities _db;

        public EmployeesController()
        {
            _db = new NORTHWNDEntities();
        }

        // GET: api/employees
        [HttpGet, Route("api/employees"), ResponseType(typeof(IQueryable<Employee>))]
        public IHttpActionResult GetEmployees()
        {
            var resultSet = from employee in _db.Employees
                            select employee;
              
            return Ok(resultSet);
            //throw new NotImplementedException("Write a query to return all employees");
        }

        // GET: api/employees/title/Sales Manager
        [HttpGet, Route("api/employees/title/{title}"), ResponseType(typeof(IQueryable<Employee>))]
        public IHttpActionResult GetEmployeesByTitle(string title)
        {
            var resultSet = from employee in _db.Employees
                            where employee.Title.Contains(title)
                            select employee;
            return Ok(resultSet);
            //throw new NotImplementedException("Write a query to return all employees with the given Title");
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
        }
    }
}
