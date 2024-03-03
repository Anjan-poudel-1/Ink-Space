using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component1Assignment
{
    /// <summary>
    /// This class has properties of the canvas , i.e. pen colors, pointer ...
    /// </summary>
    public class DrawingCanvas
    {
        Graphics g;
        Pen drawingPen;
        Color penColor ;
        Color fillColor;
        int penThickness;
        int xCoordinate, yCoordinate;
        bool isFillModeOn;


        public DrawingCanvas()
        {
            this.penColor =  Color.Black;
            this.fillColor = Color.Transparent;
            this.penThickness = 2;
            this.isFillModeOn = false;
            xCoordinate = yCoordinate = 0;
            drawingPen = new Pen(this.penColor, this.penThickness);
        }

        public Pen DrawingPen
        {
            get { return this.drawingPen; }

            set { this.drawingPen = value; }
        }

        public bool IsFillModeOn
        {
            get { return this.isFillModeOn; }

            set { this.isFillModeOn = value; }
        }
       

        //Getter setter for pen color
        public Color PenColor
        {
            get { return this.penColor; }

            set { this.penColor = value; }
                
        }

        public Color FillColor
        {
            get { return this.fillColor; }

            set { this.fillColor = value; }
        }

        public int PenThickness
        {
            get { return this.penThickness;  }
            set { this.penThickness = value; }
        }

        public int XCoordinate
        {
            get { return this.xCoordinate; }
            set { this.xCoordinate = value; }
        }


        public int YCoordinate
        {
            get { return this.yCoordinate; }
            set { this.yCoordinate = value; }
        }


        //functionaliities


        //To reinitialise the point every time we draw shapes on canvas
        public void reInitialiseCursor(int xFinal,int yFinal)
        {
            this.yCoordinate = yFinal;
            this.xCoordinate = xFinal;
            
        }

    }
}
