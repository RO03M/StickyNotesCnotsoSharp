using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace StickyNotes
{
    public partial class Form1 : Form
    {

        public static List<string> openedWindows = new List<string>();

        private delegate void SafeCallDelegate();

        public Form1()
        {
            InitializeComponent();
            Watch();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e) {
            
        }

        private void Form1_Load(object sender, EventArgs e) {
            FileListLoader();
        }

        public void FileListLoader() {
            this.flowLayoutPanel1.Controls.Clear();
            string path = Application.LocalUserAppDataPath;
            DirectoryInfo directoryFiles = new DirectoryInfo(path);
            FileInfo[] fileInfo = directoryFiles.GetFiles("*.txt");
            for (int i = 0; i < fileInfo.Length; i++) {
                FilesList filesList = new FilesList();
                filesList.fileName = fileInfo[i].Name;
                filesList.filePath = path + "/" + fileInfo[i].Name;
                filesList.LoadContentFromFile(path + "/" + fileInfo[i].Name);
                this.flowLayoutPanel1.Controls.Add(filesList);
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            TextWrite newWindow = new TextWrite();
            newWindow.Show();
        }

        #region Main directory change detection
        private void Watch() {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = Application.LocalUserAppDataPath;
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Filter = "*.txt";
            watcher.Changed += new FileSystemEventHandler(DirChanged);
            watcher.EnableRaisingEvents = true;
        }

        private void DirChanged(object source, FileSystemEventArgs e) {
            var safeCall = new SafeCallDelegate(FileListLoader);
            this.Invoke(safeCall, new object[] { });
        }

        #endregion
    }
}
