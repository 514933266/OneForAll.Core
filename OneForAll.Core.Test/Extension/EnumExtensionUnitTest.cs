using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneForAll.Core.Extension;
using OneForAll.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneForAll.Core.Test.Extension
{
    [TestClass()]
    public class EnumExtensionUnitTest
    {
        [TestMethod()]
        public void GetDescription()
        {
            var cat = new Cat();
            var descr = CatEnum.Name.GetDescription();
        }
    }
}
