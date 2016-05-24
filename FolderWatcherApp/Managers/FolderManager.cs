using FolderWatcherApp.Configuration.Folder;
using FolderWatcherApp.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderWatcherApp.Managers
{
    public class FolderManager
    {
        Dictionary<string, FolderMonitorWorker> monitors = new Dictionary<string, FolderMonitorWorker>();

        public void AddToMonitor(FolderConfiguration folder)
        {
            FolderMonitorWorker worker = new FolderMonitorWorker(folder);
            worker.Start();
            monitors.Add(folder.FullPath, worker);
        }

        public void ChangeMonitor(string path)
        {
            RemoveMonitor(path);

            FolderConfiguration folder = FolderConfiguration.Deserialize(path);
            AddToMonitor(folder);
        }

        public void RemoveMonitor(string path)
        {
            monitors[path].Stop();
            monitors.Remove(monitors[path].Configuration.FullPath);
        }

        public void Stop()
        {
            foreach (KeyValuePair<string, FolderMonitorWorker> worker in monitors)
            {
                worker.Value.Stop();
            }
        }
    }
}
