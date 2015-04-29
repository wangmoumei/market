using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Market.Models;
using Market.DAL;
using System.Web.Security;
using System.Data;

namespace Market.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        private MarketContext db = new MarketContext();

        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles="manager")]
        public ActionResult Manage()
        {
            return View();
        }
        [Authorize(Roles = "manager")]
        public ActionResult Wellcome()
        {
            try
            {
                if (db.Banners.Count() > 0)
                    ViewBag.banner = db.Banners.First().ImgUrl;
            }
            catch
            {

            }
            return View();
        }
        public JsonResult Login(string UserID,string Password)
        {
            var res = new JsonResult();
            List<Models.AdminUser> lst = db.AdminUsers.Where(m => m.UserID == UserID).ToList();
            if (lst.Count == 0)
            {
                res.Data = 0;
            }
            else
            {
                if (lst[0].Password == Password)
                {
                    res.Data = 1;
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                    1,
                    UserID,
                    DateTime.Now,
                    DateTime.Now.Add(FormsAuthentication.Timeout),
                    false,
                    "manager"
                    );
                    HttpCookie cookie = new HttpCookie(
                        FormsAuthentication.FormsCookieName,
                        FormsAuthentication.Encrypt(ticket)
                    );
                    Response.Cookies.Add(cookie);
                }
                else res.Data = 2;
            }
            return res;    
        }
        [Authorize(Roles = "manager")]
        public ActionResult Category(int Status = -1)
        {
            if(Status == 1)
                ViewBag.success = true;
            if (Status == 2)
                ViewBag.error = true;
            if (Status == 3)
                ViewBag.worning = true;
            var data = db.Categories;
            return View(data);
        }
        [Authorize(Roles = "manager")]
        public ActionResult CategoryCreate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CategoryCreate(Category category)
        {
            try
            {
                // TODO: Add insert logic here
                //HttpPostedFileBase file = Request.Files["file1"];
                //string path = Server.MapPath("~/Content/category/");
                //file.SaveAs(path + file.FileName);

                //Category category = new Category();
                //category.Name = Name;
                //category.PicURL = file.FileName;
                category.CreateTime = System.DateTime.Now;
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Category", new {Status=1});
            }
            catch
            {
                return RedirectToAction("Category", new { Status = 2 });
            }
        }
        public ActionResult CategoryDelete(int id=0)
        {
            if (id == 0)
            {
                return RedirectToAction("Category", new { Status = 3 });
            }
            try
            {
                Models.Category model = db.Categories.Single(m => m.CategoryID == id);
                db.Categories.Remove(model);
                db.SaveChanges();
                return RedirectToAction("Category", new { Status = 1 });
            }
            catch
            {
                return RedirectToAction("Category", new { Status = 2 });
            }
        }

        [HttpPost]
        public JsonResult CategoryEdit(int id,string name)
        {
            try
            {
                Models.Category model = db.Categories.Single(m => m.CategoryID == id);
                model.Name = name;
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return Json(1);
            }
            catch
            {
                return Json(0);
            }
        }

        [HttpPost]
        public ActionResult CategoryImg(HttpPostedFileBase file)
        {
                // TODO: Add insert logic here
                if (file == null)
                {
                    return Content("0,未选择文件");
                }
                string path = Server.MapPath("~/Content/category/");
                try
                {
                    file.SaveAs(path + file.FileName);
                    return Content("1,"+file.FileName);
                }
                catch
                {
                    return Content("0,上传失败");
                }
        }

        [Authorize(Roles = "manager")]
        public ActionResult ItemList(int page = 0)
        {
            List<Models.Item> lst = new List<Item>();
            List<Models.Item> itemlst = db.Items.ToList();
            for (int i = 5 * page; i < 5 * page + 5; i++)
            {
                if (i >= itemlst.Count()) break;
                lst.Add(itemlst[i]);
            }
            if (itemlst.Count() > 5 * page + 5)
            {
                ViewBag.page1 = page + 1;
            }
            if (page > 0)
                ViewBag.page2 = page - 1;
            return View(lst);

        }
        [Authorize(Roles = "manager")]
        public ActionResult ItemCreate()
        {

            List<Models.Category> lst = db.Categories.ToList();
            ViewBag.cat = new SelectList(lst, "CategoryID", "Name", null);
            return View();
        }
        [Authorize(Roles = "manager")]
        public ActionResult ItemEdit(int id = -1)
        {
            if (id < 0)
                return RedirectToAction("ItemList");
            Models.Item item = db.Items.Single(m => m.ItemID == id);
            List<Models.Category> lst = db.Categories.ToList();
            ViewBag.cat = new SelectList(lst, "CategoryID", "Name", item.CategoryID);
            return View(item);
        }
        [HttpPost]
        public ActionResult ItemCreate(Item item)
        {
            try
            {
                // TODO: Add insert logic here
                HttpPostedFileBase file = Request.Files["file1"];
                string path = Server.MapPath("~/Content/item/");
                file.SaveAs(path + file.FileName);

                item.CoverImg = file.FileName;
                item.CreateTime = System.DateTime.Now;
                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("ItemList");
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult ItemEdit(Item item)
        {
            try
            {
                // TODO: Add insert logic here
                HttpPostedFileBase file = Request.Files["file1"];
                string path = Server.MapPath("~/Content/item/");
                file.SaveAs(path + file.FileName);

                item.CoverImg = file.FileName;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ItemList");
            }
            catch
            {
                return View();
            }
        }
        [Authorize(Roles = "manager")]
        public ActionResult ItemDelete(int id = -1)
        {
            if (id < 0)
                return RedirectToAction("ItemList");
            db.Items.Remove(db.Items.Single(m => m.ItemID == id));
            db.SaveChanges();
            return RedirectToAction("ItemList");
        }
        [Authorize(Roles = "manager")]
        public ActionResult AdminUser()
        {
            return View(db.AdminUsers);
        }
        [Authorize(Roles = "manager")]
        public ActionResult AdminUserCreate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminUserCreate(Models.AdminUser adminuser)
        {
            adminuser.CreateTime = System.DateTime.Now;
            db.AdminUsers.Add(adminuser);
            db.SaveChanges();
            return View();
        }
        [Authorize(Roles = "manager")]
        public ActionResult User()
        {

            return View(db.Users);
        }
        [HttpPost]
        public ActionResult Banner(HttpPostedFileBase file)
        {

            if (file == null)
            {
                return Content("0,未选择文件");
            }
            string path = Server.MapPath("~/Content/img/");
            try
            {
                file.SaveAs(path + file.FileName);
                db.Banners.First().ImgUrl = file.FileName;
                db.SaveChanges();
                return Content("1," + file.FileName);
            }
            catch
            {
                return Content("0,上传失败");
            }
        }
    }
}
