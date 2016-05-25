using FolderWatcherApp.Configuration.Folder;
using FolderWatcherApp.Managers;
using FolderWatcherApp.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FolderWatcherApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length > 0 && (args[0].Equals("gui", StringComparison.CurrentCultureIgnoreCase) || args[0].Equals("g", StringComparison.CurrentCultureIgnoreCase)))
            {
                ConfigurationListWindow mainWindow = new ConfigurationListWindow();
                Application.Run(mainWindow);
                return;
            }

            FolderManager folder = new FolderManager();
            FolderConfigurationManager config = new FolderConfigurationManager();
            foreach (FolderConfiguration item in config.GetCurrentConfigurations())
            {
                folder.AddToMonitor(item);
            }

            config.OnConfigChanged((path, action) =>
            {
                try
                {
                    Console.WriteLine(action.ToString() + "> " + path);

                    switch (action)
                    {
                        case System.IO.WatcherChangeTypes.Created:
                            folder.AddToMonitor(FolderConfiguration.Deserialize(path));
                            break;
                        case System.IO.WatcherChangeTypes.Deleted:
                            folder.RemoveMonitor(path);
                            break;
                        case System.IO.WatcherChangeTypes.Changed:
                            folder.ChangeMonitor(path);
                            break;
                        case System.IO.WatcherChangeTypes.Renamed:
                            break;
                        case System.IO.WatcherChangeTypes.All:
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    string contentLog = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " $ " + ex.Message + Environment.NewLine;
                    System.IO.File.AppendAllText("Log" + DateTime.Now.ToString("yyyy/MM/dd") + ".txt", contentLog);
                }
            });

            Console.ReadLine();
        }
    }
}
