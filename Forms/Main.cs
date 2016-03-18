using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SageIntegration;

namespace WebsiteXMLImport
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            
            SO_OrderBatch batch = new SO_OrderBatch();

            OpenFileDialog OpenXMLDialog = new OpenFileDialog();

            OpenXMLDialog.InitialDirectory = "C:\\";
            OpenXMLDialog.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
            OpenXMLDialog.RestoreDirectory = true;


            if(OpenXMLDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    batch = Orders.ReadXML(OpenXMLDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            // Makes sure each order has a CustomerNo for Sage100

            foreach (SalesOrder order in batch.SalesOrders())
            {
                if (order.CustomerNo == string.Empty)
                {
                    using (UserEntry EntryDialog = new UserEntry("Please input a CustomerNo", 
                        "A CustomerNo was not found in the file." + 
                        Environment.NewLine + 
                        "[Order #]: " + order.CustomerPONo + " [Bill To Company]: " + order.BillToCompany))
                    {
                        if (EntryDialog.ShowDialog() == DialogResult.OK)
                        {
                            order.CustomerNo = EntryDialog.Input;
                        }
                    }
                }
            }


            Config config = Config.Load();
            batch.Username = config.Username;
            batch.Password = config.Password;
            batch.Company = config.Company;

            batch.Post();

        }

        private void UpdateStatus(SO_OrderBatch.Status status, int total, int current)
        {
            if (status == SO_OrderBatch.Status.Working)
            {
                UI_Execute(() => progressBar.Maximum = total);
                UI_Execute(() => progressBar.Value = current);
                WriteToTextBox("Posting Orders: " + current.ToString() + " of " + total.ToString());
            }
            if (status == SO_OrderBatch.Status.Completed)
            {
                WriteToTextBox("Website XML Import: Finished Posting Orders");
            }

        }

        public static void OrdersImported(List<SalesOrder> salesorders)
        {
            if (salesorders.Count < 1)
            {
                return;
            }
            System.Windows.Forms.MessageBox.Show("Imported " + salesorders.Count() + " Order(s) for " + salesorders[0].CustomerNo + ": " + salesorders.Min(e => e.SalesOrderNo) + " to " + salesorders.Max(e => e.SalesOrderNo));
        }

        public void WriteToTextBox(string text)
        {
            UI_Execute(() => txtStatus.Text = (txtStatus.Text + Environment.NewLine + text).Trim());
        }

        private void Main_Load(object sender, EventArgs e)
        {
            // Connect to events
            SO_OrderBatch.Completed += new SO_OrderBatch.BatchComplete(OrdersImported);
            SO_OrderBatch.StatusChanged += new SO_OrderBatch.UpdateStatus(UpdateStatus);
            SageIntegration.Core.SageObject.SageError += new SageIntegration.Core.SageObject.RaiseEvent(WriteToTextBox);
        }
        public void UI_Execute(Action a)
        {
            if (InvokeRequired)
                BeginInvoke(a);
            else
                a();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            Settings SettingsForm = new Settings();
            SettingsForm.ShowDialog();
        }

    }
}
