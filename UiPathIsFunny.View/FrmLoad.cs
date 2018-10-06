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

        public List<ActivityReport> activityReports;

        public FrmLoad(string exportPath, string[] files, List<Config> configs, bool detailMode)
        {
            InitializeComponent();
            this.exportPath = exportPath;
            this.files = files;
            this.configs = configs;
            this.detailMode = detailMode;
            activityReports = new List<ActivityReport>();
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


            // Count by keyword:
            for (int i = 0; i < files.Count(); i++)
            {

                XAMLFileBusiness aMLFileBusiness = new XAMLFileBusiness();
                var lstCount = aMLFileBusiness.CountActicities(files[i], CreateListActivity(configs));
                activityReports.Add(new ActivityReport
                {
                    FileName = files[i],
                    Activities = lstCount.ToList()
                });

            }
            try
            {
                if (detailMode)
                {
                    Export.ToExcel(activityReports, exportPath);
                }

                MessageBox.Show("Done");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
