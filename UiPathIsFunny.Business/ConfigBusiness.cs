using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UiPathIsFunny.Model;

namespace UiPathIsFunny.Business
{
    public class ConfigBusiness
    {

        public List<Config> Import(string pathFile)
        {
            var lstConfig = new List<Config>();
            bool isValid = false;
            using (var fr = new StreamReader(pathFile))
            {
                string line = "";
                while ((line = fr.ReadLine()) != null)
                {
                    string[] arrLine = line.Split(',');
                    if (arrLine.Count() == 2)
                    {
                        lstConfig.Add(new Config
                        {
                            Name = arrLine[0],
                            Keyword = arrLine[1]
                        });
                    }
                    else
                        isValid = true;
                }

                if (isValid)
                {
                    lstConfig = null;
                    throw new Exception("The config file is invalid. Please check your config file again :D");
                }
                else
                    return lstConfig;
            }

        }

        public void SaveAll(List<Config> configs, string pathFile)
        {
            using (var fw = new StreamWriter(pathFile))
            {
                configs.ForEach(_ =>
                {
                    fw.WriteLine(_.Name.Trim() + "," + _.Keyword);
                });
            };
        }

    }
}
