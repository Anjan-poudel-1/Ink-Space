using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Component1Assignment
{

    
    /// <summary>
    /// this is the class created to check the syntax and run commands 
    /// </summary>
    public class CommandParser
    {
        
        //Valid commands and required variables are defined here
        string[] validCommandsForCmdArgs = new string[] { "run", "clear", "reset","stop"};
        string[] validShapes = new string[] { "circle", "square", "rectangle", "triangle" };
        string[] validCommandsForProgram=  new string[] { "moveto", "pen", "fill", "drawto","colour","shift","animate"};
        string[] validColors = new string[] { "red", "yellow", "green", "blue", "black", "gray","white" };
        string[] validFlashColour = new string[] { "redgreen", "blueyellow", "blackwhite" };
        char[] validMathematicOperations = new char[] { '+', '-', '/', '*' };
  
        bool skipCommandsForIf = false;
        bool skipCommandsForWhile = false;
        string preservedWhileCondition = "";
        string preservedWhileCommands = "";

        
        string savedMethodDefination = "";
        string runningMethod = "";
        bool skipForMethodExecution = false;

        string[] conditionalOperators = { "==", "!=",">=","<=", ">", "<" };
        string[] specialCommands = { "if", "method", "while","endif","endwhile","endmethod" };

        //Dictionary to keep track of commands if,method,while
        Dictionary<string,int> numberOfCommandsAndClosure = new Dictionary<string, int> { {"if", 0},{ "endif",0 },{"method",0 },{"endmethod",0 },{ "while", 0 }, { "endwhile",0} };
        Dictionary<string, string> customMethodDefination = new Dictionary<string, string>();
        Dictionary<string, string[]> customMethodParameters = new Dictionary<string, string[]>();
       
        //List of errors that might be genereated
    
        List<string> listOfErrors ;
        List<string> listOfCmdArgsErrors;
        List<string> listOfCommandErrors;

        Dictionary<string, string> assignmentPair = new Dictionary<string, string>();

        //Canvas and form elements will be provided in the parameter
        DrawingCanvas _canvas;
        DrawShapes _form;
        ConcreteCollection shapeCollection;


        public CommandParser()
    {
            //Initiating variables 
        listOfErrors = new List<string>();
        listOfCmdArgsErrors = new List<string>();
        listOfCommandErrors = new List<string>();

        }




        /// <summary>
        /// the function is called when execute button is clicked. It executes the command
        /// </summary>
        /// <param name="commandLineText"></param>
        /// <param name="commands"></param>
        /// <param name="drawingCanvas"></param>
        /// <param name="form"></param>
        /// <param name="collection"></param>
        public void  executeCommands(string commandLineText,string commands,DrawingCanvas drawingCanvas,DrawShapes form, ConcreteCollection collection)
        {
            //Storing the list of errors here

            _canvas = drawingCanvas;
            _form  = form;
            shapeCollection = collection;


                //check if the command written in command line are valid...
                listOfCmdArgsErrors = checkCommandLineSyntax(commandLineText);

                
                if (listOfCmdArgsErrors.Count == 0)
                {
                //if there are no error in command line interface.. execute the command line code

                
                    executeCliCommand(commandLineText, commands);
               
                     
                }
                else
                {
                    //add all errors to listoferrors list
                    listOfErrors.AddRange(listOfCmdArgsErrors);
                }

            
            
            if (listOfErrors.Count > 0)
            {
                form.displayErrorLogs(listOfErrors);
            }
            else
            {
              
                form.displaySuccessMessage("Program executed");
           
            }
           
        }


        /// <summary>
        /// Function to execute the command line arguments
        /// </summary>
        /// <param name="_cliCommand"></param>
        /// <param name="programCommands"></param>
        public void executeCliCommand(string _cliCommand, string programCommands)
        {
            _cliCommand = _cliCommand.ToUpper();
           
           
            //execute run function...
            if (_cliCommand.Equals("RUN"))
            {
                assignmentPair.Clear();
                _form.clearCanvas();

                //Gets the list of errors in program command
                listOfCommandErrors = checkCommandsSyntax(programCommands);

                //If there are errors in program command
                if (listOfCommandErrors.Count > 0)
                {
                    //If there are errors in program command, it adds the range to ListOfErrors
                    listOfErrors.AddRange(listOfCommandErrors);
                }
                else
                {
                    //If there are no errors in program command

                    try
                    {
                        executeRunFunction(programCommands);
                    }catch(Exception err)
                    {
                        MessageBox.Show(err.ToString());
                    }
                   
                }
                
            }
            else if (_cliCommand.Equals("CLEAR"))
            {
                //Execute clear function

                executeClearFunction();

            }
            else if (_cliCommand.Equals("RESET"))
            {
                //Execute Reset function
                executeResetFunction();
            }
            else if (_cliCommand.Equals("RESET"))
            {
                _form.stopFlash();
            }
            
        }


        /// <summary>
        /// If the command is "run" in command line interface, executeRunFunction is executed
        /// </summary>
        /// <param name="programcommands"></param>
        private void executeRunFunction(string programcommands)
        {
            _form.stopFlash();
            //It splits the commands and returns commands per line
            string[] commandsPerLineArr = (programcommands).Split('\n');
          
            
            ShapeFactory factory = new ShapeFactory();
           

            //looking at individual line of program commands
            foreach (string line in commandsPerLineArr)
            {
                //Getting individual parameter from line
                string[] individualCommand = line.Trim().Split(' ');
                string[] assignmentCommand = line.Trim().Split('=');
                string[] executeMethodCommand = line.Trim().Split('(');



                if (line.Trim().Length > 0)
                {

                    Shape s = null;
                    if (skipForMethodExecution && !(individualCommand[0].Trim()=="endmethod"))
                    {
                        Console.WriteLine("-----------------------------------------");
                        Console.WriteLine("Do nothing");
                        Console.WriteLine("-----------------------------------------");
                    }
                    else if (specialCommands.Contains(individualCommand[0]))
                    {

                        //run special commands
                        if (individualCommand[0].Trim() == "if" && !skipCommandsForIf && !skipCommandsForWhile)
                        {
                            if (individualCommand.Contains("then"))
                            {

                                //it is a single line command
                                string[] thenArr = { "then" };
                                string conditionalStatementWithThen = line.Trim().Substring(2).Trim();
                                string[] splitConditionalStatementWithThen = conditionalStatementWithThen.Split(thenArr, System.StringSplitOptions.RemoveEmptyEntries);
                                if (isConditionTrue(splitConditionalStatementWithThen[0]))
                                {
                                    Console.WriteLine(splitConditionalStatementWithThen[1]);
                                     executeRunFunction(splitConditionalStatementWithThen[1].Trim());
                                    
                                }

                            }
                            else
                            {
                                //for multiline
                                string conditionalStatement = line.Trim().Substring(2).Trim();

                                if (!isConditionTrue(conditionalStatement))
                                {
                                    skipCommandsForIf = true;
                                }

                                //start 


                            }
                        }
                        else if (individualCommand[0].Trim() == "endif")
                        {

                            skipCommandsForIf = false;

                        }
                        else if(individualCommand[0].Trim() == "while" && !skipCommandsForIf && !skipCommandsForWhile)
                        {
                            skipCommandsForWhile = true;
                            string conditionalStatement = line.Substring(5).Trim();
                            preservedWhileCondition = conditionalStatement;
                        }
                        else if (individualCommand[0].Trim() == "endwhile")
                        {
                           int numberOfLoopCount = 0;
                            skipCommandsForWhile = false;
                            while (isConditionTrue(preservedWhileCondition))
                                {
                                numberOfLoopCount++;
                                executeRunFunction(preservedWhileCommands);

                                if (numberOfLoopCount > 25)
                                {
                                    throw (new ExcessLoopException("The loop is running multiple time.Limit it to max of 25."));
                                    return;
                                }
                           
                                }
                            
                                preservedWhileCondition = "";
                                
                          
                        }

                        //here for method command

                        else if(individualCommand[0].Trim() == "method")
                        {

                            //skip all the lines that come after... cause we have already saved them... 
                            skipForMethodExecution = true;

                        }
                        else if (individualCommand[0].Trim() == "endmethod")
                        {
                            //stop skipping the commands
                            skipForMethodExecution = false;

                        }


                    }
                    else if (skipCommandsForWhile)
                    {
                        preservedWhileCommands = preservedWhileCommands + line + "\n";
                    }
                    else if (assignmentCommand.Length == 2)
                    {
                        checkAssignmentErrors(assignmentCommand, 0);
                    }
                    else if (executeMethodCommand.Length ==2 && !individualCommand.Contains("method"))
                    {
                        string methodName = executeMethodCommand[0].Trim();

                        Console.WriteLine("Reached method execution here......." + methodName);
                        if (isDefinedMethod(methodName))
                        {
                            Console.WriteLine("Reached method defination check  here......." + methodName);
                            string[]  methodParametersDefined = customMethodParameters[methodName];
                            string[]  methodParametersvalue = executeMethodCommand[1].Trim().Remove(executeMethodCommand[1].Length - 1, 1).Trim().Split(',');

                            //We are writing a command, where we will be assigning the value to parameters
                            string assignmentParametersString = "";
                            //assigning each variables to the parametrs
                            for(int i = 0; i < methodParametersDefined.Length; i++)
                            {
                                assignmentParametersString = assignmentParametersString + methodParametersDefined[i].Trim() +  " = " + methodParametersvalue[i] + "\n";
                            }
                            string methodDefinations = assignmentParametersString + customMethodDefination[methodName];
                            Console.WriteLine("-----------------------------------------");
                            Console.WriteLine(methodDefinations);
                            Console.WriteLine("-----------------------------------------");

                            //We will run the commands that are present on method....
                            CommandParser functionCommandParser = new CommandParser();
                             functionCommandParser.executeCommands("run",methodDefinations,_canvas,_form,shapeCollection);

                       
                        }

                    }
                    else
                    {
                        if (!skipCommandsForIf && !skipCommandsForWhile)
                        {
                            //run commands
                            string[] commandParameters = individualCommand[1].Split(',');

                            //The first string in commandline is command .. eg.. 'moveto','circle'...
                            string _commandName = individualCommand[0];


                            if (_commandName.Equals("circle"))
                            {
                                s = factory.getShape("circle");
                                string value = returnValueIfIsVariable(commandParameters[0]);
                                s.set(_canvas, Int16.Parse(value));
                            }

                            
                            else if (_commandName.Equals("square"))
                            {
                                s = factory.getShape("rectangle");
                                string value = returnValueIfIsVariable(commandParameters[0]);
                                s.set(_canvas, Int16.Parse(value), Int16.Parse(value));
                            }


                            else if (_commandName.Equals("triangle"))
                            {
                                s = factory.getShape("triangle");

                                //For triangle 2 points should be provided... get x1,y1 x2,y2
                                string[] group1 = individualCommand[1].Split(',');
                                string[] group2 = individualCommand[2].Split(',');
                                int x1 = Int16.Parse(returnValueIfIsVariable(group1[0]));
                                int y1 = Int16.Parse(returnValueIfIsVariable(group1[1]));
                                int x2 = Int16.Parse(returnValueIfIsVariable(group2[0]));
                                int y2 = Int16.Parse(returnValueIfIsVariable(group2[1]));

                                s.set(_canvas, x1, y1, x2, y2);
                            }


                            else if (_commandName.Equals("rectangle"))
                            {
                                s = factory.getShape("rectangle");
                                string value1 = returnValueIfIsVariable(commandParameters[0]);
                                string value2 = returnValueIfIsVariable(commandParameters[1]);
                                s.set(_canvas, Int16.Parse(value1), Int16.Parse(value2));
                            }


                            else if (_commandName.Equals("moveto"))
                            {
                                string value1 = returnValueIfIsVariable(commandParameters[0]);
                                string value2 = returnValueIfIsVariable(commandParameters[1]);
                                _canvas.XCoordinate = Int16.Parse(value1);
                                _canvas.YCoordinate = Int16.Parse(value2);
                            }


                            else if (_commandName.Equals("drawto"))
                            {
                                //For drawTo.. Line is built and the next initial point is set to  end point of current line

                                s = factory.getShape("line");
                                string value1 = returnValueIfIsVariable(commandParameters[0]);
                                string value2 = returnValueIfIsVariable(commandParameters[1]);
                                int x = Int16.Parse(value1);
                                int y = Int16.Parse(value2);
                                s.set(_canvas, x, y);

                                //Initialising the cursor after line is constructed
                                _canvas.reInitialiseCursor(x, y);

                            }


                            else if (_commandName.Equals("fill"))
                            {
                                string value = returnValueIfIsVariable(commandParameters[0]);
                                if (value == "on")
                                {
                                    //if fill mode is turned on 
                                    if (!_canvas.IsFillModeOn)
                                    {
                                        //Fill mode is turned on 
                                        _canvas.IsFillModeOn = true;
                                        _canvas.FillColor = _canvas.PenColor;
                                        _canvas.PenColor = Color.Transparent;
                                    }
                                }
                                else if (value == "off")
                                {

                                    //If fill mode is turned off
                                    if (_canvas.IsFillModeOn)
                                    {
                                        //not to let to turn on multiple times 
                                        if (_canvas.FillColor != Color.Transparent)
                                        {
                                            _canvas.PenColor = _canvas.FillColor;
                                        }

                                        _canvas.FillColor = Color.Transparent;
                                        _canvas.IsFillModeOn = false;
                                    }


                                }
                            }


                            else if (_commandName.Equals("pen"))

                            {
                                string value = returnValueIfIsVariable(commandParameters[0]);
                                //Fetch the color required form parameter
                                Color _newColor = IdentifyColor(value);

                                //Toggle pencolor and fillcolor
                                if (_canvas.IsFillModeOn)
                                {
                                    _canvas.FillColor = _newColor;
                                }
                                else
                                {
                                    _canvas.PenColor = _newColor;
                                }

                            }

                            else if (_commandName.Equals("colour"))
                            {
                                _form.changeColor(Color.Green);
                                string value1 = returnValueIfIsVariable(commandParameters[0]);
                                Color color1= Color.Transparent, color2 =Color.Transparent;
                                if(value1 == "blueyellow")
                                {
                                    color1 = Color.Blue;
                                    color2 = Color.Yellow;

                                }
                               else if (value1 == "redgreen")
                                {
                                    color1 = Color.Red;
                                    color2 = Color.Green;

                                }
                               else if (value1 == "blackwhite")
                                {
                                    color1 = Color.Black;
                                    color2 = Color.White;
                                }
                                _form.OverrideShapeColor = true;
                                _form.generateFlash(color1, color2);
                            }

                            else if (_commandName.Equals("animate"))
                            {
                                string value1 = returnValueIfIsVariable(commandParameters[0]);
                                string value2 = returnValueIfIsVariable(commandParameters[1]);
                                _form.animateCanvasShapes(int.Parse(value1), int.Parse(value2));

                            }

                            else if (_commandName.Equals("shift"))
                            {
                                string value1 = returnValueIfIsVariable(commandParameters[0]);
                                string value2 = returnValueIfIsVariable(commandParameters[1]);
                                _form.shiftCanvasShapes(int.Parse(value1), int.Parse(value2));
                            }
                        }
                        
                    }

                    //If shapes is available ... 
                    if (s != null)
                    {
                        shapeCollection.AddShape(s);
                    }
                }
            }

        }


        /// <summary>
        ///  If the command is "clear" in command line interface, executeClearFunction is executed. Clears the canvas by erasing shape
        /// </summary>
        private void executeClearFunction()
        {
            _form.clearCanvas();
            _form.stopFlash();
        }

        
        /// <summary>
        /// Reset function uses  clear function.. then resets the pointer to 0,0
        /// </summary>
        private void executeResetFunction()
        {
            _form.resetCanvas();
        }


        /// <summary>
        /// Function to return value after mathematical operration
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <param name="mathOperator"></param>
        /// <returns> finalValue</returns>
        public int performMathematicalOperation(int num1, int num2, char mathOperator)
        {
            int finalResult = 0;

            try
            {
                switch (mathOperator)
                {
                    case '+':
                        finalResult = num1 + num2;
                        break;
                    case '-':
                        finalResult = num1 - num2;
                        break;
                    case '*':
                        finalResult = num1 * num2;
                        break;
                    case '/':
                        finalResult = num1 / num2;
                        break;
                    default:
                        finalResult = 0;
                        break;

                }
            }
            catch(Exception err)
            {
                MessageBox.Show(err.ToString());
            }
           

            return finalResult;
        }



        /// <summary>
        /// Assigns value to  the key provided
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void assignVariable(string key, string value)
        {
            try
            {
                assignmentPair.Add(key, value);
            }
            catch (ArgumentException)
            {
                assignmentPair.Remove(key);
                assignmentPair.Add(key, value);
            }
        }


       
        /// <summary>
        /// To check command line syntax
        /// </summary>
        /// <param name="commandLineText"></param>
        /// <returns>Error if there are any</returns>
        public List<string> checkCommandLineSyntax(string commandLineText)
        {
    
        var _errors = new List<string>();
          if (commandLineText.Length > 0)
          {
             string[] commands = commandLineText.Split(' ');
            //Commands are always of length 1
             if (commands.Length == 1)
             {
                if (validCommandsForCmdArgs.Contains(commands[0]))
                {
                    Console.WriteLine(commands[0]);
                }
                else
                {
                    _errors.Add("Invalid Command Line Argument.");
                }

             }
             else
             {
                _errors.Add("Invalid Command Line Argument.");
             }
          }
          else
          {

                _errors.Add("Command Line Arguments Missing");
                
        }

            return _errors;
        }

        /// <summary>
        /// Checks the syntax of command
        /// </summary>
        /// <param name="commands"></param>
        /// <returns>Error if there are any</returns>
        public List<string> checkCommandsSyntax(string commands)
        {

            var _errors = new List<string>();

            if (commands.Trim().Length > 0)
            {
                string[] commandsPerLineArr = commands.Split('\n');
                int lineIteration = 0;

                foreach (string line in commandsPerLineArr)
                {
                    lineIteration++;
                    if (line.Trim().Length > 0)
                    {
                        string[] splitLineBySpace = line.Trim().Split(' ');
                        string[] splitLineByAssignment = line.Trim().Split('=');
                        string[] splitLineByOpenMethod = line.Trim().Split('(');

                        List<string> commandsError = new List<string>();

                        if (runningMethod.Length > 0 && !splitLineBySpace.Contains("endmethod"))
                        {
                            savedMethodDefination = savedMethodDefination + line + '\n';
                        }


                        if (splitLineBySpace.Contains("if"))
                        {
                            commandsError = checkConditionalErrors(line.Trim(), lineIteration);

                        }
                        else if (splitLineBySpace.Contains("endif"))
                        {
                            if (splitLineBySpace.Length > 1)
                            {
                                _errors.Add("Line " + lineIteration + ": Invalid endif statement");
                            }
                            else
                            {
                                numberOfCommandsAndClosure["endif"]++;
                            }
                        }
                        else if (splitLineBySpace.Contains("while"))
                        {
                            commandsError = checkLoopErrors(line.Trim(), lineIteration);
                        }
                        else if (splitLineBySpace.Contains("endwhile"))
                        {
                            if (splitLineBySpace.Length > 1)
                            {
                                _errors.Add("Line " + lineIteration + ": Invalid endwhile statement");
                            }
                            else
                            {
                                numberOfCommandsAndClosure["endwhile"]++;
                            }
                        }
                        else if (splitLineBySpace.Contains("method"))
                        {
                            if (splitLineBySpace[0] != "method")
                            {
                                //error saying invalid method
                                _errors.Add("Line " + lineIteration + ":Invalid method declaration ");
                            }
                            else
                            {
                                string lineWithoutMethod = line.Trim().Remove(0, 6).Trim();
                                Console.WriteLine("lineWithoutMethod" + lineWithoutMethod);
                                //the lineWithoutMethod should have 1-1 "(" and ")"
                                string[] lineSplittedByOpenParanthesis = lineWithoutMethod.Split('(');
                                string[] lineSplittedByCloseParanthesis = lineWithoutMethod.Split(')');


                                //there must be 2 values after split by "("

                                if (lineSplittedByOpenParanthesis.Length == 2 && lineSplittedByCloseParanthesis.Length == 2 && lineSplittedByCloseParanthesis[1].Trim() == "")
                                {
                                    //the first index must be a valid variable..and variable not used in assignment
                                    string error = validateVariable(lineSplittedByOpenParanthesis[0].Trim(), lineIteration);

                                    if (error.Length > 0)
                                    {
                                        _errors.Add(error);
                                    }
                                    else
                                    {
                                        string tempMethodNameSave = lineSplittedByOpenParanthesis[0].Trim();
                                        if (isDefinedMethod(tempMethodNameSave))
                                        {
                                            _errors.Add("Line " + lineIteration + ": Method " + tempMethodNameSave + " already defined");
                                        }
                                        else
                                        {
                                            string parametersInString = lineSplittedByOpenParanthesis[1].Trim().Remove(lineSplittedByOpenParanthesis[1].Length - 1, 1).Trim();

                                            if (parametersInString.Length == 0)
                                            {
                                                //valid method with no parameter

                                                //customMethodDefination.Add(tempMethodNameSave, "");
                                                runningMethod = tempMethodNameSave;
                                                string[] parameters = { };
                                                customMethodParameters.Add(tempMethodNameSave, parameters);
                                                numberOfCommandsAndClosure["method"]++;
                                                Console.WriteLine("valid method with no parameter");
                                            }
                                            else
                                            {
                                                string[] parameters = parametersInString.Split(',');
                                                int errorCount = 0;


                                                for (int i = 0; i < parameters.Length; i++)
                                                {
                                                    int duplicateCount = 0;
                                                    for (int j = i; j < parameters.Length; j++)
                                                    {
                                                        if (parameters[j] == parameters[i] && j != i)
                                                        {
                                                            _errors.Add("Line " + lineIteration + ": Duplicate variable '" + parameters[i] + "' for method");
                                                            errorCount++;
                                                            break;
                                                        }
                                                        Console.WriteLine("parameter " + parameters[i]);
                                                        string parameterError = validateVariable(parameters[i], lineIteration);

                                                        if (parameterError.Length > 0)
                                                        {
                                                            _errors.Add(parameterError);
                                                            errorCount++;
                                                        }
                                                    }

                                                }
                                                if (errorCount == 0)
                                                {
                                                    //valid methods with parameter

                                                    // customMethodDefination.Add(tempMethodNameSave, "");
                                                    customMethodParameters.Add(tempMethodNameSave, parameters);
                                                    runningMethod = tempMethodNameSave;
                                                    numberOfCommandsAndClosure["method"]++;

                                                }
                                            }

                                        }
                                    }

                                }
                                else
                                {

                                    _errors.Add("Line " + lineIteration + ":Invalid method declaration. ");  //errors saying properly enclose the parameters
                                }
                            }
                            //should have a valid methodName
                        }

                        else if (splitLineBySpace.Contains("endmethod"))
                        {
                            //should only have endMethod.... nothing more
                            //saveFormethod = false


                            //We ned to delete the last line cause it contains endmethod
                            savedMethodDefination = String.Join("\n", savedMethodDefination.Split('\n').Take(savedMethodDefination.Split('\n').Length - 1).ToArray());
                            customMethodDefination.Add(runningMethod, savedMethodDefination);
                            numberOfCommandsAndClosure["endmethod"]++;
                            runningMethod = "";
                        }
                        else if (splitLineByOpenMethod.Length >= 2)
                        {
                            if (splitLineByOpenMethod.Length == 2)
                            {
                                string methodName = splitLineByOpenMethod[0].Trim();
                                string tempParameters = splitLineByOpenMethod[1].Trim();
                                //check if it is a method.... 
                                if (isDefinedMethod(methodName))
                                {
                                    //check its parameters
                                    string methodParameters = tempParameters.Remove(tempParameters.Length - 1, 1);
                                    string[] listOfMethodParameters = methodParameters.Split(',');
                                    string[] listOfParametersDefinedForMethod = customMethodParameters[methodName];

                                    //if listofparameters ==number of parameters from dictionary...... we are all good to go
                                    if (listOfMethodParameters.Length == listOfParametersDefinedForMethod.Length)
                                    {
                                        foreach (string parameter in listOfMethodParameters)
                                        {
                                            if (parameter.Length == 0 && listOfMethodParameters.Length >= 1)
                                            {
                                                commandsError.Add("Line " + lineIteration + ": " + "Empty parametr was passed");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        commandsError.Add("Line " + lineIteration + ": " + "Method has invalid number of parameters");
                                    }
                                }
                                else
                                {
                                    commandsError.Add("Line " + lineIteration + ": " + methodName + " is not a method");
                                }
                            }
                            else
                            {
                                commandsError.Add("Line " + lineIteration + ": Invalid method call");
                            }
                        }

                        else if (splitLineByAssignment.Length > 1)
                        {

                            //These are assignment....

                            commandsError = checkAssignmentErrors(splitLineByAssignment, lineIteration);
                        }
                        else
                        {
                            //for commands
                            if (splitLineBySpace[0].Trim()!="//")
                            {
                                commandsError = checkCommandAndItsParameters(splitLineBySpace, lineIteration);
                            }
                           

                        }



                        _errors.AddRange(commandsError);
                    }

                }
                try
                {
                   checkStatementOpeningAndClosure();
                }
                catch(InvalidOpeningAndClosureException err)
                {
                    _errors.Add(err.Message);
                }
                
            }
            else
            {
                _errors.Add("Line 1: No commands to execute");
            }

            return _errors;
        }


        /// <summary>
        /// Checks the syntax of commands to draw
        /// </summary>
        /// <param name="line"></param>
        /// <param name="lineNumber"></param>
        /// <returns>error</returns>
        private List<string> checkCommandAndItsParameters(string[] line, int lineNumber)
        {


            List<string> parametersError = new List<string>();

            //if the first member of the  array is not command... error
            if ((validCommandsForProgram.Contains(line[0])) || (validShapes.Contains(line[0])))
            {
                //we neeed to check parameter list here

                string[] commandsWithOneParam = { "circle", "pen", "fill", "square", "colour" };
                string[] commandsWithTwoParams = { "moveto", "drawto", "rectangle", "shift", "animate" };
                string[] commandsWithMultipleParams = { "triangle" };
                string[] commandsWithStringParam = { "pen", "fill", "colour" };
                string[] commandsWithNumParam = { "moveto", "drawto", "circle", "square", "rectangle", "triangle", "animate", "shift" };

                //Checking number of parameters

                //all have single param except for triangle

                if (line.Length >= 2)
                {
                  
                    string[] commandsSplittedByComma = line[1].Split(',');
                    line[1] = String.Join(" ", line.Skip(1).ToArray()).Trim();

                    if (commandsWithOneParam.Contains(line[0]))
                    {
                        //these are elements with 2 params in total

                        //there should not be ','
                        if (commandsSplittedByComma.Length < 2)
                        {
                            if (line[0] == "pen")
                            {
                                if (validColors.Contains(line[1]))
                                {

                                }
                                else
                                {
                                    if (runningMethod.Length > 0 && customMethodParameters[runningMethod].Contains(line[1].Trim()))
                                    {

                                    }
                                    else
                                    {
                                        string value = getValueFromVariable(line[1]);
                                        if (!validColors.Contains(value))
                                        {
                                            parametersError.Add("Line " + lineNumber + ": " + "Invalid color.Please have a look at valid colors");
                                        }
                                    }


                                }
                            }

                            //Fill has just 2 options ... on or off
                            else if (line[0] == "fill")
                            {
                                if (line[1] == "on" || line[1] == "off")
                                {

                                }
                                else
                                {
                                    if (runningMethod.Length > 0 && customMethodParameters[runningMethod].Contains(line[1].Trim()))
                                    {

                                    }
                                    else
                                    {
                                        string value = getValueFromVariable(line[1].Trim()).Trim();
                                        if (!(value == ("on") || value == ("off")))
                                        {
                                            parametersError.Add("Line " + lineNumber + ": " + "Invalid parameters. Provide On || Off");
                                        }

                                    }


                                }
                            }

                            else if (line[0] == "colour")
                            {
                                if (validFlashColour.Contains(line[1].Trim()))
                                {

                                }
                                else
                                {
                                    if (!validFlashColour.Contains(getValueFromVariable(line[1])))
                                    {
                                        parametersError.Add("Line " + lineNumber + " :" + "Invalid colors provided");
                                    }
                                }

                            }

                        }

                        else
                        {
                            parametersError.Add("Line " + lineNumber + ": " + "Invalid Number of parameters for the given command.");
                        }






                    }
                    else if (commandsWithTwoParams.Contains(line[0]))
                    {
                        //these are elements with 3 params in total

                        if (commandsSplittedByComma.Length != 2)
                        {
                            parametersError.Add("Line " + lineNumber + ": " + "Invalid Number of parameters for the given command.");
                        }
                    }

                    //checking data type of parameters


                    if (commandsWithStringParam.Contains(line[0]))
                    {
                        bool _hasError = false;
                        //only string paramters should be allowed here

                        //run loop through commands.. if number found.. throws error
                        for (int i = 0; i < line.Length; i++)
                        {
                            if (i > 0)
                            {
                                if (runningMethod.Length > 0 && customMethodParameters[runningMethod].Contains(line[i]))
                                {

                                }
                                else
                                {


                                    try
                                    {
                                        int _number = Int16.Parse(line[i]);
                                        //If number is there to parse. there is an error
                                        _hasError = true;
                                    }
                                    catch (Exception e)
                                    {
                                        string value = getValueFromVariable(line[i]);
                                        if (value.Length > 0)
                                        {
                                            if (isNumber(value))
                                            {
                                                _hasError = true;
                                            }
                                        }


                                    }
                                }

                            }
                        }

                        if (_hasError)
                        {
                            parametersError.Add("Line " + lineNumber + ": " + " Invalid parameter. String paramters expected. Numbers provided");
                        }
                        else
                        {




                        }



                    }
                    else if (commandsWithNumParam.Contains(line[0]))
                    {
                        //only number parameters should be allowed here
                        bool _hasError = false;

                        for (int i = 0; i < commandsSplittedByComma.Length; i++)
                        {


                            if (!isNumber(commandsSplittedByComma[i].Trim()) && !(runningMethod.Length > 0 && customMethodParameters[runningMethod].Contains(commandsSplittedByComma[i].Trim())))
                            {
                                //must be a valid variable
                                string value = getValueFromVariable(commandsSplittedByComma[i]);

                                if (value.Length == 0)
                                {
                                    parametersError.Add("Line " + lineNumber + ": " + " Invalid parameter. Variable is not defined");

                                }
                                else
                                {
                                    if (!isNumber(value))
                                    {
                                        parametersError.Add("Line " + lineNumber + ": " + " Invalid parameter. Numeric parameter expected");
                                    }
                                }

                            }






                        }





                    }



                    
                }
                    

                    else
                    {
                    if (line[0].Equals("triangle"))
                    {
                        //the first and second param must have , spearated thinfs.... 

                        string[] group1 = line[1].Split(',');
                        string[] group2 = line[2].Split(',');

                        if (group1.Length == 2 && group2.Length == 2)
                        {
                            //we need to have both splited elements number

                            group1 = group1.Concat(group2).ToArray();

                            for (int i = 0; i < group1.Length; i++)
                            {
                                try
                                {
                                    int num = Int16.Parse(group1[i]);

                                }
                                catch (Exception)
                                {
                                    parametersError.Add("Line " + lineNumber + ": " + " Invalid parameters. Provide numbers separated by ',' as points");
                                    break;
                                }
                            }
                        }
                        else
                        {
                            parametersError.Add("Line " + lineNumber + ": " + " Invalid parameters. Provide numbers separated by ',' as points");
                        }
                    }
                    else
                    {
                        parametersError.Add("Line " + lineNumber + ": " + "Invalid Number of parameters for the given command.");
                    }
                    
                    }



                



            }
            else
            {

                parametersError.Add("Line " + lineNumber + ": " + "Invalid Command");

            }


            return parametersError;

        }

        /// <summary>
        /// Cjecks the error in assigning variables
        /// </summary>
        /// <param name="splitLineByAssignment"></param>
        /// <param name="lineIteration"></param>
        /// <returns></returns>
        public List<string> checkAssignmentErrors(string[] splitLineByAssignment, int lineIteration)
        {
            var _errors = new List<string>();
            if (splitLineByAssignment.Length == 2)
            {
                if ((splitLineByAssignment[0].Trim().Split(' ').Length > 1))
                {
                    _errors.Add("Line " + lineIteration + ": Invalid assignment operation");
                }
                else
                {

                    //the first part is checked
                    string error = validateVariable(splitLineByAssignment[0].Trim(), lineIteration);

                    if (error.Length > 0)
                    {
                        _errors.Add(error);
                    }

                    //to check the second part....


                    else
                    {
                        bool hasOperator = false;
                        char operatorValue = ' ';

                        foreach (char character in splitLineByAssignment[1].Trim())
                        {
                            if (validMathematicOperations.Contains(character))
                            {

                                try
                                {
                                    operatorValue = character;
                                    if (hasOperator)
                                    {

                                        throw new OperationsOverloadedException("Provide only two operands at a time");
                                          
                                    }
                                    hasOperator = true;
                                }
                                catch(OperationsOverloadedException e)
                                {
                                    _errors.Add("Line " + lineIteration + ": "+ e.Message);
                                }
                               

                            }

                        }
                        if (hasOperator)
                        {
                            string[] variableSplittedByOperator = splitLineByAssignment[1].Trim().Split(operatorValue);

                            //if the second part consists of mathemetical operator in it , 
                            //there must be only two 
                            if (variableSplittedByOperator.Length > 2 || variableSplittedByOperator[0].Trim().Length == 0 || variableSplittedByOperator[1].Trim().Length == 0)
                            {
                                _errors.Add("Line " + lineIteration + ": Invalid operands");
                            }
                            else
                            {
                                int num1 = 0;
                                int num2 = 0;
                                int operatedValue;
                                //if there are two, each must be either a number or already declared variable that has a number
                                try
                                {
                                    num1 = int.Parse(variableSplittedByOperator[0].Trim());
                                    num2 = int.Parse(variableSplittedByOperator[1].Trim());

                                    operatedValue = performMathematicalOperation(num1, num2, operatorValue);
                                    assignVariable(splitLineByAssignment[0].Trim(), operatedValue.ToString());
                                }
                                catch (FormatException)
                                {
                                    //either num1 or num2 was not a number
                                    if (!isNumber(variableSplittedByOperator[0].Trim()))
                                    {
                                        string variableValue = getValueFromVariable(variableSplittedByOperator[0].Trim());
                                        if (variableValue.Length > 0)
                                        {
                                            num1 = int.Parse(variableValue);
                                        }
                                        else
                                        {
                                            _errors.Add("Line " + lineIteration + ": " + variableSplittedByOperator[0].Trim() + " is not defined to perform operation.");
                                        }
                                    }

                                    if (!isNumber(variableSplittedByOperator[1].Trim()))
                                    {
                                        string variableValue = getValueFromVariable(variableSplittedByOperator[1].Trim());
                                        if (variableValue.Length > 0)
                                        {
                                            num2 = int.Parse(variableValue);
                                        }
                                        else
                                        {
                                            _errors.Add("Line " + lineIteration + ": " + variableSplittedByOperator[1].Trim() + " is not defined to perform operation.");
                                        }
                                    }
                                    else
                                    {
                                        num2 = int.Parse(variableSplittedByOperator[1].Trim());
                                    }
                                    operatedValue = performMathematicalOperation(num1, num2, operatorValue);

                                    assignVariable(splitLineByAssignment[0].Trim(), operatedValue.ToString());


                                }

                            }
                        }
                        else
                        {
                            //if there are no mathematical operators in the word, 
                            //must be a single word....
                            if (splitLineByAssignment[1].Trim().Split(' ').Length > 1 || splitLineByAssignment[1].Trim().Length == 0)
                            {
                                _errors.Add("Line " + lineIteration + ": Invalid assignment operation");
                            }
                            else
                            {
                                assignVariable(splitLineByAssignment[0].Trim(), splitLineByAssignment[1].Trim());
                            }

                        }
                    }






                }
            }

            else
            {
                _errors.Add("Line " + lineIteration + ": Invalid assignment operation");
            }

            return _errors;

        }

        /// <summary>
        /// Checks errors while working with conditional operators
        /// </summary>
        /// <param name="line"></param>
        /// <param name="lineIteration"></param>
        /// <returns></returns>
        public List<string> checkConditionalErrors(string line, int lineIteration)
        {
            List<string> errors = new List<string>();
            string[] splitLineBySpace = line.Split(' ');
            if (splitLineBySpace[0].Trim().ToLower().Equals("if"))
            {
                //has conditionalstatement removing if
                //to check if it is a single line if command or multi..... 

                if (splitLineBySpace.Contains("then".Trim()))
                {
                    //this is a single line 
                    string conditionalStatementWithThen = line.Substring(2).Trim();
                    string[] thenArr = { "then" };

                    string[] splitConditionalStatementWithThen = conditionalStatementWithThen.Split(thenArr, System.StringSplitOptions.RemoveEmptyEntries);
                    if (splitConditionalStatementWithThen.Length == 2)
                    {
                        string conditionalStatement = splitConditionalStatementWithThen[0].Trim();

                        //the conditionalstatement must return boolean
                        if (isConditionalStatement(conditionalStatement))
                        {
                            //should be a correct statement

                            if (checkCommandAndItsParameters(splitConditionalStatementWithThen[1].Trim().Split(' '), lineIteration).Count > 0)
                            {
                                List<string> newErrors = checkCommandAndItsParameters(splitConditionalStatementWithThen[1].Trim().Split(' '), lineIteration);

                                errors.AddRange(newErrors);
                            }
                        }
                        else
                        {
                            errors.Add("Line " + lineIteration + ": not a valid conditional statement");
                        }
                    }
                    else
                    {
                        errors.Add("Line " + lineIteration + ": Invalid inline if statement");
                    }



                }
                else
                {
                    //this is for multi line if....
                    string conditionalStatement = line.Substring(2).Trim();

                    //the conditionalstatement must return boolean
                    if (isConditionalStatement(conditionalStatement))
                    {
                        numberOfCommandsAndClosure["if"]++;
                    }
                    else
                    {
                        errors.Add("Line " + lineIteration + ": not a conditional statement");
                    }
                }





            }
            else
            {
                errors.Add("Line " + lineIteration + ": Invalid if statement");
            }



            return errors;
        }

        /// <summary>
        /// Checks error while working with loop
        /// </summary>
        /// <param name="line"></param>
        /// <param name="lineIteration"></param>
        /// <returns></returns>
        public List<string> checkLoopErrors(string line,int lineIteration)
        {
            List<string> errors = new List<string>();
            string[] splitLineBySpace = line.Split(' ');

            if (splitLineBySpace.Length >= 2 && splitLineBySpace[0].Equals("while"))
            {
                //removing the while statement from line
                string conditionalStatement = line.Substring(5).Trim();

                if (isConditionalStatement(conditionalStatement))
                {
                    numberOfCommandsAndClosure["while"]++;
                }
                else
                {
                    errors.Add("Line " + lineIteration + ": Invalid conditional statement");
                }
            }
            else
            {
                errors.Add("Line " + lineIteration + ": Invalid while statement");
            }

            return errors;
        }
        
      /// <summary>
      /// This function runs at last, to check if there are correct number of closure statement for opening of if,while,method
      /// </summary>
      /// <returns></returns>
        public void checkStatementOpeningAndClosure()
        {
            List<string> _errors = new List<string>();
            if (numberOfCommandsAndClosure["if"] != numberOfCommandsAndClosure["endif"])
            {
                if (numberOfCommandsAndClosure["if"] > numberOfCommandsAndClosure["endif"])
                {
                    throw new InvalidOpeningAndClosureException("No closure for if statement"); 
                }
                else
                {
                    throw new InvalidOpeningAndClosureException("Invalid Endif provided");
                }

            }
            else if (numberOfCommandsAndClosure["while"] != numberOfCommandsAndClosure["endwhile"])
            {
                if (numberOfCommandsAndClosure["while"] > numberOfCommandsAndClosure["endwhile"])
                {
                    throw new InvalidOpeningAndClosureException("No closure for while command"); 
                }
                else
                {
                    throw new InvalidOpeningAndClosureException("Invalid EndWhile provided"); 
                }

            }
            else if (numberOfCommandsAndClosure["method"] != numberOfCommandsAndClosure["endmethod"])
            {
                if (numberOfCommandsAndClosure["method"] > numberOfCommandsAndClosure["endmethod"])
                {
                    throw new InvalidOpeningAndClosureException("No closure for method command");
                }
                else
                {
                    throw new InvalidOpeningAndClosureException("Invalid EndMethod provided"); 
                }

            }

        }

       

        /// <summary>
        /// Validation of declared variables.
        /// </summary>
        /// <param name="variableName"></param>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public string validateVariable(string variableName, int lineNumber)
        {
            string error = "";

            variableName = variableName.Trim();

            if (variableName.Length > 0)
            {

                //the variables cannot be valid commands
                if (validCommandsForCmdArgs.Contains(variableName.ToLower()) || validColors.Contains(variableName.ToLower()) || validCommandsForProgram.Contains(variableName.ToLower()) || validShapes.Contains(variableName.ToLower()) || specialCommands.Contains(variableName.ToLower()))
                {
                    error = ("Line " + lineNumber + ": " + variableName.ToUpper() + " is a reserved word. Cannot construct variable");
                }
                else
                {
                    //If it is not a reserved word 

                    //split the word into each letter
                    int letterCount = 0;
                    foreach (char character in variableName)
                    {
                        //firstletter must be alphabet or _
                        letterCount++;
                        try
                        {
                            int characterAscii = (int)character;
                            if (letterCount == 1)
                            {
                                //the first letter 
                                if ((characterAscii > 96 && characterAscii <= 122) || (characterAscii >= 65 && characterAscii <= 90) || characterAscii == 95)
                                {

                                }
                                else
                                {
                                    throw new InvalidVariableException("Line " + lineNumber + ": " + variableName + " is not a valid variable");
                                }
                            }
                            else
                            {
                                //otherletters
                                if ((characterAscii >= 48 && characterAscii <= 57) || (characterAscii > 96 && characterAscii <= 122) || (characterAscii >= 65 && characterAscii <= 90) || characterAscii == 95)
                                {

                                }
                                else
                                {

                                    throw new InvalidVariableException("Line " + lineNumber + ": " + variableName + " is not a valid variable");
                                }

                            }

                        }
                        catch (InvalidVariableException err)
                        {
                            error = (err.Message);
                        }



                    }


                }
            }
            else
            {
                error = ("Line " + lineNumber + ": Empty variable assigned");
            }

            return error;
        }

        /// <summary>
        /// Fetch value from stored variable
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string getValueFromVariable(string data)
        {
            string returnVal = "";
            try
            {
                returnVal = assignmentPair[data];
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine("Key  is not found.");
            }

            return returnVal;
        }

        /// <summary>
        /// Returns value of the variable if there is any, else returns the same string that was provided
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private string returnValueIfIsVariable(string data)
        {
            string value = getValueFromVariable(data);
            value = value.Length > 0 ? value : data;

            return value;
        }


        /// <summary>
        /// Checks if the provided condition is valid conditional statemnent
        /// </summary>
        /// <param name="statement"></param>
        /// <returns></returns>
        public bool isConditionalStatement(string statement)
        {
            bool toReturn = false;
            statement = statement.Trim();
            //check is type boolean or the value it stoores
            if (isBoolean(statement) || isBoolean(getValueFromVariable(statement)))
            {
                toReturn = true;
            }
            //else if it is a variable
            else if (statement.Split(conditionalOperators, System.StringSplitOptions.RemoveEmptyEntries).Length == 2)
            {
                string firstOperand = statement.Split(conditionalOperators, System.StringSplitOptions.RemoveEmptyEntries)[0].Trim();
                string secondOperand = statement.Split(conditionalOperators, System.StringSplitOptions.RemoveEmptyEntries)[1].Trim();



                if (isNumber(returnValueIfIsVariable(firstOperand)))
                {
                    if (isNumber(returnValueIfIsVariable(secondOperand)))
                    {
                        toReturn = true;
                    }
                }
                else
                {
                    if (!isNumber(returnValueIfIsVariable(secondOperand)))
                    {
                        toReturn = true;
                    }
                }




            }
            else
            {
                toReturn = false;
            }

            return toReturn;
        }


        /// <summary>
        /// Checks if the conditional value is true
        /// </summary>
        /// <param name="statement"></param>
        /// <returns></returns>
        public bool isConditionTrue(string statement)
        {
            bool toReturn = false;
            statement = statement.Trim();
            //if it is directly a boolean
            try
            {
                toReturn = bool.Parse(statement);
            }
            catch (FormatException)
            {
                //is a variable?
                if (isBoolean(getValueFromVariable(statement)))
                {
                    toReturn = bool.Parse(getValueFromVariable(statement));
                }
                else
                {
                    string firstOperand = statement.Split(conditionalOperators, System.StringSplitOptions.RemoveEmptyEntries)[0].Trim();

                    string secondOperand = statement.Split(conditionalOperators, System.StringSplitOptions.RemoveEmptyEntries)[1].Trim();

                    if (!isNumber(returnValueIfIsVariable(firstOperand)))
                    {
                        if (statement.Contains("==") && firstOperand.Equals(secondOperand))
                        {
                            toReturn = true;
                        }

                    }
                    else
                    {
                        int first = int.Parse(returnValueIfIsVariable(firstOperand));
                        int second = int.Parse(returnValueIfIsVariable(secondOperand));
                        if (statement.Contains("==") && first == second)
                        {
                            toReturn = true;

                        }
                        else if (statement.Contains("!=") && first != second)
                        {
                            toReturn = true;
                        }
                        else if (statement.Contains(">=") && first >= second)
                        {
                            toReturn = true;
                        }
                        else if (statement.Contains("<=") && first <= second)
                        {
                            toReturn = true;
                        }
                        else if (statement.Contains(">") && first > second)
                        {
                            toReturn = true;
                        }
                        else if (statement.Contains("<") && first < second)
                        {
                            toReturn = true;
                        }


                    }




                }
            }

            return toReturn;
        }


        /// <summary>
        /// Returns true if the value can be parsed to number
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool isNumber(string data)
        {
            bool returnVal = false;
            
            try{
                int num = int.Parse(data);
                returnVal = true;
            }
            catch (FormatException)
            {

            }

            return returnVal;
        }


        /// <summary>
        /// returs true if value can be parsed to boolean
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool isBoolean(string data)
        {

            bool returnVal = false;
            try
            {
                bool value = bool.Parse(data);
                returnVal = true;
            }
            catch (FormatException)
            {

            }

            return returnVal;

        }
  
      /// <summary>
      /// Checks if the methos is already defined
      /// </summary>
      /// <param name="methodName"></param>
      /// <returns></returns>
        public bool isDefinedMethod(string methodName)
        {
            bool toReturn = false;

            try
            {
                string methoddef = customMethodDefination[methodName];
                toReturn = true;
            }
            catch (Exception)
            {

            }

            return toReturn;
        }




        /// <summary>
        /// Function is used to return color by identifying it 
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        public Color IdentifyColor(string _name)
        {
            Color colorToReturn;

            if (_name.Equals("red"))
            {
               colorToReturn = Color.Red;
            }
            else if (_name.Equals("blue"))
            {
                colorToReturn = Color.Blue;
            }
            else if (_name.Equals("green"))
            {
                colorToReturn = Color.Green;
            }
            else if (_name.Equals("gray"))
            {
                colorToReturn = Color.Gray;
            }
            else if (_name.Equals("yellow"))
            {
                colorToReturn = Color.Yellow;
            }
            else if (_name.Equals("white"))
            {
                colorToReturn = Color.White;
            }
            else
            {
                colorToReturn = Color.Black;
            }

            return colorToReturn;
        }
    }
}
