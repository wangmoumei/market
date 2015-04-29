using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Market.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "用户名")]
        public string Name { get; set; }

        [Display(Name = "账号")]
        public string UserID { get; set; }

        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "头像")]
        public string HeadImg { get; set; }

        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Display(Name = "地址")]
        public string Address { get; set; }

        [Display(Name = "电话")]
        public string CellNum { get; set; }

        public DateTime CreateTime { get; set; }
    }
}