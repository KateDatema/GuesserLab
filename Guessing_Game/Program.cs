using System;
using System.Collections.Generic;
using System.Linq;

namespace GuessingGame
{
    class Program
    {
        static void Main(string[] args)
        {
            bool goOn = true;
            //int lastTry = 0;
            int loopRound = 1; 
            while (goOn == true)
            {
                Random r = new Random();
                int secret = r.Next(1, 101);
                int tries = 0;
                string response = "";
                //int bruteForceTotal;


                while (response != "Match!")
                {
                    int num = BruteForce(tries);

                    response = Guess(num, secret);
                    //Console.WriteLine(response);
                    //Console.WriteLine();

                    tries++;
             

                }
                //bruteForceTotal = lastTry + tries;
                //lastTry = tries;
                //int bfAverage = bruteForceTotal / loopRound;
                

                Console.WriteLine($"it took BruteForce {tries} to guess {secret}");
                //Console.WriteLine($"BruteForce average tries currently is {bfAverage}");



                Console.WriteLine();
                int current = 1;
                response = "";
                while (response != "Match!")
                {
                    int num = RandomGuesser();
                    response = Guess(num, secret);
                    //Console.WriteLine(response);
                    //Console.WriteLine();
                    current++;
                }

                Console.WriteLine($"The Random Guesser took {current} times to guess the number {secret} "); Console.WriteLine();
                List<int> triedNumbers = new List<int>();
                int i = 1;
                response = "";
                while (response != "Match!")
                {
                    int num = Elimination(triedNumbers);
                    response = Guess(num, secret);
                    //Console.WriteLine(response);
                    //Console.WriteLine();
                    i++;
                }
                //bruteForceTotal = lastTry + i;
                //lastTry = tries;
                //int bfAverage = bruteForceTotal / loopRound;
                Console.WriteLine($"The Elimination took {i} times to guess the number {secret} ");
                //foreach (int number in triedNumbers)
                //{ Console.WriteLine(number); }
                loopRound++;
                goOn=GetContinue();
                
            }

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