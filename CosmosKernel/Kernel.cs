using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using Cosmos.Hardware;

namespace CosmosKernel1
{
    public class Kernel : Sys.Kernel
    {
        public List<File> documents;
        public Directory homeDir;
        public Directory currentDir;
        public Variable variables;
        public CMD command;

        protected override void BeforeRun()
        {
            homeDir = new Directory("C://", null);
            currentDir = homeDir;
            documents = new List<File>();
            variables = new Variable();
            command = new CMD();
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
                    string[] var = new string[2];
                    if (prefix == "set")
                    {
                        action = input.Split(' ')[1];
                        var = command.EvaluateCommand(input, variables);
                        variables.AddVar((string)var[0], var[1]);
                    }
                    else if (prefix == "out")
                    {
                        action = input.Split(' ')[1];
                        object a = variables.GetVarValue(action);
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
                                    for (int j = 0; j < lines.Count; j++)
                                    {
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
                    Console.Write(" \t");
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
