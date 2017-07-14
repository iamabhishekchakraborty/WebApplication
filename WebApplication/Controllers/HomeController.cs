using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.IO;
using WebApplication.Models.ViewModel;
using WebApplication.Models.EntityManager;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        //[Authorize]
        public ActionResult Welcome()
        {
            return View();
        }

        public ActionResult ManageUserPartial()
        {
            //if (User.Identity.IsAuthenticated)
            //{
                ProviderManager UM = new ProviderManager();
                ProviderDataView UDV = UM.GetUserDataView();
                return PartialView(UDV);
            //}
            //return View();
        }

        public FileContentResult Download()
        {

            string filePath = @"D:\SalesEdge\";
            string fileName = "DataLakeProviders" + DateTime.Now.ToString("yyyy_MM_dd") + ".xlsx";
            string providerReportPath = filePath + fileName;
            var fileDownloadName = String.Format(fileName);
            const string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            if (System.IO.File.Exists(providerReportPath))
            {
                System.IO.File.Delete(providerReportPath);
            }

            ProviderManager UM = new ProviderManager();
            ProviderDataView UDV = UM.GetUserDataView();

            // Pass data to method
            ExportToExcel ee = new ExportToExcel();
            ee.GenerateExcelFile(UDV.ProviderUserProfile.ToList(), providerReportPath);

            byte[] file = System.IO.File.ReadAllBytes(providerReportPath);

            var fsr = new FileContentResult(file, contentType);
            fsr.FileDownloadName = fileDownloadName;

            return fsr;
            
        }

    }
}