using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component1Assignment
{

   public class ShapeFactory
    {

        /// <summary>
        /// Getshape command
        /// </summary>
        /// <param name="shapeType"></param>
        /// <returns></returns>
        public Shape getShape(String shapeType)
        {
            shapeType = shapeType.ToUpper().Trim(); //you could argue that you want a specific word string to create an object but I'm allowing any case combination


            if (shapeType.Equals("CIRCLE"))
            {
                return new Circle();

            }
            else if (shapeType.Equals("RECTANGLE"))
            {
                return new Rectangle();

            }
            else if (shapeType.Equals("LINE"))
            {
                return new Line();
            }
            else if (shapeType.Equals("TRIANGLE"))
            {
                return new Triangle();
            }
            else
            {
                //if we get here then what has been passed in is inkown so throw an appropriate exception
                System.ArgumentException argEx = new System.ArgumentException("Factory error: provided shape does not exist");
                throw argEx;
            }


        }
    }

}
