using System.Collections.Generic;
using System.IO;
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
                    if (string.IsNullOrEmpty(_.KeyWord.Trim()) || _.KeyWord.Trim().Length <= 2)
                    {
                        _.Problem = "OOps..! This Keyword is invalid, Please fix to correct!";
                        _.Count = 0;
                    }
                    else
                    {
                        _.Problem = "";
                        _.Count = Regex.Matches(text, _.KeyWord).Count;
                    }
                });
            }
            return lstActivities;
        }

    }
}
