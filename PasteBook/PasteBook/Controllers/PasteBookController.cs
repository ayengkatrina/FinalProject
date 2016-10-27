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

            if(Session["userID"] != null)
            {
                return RedirectToAction("Home", "PasteBook");
            }else
            {
                return View();
            }
            

           
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
                Session["profilePic"] = userModel.PROFILE_PIC;
                Session["userName"] = userModel.USER_NAME;
                Session["userFullName"] = userModel.FIRST_NAME + " " + userModel.LAST_NAME;
                return RedirectToAction("Home", "PasteBook");
            }
            else
            {
                ModelState.AddModelError("loginValidationMessage", "Email or password does not match our records, Please double check and try again.");
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
        public ActionResult Register(USER_TABLE model, HttpPostedFileBase file)
        {
            bool checkUserName = userManager.CheckIfUserNameAlreadyExist(model.USER_NAME);
            bool checkEmail = userManager.CheckIfEmailAlreadyExist(model.EMAIL_ADDRESS);

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
                    bool registered = userManager.SimulateUserCreation(model);
                    string email = model.EMAIL_ADDRESS;
                    USER_TABLE userDetails = userManager.GetUserDetails(email);
                    Session["userID"] = userDetails.ID;
                    Session["profilePic"] = userDetails.PROFILE_PIC;
                    Session["userName"] = userDetails.USER_NAME;
                    Session["userFullName"] = userDetails.FIRST_NAME + " " + userDetails.LAST_NAME;


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

            int? ID = (int?)Session["userID"];
            if (ID != null)
            {
                int userID = (int)ID;
                

                List<POST_TABLE> postList = new List<POST_TABLE>();
                postList = postManager.PostInTheNewsFeed(userID);
                return View(postList);


            }
            else
            {
                return RedirectToAction("HomePage", "PasteBook");
            }

           
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
            int userID = (int)Session["userID"];
            
            if(comment.CONTENT != null) {
                comment.POST_ID = ID;
                comment.DATE_CREATED = DateTime.Now;
                comment.POSTER_ID = userID;
                createComment = commentManager.CommentOnAPost(comment);
            }

            return RedirectToAction("Home", "PasteBook");
        }


        
        [HttpGet]
        public ActionResult Profile()
        {
           
            int? userID = (int?)Session["userID"];
            if(userID != null)
            {
                int ID = (int)userID;
                UserProfileModel userProfile = new UserProfileModel();

                userProfile.User = userManager.GetUserDetailsByID(ID);
                return View(userProfile);
            }
            else
            {
               return RedirectToAction("HomePage", "PasteBook");
            }

           
        }

        [HttpPost]
        public ActionResult MakeAPost(string postContent, int profileOwnerID )
        {
            if(postContent != null)
            {
                int posterID = (int)Session["userID"];
                POST_TABLE postTable = new POST_TABLE();
                postTable.CONTENT = postContent;

                postTable.PROFILE_ID = profileOwnerID;
                postTable.POSTER_ID = posterID;
                var postCreationResult = postManager.CreatePost(postTable);
            }
            

            return RedirectToAction("Profile", "PasteBook");
        }

        public ActionResult UploadPicture(HttpPostedFileBase file)
        {
            //stackoverflow.com/questions/16255882/uploading-displaying-images-in-mvc-4
            if (file != null)
            {
                int userID = (int)Session["userID"];
                using (MemoryStream ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                    //byte[] array = ms.GetBuffer();
                    byte[] profilePic = ms.GetBuffer();
                    var addingPicResult = userManager.AddProfilePicture(profilePic, userID);
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
            int userID = (int)Session["userID"];
            var addingAboutMeResult = userManager.AddAboutMe(txtaboutMe, userID);
            return RedirectToAction("Profile", "PasteBook");
        }

        public PartialViewResult ProfilePost(int ID)
        {
            var postList = postManager.TimelinePost(ID);
            return PartialView("ProfilePost", postList);
        }
      


        public PartialViewResult CommentList(int ID)
        {


            var result = commentManager.GetComments(ID);

           return PartialView("Comment", result);


        }

        [HttpGet]
        public PartialViewResult MakeCommentOnPost(int ID)
        {
            return PartialView();
        }

       


        [HttpPost]
        public ActionResult MakeCommentOnPost(int ID, COMMENTS_TABLE comment)
        {
            var createComment = false;

           
            int userID = (int)Session["userID"];           
            if (comment.CONTENT != null)
            {
                comment.POST_ID = ID;
                comment.DATE_CREATED = DateTime.Now;
                comment.POSTER_ID = userID;
                createComment = commentManager.CommentOnAPost(comment);
                
            }
           return RedirectToAction("Profile", "PasteBook"); 
           
        }

        [HttpGet]
        public ActionResult LikeAPostProfile()
        {

            return View();
        }

        [HttpPost]
        public JsonResult LikeAPostProfile(int postID)
        {
            int userID = (int)Session["userID"]; 
            LIKES_TABLE likeModel = new LIKES_TABLE();
            likeModel.LIKED_BY = userID;
            likeModel.POST_ID = postID;
            var result = likeManager.LikeAPost(likeModel);
            
            return Json(JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult LikeAPostHome(int postID)
        {
            int userID = (int)Session["userID"];
            LIKES_TABLE likeModel = new LIKES_TABLE();
            likeModel.LIKED_BY = userID;
            likeModel.POST_ID = postID;
            var result = likeManager.LikeAPost(likeModel);

            return Json(JsonRequestBehavior.AllowGet);

        }

        public JsonResult LikeListOfUsers(int postID)
        {
            

            return Json(JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult Settings()
        {
            int? ID = (int?)Session["userID"];
            if (ID != null)
            {
                int userID = (int)ID;
                
                USER_TABLE user = userManager.GetUserDetailsByID(userID);

                if (user.GENDER == "F")
                {
                    user.GENDER = "Female";
                }else if(user.GENDER == "M")
                {
                    user.GENDER = "Male";
                }
                else if(user.GENDER == "U")
                {
                    user.GENDER = "";
                }

                return View(user);

            }
            else
            {
                return RedirectToAction("HomePage", "PasteBook");
            }


            
        }

        [HttpPost]
        public ActionResult Settings(USER_TABLE userModel)
        {
            int userID = (int)Session["userID"];
            var editProfileResult = userManager.EditProfile(userModel, userID);

            return View(userModel);
        }

        
        [HttpGet]
        public ActionResult Security()
        {
            int? ID = (int?)Session["userID"];
            if (ID != null)
            {

                return View();

            }
            else
            {
                return RedirectToAction("HomePage", "PasteBook");
            }

           
        }

        [HttpPost]
        public ActionResult Security(SecurityModel model)
        {
            int userID = (int)Session["userID"];
            USER_TABLE user = userManager.GetUserDetailsByID(userID);
            bool isPasswordMatch = userManager.IsPasswordMatch(model.CurrentPassword, user.SALT, user.PASSWORD);
            if (isPasswordMatch && model.NewPassword != null && model.NewEmailAddress != null)
            {
                string saltResult = null;

                string hash = userManager.GeneratePasswordHash(model.NewPassword, out saltResult);
                string salt = saltResult;

               bool result= userManager.EditCredential(userID, model.NewEmailAddress, hash, salt);
                if(result == true)
                {
                    ModelState.AddModelError("changesSave", "Wrong password");
                    return View("Settings", "PasteBook");
                }

                return View("Settings", "PasteBook");
            }
            else if(isPasswordMatch && model.NewPassword == null && model.NewEmailAddress != null)
            {
                bool result =userManager.EditEmailAddress(userID, model.NewEmailAddress);

                if (result)
                {
                    ModelState.AddModelError("changesSave", "Wrong password");
                    return View("Settings", "PasteBook");
                }
                return View("Settings", "PasteBook");
            }
            else if(isPasswordMatch && model.NewPassword != null && model.NewEmailAddress == null)
            {
                string saltResult = null;

                string hash = userManager.GeneratePasswordHash(model.NewPassword, out saltResult);
                string salt = saltResult;
               bool result = userManager.EditPassword(userID, hash, salt);
                if (result)
                {
                    ModelState.AddModelError("changesSave", "Wrong password");
                    return View("Settings", "PasteBook");
                }
                return View("Settings", "PasteBook");
            }
            else if(isPasswordMatch == false && model.CurrentPassword != null)
            {
                ModelState.AddModelError("passwordNotMatch", "Wrong password");
                return View("Settings", "PasteBook");
            }

            return View("Settings", "PasteBook");

        }


        
        [HttpGet]
        public ActionResult Friends()
        {

            int? ID = (int?)Session["userID"];
            if (ID != null)
            {
                int userID = (int)ID;

                
                List<USER_TABLE> friendsList = new List<USER_TABLE>();
                friendsList = friendManager.FriendsList(userID);
                return View(friendsList);

            }
            else
            {
                return RedirectToAction("HomePage", "PasteBook");
            }

           
        }


        public ActionResult FriendProfile(int userID)
        {
          
            UserProfileModel userProfile = new UserProfileModel();
           
            userProfile.User = userManager.GetUserDetailsByID(userID);

            return View("Profile",userProfile);
        }

       

        
        public ActionResult Search(string txtSearch)
        {
            List<USER_TABLE> registeredLlist = new List<USER_TABLE>();

            registeredLlist = userManager.Search(txtSearch);

            return View("Search",registeredLlist);
        }

        public ActionResult UserProfileFilter(int? userIDOfFriend)
        {
            int userID = (int)Session["userID"];
            var checkIfFriend = friendManager.CheckIfFriend((int)userIDOfFriend, userID);
            var checkIfTheyAlreadySendArequest = friendManager.CheckIfFriendAlreadySentRequest((int)userIDOfFriend, userID);
            var checkIfYouAlreadySendARequest = friendManager.CheckIfYouAlreadySendAnInvite((int)userIDOfFriend, userID);
          
             if(checkIfTheyAlreadySendArequest)
            {
                //view confirm request

                ViewBag.RequestStatus = "theySendARequest";
                UserProfileModel userProfile = new UserProfileModel();

                userProfile.User = userManager.GetUserDetailsByID((int)userIDOfFriend);
                return View("NonFriend", userProfile);
                //return Json(Url.Action("Index", "Home", new {friendID = userIDOfFriend }));

            }
            else if (checkIfYouAlreadySendARequest)
            {
                //view pending request
                ViewBag.RequestStatus = "youSendARequest";
                UserProfileModel userProfile = new UserProfileModel();

                userProfile.User = userManager.GetUserDetailsByID((int)userIDOfFriend);
                return View("NonFriend", userProfile);
            }
            else if (checkIfFriend)
            {
               
                UserProfileModel userProfile = new UserProfileModel();

                userProfile.User = userManager.GetUserDetailsByID((int)userIDOfFriend);
                return View("Profile", userProfile);
            }else
            { //view add as friend
                ViewBag.RequestStatus = "noRequestAtAll";
                UserProfileModel userProfile = new UserProfileModel();

                userProfile.User = userManager.GetUserDetailsByID((int)userIDOfFriend);
                return View("NonFriend", userProfile);
            }
            
        }

        [HttpPost]
        public ActionResult ConfirmFriendRequest(int friendID)
        {
            int userID = (int)Session["userID"];
            

            var checkIfConfirmed = friendManager.ConfirmFriendRequest((int)friendID, userID);
            if (checkIfConfirmed)
            {
                return RedirectToAction("UserProfileFilter", "PasteBook", new { userIDOfFriend = friendID });
            }else
            {
                return RedirectToAction("UserProfileFilter", "PasteBook", new { userIDOfFriend = friendID });
            }


        }

       [HttpPost]
       public ActionResult RejectFriendRequest(int friendID)
        {
            int userID = (int)Session["userID"];

            var checkIfRejectionSuccessful = friendManager.CheckIfRejectionSuccess(friendID, userID);
            if (checkIfRejectionSuccessful)
            {
                return RedirectToAction("UserProfileFilter", "PasteBook", new { userIDOfFriend = friendID });
            }
            else
            {
                return RedirectToAction("UserProfileFilter", "PasteBook", new { userIDOfFriend = friendID });
            }

        }

        [HttpPost]
        public ActionResult SendFriendRequest(int friendID)
        {
            int userID = (int)Session["userID"];

            var checkIfRequestSent = friendManager.SendFriendRequest((int)friendID, userID);
            if (checkIfRequestSent)
            {
                return RedirectToAction("UserProfileFilter", "PasteBook", new { userIDOfFriend = friendID });
            }
            else
            {
                return RedirectToAction("UserProfileFilter", "PasteBook", new { userIDOfFriend = friendID });
            }

        }




        public ActionResult Notifications()
        {
            return View();
        }

       
        [HttpGet]
        public ActionResult Logout()
        {

            int? ID = (int?)Session["userID"];
            if (ID != null)
            {

                Session["userID"] = null;
                Session["profilePic"] = null;
                Session["userName"] = null;

                return RedirectToAction("HomePage", "PasteBook");


            }
            else
            {
                return RedirectToAction("HomePage", "PasteBook");
            }

            
        }




    }

}     
