using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

        public List<LogStatus> LogStatuses { get; set; }
        public List<ActivityReport> ActivityReports { get; set; }

        public FrmLoad(string exportPath, string[] files, List<Config> configs, bool detailMode, ExportType exportType)
        {
            InitializeComponent();

            this.exportPath = exportPath;
            this.files = files;
            this.configs = configs;
            this.detailMode = detailMode;
            this.exportType = exportType;

            LogStatuses = new List<LogStatus>();
            ActivityReports = new List<ActivityReport>();
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Task.Factory.StartNew
            (
             () =>
             {
                 Thread.Sleep(2000);
                 Invoke(new Action(AfterLoading));
             }
            );
        }

        private void AfterLoading()
        {

            var aMLFileBusiness = new XAMLFileBusiness();
            bool isError = false;
            LogStatuses.Add(new LogStatus
            {
                CurrentTime = DateTime.Now,
                Message = "Export START!",
                MsgStatus = MessageStatus.None
            });

            // Count by keyword:
            for (int i = 0; i < files.Count(); i++)
            {
                try
                {
                    var lstCount = aMLFileBusiness.CountActicities(files[i], CreateListActivity(configs));
                    ActivityReports.Add(new ActivityReport
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
                        Message = files[i] + " -> Error: " + ex.Message,
                        MsgStatus = MessageStatus.Fail
                    });
                    isError = true;
                }

            }

            // Export to file:
            try
            {
                if (!isError)
                {
                    var listSummary = aMLFileBusiness.CountSummatyActicities(ActivityReports);
                    switch (exportType)
                    {
                        case ExportType.JSON:
                            if (detailMode)
                                Export.ToJSON(ActivityReports, exportPath);
                            Export.ToJSONSummary(listSummary.ToList(), exportPath);
                            break;
                        case ExportType.CSV:
                            if (detailMode)
                                Export.ToCSV(ActivityReports, exportPath);
                            Export.ToCSVSummary(listSummary.ToList(), exportPath);
                            break;
                        case ExportType.EXCEL:
                            if (detailMode)
                                Export.ToExcel(ActivityReports, exportPath);
                            Export.ToExcelSummary(listSummary.ToList(), exportPath);
                            break;
                        default:
                            break;
                    }

                    LogStatuses.Add(new LogStatus
                    {
                        CurrentTime = DateTime.Now,
                        Message = "Export successfully! -> " + exportPath,
                        MsgStatus = MessageStatus.None
                    });
                }
                else
                    throw new Exception("Can't export to file! Error occurred when process XAML file.");
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
            finally
            {
                LogStatuses.Add(new LogStatus
                {
                    CurrentTime = DateTime.Now,
                    Message = "Export END!",
                    MsgStatus = MessageStatus.None
                });
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // Methods:
        private List<Activity> CreateListActivity(List<Config> configs)
        {
            var activity = new List<Activity>();
            configs.ForEach(_ =>
            {
                activity.Add(new Activity
                {
                    Count = 0,
                    Keyword = _.Keyword,
                    Name = _.Name,
                    Problem = ""
                });
            });

            return activity;
        }

    }
}
