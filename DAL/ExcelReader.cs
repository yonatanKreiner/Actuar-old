using System;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace DAL
{
    public class ExcelReader
    {
        static string EXCEL_PATH = @"E:\Projects\Dev\Web\Actuar\חוק פסיקת ריבית- 16.10.2016.xlsx";

        const string MADAD_SHEET = "מדדים וריביות";
        const int MADAD_DATES_COLUMN = 1;
        const int MADAD_VALUE_COLUMN = 2;
        const int MADAD_MINIMUM_ROW = 1;

        const string INCREMENTED_RIBIT_SHEET = "עבודה";
        const int INCREMENTED_RIBIT_DATES_COLUMN = 2;
        const int INCREMENTED_RIBIT_VALUE_COLUMN = 8;
        const int INCREMENTED_RIBIT_MINIMUM_ROW = 8;

        string sheet;
        int minimumRow;
        int dateColumn;
        int valueColumn;

        Excel.Application xlApp;
        Excel.Workbook xlWorkbook;
        Excel._Worksheet xlWorksheet;
        Excel.Range xlRange;
        int currentDataToRead;
        bool isOpen;

        static readonly ExcelReader instance = new ExcelReader();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static ExcelReader()
        {
        }

        ExcelReader()
        {
            currentDataToRead = -1;
            isOpen = false;
        }

        public static ExcelReader Instance
        {
            get
            {
                return instance;
            }
        }

        public enum ExcelData
        {
            Madad,
            DailyRibit,
            IncrementedRibit
        }

        public void InitializeArgumentsForReading(ExcelData dataToFetch)
        {
            //if (currentDataToRead == -1 || currentDataToRead != (int)dataToFetch)
            //{
            //    currentDataToRead = (int)dataToFetch;

                switch (dataToFetch)
                {
                    case ExcelData.Madad:
                        sheet = MADAD_SHEET;
                        dateColumn = MADAD_DATES_COLUMN;
                        valueColumn = MADAD_VALUE_COLUMN;
                        minimumRow = MADAD_MINIMUM_ROW;

                        break;
                    case ExcelData.DailyRibit:
                        break;
                    case ExcelData.IncrementedRibit:
                        sheet = INCREMENTED_RIBIT_SHEET;
                        dateColumn = INCREMENTED_RIBIT_DATES_COLUMN;
                        valueColumn = INCREMENTED_RIBIT_VALUE_COLUMN;
                        minimumRow = INCREMENTED_RIBIT_MINIMUM_ROW;

                        break;
                }

                xlWorksheet = xlWorkbook.Sheets[sheet];
                xlRange = xlWorksheet.UsedRange; 
            //}
        }

        public double GetDoubleValue(DateTime date)
        {
            for (int i = minimumRow; i <= xlRange.Rows.Count; i++)
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
                            return 0;
                        }

                        return data;
                    }
                }
            }

            return 0;
        }

        public bool Open()
        {
            try
            {
                xlApp = new Excel.Application();
                xlWorkbook = xlApp.Workbooks.Open(EXCEL_PATH);
                
                isOpen = true;
            }
            catch
            {
                isOpen = true;
            }

            return isOpen;
        }

        public bool Close()
        {
            try
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

                isOpen = false;
            }
            catch
            {
                isOpen = true;
            }

            return !isOpen;
        }
    }
}
