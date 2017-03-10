using System;

namespace BL.Models
{
    public class Calculation
    {
        double debt;
        double ribitDefference { get; set; }
        double hazmadaRibit { get; set; }
        double extra { get; set; }

        public DateTime CalculationDate { get; set; }
        public DateTime Date { get; set; }
        public Madad Madad { get; set; }
        public Ribit Ribit { get; set; }
        public double FinalDebt { get; private set; }
        public double Debt
        {
            get { return debt; }
            set
            {
                debt = value;
                ribitDefference = Ribit.Difference;
                hazmadaRibit = Madad.EndValue / Madad.StartValue * ribitDefference - ribitDefference;
                extra = Madad.Difference + ribitDefference + hazmadaRibit;
                FinalDebt = extra + debt;
            }
        }

    }
}
