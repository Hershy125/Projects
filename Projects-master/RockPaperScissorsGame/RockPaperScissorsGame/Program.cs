using System;

namespace RockPaperScissorsGame
{
    class Program
    {
        static void Main(string[] args)
        {
            string compChoice;
            string userChoice;

            Console.WriteLine("Welcome to Dave's RPS Console Game!");

            while (true)
            {
                int ties = 0;
                int userWins = 0;
                int compWins = 0;

                Console.WriteLine("How many rounds of RPS do you want to play? Max is 10");
                string numberRounds = Console.ReadLine();

                if (int.TryParse(numberRounds, out int rounds))
                {
                    if (rounds > 10 || rounds < 1)
                    {
                        Console.WriteLine("Please enter a number between 1 and 10");
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a number between 1 and 10");
                    continue;
                }

                for (int i = 0; i < rounds; i++)
                {
                    Console.Write("Please Enter Rock, Paper, or Scissors: ");
                    userChoice = Console.ReadLine();
                    compChoice = CompChooser();

                    if (compChoice == "Rock" && userChoice == "Rock")
                    {
                        Console.WriteLine($"You chose {userChoice} the computer chose {compChoice}");
                        Console.WriteLine("It's a tie!");
                        ties += 1;
                    }
                    else if (compChoice == "Rock" && userChoice == "Paper")
                    {
                        Console.WriteLine($"You chose {userChoice} the computer chose {compChoice}");
                        Console.WriteLine("You Win!");
                        userWins += 1;
                    }
                    else if (compChoice == "Rock" && userChoice == "Scissors")
                    {
                        Console.WriteLine($"You chose {userChoice} the computer chose {compChoice}");
                        Console.WriteLine("You lose!");
                        compWins += 1;
                    }
                    else if (compChoice == "Paper" && userChoice == "Rock")
                    {
                        Console.WriteLine($"You chose {userChoice} the computer chose {compChoice}");
                        Console.WriteLine("You lose!");
                        compWins += 1;
                    }
                    else if (compChoice == "Paper" && userChoice == "Paper")
                    {
                        Console.WriteLine($"You chose {userChoice} the computer chose {compChoice}");
                        Console.WriteLine("It's a tie!");
                        ties += 1;
                    }
                    else if (compChoice == "Paper" && userChoice == "Scissors")
                    {
                        Console.WriteLine($"You chose {userChoice} the computer chose {compChoice}");
                        Console.WriteLine("You Win!");
                        userWins += 1;
                    }
                    else if (compChoice == "Scissors" && userChoice == "Rock")
                    {
                        Console.WriteLine($"You chose {userChoice} the computer chose {compChoice}");
                        Console.WriteLine("You Win!");
                        userWins += 1;
                    }
                    else if (compChoice == "Scissors" && userChoice == "Paper")
                    {
                        Console.WriteLine($"You chose {userChoice} the computer chose {compChoice}");
                        Console.WriteLine("You lose!");
                        compWins += 1;
                    }
                    else if (compChoice == "Scissors" && userChoice == "Scissors")
                    {
                        Console.WriteLine($"You chose {userChoice} the computer chose {compChoice}");
                        Console.WriteLine("It's a tie!");
                        ties += 1;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid option");
                        i -= 1;
                    }
                }

                Console.WriteLine("THE GAMES ARE OVER");
                Console.WriteLine($"You won {userWins} games, the computer won {compWins} games, and there were {ties} tie games");
                if (userWins > compWins)
                {
                    Console.WriteLine("Congratulations you won!");
                }
                else if (userWins == compWins)
                {
                    Console.WriteLine("It's a tie!");
                }
                else
                {
                    Console.WriteLine("Sorry, you lost!");
                }

                Console.WriteLine("Would you like to play again? (y/n)");
                string playAgain = Console.ReadLine();

                if (playAgain == "y")
                {
                    continue;
                }
                else
                {
                    Console.WriteLine("Thanks for playing!");
                    break;
                }

            }
            
        }


        public static string CompChooser()
        {
            int rps;
            Random rng = new Random();
            rps = rng.Next(1, 4);

            switch(rps)
            {
                case 1:
                    return "Rock";
                case 2:
                    return "Paper";
                case 3:
                    return "Scissors";
                default:
                    return "Rock";
            }
        }
    }
}
