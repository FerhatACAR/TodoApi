using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodoApi.Models
{
    public class UserDto
    {
        public decimal SQ_ID { get; set; }
        public string CD_NAME { get; set; }
        public string CH_EMAIL { get; set; }
        public string CH_PASSWORD { get; set; }
        public string REQUEST_TYPE { get; set; }
    }
}