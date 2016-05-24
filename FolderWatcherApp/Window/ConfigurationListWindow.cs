using FolderWatcherApp.Configuration.Folder;
using FolderWatcherApp.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FolderWatcherApp.Window
{
    public partial class ConfigurationListWindow : Form
    {
        public ConfigurationListWindow()
        {
            InitializeComponent();
        }

        private void ConfigurationListWindow_Load(object sender, EventArgs e)
        {
            FolderConfigurationManager folders = new FolderConfigurationManager();
            grdConfigurations.DataSource = folders.GetCurrentConfigurations();

            folders.OnConfigChanged((file, action) =>
            {
                switch (action)
                {
                    case System.IO.WatcherChangeTypes.Created:
                        List<FolderConfiguration> obj = ((List<FolderConfiguration>)grdConfigurations.DataSource);
                        obj.Add(FolderConfiguration.Deserialize(file));
                        grdConfigurations.Invoke(new Action(() => { grdConfigurations.DataSource = null; grdConfigurations.DataSource = obj; }));
                        break;
                    case System.IO.WatcherChangeTypes.Deleted:
                        List<FolderConfiguration> objD = ((List<FolderConfiguration>)grdConfigurations.DataSource);
                        grdConfigurations.Invoke(new Action(() => { grdConfigurations.DataSource = null; grdConfigurations.DataSource = objD.Where(q => q.FullPath != file).ToList(); }));
                        break;
                    case System.IO.WatcherChangeTypes.Changed:
                        if (((List<FolderConfiguration>)grdConfigurations.DataSource).Count(q => q.FullPath == file) > 0)
                        {
                            List<FolderConfiguration> objC = ((List<FolderConfiguration>)grdConfigurations.DataSource);
                            int index = objC.IndexOf(objC.First(q => q.FullPath == file));
                            objC[index] = FolderConfiguration.Deserialize(file);
                            grdConfigurations.Invoke(new Action(() => { grdConfigurations.DataSource = null; grdConfigurations.DataSource = objC; }));
                        }
                        break;
                    case System.IO.WatcherChangeTypes.Renamed:
                        grdConfigurations.Invoke(new Action(() => { grdConfigurations.DataSource = null; grdConfigurations.DataSource = folders.GetCurrentConfigurations(); }));
                        break;
                    case System.IO.WatcherChangeTypes.All:
                        break;
                    default:
                        break;
                }

            });
        }

        private void mnuCreate_Click(object sender, EventArgs e)
        {
            ConfigurationItemWindow itemWindow = new ConfigurationItemWindow();
            itemWindow.ShowDialog();
        }

        private void mnuEdit_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in grdConfigurations.SelectedRows)
            {
                ConfigurationItemWindow itemWindow = new ConfigurationItemWindow();
                itemWindow.SetItem(((List<FolderConfiguration>)grdConfigurations.DataSource)[item.Index]);
                itemWindow.ShowDialog();
                return;
            }
        }

        private void mnuDelete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in grdConfigurations.SelectedRows)
            {
                ((List<FolderConfiguration>)grdConfigurations.DataSource)[item.Index].Delete();
            }
        }
    }
}
