using HuynhKimNganHaTien.SachOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HuynhKimNganHaTien.SachOnline.Controllers
{
    public class UserController : Controller
    {
        DataClasses1DataContext db = new DataClasses1DataContext();

        // GET: User
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            var sTenDN = f["TenDN"];
            var sMatKhau = f["MatKhau"];
            if (string.IsNullOrEmpty(sTenDN))
            {
                ViewData["Err1"] = "Bạn chưa nhập tên đăng nhập ";
            }
            else if (string.IsNullOrEmpty(sMatKhau))
            {
                ViewData["Err2"] = "Phải nhập mật khẩu  ";
            }
            else
            {
                KHACHHANG kh = db.KHACHHANGs.SingleOrDefault(n => n.TaiKhoan == sTenDN && n.MatKhau == sMatKhau);
                if (kh != null)
                { 
                    ViewBag.ThongBao = "Chúc mừng đăng nhập thành công ";
                    Session["TaiKhoan"] = kh;
                    if (f["remember"].Contains("true"))
                    {
                        Response.Cookies["TenDN"].Value = sTenDN;
                        Response.Cookies["MatKhau"].Value = sMatKhau;
                        Response.Cookies["TenDN"].Expires = DateTime.Now.AddDays(1);
                        Response.Cookies["MatKhau"].Expires = DateTime.Now.AddDays(1);
                    }
                    else
                    {
                        Response.Cookies["TenDN"].Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies["MatKhau"].Expires = DateTime.Now.AddDays(-1);
                    }

                    Session["HoTen"] = kh.HoTen;
                    return RedirectToAction("Index", "SachOnline");
                }
                else
                {
                    ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng ";
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult chiTietKH()
        {
            var khachHang = Session["TaiKhoan"] as KHACHHANG;

            if (khachHang == null)
            {
                return HttpNotFound();
            }

            return View(khachHang);

        }

        public ActionResult DangXuat()
        {
            Session.Remove("TaiKhoan");
            return RedirectToAction("DangNhap", "User");

        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(FormCollection f, KHACHHANG kh)
        {
            //Gan cac gia tri nguoi dung nhap du lieu cho cac bien
            var sHoTen = f["HoTen"];
            var sTaiKhoan = f["TaiKhoan"];
            var sMatKhau = f["MatKhau"];
            var sMatkhaunhapLai = f["MatkhauNL"];
            var sDiaChi = f["Diachi"];
            var sEmail = f["Email"];
            var sDienThoai = f["DienThoai"];
            var dNgaySinh = String.Format("{0:MM/dd/yyyy}", f["NgaySinh"]);
            if (db.KHACHHANGs.SingleOrDefault(n => n.TaiKhoan == sTaiKhoan) != null)
            {
                ViewBag.ThongBao = "Tên đăng nhập đã tồn tại";
            }
            else if (db.KHACHHANGs.SingleOrDefault(n => n.Email == sEmail) != null)
            {
                ViewBag.ThongBao = "Email đã được sử dụng";
            }
            else if (ModelState.IsValid)
            {
                //Gần giá trị cho đối tượng được tạo mới (kh)
                kh.HoTen = sHoTen;
                kh.TaiKhoan = sTaiKhoan;
                kh.MatKhau = sMatKhau;
                kh.Email = sEmail;
                kh.DiaChi = sDiaChi;
                kh.DienThoai = sDienThoai;
                kh.NgaySinh = DateTime.Parse(dNgaySinh);
                db.KHACHHANGs.InsertOnSubmit(kh);
                db.SubmitChanges();
                return Redirect("~/User/DangNhap");
            }
            return View("Register");
        }
    }
}