using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Component1Assignment
{
    /// <summary>
    /// the interface for any type of collection, regardless of structure.
    /// </summary>
    interface AbstractCollection
    {
        Iterator CreateIterator();
    }
}
