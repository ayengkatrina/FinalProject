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
        Manager manager = new Manager();
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
                string email = model.EmailAddress;
                Session["userSession"] = email;



                return RedirectToAction("Home", "PasteBook");
            }
            else
            {
                return View(model);
            }

            
        }

        [HttpGet]
        public ActionResult Home()
        {
           
          var post =  dataAccess.GetPost();

            var result = manager.GetAllPost();



            return View(result);
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
                if (file.ContentLength < 4096 * 1024)
                {
                    string pic = System.IO.Path.GetFileName(file.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/Content/Images"), pic);
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
                else
                {


                }
            }else
            {
                dataAccess.SimulateUserCreation(Mapper.ToUser(model));
            }



            return View();
        }

        public ActionResult CreatedAccount()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Profile()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Search()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {


            return View();
        }

        [HttpGet]
        public ActionResult ProfileInfo()
        {
            var email = Session["userSession"].ToString();

           UserModel model = Mapper.ToUserModel(dataAccess.GetUserAccount(email));

            return View(model);
        }
    }
}