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
                    Name = "Click",
                    Keyword = "<ui:Click "
                },
                new Config{
                    Name = "Hover",
                    Keyword = "<ui:Hover "
                },
                new Config{
                    Name = "SendHotkey",
                    Keyword = "<ui:SendHotkey "
                },
                new Config{
                    Name = "TypeInto",
                    Keyword = "<ui:TypeInto "
                },
                new Config{
                    Name = "TypeSecureText",
                    Keyword = "<ui:TypeSecureText "
                },
                new Config{
                    Name = "Check",
                    Keyword = "<ui:Check "
                },
                new Config{
                    Name = "Activate",
                    Keyword = "<ui:Activate "
                },
                new Config{
                    Name = "GetValue",
                    Keyword = "<ui:GetValue "
                },
                new Config{
                    Name = "SelectItem",
                    Keyword = "<ui:SelectItem "
                },
                new Config{
                    Name = "SetFocus",
                    Keyword = "<ui:SetFocus "
                },
                new Config{
                    Name = "SetValue",
                    Keyword = "<ui:SetValue "
                },
                new Config{
                    Name = "UiElementExists",
                    Keyword = "<ui:UiElementExists "
                },
                new Config{
                    Name = "ElementScope",
                    Keyword = "<ui:ElementScope "
                },
                new Config{
                    Name = "FindChildren",
                    Keyword = "<ui:FindChildren "
                },
                new Config{
                    Name = "WaitUiElementAppear",
                    Keyword = "<ui:WaitUiElementAppear "
                },
                new Config{
                    Name = "FindRelative",
                    Keyword = "<ui:FindRelative "
                },
                new Config{
                    Name = "GetAttribute",
                    Keyword = "<ui:GetAttribute "
                },
                new Config{
                    Name = "GetPosition",
                    Keyword = "<ui:GetPosition "
                },
                new Config{
                    Name = "WaitAttribute",
                    Keyword = "<ui:WaitAttribute "
                },
                new Config{
                    Name = "OnUiElementAppear",
                    Keyword = "<ui:OnUiElementAppear "
                },
                new Config{
                    Name = "OnUiElementVanish",
                    Keyword = "<ui:OnUiElementVanish "
                },
                new Config{
                    Name = "GetOCRText",
                    Keyword = "<ui:GetOCRText "
                },
                new Config{
                    Name = "FindOCRText",
                    Keyword = "<ui:FindOCRText "
                },
                new Config{
                    Name = "ClickImage",
                    Keyword = "<ui:ClickImage "
                },
                new Config{
                    Name = "HoverImage",
                    Keyword = "<ui:HoverImage "
                },
                new Config{
                    Name = "WaitImageAppear",
                    Keyword = "<ui:WaitImageAppear "
                },
                new Config{
                    Name = "FindImageMatches",
                    Keyword = "<ui:FindImageMatches "
                },
                new Config{
                    Name = "ImageFound",
                    Keyword = "<ui:ImageFound "
                },
                new Config{
                    Name = "WaitImageVanish",
                    Keyword = "<ui:WaitImageVanish "
                },
                new Config{
                    Name = "OnImageAppear",
                    Keyword = "<ui:OnImageAppear "
                },
                new Config{
                    Name = "OnImageVanish",
                    Keyword = "<ui:OnImageVanish "
                },
                new Config{
                    Name = "LoadImage",
                    Keyword = "<ui:LoadImage "
                },
                new Config{
                    Name = "SaveImage",
                    Keyword = "<ui:SaveImage "
                },
                new Config{
                    Name = "BrowserScope",
                    Keyword = "<ui:BrowserScope "
                },
                new Config{
                    Name = "CloseTab",
                    Keyword = "<ui:CloseTab "
                },
                new Config{
                    Name = "GoForward",
                    Keyword = "<ui:GoForward "
                },
                new Config{
                    Name = "GoHome",
                    Keyword = "<ui:GoHome "
                },
                new Config{
                    Name = "InjectJsScript",
                    Keyword = "<ui:InjectJsScript "
                },
                new Config{
                    Name = "NavigateTo",
                    Keyword = "<ui:NavigateTo "
                },
                new Config{
                    Name = "RefreshBrowser",
                    Keyword = "<ui:RefreshBrowser "
                },
                new Config{
                    Name = "SetWebAttribute",
                    Keyword = "<ui:SetWebAttribute "
                },
                new Config{
                    Name = "WindowScope",
                    Keyword = "<ui:WindowScope "
                },
                new Config{
                    Name = "CloseWindow",
                    Keyword = "<ui:CloseWindow "
                },
                new Config{
                    Name = "GetActiveWindow",
                    Keyword = "<ui:GetActiveWindow "
                },
                new Config{
                    Name = "HideWindow",
                    Keyword = "<ui:HideWindow "
                },
                new Config{
                    Name = "MaximizeWindow",
                    Keyword = "<ui:MaximizeWindow "
                },
                new Config{
                    Name = "MinimizeWindow",
                    Keyword = "<ui:MinimizeWindow "
                },
                new Config{
                    Name = "MoveWindow",
                    Keyword = "<ui:MoveWindow "
                },
                new Config{
                    Name = "RestoreWindow",
                    Keyword = "<ui:RestoreWindow "
                },
                new Config{
                    Name = "ShowWindow",
                    Keyword = "<ui:ShowWindow "
                },
                new Config{
                    Name = "AddQueueItem",
                    Keyword = "<ui:AddQueueItem "
                },
                new Config{
                    Name = "AddTransactionItem",
                    Keyword = "<ui:AddTransactionItem "
                },
                new Config{
                    Name = "GetQueueItem",
                    Keyword = "<ui:GetQueueItem "
                },
                new Config{
                    Name = "PostponeTransactionItem",
                    Keyword = "<ui:PostponeTransactionItem "
                },
                new Config{
                    Name = "SetTransactionProgress",
                    Keyword = "<ui:SetTransactionProgress "
                },
                new Config{
                    Name = "SetTransactionStatus",
                    Keyword = "<ui:SetTransactionStatus "
                },
                new Config{
                    Name = "GetRobotAsset",
                    Keyword = "<ui:GetRobotAsset "
                },
                new Config{
                    Name = "GetRobotCredential",
                    Keyword = "<ui:GetRobotCredential "
                },
                new Config{
                    Name = "ShouldStop",
                    Keyword = "<ui:ShouldStop "
                },
                new Config{
                    Name = "MergePDFs",
                    Keyword = "<p:MergePDFs "
                },
                new Config{
                    Name = "SplitPDF",
                    Keyword = "<p:SplitPDF "
                },
                new Config{
                    Name = "GetPythonObject",
                    Keyword = "<p:GetObject "
                },
                new Config{
                    Name = "InvokePythonMethod",
                    Keyword = "<p:InvokeMethod "
                },
                new Config{
                    Name = "LoadPythonScript",
                    Keyword = "<p:LoadScript "
                },
                new Config{
                    Name = "PythonScope",
                    Keyword = "<p:PythonScope "
                },
                new Config{
                    Name = "RunPythonScript",
                    Keyword = "<p:RunScript "
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
                    Keyword = "<ui:ExecuteMacro "
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
                    Name = "ExcelReadRange",
                    Keyword = "<ui:ExcelReadRange "
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
                    Name = "ExcelDeleteColumn",
                    Keyword = "<ui:ExcelDeleteColumn "
                },
                new Config{
                    Name = "ExcelFilterTable",
                    Keyword = "<ui:ExcelFilterTable "
                },
                new Config{
                    Name = "ExcelGetTableRange",
                    Keyword = "<ui:ExcelGetTableRange "
                },
                new Config{
                    Name = "ExcelInsertColumn",
                    Keyword = "<ui:ExcelInsertColumn "
                },
                new Config{
                    Name = "ExcelSortTable",
                    Keyword = "<ui:ExcelSortTable "
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
                    Name = "DatabaseConnect",
                    Keyword = "<ui:DatabaseConnect "
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
                    Keyword = "<ui:DatabaseTransaction "
                },
                new Config{
                    Name = "TerminalGetField",
                    Keyword = "<uit:TerminalGetField "
                },
                new Config{
                    Name = "TerminalGetText",
                    Keyword = "<uit:TerminalGetText "
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
                    Name = "AddToCollection",
                    Keyword = "<AddToCollection "
                },
                new Config{
                    Name = "ClearCollection",
                    Keyword = "<ClearCollection "
                },
                new Config{
                    Name = "ExistsInCollection",
                    Keyword = "<ExistsInCollection "
                },
                new Config{
                    Name = "RemoveFromCollection",
                    Keyword = "<RemoveFromCollection "
                },
                new Config{
                    Name = "AddDataColumn",
                    Keyword = "<ui:AddDataColumn "
                },
                new Config{
                    Name = "AddDataRow",
                    Keyword = "<ui:AddDataRow "
                },
                new Config{
                    Name = "BuildDataTable",
                    Keyword = "<ui:BuildDataTable "
                },
                new Config{
                    Name = "ClearDataTable",
                    Keyword = "<ui:ClearDataTable "
                },
                new Config{
                    Name = "ForEachRow",
                    Keyword = "<ui:ForEachRow "
                },
                new Config{
                    Name = "GenerateDataTable",
                    Keyword = "<ui:GenerateDataTable "
                },
                new Config{
                    Name = "GetRowItem",
                    Keyword = "<ui:GetRowItem "
                },
                new Config{
                    Name = "MergeDataTable",
                    Keyword = "<ui:MergeDataTable "
                },
                new Config{
                    Name = "OutputDataTable",
                    Keyword = "<ui:OutputDataTable "
                },
                new Config{
                    Name = "RemoveDataColumn",
                    Keyword = "<ui:RemoveDataColumn "
                },
                new Config{
                    Name = "RemoveDataRow",
                    Keyword = "<ui:RemoveDataRow "
                },
                new Config{
                    Name = "Comment",
                    Keyword = "<ui:Comment "
                },
                new Config{
                    Name = "LogMessage",
                    Keyword = "<ui:LogMessage "
                },
                new Config{
                    Name = "WriteLine",
                    Keyword = "WriteLine"
                },
                new Config{
                    Name = "InvokeComMethod",
                    Keyword = "<ui:InvokeComMethod "
                },
                new Config{
                    Name = "InvokeMethod",
                    Keyword = "<InvokeMethod "
                },
                new Config{
                    Name = "Assign",
                    Keyword = "<Assign "
                },
                new Config{
                    Name = "Delay",
                    Keyword = "<Delay "
                },new Config{
                    Name = "DoWhile",
                    Keyword = "<DoWhile "
                },new Config{
                    Name = "If",
                    Keyword = "<If "
                },new Config{
                    Name = "Sequence",
                    Keyword = "<Sequence "
                },new Config{
                    Name = "Switch",
                    Keyword = "<Switch "
                },new Config{
                    Name = "While",
                    Keyword = "<While "
                },new Config{
                    Name = "FlowDecision",
                    Keyword = "<FlowDecision "
                },new Config{
                    Name = "FlowSwitch",
                    Keyword = "<FlowSwitch "
                },new Config{
                    Name = "Flowchart",
                    Keyword = "<Flowchart "
                },new Config{
                    Name = "StateMachine",
                    Keyword = "<StateMachine "
                },new Config{
                    Name = "Throw",
                    Keyword = "<Throw "
                },new Config{
                    Name = "TerminateWorkflow",
                    Keyword = "<TerminateWorkflow "
                },new Config{
                    Name = "TryCatch",
                    Keyword = "<TryCatch "
                },
                new Config{
                    Name = "Break",
                    Keyword = "<ui:Break "
                },
                new Config{
                    Name = "ForEach",
                    Keyword = "<ui:ForEach "
                },
                new Config{
                    Name = "RetryScope",
                    Keyword = "<ui:RetryScope "
                },
                new Config{
                    Name = "InvokeCode",
                    Keyword = "<ui:InvokeCode "
                },
                new Config{
                    Name = "InvokeWorkflowFile",
                    Keyword = "<ui:InvokeWorkflowFile "
                },
                new Config{
                    Name = "InvokeWorkflowInteractive",
                    Keyword = "<ui:InvokeWorkflowInteractive "
                },
                new Config{
                    Name = "AppendLine",
                    Keyword = "<ui:AppendLine "
                },
                new Config{
                    Name = "CopyFile",
                    Keyword = "<ui:CopyFile "
                },
                new Config{
                    Name = "CreateDirectory",
                    Keyword = "<ui:CreateDirectory "
                },
                new Config{
                    Name = "CreateFile",
                    Keyword = "<ui:CreateFile "
                },
                new Config{
                    Name = "Delete",
                    Keyword = "<ui:Delete "
                },
                new Config{
                    Name = "MoveFile",
                    Keyword = "<ui:MoveFile "
                },
                new Config{
                    Name = "PathExists",
                    Keyword = "<ui:PathExists "
                },
                new Config{
                    Name = "ReadTextFile",
                    Keyword = "<ui:ReadTextFile "
                },
                new Config{
                    Name = "WriteTextFile",
                    Keyword = "<ui:WriteTextFile "
                },
                new Config{
                    Name = "DocumentAppendText",
                    Keyword = "<p:DocumentAppendText "
                },
                new Config{
                    Name = "DocumentReadText",
                    Keyword = "<p:DocumentReadText "
                },
                new Config{
                    Name = "DocumentReplaceText",
                    Keyword = "<p:DocumentReplaceText "
                },
                new Config{
                    Name = "AppendRange",
                    Keyword = "<ui:AppendRange "
                },
                new Config{
                    Name = "GetTableRange",
                    Keyword = "<ui:GetTableRange "
                },
                new Config{
                    Name = "ReadCell",
                    Keyword = "<ui:ReadCell "
                },
                new Config{
                    Name = "ReadCellFormula",
                    Keyword = "<ui:ReadCellFormula "
                },
                new Config{
                    Name = "ReadColumn",
                    Keyword = "<ui:ReadColumn "
                },
                new Config{
                    Name = "ReadRange",
                    Keyword = "<ui:ReadRange "
                },
                new Config{
                    Name = "ReadRow",
                    Keyword = "<ui:ReadRow "
                },
                new Config{
                    Name = "WriteCell",
                    Keyword = "<ui:WriteCell "
                },
                new Config{
                    Name = "WriteRange",
                    Keyword = "<ui:WriteRange "
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
                    Name = "InvokePowerShell",
                    Keyword = "<ui:InvokePowerShell "
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
                    Name = "AddCredential",
                    Keyword = "<ui:AddCredential "
                },
                new Config{
                    Name = "DeleteCredential",
                    Keyword = "<ui:DeleteCredential "
                },
                new Config{
                    Name = "GetSecureCredential",
                    Keyword = "<ui:GetSecureCredential "
                },
                new Config{
                    Name = "RequestCredential",
                    Keyword = "<ui:RequestCredential "
                },
                new Config{
                    Name = "RemoveFromDictionary",
                    Keyword = "<p:RemoveFromDictionary "
                },
                new Config{
                    Name = "GetItemFromCollection",
                    Keyword = "<p:GetItemFromCollection "
                },
                new Config{
                    Name = "GetDictionaryValue",
                    Keyword = "<p:GetDictionaryValue "
                },
                new Config{
                    Name = "DictionaryContains",
                    Keyword = "<p:DictionaryContains "
                },
                new Config{
                    Name = "CountDictionary",
                    Keyword = "<p:CountDictionary "
                },
                new Config{
                    Name = "CountCollection",
                    Keyword = "<p:CountCollection "
                },
                new Config{
                    Name = "ClearDictionary",
                    Keyword = "<p:ClearDictionary "
                },
                new Config{
                    Name = "BuildCollection",
                    Keyword = "<p:BuildCollection "
                },
                new Config{
                    Name = "AddToDictionary",
                    Keyword = "<p:AddToDictionary "
                },
                new Config{
                    Name = "BuildDictionary",
                    Keyword = "<p:BuildDictionary "
                },
                new Config{
                    Name = "HttpDelete",
                    Keyword = "<p1:HttpDelete "
                },
                new Config{
                    Name = "HttpGet",
                    Keyword = "<p1:HttpGet "
                },
                new Config{
                    Name = "HttpMerge",
                    Keyword = "<p1:HttpMerge "
                },
                new Config{
                    Name = "HttpPost",
                    Keyword = "<p1:HttpPost "
                },
                new Config{
                    Name = "HttpPut",
                    Keyword = "<p1:HttpPut "
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
