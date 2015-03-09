using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using Cosmos.Hardware;

namespace CosmosKernel1
{
    public class Kernel : Sys.Kernel
    {

        Directory home;
        Directory currentDir;
        public static Variable variables;
        public static CMD command;

        //public static  Variable Variables
        //{
        //    get
        //    {
        //        return variables;
        //    }
        //    set
        //    {
        //        variables = value;
        //    }

        //}
        //public CMD Command
        //{

        //    get
        //    {
        //        return command;
        //    }
        //    set
        //    {
        //        command = value;
        //    }
        //}

        protected override void BeforeRun()
        {
            home = new Directory();
            currentDir = home;
            variables = new Variable();
            command = new CMD();
            Console.WriteLine("Cosmos booted successfully. \nPlease enter a command or type 'help' for more options.");
        }

        public void CreateNewDirectory(string name)
        {
            //Adds the new directory to the directory specified. 
            currentDir.children.Add(new Directory(name, currentDir));
        }


        protected override void Run()
        {
            while (true)
            {
                if (currentDir == home)
                    Console.Write(currentDir + "> ");
                else
                    Console.WriteLine("../" + currentDir + "> ");
                var input = Console.ReadLine().ToLower();
                string[] line = input.Split(' ');
                string dirname = line[1];

                switch (line[0])
                {
                    case "mkdir": 
                      CreateNewDirectory(dirname);
                      currentDir.documents.Add(new File(dirname, "Directory"));
                      break;
                    case "cd":
                      if (dirname == "..")
                      {
                          currentDir = currentDir.parent;
                      }
                      else
                      {
                          for (int i = 0; i < currentDir.children.Count; i++)
                          {
                              if (currentDir.children[i].ToString() == dirname)
                                  currentDir = currentDir.children[i];
                          }
                      }
                          break;
                    default:
                          bool isBatch = false;
                          currentDir.menuSelection(input, isBatch);
                      break;

                }
            }
        }

    }
}
