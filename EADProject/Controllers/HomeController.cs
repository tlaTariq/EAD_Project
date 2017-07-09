using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EADProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Blog()
        {
            return View();
        }

        public ActionResult Songs()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        //[ActionName("Login")]
        [HttpPost]
        public ActionResult Login2()
        {
            String login = Request["txtLogin"];
            String password = Request["txtPassword"];

            var dto = Models.DBHelper.Validate(login, password);
            if (dto != null)
            {
                Session["Login"] = login;
                
                return Redirect("~/Home/Index");
            }
            else
            {
                ViewBag.Login = login;
                ViewBag.Msg = "Not valid";
            }

            return View("Login");
        }

        public ActionResult Logout()
        {
            Session["Login"] = null;
            Session.Abandon();

            return Redirect("~/Home/Index");
        }

        public ActionResult Add()
        {
            if (Session["Login"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Add2(string txtName, string txtSinger, HttpPostedFile file)
        {
            if (Session["Login"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            String name = txtName;
            String singer = txtSinger;
            var song = Request.Files.Get("file");
                 
            var allowedExtensions = new[] { ".mp3" };
            var checkextension = Path.GetExtension(song.FileName).ToLower();

            if (!allowedExtensions.Contains(checkextension))
            {
                TempData["notice"] = "PLEASE SELECT \"mp3\" FILE";
                return Redirect("Add");
            }



            if (song != null && song.ContentLength > 0)
            {
                var fileName = Path.GetFileName(song.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/songs"), fileName);
                song.SaveAs(path);

                Models.DBHelper.addSong(name, singer, fileName);
            }

            return View("Songs");
        }



        public ActionResult Edit(int id)
        {
            if (Session["Login"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Models.SongsDTO obj = Models.DBHelper.getSongByID(id);

            ViewBag.id = obj.SongID;
            ViewBag.singer = obj.Singer;
            ViewBag.name = obj.Name;

            return View();
        }

        public ActionResult Edit2()
        {
            if (Session["Login"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            int id = Convert.ToInt32(Request["txtID"]);
            String name = Request["txtName"];
            String singer = Request["txtSinger"];

            Models.DBHelper.updateSong(id, name, singer);
            
            return View("Songs");
        }

        public ActionResult Delete(int id)
        {
            if (Session["Login"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Models.SongsDTO obj = Models.DBHelper.getSongByID(id);

            ViewBag.link = obj.Link;
            String link = obj.Link;

            var path = Path.Combine(Server.MapPath("~/Content/songs"), link);
            FileInfo f = new FileInfo(path);
            f.Delete();


            Models.DBHelper.deleteSong(id);

            return View("Songs");
        }
    }
}