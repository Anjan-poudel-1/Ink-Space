using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component1Assignment
{
    /// <summary>
    ///  implementations of Collection. These concrete collections can be structured in various ways. There is usually an iterator for each concrete collection.
    /// </summary>
    public class ConcreteCollection : AbstractCollection
    {
        private List<Shape> listShapes = new List<Shape>();

        /// <summary>
        /// Create Iterator
        /// </summary>
        /// <returns></returns>
        public Iterator CreateIterator()
        {
            return new Iterator(this);
        }

     
        /// <summary>
        /// Gets item count
        /// </summary>
        public int Count
        {
            get { return listShapes.Count; }
        }

       
        /// <summary>
        /// Add items to the collection
        /// </summary>
        /// <param name="shape"></param>
        public void AddShape(Shape shape)
        {
            listShapes.Add(shape);
        }

        /// <summary>
        /// Clears the shape
        /// </summary>
        public void clearShapes()
        {
            listShapes.Clear();
        }

      
        /// <summary>
        /// Get item from collection
        /// </summary>
        /// <param name="IndexPosition"></param>
        /// <returns></returns>
        public Shape GetShape(int IndexPosition)
        {
            try
            {
                return listShapes[IndexPosition];
            }
            catch (Exception)
            {
                return null; 
            }
            
        }
    }
}
