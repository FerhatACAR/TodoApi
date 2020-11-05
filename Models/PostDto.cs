using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodoApi.Models
{
    public class PostDto
    {
        public decimal SQ_ID { get; set; }
        public Nullable<decimal> RF_USER_ID { get; set; }
        public string TX_POST { get; set; }
    }
}