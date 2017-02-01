using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using blogmvc.Models;
using System.Data.Entity;

namespace blogmvc.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //opcion que veo interesante es la lista UserCanRegister ponerla aqui para poder usarla en el EditOption
        //List<SelectListItem> UserCanRegister { get; set; }
        // GET: Admin
        [Authorize]
        public ActionResult Index()
        {
            //comprobar el usuario que accede a la parte de administracion 
            
            return View();
        }
        //GET UserProfile
        [Authorize]
        public ActionResult UserProfile()
        {

            string userName = User.Identity.Name;
            //consulta LINSQ to SQL

            //el List para rellenar el dropdownlist 
            List<SelectListItem> UserCanRegister = new List<SelectListItem>()
            {
                new SelectListItem {Text = "No", Value = "0", Selected = true },
                new SelectListItem {Text = "Yes", Value = "1" }
            };
            ViewBag.UserCanRegister = UserCanRegister;
            //ViewData["DropDownListUsers"] = UserCanRegister;
            List<Option> Options = new List<Option>();
            Options = db.Option.ToList();
            Option Option = new Option();
            Option.siteurl = "";
            Option.home = "";
            Option.blogname = "";
            Option.blogdescription = "";
            Option.user_can_register = "No";
            Option.admin_email = "";
            Option.ApplicationUserID = "";
            
            if (Options.Count == 1)
            {
                //proceder a mostrar o que haya insertado 
                //mostrar la tupla que pertenezca a ese usuario 
                //necesito saber el id que tiene el username asociado desde la propia tabla Users 
                List<ApplicationUser> Users = db.Users.ToList();
                //usuario logueado en el sistema 
                var User = Users.Find(user => user.UserName == userName);
                //busca el usuario donde user.id es el que esta lgoueado en el sistema
                var usuario = Options.Where(user => user.ApplicationUserID == User.Id);
                Option = usuario.ElementAt(0);

            }
            ViewBag.Option = Option;
            //ViewData["Option"] = Options;
            return View("User", Option);
             
            
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOption(Option Option)
        {
            string userName = User.Identity.Name;
            //consulta LINSQ to SQL
            
            List<SelectListItem> UserCanRegister = new List<SelectListItem>()
            {
                new SelectListItem {Text = "No", Value = "0" },
                new SelectListItem {Text = "Yes", Value = "1" }
            };
            ViewBag.UserCanRegister = UserCanRegister;
            string userCanRegister = Request.Form["UserCanRegister"].ToString();
            if (userCanRegister == "0")
            {
                Option.user_can_register = "No";
            }
            else if (userCanRegister == "1")
            {
                Option.user_can_register = "Yes";
            }

            /*var query = from op in db.Option
                        select op; */
            List<ApplicationUser> AppUser = db.Users.ToList();
            List<Option> ListOption = db.Option.ToList();
            //estas variables sderan nulas si no hay tuplas
            var UserName = AppUser.Where(user => user.UserName == userName).SingleOrDefault();
            var optionUser = ListOption.Where(option => option.ApplicationUserID == UserName.Id);
            Option Options = new Option();

            if (optionUser.Count() == 0)
            {
                //aqui se debe proceder al insertado de los datos que haya en el formulario
                db.Option.Add(new Option {
                    siteurl = Option.siteurl,
                    home = Option.home,
                    blogname = Option.blogname,
                    blogdescription = Option.blogdescription,
                    post_per_page = Option.post_per_page,
                    user_can_register = Option.user_can_register,
                    admin_email = Option.admin_email,
                    ApplicationUserID = UserName.Id
                });
                db.SaveChanges();

            }
            if (optionUser.Count() == 1)
            {
                using (ApplicationDbContext db = new ApplicationDbContext())
                {

                    var userOption = db.Option.Where(option => option.ApplicationUserID == UserName.Id).Single();
                    //userOption.OptionID = Option.OptionID;
                    userOption.siteurl = Option.siteurl;
                    userOption.home = Option.home;
                    userOption.blogname = Option.blogname;
                    userOption.blogdescription = Option.blogdescription;
                    userOption.post_per_page = Option.post_per_page;
                    userOption.user_can_register = Option.user_can_register;
                    userOption.admin_email = Option.admin_email;
                    userOption.ApplicationUserID = UserName.Id;
                    db.Entry(userOption).State = EntityState.Modified;
                    //db.Option.Attach(userOption);
                    db.SaveChanges();
                }

                   
                
            }
            else if (optionUser.Count() > 1) 
            {
                var optionsusers = db.Option.Where(option => option.ApplicationUserID == UserName.Id);
                List<Option> OptionsUser = optionsusers.ToList();
                int userCount = OptionsUser.Count();
                for (int i = 0; i < userCount; i++)
                {
                    var optionUsers1 = db.Option.ElementAt(i);
                    db.Option.Remove(optionUsers1);
                }
                db.Option.Add(new Option
                {
                    siteurl = Option.siteurl,
                    home = Option.home,
                    blogname = Option.blogname,
                    blogdescription = Option.blogdescription,
                    post_per_page = Option.post_per_page,
                    user_can_register = Option.user_can_register,
                    admin_email = Option.admin_email,
                    ApplicationUserID = UserName.Id
                });
                db.SaveChanges();
            }


            return View("User");
        }

    }
}