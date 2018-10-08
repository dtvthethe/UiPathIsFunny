using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UiPathIsFunny.Model;
using UiPathIsFunny.Utility;

namespace UiPathIsFunny.Business
{
    public class ConfigBusiness
    {

        public List<Config> Import(string pathFile)
        {
            var lstConfig = new List<Config>();

            using (var fr = new StreamReader(pathFile))
            {
                string line = "";
                int index = 0;
                try
                {
                    while ((line = fr.ReadLine()) != null)
                    {
                        index++;
                        string[] arrLine = line.Split(',');
                        if (arrLine.Count() == 2)
                        {
                            var nameValidate = ConfigValidate.NameValidate(arrLine[0], -1, lstConfig);
                            var keywordValidate = ConfigValidate.KeywordValidate(arrLine[1], -1, lstConfig);

                            if (!nameValidate.IsError && !keywordValidate.IsError)
                            {
                                lstConfig.Add(new Config
                                {
                                    Name = arrLine[0],
                                    Keyword = arrLine[1]
                                });
                            }
                            else
                            {
                                if (!String.IsNullOrEmpty(nameValidate.Message))
                                    throw new Exception(nameValidate.Message + " Your config at line: " + index + " -> Invalid Name value =  \n\"" + arrLine[0] + "\"");
                                if (!String.IsNullOrEmpty(keywordValidate.Message))
                                    throw new Exception(keywordValidate.Message + " Your config at line: " + index + "-> Invalid Keyword value =  \n\"" + arrLine[1] + "\"");

                                break;
                            }
                        }
                        else
                            throw new Exception("This item wasn't correct format <Name>,<Keyword>");
                    }
                }
                catch (Exception ex)
                {
                    lstConfig = null;
                    throw new Exception("The config file is invalid. Please check your config file again :D" + Environment.NewLine + ex.Message);
                }

                return lstConfig;
            }

        }

        public List<Config> DefaultConfig()
        {
            var lst = new List<Config>();
            lst.AddRange(new List<Config> {
                new Config{
                    Name = "If",
                    Keyword = "<If "
                },
                new Config{
                    Name = "Sequence",
                    Keyword = "<Sequence "
                },
                new Config{
                    Name = "Flowchart",
                    Keyword = "<Flowchart "
                },
                new Config{
                    Name = "Assign",
                    Keyword = "<Assign "
                },
                new Config{
                    Name = "WriteLine",
                    Keyword = "<WriteLine "
                },
                new Config{
                    Name = "DatabaseConnect",
                    Keyword = "<ui:DatabaseConnect  "
                },
                new Config{
                    Name = "DatabaseDisconnect",
                    Keyword = "<ui:DatabaseDisconnect "
                },
                new Config{
                    Name = "ExecuteNonQuery",
                    Keyword = "<ui:ExecuteNonQuery "
                },
                new Config{
                    Name = "ExecuteQuery",
                    Keyword = "<ui:ExecuteQuery "
                },
                new Config{
                    Name = "InsertDataTable",
                    Keyword = "<ui:InsertDataTable "
                },
                new Config{
                    Name = "DatabaseTransaction",
                    Keyword = "<ui:DatabaseTransaction  "
                },
                new Config{
                    Name = "AppendCsvFile",
                    Keyword = "<ui:AppendCsvFile "
                },
                new Config{
                    Name = "ReadCsvFile",
                    Keyword = "<ui:ReadCsvFile "
                },
                new Config{
                    Name = "WriteCsvFile",
                    Keyword = "<ui:WriteCsvFile "
                },
                new Config{
                    Name = "ExcelAppendRange",
                    Keyword = "<ui:ExcelAppendRange "
                },
                new Config{
                    Name = "ExcelCloseWorkbook",
                    Keyword = "<ui:ExcelCloseWorkbook "
                },
                new Config{
                    Name = "ExcelApplicationScope",
                    Keyword = "<ui:ExcelApplicationScope "
                },
                new Config{
                    Name = "ExecuteMacro",
                    Keyword = " <ui:ExecuteMacro "
                },
                new Config{
                    Name = "ExcelGetCellColor",
                    Keyword = "<ui:ExcelGetCellColor "
                },
                new Config{
                    Name = "InvokeVBA",
                    Keyword = "<ui:InvokeVBA "
                },
                new Config{
                    Name = "ExcelReadCell",
                    Keyword = "<ui:ExcelReadCell "
                },
                new Config{
                    Name = "ExcelReadCellFormula",
                    Keyword = "<ui:ExcelReadCellFormula "
                },
                new Config{
                    Name = "ExcelReadColumn",
                    Keyword = "<ui:ExcelReadColumn "
                },
                new Config{
                    Name = "ExcelReadRow",
                    Keyword = "<ui:ExcelReadRow "
                },
                new Config{
                    Name = "ExcelSaveWorkbook",
                    Keyword = "<ui:ExcelSaveWorkbook "
                },
                new Config{
                    Name = "ExcelSelectRange",
                    Keyword = "<ui:ExcelSelectRange "
                },
                new Config{
                    Name = "ExcelSetRangeColor",
                    Keyword = "<ui:ExcelSetRangeColor "
                },
                new Config{
                    Name = "ExcelWriteCell",
                    Keyword = "<ui:ExcelWriteCell "
                },
                new Config{
                    Name = "ExcelWriteRange",
                    Keyword = "<ui:ExcelWriteRange "
                },
                new Config{
                    Name = "TerminalGetFieldAtPosition",
                    Keyword = "<uit:TerminalGetFieldAtPosition "
                },
                new Config{
                    Name = "TerminalGetScreenArea",
                    Keyword = "<uit:TerminalGetScreenArea "
                },
                new Config{
                    Name = "TerminalGetTextAtPosition",
                    Keyword = "<uit:TerminalGetTextAtPosition "
                },
                new Config{
                    Name = "TerminalMoveCursor",
                    Keyword = "<uit:TerminalMoveCursor "
                },
                new Config{
                    Name = "TerminalSendKeys",
                    Keyword = "<uit:TerminalSendKeys "
                },
                new Config{
                    Name = "TerminalSendKeysSecure",
                    Keyword = "<uit:TerminalSendKeysSecure "
                },
                new Config{
                    Name = "TerminalSetFieldAtPosition",
                    Keyword = "<uit:TerminalSetFieldAtPosition "
                },
                new Config{
                    Name = "TerminalWaitTextAtPosition",
                    Keyword = "<uit:TerminalWaitTextAtPosition "
                },
                new Config{
                    Name = "TerminalGetField",
                    Keyword = "<uit:TerminalGetField "
                },
                new Config{
                    Name = "TerminalGetText",
                    Keyword = "<ui:TerminalGetText "
                },
                new Config{
                    Name = "TerminalSendControlKey",
                    Keyword = "<uit:TerminalSendControlKey "
                },
                new Config{
                    Name = "TerminalSetField",
                    Keyword = "<uit:TerminalSetField "
                },
                new Config{
                    Name = "TerminalSession",
                    Keyword = "<uit:TerminalSession "
                },
                new Config{
                    Name = "TerminalWaitFieldText",
                    Keyword = "<uit:TerminalWaitFieldText "
                },
                new Config{
                    Name = "TerminalWaitScreenText",
                    Keyword = "<uit:TerminalWaitScreenText "
                },
                new Config{
                    Name = "HttpClient",
                    Keyword = "<ui:HttpClient "
                },
                new Config{
                    Name = "SoapClient",
                    Keyword = "<ui:SoapClient "
                },
                new Config{
                    Name = "WordAddImage",
                    Keyword = "<p:WordAddImage "
                },
                new Config{
                    Name = "WordAppendText",
                    Keyword = "<p:WordAppendText "
                },
                new Config{
                    Name = "WordExportToPdf",
                    Keyword = "<p:WordExportToPdf "
                },
                new Config{
                    Name = "WordReadText",
                    Keyword = "<p:WordReadText "
                },
                new Config{
                    Name = "WordReplaceText",
                    Keyword = "<p:WordReplaceText "
                },
                new Config{
                    Name = "WordSetBookmarkContent",
                    Keyword = "<p:WordSetBookmarkContent "
                },
                new Config{
                    Name = "WordApplicationScope",
                    Keyword = "<p:WordApplicationScope "
                },
                new Config{
                    Name = "Callout",
                    Keyword = "<ui:Callout "
                },
                new Config{
                    Name = "InputDialog",
                    Keyword = "<ui:InputDialog "
                },
                new Config{
                    Name = "MessageBox",
                    Keyword = "<ui:MessageBox "
                },
                new Config{
                    Name = "SelectFile",
                    Keyword = "<ui:SelectFile "
                },
                new Config{
                    Name = "SelectFolder",
                    Keyword = "<ui:SelectFolder "
                },
                new Config{
                    Name = "CloseApplication",
                    Keyword = "<ui:CloseApplication "
                },
                new Config{
                    Name = "GetProcesses",
                    Keyword = "<ui:GetProcesses "
                },
                new Config{
                    Name = "KillProcess",
                    Keyword = "<ui:KillProcess "
                },
                new Config{
                    Name = "OpenApplication",
                    Keyword = "<ui:OpenApplication "
                },
                new Config{
                    Name = "StartProcess",
                    Keyword = "<ui:StartProcess "
                },
                new Config{
                    Name = "aaaaaaaaaa",
                    Keyword = "<ui:aaaaaaaaaa "
                },
                new Config{
                    Name = "aaaaaaaaaa",
                    Keyword = "<ui:aaaaaaaaaa "
                },
                new Config{
                    Name = "aaaaaaaaaa",
                    Keyword = "<ui:aaaaaaaaaa "
                },
                new Config{
                    Name = "aaaaaaaaaa",
                    Keyword = "<ui:aaaaaaaaaa "
                },
                new Config{
                    Name = "aaaaaaaaaa",
                    Keyword = "<ui:aaaaaaaaaa "
                },
                new Config{
                    Name = "aaaaaaaaaa",
                    Keyword = "<ui:aaaaaaaaaa "
                },
                new Config{
                    Name = "aaaaaaaaaa",
                    Keyword = "<ui:aaaaaaaaaa "
                },
                new Config{
                    Name = "aaaaaaaaaa",
                    Keyword = "<ui:aaaaaaaaaa "
                },
                new Config{
                    Name = "aaaaaaaaaa",
                    Keyword = "<ui:aaaaaaaaaa "
                },
                new Config{
                    Name = "aaaaaaaaaa",
                    Keyword = "<ui:aaaaaaaaaa "
                },
                new Config{
                    Name = "aaaaaaaaaa",
                    Keyword = "<ui:aaaaaaaaaa "
                },
                new Config{
                    Name = "aaaaaaaaaa",
                    Keyword = "<ui:aaaaaaaaaa "
                },
                new Config{
                    Name = "aaaaaaaaaa",
                    Keyword = "<ui:aaaaaaaaaa "
                },
                new Config{
                    Name = "aaaaaaaaaa",
                    Keyword = "<ui:aaaaaaaaaa "
                },
                new Config{
                    Name = "aaaaaaaaaa",
                    Keyword = "<ui:aaaaaaaaaa "
                },
                new Config{
                    Name = "aaaaaaaaaa",
                    Keyword = "<ui:aaaaaaaaaa "
                },
                new Config{
                    Name = "aaaaaaaaaa",
                    Keyword = "<ui:aaaaaaaaaa "
                },
                new Config{
                    Name = "aaaaaaaaaa",
                    Keyword = "<ui:aaaaaaaaaa "
                },
                new Config{
                    Name = "aaaaaaaaaa",
                    Keyword = "<ui:aaaaaaaaaa "
                },
                new Config{
                    Name = "aaaaaaaaaa",
                    Keyword = "<ui:aaaaaaaaaa "
                },
                new Config{
                    Name = "aaaaaaaaaa",
                    Keyword = "<ui:aaaaaaaaaa "
                },
                new Config{
                    Name = "aaaaaaaaaa",
                    Keyword = "<ui:aaaaaaaaaa "
                },
                new Config{
                    Name = "aaaaaaaaaa",
                    Keyword = "<ui:aaaaaaaaaa "
                },
                new Config{
                    Name = "aaaaaaaaaa",
                    Keyword = "<ui:aaaaaaaaaa "
                },
                new Config{
                    Name = "aaaaaaaaaa",
                    Keyword = "<ui:aaaaaaaaaa "
                },
                new Config{
                    Name = "aaaaaaaaaa",
                    Keyword = "<ui:aaaaaaaaaa "
                },
                new Config{
                    Name = "aaaaaaaaaa",
                    Keyword = "<ui:aaaaaaaaaa "
                }


            });

            return lst;
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
