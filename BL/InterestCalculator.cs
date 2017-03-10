using System;
using System.Collections.Generic;
using BL.Models;
using DAL;

namespace BL
{
    public class InterestCalculator
    {
        public DateTime CalculationDate { get; set; }
        public List<Calculation> Calculations { get; set; }
        public double FinalDebt { get; set; }

        public InterestCalculator(DateTime calculationDate, Dictionary<DateTime, double> debts)
        {
            ExcelReader.Instance.Open();

            try
            {
                CalculationDate = calculationDate;
                Calculations = new List<Calculation>();
                FinalDebt = 0;

                foreach (var debt in debts)
                {
                    Calculation calculation = GetCalculation(CalculationDate, debt.Key, debt.Value);
                    Calculations.Add(calculation);
                    FinalDebt += calculation.FinalDebt;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                ExcelReader.Instance.Close();
            }            
        }

        Calculation GetCalculation(DateTime calculationDate, DateTime debtDate, double debt)
        {
            return new Calculation
            {
                Date = debtDate,
                CalculationDate = calculationDate,
                Madad = new Madad(debtDate, calculationDate, debt),
                Ribit = new Ribit(debtDate, debt),
                Debt = debt
            };
        }
    }
}
