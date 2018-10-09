using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UiPathIsFunny.Model;

namespace UiPathIsFunny.Business
{
    public class XAMLFileBusiness
    {

        public IList<Activity> CountActicities(string xamlFilePath, List<Activity> lstActivities)
        {
            using (var fr = new StreamReader(xamlFilePath))
            {
                var text = fr.ReadToEnd();
                lstActivities.ForEach(_ =>
                {
                    if (string.IsNullOrEmpty(_.Keyword.Trim()) || _.Keyword.Trim().Length <= 2)
                    {
                        _.Problem = "OOps..! This Keyword is invalid, Please fix to correct!";
                        _.Count = 0;
                    }
                    else
                    {
                        _.Problem = "";
                        _.Count = Regex.Matches(text, _.Keyword).Count;
                    }
                });
            }
            return lstActivities;
        }

        public List<Activity> CountSummatyActicities(List<ActivityReport> activities)
        {
            var lstActivity = new List<Activity>();

            if (activities.Count == 1)
            {
                lstActivity = activities[0].Activities;
            }
            else if (activities.Count > 1)
            {
                lstActivity = activities[0].Activities;
                for (int i = 1; i < activities.Count; i++)
                {
                    lstActivity = lstActivity.Join(activities[i].Activities,
                        inner => new { Name = inner.Name, Key = inner.Keyword },
                        outer => new { Name = outer.Name, Key = outer.Keyword }
                , (inner, outer) =>
                      new Activity
                      {
                          Keyword = inner.Keyword,
                          Count = inner.Count + outer.Count,
                          Problem = inner.Problem,
                          Name = inner.Name
                      }).ToList();
                }

            }
            else
            {
                return lstActivity;
            }

            return lstActivity;
        }

    }
}
