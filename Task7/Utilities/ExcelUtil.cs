using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using Task7.Utilities.Logging;
namespace Task7.Utilities
{
    public static class ExcelUtil
    {
        public static void Write<T>(IEnumerable<T> objs, int sheetNumber, string sheetName)
        {            
            Type typeObject = typeof(T);
            FieldInfo[] fieldInfos = typeObject.GetFields(BindingFlags.NonPublic
                | BindingFlags.Instance
                | BindingFlags.Public);
            Excel.Application applicationExcel = new Excel.Application();
            try
            {
                string pathDirectory = $"{Directory.GetCurrentDirectory()}\\Out";
                if (!Directory.Exists(pathDirectory))
                    Directory.CreateDirectory(pathDirectory);
                string outFile = "\\testeddata.xlsx";
                string path = $"{pathDirectory}{outFile}";
                Excel.Workbook workbook;
                bool flagExistFile = File.Exists(path);
                if (!flagExistFile)
                    workbook = applicationExcel.Workbooks.Add();
                else
                    workbook = applicationExcel.Workbooks.Open(path);
                Excel.Worksheet worksheet;
                if (workbook.Worksheets.Count < sheetNumber)
                    worksheet = workbook.Worksheets.Add(After: workbook.Sheets[workbook.Sheets.Count]) as Excel.Worksheet;
                else
                    worksheet = workbook.Sheets[sheetNumber] as Excel.Worksheet;
                worksheet.Name = sheetName;
                List<string> fildNames = new List<string>();
                for (int i = 1; i <= fieldInfos.Length; i++)
                {
                    string name = fieldInfos[i - 1].Name;
                    string[] vs = name.Split(new char[] { '<', '>' });
                    fildNames.Add(vs[1]);
                    worksheet.Cells[1, i] = vs[1];
                    Excel.Range range = (Excel.Range)worksheet.Cells[1, i];
                    range.Font.Bold = true;
                    range.Font.Size = 14;
                }               
                int count = objs.Count();
                int counterRow = 2;                
                foreach (var result in objs)
                {                    
                    int counterColumn = 1;
                    foreach (string name in fildNames)
                    {                        
                        var value = result.GetType().GetProperty(name).GetValue(result, null).ToString();
                        worksheet.Cells[counterRow, counterColumn] = result.GetType().GetProperty(name).GetValue(result, null);
                        counterColumn++;
                    }                    
                    counterRow++;
                }                
                if (!flagExistFile)
                    workbook.SaveAs(path);
                else
                    workbook.Save();
                workbook.Close(false);
                Marshal.ReleaseComObject(workbook);
            }
            catch (Exception ex) 
            {
                Log.Error(ex, $"Unexpected error occurred during writing to excel file.");
            }
            finally 
            {
                applicationExcel.Quit();
                Marshal.ReleaseComObject(applicationExcel);
            }
        }
    }
}
