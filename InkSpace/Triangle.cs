using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component1Assignment
{

    /// <summary>
    /// CClass to define the shape , "Triangle"
    /// </summary>
    public class Triangle:Shape
    {
        Point point1;
        Point point2;


        /// <summary>
        /// Constructor
        /// </summary>
        public Triangle() : base()
        {

        }
        /// <summary>
        /// Parameterised constructor
        /// </summary>
        /// <param name="_canvas"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        public Triangle(DrawingCanvas _canvas, Point p1 , Point p2) : base(_canvas)
        {

            this.point1 = p1; //the only thing that is different from shapea
            this.point2 = p2;

        }

        /// <summary>
        /// Set values for triangle 
        /// </summary>
        /// <param name="_canvas"></param>
        /// <param name="list"></param>
        public override void set(DrawingCanvas _canvas, params int[] list)
        {
            //list[0]  is radius
            base.set(_canvas);
            Point p1 = new Point(list[0]+x, list[1]+y);
            Point p2 = new Point(list[2]+x, list[3]+y);

            this.point1 = p1;
            this.point2 = p2;



        }

        /// <summary>
        /// we need to shift every point , not just the origin for triangle 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public override void shiftOrigin(int x, int y)
        {
            base.shiftOrigin(x, y);
           point1 = new Point(point1.X + x, point1.Y + y);
           point2 = new Point(point2.X + x, point2.Y + y);

        }


        /// <summary>
        /// Draw triangle 
        /// </summary>
        /// <param name="g"></param>
        public override void draw(Graphics g)
        {

            Pen p = new Pen(penColor, 2);
            SolidBrush b = new SolidBrush(fillcolor);

            Point initialPoint = new Point(x, y);
            Point[] groupPoints = {initialPoint, point1, point2};

            if (penColor == Color.Transparent)
            {
                
                g.FillPolygon(b, groupPoints);
            }
            else
            {
                g.DrawPolygon(p, groupPoints);


            }

        }

        /// <summary>
        /// all classes inherit from object and ToString() is abstract in object
        /// </summary>
        /// <returns></returns>
        public override string ToString() 
        {
            return base.ToString() + "  " ;
        }
    }
}
