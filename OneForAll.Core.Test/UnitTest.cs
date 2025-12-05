using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneForAll.Core.Test
{
    [TestClass()]
    public class UnitTest
    {
        [TestMethod()]
        public void ObjeTest()
        {
            // 基础消息对象
            var msg = new BaseMessage();
            BaseMessage.Success();
            BaseMessage.Fail();

            var msg2 = new BaseMessage<string>();
            msg2.Success();
            msg2.Fail();
            BaseMessage<string>.Success();
            BaseMessage<string>.Fail();
        }
    }
}
