using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace TranslationUtil
{


    public partial class Form1 : Form
    {
        string FolderPath;
        List<FileInfo> FilesToCheck = new List<FileInfo>();
        List<FileInfo> AllFiles = new List<FileInfo>();
        DirectoryInfo Directory;
        bool Initialized;
        int TotalLines;
        int LineIndex;
        List<UntranslatedLine> UntranslatedLines = new List<UntranslatedLine>();

        string[] TranslationDataNodes = new string[] { "named_message", "item_message", "message" };
        private DateTime StartTime;

        FileInfo OutputFile;

        public Form1()
        {
            InitializeComponent();
        }

        private void btn_open_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.SelectedPath = FolderPath;
            dialog.ShowNewFolderButton = false;
            var result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                FolderPath = dialog.SelectedPath;
                SetFolder(new DirectoryInfo(dialog.SelectedPath));
            }
        }

        private void btn_count_Click(object sender, EventArgs e)
        {
            InitFiles();
        }

        private void ClearOutput()
        {
            txt_output.Text = String.Format("Looking at {0}.\r\n", Directory.FullName);
            txt_output.Text += "\r\n";

            btn_save.Enabled = false;
        }

        private void InitFiles()
        {
            ClearOutput();

            if (Directory == null)
                return;

            AllFiles.Clear();
            foreach (var file in Directory.EnumerateFiles("*.xml", SearchOption.AllDirectories))
            {
                AllFiles.Add(file);
            }
            FilesToCheck = new List<FileInfo>(AllFiles.OrderBy(x => -x.Length));
            long totalsize = FilesToCheck.Sum(x => x.Length) / 1024;
            long biggestsize = FilesToCheck.First().Length / 1024;
            Initialized = true;

            txt_output.Text += String.Format("Found {0} xml files to check.\r\n", FilesToCheck.Count);
            txt_output.Text += String.Format("(~{0} kb total size, largest is {1} kb)\r\n", totalsize, biggestsize);
            txt_output.Text += "\r\n";
        }

        private void SetFolder(DirectoryInfo dir)
        {
            Directory = dir;
            btn_count.Enabled = button1.Enabled = (Directory != null && Directory.Exists);
            Initialized = false;

            ClearOutput();
        }

        private UntranslatedLine CheckElement(XmlReader node)
        {
            int lineindex = LineIndex;
            bool hastranslation = false;
            string originalline = null;

            while (node.Read())
            {
                if (node.NodeType == XmlNodeType.Element && node.Name == "original")
                {
                    lineindex = ((IXmlLineInfo)node).LineNumber;
                    originalline = node.ReadInnerXml();
                }
                else if (node.NodeType == XmlNodeType.Element && node.Name == "translation")
                {
                    hastranslation = true;
                }
                else if (node.NodeType == XmlNodeType.Whitespace && node.Value.Contains("\n"))
                {
                    LineIndex++;
                }
            }

            UntranslatedLine rline = null;
            if(!hastranslation)
                rline = new UntranslatedLine() { LineIndex = lineindex, OriginalLine = originalline };

            return rline;
        }

        private void background_load_DoWork(object sender, DoWorkEventArgs e)
        {
            while (FilesToCheck.Count > 0)
            {
                FileInfo file = FilesToCheck.First();
                FilesToCheck.RemoveAt(0);

                LineIndex = 0;

                int fileschecked = AllFiles.Count - FilesToCheck.Count;
                background_load.ReportProgress((int)((fileschecked / (float)AllFiles.Count) * 100));

                Stream stream = file.OpenRead();
                var xml = XmlReader.Create(stream);
                while (xml.Read())
                {
                    if (xml.NodeType == XmlNodeType.Element && TranslationDataNodes.Contains(xml.Name))
                    {
                        
                        var subtree = xml.ReadSubtree();
                        var line = CheckElement(subtree);
                        if (line != null)
                        {
                            UntranslatedLines.Add(line);
                            line.SourceFile = file;
                        }
                        TotalLines++;
                    }
                    else if (xml.NodeType == XmlNodeType.Whitespace && xml.Value.Contains("\n"))
                    {
                        LineIndex++;
                    }
                }
                stream.Close();
            }
        }

        private void background_load_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progress_read.Value = e.ProgressPercentage;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!Initialized)
                InitFiles();

            Start();
        }

        private void Start()
        {
            TotalLines = 0;
            UntranslatedLines.Clear();
            StartTime = DateTime.Now;
            background_load.RunWorkerAsync();
            button1.Enabled = btn_count.Enabled = btn_open.Enabled = false;
        }

        private void SaveResult(FileInfo file)
        {
            OutputFile = file;
            background_save.RunWorkerAsync();
            button1.Enabled = btn_count.Enabled = btn_open.Enabled = btn_save.Enabled = false;
        }

        private void background_load_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            button1.Enabled = btn_count.Enabled = btn_open.Enabled = true;
            Initialized = false;

            int translatedlines = TotalLines - UntranslatedLines.Count;
            float completion = translatedlines / (float)TotalLines;
            TimeSpan runtime = DateTime.Now - StartTime;

            txt_output.Text += String.Format("Finished in {0}\r\n", runtime);
            txt_output.Text += String.Format("There's {0}/{1}({2}%) lines translated.\r\n", translatedlines, TotalLines, (int)(completion * 100));
            txt_output.Text += "\r\n";

            btn_save.Enabled = true;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.DefaultExt = "txt";
            dialog.AddExtension = true;
            dialog.OverwritePrompt = true;
            var result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                SaveResult(new FileInfo(dialog.FileName));
            }

        }

        private void background_save_DoWork(object sender, DoWorkEventArgs e)
        {
            var stream = OutputFile.CreateText();
            stream.WriteLine(txt_output.Text);
            stream.WriteLine();
            UntranslatedLines.OrderBy(x => x.SourceFile.Name);
            FileInfo currentfile = null;
            int n = 0;
            foreach (var line in UntranslatedLines)
            {
                n++;

                background_save.ReportProgress((int)((n / (float)UntranslatedLines.Count) * 100));

                if (currentfile != line.SourceFile)
                {
                    stream.WriteLine(String.Format("--- {0} ---", line.SourceFile.FullName));
                    currentfile = line.SourceFile;
                }

                stream.WriteLine(String.Format("Line {0} - {1}", line.LineIndex, line.OriginalLine));
            }
            stream.Close();
        }

        private void background_save_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progress_read.Value = e.ProgressPercentage;
        }

        private void background_save_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            button1.Enabled = btn_count.Enabled = btn_open.Enabled = btn_save.Enabled = true;
        }
    }

    public class UntranslatedLine
    {
        public int LineIndex;
        public FileInfo SourceFile;
        public string OriginalLine;
    }
}
