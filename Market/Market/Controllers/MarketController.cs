using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Market.DAL;

namespace Market.Controllers
{
    public class MarketController : Controller
    {
        //
        // GET: /Market/
        private MarketContext db = new MarketContext();
        public ActionResult Index()
        {
            if (db.Banners.Count() > 0)
                ViewBag.banner = db.Banners.First().ImgUrl;
            return View(db.Categories);
        }
        public ActionResult List(int id = 0)
        {
            if(db.Banners.Count() > 0)
                ViewBag.banner = db.Banners.First().ImgUrl;
            if(id == 0)
                return RedirectToAction("Index");
            ViewBag.Title =  db.Categories.Single(m => m.CategoryID == id).Name;
            List<Models.Item> lst = new List<Models.Item>();
            foreach (Models.Item item in db.Items)
            {
                if (item.CategoryID == id)
                    lst.Add(item);
            }
            return View(lst);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Login(string UserID, string Password)
        {
            List<Models.User> lst = db.Users.Where(m => m.UserID == UserID).ToList();
            if (lst.Count > 0)
            {
                if (lst[0].Password == Password)
                {
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                    1,
                    lst[0].ID.ToString(),
                    DateTime.Now,
                    DateTime.Now.Add(FormsAuthentication.Timeout),
                    false,
                    "guest"
                    );
                    HttpCookie cookie = new HttpCookie(
                        FormsAuthentication.FormsCookieName,
                        FormsAuthentication.Encrypt(ticket)
                    );
                    Response.Cookies.Add(cookie);
                    return Json(1);//登陆成功
                }
                else return Json(2);//密码错误
            }
            else
            {
                return Json(0);//用户不存在
            }
            
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Register(string UserID,string Password)
        {
            List<Models.User> lst = db.Users.Where(m=>m.UserID == UserID).ToList();
            if (lst.Count > 0)
            {
                return Json(0);
            }
            else
            {
                Models.User user = new Models.User();
                user.UserID = UserID;
                user.Password = Password;
                user.CreateTime = System.DateTime.Now;
                db.Users.Add(user);
                db.SaveChanges();

                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                    1,
                    user.ID.ToString(),
                    DateTime.Now,
                    DateTime.Now.Add(FormsAuthentication.Timeout),
                    false,
                    "guest"
                    );
                HttpCookie cookie = new HttpCookie(
                    FormsAuthentication.FormsCookieName,
                    FormsAuthentication.Encrypt(ticket)
                );
                Response.Cookies.Add(cookie);
                return Json(1);
            }
            //return Json("Login");
        }
        [Authorize(Roles = "guest")]
        public ActionResult Center()
        {
            int id = Convert.ToInt16(User.Identity.Name);
            Models.User user = db.Users.Single(m => m.ID == id);
            List<Models.OrderHead> lst = new List<Models.OrderHead>();
            foreach (Models.OrderHead order in db.OrderHeads)
            {
                if (order.UserID == id)
                    lst.Add(order);
            }
            ViewBag.order = lst.OrderByDescending(m => m.CreateTime).ToList(); ;
            return View(user);
        }

        [Authorize(Roles = "guest")]
        public ActionResult Cart()
        {
            int id = Convert.ToInt16(User.Identity.Name);
            List<Models.Cart> lst = new List<Models.Cart>();
            foreach (Models.Cart cart in db.Carts)
            {
                if (cart.UserID == id && cart.IsBought == false)
                    lst.Add(cart);
            }
            return View(lst);
        }
        public ActionResult UserEdit(int status = 0)
        {
            if (status == 1) ViewBag.no = "(´･_･`)先把名字填上吧";
            else if (status == 2) ViewBag.no = " (ｏ・_・)ノ地址还没写哦";
            else if (status == 3) ViewBag.no = "<(*ΦωΦ*)>告诉我你的手机号";
            int id = Convert.ToInt16(User.Identity.Name);
            Models.User user = db.Users.Single(m => m.ID == id);
            return View(user);
        }
        [HttpPost]
        public ActionResult UserEdit(Models.User user)
        {
            int id = Convert.ToInt16(User.Identity.Name);
            Models.User newuser = db.Users.Single(m => m.ID == id);
            newuser.CellNum = user.CellNum;
            newuser.Name = user.Name;
            newuser.Address = user.Address;
            newuser.Email = user.Email;
            //db.Entry(newuser).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Center");
        }
        public RedirectResult Jump()
        {
            string url = Request["ReturnUrl"];
            if (string.IsNullOrEmpty(url))
            {
                return new RedirectResult("/market/login");
            }
            else if(url.ToLower().Contains("/admin"))
            {
                return new RedirectResult("/admin/index"+"?ReturnUrl="+url);
            }
            else
            {
                return new RedirectResult("/market/login" + "?ReturnUrl=" + url);
            }
        }

        [HttpPost]
        public JsonResult AddCart(int itemid)
        {
            //1加商品2加商品数量3超出库存0失败4缺用户
            try
            {
                
                if (string.IsNullOrEmpty(User.Identity.Name))
                    return Json(4);
                int id = Convert.ToInt16(User.Identity.Name);
                List<Models.Cart> lst = db.Carts.Where(m => m.ItemID == itemid && m.UserID == id && m.IsBought == false).ToList();
                if (lst.Count > 0)
                {
                    lst[0].Count++;
                    if (lst[0].Count > lst[0].Item.Stock)
                        return Json(3);
                    db.Entry(lst[0]).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(2);
                }
                else
                {
                    Models.Cart cart = new Models.Cart();
                    cart.UserID = id;
                    cart.ItemID = itemid;
                    cart.CreateTime = System.DateTime.Now;
                    cart.Count = 1;
                    cart.IsBought = false;
                    db.Carts.Add(cart);
                    db.SaveChanges();
                    return Json(1);
                }
                
            }
            catch
            {
                return Json(0);
            }
            
        }

        [HttpPost]
        public JsonResult CartPlus(int itemid)
        {
            //type 1加2减3删除
            //1成功2缺记录3超出库存0失败4缺用户5未知操作
            try
            {
                if (string.IsNullOrEmpty(User.Identity.Name))
                    return Json(4);
                int id = Convert.ToInt16(User.Identity.Name);
                List<Models.Cart> lst = db.Carts.Where(m => m.ItemID == itemid && m.UserID == id && m.IsBought == false).ToList();
                if (lst.Count > 0)
                {
                    lst[0].Count++;
                    if (lst[0].Count > lst[0].Item.Stock)
                        return Json(3);
                    db.SaveChanges();
                    return Json(1);
                }
                else
                {
                    return Json(2);
                }
                
            }
            catch
            {
                return Json(0);
            }
            
        }
        public JsonResult Cartmini(int itemid)
        {
            //type 1加2减3删除
            //1成功2缺记录3超出库存0失败4缺用户5未知操作
            try
            {
                if (string.IsNullOrEmpty(User.Identity.Name))
                    return Json(4);
                int id = Convert.ToInt16(User.Identity.Name);
                List<Models.Cart> lst = db.Carts.Where(m => m.ItemID == itemid && m.UserID == id && m.IsBought == false).ToList();
                if (lst.Count > 0)
                {
                    lst[0].Count--;
                    db.SaveChanges();
                    return Json(1);
                }
                else
                {
                    return Json(2);
                }

            }
            catch
            {
                return Json(0);
            }

        }
        public JsonResult CartDelete(int itemid)
        {
            //type 1加2减3删除
            //1成功2缺记录3超出库存0失败4缺用户5未知操作
            try
            {
                if (string.IsNullOrEmpty(User.Identity.Name))
                    return Json(4);
                int id = Convert.ToInt16(User.Identity.Name);
                List<Models.Cart> lst = db.Carts.Where(m => m.ItemID == itemid && m.UserID == id && m.IsBought == false).ToList();
                if (lst.Count > 0)
                {
                    db.Carts.Remove(lst[0]);
                    db.SaveChanges();
                    return Json(1);
                }
                else
                {
                    return Json(2);
                }

            }
            catch
            {
                return Json(0);
            }

        }
        [Authorize(Roles = "guest")]
        public ActionResult OrderCteate()
        {
            int id = Convert.ToInt16(User.Identity.Name);
            Models.User user = db.Users.Single(m => m.ID == id);
            if (string.IsNullOrEmpty(user.Address))
            {
                return RedirectToAction("useredit", new { status = 2 });
            }
            else if (string.IsNullOrEmpty(user.Name))
            {
                return RedirectToAction("useredit", new { status = 1 });
            }
            else if (string.IsNullOrEmpty(user.CellNum))
            {
                return RedirectToAction("useredit", new { status = 3 });
            }
            Models.OrderHead orderhead = new Models.OrderHead();
            orderhead.CreateTime = System.DateTime.Now;
            orderhead.UserID = id;
            db.OrderHeads.Add(orderhead);
            db.SaveChanges();

            double sum = 0;
            foreach (Models.Cart cart in db.Carts)
            {
                if (cart.UserID == id && cart.IsBought == false)
                {
                    cart.IsBought = true;
                    Models.OrderBody orderbody = new Models.OrderBody();
                    orderbody.ItemID = cart.ItemID;
                    orderbody.OrderHeadID = orderhead.OrderHeadID;
                    orderbody.Count = cart.Count;
                    db.OrderBodys.Add(orderbody);
                    sum += cart.Item.Price * cart.Count;
                }
            }
            orderhead.Price = sum;
            db.SaveChanges();

            return RedirectToAction("Order", new { OrderheadID = orderhead.OrderHeadID, status =1});
        }
        [Authorize(Roles = "guest")]
        public ActionResult Order(int OrderheadID,int status = 0)
        {
            List<Models.OrderBody> lst = new List<Models.OrderBody>();
            foreach (Models.OrderBody orderbody in db.OrderBodys)
            {
                if (orderbody.OrderHeadID == OrderheadID)
                    lst.Add(orderbody);
            }
            if(status == 1)ViewBag.neworder = "结算成功";
            return View(lst);
        }
        public ActionResult Search(string search = null)
        {
            ViewBag.search = search;
            List<Models.Item> lst = new List<Models.Item>();
            if (search == "随便逛逛")
            {
                ViewBag.search = "随便逛逛";
                Random rm = new Random();
                int nn = rm.Next(1, 5);
                for (int i = 0; i < nn; i++)
                {
                    int x = rm.Next(1, db.Items.Count());
                    List<Models.Item> rlst = db.Items.Where(m => m.ItemID == x).ToList();
                    if (rlst.Count() > 0 && lst.Count()>0)
                    {
                        for (int j = 0; j < lst.Count(); j++)
                        {
                            if(lst[j].ItemID == x)
                                return View(lst);
                        }
                    }
                    if (rlst.Count() > 0)
                    lst.Add(rlst[0]);
                }
                    return View(lst);
            }
            if (string.IsNullOrEmpty(search))
            {
                return View(lst);
            }
            lst = db.Items.Where(s => s.Title.ToUpper().Contains(search.ToUpper())).ToList();
            return View(lst);
        }


    }
}
