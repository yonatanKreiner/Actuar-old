using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class InterestCalculator
    {
        double debt;
        DateTime lawnDate;
        DateTime calculationDate;
        double startMadad = 9528500.96;
        double endMadad = 33671038.31;
        double ribitPrecentage = 265.9031;
        double MadadDifference;
        double ribitDifference;
        double hazmadaRibit;
        double sumExtra;
        public double finalDebt;

        public InterestCalculator() { }

        public InterestCalculator(double debt)
        {
            UpdateDebt(debt);
        }


        public InterestCalculator(DateTime lawnDate, DateTime calculationDate) { }

        public void UpdateDebt(double debt)
        {
            this.debt = debt;
            MadadDifference = endMadad / startMadad * debt - debt;
            ribitDifference = 265.8471 / 100 * debt - debt;
            hazmadaRibit = endMadad / startMadad * ribitDifference - ribitDifference;
            sumExtra = MadadDifference + ribitDifference + hazmadaRibit;
            finalDebt = sumExtra + debt;
        }
    }
}
