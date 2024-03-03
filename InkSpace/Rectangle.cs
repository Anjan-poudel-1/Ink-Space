using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component1Assignment
{
    /// <summary>
    /// Rectangle class
    /// </summary>
   public class Rectangle : Shape
    {
        int width, height;
        public Rectangle() : base()
        {
            width = 100;
            height = 100;
        }

        /// <summary>
        /// Constructor to set rectangle
        /// </summary>
        /// <param name="_canvas"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Rectangle(DrawingCanvas _canvas, int width, int height) : base(_canvas)
        {

            this.width = width; //the only thingthat is different from shape
            this.height = height;
        }

        /// <summary>
        /// Set function override from its super class Shape
        /// </summary>
        /// <param name="_canvas"></param>
        /// <param name="list"></param>
        public override void set(DrawingCanvas _canvas, params int[] list)
        {
            //list[0] is x, list[1] is y, list[2] is width, list[3] is height
            base.set(_canvas);
            this.width = list[0];
            this.height = list[1];

        }

        /// <summary>
        /// Draw rectangle
        /// </summary>
        /// <param name="g"></param>
        public override void draw(Graphics g)
        {
            Pen p = new Pen(penColor,2);
            SolidBrush b = new SolidBrush(fillcolor);
            
            g.FillRectangle(b, x, y, width, height);
            g.DrawRectangle(p, x, y, width, height);
        }

     
    }

}
