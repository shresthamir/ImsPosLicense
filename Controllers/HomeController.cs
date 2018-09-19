using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using test.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Dapper;
namespace test.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost]
        [Route("api/CheckLicense")]
        [AllowAnonymous]
        public IActionResult CheckLicense([FromBody]object param)
        {
            try
            {
                LicenseInfo li = JsonConvert.DeserializeObject<LicenseInfo>(param.ToString());
                if (LicenseInfo.IgnoreHosts.Any(x => x.ToLower() == li.HostName.ToLower()))
                    return new OkObjectResult("OK");
                using (SqlConnection conn = new SqlConnection("SERVER = 178.128.24.90; DATABASE = ImsPosLicenseInfo; UID = sa; PWD = Himsh@ngteb@h@l55059"))
                {
                    if (conn.ExecuteScalar<int>("SELECT COUNT(*) FROM ClientList WHERE CompanyPan = @CompanyPan AND Branch = @Branch AND Terminal = @Terminal AND HostName = @HostName AND AppName = @AppName", li) > 0)
                    {
                        conn.Execute("UPDATE ClientList SET LastLoginDate = GETDATE(), BuildNo = @BuildNo  WHERE CompanyPan = @CompanyPan AND Branch = @Branch AND Terminal = @Terminal AND HostName = @HostName AND AppName = @AppName", li);
                    }
                    else
                    {
                        conn.Execute("INSERT INTO ClientList (CompanyName, CompanyPan, Branch, Terminal, HostName, BuildNo, AppName) VALUES (@CompanyName, @CompanyPan, @Branch, @Terminal, @HostName, @BuildNo, @AppName)", li);
                    }
                }
                return new OkObjectResult("OK");
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.GetBaseException().Message);
            }
        }

        [HttpPost]
        [Route("api/ClientBackupLog")]
        [AllowAnonymous]
        public IActionResult ClientBackupLog([FromBody]object param)
        {
            try
            {
                LicenseInfo li = JsonConvert.DeserializeObject<LicenseInfo>(param.ToString());
                //if (li.IgnoreHosts.Any(x => x == li.HostName))
                //    return new OkObjectResult("OK");
                using (SqlConnection conn = new SqlConnection("SERVER = 178.128.24.90; DATABASE = ImsPosLicenseInfo; UID = sa; PWD = Himsh@ngteb@h@l55059"))
                {
                    conn.Execute("INSERT INTO ClientBackupLog (CompanyPan, Branch, PhiscalId, BackupDate) VALUES (@CompanyPan, @Branch, @PhiscalId, GETDATE())", li);
                }
                return new OkObjectResult("OK");
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.GetBaseException().Message);
            }
        }

    }
}