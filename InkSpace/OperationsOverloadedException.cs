using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component1Assignment
{
    /// <summary>
    /// The exception arises when user tries to perform operation for more than two operands
    /// </summary>
    internal class OperationsOverloadedException:Exception
    {
        public OperationsOverloadedException(string message) : base(message)
        {
        }
    }
}
