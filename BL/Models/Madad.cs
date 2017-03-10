using System;
using DAL;

namespace BL.Models
{
    public class Madad
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Debt { get; set; }
        public double StartValue { get; set; }
        public double EndValue { get; set; }
        public double Difference { get; set; }

        public Madad(DateTime startDate, DateTime endDate, double debt)
        {
            StartDate = startDate;
            EndDate = endDate;
            Debt = debt;
            StartValue = GetDataByDate(StartDate);
            EndValue = GetDataByDate(EndDate);
            Difference = EndValue / StartValue * Debt - Debt;
        }

        double GetDataByDate(DateTime date)
        {
            DateTime madadDate = new DateTime(date.Year, date.Month, 1);

            if (date.Day < 15)
            {
                madadDate = madadDate.AddMonths(-2);
            }
            else
            {
                madadDate = madadDate.AddMonths(-1);
            }

            ExcelReader.Instance.InitializeArgumentsForReading(ExcelReader.ExcelData.Madad);
            return ExcelReader.Instance.GetDoubleValue(madadDate);
        }
    }
}
