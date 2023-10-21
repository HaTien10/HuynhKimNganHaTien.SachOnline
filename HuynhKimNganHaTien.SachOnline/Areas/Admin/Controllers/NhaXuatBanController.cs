using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HuynhKimNganHaTien.SachOnline.Models;

namespace HuynhKimNganHaTien.SachOnline.Areas.Admin.Controllers
{
    public class NhaXuatBanController : Controller
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        // GET: Admin/NhaXuatBan
        public ActionResult NXB()
        {
            return View(db.NHAXUATBANs);
        }
        public NHAXUATBAN LayNXB(int id)
        {
            return db.NHAXUATBANs.SingleOrDefault(nxb => nxb.MaNXB == id);

        }
        public ActionResult ChiTiet(int id)
        {
            return View(LayNXB(id));
        }
        [HttpGet]
        public ActionResult Sua(int id)
        {
            return View(LayNXB(id));
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Sua()
        {
            if (ModelState.IsValid)
            {
                var nxb = LayNXB(int.Parse(Request.Form["MaNXB"]));
                nxb.TenNXB = Request.Form["TenNXB"];
                nxb.DiaChi = Request.Form["DiaChi"];
                nxb.DienThoai = Request.Form["DienThoai"];
                db.SubmitChanges();
                return RedirectToAction("NXB");
            }
            else
            {
                return RedirectToAction("Sua");
            }
        }

        [HttpGet]
        public ActionResult Them()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Them(NHAXUATBAN nxb)
        {
            if (ModelState.IsValid)
            {
                db.NHAXUATBANs.InsertOnSubmit(nxb);
                db.SubmitChanges();
                return RedirectToAction("NXB");
            }

            return View(nxb);
        }

        [HttpGet]
        public ActionResult Xoa(int id)
        {
            return View(LayNXB(id));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Xoa(NHAXUATBAN nxb, int id)
        {
            using (var db = new DataClasses1DataContext())
            {
                var nxbToDelete = db.NHAXUATBANs.SingleOrDefault(NXB => NXB.MaNXB == id);
                if (nxbToDelete != null)
                {
                    db.NHAXUATBANs.DeleteOnSubmit(nxbToDelete);
                    db.SubmitChanges();
                }
            }

            return RedirectToAction("NXB");
        }

    }
}