using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component1Assignment
{
    /// <summary>
    ///  implementations of Iterator.
    /// </summary>
    interface AbstractIterator
    {
        Shape First();
        Shape Next();
        bool IsCompleted { get; }
    }
}
