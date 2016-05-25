using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderWatcherApp.Configuration.Folder
{
    [Serializable]
    public class FolderItemConfiguration
    {
        public string Path { get; set; }
        public bool Recursive { get; set; }
        public string Log { get; set; }
        public string Filter { get; set; }

        public override int GetHashCode()
        {
            return (Path + Log + Filter + Recursive.ToString()).GetHashCode();
        }
    }
}
