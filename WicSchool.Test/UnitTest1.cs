using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WicSchool.Models;

namespace WicSchool.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestWage()
        {
            double expected = double.Parse("450");
            Employee employee = new Employee();
            employee.EmployeeId = 1;
            employee.RatePerHour = double.Parse("45");

            double actual = employee.CalculateWage(10);
            double diff = (double)0.0001M;

            Assert.IsTrue(Math.Abs(expected - actual) < diff);
        }

        [TestMethod]
        public void TestTax()
        {
            double expected = double.Parse("86.4");
            Employee employee = new Employee();
            employee.EmployeeId = 1;
            employee.RatePerHour = double.Parse("45");

            double actual = employee.CalculateTax(10);
            double diff = (double)0.0001M;

            Assert.IsTrue(Math.Abs(expected - actual) < diff);
        }
    }
}
