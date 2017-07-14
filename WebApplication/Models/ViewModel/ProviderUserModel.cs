using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models.ViewModel
{
    public class ProviderUserModel
    {
        [Key]
        [Required(ErrorMessage = "*")]
        [Display(Name = "System Name")]
        public string ProviderName { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "Data Lake Landing Path")]
        public string DataLakeLandingFilePath { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "File schedule")]
        public string FileSchedule { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "Credentials required for connecting to Source System/ Target system")]
        public string ProviderCredentials { get; set; }
    }

    public class ProviderUserView
    {
        [Key]
        [Required(ErrorMessage = "*")]
        [Display(Name = "System Name")]
        public string ProviderName { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "Data Lake Landing Path")]
        public string DataLakeLandingFilePath { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "File schedule")]
        public string FileSchedule { get; set; }
        [Display(Name = "Number of Files Expected")]
        public Int32 NumberofFilesExpected { get; set; }
        [Display(Name = "Number of Files Placed")]
        public Int32 NumberofFilesPlaced { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "Credentials required for connecting to Source System/ Target system")]
        public string ProviderCredentials { get; set; }
    }

    public class ProviderDataView
    {
        public IEnumerable<ProviderUserView> ProviderUserProfile { get; set; }
    }
}