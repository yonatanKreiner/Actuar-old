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

        // GET: api/Interest
        [HttpGet]
        public double Get()
        {
            return calculator.FinalDebt;
        }

        // GET: api/Interest
        [HttpGet]
        public double Get(double debt)
        {
            calculator = new InterestCalculator(new DateTime(1990, 2, 17), new DateTime(2016, 10, 16), debt);
            return calculator.FinalDebt;
        }
    }
}
