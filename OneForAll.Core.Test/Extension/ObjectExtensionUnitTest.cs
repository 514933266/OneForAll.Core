using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneForAll.Core.Extension;
using OneForAll.Core.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneForAll.Core.Test.Extension
{
    [TestClass()]
    public class ObjectExtensionUnitTest
    {
        [TestMethod()]
        public void HasAttributeTest()
        {
            var cat = new Cat();
            var exists = cat.Name.HasAttribute<DisplayNameAttribute>();
        }

        [TestMethod()]
        public void GetAttributeValue()
        {
            var cat = new Cat();
            var descr = cat.Name.GetAttribute<System.ComponentModel.DescriptionAttribute>("Description");
        }
    }

    public class Cat
    {
        [DisplayName("夏天")]
        [System.ComponentModel.Description("布偶猫")]
        public string Name { get; set; } = "布偶";
    }

    public enum CatEnum
    {
        [System.ComponentModel.Description("布偶猫")]
        Name,
        Foot
    }
}
