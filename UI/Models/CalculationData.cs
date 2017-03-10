using System;
using System.Collections.Generic;

namespace UI.Models
{
    public class CalculationData
    {
        public DateTime CalculationDate { get; set; }
        public Dictionary<DateTime, double> Debts { get; set; }

        public CalculationData(string calculationDate, Dictionary<string, double> debts)
        {
            CalculationDate = GetDateFromString(calculationDate);
            Debts = new Dictionary<DateTime, double>();

            foreach (var debt in debts)
            {
                Debts.Add(GetDateFromString(debt.Key), debt.Value);
            }
        }

        DateTime GetDateFromString(string date)
        {
            int year = int.Parse(date.Split('/')[2]);
            int month = int.Parse(date.Split('/')[1]);
            int day = int.Parse(date.Split('/')[0]);

            return new DateTime(year, month, day);
        }
    }
}