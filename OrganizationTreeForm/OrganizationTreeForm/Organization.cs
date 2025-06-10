using OrganizationTreeForm.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrganizationTreeForm
{
    public partial class Organization : Form
    {
        public Organization()
        {
            InitializeComponent();
            Company OrgTree = new Company();
        }
    }
}
