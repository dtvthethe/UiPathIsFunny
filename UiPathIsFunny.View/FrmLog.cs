using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using UiPathIsFunny.View.Model;
using static UiPathIsFunny.Utility.Enums;

namespace UiPathIsFunny.View
{
    public partial class FrmLog : Form
    {
        private List<LogStatus> logStatuses;

        public FrmLog(List<LogStatus> logStatuses)
        {
            InitializeComponent();
            this.logStatuses = logStatuses;
            lsvStatus.Columns.Add("", lsvStatus.Width - 10);
        }

        private void FrmLog_Load(object sender, EventArgs e)
        {
            lsvStatus.Items.Clear();
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
}
