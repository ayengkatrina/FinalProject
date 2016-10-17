using BusinessLogicLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PasteBook.Controllers
{
    public class PasteBookController : Controller
    {
        DataAccess dataAccess = new DataAccess();
        // GET: PasteBook
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HomePage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult HomePage(UserModel model)
        {
            bool ifMatched = dataAccess.SimulateLogin(model.EmailAddress, model.Password);
            if (ifMatched)
            {
                return RedirectToAction("AlreadyLogin", "PasteBook");
            }
            else
            {
                return View(model);
            }

            
        }

        [HttpGet]
        public ActionResult AlreadyLogin()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserModel model, HttpPostedFileBase file)
        {

            //stackoverflow.com/questions/16255882/uploading-displaying-images-in-mvc-4

            if (file != null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/Content/Images"), pic);
                // file is uploaded
                file.SaveAs(path);

                // save the image path path to the database or you can send image 
                // directly to database
                // in-case if you want to store byte[] ie. for DB
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    //byte[] array = ms.GetBuffer();
                    model.ProfilePic = ms.GetBuffer();
                }
                dataAccess.SimulateUserCreation(Mapper.ToUser(model));

                return RedirectToAction("CreatedAccount", "PasteBook");
            }



            return View();
        }

        public ActionResult CreatedAccount()
        {
            return View();
        }
    }
}