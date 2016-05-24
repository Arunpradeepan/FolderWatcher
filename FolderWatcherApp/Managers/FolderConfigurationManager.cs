using FolderWatcherApp.Configuration;
using FolderWatcherApp.Configuration.Folder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderWatcherApp.Managers
{
    public class FolderConfigurationManager
    {
        System.IO.FileSystemWatcher watcher = new System.IO.FileSystemWatcher();

        List<Action<string, System.IO.WatcherChangeTypes>> onConfigChanged = new List<Action<string, System.IO.WatcherChangeTypes>>();

        public void OnConfigChanged(Action<string, System.IO.WatcherChangeTypes> path)
        {
            onConfigChanged.Add(path);
        }

        public FolderConfigurationManager()
        {
            if (!System.IO.Directory.Exists(ConfigurationData.ConfiurationFolder))
                System.IO.Directory.CreateDirectory(ConfigurationData.ConfiurationFolder);

            watcher.Path = ConfigurationData.ConfiurationFolder;
            watcher.Filter = "*.xml";

            watcher.Created += WatcherPulse;
            watcher.Deleted += WatcherPulse;
            watcher.Changed += WatcherPulse;
            watcher.Renamed += WatcherPulse;

            watcher.EnableRaisingEvents = true;
        }

        public List<FolderConfiguration> GetCurrentConfigurations()
        {
            List<FolderConfiguration> ret = new List<FolderConfiguration>();

            string[] files = System.IO.Directory.GetFiles(ConfigurationData.ConfiurationFolder);

            foreach (string item in files)
                ret.Add(FolderConfiguration.Deserialize(item));

            return ret;
        }

        private void WatcherPulse(object sender, System.IO.FileSystemEventArgs e)
        {
            foreach (Action<string, System.IO.WatcherChangeTypes> item in onConfigChanged)
                item(e.FullPath, e.ChangeType);
        }
    }
}
