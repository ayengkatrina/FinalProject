using BusinessLogicLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace PasteBook.Controllers
{
    public class PasteBookController : Controller
    {
        DataAccess dataAccess = new DataAccess();
        MVCManager manager = new MVCManager();
      
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
            bool ifMatched = manager.Login(model.EmailAddress, model.Password);
            if (ifMatched)
            {
                string email = model.EmailAddress;
                UserModel userModel = manager.GetUserAccount(email);
                Session["userSession"] = userModel;
                return RedirectToAction("Home", "PasteBook");
            }
            else
            {
                return View(model);
            }         
        }

        public PartialViewResult HomePost()
        {
            List<PostModel> result = new List<PostModel>();
             result = manager.GetAllPost();

            return PartialView( "HomePost", result );
        }

        //public PartialViewResult Comment()
        //{
        //    List<CommentModel> result = new List<CommentModel>();

        //    result = manager.GetComments()

        //}
      
        [HttpGet]
        public ActionResult Home()
        {

            return View() ;
        }

        [HttpPost]
        public ActionResult Home(PostModel post)
        {
            UserModel userModel = (UserModel)Session["userSession"];
            //PostModel post = new PostModel();
           

            if (post.Content != null)
            {
                post.PosterID = userModel.UserID;
                post.ProfileID = userModel.UserID;
                manager.CreatePost(post);
                return View();
            }else
            {
                return View();
            }
        }

        


        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserModel model, HttpPostedFileBase file)
        {
            if (manager.CheckEmailIfAlreadyExist(model.EmailAddress.ToString()))
            {
                ModelState.AddModelError("emailExist", "Email address already exist");
                return View();
            }
            else
            {
                if (file != null)
                {

                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                    var checkextension = Path.GetExtension(file.FileName).ToLower();

                    if (allowedExtensions.Contains(checkextension))
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

                        bool registered = manager.RegisterAccount(model);


                        return RedirectToAction("CreatedAccount", "PasteBook");

                    }
                    else
                    {
                        ModelState.AddModelError("ImageExtensionValidation", "Please select .jpg, or .jpeg, or .png file to upload");
                        return View();
                    }
                }

                else
                {
                    bool registered = manager.RegisterAccount(model);
                    if (registered == true)
                    {
                        return RedirectToAction("CreatedAccount", "PasteBook");
                    }
                    else
                    {
                        return View();
                    }
                }
            }
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
        public ActionResult ProfileInfo()
        {
            UserModel userModel = (UserModel) Session["userSession"];

           // string email = userModel.EmailAddress;

          

            return View(userModel);
        }


        [HttpGet]
        public ActionResult Logout()
        {


            return View();
        }
    }
}