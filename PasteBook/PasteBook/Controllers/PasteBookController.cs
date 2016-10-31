using BusinessLogicLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using PasteBookEFLibrary;


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
        RefCountryManger countryManager = new RefCountryManger();
        NotificationManager notificationManager = new NotificationManager();

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
            



            ViewBag.Country = countryManager.GetAllCountry();





            return View();
        }

        [HttpPost]

        public ActionResult Register(CreateAccountViewModel model, HttpPostedFileBase file)
        {
            ViewBag.CountryList = new SelectList(countryManager.GetAllCountry(), "ID", "COUNTRY");
            bool checkUserName = userManager.CheckIfUserNameAlreadyExist(model.User.USER_NAME);
            bool checkEmail = userManager.CheckIfEmailAlreadyExist(model.User.EMAIL_ADDRESS);

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
                    bool registered = userManager.SimulateUserCreation(model.User);
                    string email = model.User.EMAIL_ADDRESS;
                    USER_TABLE userDetails = userManager.GetUserDetails(email);
                    Session["userID"] = userDetails.ID;
                    Session["profilePic"] = userDetails.PROFILE_PIC;
                    Session["userName"] = userDetails.USER_NAME;



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
                return View("ProfilePost",postList);


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
        public ActionResult MakeComment(int postID, string comment, COMMENTS_TABLE commentTable)
        {
            var createComment = false;
            int userID = (int)Session["userID"];

            
            if(comment != null) {
                commentTable.POST_ID = postID;
                commentTable.DATE_CREATED = DateTime.Now;
                commentTable.POSTER_ID = userID;
                commentTable.CONTENT = comment;
                createComment = commentManager.CommentOnAPost(commentTable);
               
                if (createComment)
                {
                    int receiverID = postManager.GetPosterID(commentTable.POST_ID);
                    bool result = notificationManager.NotificationComment(receiverID, userID, commentTable.POST_ID, commentTable.ID);
                    return Json(new { createComment = createComment }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { createComment = createComment }, JsonRequestBehavior.AllowGet);
                }
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
        public ActionResult MakeAPost(string postContent, int profileID)
        {
            if(postContent != null)
            {
                int posterID = (int)Session["userID"];
                POST_TABLE postTable = new POST_TABLE();
                postTable.CONTENT = postContent;

                postTable.PROFILE_ID = profileID;
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
                    if (addingPicResult)
                    {
                        USER_TABLE user = userManager.GetUserDetailsByID(userID);
                        Session["profilePic"] = user.PROFILE_PIC;
                    }
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
      
        
        [HttpPost]
        public JsonResult LikeAPostHome(int postID)
        {
            int userID = (int)Session["userID"];
            LIKES_TABLE likeModel = new LIKES_TABLE();
            likeModel.LIKED_BY = userID;
            likeModel.POST_ID = postID;
            var resultLike = likeManager.LikeAPost(likeModel);
            if (resultLike)
            {
                int receiverID = postManager.GetPosterID(postID);
                bool result = notificationManager.NotificationLike(receiverID, userID, postID);
                return Json(new { resultLike = resultLike }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { resultLike = resultLike }, JsonRequestBehavior.AllowGet);
            }

           

        }

        public JsonResult LikeListOfUsers(int postID)
        {
            var userListWhoLikeAPost = userManager.ListOfUserWhoLikeAPost(postID);

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
                var notifAddFriend = notificationManager.NotificationFriendRequest(friendID, userID);
                return RedirectToAction("UserProfileFilter", "PasteBook", new { userIDOfFriend = friendID });
            }
            else
            {
                return RedirectToAction("UserProfileFilter", "PasteBook", new { userIDOfFriend = friendID });
            }

        }




        public ActionResult Notifications()
        {
            int userID = (int)Session["userID"];
            List<NOTIFICATION_TABLE> notifLlist = notificationManager.NotifList(userID);

            return View(notifLlist);
        }

        public ActionResult Post( int? postID)
        {
            POST_TABLE post = new POST_TABLE();

            post = postManager.Post((int)postID);

            return View("Post",post);
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
