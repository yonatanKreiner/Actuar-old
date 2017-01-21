using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace DAL
{
    public static class ExcelReader
    {
        static string EXCEL_PATH = @"E:\Projects\Actuar\חוק פסיקת ריבית- 16.10.2016.xlsx";

        const int DATES_COLUMN = 1;
        const int MADAD_COLUMN = 2;
        const string MADAD_SHEET = "מדדים וריביות";

        public static double GetMadad(DateTime date)
        {
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(EXCEL_PATH);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[MADAD_SHEET];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            for (int i = 3; i <= xlRange.Rows.Count; i++)
            {
                if (xlRange.Cells[i, DATES_COLUMN] != null && xlRange.Cells[i, DATES_COLUMN].Value2 != null)
                {
                    // Converting the string in the excel cell to DateTime object and checking if the dates equals
                    if(DateTime.FromOADate(double.Parse(xlRange.Cells[i, DATES_COLUMN].Value2.ToString())) == date && xlRange.Cells[i, MADAD_COLUMN].Value2 != null)
                    {
                        double madadValue;

                        if (!double.TryParse(xlRange.Cells[i, MADAD_COLUMN].Value2.ToString(), out madadValue))
                        {
                            return 0;
                        }

                        CloseExcel(xlApp, xlWorkbook, xlWorksheet, xlRange);

                        return madadValue;
                    }
                }
            }

            CloseExcel(xlApp, xlWorkbook, xlWorksheet, xlRange);

            return 0;   
        }

        static void CloseExcel(Excel.Application xlApp, Excel.Workbook xlWorkbook, Excel._Worksheet xlWorksheet, Excel.Range xlRange)
        {
            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Close(false);
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
        }
    }
}
