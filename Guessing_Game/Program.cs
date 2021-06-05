using System;
using System.Collections.Generic;
using System.Linq;

namespace GuessingGame
{
    class Program
    {
        static void Main(string[] args)
        {
            //bool goOn = true;

            //setting up our random number
            //Random r = new Random();
            int runs = 1;
            //setting up our list so we can hold how many rounds it took and get the best, worst, and average
            List<int> bruteForceTries = new List<int>();
            List<int> elimatation = new List<int>();
            List<int> randomGuesser = new List<int>();

            while (runs <= 100)
            {
                int secret = runs;
                runs++;
                //setting up our gobal var for string response
                string response = "";


                //setting up a var for BruteForce to count rounds with
                int tries = 0;
                //running BruteForce function until it finds a match
                while (response != "Match!")
                {
                    //running our method to get a num
                    int num = BruteForce(tries);
                    // runing our num threw the guess method (too see if it's right)
                    response = Guess(num, secret);
                    // adds a try every round so we can count how long it takes to get to a match
                    tries++;
                }

                bruteForceTries.Add(tries);
                //Console.WriteLine($"it took BruteForce {tries} to guess {secret}");
                //adding the tries after the while loop breaks so that we can find average..
                

                // setting up a var to count rounds for the random guesser
                int current = 0;
                response = "";
                while (response != "Match!")
                {
                    int num = RandomGuesser();
                    response = Guess(num, secret);
                    current++;
                }

                //Console.WriteLine($"The Random Guesser took {current} times to guess the number {secret} "); Console.WriteLine();
                randomGuesser.Add(current);

                List<int> triedNumbers = new List<int>();

                int i = 1;
                response = "";
                while (response != "Match!")
                {
                    int num = Elimination(triedNumbers);
                    response = Guess(num, secret);
                    i++;
                }
                //Console.WriteLine($"The Elimination took {i} times to guess the number {secret} ");
                elimatation.Add(i);

                //analzing results
                Console.WriteLine();
                AnalzyeList(bruteForceTries, "Brute Force");
                Console.WriteLine();
                AnalzyeList(elimatation, "Elimatation");
                Console.WriteLine();
                AnalzyeList(randomGuesser, "random Guesser");
                
            }

        }

        public static void AnalzyeList(List<int> attempts, string guesserName)
        {

            int best = attempts.Min();
            int worst = attempts.Max();
            double average = attempts.Average();

            Console.WriteLine($"Best {guesserName} Case: " + best);
            Console.WriteLine($"Worst {guesserName} Case: " + worst);
            Console.WriteLine($"Average {guesserName} Case: " + average);
        }

        public static int GetUserGuess()
        {
            while (true)
            {
                Console.WriteLine("Please guess a number between 1 and 100 and I will tell how close you are");
                try
                {
                    int num = RandomGuesser();
                    if (num < 1)
                    {
                        throw new Exception("That number is too small, please input a number between 1 and 100");
                    }
                    else if (num > 100)
                    {
                        throw new Exception("That number is too large, please inptu a number between 1 and 100");
                    }
                    return num;

                }
                catch (FormatException)
                {
                    Console.WriteLine("That was not a valid number please try again");
                    continue;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
            }
        }

        public static int BruteForce(int tries)
        {   
            int guess = 100 - tries;
            return guess;
        }

        public static int RandomGuesser()
        {
            Random r = new Random();
            int randomGuess = r.Next(1, 101);
            return randomGuess;
        }

        public static int Elimination(List<int> triedNumbers)
        {
            Random r = new Random();
            int randomGuess = r.Next(1, 101);
            
            if (triedNumbers.Contains(randomGuess))
            {
                randomGuess = r.Next(1, 101);
   
            }

            else
            {
                triedNumbers.Add(randomGuess);
                
            }
           
            
            return randomGuess;
        }

        public static string Guess(int guess, int secretNum)
        {
            if (guess == secretNum)
            {
                return "Match!";
            }
            int diff = guess - secretNum;
            diff = Math.Abs(diff);

            if (guess > secretNum)
            {
                if (diff > 10)
                {
                    return "Way too high!";
                }
                else
                {
                    return "too high!";
                }
            }
            else
            {
                if (diff > 10)
                {
                    return "Way too low!";
                }
                else
                {
                    return "too low!";
                }
            }
        }
        public static bool GetContinue()
        {
            Console.WriteLine("Would you like to continue? y/n");
            string answer = Console.ReadLine();

            //There are 3 cases we care about 
            //1) y - we want to continue 
            //2) n - we want to stop 
            //3) anything else - we want to try again

            if (answer == "y")
            {
                return true;
            }
            else if (answer == "n")
            {
                return false;
            }
            else
            {
                Console.WriteLine("I didn't understand that, please try again");
                //Calling a method inside itself is called recursion
                //Think of this as just trying again 
                return GetContinue();
            }
        }
    }
}