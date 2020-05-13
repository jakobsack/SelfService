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
        private List<string> LogMessages;

        public MainForm()
        {
            InitializeComponent();

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
        }

        #region FilesTab
        private void UpdateFilesPage()
        {
            string hostname = listBoxMachines.SelectedItem.ToString();
            FilesTab.GetFileSources(hostname);
        }

        private void listBoxFileSources_SelectedIndexChanged(object sender, EventArgs e)
        {
            string path = listBoxFileSources.SelectedItem.ToString();
            FilesTab.GetFolder(path);
        }

        private void treeViewFiles_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode treeNode = e.Node;
            FilesTab.SelectFile(treeNode.FullPath);
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
                toolStripStatusLabelStatus.Text = error;
                LogMessages.Add(error);
            }
        }
    }
}
