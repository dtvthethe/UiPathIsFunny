using AutoMapper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
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
        private List<LogStatus> logStatuses;
        private ConfigBusiness configBusiness;
        private string[] filesProcess;

        public FrmMain()
        {
            InitializeComponent();

            lstConfig = new List<Config>();
            configBusiness = new ConfigBusiness();

            lsvConfig.Columns.Add("Name", (lsvConfig.Width / 2) - 5);
            lsvConfig.Columns.Add("Keyword", (lsvConfig.Width / 2));

            lsvStatus.Columns.Add("", lsvStatus.Width - 10);


            var lstStAppend = new List<LogStatus>();
            lstStAppend.Add(new LogStatus
            {
                CurrentTime = DateTime.Now,
                Message = "Please click on Browser button to import your XAML files!",
                MsgStatus = MessageStatus.Fail
            });
            logStatuses = StatusList(lstStAppend);

            logStatuses.ForEach(_ =>
            {
                var item = new ListViewItem(_.CurrentTime.ToString("dd/MM/yyyy HH:mm:ss") + " - " + _.Message);
                lsvStatus.Items.Add(item);
                if (_.MsgStatus == MessageStatus.Fail)
                    item.ForeColor = Color.Red;
            });

            cmbExport.DataSource = Enum.GetValues(typeof(ExportType));

        }


        private List<LogStatus> StatusList(List<LogStatus> statuses)
        {

            var lstSt = new List<LogStatus>();
            lstSt.Add(new LogStatus
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
            FrmConfig frmConfig = new FrmConfig(null, lstConfig, -1);
            frmConfig.ShowDialog();
            if (frmConfig.DialogResult == DialogResult.Cancel)
                return;

            if (frmConfig.ConfigViewModel != null && !String.IsNullOrEmpty(frmConfig.ConfigViewModel.Name.Trim()))
            {
                lstConfig.Add(Mapper.Map<Config>(frmConfig.ConfigViewModel));
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
                DialogResult dialogResult = MessageBox.Show("Do you want remove \"" + lstConfig[index].Name + "\"?", "Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
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

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lsvConfig.SelectedItems.Count > 0)
            {
                int index = lsvConfig.Items.IndexOf(lsvConfig.SelectedItems[0]);
                FrmConfig frmConfig = new FrmConfig(Mapper.Map<ConfigViewModel>(lstConfig[index]), lstConfig, index);
                frmConfig.ShowDialog();
                if (frmConfig.DialogResult == DialogResult.Cancel)
                    return;

                if (frmConfig.ConfigViewModel != null && !String.IsNullOrEmpty(frmConfig.ConfigViewModel.Name.Trim()))
                {
                    lstConfig[index] = Mapper.Map<Config>(frmConfig.ConfigViewModel);
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

            try
            {
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
                MessageBox.Show("Save to config file successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Oops", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (filesProcess == null || filesProcess.Length == 0)
            {
                MessageBox.Show("Opps..! There aren't any XAML file. Please click on Browser button to import your XAML files!");
                return;
            }

            if (lstConfig == null || lstConfig.Count() == 0)
            {
                MessageBox.Show("Please go to [Config] tab to setting your Keywords first!");
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

                FrmLoad frmLoad = new FrmLoad(folderDlg.SelectedPath.Trim(), filesProcess, lstConfig, chkDetail.Checked, (ExportType)cmbExport.SelectedItem);
                frmLoad.ShowDialog();

                // update list here:
                lsvStatus.Items.Clear();
                logStatuses = frmLoad.LogStatuses;
                logStatuses.ForEach(_ =>
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

            filesProcess = null;
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

                var lstStAppend = new List<LogStatus>();

                if (filesProcess.Count() == 0)
                    lstStAppend.Add(new LogStatus
                    {
                        CurrentTime = DateTime.Now,
                        Message = "Wrong! There aren't any file XAML in this folder :(",
                        MsgStatus = MessageStatus.Warning
                    });
                else
                {
                    lstStAppend.Add(new LogStatus
                    {
                        CurrentTime = DateTime.Now,
                        Message = "List of XAML files loaded successfully!",
                        MsgStatus = MessageStatus.None
                    });

                    foreach (var item in filesProcess)
                    {
                        lstStAppend.Add(new LogStatus
                        {
                            CurrentTime = DateTime.Now,
                            Message = item + " -> Ready",
                            MsgStatus = MessageStatus.OK
                        });
                    }

                    lstStAppend.Add(new LogStatus
                    {
                        CurrentTime = DateTime.Now,
                        Message = "Click Start button to Make it Funny!",
                        MsgStatus = MessageStatus.None
                    });
                }


                logStatuses = StatusList(lstStAppend);

                logStatuses.ForEach(_ =>
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

        private void btnLog_Click(object sender, EventArgs e)
        {
            FrmLog frmLog = new FrmLog(logStatuses);
            frmLog.ShowDialog();
        }
    }
}
