using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using UiPathIsFunny.Business;
using UiPathIsFunny.Model;
using UiPathIsFunny.Utility;
using UiPathIsFunny.View.Model;
using static UiPathIsFunny.Utility.Enums;

namespace UiPathIsFunny.View
{
    public partial class FrmLoad : Form
    {
        private string exportPath;
        private string[] files;
        private bool detailMode;
        private List<Config> configs;
        private ExportType exportType;

        public List<LogStatus> LogStatuses;


        public List<ActivityReport> activityReports;

        public FrmLoad(string exportPath, string[] files, List<Config> configs, bool detailMode, ExportType exportType)
        {
            InitializeComponent();
            this.exportPath = exportPath;
            this.files = files;
            this.configs = configs;
            this.detailMode = detailMode;
            this.exportType = exportType;
            LogStatuses = new List<LogStatus>();
            activityReports = new List<ActivityReport>();
            LogStatuses.Add(new LogStatus
            {
                CurrentTime = DateTime.Now,
                Message = "XAML files processed.",
                MsgStatus = MessageStatus.None
            });
        }

        private List<Activity> CreateListActivity(List<Config> configs)
        {
            var acti = new List<Activity>();
            configs.ForEach(_ =>
            {
                acti.Add(new Activity
                {
                    Count = 0,
                    Keyword = _.Keyword,
                    Name = _.Name,
                    Problem = ""
                });
            });
            return acti;
        }

        private void FrmLoad_Load(object sender, EventArgs e)
        {
            this.Activated += AfterLoading;
        }

        private void AfterLoading(object sender, EventArgs e)
        {
            this.Activated -= AfterLoading;
            XAMLFileBusiness aMLFileBusiness = new XAMLFileBusiness();

            bool isError = false;

            // Count by keyword:
            for (int i = 0; i < files.Count(); i++)
            {
                try
                {
                    var lstCount = aMLFileBusiness.CountActicities(files[i], CreateListActivity(configs));
                    activityReports.Add(new ActivityReport
                    {
                        FileName = files[i],
                        Activities = lstCount.ToList()
                    });
                    LogStatuses.Add(new LogStatus
                    {
                        CurrentTime = DateTime.Now,
                        Message = files[i] + " -> Success.",
                        MsgStatus = MessageStatus.OK
                    });
                }
                catch (Exception ex)
                {
                    LogStatuses.Add(new LogStatus
                    {
                        CurrentTime = DateTime.Now,
                        Message = files[i] + " -> Error: " + Environment.NewLine + ex.Message,
                        MsgStatus = MessageStatus.Fail
                    });
                    isError = true;
                }

            }

            if (!isError)
            {
                try
                {
                    var listSummary = aMLFileBusiness.CountSummatyActicities(activityReports);
                    switch (exportType)
                    {
                        case ExportType.JSON:
                            if (detailMode)
                                Export.ToJSON(activityReports, exportPath);
                            Export.ToJSONSummary(listSummary.ToList(), exportPath);
                            break;
                        case ExportType.CSV:
                            if (detailMode)
                                Export.ToCSV(activityReports, exportPath);
                            Export.ToCSVSummary(listSummary.ToList(), exportPath);
                            break;
                        case ExportType.EXCEL:
                            if (detailMode)
                                Export.ToExcel(activityReports, exportPath);
                            Export.ToExcelSummary(listSummary.ToList(), exportPath);
                            break;
                        default:
                            break;
                    }

                    LogStatuses.Add(new LogStatus
                    {
                        CurrentTime = DateTime.Now,
                        Message = "Export successfully! -> " + exportPath,
                        MsgStatus = MessageStatus.OK
                    });
                    //isError = true;
                }
                catch (Exception ex)
                {
                    LogStatuses.Add(new LogStatus
                    {
                        CurrentTime = DateTime.Now,
                        Message = "Export faild: " + ex.Message,
                        MsgStatus = MessageStatus.Fail
                    });
                }
            }
            else
            {
                MessageBox.Show("Error occurred when process XAML file. See the error in log.");
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
