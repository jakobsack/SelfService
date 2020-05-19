using SelfService.Client.WcfServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SelfService.Client.Tabs
{
    class Services
    {
        private string Hostname = "";
        private MainForm form;
        private ServiceItem[] ServiceItems = null;

        public Services(MainForm mainForm)
        {
            form = mainForm;
        }

        public void GetServices(string hostname)
        {
            Hostname = hostname;
            using (WcfClient client = Helper.GetWcfClient(Hostname))
            {
                var result = client.GetServices();
                form.SetStatusLabel(result.Success, result.Error);
                if (result.Success)
                {
                    ServiceItems = result.Data;
                }
                else
                {
                    ServiceItems = new ServiceItem[0];
                }
            }

            form.dataGridViewServices.Rows.Clear();

            foreach (ServiceItem serviceItem in ServiceItems)
            {
                DataGridViewRow row = form.dataGridViewServices.Rows[form.dataGridViewServices.Rows.Add()];
                row.Cells[0].Value = serviceItem.Name;
                row.Cells[1].Value = serviceItem.Status;
                row.Cells[2].Value = serviceItem.Memory.ToHumanBytes();
            }
        }
    }
}
