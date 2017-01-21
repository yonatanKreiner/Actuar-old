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
        const string EXCEL_PATH = @"E:\Projects\Actuar\חוק פסיקת ריבית- 16.10.2016.xlsx";
        const int DATES_COLUMN = 1;
        const int MADAD_COLUMN = 2;

        public static double GetMadad(DateTime date)
        {
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(EXCEL_PATH);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets["מדדים וריביות"];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            DateTime madadDate = new DateTime(date.Year, date.Month, 1);

            if (date.Day < 15)
            {
                madadDate = madadDate.AddMonths(-2);
            }
            else
            {
                madadDate = madadDate.AddMonths(-1);
            }

            List<DateTime> dates = new List<DateTime>();

            for (int i = 3; i <= xlRange.Rows.Count; i++)
            {
                if (xlRange.Cells[i, DATES_COLUMN] != null && xlRange.Cells[i, DATES_COLUMN].Value2 != null)
                {
                    // Converting the string in the excel cell to DateTime object
                    if(DateTime.FromOADate(double.Parse(xlRange.Cells[i, DATES_COLUMN].Value2.ToString())) == madadDate)
                    {
                        CloseExcel(xlApp, xlWorkbook, xlWorksheet, xlRange);
                        return double.Parse(xlRange.Cells[i, MADAD_COLUMN].Value2.ToString());
                    }
                }
            }

            CloseExcel(xlApp, xlWorkbook, xlWorksheet, xlRange);

            return 2;   
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
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
        }
    }
}
