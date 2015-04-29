using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Market.Models
{
    public class Category
    {
        public int CategoryID { get; set; }

        [Display(Name = "名称")]
        public string Name { get; set; }

        [Display (Name = "图片")]
        public string PicURL { get; set; }
        public DateTime CreateTime { get; set; }
    }
}