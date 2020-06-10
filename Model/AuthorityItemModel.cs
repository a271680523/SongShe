using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Common.Extend;

namespace Model
{
    public class AuthorityItemModel
    {
        [Key]
        public int ID { get; set; }
        public int AuthorityID { get; set; }
        public int MenuID { get; set; }
        [Newtonsoft.Json.JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime AddTime { get; set; }
    }
}