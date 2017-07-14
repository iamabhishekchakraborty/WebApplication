using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models.DB;
using WebApplication.Models.ViewModel;

namespace WebApplication.Models.EntityManager
{
    public class ProviderManager
    {
        public void AddUserAccount(ProviderUserModel user)
        {

            using (EDL_CONFIG_DBEntities db = new EDL_CONFIG_DBEntities())
            {

                EDL_DataProviderEnrollmentConfig SU = new EDL_DataProviderEnrollmentConfig();
                SU.ProviderName = user.ProviderName;
                SU.DataLakeLandingFilePath = user.DataLakeLandingFilePath;
                SU.FileSchedule = user.FileSchedule;
                SU.ProviderCredentials = user.ProviderCredentials;
                SU.NumberofFilesExpected = 1;
                SU.NumberofFilesPlaced = 1;               
                SU.CreateUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                SU.UpdateUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                SU.CreateDate = DateTime.Now;
                SU.UpdateDate = DateTime.Now;
                SU.Active = true;

                db.EDL_DataProviderEnrollmentConfig.Add(SU);
                db.SaveChanges();

                
            }
        }

        public bool IsLoginNameExist(string providerName)
        {
            using (EDL_CONFIG_DBEntities db = new EDL_CONFIG_DBEntities())
            {
                return db.EDL_DataProviderEnrollmentConfig.Where(o => o.ProviderName.Equals(providerName)).Any();
            }
        }

        public List<ProviderUserView> GetAllUserProfiles()
        {
            List<ProviderUserView> profiles = new List<ProviderUserView>();

            using (EDL_CONFIG_DBEntities db = new EDL_CONFIG_DBEntities())
            {
                ProviderUserView UPV;
                var users = db.EDL_DataProviderEnrollmentConfig.ToList();

                foreach (EDL_DataProviderEnrollmentConfig u in db.EDL_DataProviderEnrollmentConfig)
                {
                    UPV = new ProviderUserView();

                    UPV.ProviderName = u.ProviderName;
                    UPV.DataLakeLandingFilePath = u.DataLakeLandingFilePath;
                    UPV.FileSchedule = u.FileSchedule;
                    UPV.NumberofFilesExpected = (Int32)u.NumberofFilesExpected;
                    UPV.NumberofFilesPlaced = (Int32)u.NumberofFilesPlaced;
                    UPV.ProviderCredentials = u.ProviderCredentials;

                    profiles.Add(UPV);
                }
            }

            return profiles;
        }

        public ProviderDataView GetUserDataView()
        {
            ProviderDataView UDV = new ProviderDataView();
            List<ProviderUserView> profiles = GetAllUserProfiles();
            
            UDV.ProviderUserProfile = profiles;
       
            return UDV;
        }

    }
}