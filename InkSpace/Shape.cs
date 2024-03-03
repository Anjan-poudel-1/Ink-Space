using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Component1Assignment
{
    public abstract class Shape : ShapesGroup
    {
        protected Color fillcolor; //shape's colour
        protected Color penColor;
        protected int x, y; 
        protected int[] shapeParams;
        public Shape()
        {
            fillcolor = Color.Transparent;
            penColor = Color.Black;
            x = y = 0;
        }

        
        public int[] ShapeParams
        {
            get
            {
                return this.shapeParams;
            }

            set
            {
                Console.WriteLine("Called");
                this.shapeParams = value;
            }
        }
        /// <summary>
        /// Defines shpe using params from the canvas
        /// </summary>
        /// <param name="_canvas"></param>
        public Shape(DrawingCanvas _canvas)
        {
            this.fillcolor = _canvas.FillColor; //shape's colour
            this.penColor = _canvas.PenColor;
            this.x = _canvas.XCoordinate; //its x pos
            this.y = _canvas.YCoordinate; //its y pos
            //can't provide anything else as "shape" is too general
        }

        //the three methods below are from the Shapes interface
        //here we are passing on the obligation to implement them to the derived classes by declaring them as abstract
        public abstract void draw(Graphics g ); //any derrived class must implement this method


        //set is declared as virtual so it can be overridden by a more specific child version
        //but is here so it can be called by that child version to do the generic stuff
        //note the use of the param keyword to provide a variable parameter list to cope with some shapes having more setup information than others
        public virtual void set(DrawingCanvas _canvas, params int[] list)
        {
            this.fillcolor = _canvas.FillColor;
            this.penColor = _canvas.PenColor;
            this.x = _canvas.XCoordinate; //its x pos
            this.y = _canvas.YCoordinate; //its y pos
            
        }

        /// <summary>
        /// Override the coolor of shape
        /// </summary>
        /// <param name="penColor"></param>
        /// <param name="fillColor"></param>
        public virtual void setColor(Color penColor, Color fillColor)
        {
            this.fillcolor = fillColor;
            this.penColor = penColor;
        }

        /// <summary>
        /// Override the set origin of shape
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public virtual void shiftOrigin(int x, int y)
        {
            this.x = this.x + x;
            this.y = this.y + y;
        }
        public override string ToString()
        {
            return base.ToString() + "    " + this.x + "," + this.y + " : ";
        }

    }

}
