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

namespace UiPathIsFunny.View
{
    public partial class FrmMain : Form
    {

        private List<Config> lstConfig;
        private ConfigBusiness configBusiness;

        //public static ConfigViewModel configView { get; set; }

        public FrmMain()
        {
            InitializeComponent();

            lstConfig = new List<Config>();
            configBusiness = new ConfigBusiness();

            lsvConfig.Columns.Add("Name", lsvConfig.Width / 2);
            lsvConfig.Columns.Add("Keyword", lsvConfig.Width / 2);

            lsvStatus.Columns.Add("", lsvStatus.Width);
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

        static System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();
        static int alarmCounter = 1;
        static bool exitFlag = false;

        private void btnStart_Click(object sender, EventArgs e)
        {
            myTimer.Tick += new EventHandler(TimerEventProcessor);

            // Sets the timer interval to 5 seconds.
            myTimer.Interval = 5000;
            myTimer.Start();

            // Runs the timer, and raises the event.
            while (exitFlag == false)
            {
                // Processes all the events in the queue.
                Application.DoEvents();
            }
        }

        private static void TimerEventProcessor(Object myObject,
                                            EventArgs myEventArgs)
        {
            myTimer.Stop();

            // Displays a message box asking whether to continue running the timer.
            if (MessageBox.Show("Continue running?", "Count is: " + alarmCounter,
               MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // Restarts the timer and increments the counter.
                alarmCounter += 1;
                myTimer.Enabled = true;
            }
            else
            {
                // Stops the timer.
                exitFlag = true;
            }
        }
    }
}
