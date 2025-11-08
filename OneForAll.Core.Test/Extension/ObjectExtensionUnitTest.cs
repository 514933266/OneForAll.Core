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
            //var exists = CatEnum.Name.HasAttribute<System.ComponentModel.DescriptionAttribute>();
        }

        [TestMethod()]
        public void GetAttribute()
        {
            var cat = new Cat();
            var descr = ObjectHelper.GetAttribute<System.ComponentModel.DescriptionAttribute>(cat, "Name1");
        }

        [TestMethod()]
        public void GetAttributeValue()
        {
            var cat = new Cat();
            var descr = ObjectHelper.GetAttributeValue<System.ComponentModel.DescriptionAttribute>(cat, "Name", "Description");
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
        [System.ComponentModel.Description("名字")]
        Name,
        [System.ComponentModel.Description("脚")]
        Foot
    }
}
