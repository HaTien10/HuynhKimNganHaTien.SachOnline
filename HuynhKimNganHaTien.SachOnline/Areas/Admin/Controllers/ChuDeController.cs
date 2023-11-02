using HuynhKimNganHaTien.SachOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Tokenizer.Symbols;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;
using static HuynhKimNganHaTien.SachOnline.Models.DataClasses1DataContext;

namespace HuynhKimNganHaTien.SachOnline.Areas.Admin.Controllers
{
    public class ChuDeController : Controller
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        public CHUDE Lay1CD(int id)
        {
            return db.CHUDEs.SingleOrDefault(CD => CD.MaCD == id);

        }
        // GET: Admin/NXB
        public ActionResult ChuDe()
        {
            return View(db.CHUDEs);
        }

        public ActionResult ChiTiet(int id)
        {
            return View(Lay1CD(id));
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(Lay1CD(id));
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit()
        {
            if (ModelState.IsValid)
            {
                var cd = Lay1CD(int.Parse(Request.Form["MaCD"]));
                cd.TenChuDe = Request.Form["TenChuDe"];
                db.SubmitChanges();
                return RedirectToAction("ChuDe");
            }
            else
            {
                return RedirectToAction("ChuDe");
            }
        }

        [HttpGet]
        public ActionResult insert()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult insert(CHUDE cd)
        {
            if (ModelState.IsValid)
            {
                db.CHUDEs.InsertOnSubmit(cd); // Add the new publisher
                db.SubmitChanges();
                return RedirectToAction("ChuDe");
            }

            return View(cd);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(Lay1CD(id));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Delete(CHUDE CD, int id)
        {
            using (var db = new DataClasses1DataContext())
            {
                var CDToDelete = db.CHUDEs.SingleOrDefault(cd => cd.MaCD == id);
                if (CDToDelete != null)
                {
                    db.CHUDEs.DeleteOnSubmit(CDToDelete);
                    db.SubmitChanges();
                }
            }

            return RedirectToAction("ChuDe");
        }


    }
}