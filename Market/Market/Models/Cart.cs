using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Market.Models
{
    public class Cart
    {
        public int CartID { get; set; }

        [Display(Name = "用户")]
        public int UserID { get; set; }
        public virtual User User { get; set; }

        [Display(Name = "商品")]
        public int ItemID { get; set; }
        public virtual Item Item { get; set; }

        [Display(Name = "数量")]
        public int Count { get; set; }

        [Display(Name = "购买与否")]
        public bool IsBought { get; set; }

        public DateTime CreateTime { get; set; }
    }
}