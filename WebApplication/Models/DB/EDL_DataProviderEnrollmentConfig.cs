//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication.Models.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class EDL_DataProviderEnrollmentConfig
    {
        public int ProviderEnrollmentConfigId { get; set; }
        public string ProviderName { get; set; }
        public string DataLakeLandingFilePath { get; set; }
        public string FileSchedule { get; set; }
        public Nullable<int> NumberofFilesExpected { get; set; }
        public Nullable<int> NumberofFilesPlaced { get; set; }
        public string ProviderCredentials { get; set; }
        public string CreateUser { get; set; }
        public string UpdateUser { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public Nullable<bool> Active { get; set; }
    }
}
