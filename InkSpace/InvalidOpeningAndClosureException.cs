using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component1Assignment
{
    /// <summary>
    /// The exception arises when there are not valid amount of closure for opening if,while,method commands
    /// </summary>
    public class InvalidOpeningAndClosureException:Exception
    {
        public InvalidOpeningAndClosureException(string message) : base(message)
        {
        }
    }
}
