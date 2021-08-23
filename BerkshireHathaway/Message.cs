using System;
using System.Collections.Generic;
using System.Text;


namespace BerkshireHathaway
{
    /// <summary>
    /// This class handles the presentation of information to the command window.
    /// Presentation of Menu and Database output
    /// </summary>
    class Message
    {
        private BerkshireController controller;
        public Message()
        {
            controller = new BerkshireController();
        }

        /// <summary>
        /// Intro Text and Kicks off the Menu
        /// </summary>
        public void Intro()
        {
            Console.WriteLine("BerkShire Hathaway Demo Project");
            Console.WriteLine("-------------------------------\n");            
            Menu();

        }

        /// <summary>
        /// Displays the available Commands
        /// </summary>
        private void Menu()
        {
            Console.WriteLine("\n----\n");
            Console.WriteLine("MENU\n");
            Console.WriteLine("----\n");
            Console.WriteLine("READ - See Current Reasons to Work At Berkshire Hathaway\n");
            Console.WriteLine("ADD - Add a Single New Reason\n");
            Console.WriteLine("LOAD - Load New Reasons From a File\n");
            Console.WriteLine("EXIT - Exit the Application\n");
            ReadResponse();
        }

        /// <summary>
        /// Loops for User Input and kicks off individual commands
        /// </summary>
        private void ReadResponse()
        {            
            switch (Console.ReadLine().ToUpper())
            {
                case "READ":
                    Console.WriteLine("\nThese are the Reasons I Would Like to Work at Berkshire Hathaway\n");
                    Console.WriteLine("----------------------------------------------------------------\n");
                    PrintOutput("READ");
                    break;
                case "ADD":
                    PrintOutput("ADD");
                    break;
                case "LOAD":
                    PrintOutput("LOAD");
                    break;
                case "EXIT":
                    Console.WriteLine("Exiting Application");
                    Environment.Exit(0);
                    break;
            }
            ReadResponse();
        }

        /// <summary>
        /// Displays the Results of input commands
        /// </summary>
        /// <param name="Command"></param>
        private void PrintOutput(string Command)
        {
            var prints = new List<Reason>();
            string username;
            switch (Command)
            {
                case "READ":
                    prints = controller.ListReasons();
                    prints.ForEach(delegate(Reason reason)
                    {
                        Console.WriteLine(reason.ReasonText + " - " + reason.User.Name);
                    });
                    break;
                case "ADD":
                    Console.WriteLine("Who is Adding a Reason?:");
                    username = Console.ReadLine();
                    Console.WriteLine("Give a new reason:");
                    string reason = Console.ReadLine();
                    reason = controller.Add(reason,username);             
                    break;
                case "LOAD":
                    Console.WriteLine("Who is Loading a file?:");
                    username = Console.ReadLine();
                    Console.WriteLine("Give File Location:");
                    string location = Console.ReadLine();
                    prints = controller.AddFromFile(location,username);
                    if (prints.Count > 0)
                    {
                        Console.WriteLine("\nThe Following Reasons have been Successfully Added");
                        Console.WriteLine("---------------------------------------------------\n");
                        prints.ForEach(delegate (Reason reason)
                        {
                            Console.WriteLine(reason.ReasonText + " - " + reason.User.Name);
                        });
                    }                    
                    break;

            }
        }
    }

}
