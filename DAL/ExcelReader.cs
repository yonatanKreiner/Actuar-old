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

        const string MADAD_SHEET = "מדדים וריביות";
        const int MADAD_DATES_COLUMN = 1;
        const int MADAD_VALUE_COLUMN = 2;
        const int MADAD_MINIMUM_ROW = 1;

        const string INCREMENTED_RIBIT_SHEET = "עבודה";
        const int INCREMENTED_RIBIT_DATES_COLUMN = 2;
        const int INCREMENTED_RIBIT_VALUE_COLUMN = 8;
        const int INCREMENTED_RIBIT_MINIMUM_ROW = 8;

        public enum ExcelData
        {
            Madad,
            DailyRibit,
            IncrementedRibit
        }

        public static double GetDoubleValueFromExcel(ExcelData dataToFetch, DateTime date)
        {
            string sheet = string.Empty;
            int row = 0;
            int dateColumn = 0, valueColumn = 0;

            switch (dataToFetch)
            {
                case ExcelData.Madad:
                    sheet = MADAD_SHEET;
                    dateColumn = MADAD_DATES_COLUMN;
                    valueColumn = MADAD_VALUE_COLUMN;
                    row = MADAD_MINIMUM_ROW;

                    break;
                case ExcelData.DailyRibit:
                    break;
                case ExcelData.IncrementedRibit:
                    sheet = INCREMENTED_RIBIT_SHEET;
                    dateColumn = INCREMENTED_RIBIT_DATES_COLUMN;
                    valueColumn = INCREMENTED_RIBIT_VALUE_COLUMN;
                    row = INCREMENTED_RIBIT_MINIMUM_ROW;

                    break;
                default:
                    return 0;
            }

            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(EXCEL_PATH);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[sheet];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            for (int i = row; i <= xlRange.Rows.Count; i++)
            {
                if (xlRange.Cells[i, dateColumn] != null && xlRange.Cells[i, dateColumn].Value2 != null)
                {
                    double excelDate;
                    
                    // Converting the string in the excel cell to DateTime object and checking if the dates equals
                    if (double.TryParse(xlRange.Cells[i, dateColumn].Value2.ToString(), out excelDate) && DateTime.FromOADate(excelDate) == date && xlRange.Cells[i, valueColumn].Value2 != null)
                    {
                        double data;

                        if (!double.TryParse(xlRange.Cells[i, valueColumn].Value2.ToString(), out data))
                        {
                            CloseExcel(xlApp, xlWorkbook, xlWorksheet, xlRange);

                            return 0;
                        }

                        CloseExcel(xlApp, xlWorkbook, xlWorksheet, xlRange);

                        return data;
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
            Marshal.FinalReleaseComObject(xlRange);
            Marshal.FinalReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Close(0);
            Marshal.FinalReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.FinalReleaseComObject(xlApp);
        }
    }
}
