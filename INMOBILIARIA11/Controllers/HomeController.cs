using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using INMOBILIARIA11.Models;
using System.Text;

namespace INMOBILIARIA11.Controllers
{


    public class HomeController : Controller
    {
        // GET: Home

        public DB_Entities _db = new DB_Entities();
        public ActionResult Index()
        {
            if (Session["IdUser"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        /*
         * Registro
         */

        public ActionResult Register()
        {
            return View();

        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User _user)
        {
            if (ModelState.IsValid)
            {
                var check = _db.Users.FirstOrDefault(s => s.Email == _user.Email);
                if (check == null)
                {
                    _user.Contraseña = GetMD5(_user.Contraseña);
                    _db.Configuration.ValidateOnSaveEnabled = false;
                    _db.Users.Add(_user);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "El email ya exiate";
                    return View();
                }


            }
            return View();


        }


        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
        /*
         * Login
         */

        public ActionResult Login()
        {

        return View();

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin _userLogin)
        {
            if (ModelState.IsValid)
            {


                var f_password = GetMD5(_userLogin.Contraseña);
                var data = _db.Users.Where(s => s.Email.Equals(_userLogin.Email) && s.Contraseña.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["FullName"] = data.FirstOrDefault().Nombre + " " + data.FirstOrDefault().Apellido;
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["IdUser"] = data.FirstOrDefault().IdUser;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
            }
            
            return View();
        }


        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }




    }
}