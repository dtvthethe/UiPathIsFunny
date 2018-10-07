using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using UiPathIsFunny.Model;
using UiPathIsFunny.View.Model;

namespace UiPathIsFunny.View
{
    public partial class FrmConfig : Form
    {
        public ConfigViewModel ConfigViewModel { get; set; }

        private List<Config> configs;
        private int index;

        public FrmConfig(ConfigViewModel configViewModel, List<Config> lstConfig, int index)
        {
            InitializeComponent();

            configs = lstConfig;
            this.index = index;
            Init(configViewModel);
        }

        private void Init(ConfigViewModel configViewModel)
        {
            configViewModel = new ConfigViewModel();

            if (configViewModel != null)
            {
                this.ConfigViewModel = configViewModel;
                txtName.Text = this.ConfigViewModel.Name;
                txtKeyword.Text = this.ConfigViewModel.Keyword;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool isValidName = false;
            bool isValidKeyword = false;

            if (String.IsNullOrEmpty(txtName.Text.Trim()))
            {
                errorProvider.SetError(txtName, "Please enter the name value");
                txtName.Focus();
                //return;
            }
            else
            {
                if (!Regex.IsMatch(txtName.Text, @"^[a-zA-Z0-9]+$"))
                {
                    errorProvider.SetError(txtName, "The name is not valid :(");
                    txtName.Focus();
                    //return;
                }
                else
                {
                    if (index == -1) {
                        // add case:
                        if (configs.Where(_ => _.Name.Equals(txtName.Text)).Count() > 0)
                        {
                            errorProvider.SetError(txtName, "Opps! This name really exists in your config.");
                            txtName.Focus();
                            //return;
                        }
                        else {
                            errorProvider.SetError(txtName, null);
                            isValidName = true;
                        }
                    }
                    else
                    {
                        // edit case:
                        bool fl = false;
                        for (int i = 0; i < configs.Count(); i++)
                        {
                            if (i != index && configs[i].Name.Equals(txtName.Text))
                            {
                                fl = true;
                                break;
                            }
                        }
                        if (fl)
                        {
                            errorProvider.SetError(txtName, "Opps! This name really exists in your config.");
                            txtName.Focus();
                        }
                        else
                        {
                            errorProvider.SetError(txtName, null);
                            isValidName = true;
                        }
                    }

                }
            }


            if (String.IsNullOrEmpty(txtKeyword.Text.Trim()))
            {
                errorProvider.SetError(txtKeyword, "Please enter the keyword value");
                txtKeyword.Focus();
                //return;
            }
            else
            {
                if (index == -1) {
                    // add case:
                    if (configs.Where(_ => _.Keyword.Equals(txtKeyword.Text)).Count() > 0)
                    {
                        errorProvider.SetError(txtKeyword, "Opps! This keyword really exists in your config.");
                        txtKeyword.Focus();
                        //return;
                    }
                    else
                    {
                        errorProvider.SetError(txtKeyword, null);
                        isValidKeyword = true;
                    }
                }
                else
                {
                    bool fl = false;
                    for (int i = 0; i < configs.Count(); i++)
                    {
                        if (i != index && configs[i].Keyword.Equals(txtKeyword.Text))
                        {
                            fl = true;
                            break;
                        }
                    }
                    if (fl)
                    {
                        errorProvider.SetError(txtKeyword, "Opps! This keyword really exists in your config.");
                        txtKeyword.Focus();
                    }
                    else
                    {
                        errorProvider.SetError(txtKeyword, null);
                        isValidKeyword = true;
                    }
                }
            }

            if (isValidName == true && isValidKeyword == true)
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ConfigViewModel = null;
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
