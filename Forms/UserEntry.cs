using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebsiteXMLImport
{
    public partial class UserEntry : Form
    {
        public string Input { get; set; }

        public string Message { get; set; }
        public UserEntry()
        {
            InitializeComponent();
        }
        public UserEntry (string title, string message)
        {
            InitializeComponent();
            this.Text = title;
            Message = message;
        }

        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            Input = txtInput.Text;
        }

        private void UserEntry_Load(object sender, EventArgs e)
        {
            lblMessage.Text = Message;
        }

    }
}
