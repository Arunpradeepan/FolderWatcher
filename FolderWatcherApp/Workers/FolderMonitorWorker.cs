using FolderWatcherApp.Configuration.Folder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderWatcherApp.Workers
{
    public class FolderMonitorWorker
    {
        Dictionary<int, System.IO.FileSystemWatcher> runners = new Dictionary<int, System.IO.FileSystemWatcher>();
        public FolderConfiguration Configuration { get; set; }

        public FolderMonitorWorker(FolderConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void Start()
        {
            foreach (FolderItemConfiguration item in Configuration.Folders)
            {
                System.IO.FileSystemWatcher watcher = new System.IO.FileSystemWatcher();
                watcher.Path = item.Path;
                watcher.Filter = item.Filter;
                watcher.IncludeSubdirectories = item.Recursive;

                watcher.Created += WatcherPulse;
                watcher.Deleted += WatcherPulse;
                watcher.Changed += WatcherPulse;
                watcher.Renamed += WatcherPulse;

                watcher.EnableRaisingEvents = true;

                runners.Add(item.GetHashCode(), watcher);
            }
        }
        private void WatcherPulse(object sender, System.IO.FileSystemEventArgs e)
        {
            System.IO.FileSystemWatcher watcher = (System.IO.FileSystemWatcher)sender;

            KeyValuePair<int, System.IO.FileSystemWatcher> watch = runners.Where(q => q.Value == watcher).FirstOrDefault();

            if (watch.Value == null) return;

            foreach (FolderItemConfiguration item in Configuration.Folders)
            {
                if (watch.Key == item.GetHashCode())
                {
                    string logFile = TransformValues(item.Log);
                    string contentLog = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " $ " + e.ChangeType.ToString() + " - " + e.FullPath + Environment.NewLine;
                    System.IO.File.AppendAllText(logFile, contentLog);
                    Console.Write(contentLog);
                }
            }   
            
        }

        public string TransformValues(string input, string type = "", bool sleepTime = false)
        {
            if (string.IsNullOrWhiteSpace(input)) return input;

            string ret = input;

            if (ret.Contains("{MILI}"))
                System.Threading.Thread.Sleep(1);
            else
            {
                if (ret.Contains("{SS}"))
                    System.Threading.Thread.Sleep(1000);
            }

            ret = ret.Replace("{MAQUINA}", System.Environment.MachineName);
            ret = ret.Replace("{MACHINE}", System.Environment.MachineName);
            ret = ret.Replace("{DATA}", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            ret = ret.Replace("{ANO}", DateTime.Now.Year.ToString("00"));
            ret = ret.Replace("{MES}", DateTime.Now.Month.ToString("00"));
            ret = ret.Replace("{DIA}", DateTime.Now.Day.ToString("00"));
            ret = ret.Replace("{HH}", DateTime.Now.ToString("HH"));
            ret = ret.Replace("{MM}", DateTime.Now.ToString("mm"));
            ret = ret.Replace("{SS}", DateTime.Now.ToString("ss"));
            ret = ret.Replace("{MILI}", DateTime.Now.Millisecond.ToString("0000"));
            ret = ret.Replace("{HORA}", DateTime.Now.ToString("HHmmss"));
            ret = ret.Replace("{DATETIME}", DateTime.Now.ToString("yyyyMMdd_HHmmss"));
            ret = ret.Replace("{?}", type);

            return ret;
        }

        public void Stop()
        {
            foreach (var item in runners)
            {
                if(item.Value != null)
                    item.Value.Dispose();
            }
        }
    }
}
