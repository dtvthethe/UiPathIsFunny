using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UiPathIsFunny.Business;
using UiPathIsFunny.Model;

namespace UiPathIsFunny.Test
{
    [TestClass]
    public class UnitTest1
    {



        [TestMethod]
        public void TestMethod1()
        {
            string strConfig = @"C:\Users\thedtv\Desktop\abc.config";

            string flowPath = @"C:\Users\thedtv\Desktop\Maaa\Main.xaml";
            string flowSeq = @"C:\Users\thedtv\Desktop\Maaa\MainSeq.xaml";
            ConfigBusiness configBusiness = new ConfigBusiness();


            string[] paths = { flowPath, flowSeq };

            var lstCopnfig = configBusiness.Import(strConfig);


            Assert.AreEqual(lstCopnfig.Count, 6);


            Assert.AreNotEqual(lstCopnfig[0].Keyword, "");
         

        }

        public static List<Activity> GetActivities()
        {
            return new List<Activity>() {
                new Activity(){
                    Name = "Hhaah",
                    Keyword = "<HAHAHAHAHAHAHA ",
                },
                new Activity(){
                    Name = "??",
                    Keyword = "<?:: ",
                },
                new Activity(){
                    Name = "If",
                    Keyword = "<If ",
                },
                new Activity(){
                    Name = "NBullll",
                    Keyword = "",
                },
                new Activity(){
                    Name = "Sequence",
                    Keyword = "<Sequence ",
                },
                new Activity(){
                    Name = "One",
                    Keyword = " ",
                },
                new Activity(){
                    Name = "One1",
                    Keyword = "S",
                },
                new Activity(){
                    Name = "One2",
                    Keyword = "If",
                },
                new Activity(){
                    Name = "NoThing",
                    Keyword = "     ",
                },
                 new Activity(){
                     Name = "FlowChart",
                     Keyword = "<Flowchart ",
                }
            };
        }

    }

}
