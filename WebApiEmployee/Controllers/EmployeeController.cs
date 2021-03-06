using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using EmployeeData;

namespace WebApiEmployee.Controllers
{
    public class EmployeeController : ApiController
    {    
        /*[HttpGet]
        public IEnumerable<Employee> Data()
        {
            using(EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                return entities.Employees.ToList();
            }
        }*/


        [BasicAuthenticate]

        //Query string Parameter
        public HttpResponseMessage Get(string gender = "All")
        {
            //authontication
            string username = Thread.CurrentPrincipal.Identity.Name;

            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                switch (username.ToLower())
                {
                   
                    case "male":
                        return Request.CreateResponse(HttpStatusCode.OK,
                             entities.Employees.Where(e => e.Gender.ToLower() == "male").ToList() );

                    case "female":
                        return Request.CreateResponse(HttpStatusCode.OK,
                             entities.Employees.Where(e => e.Gender.ToLower() == "female").ToList());

                    default:
                        return Request.CreateResponse(HttpStatusCode.BadRequest);

                   /* default:
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                            "Value for gender must be All, Male or Female " + gender + " is invalid ");*/


                }
            }
        }

        public HttpResponseMessage Get(int id)
        {
            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                var entity =  entities.Employees.FirstOrDefault(e=> e.ID == id);

                if(entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with id = " + id.ToString() + " not found ");
                }
            }
        }

        public HttpResponseMessage Post([FromBody] Employee employee)
        {
            try
            {
                using(EmployeeDBEntities entities = new EmployeeDBEntities())
                {
                    entities.Employees.Add(employee);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, employee);

                    //location
                    message.Headers.Location = new Uri(Request.RequestUri + employee.ID.ToString());

                    return message;

                }
            }
            catch(Exception ex)
            {
               return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Delete(int id)
        {
            try
            {

                using(EmployeeDBEntities entities = new EmployeeDBEntities())
                {
                    var entity =  entities.Employees.FirstOrDefault(e => e.ID == id);
              

                    if(entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with id " + id.ToString() + " not found ");
                    }
                    else
                    {
                        entities.Employees.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put( int id, [FromUri] Employee employee)
        {
            try
            {

       
            using(EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                var entity = entities.Employees.FirstOrDefault(e => e.ID == id);

                if(entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with id " + id.ToString() + " not found");
                }
                else
                {
                    entity.FirstName = employee.FirstName;
                    entity.LastName = employee.LastName;
                    entity.Gender = employee.Gender;
                    entity.Salary = employee.Salary;

                    entities.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }

                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
           
        }
    }
}
