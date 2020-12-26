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
    public partial class FilesList : UserControl
    {
        public string fileName = "";
        public string filePath = "";

        public FilesList()
        {
            InitializeComponent();
            richTextBox1.MouseEnter += new EventHandler(OnMouseEnter);
            richTextBox1.MouseLeave += new EventHandler(OnMouseLeave);
        }

        private void OnMouseEnter(object sender, EventArgs e) {
            hoverBar.BackColor = ColorTranslator.FromHtml("#2c3e50");
        }

        private void OnMouseLeave(object sender, EventArgs e) {
            hoverBar.BackColor = ColorTranslator.FromHtml("#34495e");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void LoadContentFromFile(string path) {
            string fileContent = File.ReadAllText(path);
            if (fileContent.Length <= 100 && fileContent.Length > 0) this.richTextBox1.Text = fileContent;
            else if (fileContent.Length == 0) this.richTextBox1.Text = "Nothing here yet";
            else this.richTextBox1.Text = fileContent.Substring(0, 100) + "...";
            filePath = path;
        }

        private void richTextBox1_Click(object sender, EventArgs e) {
            int index = Form1.openedWindows.IndexOf(fileName);
            if (index == -1) {
                TextWrite newWindow = new TextWrite();
                newWindow.Show();
                newWindow.LoadTextFromFile(filePath, fileName);
                if (fileName != "") Form1.openedWindows.Add(fileName);
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e) {

        }
    }
}
