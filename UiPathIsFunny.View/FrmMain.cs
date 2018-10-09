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

            lsvConfig.Columns.Add("Name", (lsvConfig.Width / 2) - 21);
            lsvConfig.Columns.Add("Keyword", (lsvConfig.Width / 2));
            lsvStatus.Columns.Add("", lsvStatus.Width - 10);

            Init();
        }

        private void Init()
        {
            var lstStAppend = new List<LogStatus>();

            logStatuses = ListStatus(CustomInitStatusList());
            logStatuses.ForEach(_ => { lsvStatus.Items.Add(ItemViewFormat(_)); });
            cmbExport.DataSource = Enum.GetValues(typeof(ExportType));

            lstConfig = configBusiness.DefaultConfig();
            ShowOnListConfig(lstConfig);

        }

        private void btnBrowConfig_Click(object sender, EventArgs e)
        {
            try
            {
                var openFileDialogConfigPath = new OpenFileDialog();
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
                    lstConfig = configBusiness.Import(openFileDialogConfigPath.FileName);
                    ShowOnListConfig(lstConfig);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Oops", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtConfigTag.Text = "";
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var frmConfig = new FrmConfig(null, lstConfig, -1);
            frmConfig.ShowDialog();
            if (frmConfig.DialogResult == DialogResult.OK)
                if (frmConfig.ConfigViewModel != null && !String.IsNullOrEmpty(frmConfig.ConfigViewModel.Name.Trim()))
                {
                    lstConfig.Add(Mapper.Map<Config>(frmConfig.ConfigViewModel));
                    ShowOnListConfig(lstConfig);
                }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lsvConfig.SelectedItems.Count > 0)
            {
                int index = lsvConfig.Items.IndexOf(lsvConfig.SelectedItems[0]);
                var dialogResult = MessageBox.Show("Do you want remove \"" + lstConfig[index].Name + "\"?", "Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    lstConfig.RemoveAt(index);
                    ShowOnListConfig(lstConfig);
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lsvConfig.SelectedItems.Count > 0)
            {
                int index = lsvConfig.Items.IndexOf(lsvConfig.SelectedItems[0]);
                var frmConfig = new FrmConfig(Mapper.Map<ConfigViewModel>(lstConfig[index]), lstConfig, index);
                frmConfig.ShowDialog();
                if (frmConfig.DialogResult == DialogResult.OK)
                    if (frmConfig.ConfigViewModel != null && !String.IsNullOrEmpty(frmConfig.ConfigViewModel.Name.Trim()))
                    {
                        lstConfig[index] = Mapper.Map<Config>(frmConfig.ConfigViewModel);
                        ShowOnListConfig(lstConfig);
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
                    var folderDlg = new FolderBrowserDialog();
                    folderDlg.ShowNewFolderButton = true;
                    // Show the FolderBrowserDialog.  
                    var result = folderDlg.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        string pathFile = Path.Combine(folderDlg.SelectedPath.Trim(), "UiPath_Is_Funny_Config_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".config");
                        txtConfigTag.Text = pathFile;
                    }
                }

                if (!String.IsNullOrEmpty(txtConfigTag.Text.Trim()))
                {
                    configBusiness.SaveAll(lstConfig, txtConfigTag.Text.Trim());
                    MessageBox.Show("Save to config file successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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
                MessageBox.Show("Opps..! There aren't any XAML file. Please click on Browser button to import your XAML files!", "Opps", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (lstConfig == null || lstConfig.Count() == 0)
            {
                MessageBox.Show("Please go to [Config] tab to setting your Keywords first!", "Opps", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            // Show the FolderBrowserDialog.  
            var result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (String.IsNullOrEmpty(folderDlg.SelectedPath.Trim()))
                    return;

                var frmLoad = new FrmLoad(folderDlg.SelectedPath.Trim(), filesProcess, lstConfig, chkDetail.Checked, (ExportType)cmbExport.SelectedItem);
                frmLoad.ShowDialog();

                // update list here:
                logStatuses = frmLoad.LogStatuses;
                lsvStatus.Items.Clear();

                logStatuses.ForEach(_ => { lsvStatus.Items.Add(ItemViewFormat(_)); });
                MessageBox.Show("Export successfully!\nYour export file path:\n" + folderDlg.SelectedPath.Trim(), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnFolderXAML_Click(object sender, EventArgs e)
        {
            var folderDlg = new FolderBrowserDialog();
            // Show the FolderBrowserDialog.  
            var result = folderDlg.ShowDialog();
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

                logStatuses = ListStatus(lstStAppend);

                logStatuses.ForEach(_ => { lsvStatus.Items.Add(ItemViewFormat(_)); });
            }
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            var frmLog = new FrmLog(logStatuses);
            frmLog.ShowDialog();
        }

        private void lsvConfig_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = lsvConfig.Columns[e.ColumnIndex].Width;
        }

        // Methods:
        private List<LogStatus> ListStatus(List<LogStatus> statuses)
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

        // Custom format:
        private ListViewItem ItemViewFormat(LogStatus logStatus)
        {
            var item = new ListViewItem(logStatus.CurrentTime.ToString() + " - " + logStatus.Message);
            switch (logStatus.MsgStatus)
            {
                case MessageStatus.OK:
                    item.ForeColor = Color.Green;
                    return item;
                case MessageStatus.Fail:
                    item.ForeColor = Color.Red;
                    return item;
                case MessageStatus.Warning:
                    item.ForeColor = Color.Orange;
                    return item;
                default:
                    return item;
            }
        }

        // Show data on Config List
        private void ShowOnListConfig(List<Config> configs)
        {
            lsvConfig.Items.Clear();
            configs.ForEach(_ =>
            {
                var item = new ListViewItem(_.Name);
                item.SubItems.Add(_.Keyword);
                lsvConfig.Items.Add(item);
            });
        }

        // Custom init status list:
        private List<LogStatus> CustomInitStatusList()
        {
            var lstStAppend = new List<LogStatus>();
            lstStAppend.Add(new LogStatus
            {
                CurrentTime = DateTime.Now,
                Message = "Please click on Browser button to import your XAML files!",
                MsgStatus = MessageStatus.Fail
            });
            return lstStAppend;
        }

        private void linkGithub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/dtvthethe/UiPathIsFunny");
        }

        private void linkLatestVersion_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/dtvthethe/UiPathIsFunny/releases");
        }
    }
}
