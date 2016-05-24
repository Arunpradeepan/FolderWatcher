using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderWatcherApp.Configuration
{
    public class ConfigurationData
    {
        public static string ConfiurationFolder
        {
            get
            {
                if (System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("ConfigurationFolder"))
                    return System.Configuration.ConfigurationManager.AppSettings["ConfigurationFolder"];
                else
                    return "Configuration";
            }
        }
    }
}
