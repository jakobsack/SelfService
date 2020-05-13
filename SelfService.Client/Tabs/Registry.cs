using SelfService.Client.WcfServiceReference;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SelfService.Client.Tabs
{
    class Registry
    {
        private MainForm form;
        private RegistryItem[] CurrentRegistry = null;
        private Dictionary<string, RegistryItem> FlatRegistry = new Dictionary<string, RegistryItem>();

        public Registry(MainForm mainForm)
        {
            form = mainForm;
        }

        internal void GetRegistry(string hostname)
        {
            using (WcfClient client = Helper.GetWcfClient(hostname))
            {
                var result = client.GetRegistry();
                form.SetStatusLabel(result.Success, result.Error);
                if (result.Success)
                {
                    CurrentRegistry = result.Data;
                    FlatRegistry.Clear();
                }
                else
                {
                    CurrentRegistry = new RegistryItem[0];
                }
            }

            form.treeViewRegistryKeys.Nodes.Clear();
            form.dataGridViewRegistry.Rows.Clear();
            foreach (RegistryItem item in CurrentRegistry)
            {
                TreeNode thisNode = GetRegistryNode(item);
                thisNode.Text = item.KeyName;
                form.treeViewRegistryKeys.Nodes.Add(thisNode);
            }
        }

        private TreeNode GetRegistryNode(RegistryItem registryItem)
        {
            FlatRegistry[registryItem.KeyName] = registryItem;
            TreeNode treeNode = new TreeNode
            {
                Text = Path.GetFileName(registryItem.KeyName),
            };
            foreach (RegistryItem child in registryItem.RegistryItems)
            {
                treeNode.Nodes.Add(GetRegistryNode(child));
            }
            return treeNode;
        }

        internal void GetValues(string fullPath)
        {
            form.dataGridViewRegistry.Rows.Clear();
            RegistryItem item = FlatRegistry[fullPath];

            foreach (RegistryValue value in item.RegistryValues)
            {
                DataGridViewRow row = form.dataGridViewRegistry.Rows[form.dataGridViewRegistry.Rows.Add()];
                row.Cells[0].Value = value.Name;
                row.Cells[1].Value = value.Value;
                row.Cells[2].Value = value.Type;
            }
        }
    }
}
