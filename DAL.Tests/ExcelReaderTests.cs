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
            //double madad = ExcelReader.GetMadad(new DateTime(2016, 8, 1));
            double madad = ExcelReader.GetDoubleValueFromExcel(ExcelReader.ExcelData.Madad, new DateTime(2016, 8, 1));
            Assert.AreEqual(33705015.13, madad, 0.1);
        }

        [TestMethod]
        public void ReadMadadFromExcelAfterDay15()
        {
            //double madad = ExcelReader.GetMadad(new DateTime(2016, 9, 1));
            double madad = ExcelReader.GetDoubleValueFromExcel(ExcelReader.ExcelData.Madad, new DateTime(2016, 9, 1));
            Assert.AreEqual(33671038.31, madad, 0.1);
        }

        [TestMethod]
        public void ReadMadadFromExcelWithANonValidDate()
        {
            //double madad = ExcelReader.GetMadad(new DateTime(2017, 9, 1));
            double madad = ExcelReader.GetDoubleValueFromExcel(ExcelReader.ExcelData.Madad, new DateTime(2017, 9, 1));
            Assert.AreEqual(0, madad, 0.1);
        }

        [TestMethod]
        public void ReadIncrementedRibitFromExcel()
        {
            //double ribit = ExcelReader.GetIncrementedRibit(new DateTime(1990, 2, 20));
            double ribit = ExcelReader.GetDoubleValueFromExcel(ExcelReader.ExcelData.IncrementedRibit, new DateTime(1990, 2, 20));
            Assert.AreEqual(2.659031, ribit, 0.001);
        }

        [TestMethod]
        public void ReadIncrementedRibitFromExcelWithANonValidDate()
        {
            //double ribit = ExcelReader.GetIncrementedRibit(new DateTime(2050, 1, 20));
            double ribit = ExcelReader.GetDoubleValueFromExcel(ExcelReader.ExcelData.IncrementedRibit, new DateTime(2050, 1, 20));
            Assert.AreEqual(0, ribit, 0.001);
        }
    }
}
