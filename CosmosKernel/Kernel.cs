using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using Cosmos.Hardware;

namespace CosmosKernel1
{
    public class Kernel : Sys.Kernel
    {
        public List<File> documents { get; set; }
        public Directory homeDir { get; set; }
        public Directory currentDir { get; set; }
        public Variable variables { get; set; }

        protected override void BeforeRun()
        {
            homeDir = new Directory("C://", null);
            currentDir = homeDir;
            documents = new List<File>();
            variables = new Variable();
            Console.WriteLine("Cosmos booted successfully. \nPlease enter a command or type 'help' for more options.");


        }
        protected void menuSelection(string input, bool isBatch)
        {

            string prefix = input.Split(' ')[0];
            string action;

            switch (prefix)
            {
                case "help":
                    Console.WriteLine("===================================================================");
                    Console.WriteLine("You have selected help. The usable commands are as follows:");
                    Console.WriteLine("     help      - You're using it! come on, this is serious business :)");
                    Console.WriteLine("     time      - Gives the current system time");
                    Console.WriteLine("     date      - Gives you the current system date");
                    Console.WriteLine("     dir       - lists files in the current directory");
                    Console.WriteLine("     run       - runs the appropriate BATCH file USE <filename>.<bat>");
                    Console.WriteLine("     create    - creates the appropriate file USE <filename>.<ext>");
                    Console.WriteLine("===================================================================");
                    break;

                case "date":
                    PrintDate();
                    break;
                case "time":
                    PrintTime();
                    break;
                case "dir":
                    PrintDirContents();
                    break;
                case "create":
                    if (!isBatch)
                    { 
                    action = input.Split(' ')[1];
                    string filename = action.Split('.')[0];
                    string ext = action.Split('.')[1];
                    File newFile = new File(filename, ext);
                    if (ext == "txt")
                    {
                        string line = "";
                        int count = 0;
                        while (line != "save")
                        {
                            Console.Write(count + ". ");
                            line = Console.ReadLine();
                            if (line != "save")
                                newFile.addLine(line);
                            count++;
                        }
                    }
                    else if (ext == "bat")
                    {
                        string line = "";
                        int count = 0;
                        while (line != "save")
                        {
                            Console.Write(count + ". ");
                            line = Console.ReadLine();
                            if (line != "save")
                                newFile.addLine(line);
                            count++;
                        }
                    }

                    documents.Add(newFile);
                    }
                    else
                    {
                        Console.WriteLine("Creating a batch file is not supported inside a batch file..");
                    }
                    break;

                default:

                    int startOfString = -1;
                    int endOfString = -1;
                    bool stringFound = false;
                    if (prefix == "set")
                    {
                        action = input.Split(' ')[1];
                        string varName = input.Split(' ')[1].Trim();
                        string expression = input.Split('=')[1].Trim();

                        List<char> operations = new List<char>();
                        for (int i = 0; i < expression.Length; i++)
                        {
                            if (expression[i] == '+') operations.Add('+');
                            else if (expression[i] == '-') operations.Add('-');
                            else if (expression[i] == '*') operations.Add('*');
                            else if (expression[i] == '/') operations.Add('/');
                            else if (expression[i] == '"')
                            {
                                if (startOfString == -1) startOfString = i;
                                else endOfString = i;
                                stringFound = true;

                            }
                        }
                        int varOf = 0;
                        if (operations.Count > 0 && !stringFound)
                        {
                            string leftArg = expression.Split(operations[0])[0].Trim();
                            string rightArg = expression.Split(operations[0])[1].Trim();
                            int varOfLeft = Int32.Parse(leftArg);
                            int varOfRight = Int32.Parse(rightArg);

                            if (operations[0] == '+') varOf = varOfLeft + varOfRight;
                            else if (operations[0] == '-') varOf = varOfLeft - varOfRight;
                            else if (operations[0] == '*') varOf = varOfLeft * varOfRight;
                            else if (operations[0] == '/')
                            {
                                if (varOfRight == 0)
                                    Console.WriteLine("Can not divide by 0");
                                else
                                    varOf = varOfLeft / varOfRight;
                            }
                            variables.AddVar(varName, varOf);
                        }
                        else if (operations.Count > 0 && stringFound)
                        {
                            if (operations[0] == '+')
                            {
                                string stringVarOf;
                                string leftArgStr = expression.Split(operations[0])[0].Trim();
                                string rightArgStr = expression.Split(operations[0])[1].Trim();
                                string stringValLeft = "";
                                string stringValRight = "";

                                int leftStringStart = -1;
                                int leftStringEnd = -1;
                                int rightStringStart = -1;
                                int rightStringEnd = -1;
                                for (int i = 0; i < leftArgStr.Length; i++)
                                {
                                    if (leftArgStr[i] == '"')
                                    {
                                        if (leftStringStart == -1) leftStringStart = i;
                                        else leftStringEnd = i;
                                    }
                                }
                                for (int i = 0; i < rightArgStr.Length; i++)
                                {
                                    if (rightArgStr[i] == '"')
                                    {
                                        if (rightStringStart == -1) rightStringStart = i;
                                        else rightStringEnd = i;
                                    }
                                }
                                for (int i = leftStringStart + 1; i < leftStringEnd; i++)
                                {
                                    stringValLeft += leftArgStr[i];
                                }
                                for (int i = rightStringStart + 1; i < rightStringEnd; i++)
                                {
                                    stringValRight += rightArgStr[i];
                                }
                                stringVarOf = stringValLeft + stringValRight;
                                variables.AddVar(varName, stringVarOf);
                            }
                        }
                        else
                        {

                            if (!stringFound)
                            {
                                varOf = Int32.Parse(expression);
                                variables.AddVar(varName, varOf);
                            }
                            else
                            {
                                string stringVal = "";
                                for (int i = startOfString + 1; i < endOfString; i++)
                                {
                                    stringVal += expression[i];
                                }
                                variables.AddVar(varName, stringVal);
                            }

                        }


                    }
                    else if (prefix == "out")
                    {
                        action = input.Split(' ')[1];
                        object a = variables.VarExist(action);
                        Console.WriteLine(a);
                        break;
                    }
                    else if (prefix == "run")
                    {
                        action = input.Split(' ')[1];
                        string[] fileslice = action.Split('.');
                        if (fileslice[1] == "bat")
                        {
                            for (int i = 0; i < documents.Count; i++)
                            {

                                if (documents[i].name == fileslice[0])
                                {
                                    List<string> lines = documents[i].content;
                                    for(int j = 0; j < lines.Count; j++){
                                        menuSelection(lines[j], true);
                                    }
                                }
                            }
                        }
                    }
                    else if (prefix == "cls")
                    {
                        Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
                    }
                    else
                    {
                        Console.WriteLine("That is an unknown command. Please try again!");
                    }
                    break;

            }



        }
        protected override void Run()
        {
            while (true)
            {
                Console.Write(currentDir.ToString() + "> ");
                var input = Console.ReadLine().ToLower();
                menuSelection(input, false);
            }

        }
        public void PrintDirContents()
        {
            if (documents.Count == 0)
            {
                Console.WriteLine("There are no files in the current directory");
            }
            else
            {
                Console.WriteLine("\nName   \t\t Extention \t Date \t Size");
                Console.WriteLine("------------------------------------------");

                File[] apps = new File[documents.Count];
                documents.CopyTo(apps, 0);

                for (int i = 0; i < apps.Length; i++)
                {
                    Console.Write(apps[i].name);
                    Console.Write(" \t\t");
                    Console.Write(apps[i].ext);
                    Console.Write(" \t");
                    Console.Write(apps[i].GetDate());
                    Console.Write(" \t");
                    Console.Write(apps[i].size + " bytes\n");
                }
                Console.WriteLine("\n");
            }

        }
        public void PrintTime()
        {
            string h = Cosmos.Hardware.RTC.Hour.ToString();
            string m = Cosmos.Hardware.RTC.Minute.ToString();
            string s = Cosmos.Hardware.RTC.Second.ToString();
            Console.WriteLine(h + ":" + m + ":" + s);
        }
        public void PrintDate()
        {
            int dayweek = Cosmos.Hardware.RTC.DayOfTheWeek;
            string month = Cosmos.Hardware.RTC.Month.ToString();
            string day = Cosmos.Hardware.RTC.DayOfTheMonth.ToString();
            string year = Cosmos.Hardware.RTC.Year.ToString();
            string dayString = GetDateString(dayweek);
            Console.WriteLine("The current date is " + dayString + " " + month +
                "/" + day + "/" + year);
        }
        public string GetDateString(int day)
        {

            switch (day)
            {
                case 5:
                    return "Mon";
                case 6:
                    return "Tues";
                case 7:
                    return "Wed";
                case 1:
                    return "Thurs";
                case 2:
                    return "Fri";
                case 3:
                    return "Sat";
                case 4:
                    return "Sun";
                default:
                    return "Error";
            }

        }
    }
}
