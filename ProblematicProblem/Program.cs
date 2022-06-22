using System;
using System.Collections.Generic;
using System.Threading;

namespace ProblematicProblem
{
    class Program
    {
        static List<string> acceptableYes = new List<string>() { "yes", "sure", "true", "yep", "keep" };
        static List<string> acceptableNo = new List<string>() { "no", "no thanks", "nope", "nah", "nu uh", "no no no", "never", "never in a million years", "redo" };

        static int AskForInt(string msg = "please enter a number", int lowerBound = 0, int upperBound = 100)
        {
            bool gotInt = false;
            bool intGood = false;
            int userInput;
            do {
                Console.WriteLine(msg);
                gotInt = int.TryParse(Console.ReadLine(), out userInput);
                if (gotInt && lowerBound <= userInput && userInput <= upperBound)
                    intGood = true;
            } while (!intGood); 
            return userInput;
        }

        static bool AskForBool(string msg = "Yes/No?")
        {
            bool gotBool = false;
            bool userBool = false;
            string userInput;
            int numFailedInputs = 0;
            while (!gotBool)
            {
                Console.WriteLine(msg);
                userInput = Console.ReadLine().ToLower().Trim();
                if (acceptableYes.Contains(userInput))
                {
                    userBool = true;
                    gotBool = true;
                }
                else if (acceptableNo.Contains(userInput))
                {
                    userBool = false;
                    gotBool = true;
                }
                else
                {
                    numFailedInputs++;
                    Console.WriteLine($"\n'{userInput}' is not an acceptable input, please try again\n");
                    if (numFailedInputs % 3 == 0)
                    {
                        Console.WriteLine("acceptable inputs are: ");
                        foreach (var yes in acceptableYes) { Console.WriteLine(yes); }
                        foreach (var no in acceptableNo) { Console.WriteLine(no); }
                        Console.WriteLine("\nwould you like to set your input as acceptable? type \"YES\"");
                        string userResp = Console.ReadLine().Trim();
                        int userSel;
                        if (userResp == "YES")
                        {
                            bool confirmed = false;
                            string userConfirm;
                            do
                            {
                                Console.WriteLine($"\nplease re-enter the input to confirm, type \"EXIT\" to cancel \noriginal input: {userInput}");
                                userConfirm = Console.ReadLine();
                                if (userConfirm == userInput)
                                {
                                    userSel = AskForInt("\nwould you like to add your input as a true/yes or a false/no input? \nenter 0 to cancel \nenter 1 for true/yes \nenter 2 for false/no", 0, 2);
                                    if (userSel == 0)
                                        Console.WriteLine("good idea. try again");
                                    else if (userSel == 1)
                                    {
                                        Console.WriteLine($"adding {userInput} to acceptable 'yes' inputs\n");
                                        acceptableYes.Add(userInput);
                                    }
                                    else if (userSel == 2)
                                    {
                                        Console.WriteLine($"adding {userInput} to acceptable 'no' inputs\n");
                                        acceptableNo.Add(userInput);
                                    }
                                    else
                                        Console.WriteLine("we done made another mistake");
                                    confirmed = true;
                                }
                                else if (userConfirm == "EXIT")
                                {
                                    Console.WriteLine("canceling operation and returning...");
                                    confirmed = true;
                                }
                            } while (!confirmed);
                        }
                    }
                }
            }
            return userBool;
        }

        static void Main(string[] args)
        {
            bool cont = true;
            var rng = new Random();
            List<string> activities = new List<string>() { "Movies", "Paintball", "Bowling", "Lazer Tag", "LAN Party", "Hiking", "Axe Throwing", "Wine Tasting" };
            cont = AskForBool("Hello, welcome to the random activity generator! \nWould you like to generate a random activity? yes/no: ");
            Console.Write("\nWe are going to need your information first! What is your name? ");
            string userName = Console.ReadLine();
            int userAge = AskForInt("\nWhat is your age?");
            bool seeList = AskForBool("\nWould you like to see the current list of activities? Sure/No thanks: \n");
            if (seeList)
            {
                foreach (string activity in activities)
                {
                    Console.Write($"{activity} ");
                    Thread.Sleep(250);
                }
                bool addToList = AskForBool("\nWould you like to add any activities before we generate one? yes/no: ");
                while (addToList)
                {
                    Console.Write("\nWhat would you like to add? ");
                    string userAddition = Console.ReadLine();
                    activities.Add(userAddition);
                    foreach (string activity in activities)
                    {
                        Console.Write($"{activity} ");
                        Thread.Sleep(250);
                    }
                    addToList = AskForBool("\nWould you like to add more? yes/no: ");
                }
            }

            while (cont)
            {
                Console.Write("Connecting to the database");
                for (int i = 0; i < 10; i++)
                {
                    Console.Write(". ");
                    Thread.Sleep(500);
                }
                Console.WriteLine();
                Console.Write("Choosing your random activity");
                for (int i = 0; i < 9; i++)
                {
                    Console.Write(". ");
                    Thread.Sleep(500);
                }
                Console.WriteLine();
                int randomNumber = rng.Next(activities.Count);
                string randomActivity = activities[randomNumber];
                if (userAge < 21 && randomActivity == "Wine Tasting")
                {
                    Console.WriteLine($"Oh no! Looks like you are too young to do {randomActivity}");
                    Console.WriteLine("Pick something else!");
                    activities.Remove(randomActivity);
                    randomNumber = rng.Next(activities.Count);
                    randomActivity = activities[randomNumber];
                }
                if (randomActivity == "LAN Party") { randomActivity += " (nerd)"; }
                cont = !AskForBool($"Ah got it! {userName}, your random activity is: {randomActivity}! Is this ok or do you want to grab another activity? Keep/Redo: ");
            }
            Console.WriteLine("\nhave a wonderful day!");
        }
    }
}