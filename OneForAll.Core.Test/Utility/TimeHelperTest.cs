using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneForAll.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneForAll.Core.Test.Utility
{
    [TestClass()]
    public class TimeHelperTest
    {
        [TestMethod()]
        public void ToLongTimestampTest()
        {
            var tt = TimeHelper.ToLongTimeStamp(DateTime.Now);
        }

        [TestMethod()]
        public void ToDateTimeTest()
        {
            var tt = TimeHelper.ToTimeStamp(DateTime.Now);
            var tt2 = TimeHelper.ToLongTimeStamp(DateTime.Now);
            var date = TimeHelper.ToDateTime(tt);
            var date2 = TimeHelper.ToDateTime(tt2, true);
        }
    }
}
