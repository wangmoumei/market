using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Market.Models
{
    public class Item
    {
        [Key]
        public int ItemID { get; set; }

        [Display(Name = "类别")]
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }

        [Display(Name = "商品名")]
        public string Title { get; set; }

        [Display(Name = "商品简介")]
        public string Content { get; set; }

        [Display(Name = "商品详情")]
        public string Text { get; set; }

        [Display(Name = "封面照片")]
        public string CoverImg { get; set; }

        [Display(Name = "价格")]
        public double Price { get; set; }

        [Display(Name = "原价")]
        public double OldPrice { get; set; }

        [Display(Name = "添加时间")]
        public DateTime CreateTime { get; set; }

        [Display(Name = "已售")]
        public int Sellnum { get; set; }

        [Display(Name = "库存")]
        public int Stock { get; set; }
    }
}