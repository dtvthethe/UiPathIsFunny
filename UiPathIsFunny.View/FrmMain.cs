using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UiPathIsFunny.Business;
using UiPathIsFunny.Model;
using UiPathIsFunny.View.Model;

namespace UiPathIsFunny.View
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();

            lsvConfig.Columns.Add("Name", lsvConfig.Width/2);
            lsvConfig.Columns.Add("Keyword", lsvConfig.Width / 2);
        }
        private List<Config> lstConfig;
        public static Config configView { get; set; }

        private void btnBrowConfig_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialogConfigPath = new OpenFileDialog();
                openFileDialogConfigPath.InitialDirectory = @"C:\";
                openFileDialogConfigPath.Title = "Browse Config File";

                openFileDialogConfigPath.CheckFileExists = true;
                openFileDialogConfigPath.CheckPathExists = true;

                openFileDialogConfigPath.DefaultExt = "config";
                openFileDialogConfigPath.Filter = "Config file (*.config)|*.config";
                openFileDialogConfigPath.FilterIndex = 2;
                openFileDialogConfigPath.RestoreDirectory = true;

                openFileDialogConfigPath.ReadOnlyChecked = true;
                openFileDialogConfigPath.ShowReadOnly = true;

                if (openFileDialogConfigPath.ShowDialog() == DialogResult.OK)
                {
                    txtConfigTag.Text = openFileDialogConfigPath.FileName;
                    ConfigBusiness cf = new ConfigBusiness();

                    lstConfig = cf.Import(openFileDialogConfigPath.FileName);

                    lstConfig.ForEach(_ => {
                        var item = new ListViewItem(_.Name);
                        item.SubItems.Add(_.KeyWord);
                        lsvConfig.Items.Add(item);
                    });

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmConfig frmConfig = new FrmConfig(null);
            frmConfig.ShowDialog();
            lstConfig.Add(configView);
            lsvConfig.Items.Clear();
            lstConfig.ForEach(_ => {
                var item = new ListViewItem(_.Name);
                item.SubItems.Add(_.KeyWord);
                lsvConfig.Items.Add(item);
            });
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lsvConfig.SelectedItems.Count > 0)
            {
                int index = lsvConfig.Items.IndexOf(lsvConfig.SelectedItems[0]);
                lstConfig.RemoveAt(index);
            }
            lsvConfig.Items.Clear();
            lstConfig.ForEach(_ => {
                var item = new ListViewItem(_.Name);
                item.SubItems.Add(_.KeyWord);
                lsvConfig.Items.Add(item);
            });
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lsvConfig.SelectedItems.Count > 0)
            {
                int index = lsvConfig.Items.IndexOf(lsvConfig.SelectedItems[0]);
                FrmConfig frmConfig = new FrmConfig(lstConfig[index]);
                frmConfig.ShowDialog();
            }
        }
    }
}
