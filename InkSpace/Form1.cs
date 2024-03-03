using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Component1Assignment
{
    /// <summary>
    /// This is the main class that represents  form 
    /// </summary>
    public partial class DrawShapes : Form
    {

        Thread newThread;
        bool flag = false, running = false;
        Color color1,color2;
        int animateX=0,animateY=0,shiftX=0,shiftY=0;   
        bool overrideShapeColor = false;
        bool overrideOrigins = false;
        bool animateShapes = false;


       
        DrawingCanvas drawingCanvas;
        Graphics g;

        /// <summary>
        /// Getter, setter for oveerrideColor
        /// </summary>
       public bool OverrideShapeColor
        {
            get { return overrideShapeColor; }

            set { this.overrideShapeColor = value; }
        }
        
        //Creating object for shapes
        ConcreteCollection shapeCollection = new ConcreteCollection();

        string syntaxInfo = "" +
               "\t\t\tCommand Syntax\n\n" +
               "Command Line Arguments\n\n" +
               "\t 1. Run - to run the program code\n" +
               "\t 2. Clear - to clear the canvas \n" +
               "\t 3. Reset - to clear the canvas and reset the pointer to 0,0\n" +
               "\t 4. Stop - to stop the thread/animation\n\n" +
               "Program Commands\n\n" +
                "\t Drawing Shapes\n\n"+
               "\t 1. drawTo <int> <int> - Draws line\n" +
               "\t 2. moveTo <int> <int> - Move to pointer\n" +
               "\t 3. fill ('on' || 'off') - toggle fill mode\n" +
               "\t 4. pen <string>'colorName - changes pen color'\n" +
               "\t 5. rectangle <int> <int> - Draw rectangle\n" +
               "\t 6. square <int> - Draw square\n" +
               "\t 7. circle <int> - Draw circle\n" +
               "\t 8. triangle <int,int> <int,int>\n\n"+
               "\t Loops,method and assignments\n\n" +
               "\t 1.<variable> = <value> - Assign value to variable\n" +
               "\t 2.if <condtion> then <statement> - Single line if command\n" +
               "\t 3. if <codition>\n \t   ...... \n\t endif - Multi line if command\n" +
                "\t 4. while <codition>\n \t  ...... \n\t  endwhile - Loop\n" +
                "\t 5. method <methodName>(<parameter1>,<parameter2>,...)\n \t  ...... \n\t  endmethod - Method Declaration\n" +
                "\t 6. method <methodName>(<parameter1>,<parameter2>,...) \n\n" +
               "Additional Commands\n\n" + 
               "\t 1. shift <x1>,<y1> - shifts the shape by defined coordinates\n"+
               "\t 2. animate <x1>,<y1> - animates shape by defined direction";




        public DrawShapes()
        {
            //Initialising components and canvas
            InitializeComponent();
            drawingCanvas = new DrawingCanvas();
            newThread = new Thread(new ThreadStart(thread));
            newThread.IsBackground = true;
            newThread.Start();

        }

        /// <summary>
        /// Thread function 
        /// </summary>
        public void thread()
        {

            while (true)
            {
                while (running)
                {
                    Console.WriteLine("Thread is runnign");
                    if (flag == false)
                    {
                        changeColor(color1);
                        flag = true;
                    }
                    else
                    {
                        changeColor(color2);
                        flag = false;
                    }
                    Thread.Sleep(500);
                }
            }
        }


        public string SyntaxInfo{
           get{ return syntaxInfo; }
         set{ syntaxInfo = value; }
 
}

       
        /// <summary>
        /// When 'excute' buttton is clicked or clicked enter on command line
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void executeBtn_Click(object sender, EventArgs e)
        {
            running =false;
           runExecuteCommand();
        }


        /// <summary>
        /// The function is called when run button is pressed.. or enter clicked on command line
        /// </summary>
        public void runExecuteCommand()
        {

            
            drawingCanvas.FillColor = Color.Transparent;
            drawingCanvas.IsFillModeOn = false;
            drawingCanvas.PenColor = Color.Black;
            //Get values for program command and command line
            string commandLineText = commandLine.Text.Trim().ToLower();
            string commands = codeTextBox.Text.Trim().ToLower();
            CommandParser commandParser = new CommandParser();

            //We except shapes to return on 'run' command. For other command empty list will be returned
           commandParser.executeCommands(commandLineText, commands, drawingCanvas, this, shapeCollection);

            Refresh();
        }


        /// <summary>
        /// Function to print errors on error log
        /// </summary>
        /// <param name="datas"></param>
        public void displayErrorLogs(List<string> datas)
        {
            string dataToPrint = "";
            datas.ForEach(data =>
            {
               
                //Environment.NewLine provides line break 
                dataToPrint = dataToPrint + data + Environment.NewLine;
            });
            logsTextBox.Text = dataToPrint;
            logsTextBox.ForeColor = Color.Red;
        }

        /// <summary>
        /// To display success messsage , message will be passed as string
        /// </summary>
        /// <param name="message"></param>
        public void displaySuccessMessage(string message)
        {
          
            logsTextBox.Text = message;
            logsTextBox.ForeColor = Color.Green;
        }

        /// <summary>
        /// displaySyntaxInfo
        /// </summary>
        public void displaySyntax()
        {
            logsTextBox.Text = syntaxInfo;
        }


        /// <summary>
        /// When check syntax button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSyntaxBtn_Click(object sender, EventArgs e)
        {
            CommandParser commandParser = new CommandParser();

            //Stores all the error received from checkCommandsSyntax function
            var _errorList = new List<string>();

            string commands = codeTextBox.Text.Trim().ToLower();

            
            if (commands.Length > 0)
            {
                _errorList = commandParser.checkCommandsSyntax(commands);

            }
            else
            {
                //If there are no program commands 
                _errorList.Add("No commands to configure!!");
            }


            if (_errorList.Count > 0)
            {
                //display the errors here
                displayErrorLogs(_errorList);
            }
            else
            {
                displaySuccessMessage("Sucess: Syntax correct! Execute your commands!!");
                //display that everything is good.. its running
            }
        }


        /// <summary>
        /// Paint object for the canvas.. when Refresh is hit, this function is called
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            g= e.Graphics;
            Iterator iterator = shapeCollection.CreateIterator();
            //For all shapes in arraylist , shapesList

            for (Shape shape = iterator.First(); !iterator.IsCompleted; shape = iterator.Next())
            {
                if (shape != null)
                {
                    if (overrideShapeColor )
                    {
                        shape.setColor(drawingCanvas.PenColor,drawingCanvas.FillColor);
                       
                    }
                    if (animateShapes)
                    {
                        shape.shiftOrigin(animateX, animateY);
                        
                    }
                    if (overrideOrigins)
                    {
                        shape.shiftOrigin(shiftX, shiftY);
                    }
                    

                    shape.draw(g);



                }
            }
            pointerLabel.Text = "Pointer ("+drawingCanvas.XCoordinate+","+drawingCanvas.YCoordinate+")";

        }

        /// <summary>
        /// When clearCanvasa is called
        /// </summary>
        public void clearCanvas()
        {

            shapeCollection.clearShapes();
            stopFlash();
            overrideShapeColor = false;
            animateShapes = false;
            overrideOrigins = false;
            animateX = 0;
            animateY = 0;
            shiftX = 0;
            shiftY = 0;
            Refresh();  
        }

        
        /// <summary>
        /// generating flash colour
        /// </summary>
        /// <param name="color1"></param>
        /// <param name="color2"></param>
        public void generateFlash(Color color1,Color color2)
        {
            running = true;
            this.color1 = color1;
            this.color2 = color2;
        }

        /// <summary>
        /// To animate the shapes
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void animateCanvasShapes(int x,int y)
        {
            running = true;
            animateShapes = true;
            this.animateX = x;
            this.animateY = y;
        }

        /// <summary>
        /// shift the shapesfrom canvas
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void shiftCanvasShapes(int x, int y)
        {
           
            overrideOrigins = true;
            this.shiftX = x;
            this.shiftY = y;
        }

        /// <summary>
        /// Command line arg defined to stop the thread running
        /// </summary>
        public void stopFlash()
        {
            running = false;
           // newThread.Abort();
        }

        /// <summary>
        /// Resets the canvas
        /// </summary>
        public void resetCanvas()
        {
            clearCanvas();
            drawingCanvas.reInitialiseCursor(0,0);
        }


        /// <summary>
        /// Change colour on the command "colour"
        /// </summary>
        /// <param name="newColor"></param>
        public void changeColor(Color newColor)
        {
            //drawingCanvas.FillColor = newColor;
            if (drawingCanvas.IsFillModeOn)
            {
                drawingCanvas.FillColor = newColor;
            }
            else
            {
                drawingCanvas.PenColor = newColor;
            }
            
         
            if (InvokeRequired)
            {
                try
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        Refresh();
                        //Your code here, like set text box content or get text box contents etc..
                    }));
                }
                catch (Exception)
                {

                }
               
            }
            else
            {
                Refresh();
            }
            
            //this.codeTextBox
        }

        /// <summary>
        /// When save command is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string fileName = @"C:\\Component1\commands.txt";

            if (codeTextBox.Text.Trim().Length > 0)
            {
                try
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();

                    saveFileDialog.Title = "Save the code for future use";

                    saveFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

                    saveFileDialog.FilterIndex = 2;
                    saveFileDialog.RestoreDirectory = true;
                    saveFileDialog.DefaultExt = "txt";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        using (Stream s = File.Open(saveFileDialog.FileName, FileMode.Create))
                        using (StreamWriter sw = new StreamWriter(s))
                        {
                            sw.Write(codeTextBox.Text);
                        }

                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.ToString());
                }
               
            }
            else
            {
                MessageBox.Show("There was nothing to save!!");
              
            }
           
        }

        /// <summary>
        /// Load the file to commandBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //Checking if user saved files has b een saved previously 

            if(openFileDialog.ShowDialog()==System.Windows.Forms.DialogResult.OK && openFileDialog.CheckFileExists)
            {
                try
                {
                    string file = openFileDialog.FileName;
                    string codeRead = File.ReadAllText(file);
                    codeTextBox.Text = codeRead;
                    MessageBox.Show("Your Code has been loaded!!");
                }
                catch(Exception err)
                {
                    MessageBox.Show(err.ToString());
                }
                
            }
            
            
        }


        /// <summary>
        /// The about menu is called
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The Program presents interface for a user to draw shapes according to his/her requirement. \n\n \t \t\t By- Anjan Poudel");
        }

        
        /// <summary>
        /// Info button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            MessageBox.Show(syntaxInfo);
        }



        //
        /// <summary>
        /// calls  When enter is pressed on the command line
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void commandLine_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==(char)13)
            {
                runExecuteCommand();
            }
        }

       
    }
}



