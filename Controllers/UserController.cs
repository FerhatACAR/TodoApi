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
    public class UserController : ApiController
    {
        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] UserDto data)
        {
            try
            {
                using (TodoAPIEntities context = new TodoAPIEntities())
                {
                    if (data.REQUEST_TYPE == "signup")
                    {
                        var result = context.TODO_USER.Where(x => x.CH_EMAIL == data.CH_EMAIL).Any();

                        if (result)
                        {
                            return Request.CreateResponse(HttpStatusCode.Conflict, "Email'e ait kayıt bulunmaktadır");
                        }

                        context.TODO_USER.Add(new TODO_USER()
                        {
                            SQ_ID = (context.TODO_USER.Max(x => x.SQ_ID) + 1),
                            CD_NAME = data.CD_NAME,
                            CH_EMAIL = data.CH_EMAIL,
                            CH_PASSWORD = data.CH_PASSWORD
                        });

                        context.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, "Kayıt başarıyla tamamlandı");
                    }
                    else if (data.REQUEST_TYPE == "signin")
                    {
                        var result = context.TODO_USER.Where(x => x.CH_EMAIL == data.CH_EMAIL && x.CH_PASSWORD == data.CH_PASSWORD).Any();

                        if (result)
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, "Kullanıcı girişi başarılı");
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.NotFound, "Email/Şifre Yanlış tekrar deneyiniz");
                        }
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "Request anlaşılamadı");
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