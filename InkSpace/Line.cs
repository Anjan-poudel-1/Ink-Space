using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component1Assignment
{
    /// <summary>
    /// To create a line drawn by command , moveto
    /// </summary>
    public class Line : Shape
    {
        int x2;
        int y2;


        /// <summary>
        /// constructor
        /// </summary>
        public Line() : base()
        {

        }

        /// <summary>
        /// Create line with provided parameter
        /// </summary>
        /// <param name="_canvas"></param>
        /// <param name="_x2"></param>
        /// <param name="_y2"></param>
        public Line(DrawingCanvas _canvas, int _x2, int _y2) : base(_canvas)
        {

            this.x2 = _x2;
            this.y2 = _y2;
        }

        /// <summary>
        /// Set the value for line
        /// </summary>
        /// <param name="_canvas"></param>
        /// <param name="list"></param>
        public override void set(DrawingCanvas _canvas, params int[] list)
        {
            base.set(_canvas, list[0], list[1]);
            this.x2 = list[0];
            this.y2 = list[1];


        }


        /// <summary>
        /// Drawing the line
        /// </summary>
        /// <param name="g"></param>
        public override void draw(Graphics g)
        {
            //line cannot be transparent.. so check if it is transparent

            Color newColor;
            if (penColor == Color.Transparent)
            {
                newColor = fillcolor;
            }
            else
            {
                newColor = penColor;
            }
            Pen p = new Pen(newColor, 2);

            g.DrawLine(p,x, y, x2, y2);


        }

        /// <summary>
        /// all classes inherit from object and ToString() is abstract in object
        /// </summary>
        /// <returns></returns>
        public override string ToString() 
        {
            return base.ToString() + "  " + this.x2 + " " + this.y2;
        }
    }
}

