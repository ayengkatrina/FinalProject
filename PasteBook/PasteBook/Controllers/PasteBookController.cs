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
        FriendManager friendManager = new FriendManager();

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

                Session["userID"] = userModel.ID;
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

        


        [HttpGet]
        public ActionResult Home()
        {
            string email = (string)Session["email"];
            USER_TABLE user = userManager.GetUserDetails(email);
            int ID = user.ID;

            List<NEWSFEEDPOST_Result> result = new List<NEWSFEEDPOST_Result>();
            result = postManager.NewsFeedPost(ID);
            return View(result);
        }

       

        

        public PartialViewResult Comment(int ID)
        {

            
            var result = commentManager.GetComments(ID);
           
           return PartialView("Comment", result);
           

        }

        [HttpGet]
        public PartialViewResult MakeComment()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult MakeComment(int ID, COMMENTS_TABLE comment)
        {
            var createComment = false;
            string email = (string)Session["email"];
            USER_TABLE user = userManager.GetUserDetails(email);
            if(comment.CONTENT != null) {
                comment.POST_ID = ID;
                comment.DATE_CREATED = DateTime.Now;
                comment.POSTER_ID = user.ID;
                createComment = commentManager.CommentOnAPost(comment);
            }

            return RedirectToAction("Home", "PasteBook");
        }

       

        [HttpGet]
        public ActionResult Profile()


        {
            UserProfileModel userProfile = new UserProfileModel();
            //userProfile.User = (USER_TABLE)Session["userSession"];
            string email = (string)Session["email"];
            Session["userID"] = userProfile.User.ID;

            userProfile.User = userManager.GetUserDetails(email);

            return View(userProfile);
        }

        public ActionResult UploadPicture(HttpPostedFileBase file)
        {
            //stackoverflow.com/questions/16255882/uploading-displaying-images-in-mvc-4
            if (file != null)
            {
                string email = (string)Session["email"];
                using (MemoryStream ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                    //byte[] array = ms.GetBuffer();
                    byte[] profilePic = ms.GetBuffer();
                    var addingPicResult = userManager.AddProfilePicture(profilePic, email);
                }               
               
                return RedirectToAction("Profile", "PasteBook");

            }
            else
            {
                return RedirectToAction("Profile", "PasteBook");
            }

        }
        public ActionResult AddAboutMe(string txtaboutMe)
        {
            string email = (string)Session["email"];
            var addingAboutMeResult = userManager.AddAboutMe(txtaboutMe, email);
            return RedirectToAction("Profile", "PasteBook");
        }

        public PartialViewResult ProfilePost(int ID)
        {
            var postList = postManager.TimelinePost(ID);
            return PartialView("ProfilePost", postList);
        }

        [HttpGet]
        public ActionResult Friends()
        {
            return View();
        }


        public PartialViewResult CommentList(int ID)
        {


            var result = commentManager.GetComments(ID);

            return PartialView("Comment", result);


        }

        [HttpGet]
        public PartialViewResult MakeCommentOnPost()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult MakeCommentOnPost(int ID, COMMENTS_TABLE comment)
        {
            var createComment = false;
            string email = (string)Session["email"];
            USER_TABLE user = userManager.GetUserDetails(email);
            if (comment.CONTENT != null)
            {
                comment.POST_ID = ID;
                comment.DATE_CREATED = DateTime.Now;
                comment.POSTER_ID = user.ID;
                createComment = commentManager.CommentOnAPost(comment);
            }

            return RedirectToAction("Profile", "PasteBook");
        }

        [HttpGet]
        public ActionResult Settings()
        {
            string email = (string)Session["email"];
            USER_TABLE user = userManager.GetUserDetails(email);

            

            return View(user);
        }

        [HttpPost]
        public ActionResult Settings(USER_TABLE userModel)
        {
            string email = (string)Session["email"];
            var editProfileResult = userManager.EditProfile(userModel, email);

            return View(userModel);
        }

        [HttpGet]
        public ActionResult Security()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Security(SecurityModel model)
        {
            int userID = (int)Session["userID"];
            USER_TABLE user = userManager.GetUserDetailsByID(userID);
            bool isPasswordMatch = userManager.IsPasswordMatch(model.CurrentPassword, user.SALT, user.PASSWORD);
            if (isPasswordMatch)
            {
                string saltResult = null;

                string hash = userManager.GeneratePasswordHash(model.NewPassword, out saltResult);
                string salt = saltResult;

                userManager.EditCredential(userID, model.NewEmailAddress, hash, salt);
                return RedirectToAction("Settings", "PasteBook");
            }
            else
            {
                ModelState.AddModelError("passwordNotMatch", "Wrong password");
                return RedirectToAction("Settings", "PasteBook");
            }

           

        }

        [HttpGet]
        public ActionResult Search()
        {
            return View();
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
