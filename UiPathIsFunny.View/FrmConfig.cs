using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UiPathIsFunny.Model;
using UiPathIsFunny.View.Model;

namespace UiPathIsFunny.View
{
    public partial class FrmConfig : Form
    {
        private Config config;

        public FrmConfig(ConfigViewModel cf)
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FrmMain.configView = new ConfigViewModel
            {
                Name = "AA",
                Keyword = "AAAAAA"
            };
            this.Close();
        }
    }
}
