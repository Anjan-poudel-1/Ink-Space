using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component1Assignment
{
    /// <summary>
    /// The exceeption arises if user performs loop in exceessive amount
    /// </summary>
    public class ExcessLoopException:Exception

    {
        public ExcessLoopException(string message) : base(message)
        {
        }
    }
}
