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

namespace StickyNotes {
    public partial class TextWrite : Form {

        public string fileName = "";
        public string filePath = "";
        public Form m_Form;//main window (Form1) just to refresh content after changing a file

        public TextWrite() {
            InitializeComponent();
            this.FormClosed += OnClose;
        }

        private void OnClose(object sender, FormClosedEventArgs e) {
            if (fileName != "") Form1.openedWindows.RemoveAt(Form1.openedWindows.IndexOf(fileName));
        }

        private void button1_Click(object sender, EventArgs e) {
            TextWrite newWindow = new TextWrite();
            newWindow.Show();
        }

        public void LoadTextFromFile(string path, string fileName) {
            string text = File.ReadAllText(path);
            this.richTextBox1.Text = text;
            this.filePath = path;
            this.fileName = fileName;
        }

        private void TextWrite_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.S && Control.ModifierKeys == Keys.Control) {
                if (filePath == "") {
                    DirectoryInfo m_Directory = new DirectoryInfo(Application.LocalUserAppDataPath);
                    FileInfo[] fileInfo = m_Directory.GetFiles("*.txt");
                    int newName = fileInfo.Length;
                    string newPath = Application.LocalUserAppDataPath + "/" + newName + ".txt";
                    fileName = newName + ".txt";
                    filePath = newPath;
                    Form1.openedWindows.Add(fileName);
                    var newFile = File.Create(newPath);
                    newFile.Close();
                    File.WriteAllText(newPath, this.richTextBox1.Text);
                } else {
                    File.WriteAllText(filePath, this.richTextBox1.Text);
                }
            }
        }
    }
}
