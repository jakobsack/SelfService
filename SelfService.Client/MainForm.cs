using SelfService.Client.Tabs;
using SelfService.Client.WcfServiceReference;
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

namespace SelfService.Client
{
    public partial class MainForm : Form
    {
        private Msmq MsmqTab = null;
        private Registry RegistryTab = null;
        private Files FilesTab = null;
        private Services ServicesTab = null;
        private List<string> LogMessages = new List<string>();

        public MainForm()
        {
            InitializeComponent();
            ResizeObjects();

            // Load Icons from Shell32
            imageList1.Images.Add(Helper.ExtractIcon("shell32.dll", 124, false));
            imageList1.Images.Add(Helper.ExtractIcon("shell32.dll", 45, false));
            imageList1.Images.Add(Helper.ExtractIcon("shell32.dll", 238, false));
            imageList1.Images.Add(Helper.ExtractIcon("shell32.dll", 3, false));
            imageList1.Images.Add(Helper.ExtractIcon("shell32.dll", 0, false));

            // Machines we can contact
            listBoxMachines.Items.Clear();
            string[] machines = File.ReadAllLines("machines.txt");
            foreach (string machine in machines.Where(x => !string.IsNullOrWhiteSpace(x)))
            {
                listBoxMachines.Items.Add(machine);
            }

            // The tabs
            MsmqTab = new Tabs.Msmq(this);
            RegistryTab = new Tabs.Registry(this);
            FilesTab = new Files(this);
            ServicesTab = new Services(this);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void listBoxMachines_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxMachines.SelectedIndex == -1)
            {
                return;
            }

            splitContainer1.Panel2.Enabled = true;
            UpdateActiveTab();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateActiveTab();
        }

        private void UpdateActiveTab()
        {
            if (tabControl1.SelectedTab == tabPageMsmq)
            {
                UpdateMsmqPage();
            }
            else if (tabControl1.SelectedTab == tabPageRegistry)
            {
                UpdateRegistryPage();
            }
            else if (tabControl1.SelectedTab == tabPageFiles)
            {
                UpdateFilesPage();
            }
            else if (tabControl1.SelectedTab == tabPageServices)
            {
                UpdateServicesPage();
            }


            ResizeObjects();
        }

        #region FilesTab
        private void listViewFileSources_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Don't do anything on empty list
            if(listViewFileSources.SelectedIndices.Count == 0)
            {
                return;
            }

            FilesTab.SelectItem(listViewFileSources.SelectedItems[0]);
        }

        internal void JumpToFolder(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            FilesTab.GetFolder(item.Tag as string);
        }

        private void UpdateFilesPage()
        {
            string hostname = listBoxMachines.SelectedItem.ToString();
            FilesTab.GetFileSources(hostname);
        }

        private void buttonDownload_Click(object sender, EventArgs e)
        {
            FilesTab.DownloadFile();
        }

        private void buttonShow_Click(object sender, EventArgs e)
        {
            FilesTab.ShowFile();
        }

        private void buttonTail_Click(object sender, EventArgs e)
        {
            FilesTab.TailFile();
        }
        #endregion

        #region RegistryTab
        private void UpdateRegistryPage()
        {
            string hostname = listBoxMachines.SelectedItem.ToString();
            RegistryTab.GetRegistry(hostname);
        }

        private void treeViewRegistryKeys_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode treeNode = e.Node;
            RegistryTab.GetValues(treeNode.FullPath);
        }
        #endregion

        #region MSMQTab
        private void UpdateMsmqPage()
        {
            string hostname = listBoxMachines.SelectedItem.ToString();
            MsmqTab.GetQueues(hostname);
        }

        private void dataGridViewQueues_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            MsmqTab.GetMessageList(index);
        }

        private void dataGridViewMessages_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            MsmqTab.GetMessage(index);
        }
        #endregion

        #region ServicesTab
        private void UpdateServicesPage()
        {
            string hostname = listBoxMachines.SelectedItem.ToString();
            ServicesTab.GetServices(hostname);
        }
        #endregion

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string text = "SelfService has been written by Jakob Sack. You can find its source under https://github.com/jakobsack/SelfService";
            string caption = "About SelfService";
            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        internal void SetStatusLabel(bool success, string error)
        {
            if (success)
            {
                toolStripStatusLabelStatus.Text = "Success";
            }
            else
            {
                string[] lines = error.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                toolStripStatusLabelStatus.Text = lines.FirstOrDefault();
                LogMessages.Add(error);
                listBoxErrorMessages.Items.Add(DateTime.Now.ToString());
            }
        }

        private void toolStripStatusLabelStatus_Click(object sender, EventArgs e)
        {
            tabControl2.SelectedTab = tabPage2;
        }

        private void listBoxErrorMessages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxErrorMessages.SelectedIndex == -1)
            {
                return;
            }

            textBoxErrorMessage.Text = LogMessages[listBoxErrorMessages.SelectedIndex];
        }

        internal void AddTextToListBoxFile(string text)
        {
            // Stop drawing
            listBoxFile.BeginUpdate();

            // Figure out if we are showing the last five lines
            bool tailingFile = false;
            int oldTopIndex = listBoxFile.TopIndex;
            int lineheight = listBoxFile.ItemHeight;
            int topIndex = listBoxFile.TopIndex;
            int visibleLines = listBoxFile.Height / lineheight;
            if (listBoxFile.Items.Count < 2)
            {
                tailingFile = true;
            }
            else if (listBoxFile.Items.Count - (topIndex + visibleLines) < 2)
            {
                tailingFile = true;
            }

            // Split text in lines, strip \r
            string[] lines = text
                .Split('\n')
                .Select(x => x.TrimEnd('\r'))
                .ToArray();

            // If items exist the append first line to last of old content
            int line = 0;
            int itemCount = listBoxFile.Items.Count;
            if (itemCount > 0)
            {
                listBoxFile.Items[itemCount - 1] += lines[0];
                line++;
            }

            // now add rest of lines to box
            for (; line < lines.Length; line++)
            {
                listBoxFile.Items.Add(lines[line]);
            }

            // move to the right position
            if (tailingFile)
            {
                // + 2 for (int)height/lineHeight and 1 for 0 based index
                topIndex = listBoxFile.Items.Count - visibleLines + 2;
                if (topIndex < 0)
                {
                    topIndex = 0;
                }
                listBoxFile.TopIndex = topIndex;
            }
            else
            {
                listBoxFile.TopIndex = oldTopIndex;
            }

            // Draw again
            listBoxFile.EndUpdate();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            ResizeObjects();
        }

        private void ResizeObjects()
        {
            listBoxFile.Height = splitContainer5.Panel2.Height - 33;
            splitContainer5.Height = tabPageFiles.Height - 33;
        }
    }
}
