using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component1Assignment
{
    /// <summary>
    /// the interface for all  iterators. It implements two simple functions, next() and isCompleted().
    /// </summary>
    public class Iterator : AbstractIterator
    {
        private ConcreteCollection collection;
        private int current = 0;
        private int step = 1;

        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="collection"></param>
        public Iterator(ConcreteCollection collection)
        {
            this.collection = collection;
        }


        /// <summary>
        /// Gets first item
        /// </summary>
        /// <returns></returns>
        public Shape First()
        {
            current = 0;
            return collection.GetShape(current);
        }

      
        /// <summary>
        /// Gets next item
        /// </summary>
        /// <returns></returns>
        public Shape Next()
        {
            current += step;
            if (!IsCompleted)
            {
                return collection.GetShape(current);
            }
            else
            {
                return null;
            }
        }

        
        /// <summary>
        /// Check whether iteration is complete
        /// </summary>
        public bool IsCompleted
        {
            get { return current >= collection.Count; }
        }
    } 
}
