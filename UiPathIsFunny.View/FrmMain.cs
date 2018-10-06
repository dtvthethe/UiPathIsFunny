using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using UiPathIsFunny.Business;
using UiPathIsFunny.Model;
using UiPathIsFunny.View.Model;
using static UiPathIsFunny.Utility.Enums;

namespace UiPathIsFunny.View
{
    public partial class FrmMain : Form
    {

        private List<Config> lstConfig;
        private ConfigBusiness configBusiness;
        private string[] filesProcess;

        public FrmMain()
        {
            InitializeComponent();

            lstConfig = new List<Config>();
            configBusiness = new ConfigBusiness();

            lsvConfig.Columns.Add("Name", lsvConfig.Width / 2);
            lsvConfig.Columns.Add("Keyword", lsvConfig.Width / 2);

            lsvStatus.Columns.Add("", lsvStatus.Width);


            var lstStAppend = new List<Status>();
            lstStAppend.Add(new Status
            {
                CurrentTime = DateTime.Now,
                Message = "Please click on Browser button to import your XAML files!",
                MsgStatus = MessageStatus.Fail
            });
            var lstSt = StatusList(lstStAppend);

            lstSt.ForEach(_ =>
            {
                var item = new ListViewItem(_.CurrentTime.ToString("dd/MM/yyyy HH:mm:ss") + " - " + _.Message);
                lsvStatus.Items.Add(item);
                if (_.MsgStatus == MessageStatus.Fail)
                    item.ForeColor = Color.Red;
            });


            List<ExportViewModel> exportViewModels = new List<ExportViewModel>();
            exportViewModels.Add(new ExportViewModel
            {
                Key = 1,
                Value = "JSON"
            });
            exportViewModels.Add(new ExportViewModel
            {
                Key = 2,
                Value = "CSV"
            });
            exportViewModels.Add(new ExportViewModel
            {
                Key = 3,
                Value = "Excel"
            });

            cmbExport.DataSource = new BindingSource(exportViewModels, null);
            cmbExport.DisplayMember = "Value";
            cmbExport.ValueMember = "Key";
        }


        private List<Status> StatusList(List<Status> statuses)
        {

            var lstSt = new List<Status>();
            lstSt.Add(new Status
            {
                CurrentTime = DateTime.Now,
                Message = "Hi! Let make it funny now!",
                MsgStatus = MessageStatus.None,
            });

            if (statuses != null && statuses.Count > 0)
                lstSt.AddRange(statuses);

            return lstSt;
        }

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
                    lsvConfig.Items.Clear();
                    lstConfig.ForEach(_ =>
                    {
                        var item = new ListViewItem(_.Name);
                        item.SubItems.Add(_.Keyword);
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
            if (frmConfig.DialogResult == DialogResult.Cancel)
                return;

            if (frmConfig.configViewModel != null && !String.IsNullOrEmpty(frmConfig.configViewModel.Name.Trim()))
            {
                lstConfig.Add(Mapper.Map<Config>(frmConfig.configViewModel));
                lsvConfig.Items.Clear();
                lstConfig.ForEach(_ =>
                {
                    var item = new ListViewItem(_.Name);
                    item.SubItems.Add(_.Keyword);
                    lsvConfig.Items.Add(item);
                });
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lsvConfig.SelectedItems.Count > 0)
            {
                int index = lsvConfig.Items.IndexOf(lsvConfig.SelectedItems[0]);
                lstConfig.RemoveAt(index);
                lsvConfig.Items.Clear();
                lstConfig.ForEach(_ =>
                {
                    var item = new ListViewItem(_.Name);
                    item.SubItems.Add(_.Keyword);
                    lsvConfig.Items.Add(item);
                });
            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lsvConfig.SelectedItems.Count > 0)
            {
                int index = lsvConfig.Items.IndexOf(lsvConfig.SelectedItems[0]);
                FrmConfig frmConfig = new FrmConfig(Mapper.Map<ConfigViewModel>(lstConfig[index]));
                frmConfig.ShowDialog();
                if (frmConfig.DialogResult == DialogResult.Cancel)
                    return;

                if (frmConfig.configViewModel != null && !String.IsNullOrEmpty(frmConfig.configViewModel.Name.Trim()))
                {
                    lstConfig[index] = Mapper.Map<Config>(frmConfig.configViewModel);
                    lsvConfig.Items.Clear();
                    lstConfig.ForEach(_ =>
                    {
                        var item = new ListViewItem(_.Name);
                        item.SubItems.Add(_.Keyword);
                        lsvConfig.Items.Add(item);
                    });
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (lstConfig == null || lstConfig.Count == 0)
                return;

            if (String.IsNullOrEmpty(txtConfigTag.Text.Trim()))
            {
                FolderBrowserDialog folderDlg = new FolderBrowserDialog();
                folderDlg.ShowNewFolderButton = true;
                // Show the FolderBrowserDialog.  
                DialogResult result = folderDlg.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string pathFile = Path.Combine(folderDlg.SelectedPath.Trim(), "UiPath_Is_Funny_Config_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".config");
                    txtConfigTag.Text = pathFile;

                    configBusiness.SaveAll(lstConfig, pathFile);
                }
            }
            else
            {
                configBusiness.SaveAll(lstConfig, txtConfigTag.Text.Trim());
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (filesProcess == null && filesProcess.Count() == 0)
            {
                MessageBox.Show("Please click on Browser button to import your XAML files!");
                return;
            }

            if (lstConfig == null && lstConfig.Count() == 0)
            {
                MessageBox.Show("Please setting your Keywords first!");
                return;
            }

            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            // Show the FolderBrowserDialog.  
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (String.IsNullOrEmpty(folderDlg.SelectedPath.Trim()))
                    return;
                FrmLoad frmLoad = new FrmLoad(folderDlg.SelectedPath.Trim(), filesProcess, lstConfig, chkDetail.Checked);
                frmLoad.ShowDialog();

                // update list here:
            }

            filesProcess = null;
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void btnExport_Click(object sender, EventArgs e)
        {

        }

        private void btnFolderXAML_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            // Show the FolderBrowserDialog.  
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (String.IsNullOrEmpty(folderDlg.SelectedPath.Trim()))
                    return;
                string directory = folderDlg.SelectedPath.Trim();
                filesProcess = Directory.GetFiles(directory, "*.xaml", SearchOption.AllDirectories);

                txtXAMpath.Text = directory;
                lsvStatus.Items.Clear();

                var lstStAppend = new List<Status>();

                if (filesProcess.Count() == 0)
                    lstStAppend.Add(new Status
                    {
                        CurrentTime = DateTime.Now,
                        Message = "Wrong! There aren't any file XAML in this folder :(",
                        MsgStatus = MessageStatus.Warning
                    });
                else {
                    lstStAppend.Add(new Status
                    {
                        CurrentTime = DateTime.Now,
                        Message = "List of XAML files loaded successfully!",
                        MsgStatus = MessageStatus.None
                    });

                    foreach (var item in filesProcess)
                    {
                        lstStAppend.Add(new Status
                        {
                            CurrentTime = DateTime.Now,
                            Message = item + " -> Ready",
                            MsgStatus = MessageStatus.OK
                        });
                    }

                    lstStAppend.Add(new Status
                    {
                        CurrentTime = DateTime.Now,
                        Message = "Click Start button to Make it Funny!",
                        MsgStatus = MessageStatus.None
                    });
                }
                
                
                var lstSt = StatusList(lstStAppend);

                lstSt.ForEach(_ =>
                {
                    var item = new ListViewItem(_.CurrentTime.ToString("dd/MM/yyyy HH:mm:ss") + " - " + _.Message);
                    lsvStatus.Items.Add(item);
                    if (_.MsgStatus == MessageStatus.Fail)
                        item.ForeColor = Color.Red;
                    if (_.MsgStatus == MessageStatus.OK)
                        item.ForeColor = Color.Green;
                    if (_.MsgStatus == MessageStatus.Warning)
                        item.ForeColor = Color.Orange;
                });
            }
        }
    }
}
