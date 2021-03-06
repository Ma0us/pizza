using DarkUI.Forms;
using Microsoft.Win32;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace pizza
{
    public partial class Form1 : DarkForm
    {
        public Form1(string fn)
        {
            InitializeComponent();
            if (Properties.Settings.Default.Font == null)
            {
                Properties.Settings.Default.Font = textBox.Font;
            } else
            {
                textBox.Font = Properties.Settings.Default.Font;
            }
            textBox.Clear();
            using (StreamReader sr = new StreamReader(fn.ToString())) 
            {
                textBox.Text = sr.ReadToEnd();
                sr.Close();
            }
            filePath = fn.ToString();
            toolStripMenuItem2.Enabled = true;
            this.Text = filePath + " -  pizza";
            isSaved = true;
        }

        public Form1()
        {
            InitializeComponent();
            if (Properties.Settings.Default.Font == null)
            {
                Properties.Settings.Default.Font = textBox.Font;
            }
            else
            {
                textBox.Font = Properties.Settings.Default.Font;
            }
        }

        public string filePath = "";
        public bool isSaved = false;

        public void Save()
        {
            StreamWriter txtoutput = new StreamWriter(filePath);
            txtoutput.Write(textBox.Text);
            txtoutput.Close();
            this.Text = filePath + " -  pizza";
            isSaved = true;
        }


        // Dark Titlebar
        [DllImport("DwmApi")] //System.Runtime.InteropServices
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);

        protected override void OnHandleCreated(EventArgs e)
        {
            if (DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4) != 0)
                DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4);
        }

        private static bool IsWindows10OrGreater(int build = -1)
        {
            return Environment.OSVersion.Version.Major >= 10 && Environment.OSVersion.Version.Build >= build;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Title = "Open..";
            openfile.InitialDirectory = "c:\\";
            openfile.Filter = "Text Documents(*.txt)|*.txt|All files (*.*)|*.*";
            openfile.RestoreDirectory = true;
            if (openfile.ShowDialog() == DialogResult.OK)
            {
                textBox.Clear();
                using (StreamReader sr = new StreamReader(openfile.FileName))
                {
                    textBox.Text = sr.ReadToEnd();
                    sr.Close();
                }
                filePath = openfile.FileName;
                toolStripMenuItem2.Enabled = true;
                this.Text = filePath + " -  pizza";
                isSaved = true;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.Title = "Save as..";
            savefile.InitialDirectory = "c:\\";
            savefile.Filter = "Text Documents(*.txt)|*.txt|All files (*.*)|*.*";
            savefile.RestoreDirectory = true;
            if (savefile.ShowDialog() == DialogResult.OK)
            {
                StreamWriter txtoutput = new StreamWriter(savefile.FileName);
                txtoutput.Write(textBox.Text);
                txtoutput.Close();
                toolStripMenuItem2.Enabled = true;
                this.Text = savefile.FileName + " -  pizza";
                filePath = savefile.FileName;
                this.Text = filePath + " -  pizza";
                isSaved = true;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox.Undo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox.Paste();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox.ClearSelected();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox.Redo();
        }

        private void deleteLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox.ClearCurrentLine();
        }

        public void changeLang(FastColoredTextBoxNS.Language lang)
        {
            textBox.Language = lang;
            //reload text
            string bstring;
            bstring = textBox.Text;
            textBox.Clear();
            textBox.Text = bstring;
        }

        private void noneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeLang(FastColoredTextBoxNS.Language.Custom);
        }

        private void cToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeLang(FastColoredTextBoxNS.Language.CSharp);
        }

        private void vBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeLang(FastColoredTextBoxNS.Language.VB);
        }

        private void hTMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeLang(FastColoredTextBoxNS.Language.HTML);
        }

        private void xMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeLang(FastColoredTextBoxNS.Language.XML);
        }

        private void sQLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeLang(FastColoredTextBoxNS.Language.SQL);
        }

        private void pHPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeLang(FastColoredTextBoxNS.Language.PHP);
        }

        private void jSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeLang(FastColoredTextBoxNS.Language.JS);
        }

        private void luaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeLang(FastColoredTextBoxNS.Language.Lua);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DarkForm frm = new Form1();
            frm.Show();
        }

        private void changeFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                textBox.Font = fontDialog.Font;
                Properties.Settings.Default.Font = fontDialog.Font;
                Properties.Settings.Default.Save();
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void googleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string url = "https://www.google.com/search?q=" + textBox.SelectedText;
            Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true });
        }

        private void duckDuckGoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string url = "https://duckduckgo.com/?t=ffab&q=" + textBox.SelectedText;
            Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true });
        }

        private void braveSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string url = "https://search.brave.com/search?q=" + textBox.SelectedText;
            Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true });
        }

        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox.Zoom = textBox.Zoom + 10;
        }

        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox.Zoom = textBox.Zoom - 10;
        }

        private void restoreDefaultZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox.Zoom = 100;
        }

        private void textBox_ZoomChanged(object sender, EventArgs e)
        {
            lineLabel.Text = (textBox.Zoom.ToString() + "%");
        }

        private void textBox_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            lineLabel.Text = ("ln: " + textBox.LinesCount.ToString());
            isSaved = false;
            this.Text = "*" + filePath + " -  pizza";
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (toolStripMenuItem4.Checked == true) {
                toolStripMenuItem4.Checked = false;
                autoSaveTimer.Enabled = false;
            } else
            {
                toolStripMenuItem4.Checked = true;
                autoSaveTimer.Enabled = true;
            }
        }

        private void autoSaveTimer_Tick(object sender, EventArgs e)
        {
            if (filePath != "")
            {
                Save();
            }
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            textBox.ShowFindDialog();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            textBox.ShowReplaceDialog();
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            textBox.SelectAll();
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            textBox.InsertText(DateTime.Now.ToString());
        }

        private void viewOnlineHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string url = "https://github.com/Ma0us/pizza/wiki";
            Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true });
        }

        private void sendFeedbackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string url = "https://github.com/Ma0us/pizza/issues/new";
            Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true });
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string url = "https://github.com/Ma0us/pizza#readme";
            Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true });
        }

        private void showLineNumbersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (showLineNumbersToolStripMenuItem.Checked == true) {
                showLineNumbersToolStripMenuItem.Checked = false;
                textBox.ShowLineNumbers = false;
            } else
            {
                showLineNumbersToolStripMenuItem.Checked = true;
                textBox.ShowLineNumbers = true;
            }
        }
    }
}