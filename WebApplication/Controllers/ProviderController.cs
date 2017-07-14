using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using WebApplication.Models.ViewModel;
using WebApplication.Models.EntityManager;

namespace WebApplication.Controllers
{
    public class ProviderController : Controller
    {
        // GET: Provider
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(ProviderUserModel USV)
        {
            if (ModelState.IsValid)
            {
                ProviderManager UM = new ProviderManager();
                if (!UM.IsLoginNameExist(USV.ProviderName))
                {
                    UM.AddUserAccount(USV);
                    FormsAuthentication.SetAuthCookie(USV.ProviderName, false);
                    return RedirectToAction("Welcome", "Home");

                }
                else
                    ModelState.AddModelError("", "Login Name already taken.");
            }
            return View();
        }
    }
}

