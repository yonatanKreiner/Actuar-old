using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Models;

namespace BL
{
    public class InterestCalculator
    {
        double debt;

        public double inalDebt;
        public DateTime DebtDate { get; set; }
        public DateTime CalculationDate { get; set; }
        public Madad Madad { get; set; }
        public Ribit Ribit { get; set; }
        public double RibitDefference { get; set; }
        public double HazmadaRibit { get; set; }
        public double Debt
        {
            get { return debt; }
            set
            {
                debt = value;
                RibitDefference = Ribit.NextAccumulativeRibit * debt - debt;
                HazmadaRibit = Madad.EndValue / Madad.StartValue * RibitDefference - RibitDefference;
                Extra = Madad.Difference + RibitDefference + HazmadaRibit;
                FinalDebt = Extra + debt;
            }
        }
        public double FinalDebt { get; set; }
        public double Extra { get; set; }

        public InterestCalculator() { }

        public InterestCalculator(DateTime debtDate, DateTime calculationDate, double debt)
        {
            DebtDate = debtDate;
            CalculationDate = calculationDate;
            Madad = new Madad(DebtDate, CalculationDate, debt);
            Ribit = new Ribit(DebtDate);
            Debt = debt;
        }
        
    }
}
