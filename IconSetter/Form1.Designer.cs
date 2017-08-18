namespace IconSetter
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.textBox_Folder = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.button_Apply = new System.Windows.Forms.Button();
            this.olvColumn_Folder = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn_Icon = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.labelHelp = new System.Windows.Forms.Label();
            this.buttonBrowseFolder = new System.Windows.Forms.Button();
            this.objectListView = new BrightIdeasSoftware.ObjectListView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.objectListView)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox_Folder
            // 
            this.textBox_Folder.AllowDrop = true;
            this.textBox_Folder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_Folder.Location = new System.Drawing.Point(12, 9);
            this.textBox_Folder.Name = "textBox_Folder";
            this.textBox_Folder.Size = new System.Drawing.Size(775, 20);
            this.textBox_Folder.TabIndex = 0;
            this.textBox_Folder.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox_Folder_DragDrop);
            this.textBox_Folder.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBox_Folder_DragEnter);
            this.textBox_Folder.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_Folder_KeyDown);
            // 
            // button_Apply
            // 
            this.button_Apply.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Apply.Location = new System.Drawing.Point(12, 682);
            this.button_Apply.Name = "button_Apply";
            this.button_Apply.Size = new System.Drawing.Size(810, 45);
            this.button_Apply.TabIndex = 3;
            this.button_Apply.Text = "Apply";
            this.button_Apply.UseVisualStyleBackColor = true;
            this.button_Apply.Click += new System.EventHandler(this.button_Apply_Click);
            // 
            // olvColumn_Folder
            // 
            this.olvColumn_Folder.AspectName = "FolderLocation";
            this.olvColumn_Folder.FillsFreeSpace = true;
            this.olvColumn_Folder.Text = "Folder";
            this.olvColumn_Folder.Width = 500;
            // 
            // olvColumn_Icon
            // 
            this.olvColumn_Icon.AspectName = "";
            this.olvColumn_Icon.ImageAspectName = "Icon";
            this.olvColumn_Icon.Text = "Icon";
            this.olvColumn_Icon.Width = 130;
            // 
            // labelHelp
            // 
            this.labelHelp.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.labelHelp.AutoSize = true;
            this.labelHelp.Location = new System.Drawing.Point(147, 666);
            this.labelHelp.Name = "labelHelp";
            this.labelHelp.Size = new System.Drawing.Size(536, 13);
            this.labelHelp.TabIndex = 5;
            this.labelHelp.Text = "drag and drop .ico files on to the subfolders you want (drop a folder or multiple" +
    " .ico files to perform an auto-match)";
            this.labelHelp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonBrowseFolder
            // 
            this.buttonBrowseFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowseFolder.Location = new System.Drawing.Point(793, 7);
            this.buttonBrowseFolder.Name = "buttonBrowseFolder";
            this.buttonBrowseFolder.Size = new System.Drawing.Size(29, 23);
            this.buttonBrowseFolder.TabIndex = 6;
            this.buttonBrowseFolder.Text = "...";
            this.buttonBrowseFolder.UseVisualStyleBackColor = true;
            this.buttonBrowseFolder.Click += new System.EventHandler(this.buttonBrowseFolder_Click);
            // 
            // objectListView
            // 
            this.objectListView.AllColumns.Add(this.olvColumn_Folder);
            this.objectListView.AllColumns.Add(this.olvColumn_Icon);
            this.objectListView.AllowDrop = true;
            this.objectListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.objectListView.CellEditUseWholeCell = false;
            this.objectListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn_Folder,
            this.olvColumn_Icon});
            this.objectListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.objectListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.objectListView.FullRowSelect = true;
            this.objectListView.GridLines = true;
            this.objectListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.objectListView.Location = new System.Drawing.Point(12, 35);
            this.objectListView.Name = "objectListView";
            this.objectListView.RowHeight = 130;
            this.objectListView.ShowGroups = false;
            this.objectListView.Size = new System.Drawing.Size(810, 628);
            this.objectListView.TabIndex = 4;
            this.objectListView.UseCompatibleStateImageBehavior = false;
            this.objectListView.View = System.Windows.Forms.View.Details;
            this.objectListView.DragDrop += new System.Windows.Forms.DragEventHandler(this.objectListView_DragDrop);
            this.objectListView.DragEnter += new System.Windows.Forms.DragEventHandler(this.objectListView_DragEnter);
            this.objectListView.DragOver += new System.Windows.Forms.DragEventHandler(this.objectListView_DragOver);
            this.objectListView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.objectListView_MouseClick);
            this.objectListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.objectListView_MouseDoubleClick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Icon Files|*.ico";
            this.openFileDialog1.Title = "Choose .ico file";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 739);
            this.Controls.Add(this.buttonBrowseFolder);
            this.Controls.Add(this.labelHelp);
            this.Controls.Add(this.objectListView);
            this.Controls.Add(this.button_Apply);
            this.Controls.Add(this.textBox_Folder);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(575, 320);
            this.Name = "Form1";
            this.Text = "Icon Setter";
            ((System.ComponentModel.ISupportInitialize)(this.objectListView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBox_Folder;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button button_Apply;
        private BrightIdeasSoftware.OLVColumn olvColumn_Folder;
        private BrightIdeasSoftware.OLVColumn olvColumn_Icon;
        private System.Windows.Forms.Label labelHelp;
        private System.Windows.Forms.Button buttonBrowseFolder;
        private BrightIdeasSoftware.ObjectListView objectListView;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

