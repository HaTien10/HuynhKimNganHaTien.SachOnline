using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HuynhKimNganHaTien.SachOnline.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;

namespace HuynhKimNganHaTien.SachOnline.Areas.Admin.Controllers
{
    public class SachController : Controller
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        // GET: Admin/Sach
        public ActionResult Index(int? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            return View(db.SACHes.ToList().OrderBy(n => n.MaSach).ToPagedList(iPageNum, iPageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
    }
}