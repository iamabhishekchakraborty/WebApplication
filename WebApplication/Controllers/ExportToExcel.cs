using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication.Models.ViewModel;
using WebApplication.Models.EntityManager;
using System.Runtime.Remoting.Contexts;
using System.Data.OleDb;
using System.Text;
using System.Data;
using FastMember;

namespace WebApplication.Controllers
{
    public class ExportToExcel : Controller
    {
        //public FileContentResult Download()
        //{

        //    var fileDownloadName = String.Format("FileName.xlsx");
        //    const string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";


        //    // Pass your ef data to method
        //    ExcelPackage package = GenerateExcelFile(_db.Contexts.ToList());

        //    var fsr = new FileContentResult(package.GetAsByteArray(), contentType);
        //    fsr.FileDownloadName = fileDownloadName;

        //    return fsr;
        //}

        public void GenerateExcelFile(List<ProviderUserView> list, string fileName)
        {
            //var connectionString = string.Format("Provider = Microsoft.Jet.OLEDB.4.0; datasource = {0}; Extended Properties = \"Excel 8.0 XML;HDR=YES;IMEX=2\"", fileName);

            Dictionary<string, string> props = new Dictionary<string, string>();
            props["Provider"] = "Microsoft.ACE.OLEDB.12.0;";
            props["Extended Properties"] = "Excel 12.0 XML";
            props["Data Source"] = fileName;

            StringBuilder connectionString = new StringBuilder();
            foreach (KeyValuePair<string, string> prop in props)
            {
                connectionString.Append(prop.Key);
                connectionString.Append('=');
                connectionString.Append(prop.Value);
                connectionString.Append(';');
            }

            IEnumerable<ProviderUserView> data = list;
            DataTable dt = new DataTable("DataLakeProviders");
            using (var reader = ObjectReader.Create(data))
            {
                dt.Load(reader);
            }

            using (OleDbConnection conn = new OleDbConnection(connectionString.ToString()))
            {
                conn.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = conn;

                //excel sheet name
                string retVal = dt.TableName.ToString();

                //building the header                
                StringBuilder sbCreate = new StringBuilder();

                // Start the command build.
                sbCreate.AppendFormat("CREATE TABLE [{0}] (", retVal);

                // Build column names
                foreach (DataColumn col in dt.Columns)
                {
                    string type = string.Empty;
                    type = col.DataType.Name.ToString().ToLower();
                    if (type.Contains("int"))
                        type = "NUMBER";
                    else if (type.Contains("string"))
                        type = "TEXT";
                    sbCreate.AppendFormat("[{0}] {1},", col.Caption.Replace(' ', '_'), type);
                }
                sbCreate = sbCreate.Replace(',', ')', sbCreate.ToString().LastIndexOf(','), 1);

                //cmd = new OleDbCommand(sbCreate.ToString());
                cmd.CommandText = sbCreate.ToString();
                cmd.ExecuteNonQuery();

                // Puch each row into Excel.
                for (int rowIndex = 0; rowIndex < dt.Rows.Count; rowIndex++)
                {
                    StringBuilder sbInsert = new StringBuilder();

                    // Remove whitespace.
                    sbInsert.AppendFormat("INSERT INTO [{0}](", retVal);
                    foreach (DataColumn col in dt.Columns)
                        sbInsert.AppendFormat("[{0}],", col.Caption.Replace(' ', '_'));
                    sbInsert = sbInsert.Replace(',', ')', sbInsert.ToString().LastIndexOf(','), 1);

                    // Write values.
                    sbInsert.Append("VALUES (");
                    foreach (DataColumn col in dt.Columns)
                    {
                        string type = col.DataType.ToString();
                        string strToInsert = String.Empty;
                        strToInsert = dt.Rows[rowIndex][col].ToString().Replace("'", "''");
                        sbInsert.AppendFormat("'{0}',", strToInsert);
                    }
                    sbInsert = sbInsert.Replace(',', ')', sbInsert.ToString().LastIndexOf(','), 1);

                    cmd.CommandText = sbInsert.ToString();
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }

        }
        
    }
}