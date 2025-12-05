using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneForAll.Core.Extension;
using OneForAll.Core.ORM.Models;
using OneForAll.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneForAll.Core.Test
{
    [TestClass()]
    public class CollectionExtensionUnitTest
    {
        List<Tree> list = new List<Tree>()
        {
            new Tree() { Id = 1, ParentId = 0, Name = "姓名0" },
            new Tree() { Id = 2, ParentId = 1, Name = "姓名1" },
            new Tree() { Id = 6, ParentId = 2, Name = "姓名11"},
            new Tree() { Id = 3, ParentId = 1, Name = "姓名2" },
            new Tree() { Id = 4, ParentId = 1, Name = "姓名3" },
            new Tree() { Id = 5, ParentId = 2, Name = "姓名4" },
        };

        [TestMethod()]
        public void FindChildrenTest()
        {
            var parent = new Tree() { Id = 1, ParentId = 0 };
            var children = CollectionHelper.FindChildren<Tree, int>(list, parent, false);
            var children2 = list.FindChildren<Tree, int>(parent, true);
        }

        [TestMethod()]
        public void FindParentTest()
        {
            var entity = list.First(w => w.Id == 5);
            var parent = CollectionHelper.FindParent<Tree, int>(list, entity);
            var parent2 = list.FindParent(2, 1);
        }

        [TestMethod()]
        public void ConverToTreeTest()
        {
            var tree = list.ToTree<Tree, int>();
        }

        [TestMethod()]
        public void FindNode()
        {
            var tree = list.ToTree<Tree, int>();
            var item = list.FindNode(1);
        }
    }

    public class Tree : IEntity<int>, IParent<int>, IChildren<Tree>
    {
        public Tree()
        {
            Children = new HashSet<Tree>();
        }
        public int Id { get; set; }

        public int ParentId { get; set; }
        public string Name { get; set; }

        public IEnumerable<Tree> Children { get; set; }
    }
}