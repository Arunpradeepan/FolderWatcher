namespace FolderWatcherApp.Window
{
    partial class ConfigurationListWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.grdConfigurations = new System.Windows.Forms.DataGridView();
            this.ctxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDelete = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.grdConfigurations)).BeginInit();
            this.ctxMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdConfigurations
            // 
            this.grdConfigurations.AllowUserToAddRows = false;
            this.grdConfigurations.AllowUserToDeleteRows = false;
            this.grdConfigurations.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grdConfigurations.ContextMenuStrip = this.ctxMenu;
            this.grdConfigurations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdConfigurations.Location = new System.Drawing.Point(0, 0);
            this.grdConfigurations.MultiSelect = false;
            this.grdConfigurations.Name = "grdConfigurations";
            this.grdConfigurations.ReadOnly = true;
            this.grdConfigurations.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdConfigurations.Size = new System.Drawing.Size(631, 408);
            this.grdConfigurations.TabIndex = 0;
            // 
            // ctxMenu
            // 
            this.ctxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCreate,
            this.mnuEdit,
            this.mnuDelete});
            this.ctxMenu.Name = "ctxMenu";
            this.ctxMenu.Size = new System.Drawing.Size(109, 70);
            // 
            // mnuCreate
            // 
            this.mnuCreate.Name = "mnuCreate";
            this.mnuCreate.Size = new System.Drawing.Size(108, 22);
            this.mnuCreate.Text = "Create";
            this.mnuCreate.Click += new System.EventHandler(this.mnuCreate_Click);
            // 
            // mnuEdit
            // 
            this.mnuEdit.Name = "mnuEdit";
            this.mnuEdit.Size = new System.Drawing.Size(108, 22);
            this.mnuEdit.Text = "Edit";
            this.mnuEdit.Click += new System.EventHandler(this.mnuEdit_Click);
            // 
            // mnuDelete
            // 
            this.mnuDelete.Name = "mnuDelete";
            this.mnuDelete.Size = new System.Drawing.Size(108, 22);
            this.mnuDelete.Text = "Delete";
            this.mnuDelete.Click += new System.EventHandler(this.mnuDelete_Click);
            // 
            // ConfigurationListWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 408);
            this.Controls.Add(this.grdConfigurations);
            this.Name = "ConfigurationListWindow";
            this.Text = "Folder Watcher  - List";
            this.Load += new System.EventHandler(this.ConfigurationListWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdConfigurations)).EndInit();
            this.ctxMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grdConfigurations;
        private System.Windows.Forms.ContextMenuStrip ctxMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuCreate;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit;
        private System.Windows.Forms.ToolStripMenuItem mnuDelete;
    }
}