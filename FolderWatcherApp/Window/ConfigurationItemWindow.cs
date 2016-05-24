using FolderWatcherApp.Configuration.Folder;
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
    public partial class ConfigurationItemWindow : Form
    {
        public ConfigurationItemWindow()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FolderConfiguration save = new FolderConfiguration();
            save.Title = txtTitle.Text;
            save.Enabled = chkEnabled.Checked;

            foreach (DataGridViewRow row in grdFolders.Rows)
            {
                if (row.Cells["Path"].Value == null || string.IsNullOrWhiteSpace(row.Cells["Path"].Value.ToString())) continue;

                FolderItemConfiguration config = new FolderItemConfiguration();
                config.Path = row.Cells["Path"].Value.ToString();
                config.Filter = row.Cells["Filter"].Value.ToString();
                config.Log = row.Cells["Log"].Value.ToString();
                config.Recursive = row.Cells["Recursive"].Value == null ? false : row.Cells["Recursive"].Value.ToString().Equals("true", StringComparison.CurrentCultureIgnoreCase);

                save.Folders.Add(config);
            }

            save.Save();

            this.Close();
        }
        public void SetItem(FolderConfiguration folderConfiguration)
        {
            txtTitle.Text = folderConfiguration.Title;
            chkEnabled.Checked = folderConfiguration.Enabled;
            txtTitle.Enabled = false;
            grdFolders.AutoGenerateColumns = false;

            foreach (var config in folderConfiguration.Folders)
            {
                int row = grdFolders.Rows.Add();
                grdFolders.Rows[row].Cells["Path"].Value = config.Path;
                grdFolders.Rows[row].Cells["Filter"].Value = config.Filter;
                grdFolders.Rows[row].Cells["Log"].Value = config.Log;
                grdFolders.Rows[row].Cells["Recursive"].Value = config.Recursive;
            }
        }

        private void ConfigurationItemWindow_Load(object sender, EventArgs e)
        {
        }
    }
}
