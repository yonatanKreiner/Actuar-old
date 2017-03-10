using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Threading.Tasks;

namespace BL.Models
{
    public class Ribit
    {
        public DateTime Date { get; set; }
        public double Debt { get; set; }
        public double TomorrowAccumulativePrecentage { get; set; }
        public double Difference { get; set; }

        public Ribit(DateTime date, double debt)
        {
            ExcelReader.Instance.InitializeArgumentsForReading(ExcelReader.ExcelData.IncrementedRibit);

            Date = date;
            Debt = debt;
            TomorrowAccumulativePrecentage = ExcelReader.Instance.GetDoubleValue(Date.AddDays(1));
            Difference = TomorrowAccumulativePrecentage * Debt - Debt;
        }
    }
}
