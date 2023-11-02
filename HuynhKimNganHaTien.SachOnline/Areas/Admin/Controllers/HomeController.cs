using HuynhKimNganHaTien.SachOnline.Models;
using SachOnline.Areas.Admin.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HuynhKimNganHaTien.SachOnline.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        // GET: Admin/Home
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection f)
        {
            //Gán các giá trị người dùng nhập liệu cho các biến
            var sTenDN = f["UserName"];
            var sMatKhau = f["Password"];
            //Gán giá trị cho đối tượng được tạo mới (ad)
            ADMIN ad = db.ADMINs.SingleOrDefault(n => n.TenDN == sTenDN && n.MatKhau
            == sMatKhau);
            if (ad != null)
            {
                Session["Admin"] = ad;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }
        public ActionResult IndexAdmin()
        {
            return View(db.TRANGTINs.ToList());
        }
        public ActionResult LoginLogout()
        {
            return PartialView("LoginLogoutPartial");
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(TRANGTIN tt)
        {
            if (ModelState.IsValid)
            {
                tt.MetaTitle = tt.TenTrang.RemoveDiacritics().Replace(" ", "-");

                tt.NgayTao = DateTime.Now;
                db.TRANGTINs.InsertOnSubmit(tt);
                db.SubmitChanges();
                return RedirectToAction("IndexAdmin");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var tt = db.TRANGTINs.Where(t => t.MaTT == id);
            return View(tt.SingleOrDefault());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f)
        {
            if (ModelState.IsValid)
            {
                var tt = db.TRANGTINs.Where(t => t.MaTT == int.Parse(f["MaTT"])).SingleOrDefault(); tt.TenTrang = f["TenTrang"];
                tt.NoiDung = f["NoiDung"];
                tt.NgayTao = Convert.ToDateTime(f["NgayTao"]);
                tt.MetaTitle = f["TenTrang"].RemoveDiacritics().Replace(" ", "-");
                db.SubmitChanges();
                return RedirectToAction("IndexAdmin");
            }
            else
            {
                return RedirectToAction("Edit");
            }
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            var tt = from t in db.TRANGTINs where t.MaTT == id select t;
            return View(tt.SingleOrDefault());
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            var tt = (from t in db.TRANGTINs where t.MaTT == id select t).SingleOrDefault();
            db.TRANGTINs.DeleteOnSubmit(tt);
            db.SubmitChanges();
            return RedirectToAction("IndexAdmin");
        }
    }
}