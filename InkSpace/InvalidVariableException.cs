using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component1Assignment
{
    /// <summary>
    /// The exception arises if user would try to declare invalid variable
    /// </summary>
    public class InvalidVariableException:Exception
    {
        public InvalidVariableException(string message) : base(message)
        {
        }
    }
}
