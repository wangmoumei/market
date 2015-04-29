using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Market.Models
{
    public class OrderBody
    {
        public int OrderBodyID { get; set; }

        [Display(Name = "订单头")]
        public int OrderHeadID { get; set; }
        public virtual OrderHead OrderHead { get; set; }

        [Display(Name = "商品")]
        public int ItemID { get; set; }
        public virtual Item Item { get; set; }

        [Display(Name = "商品数量")]
        public int Count { get; set; }
    }
}