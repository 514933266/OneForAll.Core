using System;
using System.Collections.Generic;
using System.Text;

namespace OneForAll.Core.ORM
{
    public interface IUnitAction
    {
        Func<int> Action { get; set; }
    }
}
