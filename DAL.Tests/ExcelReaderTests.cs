using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL;

namespace DAL.Tests
{
    [TestClass]
    public class ExcelReaderTests
    {
        [TestMethod]
        public void ReadMadadFromExcelBeforeDay15()
        {
            double madad = ExcelReader.GetMadad(new DateTime(2016, 10, 14));
            Assert.AreEqual(33705015.13, madad, 0.001);
        }

        [TestMethod]
        public void ReadMadadFromExcelAfterDay15()
        {
            double madad = ExcelReader.GetMadad(new DateTime(2016, 10, 16));
            Assert.AreEqual(33671038.31, madad, 0.001);
        }

        //[TestMethod]
        //public void ReadRibitFromExcel()
        //{

        //}
    }
}
