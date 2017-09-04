using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using BrightIdeasSoftware;
using System.Runtime.InteropServices;

namespace IconSetter
{

    public partial class Form1 : Form
    {
        [DllImport("Shell32.dll", CharSet = CharSet.Auto)]
        static extern UInt32 SHGetSetFolderCustomSettings(ref LPSHFOLDERCUSTOMSETTINGS pfcs, string pszPath, UInt32 dwReadWrite);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        struct LPSHFOLDERCUSTOMSETTINGS
        {
            public UInt32 dwSize;
            public UInt32 dwMask;
            public IntPtr pvid;
            public string pszWebViewTemplate;
            public UInt32 cchWebViewTemplate;
            public string pszWebViewTemplateVersion;
            public string pszInfoTip;
            public UInt32 cchInfoTip;
            public IntPtr pclsid;
            public UInt32 dwFlags;
            public string pszIconFile;
            public UInt32 cchIconFile;
            public int iIconIndex;
            public string pszLogo;
            public UInt32 cchLogo;
        }


        private List<FolderAndIcon> Folders = new List<FolderAndIcon>();

        public Form1(string[] args)
        {
            InitializeComponent();

            for (int f = 0; f < args.Length; f++)
            {
                if (!Directory.Exists(args[f]))
                    continue;
                AddFolder(args[f]);

                string[] folders = Directory.GetDirectories(args[f]);
                for (int i = 0; i < folders.Length; i++)
                {
                    AddFolder(folders[i]);

                    string[] subfolders = Directory.GetDirectories(folders[i]);
                    for (int s = 0; s < subfolders.Length; s++)
                        AddFolder(subfolders[s]);
                }

                objectListView.SetObjects(Folders);
            }
        }

        private void LoadFoldersIntoList(string parentFolder, bool includeSubFolders = true)
        {
            Folders.Clear();

            textBox_Folder.Text = parentFolder;
            string[] folders = Directory.GetDirectories(parentFolder);
            for (int i = 0; i < folders.Length; i++)
            {
                AddFolder(folders[i]);

                if (includeSubFolders)
                {
                    string[] subfolders = Directory.GetDirectories(folders[i]);
                    for (int s = 0; s < subfolders.Length; s++)
                        AddFolder(subfolders[s]);
                }
            }

            objectListView.SetObjects(Folders);
        }
        private void AddFolder(string folder)
        {
            FolderAndIcon info = new FolderAndIcon();
            info.FolderLocation = folder;

            string[] files = Directory.GetFileSystemEntries(folder);
            for (int f = 0; f < files.Length; f++)
            {
                if (!files[f].Contains(".ico"))
                    continue;
                info.AlreadySet = true;
                Icon icon = new Icon(files[f]);
                info.Icon = new Bitmap(ExtractVistaIcon(icon), new Size(128, 128));
                info.IconLocation = files[f];
            }

            Folders.Add(info);
        }

        private void textBox_Folder_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;
        }

        private void textBox_Folder_DragDrop(object sender, DragEventArgs e)
        {
            string folder = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
            if (Directory.Exists(folder))
                LoadFoldersIntoList(folder);
        }

        private void button_Apply_Click(object sender, EventArgs e)
        {
            foreach (FolderAndIcon info in Folders)
            {
                if (info.AlreadySet)
                    continue;

                // delete existing .ico(s)
                string[] files = Directory.GetFileSystemEntries(info.FolderLocation);
                for (int i = 0; i < files.Length; i++)
                {
                    if (files[i].IndexOf(".ico") != -1)
                        File.Delete(files[i]);
                }

                // delete existing desktop.ini
                string desktopini = Path.Combine(info.FolderLocation, "desktop.ini");
                if (File.Exists(desktopini))
                    File.Delete(desktopini);

                if (info.IconLocation == "")
                    continue;

                // copy over .ico
                string iconName = "folder.ico";
                string icon = Path.Combine(info.FolderLocation, iconName);

                File.Copy(info.IconLocation, icon);
                File.SetAttributes(icon, File.GetAttributes(icon) | FileAttributes.Hidden | FileAttributes.System);

                // write new desktop.ini
                using (StreamWriter writer = File.CreateText(desktopini))
                {
                    writer.WriteLine("[.ShellClassInfo]");
                    writer.WriteLine("IconResource=" + iconName + ",0");
                    writer.WriteLine("IconFile=" + iconName);
                    writer.WriteLine("IconIndex=0");
                }
                File.SetAttributes(desktopini, File.GetAttributes(icon) | FileAttributes.Hidden | FileAttributes.System);

                File.SetAttributes(info.FolderLocation, FileAttributes.ReadOnly);

                // Refresh folder icon
                try
                {
                    LPSHFOLDERCUSTOMSETTINGS FolderSettings = new LPSHFOLDERCUSTOMSETTINGS();
                    FolderSettings.dwMask = 0x10;
                    FolderSettings.pszIconFile = icon;
                    FolderSettings.iIconIndex = 0;
                    UInt32 FCS_FORCEWRITE = 0x00000002;

                    string pszPath = info.FolderLocation;
                    SHGetSetFolderCustomSettings(ref FolderSettings, pszPath, FCS_FORCEWRITE);
                }
                catch (Exception) { }

                info.AlreadySet = true;
            }
        }

        private void objectListView_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void objectListView_DragDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
                return;
            List<string> files = ((string[])e.Data.GetData(DataFormats.FileDrop, true)).ToList();

            if (Directory.Exists(files[0]))
            {
                List<string> foldersFiles = Directory.GetFiles(files[0]).ToList();
                if (foldersFiles.Count <= 0)
                    return;
                AutoMatchIcons(foldersFiles);
            }
            else if (files.Count > 1)
                AutoMatchIcons(files);
            else
            {
                if (objectListView.SelectedItem == null)
                    return;
                Icon icon = new Icon(files[0]);
                Folders[objectListView.SelectedIndex].Icon = new Bitmap(ExtractVistaIcon(icon), new Size(128, 128));
                Folders[objectListView.SelectedIndex].IconLocation = files[0];
                Folders[objectListView.SelectedIndex].AlreadySet = false;
                objectListView.RefreshItem(objectListView.SelectedItem);
            }
        }

        private void objectListView_DragOver(object sender, DragEventArgs e)
        {
            if (objectListView.SelectedItem != null)
                objectListView.SelectedItem.Selected = false;

            Point p = PointToClient(new Point(e.X, e.Y));
            OLVListItem item = (OLVListItem)objectListView.GetItemAt(p.X - objectListView.Left, p.Y - objectListView.Top);
            if (item == null)
                return;
            Select();
            objectListView.Select();
            item.Selected = true;
            item.EnsureVisible();
        }

        private void objectListView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right || objectListView.SelectedItem == null)
                return;

            Folders[objectListView.SelectedIndex].Icon = null;
            Folders[objectListView.SelectedIndex].IconLocation = "";
            Folders[objectListView.SelectedIndex].AlreadySet = false;
            objectListView.RefreshItem(objectListView.SelectedItem);
        }
        private void objectListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (objectListView.SelectedItem == null)
                return;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Icon icon = new Icon(openFileDialog1.FileName);
                Folders[objectListView.SelectedIndex].Icon = new Bitmap(ExtractVistaIcon(icon), new Size(128, 128));
                Folders[objectListView.SelectedIndex].IconLocation = openFileDialog1.FileName;
                Folders[objectListView.SelectedIndex].AlreadySet = false;
                objectListView.RefreshItem(objectListView.SelectedItem);
            }
        }


        // http://stackoverflow.com/questions/220465/using-256-x-256-vista-icon-in-application/1945764#1945764
        // Based on: http://www.codeproject.com/KB/cs/IconExtractor.aspx
        // And a hint from: http://www.codeproject.com/KB/cs/IconLib.aspx
        public static Bitmap ExtractVistaIcon(Icon icoIcon)
        {
            Bitmap bmpPngExtracted = null;
            try
            {
                byte[] srcBuf = null;
                using (MemoryStream stream = new MemoryStream())
                { icoIcon.Save(stream); srcBuf = stream.ToArray(); }
                const int SizeICONDIR = 6;
                const int SizeICONDIRENTRY = 16;
                int iCount = BitConverter.ToInt16(srcBuf, 4);
                for (int iIndex = 0; iIndex < iCount; iIndex++)
                {
                    int iWidth = srcBuf[SizeICONDIR + SizeICONDIRENTRY * iIndex];
                    int iHeight = srcBuf[SizeICONDIR + SizeICONDIRENTRY * iIndex + 1];
                    int iBitCount = BitConverter.ToInt16(srcBuf, SizeICONDIR + SizeICONDIRENTRY * iIndex + 6);
                    if (iWidth == 0 && iHeight == 0 && iBitCount == 32)
                    {
                        int iImageSize = BitConverter.ToInt32(srcBuf, SizeICONDIR + SizeICONDIRENTRY * iIndex + 8);
                        int iImageOffset = BitConverter.ToInt32(srcBuf, SizeICONDIR + SizeICONDIRENTRY * iIndex + 12);
                        MemoryStream destStream = new MemoryStream();
                        BinaryWriter writer = new BinaryWriter(destStream);
                        writer.Write(srcBuf, iImageOffset, iImageSize);
                        destStream.Seek(0, SeekOrigin.Begin);
                        bmpPngExtracted = new System.Drawing.Bitmap(destStream);
                        break;
                    }
                }
            }
            catch { return null; }
            return bmpPngExtracted;
        }

        public void AutoMatchIcons(List<string> files)
        {
            // remove non .icos
            for (int f = 0; f < files.Count; f++)
            {
                if (files[f].IndexOf(".ico") != -1)
                    continue;
                files.RemoveAt(f);
                f--;
            }

            // get a list of just the file names (no directory or file extension) - make it easier to do comparisons
            List<string> fileNames = new List<string>(files);
            for (int f = 0; f < fileNames.Count; f++)
            {
                fileNames[f] = fileNames[f].Substring(fileNames[f].LastIndexOf("\\") + 1);
                fileNames[f] = fileNames[f].Remove(fileNames[f].Length - 4).ToLower().Replace("!", "").Replace("season", "").Replace("specials", "");
            }

            int count = 0;

            foreach (FolderAndIcon info in Folders)
            {
                if (info.AlreadySet)
                    continue;

                string folderName = info.FolderLocation.Substring(info.FolderLocation.LastIndexOf("\\") + 1).ToLower().Replace("!", "").Replace("season", "").Replace("specials", "");
                int index = fileNames.FindIndex(file => file == folderName || (file.Length >= 4 && folderName.Length >= 4 && (file.IndexOf(folderName) != -1 || folderName.IndexOf(file) != -1)));
                if (index == -1)
                    continue;

                Icon icon = new Icon(files[index]);
                if (icon == null)
                    continue;
                Bitmap vistaIcon = ExtractVistaIcon(icon);
                if (vistaIcon == null)
                    continue;
                info.Icon = new Bitmap(vistaIcon, new Size(128, 128));
                info.IconLocation = files[index];
                info.AlreadySet = false;

                count++;

                objectListView.RefreshObject(info);
            }

            MessageBox.Show("Matched " + count + " icons.");
        }

        private void buttonBrowseFolder_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result != DialogResult.OK)
                return;

            LoadFoldersIntoList(folderBrowserDialog1.SelectedPath);
        }

        private void textBox_Folder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return && Directory.Exists(textBox_Folder.Text))
                LoadFoldersIntoList(textBox_Folder.Text);
        }

    }

    class FolderAndIcon
    {
        public string FolderLocation;
        public string IconLocation = "";
        public Bitmap Icon;
        public bool AlreadySet = false;
    }
}
