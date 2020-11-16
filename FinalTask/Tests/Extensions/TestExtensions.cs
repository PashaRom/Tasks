using System;
using System.Collections.Generic;
using FinalTask.Tests.Models;
namespace FinalTask.Tests.Extension
{
    public static class TestExtensions
    {
        public static bool IsTestsDesc(this List<Test> tests)
        {
            Test previewTest = new Test();            
            bool descFlag = false;
            for (int i = 0; i < tests.Count; i++)
            {
                if (i == 0)
                {
                    previewTest = tests[i];
                    continue;
                }
                if (DateTime.Parse(previewTest.StartTime) > DateTime.Parse(tests[i].StartTime))
                {
                    previewTest = tests[i];
                    descFlag = true;
                    continue;
                }
                else
                    return false;
            }
            return descFlag;
        }                
    }
}
