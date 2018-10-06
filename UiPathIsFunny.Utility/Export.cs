using ClosedXML.Excel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UiPathIsFunny.Model;

namespace UiPathIsFunny.Utility
{
    public static class Export
    {

        public static void ToJSON(List<ActivityReport> activityReports, string folderPath)
        {
            string json = JsonConvert.SerializeObject(activityReports);

            string path = Path.Combine(folderPath, "Export_To_JSON_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".json");

            using (var fw = new StreamWriter(path))
            {
                fw.WriteLine(json);
            }
        }

        public static void ToJSONSummary(List<Activity> activities, string folderPath)
        {
            string json = JsonConvert.SerializeObject(activities);

            string path = Path.Combine(folderPath, "Export_To_JSON_SUMMARY_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".json");

            using (var fw = new StreamWriter(path))
            {
                fw.WriteLine(json);
            }
        }

        public static void ToCSV(List<ActivityReport> activityReports, string folderPath)
        {
            string path = Path.Combine(folderPath, "Export_To_CSV_" + DateTime.Now.ToString("yyyyMMdd_HHmmss"));

            Directory.CreateDirectory(path);

            activityReports.ForEach(_ =>
            {
                string fileName = Path.Combine(path, Path.GetFileNameWithoutExtension(_.FileName) + ".csv");
                using (var fw = new StreamWriter(fileName))
                {
                    // Create file csv
                    Type typeOfMyObject = activityReports[0].Activities[0].GetType();
                    PropertyInfo[] properties = typeOfMyObject.GetProperties();
                    string header = "";
                    foreach (PropertyInfo pi in properties)
                    {
                        header += pi.Name.ToUpper() + ",";
                    }

                    // Write header
                    fw.WriteLine(header.Remove(header.Length - 1, 1));

                    // Write content
                    _.Activities.ForEach(rec =>
                    {
                        string content = "";

                        foreach (PropertyInfo pi in properties)
                        {
                            string itemValue = rec.GetType().GetProperty(pi.Name).GetValue(rec, null).ToString();
                            content += itemValue + ",";
                        }

                        fw.WriteLine(content.Remove(content.Length - 1, 1));
                    });
                }
            });
        }

        public static void ToCSVSummary(List<Activity> activities, string folderPath)
        {
            string fileName = Path.Combine(folderPath, "Export_To_CSV_SUMMARY_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv");

            using (var fw = new StreamWriter(fileName))
            {
                // Create file csv
                Type typeOfMyObject = activities[0].GetType();
                PropertyInfo[] properties = typeOfMyObject.GetProperties();
                string header = "";
                foreach (PropertyInfo pi in properties)
                {
                    header += pi.Name.ToUpper() + ",";
                }

                // Write header
                fw.WriteLine(header.Remove(header.Length - 1, 1));

                // Write content
                activities.ForEach(rec =>
                {
                    string content = "";

                    foreach (PropertyInfo pi in properties)
                    {
                        string itemValue = rec.GetType().GetProperty(pi.Name).GetValue(rec, null).ToString();
                        content += itemValue + ",";
                    }

                    fw.WriteLine(content.Remove(content.Length - 1, 1));
                });
            }

        }

        public static void ToExcel(List<ActivityReport> activityReports, string folderPath)
        {
            string pathFile = Path.Combine(folderPath, "Export_To_EXCEL_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");

            var wb = new XLWorkbook();

            activityReports.ForEach(_ =>
            {
                // Sheet name:
                var ws = wb.Worksheets.Add(WorkSheetName(Path.GetFileNameWithoutExtension(_.FileName)));
                // Title:
                var title = ws.Cell(1, 1);
                title.Value = "UiPath Count Activity";
                title.Style.Font.Bold = true;
                title.Style.Font.FontColor = XLColor.White;
                title.Style.Fill.BackgroundColor = XLColor.FromArgb(37, 115, 177);

                ws.Range(1, 1, 1, 4).Merge().AddToNamed("Title");
                // Columns:
                Type typeOfMyObject = activityReports[0].Activities[0].GetType();
                PropertyInfo[] properties = typeOfMyObject.GetProperties();

                for (int i = 0; i < properties.Length; i++)
                {
                    var column = ws.Cell(3, i + 1);
                    column.Value = properties[i].Name.ToUpper();
                    column.Style.Font.Bold = true;
                    column.Style.Font.FontColor = XLColor.Black;
                    column.Style.Fill.BackgroundColor = XLColor.Orange;
                    column.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    column.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    column.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    column.Style.Border.BottomBorder = XLBorderStyleValues.Thin;

                    column.Style.Border.RightBorderColor = XLColor.Black;
                    column.Style.Border.LeftBorderColor = XLColor.Black;
                    column.Style.Border.TopBorderColor = XLColor.Black;
                    column.Style.Border.BottomBorderColor = XLColor.Black;
                }

                // Rows:
                var row = ws.Cell(4, 1);
                row.InsertData(_.Activities);

                var cl = ws.Range(4, 1, 3 + _.Activities.Count, 4);
                cl.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                cl.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                cl.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                cl.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                cl.Style.Border.RightBorderColor = XLColor.Black;
                cl.Style.Border.LeftBorderColor = XLColor.Black;
                cl.Style.Border.TopBorderColor = XLColor.Black;
                cl.Style.Border.BottomBorderColor = XLColor.Black;
            });
            wb.SaveAs(pathFile);
            wb.Dispose();
        }

        public static void ToExcelSummary(List<Activity> activities, string folderPath)
        {
            string pathFile = Path.Combine(folderPath, "Export_To_EXCEL_SUMMARY_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");

            var wb = new XLWorkbook();

            // Sheet name:
            var ws = wb.Worksheets.Add("Summary");
            // Title:
            var title = ws.Cell(1, 1);
            title.Value = "UiPath Count Activity";
            title.Style.Font.Bold = true;
            title.Style.Font.FontColor = XLColor.White;
            title.Style.Fill.BackgroundColor = XLColor.FromArgb(37, 115, 177);

            ws.Range(1, 1, 1, 4).Merge().AddToNamed("Title");
            // Columns:
            Type typeOfMyObject = activities[0].GetType();
            PropertyInfo[] properties = typeOfMyObject.GetProperties();

            for (int i = 0; i < properties.Length; i++)
            {
                var column = ws.Cell(3, i + 1);
                column.Value = properties[i].Name.ToUpper();
                column.Style.Font.Bold = true;
                column.Style.Font.FontColor = XLColor.Black;
                column.Style.Fill.BackgroundColor = XLColor.Orange;
                column.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                column.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                column.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                column.Style.Border.BottomBorder = XLBorderStyleValues.Thin;

                column.Style.Border.RightBorderColor = XLColor.Black;
                column.Style.Border.LeftBorderColor = XLColor.Black;
                column.Style.Border.TopBorderColor = XLColor.Black;
                column.Style.Border.BottomBorderColor = XLColor.Black;
            }

            // Rows:
            var row = ws.Cell(4, 1);
            row.InsertData(activities);

            var cl = ws.Range(4, 1, 3 + activities.Count, 4);
            cl.Style.Border.RightBorder = XLBorderStyleValues.Thin;
            cl.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            cl.Style.Border.TopBorder = XLBorderStyleValues.Thin;
            cl.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            cl.Style.Border.RightBorderColor = XLColor.Black;
            cl.Style.Border.LeftBorderColor = XLColor.Black;
            cl.Style.Border.TopBorderColor = XLColor.Black;
            cl.Style.Border.BottomBorderColor = XLColor.Black;

            wb.SaveAs(pathFile);
            wb.Dispose();
        }

        private static string WorkSheetName(string Name)
        {
            if (Name.Length < 30)
                return Name;
            else
                return Name.Substring(0, 25) + "...";
        }

    }
}
