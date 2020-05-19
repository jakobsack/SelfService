using SelfService.Client.WcfServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SelfService.Client.Tabs
{
    class Msmq
    {
        private string Hostname = "";
        private MainForm form;
        private Queue[] Queues = null;
        private Queue Queue = null;
        private QueueMessage[] Messages = null;

        public Msmq(MainForm mainForm)
        {
            form = mainForm;
        }

        public void GetQueues(string hostname)
        {
            Hostname = hostname;
            using (WcfClient client = Helper.GetWcfClient(Hostname))
            {
                var result = client.GetQueues();
                form.SetStatusLabel(result.Success, result.Error);
                if (result.Success)
                {
                    Queues = result.Data;
                }
                else
                {
                    Queues = new Queue[0];
                }
            }

            form.dataGridViewQueues.Rows.Clear();
            form.dataGridViewMessages.Rows.Clear();
            form.textBoxMessage.Text = "";

            foreach (Queue queue in Queues)
            {
                DataGridViewRow row = form.dataGridViewQueues.Rows[form.dataGridViewQueues.Rows.Add()];
                row.Cells[0].Value = queue.Label;
                row.Cells[1].Value = queue.Messages;
            }
        }

        public void GetMessageList(int index)
        {
            Queue = Queues[index];

            form.dataGridViewMessages.Rows.Clear();
            form.textBoxMessage.Text = "";

            using (WcfClient client = Helper.GetWcfClient(Hostname))
            {
                var result = client.GetQueueMessageList(Queue.Path);
                form.SetStatusLabel(result.Success, result.Error);
                if (result.Success)
                {
                    Messages = result.Data;
                }
                else
                {
                    Messages = new QueueMessage[0];
                }
            }

            foreach (QueueMessage queueMessage in Messages)
            {
                DataGridViewRow row = form.dataGridViewMessages.Rows[form.dataGridViewMessages.Rows.Add()];
                row.Cells[0].Value = queueMessage.Label;
                row.Cells[1].Value = queueMessage.Size.ToHumanBytes();
                row.Cells[2].Value = queueMessage.SentTime;
            }
        }

        internal void GetMessage(int index)
        {
            QueueMessage message = Messages[index];
            QueueMessage fullMessage = null;

            using (WcfClient client = Helper.GetWcfClient(Hostname))
            {
                var result = client.GetQueueMessage(Queue.Path, message.Id);
                form.SetStatusLabel(result.Success, result.Error);
                if (result.Success)
                {
                    fullMessage = result.Data;
                }
            }

            if (fullMessage != null)
            {
                string text = Encoding.Unicode.GetString(Convert.FromBase64String(fullMessage.Body));
                form.textBoxMessage.Text = text;
            }
        }
    }
}
