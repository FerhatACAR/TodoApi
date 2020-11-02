using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TodoApiDal;

namespace TodoApi.Controllers
{
    public class TestController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<API_TEST_TABLE> Get()
        {
            try
            {
                using (TESTEntities context = new TESTEntities())
                {
                    var result = context.API_TEST_TABLE.ToList();
                    return result;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // GET api/<controller>/5
        public API_TEST_TABLE Get(int id)
        {
            try
            {
                using (TESTEntities context = new TESTEntities())
                {
                    var result = context.API_TEST_TABLE.FirstOrDefault(e => e.ID == id);
                    return result;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // POST api/<controller>
        public void Post([FromBody] API_TEST_TABLE data)
        {
            try
            {
                using (TESTEntities context = new TESTEntities())
                {
                    context.API_TEST_TABLE.Add(data);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // PUT api/<controller>/5
        public void Put([FromBody] API_TEST_TABLE data)
        {
            try
            {
                using (TESTEntities context = new TESTEntities())
                {

                    var entity = context.API_TEST_TABLE.FirstOrDefault(e => e.ID == data.ID);

                    entity.SURNAME = data.SURNAME;
                    entity.NAME = data.NAME;
                    entity.GENDER = data.GENDER;

                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (TESTEntities context = new TESTEntities())
                {
                    var entity = context.API_TEST_TABLE.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Employee with Id = " + id.ToString() + " not found to delete");
                    }
                    else
                    {
                        context.API_TEST_TABLE.Remove(entity);
                        context.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}