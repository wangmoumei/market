using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Market.Models
{
    public class OrderHead
    {
        public int OrderHeadID { get; set; }

        [Display(Name = "用户")]
        public int UserID { get; set; }
        public virtual User User { get; set; }

        [Display(Name = "成交价")]
        public double Price { get; set; }

        [Display(Name = "付款")]
        public bool IsPay { get; set; }

        [Display(Name = "送货")]
        public bool IsSend { get; set; }

        [Display(Name = "下单时间")]
        public DateTime CreateTime { get; set; }
    }
}