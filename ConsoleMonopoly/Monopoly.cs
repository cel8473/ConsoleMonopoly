using System;
using System.Linq;

namespace ConsoleMonopoly
{
    class Monopoly
    {
        public enum Actions { EndTurn, Roll, BuyHouse, Trade, Unmortgage };
        static void Main(string[] args) 
        {
            Console.WriteLine("Welcome to Console Monopoly!");
            bool isPlayerCountWrong = true;
            string stringPlayers = "";
            while (isPlayerCountWrong) 
            {
                Console.WriteLine("How many players do you want to play with?(Max 8 players)");
                stringPlayers = Console.ReadLine();
                try
                {
                    int numberofPlayers = int.Parse(stringPlayers);
                    if (numberofPlayers > 1 && numberofPlayers < 8)
                    {
                        isPlayerCountWrong = false;
                    }
                    else { Console.WriteLine("Please enter a number between 2-8"); }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Unable to convert '{0}'.", stringPlayers);
                    Console.WriteLine("Please try again with a valid number");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("'{0}' is out of range of the Int32 type.", stringPlayers);
                    Console.WriteLine("Please try again with a valid number");
                }
                catch (ArgumentNullException) 
                {
                    Console.WriteLine("This is either a '' or null");
                    Console.WriteLine("Please try again with a valid number");
                }
            }
            int numOfPlayers = int.Parse(stringPlayers);
            Player[] listOfPlayers = new Player[numOfPlayers];
            int playerNumber = 0;
            Console.WriteLine("The max length for names is 32 characters.");
            for(int i = 0; i < numOfPlayers; i++)
            {
                bool isNameGood = true;
                string name = "";
                while (isNameGood)
                {
                    Console.WriteLine("Please enter the name of Player {0}", i + 1);
                    name = Console.ReadLine();
                    if (name != "" && name != null)
                    {
                        isNameGood = false;
                    }
                    else
                    {
                        Console.WriteLine("This is either a '' or null");
                        Console.WriteLine("Please try again with a valid string");
                    }
                }
                if(name.Length > 32)
                {
                    name = name.Substring(0, 32);
                }
                bool uniqueToken = true;
                Player.Token token = Player.Token.Null;
                while (uniqueToken)
                {
                    Console.WriteLine("Please choose a token by using the number next to the token");
                    int j = 0;
                    foreach(var token1 in Enum.GetValues(typeof(Player.Token)))
                    {
                        Console.WriteLine("{0}:{1}", j, token1);
                        j++;
                    }
                    int playerChoice = 0;
                    try
                    {
                        playerChoice = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Unable to convert '{0}'.", stringPlayers);
                        Console.WriteLine("Please try again with a valid number");
                    }
                    switch (playerChoice)
                    {
                        case 0:
                            Console.WriteLine("You chose Null.");
                            break;
                        case 1:
                            Console.WriteLine("You chose Scottish Terrier");
                            token = Player.Token.ScottishTerrier;
                            break;
                        case 2:
                            Console.WriteLine("You chose Battleship");
                            token = Player.Token.Battleship;
                            break;
                        case 3:
                            Console.WriteLine("You chose Race Car");
                            token = Player.Token.RaceCar;
                            break;
                        case 4:
                            Console.WriteLine("You chose Top Hat");
                            token = Player.Token.TopHat;
                            break;
                        case 5:
                            Console.WriteLine("You chose Thimble");
                            token = Player.Token.Thimble;
                            break;
                        case 6:
                            Console.WriteLine("You chose Shoe");
                            token = Player.Token.Shoe;
                            break;
                        case 7:
                            Console.WriteLine("You chose Wheel Barrow");
                            token = Player.Token.WheelBarrow;
                            break;
                        case 8:
                            Console.WriteLine("You chose Howitzer");
                            token = Player.Token.Howitzer;
                            break;
                        case 9:
                            Console.WriteLine("You chose Iron");
                            token = Player.Token.Iron;
                            break;
                        default:
                            Console.WriteLine("You chose an invalid number. ");
                            Console.WriteLine("You get to play as Null now.");
                            break;
                    }
                    /*This part needs fixing, does not catch copy tokens.*/
                    for(int k = 0; k < (listOfPlayers.Length - 1); k++)
                    {
                        Player player1 = listOfPlayers[k];
                        if (player1 != null)
                        {
                            if (token == Player.Token.Null)
                            {
                                uniqueToken = false;
                                break;
                            }
                            else if (token == player1.PlayerToken)
                            {
                                Console.WriteLine("Please choose a unique token");
                                Console.WriteLine("Someone has already picked {0}", player1.PlayerToken.ToString());
                            }
                            else
                            {
                                uniqueToken = false;
                            }
                        }
                        else 
                        {
                            uniqueToken = false;
                        }
                    }
                }
                IProperty[] ownedProperties = new IProperty[0];
                Player player = new Player(name, token, 1500, ownedProperties);
                listOfPlayers[playerNumber] = player;
                playerNumber++;
            }
            Console.WriteLine("Creating new board...");
            Board board = new Board(numOfPlayers);

            Console.WriteLine("Choosing who goes first. The order will be the same as when you added them.");
            int[] rolls = new int[listOfPlayers.Length];
            DiceRoll dice = new DiceRoll();
            for(int player = 0; player < listOfPlayers.Length; player++)
            {
                rolls[player] = dice.Roll();
            }
            int max = rolls.Max();
            int index = Array.IndexOf(rolls, max);
            Console.WriteLine("Congratulations {0}! You get to go first", listOfPlayers[index].Name);

            bool isGameStillGoing = true;
            int turn = index;
            Player currentPlayer = listOfPlayers[index];
            while (isGameStillGoing)
            {
                bool endTurn = true;
                while (endTurn)
                {
                    Console.WriteLine("It is {0}'s turn. What would you like to do?", listOfPlayers[turn]);
                    int j = 0;
                    foreach (var action in Enum.GetValues(typeof(Action)))
                    {
                        Console.WriteLine("{0}:{1}", j, action);
                    }
                    int playerAction = 0;
                    try
                    {
                        playerAction = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Unable to convert '{0}'.", stringPlayers);
                        Console.WriteLine("Please try again with a valid number");
                    }
                    switch (playerAction)
                    {
                        case 0:
                            Console.WriteLine("You have chosen to end your turn");
                            endTurn = false;
                            break;
                        case 1:
                            max = dice.Roll();
                            currentPlayer.Location += max;
                            if(currentPlayer.Location > 39)
                            {
                                currentPlayer.Location -= 40;
                            }
                            Console.WriteLine("You have moved forward {0} spaces and landed on {1}", max, board.Properties[currentPlayer.Location].Name);
                            string landedType = board.Properties[currentPlayer.Location].Type;
                            switch (landedType)
                            {
                                case "Reg":
                                    var regProperty = (RegularProperty) board.Properties[currentPlayer.Location];
                                    if (regProperty.IsOwned) /* If the property is owned */
                                    {
                                        if (regProperty.IsMortgaged)
                                        {
                                            Console.WriteLine("This property is mortgaged. No rent is due!");
                                        }
                                        else
                                        {
                                            int rentDue = (int) regProperty.RentArray.GetValue(regProperty.NumOfHouses);
                                            Console.WriteLine("You payed {0} ${1} in rent.", rentDue, regProperty.Owner.Name);
                                            currentPlayer.Funds -= rentDue;
                                            regProperty.Owner.Funds += rentDue;
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("No one owns this property! Would you like to purchase it? Y/N");
                                        Console.WriteLine("The default choice is No.");
                                        string answer = Console.ReadLine().Trim();
                                        if (answer.Equals("Y"))
                                        {
                                            if (regProperty.Cost > currentPlayer.Funds)
                                            {
                                                Console.WriteLine("Unfortunately you do not have sufficient funds.");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Congratulations, you have purchased {0} for ${1}", regProperty.Name, regProperty.Cost);
                                                Console.WriteLine("Your funds are now ${0}", currentPlayer.Funds);
                                                regProperty.Owner = currentPlayer;
                                                currentPlayer.OwnedProperties.Append(regProperty);
                                                if (MonopolyChecker(currentPlayer))
                                                {

                                                }
                                            }
                                        }
                                        else
                                        {
                                            /* This might be used for auctions later.*/
                                        }
                                    }
                                    break;
                                case "RR":
                                    var rrProperty = (RailRoadProperty) board.Properties[currentPlayer.Location];
                                    break;
                            }
                            /* Now that they have landed do something*/
                            break;
                    }
                }
            }
        }

        static bool MonopolyChecker(Player player) /* Does the player have a monopoly?*/
        {
            if(player.OwnedProperties.Contains())
            return false;
        }
    }



}
