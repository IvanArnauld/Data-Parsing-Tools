using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSON_XML_Project2
{
    class Program
    {
        static void Main(string[] args)
        {
            Director director = null;
            Console.WriteLine("Document Builder Console Client");
            string? userChoice = "";
            bool modeSelected = false;
            PrintHelp();
            do
            {
                Console.Write("> ");
                userChoice = Console.ReadLine();
                userChoice = userChoice!.ToUpper();
                //Console.WriteLine($"User Choice: {userChoice}");
                var choices = userChoice.Split(":");
                
                //foreach(var ch in choices)
                //{
                //    Console.WriteLine(ch);
                //}
                if (userChoice == "HELP")
                {
                    PrintHelp();
                }
                else if (userChoice == "EXIT")
                {
                    Environment.Exit(0);
                }
                else if (userChoice == "PRINT" && modeSelected)
                {
                    director.composite.Print();
                    Console.WriteLine("\n");
                }
                else if (userChoice == "CLOSE" && modeSelected)
                {
                    director.CloseBranch();
                    Console.WriteLine("\n");
                }
                else if(choices[0] == "MODE" && !modeSelected)
                {
                    modeSelected = true;
                    if (choices[1] == "JSON")
                    {
                        director = new Director("JSON");
                        //Console.WriteLine("director json");
                    }
                    else if(choices[1] == "XML")
                    {
                        director = new Director("XML");
                        //Console.WriteLine("director xml");
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. For Usage, type 'Help'");
                    }
                }
                else if(choices[0] == "BRANCH" && modeSelected)
                {
                    director._name = choices[1];
                    director.BuildBranch();
                }
                else if (choices[0] == "LEAF" && modeSelected)
                {
                    director._name = choices[1];
                    director._content = choices[2];
                    director.BuildLeaf();
                }
                else
                {
                    Console.WriteLine("\nError. Mode has not been selected. For Usage, type 'Help'\n");

                    modeSelected = false;
                }
            }
            while (userChoice != "EXIT");
        }

        public static void PrintHelp()
        {
            Console.WriteLine("\nUsage:");
            Console.WriteLine("    help                   -Prints Usage(this page).");
            Console.WriteLine("    mode:<JSON|XML>        -Sets mode to JSON or XML.Must be set before creating or closing.");
            Console.WriteLine("    branch:<name>          -Creates a new branch, assigning it the passed name.");
            Console.WriteLine("    leaf:<name>:<content>  -Creates a new leaf, assigning the passed name and content.");
            Console.WriteLine("    close                  -Closes the current branch, as long as it is not the root.");
            Console.WriteLine("    print                  -Prints the document in its current state to the console.");
            Console.WriteLine("    exit                   -Exits the application.\n");
        }
    }
}

