using System;
using System.Collections.Generic;
using System.Windows.Forms;
using UiPathIsFunny.Model;
using UiPathIsFunny.Utility;
using UiPathIsFunny.View.Model;

namespace UiPathIsFunny.View
{
    public partial class FrmConfig : Form
    {
        public ConfigViewModel ConfigViewModel { get; set; }

        private List<Config> configs;
        private int index;

        public FrmConfig(ConfigViewModel configViewModel, List<Config> configs, int index)
        {
            InitializeComponent();

            this.configs = configs;
            this.index = index;
            Init(configViewModel);
        }

        private void Init(ConfigViewModel configViewModel)
        {
            if (configViewModel != null)
            {
                ConfigViewModel = configViewModel;
                this.ConfigViewModel = configViewModel;
                txtName.Text = this.ConfigViewModel.Name;
                txtKeyword.Text = this.ConfigViewModel.Keyword;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var nameValidate = ConfigValidate.NameValidate(txtName.Text, index, configs);
                var keywordValidate = ConfigValidate.KeywordValidate(txtKeyword.Text, index, configs);

                if (nameValidate.IsError)
                {
                    errorProvider.SetError(txtName, nameValidate.Message);
                    txtName.Focus();
                }
                else
                    errorProvider.SetError(txtName, null);

                if (keywordValidate.IsError)
                {
                    errorProvider.SetError(txtKeyword, keywordValidate.Message);
                    txtKeyword.Focus();
                }
                else
                    errorProvider.SetError(txtKeyword, null);


                if (!nameValidate.IsError && !keywordValidate.IsError)
                {
                    ConfigViewModel = new ConfigViewModel
                    {
                        Name = txtName.Text,
                        Keyword = txtKeyword.Text
                    };
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                    return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Oops", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ConfigViewModel = null;
                DialogResult = DialogResult.Cancel;
                return;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ConfigViewModel = null;
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}
