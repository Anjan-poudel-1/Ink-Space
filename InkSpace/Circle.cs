using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component1Assignment
{

    /// <summary>
    /// Class for shape "circle"
    /// </summary>
  public  class Circle : Shape
    {
        int radius;

        public Circle() : base()
        {

        }
        /// <summary>
        /// Constructor to initiate circle 
        /// </summary>
        /// <param name="_canvas"></param>
        /// <param name="radius"></param>
        public Circle(DrawingCanvas _canvas, int radius) : base(_canvas)
        {

            this.radius = radius; //the only thing that is different from shape
        }


        /// <summary>
        /// Sets the circle shape
        /// </summary>
        /// <param name="_canvas"></param>
        /// <param name="list"></param>
        public override void set(DrawingCanvas _canvas, params int[] list)
        {
            //list[0]  is radius
            
            ShapeParams = list;
            base.set(_canvas, list);
            this.radius = list[0];
        }


        /// <summary>
        /// Draw circle
        /// </summary>
        /// <param name="g"></param>
        public override void draw(Graphics g)
        {

            Pen p = new Pen(penColor, 2);
            SolidBrush b = new SolidBrush(fillcolor);
            g.FillEllipse(b, x-radius, y-radius, radius * 2, radius * 2);
            g.DrawEllipse(p, x-radius, y- radius, radius * 2, radius * 2);

        }

        
        public override string ToString() //all classes inherit from object and ToString() is abstract in object
        {
            return base.ToString() + "  " + this.radius;
        }
    }

}
