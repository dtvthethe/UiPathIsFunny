using AutoMapper;
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
        public ConfigViewModel configViewModel { get; set; }

        public FrmConfig(ConfigViewModel configViewModel)
        {
            InitializeComponent();

            Init(configViewModel);
        }

        private void Init(ConfigViewModel configViewModel) {
            configViewModel = new ConfigViewModel();

            if (configViewModel != null)
            {
                this.configViewModel = configViewModel;
                txtName.Text = this.configViewModel.Name;
                txtKeyword.Text = this.configViewModel.Keyword;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            configViewModel = new ConfigViewModel
            {
                Name = txtName.Text,
                Keyword = txtKeyword.Text
            };
            DialogResult = DialogResult.OK;

            this.Close();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            configViewModel = null;
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
