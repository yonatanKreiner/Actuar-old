using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class Ribit
    {
        public DateTime Date { get; set; }
        public double YearlyPrecentage { get; set; }
        public double DailyPrecentage { get; set; }
        public double AccumulativePrecentage { get; set; }

        public double NextAccumulativeRibit { get; set; }

        public Ribit(DateTime date)
        {
            Date = date;
            NextAccumulativeRibit = 266.0152 / 100;
            GetDataByDate();
            CalculateRibitByYear();
        }

        void GetDataByDate()
        {
            YearlyPrecentage = 8;
        }

        void CalculateRibitByYear()
        {
            DailyPrecentage = Math.Pow((1 + YearlyPrecentage / 100), (1 / 365.25)) - 1;
            AccumulativePrecentage = NextAccumulativeRibit * (1 + DailyPrecentage);
        }
    }
}
