using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Market.Models;


namespace Market.DAL
{
    public class MarketInitializer
        : DropCreateDatabaseIfModelChanges<MarketContext> 
    {
        protected override void Seed(MarketContext context)
        {
            var students = new List<Category> 
            { 
                new Category { Name = "美食", PicURL = "111.png", CreateTime = DateTime.Now },
                new Category { Name = "机票", PicURL = "air.png", CreateTime = DateTime.Now },
                new Category { Name = "电影", PicURL = "movie.png", CreateTime = DateTime.Now },
                new Category { Name = "运动", PicURL = "sport.png", CreateTime = DateTime.Now },
                new Category { Name = "果蔬", PicURL = "fruit.png", CreateTime = DateTime.Now },
                new Category { Name = "车票", PicURL = "train.png", CreateTime = DateTime.Now },
                new Category { Name = "搬家", PicURL = "truck.png", CreateTime = DateTime.Now },
                new Category { Name = "办公", PicURL = "work.png", CreateTime = DateTime.Now }
            };
            students.ForEach(s => context.Categories.Add(s));

            var Users = new List<AdminUser> 
            { 
                new AdminUser { Name = "超级管理员", Type = 1, CreateTime = DateTime.Now ,UserID = "admin",Password="admin"}
            };
            Users.ForEach(s => context.AdminUsers.Add(s));

            var Items = new List<Item> 
            { 
                new Item { Title = "快餐",Content = "快餐快餐快餐快餐",Text = "快餐快餐快餐快餐快餐快餐", CreateTime = DateTime.Now ,
                    CoverImg = "fastfood.png",Price=10,OldPrice = 10,CategoryID=1,Sellnum = 10,Stock = 10},
                new Item { Title = "某某货运",Content = "某某货运货运货运货运货运",Text = "货运货运货运货运货运货运货运", CreateTime = DateTime.Now ,
                    CoverImg = "truck.png",Price=888,OldPrice = 1111,CategoryID=7,Sellnum = 20,Stock = 4},
                new Item { Title = "海鲜",Content = "海鲜海鲜海鲜海鲜",Text = "海鲜海鲜海鲜海鲜海鲜海鲜", CreateTime = DateTime.Now ,
                    CoverImg = "food9.png",Price=88,OldPrice = 100,CategoryID=1,Sellnum = 100,Stock = 1000},
                new Item { Title = "面包",Content = "面包面包面包面包面包",Text = "面包面包面包面包", CreateTime = DateTime.Now ,
                    CoverImg = "food5.gif",Price=10,OldPrice = 20,CategoryID=1,Sellnum = 100,Stock = 10},
                new Item { Title = "折扣电影票",Content = "折扣电影票折扣电影票",Text = "折扣电影票折扣电影票折扣电影票", CreateTime = DateTime.Now ,
                    CoverImg = "movie1.jpg",Price=10,OldPrice = 10,CategoryID=3,Sellnum = 100,Stock = 100},
                new Item { Title = "最新大片",Content = "折扣电影票折扣电影票",Text = "折扣电影票折扣电影票折扣电影票", CreateTime = DateTime.Now ,
                    CoverImg = "movie2.jpg",Price=10,OldPrice = 10,CategoryID=3,Sellnum = 10,Stock = 10},
                new Item { Title = "特价3D电影",Content = "折扣电影票折扣电影票",Text = "折扣电影票折扣电影票折扣电影票", CreateTime = DateTime.Now ,
                    CoverImg = "movie3.jpg",Price=10,OldPrice = 10,CategoryID=3,Sellnum = 10,Stock = 10},
                new Item { Title = "预定机票",Content = "预定机票预定机票",Text = "半价机票半价机票半价机票", CreateTime = DateTime.Now ,
                    CoverImg = "plane1.png",Price=333,OldPrice = 1230,CategoryID=2,Sellnum = 10,Stock = 10},
                new Item { Title = "特价机票",Content = "特价机票特价机票",Text = "特价机票特价机票特价机票", CreateTime = DateTime.Now ,
                    CoverImg = "plane2.png",Price=500,OldPrice = 2000,CategoryID=2,Sellnum = 10,Stock = 10},
                new Item { Title = "半价机票",Content = "半价机票半价机票",Text = "半价机票半价机票半价机票", CreateTime = DateTime.Now ,
                    CoverImg = "plane3.png",Price=100,OldPrice = 1000,CategoryID=2,Sellnum = 10,Stock = 10},
                new Item { Title = "预定机票",Content = "预定机票预定机票",Text = "预定机票预定机票预定机票", CreateTime = DateTime.Now ,
                    CoverImg = "plane3.png",Price=888,OldPrice = 6666,CategoryID=2,Sellnum = 10,Stock = 10},
                new Item { Title = "春运火车票",Content = "春运火车票春运火车票",Text = "春运火车票春运火车票春运火车票", CreateTime = DateTime.Now ,
                    CoverImg = "train.png",Price=10,OldPrice = 10,CategoryID=6,Sellnum = 10,Stock = 10},
                new Item { Title = "客运长途客车",Content = "客运长途客车客运长途客车",Text = "客运长途客车客运长途客车", CreateTime = DateTime.Now ,
                    CoverImg = "car.png",Price=10,OldPrice = 10,CategoryID=6,Sellnum = 10,Stock = 10},
                new Item { Title = "早餐",Content = "早餐早餐早餐",Text = "早餐早餐早餐早餐", CreateTime = DateTime.Now ,
                    CoverImg = "food1.png",Price=10,OldPrice = 10,CategoryID=1,Sellnum = 10,Stock = 10},
                new Item { Title = "面馆优惠券",Content = "面馆面馆面馆面馆",Text = "面馆面馆面馆面馆面馆面馆面馆面馆面馆面馆面馆面馆", CreateTime = DateTime.Now ,
                    CoverImg = "food2.png",Price=10,OldPrice = 10,CategoryID=1,Sellnum = 10,Stock = 10},
                new Item { Title = "披萨披萨披萨披萨披萨披萨",Content = "披萨披萨披萨披萨披萨披萨披萨披萨披萨披萨披萨披萨",Text = "披萨披萨披萨披萨披萨披萨披萨披萨披萨披萨披萨披萨", CreateTime = DateTime.Now ,
                    CoverImg = "food3.png",Price=10,OldPrice = 10,CategoryID=1,Sellnum = 10,Stock = 10}
            };
            Items.ForEach(s => context.Items.Add(s));

            var Banners = new List<Banner> 
            { 
                new Banner { ImgUrl = "banner3.png"}
            };
            Banners.ForEach(s => context.Banners.Add(s));

            context.SaveChanges();
        }
    }
}