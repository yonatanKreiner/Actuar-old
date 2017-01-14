using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (!date.Equals(new DateTime(2016, 10, 16)))
            {
                return 9528500.96;
            }

            return 33671038.31;
        }
    }
}
