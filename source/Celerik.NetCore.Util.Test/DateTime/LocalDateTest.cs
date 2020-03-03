using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Celerik.NetCore.Util.Test
{
    [TestClass]
    public class LocalDateTest : UtilBaseTest
    {
        [TestMethod]
        public void Now()
        {
            var iranTime = new LocalDate(3.3).Now;
            var colombiaTime = new LocalDate(-5).Now;
            var differenceHours = Math.Round((iranTime - colombiaTime).TotalHours, 2);

            Assert.AreEqual(8.5, differenceHours);
        }

        [TestMethod]
        public void FromSystemTime()
        {
            var systemTime = DateTime.Now;
            var localTime1 = new LocalDate(-5).Now;
            var localTime2 = new LocalDate(-5).FromSystemTime(systemTime);

            Assert.AreEqual(localTime1.Year, localTime2.Year);
            Assert.AreEqual(localTime1.Month, localTime2.Month);
            Assert.AreEqual(localTime1.Day, localTime2.Day);
            Assert.AreEqual(localTime1.Minute, localTime2.Minute);
            Assert.AreEqual(localTime1.Second, localTime2.Second);
        }
    }
}
