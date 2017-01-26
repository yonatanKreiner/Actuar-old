using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace UI.Controllers
{
    public class InterestController : ApiController
    {
        InterestCalculator calculator;

        DateTime GetDateFromString(string date)
        {
            int year = int.Parse(date.Split('/')[2]);
            int month = int.Parse(date.Split('/')[1]);
            int day = int.Parse(date.Split('/')[0]);

            return new DateTime(year, month, day);
        }

        // GET: api/Interest
        [HttpGet]
        public double Get(double debt, string debtDate, string calculationDate)
        {
            DateTime debtDateTime = GetDateFromString(debtDate);
            DateTime calculationDateTime = GetDateFromString(calculationDate);

            calculator = new InterestCalculator(debtDateTime, calculationDateTime, debt);
            return calculator.FinalDebt;
        }
    }
}
