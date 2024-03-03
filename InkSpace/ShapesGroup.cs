using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component1Assignment
{

    /// <summary>
    /// Interface to define the shapes
    /// </summary>
    public class ShapesGroup
    {
        interface Shapes
        {
            void set(Color c);
            void draw(Graphics g);
        }

    }
}
