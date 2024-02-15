/*
 * Program:         PasswordManager.exe
 * Module:          PasswordManager.cs
 * Date:            June 5th 2022
 * Author:          Ivan Kepseu, Saeed Alsabawi
 * Description:     Some free starting code for INFO-3138 project 1, the Password Manager
 *                  application. All it does so far is demonstrate how to obtain the system date 
 *                  and how to use the PasswordTester class provided.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Linq;

using System.IO;            // File class

namespace PasswordManager
{
    class Program
    {

        static void Main(string[] args)
        {
            try
            {
                string json_data = File.ReadAllText("Account_Data.json");
                string json_schema = File.ReadAllText("Account_Schema.json");
                List<Account> accountList = JsonConvert.DeserializeObject<List<Account>>(json_data);
                int entryChoice = 0;
                char menuChoice;
                Console.Write("PASSWORD MANAGEMENT SYSTEM (STARTING CODE)");
                do
                {
                    int i = 0;
                    Console.Write("\n\n+------------------------------------------------------------------------------+");
                    Console.Write("\n|\t\t\t       Account Entries\t\t\t\t       |");
                    Console.Write("\n+------------------------------------------------------------------------------+");
                    foreach (Account account in accountList)
                    {
                        Console.Write("\n|  " + ++i + ". " + account.Description + "\t\t\t\t\t\t\t\t       |");
                    }
                    Console.Write("\n+------------------------------------------------------------------------------+");
                    Console.Write("\n|     Press # from the above list to select an entry.\t\t\t       |");
                    Console.Write("\n|     Press A to add a new entry.\t\t\t\t\t       |");
                    Console.Write("\n|     Press X to quit.\t\t\t\t\t\t               |");
                    Console.Write("\n+------------------------------------------------------------------------------+");
                    Console.Write("\n\nEnter a command:    ");
                    menuChoice = Console.ReadLine()[0];
                    
                    if (Char.IsDigit(menuChoice))
                    {
                        entryChoice = (int)Char.GetNumericValue(menuChoice);
                        if( entryChoice < 1 || entryChoice > accountList.Count)
                        {
                            Console.WriteLine($"\nERROR:\tInvalid entry.");
                            continue;
                        }
                        PrintEntry(accountList, entryChoice);
                    }
                    else if (menuChoice == 'A')
                    {
                        PrintNew(accountList);
                    }
                    else
                    {
                        if (menuChoice != 'X')
                        {
                            Console.WriteLine($"\nERROR:\tInvalid entry.");
                            continue;
                        }
                    }
                }
                while (menuChoice != 'X');
                string json_str_write = JsonConvert.SerializeObject(accountList);

                JSchema schema = JSchema.Parse(json_schema);
                JArray accounts = JArray.Parse(json_str_write);
                bool validFile = accounts.IsValid(schema, out IList<string> messages);

                if (validFile)
                {
                    Console.WriteLine($"\nData file is valid.");
                }
                else
                {
                    Console.WriteLine($"\nERROR:\tData file is invalid.\n");

                    // Report validation error messages
                    foreach (string msg in messages)
                        Console.WriteLine($"\t{msg}");
                }
              
                File.WriteAllText("Account_Data.json", json_str_write);
                
                Environment.Exit(0);
            }
            catch (IOException)
            {
                Console.WriteLine("ERROR: Can't open the JSON file.");
            }
            catch (Exception)
            {
                //Console.WriteLine(e.Message);
                Console.WriteLine("ERROR: Can't parse the JSON data.");
            }

            //TEMPLATE CODE
            // System date demonstration
            //DateTime dateNow = DateTime.Now;
            //Console.Write("PASSWORD MANAGEMENT SYSTEM (STARTING CODE), " + dateNow.ToShortDateString());
            //bool done;
            //do
            //{
            //    Console.Write("\n\nEnter a password: ");
            //    string pwText = Console.ReadLine();
            //
            //    try
            //    {
            //        // PasswordTester class demonstration
            //        PasswordTester pw = new PasswordTester(pwText);
            //        Console.WriteLine("That password is " + pw.StrengthLabel);
            //        Console.WriteLine("That password has a strength of " + pw.StrengthPercent + "%");
            //    }
            //    catch (ArgumentException)
            //    {
            //        Console.WriteLine("ERROR: Invalid password format");
            //    }
            //    
            //    Console.Write("\nTest another password? (y/n): ");
            //    done = Console.ReadKey().KeyChar != 'y';
            //
            //} while (!done);
            Console.WriteLine("\n");
        }

        public static void PrintNew(List<Account> accountList)
        {
            Account account = new Account();
            Password password = new Password();
            DateTime dateNow = DateTime.Now;
            bool repeat;
            do
            {
                try
                {
                    Console.WriteLine("\n\nPlease key-in values for the following fields...\n");
                    Console.Write("Description: ");
                    account.Description = Console.ReadLine();
                    Console.Write("User ID: ");
                    account.UserId = Console.ReadLine();
                    Console.Write("Password: ");
                    password.Value = Console.ReadLine();
                    Console.Write("Login url: ");
                    account.LoginUrl = Console.ReadLine();
                    Console.Write("Account #: ");
                    account.AccountNum = Console.ReadLine();

                    password.LastReset = dateNow.ToShortDateString();
                    Uri uri = new Uri(account.LoginUrl);
                    PasswordTester pw = new PasswordTester(password.Value);
                    password.StrengthText = pw.StrengthLabel;
                    password.StrengthNum = pw.StrengthPercent;

                    account.Password = password;

                    if (account.Description == "")
                    {
                        throw new Exception();
                    }
                    if (account.UserId == "")
                    {
                        throw new Exception();
                    }
                    if (account.Password.Value == "")
                    {
                        throw new Exception();
                    }
                    
                    accountList.Add(account);
                    repeat = false;
                }
                catch (Exception)
                {
                    Console.WriteLine("ERROR: Invalid account information entered. Please try again.");
                    repeat = true;
                }
            }
            while (repeat);
        }

        public static void PrintEntry(List<Account> accountList, int choice)
        {
            char subMenuChoice;
            char deleteYesNo;
            DateTime dateNow = DateTime.Now;

            do
            {
                int index = choice - 1;
                Account account = accountList.ElementAt(index);
                Console.Write("\n+------------------------------------------------------------------------------+");
                Console.Write("\n|  " + choice + ". " + account.Description + "\t\t\t\t\t\t\t\t       |");
                Console.Write("\n+------------------------------------------------------------------------------+\n");
                Console.WriteLine("| User ID:\t\t" + string.Format("{0,-55}|", account.UserId));
                Console.WriteLine("| Password:\t\t" + string.Format("{0,-55}|", account.Password.Value));
                Console.WriteLine("| Password Strength:\t" + string.Format("{0,-55}|", account.Password.StrengthText + " (" + account.Password.StrengthNum + "%)"));
                Console.WriteLine("| Password Reset:\t" + string.Format("{0,-55}|", account.Password.LastReset));
                Console.WriteLine("| Login url:\t\t" + string.Format("{0,-55}|", account.LoginUrl));
                Console.WriteLine("| Account #:\t\t" + string.Format("{0,-55}|", account.AccountNum));
                Console.Write("+------------------------------------------------------------------------------+");
                Console.Write("\n|      Press P to change this password.\t\t\t\t\t       |");
                Console.Write("\n|      Press D to delete this entry.\t\t\t\t\t       |");
                Console.Write("\n|      Press M to return to the main menu.\t\t\t\t       |");
                Console.Write("\n+------------------------------------------------------------------------------+");
                Console.Write("\n\nEnter a command:    ");
                subMenuChoice = Console.ReadLine()[0];

                if (subMenuChoice == 'P')
                {
                    Password password = new Password();
                    Console.Write("New Password: \t");
                    password.Value = Console.ReadLine();
                    
                    try
                    {
                        // PasswordTester class demonstration
                        if(password.Value == "")
                        {
                            Console.WriteLine("ERROR: Invalid password format");
                            continue;
                        }
                        PasswordTester pw = new PasswordTester(password.Value);
                        password.StrengthText = pw.StrengthLabel;
                        password.StrengthNum = pw.StrengthPercent;
                        password.LastReset = dateNow.ToShortDateString();
                        account.Password = password;
                        
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("ERROR: Invalid password format");
                    }
                    subMenuChoice = 'M';
                }
                else if (subMenuChoice == 'D')
                {
                    Console.Write("Delete? (Y/N): \t");
                    deleteYesNo = Console.ReadLine()[0];
                    if (deleteYesNo == 'Y')
                    {
                        accountList.RemoveAt(index);
                        subMenuChoice = 'M';
                    }
                    else if (deleteYesNo == 'N')
                    {
                        continue;
                    }
                }
                else
                {
                    if (subMenuChoice != 'M')
                    {
                        Console.WriteLine($"\nERROR:\tInvalid entry.");
                        continue;
                    }
                }
            }
            while (subMenuChoice != 'M');
        }
    } // end class
}
