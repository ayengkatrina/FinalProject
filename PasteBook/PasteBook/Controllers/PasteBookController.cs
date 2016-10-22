using BusinessLogicLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using PasteBookEntityLibrary;


namespace PasteBook.Controllers
{
    public class PasteBookController : Controller
    {
        UserManager userManager = new UserManager();
        PostManager postManager = new PostManager();
        RefCountryManger refCountryManager = new RefCountryManger();
        LikeManager likeManager = new LikeManager();
        CommentManager commentManager = new CommentManager();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult HomePage()
        {

            return View();
        }

        [HttpPost]
        public ActionResult HomePage(USER_TABLE model)
        {
            string email = model.EMAIL_ADDRESS.ToString();
            string password = model.PASSWORD.ToString();

            bool ifMatched = userManager.SimulateLogin(email, password);
            if (ifMatched)
            {
                string emailAd = model.EMAIL_ADDRESS;

                USER_TABLE userModel = userManager.GetUserDetails(emailAd);

                Session["email"] = userModel.EMAIL_ADDRESS;
                //Session["userSession"] = userModel;
                return RedirectToAction("Home", "PasteBook");
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(PBRegisterViewModel model, HttpPostedFileBase file)
        {
            bool checkUserName = userManager.CheckIfUserNameAlreadyExist(model.userTable.USER_NAME);
            bool checkEmail = userManager.CheckIfEmailAlreadyExist(model.userTable.EMAIL_ADDRESS);

            if (ModelState.IsValid)
            {
                if ((checkUserName == true) && (checkEmail == true))
                {
                    ModelState.AddModelError("usernameExist", "User name already exist");
                    ModelState.AddModelError("emailExist", "Email address already exist");
                    return View();
                }
                else if ((checkUserName == true) && (checkEmail == false))
                {
                    ModelState.AddModelError("usernameExist", "User name already exist");
                    return View();
                }
                else if ((checkUserName == false) && (checkEmail == true))
                {
                    ModelState.AddModelError("emailExist", "Email address already exist");
                    return View();
                }
                else /*if ((checkUserName == false) && (checkEmail == false))*/
                {
                    bool registered = userManager.SimulateUserCreation(model.userTable);
                    Session["email"] = model.userTable.EMAIL_ADDRESS;


                    return RedirectToAction("Profile", "PasteBook");
                }


            }
            else
            {
                return View();
            }

           


        }

        //public JsonResult ValidateEmail(string email)
        //{
        //    bool check = userManager.CheckIfEmailAlreadyExist(email);

        //    return Json(new { Email = Email }, JsonRequestBehavior.AllowGet);
        //}
    



   


        [HttpGet]
        public ActionResult Home()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Home(POST_TABLE post)
        {
            string email = (string)Session["email"];
            USER_TABLE user = userManager.GetUserDetails(email);
            //PostModel post = new PostModel();


            if (post.CONTENT != null)
            {
                post.POSTER_ID = user.ID;
                post.PROFILE_ID = user.ID;
                postManager.CreatePost(post);
                return View();
            }
            else
            {
                return View();
            }
        }




        public PartialViewResult HomePost()
        {
            
            string email = (string)Session["email"];
            USER_TABLE user = userManager.GetUserDetails(email);
            int ID = user.ID;
           

            List<NEWSFEEDPOST_Result> result = new List<NEWSFEEDPOST_Result>();
            result = postManager.NewsFeedPost(ID);

            return PartialView("HomePost", result);
        }

        public PartialViewResult ProfilePost()

        {
            int profileID = 5;
           
            List<NEWSFEEDPOST_Result> result = new List<NEWSFEEDPOST_Result>();
            result = postManager.NewsFeedPost(profileID);
            return PartialView("ProfilePost", result);
        }

        public PartialViewResult Comment(int ID)
        {

            
            var result = commentManager.GetComments(ID);
           
           return PartialView("Comment", result);
           

        }

        public PartialViewResult MakeComment(int ID, COMMENTS_TABLE comment)
        {
            if(comment == null) {
                comment.POST_ID = ID;
                var createComment = commentManager.CommentOnAPost(comment);
            }
            
            return PartialView();
        }



        [HttpGet]
        public ActionResult Profile()


        {
            UserProfileModel userProfile = new UserProfileModel();
            //userProfile.User = (USER_TABLE)Session["userSession"];
            string email = (string)Session["email"];

            userProfile.User = userManager.GetUserDetails(email);

            return View(userProfile);
        }

        [HttpGet]
        public ActionResult Search()
        {
            return View();
        }



        [HttpGet]
        public ActionResult ProfileInfo()
        {
            ProfileViewModel profileModel = (ProfileViewModel)Session["userSession"];

            // string email = userModel.EmailAddress;



            return View(profileModel);
        }

        public ActionResult Notifications()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Logout()
        {
            Session["email"] = null;



            return RedirectToAction("HomePage", "PasteBook"); 
        }
    }

}     
