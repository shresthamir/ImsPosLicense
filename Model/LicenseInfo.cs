using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test.Model
{
    public class LicenseInfo
    {
        public string CompanyName { get; set; }
        public string CompanyPan { get; set; }
        public string Branch { get; set; }
        public string Terminal { get; set; }
        public string HostName { get; set; }
        public DateTime LastLoginDate { get; set; }
        public int BuildNo { get; set; }
        public string AppName { get; set; }
        public static string[] IgnoreHosts = new string[] { "IMS-D1", "NIKESH-PC", "PRO-PC" };
        public byte CheckOnlineLicense { get; set; }
        public DateTime LicenseExpiryDate { get; set; }
    }

    public class BackupInfo
    {
        public string CompanyPan { get; set; }
        public string Branch { get; set; }
        public string PhiscalId { get; set; }
        public string HostName { get; set; }
        public DateTime LastLoginDate { get; set; }
    }
}
