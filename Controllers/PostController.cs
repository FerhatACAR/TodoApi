using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TodoApiDal;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    public class PostController : ApiController
    {
        // GET api/<controller>/5
        public HttpResponseMessage Get(int id)
        {
            List<PostDto> result = new List<PostDto>();
            try
            {
                using (TodoAPIEntities context = new TodoAPIEntities())
                {
                    result = context.TODO_POST.Where(x => x.RF_USER_ID == id).Select(x => new PostDto()
                    {
                        SQ_ID = x.SQ_ID,
                        RF_USER_ID = x.RF_USER_ID,
                        TX_POST = x.TX_POST
                    }).ToList();
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e);
            }

            return Request.CreateResponse(HttpStatusCode.OK, result) ;
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] PostDto data)
        {
            try
            {
                using (TodoAPIEntities context = new TodoAPIEntities())
                {
                    var a = context.TODO_POST.Where(x => x.RF_USER_ID == data.RF_USER_ID).Max(x => x.SQ_ID);
                    context.TODO_POST.Add(new TODO_POST() {
                        RF_USER_ID = data.RF_USER_ID,
                        TX_POST = data.TX_POST,
                        SQ_ID = (context.TODO_POST.Max(x => x.SQ_ID) + 1)
                    });

                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put([FromBody] TODO_POST data)
        {
            try
            {
                using (TodoAPIEntities context = new TodoAPIEntities())
                {
                    var entity = context.TODO_POST.FirstOrDefault(x => x.SQ_ID == data.SQ_ID);

                    entity.TX_POST = data.TX_POST;

                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (TodoAPIEntities context = new TodoAPIEntities())
                {
                   var entity = context.TODO_POST.FirstOrDefault(x => x.SQ_ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Kayıt silinemedi");
                    }
                    else
                    {
                        context.TODO_POST.Remove(entity);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}