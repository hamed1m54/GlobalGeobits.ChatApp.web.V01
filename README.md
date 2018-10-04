# GlobalGeobits.ChatApp

* ChatApp that have:
* User Registration page
* user Login page
* User Home page (Chat DashBoard)


# Screen-Shot

![alt text](https://github.com/hamed1m54/GlobalGeobits.ChatApp.web.V01/blob/master/Capture1.PNG)

![alt text](https://github.com/hamed1m54/GlobalGeobits.ChatApp.web.V01/blob/master/Capture2.PNG)


## Used Technologies
* [ASP MVC](https://docs.microsoft.com/en-us/previous-versions/aspnet/dd381412(v=vs.108))
* [C#](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/)
* [SQL database](https://docs.microsoft.com/en-us/sql/sql-server/sql-server-technical-documentation?view=sql-server-2017)
* [SignalR](http://signalr.net/)
* [Entity framework 6 CodeFirst](https://msdn.microsoft.com/en-us/magazine/dn519921.aspx)

### Prerequisites

* .Net FrameWork 4.2
* VisualStudio 2013

## Models Classes

Business Models Clases that hold all requried classes and its proberties

## User Model class
user class that hold all User proberites and validation on each proberity

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GlobalGeobits.ChatApp.web.Models
{


    public class Users
    {
        [Key]
       
        public int UserID { get; set; }


        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "Validation_reqired")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "Validation_maxlength")]
        [Display(ResourceType = typeof(Resources.Resource), Name = "lbl_fn")]
        public string UserFristName { get; set; }


        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "Validation_reqired")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "Validation_maxlength")]
        [Display(ResourceType = typeof(Resources.Resource), Name = "lbl_ln")]
        public string UserLastName { get; set; }


        [Display(ResourceType = typeof(Resources.Resource), Name = "lbl_dn")]
        public string UserDisplayName { get; set; }


        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "Validation_reqired")]
        [Display(ResourceType = typeof(Resources.Resource), Name = "lbl_gender")]
        [UIHint("Gender")]
        public string UserGender { get; set; }


        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "Validation_reqired")]
        [Display(ResourceType = typeof(Resources.Resource), Name = "lbl_dob")]
        [DataType(DataType.Date)]
        [plus18(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "Validation_birthdate")]
        public DateTime UserDateOfBirth { get; set; }


        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "Validation_reqired")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "Validation_invalid_mail")]
        [Display(ResourceType = typeof(Resources.Resource), Name = "lbl_mail")]
        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }


        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "Validation_reqired")]
        [RegularExpression(@"^((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%]).{6,20})$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "Validation_invalid_pass")]
        [Display(ResourceType = typeof(Resources.Resource), Name = "lbl_pass")]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }


        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "Validation_reqired")]
        [Compare("UserPassword", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "Validation_Confirmpass")]
        [Display(ResourceType = typeof(Resources.Resource), Name = "lbl_confirm_pass")]
        [DataType(DataType.Password)]
        public string UserConfirmPassword { get; set; }

        // the list of all sent messages by a spasific user
        public virtual ICollection<Messages> SentMessages { get; set; }

        // the list of all received messages by a spasific user
        public virtual ICollection<Messages> ReceivedMessages { get; set; }


    }

    /// <summary>
    /// a custom validation attribute that validate that user age is plus 18 years
    /// </summary>
 
    public sealed class plus18 : ValidationAttribute
    {

        /// <summary>
        /// overrided IsValid method that check the validation 
        /// So, if user age is plus 18 so it valid and return true
        /// else return false is no Date of Birhth is enterd or age is less than 18
        /// </summary>
        /// <param name="value"> Value is the Date of Birth of the user </param>
        /// <returns>
        /// the Isvalid method return True is user age is equal or plus 18 Years,
        /// return false otherwise
        /// 
        /// </returns>
        public override bool IsValid(object value)
        {

            var DOB = value as DateTime?;

            if (DOB == null) return false;
            if (DOB != null && DOB.HasValue) {

                int ageYears = DateTime.Now.Year - DOB.Value.Year;

                if (ageYears >= 18)
                    return true;
            
            }
            return false;
        }
    }
}

```
## Messages Model Class
Once we use the EntityFrameWork 6 (CodeFirst Approch), we must declare the messages tables 
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GlobalGeobits.ChatApp.web.Models
{
    public class Messages
    {

        public int ID { get; set; }

        public string MessageContent { get; set; }

        public int MessageSenderID { get; set; }

        public int MessageReceiverID { get; set; }

        public DateTime MessageSentDateTime { get; set; }


    }
}
```
## DB Context class
 A DbContext instance represents a combination of the Unit Of Work and Repository patterns such that it can be used to query from a database and group together changes that will then be written back to the store as a unit. DbContext is conceptually similar to ObjectContext.
 ```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace GlobalGeobits.ChatApp.web.Models
{
    public class ChatAppDbContext : DbContext
    {

        //map the users model class to SQL Database table
        public DbSet<Users> Users { get; set; }

        //map the users model class to SQL Database table
        public DbSet<Messages> Messages { get; set; }
    }
}
```
### GlobalResource
A resource file is an XML file that can contain strings and other resources, such as image file paths. Resource files are typically used to store user interface strings that must be translated into other languages. This is because you can create a separate resource file for each language into which you want to translate a Web page.

Global resource files are available to any page or component in your Web site. Local resource files are associated with a single Web page, user control, or master page, and contain the resources for only that page. For more information, [see ASP.NET Web Page Resources Overview](https://msdn.microsoft.com/en-us/library/ms227427.aspx).

In Visual Web Developer, you can use the managed resource editor to create global or local resource files. For local resource files, you can also generate a culturally neutral base resource file directly from a Web page in the designer.

## Adding Resource File 
* right clik on project solution
* Add -> ASP Folder
* App_GlobalResources
* Right clik on App_GlobalResources
* Add-> resource file

## Resource.designer.cs
```csharp
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option or rebuild the Visual Studio project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Web.Application.StronglyTypedResourceProxyBuilder", "12.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public Resource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager
        {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Resources.Resource", global::System.Reflection.Assembly.Load("App_GlobalResources"));
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture
        {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Confirm Password.
        /// </summary>
        public static string lbl_confirm_pass {
            get {
                return ResourceManager.GetString("lbl_confirm_pass", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Display Name.
        /// </summary>
        public static string lbl_dn
        {
            get {
                return ResourceManager.GetString("lbl_dn", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Birth Date.
        /// </summary>
        public static string lbl_dob
        {
            get {
                return ResourceManager.GetString("lbl_dob", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Frist Name.
        /// </summary>
        public static string lbl_fn
        {
            get {
                return ResourceManager.GetString("lbl_fn", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Gender.
        /// </summary>
        public static string lbl_gender
        {
            get {
                return ResourceManager.GetString("lbl_gender", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Last Name.
        /// </summary>
        public static string lbl_ln
        {
            get {
                return ResourceManager.GetString("lbl_ln", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Email Adress.
        /// </summary>
        public static string lbl_mail
        {
            get {
                return ResourceManager.GetString("lbl_mail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to PassWord.
        /// </summary>
        public static string lbl_pass
        {
            get {
                return ResourceManager.GetString("lbl_pass", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Your Age Must be equal or over 18 years.
        /// </summary>
        public static string Validation_birthdate
        {
            get {
                return ResourceManager.GetString("Validation_birthdate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The password Are Not Matched.
        /// </summary>
        public static string Validation_Confirmpass
        {
            get {
                return ResourceManager.GetString("Validation_Confirmpass", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please, Enter a valid mail &apos;yourName@Example.com&apos;.
        /// </summary>
        public static string Validation_invalid_mail
        {
            get {
                return ResourceManager.GetString("Validation_invalid_mail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The PassWord must contains at least One digit, One lowerCase character, One UpperCase character, one special symbols in the list &quot;@#$%&quot; and must be from 6 to 20 characters.
        /// </summary>
        public static string Validation_invalid_pass
        {
            get {
                return ResourceManager.GetString("Validation_invalid_pass", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The max length of this filed is 100.
        /// </summary>
        public static string Validation_maxlength
        {
            get {
                return ResourceManager.GetString("Validation_maxlength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This Field is Requried.
        /// </summary>
        public static string Validation_reqired
        {
            get {
                return ResourceManager.GetString("Validation_reqired", resourceCulture);
            }
        }
    }
}
```

 ### Controlers
 In this section, you will learn about the Controller in ASP.NET MVC.

The Controller in MVC architecture handles any incoming URL request. Controller is a class, derived from the base class System.Web.Mvc.Controller. Controller class contains public methods called Action methods. Controller and its action method handles incoming browser requests, retrieves necessary model data and returns appropriate responses.

In ASP.NET MVC, every controller class name must end with a word "Controller". For example, controller for home page must be HomeController and controller for student must be StudentController. Also, every controller class must be located in Controller folder of MVC folder structure.
 
 ## AccountController
  AccountCotroler is used to handel the user account ie Registration and Login
  ```csharp

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GlobalGeobits.ChatApp.web.Models;

namespace GlobalGeobits.ChatApp.web.Controllers
{
    public class AccountController : Controller
    {
      

        public ActionResult Register()
        {
            if (Session["user_id"] != null)
            {
                return RedirectToAction("index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Register(Users account)
        {
            

            if (ModelState.IsValid)
            {

                using (ChatAppDbContext DB = new ChatAppDbContext())
                {

                    DB.Users.Add(account);
                    DB.SaveChanges();
                }
                ModelState.Clear();
               
            }
            return Redirect("Thanks/" + account.UserID);
        }

        public ActionResult Login()
        {
            if (Session["user_id"] != null)
            {
                return RedirectToAction("index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(Users account) {

            using (ChatAppDbContext db = new ChatAppDbContext())
            {
                var user = db.Users.FirstOrDefault(u => u.UserEmail == account.UserEmail && u.UserPassword == account.UserPassword);
                if (user != null)
                {

                    Session["user_id"] = user.UserID.ToString();
                    return RedirectToAction("index", "Home");

                }
                else {
                    ModelState.AddModelError("invaledlogin", "Email or password are not correct. Please, try again");
                
                }
            }
            return View();
        }

        public ActionResult Thanks(int id)
        {
            using (ChatAppDbContext db = new ChatAppDbContext())
            {
                var user = db.Users.Where(u => u.UserID == id).FirstOrDefault();
                if (user != null)
                {
                    ViewBag.userName = user.UserFristName + " " + user.UserLastName;
                }
            }

            return View();
        }


        [AllowAnonymous]
        public string CheckUserName(string input)
        {

            return ((new ChatAppDbContext().Users.FirstOrDefault(u => u.UserEmail == input)) == null) ? "Available" : "Not Available";
        }

    }
}


  ```
  
  ## HomeControler
  Home Controler will used to handel the message exchanging and saving
  ```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GlobalGeobits.ChatApp.web.Models;

namespace GlobalGeobits.ChatApp.web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if (Session["user_id"] != null)
            {
                int id = int.Parse(Session["user_id"].ToString());
                var user = new ChatAppDbContext().Users.FirstOrDefault(u => u.UserID == id);
                if (user != null)
                    ViewBag.username = user.UserFristName;
                return View();
            }
            return RedirectToAction("login", "account");
        }

        public ActionResult logout()
        {
            Session["user_id"] = null;
            return RedirectToAction("login", "account");
        }

    }
}
//TODOO 


```

# Code Updated
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GlobalGeobits.ChatApp.web.Models;

namespace GlobalGeobits.ChatApp.web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Chat(int id = -1)
        {
            
            if (Session["user_id"] != null)
            {
                 if (id != -1)
                {
                    ViewBag.id = id;
                    var incaht = new ChatAppDbContext().Users.FirstOrDefault(u => u.UserID == id);
                    if (incaht != null)
                        ViewBag.chatName = incaht.UserDisplayName;
                }


                int id1 = int.Parse(Session["user_id"].ToString());
                var user = new ChatAppDbContext().Users.FirstOrDefault(u => u.UserID == id1);
                if (user != null)
                {
                    string DisplayName = user.UserDisplayName;
                    if (DisplayName == string.Empty)
                        DisplayName = user.UserFristName + " " + user.UserLastName;
                    ViewBag.DisplayName = DisplayName;
                }

               

                // get all users from database except current user in sesstion
                List<Users> users = new ChatAppDbContext().Users.Where(u => u.UserID != id1).ToList();
                return View(users);
            }
            return RedirectToAction("login", "account");
        }

        public ActionResult logout()
        {
            int id = int.Parse(Session["user_id"].ToString());
            using (ChatAppDbContext db = new ChatAppDbContext())
            {
                var user = db.Users.FirstOrDefault(u => u.UserID == id);
                if (user != null)
                {
                    user.status = 0;
                    db.SaveChanges();
                }
            }
            Session["user_id"] = null;
            return RedirectToAction("login", "account");
        }

        public ActionResult startconversation(int? id = -1)
        {
         return   RedirectToAction("chat", new { id = id });
        }
       

    }
}
```


  
  ### Views
  View is the UI that prsent the data in a way that user can deal with
  
  ## Shared Views
  Views that shared and can be used in any view (Like Master Page in ASP.NET WebForms)
  * LayOut View
  ```html
  <!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>@ViewBag.Title - Global Geobits Chat App</title>
    @Styles.Render("~/Content/css")
    @Scripts.Render("~/bundles/modernizr")
</head>
<body>
    <div class="navbar navbar-inverse navbar-fixed-top">
        <div class="container">
           
        </div>
    </div>
    <div class="container body-content">
        @RenderBody()
        <hr />
        <footer>
            <p>&copy; @DateTime.Now.Year - Hamed Refaat</p>
        </footer>
    </div>

    @Scripts.Render("~/bundles/jquery")
    @Scripts.Render("~/bundles/bootstrap")
    @RenderSection("scripts", required: false)
    
</body>
</html>


  ```
  ## Account Views
  * Login View
  ```html
@model GlobalGeobits.ChatApp.web.Models.Users

@{
    ViewBag.Title = "Login";
}

<h2>Login</h2>


@using (Html.BeginForm()) 
{
    @Html.AntiForgeryToken()
    
    <div class="form-horizontal">
        <h4>Users</h4>
        <hr />
        @Html.ValidationSummary(true, "", new { @class = "text-danger" })
        

        <div class="form-group">
            @Html.LabelFor(model => model.UserEmail, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.UserEmail, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.UserEmail, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(model => model.UserPassword, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.UserPassword, new { htmlAttributes = new { @class = "form-control" } })
              
            </div>
        </div>

       

        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <input type="submit" value="Login" class="btn btn-default" />
            </div>
        </div>
    </div>
}

<div>
  You Donot have an Account ?  @Html.ActionLink("Register Now", "register")
</div>

@section Scripts {
    @Scripts.Render("~/bundles/jqueryval")
}


  ```
  * Register View
  ```html
@model GlobalGeobits.ChatApp.web.Models.Users

@{
    ViewBag.Title = "Register";
}

<h2>Register</h2>


@using (Html.BeginForm()) 
{
    @Html.AntiForgeryToken()
    
    <div class="form-horizontal">
        <h4>GlobalGeobits ChatApp</h4>
        <hr />
        @Html.ValidationSummary(true, "", new { @class = "text-danger" })
        <div class="form-group">
            @Html.LabelFor(model => model.UserFristName, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.UserFristName, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.UserFristName, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(model => model.UserLastName, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.UserLastName, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.UserLastName, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(model => model.UserDisplayName, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.UserDisplayName, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.UserDisplayName, "", new { @class = "text-danger" })
            </div>
        </div>


        <div class="form-group">
            @Html.LabelFor(model => model.UserDateOfBirth, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.UserDateOfBirth, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.UserDateOfBirth, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(model => model.UserEmail, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.UserEmail, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.UserEmail, "", new { @class = "text-danger" })
                <span id="result"></span>
                 
             </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(model => model.UserPassword, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.UserPassword, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.UserPassword, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(model => model.UserConfirmPassword, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.UserConfirmPassword, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.UserConfirmPassword, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <input type="submit" value="Register" class="btn btn-default" />
            </div>
        </div>
    </div>
}


@section Scripts {
    @Scripts.Render("~/bundles/jqueryval")

<script type="text/jscript">
    $('#UserEmail').blur(function () {
            var url = "/Account/CheckUsermail";
            var mail = $('#UserEmail').val();
            $.get(url, { input: mail }, function (data) {
                if (data == "yes") {
                    $("#result").html("<span style='color:green'>Available</span>");
                    $("#UserEmail").css('background-color', '');
                }
                else {
                    $("#result").html("<span style='color:red'>Email already exiestes</span>");
                    $("#UserEmail").css('background-color', '#e97878');
                }
            });
        })
</script>
    
}


  ```
  * Thanks after registration View
  ```html

@{
    ViewBag.Title = "Thanks";
}

<h1>Welcome @ViewBag.userName Thanks for Registration. Please, @Html.ActionLink("Login here","login")</h1>


```
## Home Views
Not completed yet

* Index view
```html

@{
    ViewBag.Title = "Index";
}

<h2>Index</h2>
<h4>welcome, @ViewBag.username  @Html.ActionLink("logOut","logout")</h4>



```

  ### SignalR
  ASP.NET SignalR is a new library for ASP.NET developers that makes it incredibly simple to add real-time web functionality to your applications. What is "real-time web" functionality? It's the ability to have your server-side code push content to the connected clients as it happens, in real-time.

You may have heard of WebSockets, a new HTML5 API that enables bi-directional communication between the browser and server. SignalR will use WebSockets under the covers when it's available, and gracefully fallback to other techniques and technologies when it isn't, while your application code stays the same.

SignalR also provides a very simple, high-level API for doing server to client RPC (call JavaScript functions in your clients' browsers from server-side .NET code) in your ASP.NET application, as well as adding useful hooks for connection management, e.g. connect/disconnect events, grouping connections, authorization.

## Install SignalR

* using this commant to install SignalR in the Project solution
```
Install-Package Microsoft.AspNet.SignalR
```
Or
using NuGet Pacakge Manager
* right click on project solution or From Tools
* Manage Nuget Pacakge
* Search or SignalR and install

## startup class

* after installing SignalR, you must add a class to the Project solution with name Startup.cs
* add the follwing code to the class to enable the SignalR

```csharp
using Microsoft.Owin;
using Owin;
[assembly: OwinStartup(typeof(GlobalGeobits.ChatApp.web.Startup))]
namespace GlobalGeobits.ChatApp.web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
        }
    }
}
  ```
  ## hubs Class
  To create a Hub, create a class that derives from [Microsoft.Aspnet.Signalr.Hub](https://msdn.microsoft.com/library/microsoft.aspnet.signalr.hub(v=vs.111).aspx). The following example shows a simple Hub class for a chat application.
  ```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using GlobalGeobits.ChatApp.web.Models;

namespace GlobalGeobits.ChatApp.web.SignalR.hubs
{
    public class ChatHub : Hub
    {
        // a broadcast message
        public void Send(string name, string message)
        {
            Clients.All.addNewMessageToPage(name, message);
        }

       

        

        public void Connect(string UserName, int UserID)
        {
           
           //TooDo Connect on user private chat

        }

       

        public void SendPrivateMessage(string ReciverId, string message)
        {
            //TOODo send message to a client with id
        }

        public void ReceivePrivateMessage(string SenderId, string message)
        {
            //TOODo Receive message to a client with id
        }
       
      
    }
    
}

  ```
 
 ## Connect To SignalR
 
 
 ```js
@section scripts {
    <!--Script references. -->
    <!--The jQuery library is required and is referenced by default in _Layout.cshtml. -->
    <!--Reference the SignalR library. -->
   
<script src="~/Scripts/jquery.signalR-2.3.0.min.js"></script>

    <!--Reference the autogenerated SignalR hub script. -->
    <script src="~/signalr/hubs"></script>
    <!--SignalR script to update the chat page and send messages.-->
    <script>
        $(function () {
            // Reference the auto-generated proxy for the hub.
            var chat = $.connection.chatHub;
            // Create a function that the hub can call back to display messages.
            chat.client.addNewMessageToPage = function (name, message) {
                if (name == "Rania") {
                    // Add the message to the page.
                    $('#discussion').append('<p style="color:green; text-align:left; width:500px"><strong><img = src="https://www.phplivesupport.com/pics/icons/avatars/public/avatar_7.png" title="Atir">'
                        + ' </strong> ' + htmlEncode(message) + '</p>');
                }
                else if (name != "Rania") {
                    // Add the message to the page.
                    $('#discussion').append('<p style="color:blue;text-align:right;"><strong><img = src="https://www.phplivesupport.com/pics/icons/avatars/public/avatar_71.png" title="Peter">'
                        + ' </strong> ' + htmlEncode(message) + '</p>');
                }
            };
            // Get the user name and store it to prepend to messages.
            $('#displayname').val(prompt('Enter your Rania in one browser and any other name in the other Browser:', ''));
            // Set initial focus to message input box.
            $('#message').focus();
            // Start the connection.
            $.connection.hub.start().done(function () {
                $('#sendmessage').click(function () {
                    // Call the Send method on the hub.
                    chat.server.send($('#displayname').val(), $('#message').val());
                    // Clear text box and reset focus for next comment.
                    $('#message').val('').focus();
                });
            });
        });
        // This optional function html-encodes messages for display in the page.
        function htmlEncode(value) {
            var encodedValue = $('<div />').text(value).html();
            return encodedValue;
        }
    </script>
 ```
 ### DataBase Update
 I update the Users Model to add a status code that tells if the user is online or not 
 Here How we detect if user is online or not
 
 ## Editing Database in codeFrist Approch after database is created
 
To enable this we must add the follwing code int the Application class

```csharp

Database.SetInitializer<ChatAppDbContext>(new DropCreateDatabaseIfModelChanges<ChatAppDbContext>());

```
## AppStart method 
```csharp
using GlobalGeobits.ChatApp.web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace GlobalGeobits.ChatApp.web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer<ChatAppDbContext>(new DropCreateDatabaseIfModelChanges<ChatAppDbContext>());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}


```

 ## Detect if user online
 ```html
  <div class="col-md-3">
        <div class="chat-panel panel panel-default">
            <div class="panel-heading">
                Users
                <i class="fa fa-paper-plane fa-fw"></i>
            </div>
            <div class="panel-body" style="height:300px;">
                <ul>
                    @foreach (var user in Model)
                    {
                        int uid = -1;

                        if (user != null)
                        {
                            uid = user.UserID; 
                        }
                       
                        <li>
                            <h3>
                              
                        @Html.ActionLink(@user.UserDisplayName, "startconversation", new { id = uid })

                                </h3>
                            @if (user.status == 1) { 
                                
                           <span>online</span>
                           
                            }
                            
                            @if (user.status == 0)
                            {
                                <span>ofline</span>
                            }
                            
                            
                            

                        </li>

                    }
                </ul>
            </div>
        </div>
    </div>
 ```
 ## update status code in login and logout
 * in user login we update the status code to 1 which mean that user is online
 * in user sesstion time out or logout we udate the status code to 0 which mean user is ofline
 
 # login update
  
  ```csharp
 [HttpPost]
        public ActionResult Login(Users account) {

            using (ChatAppDbContext db = new ChatAppDbContext())
            {
                var user = db.Users.FirstOrDefault(u => u.UserEmail == account.UserEmail && u.UserPassword == account.UserPassword);
                if (user != null)
                {

                    Session["user_id"] = user.UserID.ToString();
                    user.status = 1; // here we change the status to be online
                    db.SaveChanges();
                    return RedirectToAction("chat", "Home");

                }
                else {
                    ModelState.AddModelError("invaledlogin", "Email or password are not correct. Please, try again");
                
                }
            }
            return View();
        }
   ```
 
  # logout update
  
  ```csharp

      public ActionResult logout()
        {
            int id = int.Parse(Session["user_id"].ToString());
            using (ChatAppDbContext db = new ChatAppDbContext())
            {
                var user = db.Users.FirstOrDefault(u => u.UserID == id);
                if (user != null)
                {
                    user.status = 0;
                    db.SaveChanges();
                }
            }
            Session["user_id"] = null;
            return RedirectToAction("login", "account");
        }

   ```
   
   ### Work ToDo
   * implement the connect method in chathub class to enable private chat
   * saving the users messages history to the Database which alredy exiestes
   * Update the online user list using Ajax or just using SignalR

  


## Authors

* **Hamed Refaat** - *Initial work* - [Hamed Refaat](https://gist.github.com/hamed1m54)



## License

This project is licensed under the MIT License - see the [LICENSE.md](https://github.com/hamed1m54/GlobalGeobits.ChatApp/blob/master/License.md) file for details

