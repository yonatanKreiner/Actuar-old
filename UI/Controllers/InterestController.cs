using BL;
using System;
using System.Web.Http;
using UI.Models;

namespace UI.Controllers
{
    public class InterestController : ApiController
    {
        InterestCalculator calculator;

        // Get: api/Interest
        [HttpPost]
        public double Post([FromBody]CalculationData data)
        {
            calculator = new InterestCalculator(data.CalculationDate, data.Debts);
            
            return calculator.FinalDebt;
        }
    }
}
