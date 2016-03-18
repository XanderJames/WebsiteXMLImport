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
    public partial class Settings : Form
    {
        Config config = Config.Load();
        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            foreach (SageIntegration.Core.Company company in SageIntegration.Core.CompanyList())
            {
                cmbCompanies.Items.Add(company.Code);
            }

            txtUsername.Text = config.Username;
            txtPassword.Text = config.Password;
            cmbCompanies.SelectedItem = config.Company;

        }

        
        
        

        private void btnOK_Click(object sender, EventArgs e)
        {
            config.Save();
            this.Close();
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            config.Username = txtUsername.Text;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            config.Password = txtPassword.Text;
        }

        private void cmbCompanies_SelectionChangeCommitted(object sender, EventArgs e)
        {
            config.Company = cmbCompanies.SelectedItem.ToString();
        }
    }
}
