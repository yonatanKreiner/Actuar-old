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
        InterestCalculator calculator = new InterestCalculator();

        // GET: api/Interest
        [HttpGet]
        public double Get()
        {
            return calculator.finalDebt;
        }

        // GET: api/Interest
        [HttpGet]
        public double Get(double debt)
        {
            calculator.UpdateDebt(debt);
            return calculator.finalDebt;
        }

        // POST: api/Interest
        [HttpPost]
        public void Post(double debt)
        {
            calculator.UpdateDebt(debt);
        }
    }
}
