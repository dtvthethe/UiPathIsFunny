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
            logStatuses.ForEach(_ => { lsvStatus.Items.Add(ItemViewFormat(_)); });
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

        private void lsvStatus_Resize(object sender, EventArgs e)
        {
            int width = lsvStatus.Width - 10;
            lsvStatus.Columns[0].Width = width;
        }
    }
}
