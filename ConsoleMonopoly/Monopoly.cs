using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Threading;

namespace ConsoleMonopoly
{
    class Monopoly
    {
        public enum Actions { EndTurn, Roll, BuyHouse, Trade };
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
            return false;
        }
    }

    class Board 
    {
        /*Create the board, each property, list of Chance/CC, player tokens, etc.*/
        public IProperty[] Properties;
        public HousesAndHotels HAndH;

        public Board(int NumberOfPlayers)
        {
            /* TO DO List
            */
            /* Creating all of the spaces on the board*/
            IProperty[] prop = new IProperty[40];
            prop[0] = new MiscSpace("GO", 0, MiscSpace.MiscType.GO);
            int[] MediteraneanRent = new int[6] { 2, 10, 30, 90, 160, 250 };
            prop[1] = new RegularProperty("Mediterranean Avenue", 60, 1, 50, MediteraneanRent, RegularProperty.ColorGroup.DarkPurple);
            int [] CommChestDeck = RandomDeck();
            prop[2] = new ChanceAndCommChest("Community Chest", 2, CommChestDeck);
            int[] BalticRent = new int[6] { 4, 20, 60, 180, 320, 500 };
            prop[3] = new RegularProperty("Baltic Avenue", 60, 3, 50, BalticRent, RegularProperty.ColorGroup.DarkPurple);
            prop[4] = new RegularProperty("Income Tax", 200, 4, 0, null, RegularProperty.ColorGroup.Tax);
            int[] RRRent = new int[4] { 25, 50, 100, 200 };
            prop[5] = new RailRoadProperty("Reading Railroad", 5, RRRent);
            int[] OriAndVerRent = new int[6] { 6, 30, 90, 270, 400, 550 };
            prop[6] = new RegularProperty("Oriental Avenue", 100, 6, 50, OriAndVerRent, RegularProperty.ColorGroup.LightBlue);
            int[] ChanceDeck = RandomDeck();
            prop[7] = new ChanceAndCommChest("Chance", 7, ChanceDeck);
            prop[8] = new RegularProperty("Vermont Avenue", 100, 8, 50, OriAndVerRent, RegularProperty.ColorGroup.LightBlue);
            int[] ConnectRent = new int[6] { 8, 40, 100, 300, 450, 600 };
            prop[9] = new RegularProperty("Connecticut Avenue", 120, 9, 50, ConnectRent, RegularProperty.ColorGroup.LightBlue);
            prop[10] = new MiscSpace("Just Visiting Jail", 10, MiscSpace.MiscType.Visiting);
            int[] CharAndStateRent = new int[6] { 10, 50, 150, 450, 625, 750 };
            prop[11] = new RegularProperty("St.Charles Avenue", 140, 11, 100, CharAndStateRent, RegularProperty.ColorGroup.Violet);
            prop[12] = new UtilityProperty("Electric Company", 12, null, null);
            prop[13] = new RegularProperty("States Avenue", 140, 13, 100, CharAndStateRent, RegularProperty.ColorGroup.Violet);
            int[] VirgRent = new int[6] { 12, 60, 180, 500, 700, 900 };
            prop[14] = new RegularProperty("Virginia Avenue", 160, 14, 100, VirgRent, RegularProperty.ColorGroup.Violet);
            prop[15] = new RailRoadProperty("Pennsylvania Railroad", 15, RRRent);
            int[] JamesAndTennRent = new int[6] { 14, 70, 200, 550, 750, 950 };
            prop[16] = new RegularProperty("St.James Place", 180, 16, 100, JamesAndTennRent, RegularProperty.ColorGroup.Orange);
            prop[17] = new ChanceAndCommChest("Community Chest", 17, CommChestDeck);
            prop[18] = new RegularProperty("Tennessee Avenue", 180, 18, 100, JamesAndTennRent, RegularProperty.ColorGroup.Orange);
            int[] NYRent = new int[6] { 16, 80, 220, 600, 800, 100 };
            prop[19] = new RegularProperty("New York Avenue", 200, 19, 100, NYRent, RegularProperty.ColorGroup.Orange);
            prop[20] = new MiscSpace("Free Parking", 20, MiscSpace.MiscType.Parking);
            int[] KentAndIndiRent = new int[6] { 18, 90, 250, 700, 875, 1050 };
            prop[21] = new RegularProperty("Kentucky Avenue", 220, 21, 150, KentAndIndiRent, RegularProperty.ColorGroup.Red);
            prop[22] = new ChanceAndCommChest("Chance", 22, ChanceDeck);
            prop[23] = new RegularProperty("Indiana Avenue", 220, 23, 150, KentAndIndiRent, RegularProperty.ColorGroup.Red);
            int[] IllRent = new int[6] { 20, 100, 300, 750, 925, 1100 };
            prop[24] = new RegularProperty("Indiana Avenue", 240, 24, 150, IllRent, RegularProperty.ColorGroup.Red);
            prop[25] = new RailRoadProperty("B & O Railroad", 25, RRRent);
            int[] AtlAndVentRent = new int[6] { 22, 110, 330, 800, 975, 1150 };
            prop[26] = new RegularProperty("Atlantic Avenue", 260, 26, 150, AtlAndVentRent, RegularProperty.ColorGroup.Yellow);
            prop[27] = new RegularProperty("Ventnor Avenue", 260, 27, 150, AtlAndVentRent, RegularProperty.ColorGroup.Yellow);
            prop[28] = new UtilityProperty("Water Works", 28, null, null);
            int[] MarvRent = new int[6] { 24, 120, 360, 850, 1025, 1200 };
            prop[29] = new RegularProperty("Marvin Gardens", 280, 29, 150, MarvRent, RegularProperty.ColorGroup.Yellow);
            prop[30] = new MiscSpace("Go To Jail", 30, MiscSpace.MiscType.GoToJail);
            int[] PacAndNCRent = new int[6] { 26, 130, 390, 900, 1100, 1275 };
            prop[31] = new RegularProperty("Pacific Avenue", 300, 31, 200, PacAndNCRent, RegularProperty.ColorGroup.Green);
            prop[32] = new RegularProperty("North Carolina Avenue", 300, 32, 200, PacAndNCRent, RegularProperty.ColorGroup.Green);
            prop[33] = new ChanceAndCommChest("Community Chest", 33, CommChestDeck);
            int[] PennRent = new int[6] { 28, 150, 450, 100, 1200, 1400 };
            prop[34] = new RegularProperty("Pennsylvania Avenue", 320, 34, 200, PennRent, RegularProperty.ColorGroup.Green);
            prop[35] = new RailRoadProperty("Short Line", 35, RRRent);
            prop[36] = new ChanceAndCommChest("Chance", 36, ChanceDeck);
            int[] PPRent = new int[6] { 35, 175, 500, 1100, 1300, 1500 };
            prop[37] = new RegularProperty("Park Place", 350, 37, 200, PPRent, RegularProperty.ColorGroup.DarkBlue);
            prop[38] = new RegularProperty("Income Tax", 100, 38, 0, null, RegularProperty.ColorGroup.Tax);
            int[] BWRent = new int[6] { 50, 200, 600, 1400, 1700, 2000 };
            prop[39] = new RegularProperty("Boardwalk", 400, 39, 200, BWRent, RegularProperty.ColorGroup.DarkBlue);
            Properties = prop;
            
            /* Creates the sets of houses and hotels*/
            HAndH = new HousesAndHotels();
        }


        /* Chance and Community Chest Maker */
        /* List of instructions for the players to randomly choose when they land on one of the spaces*/
        /* Two randomized int arrays with each number corresponding to a specific instruction */
        public int[] RandomDeck()
        {
            /*There are sixteen Chance/CC cards. This makes an int array with random numbers from 1-16 with no repeats*/
            int Min = 1;
            int Max = 17;
            int[] Chance = new int[16];
            Random randNum = new Random();
            for (int i = 0; i < Chance.Length; i++) {
                int j = randNum.Next(Min, Max);
                if (Chance.Contains(j))
                {
                    i--;
                }
                else
                {
                    Chance[i] = j;
                }
            }
            return Chance;
        }
    }

    class DiceRoll 
    {
        public int Roll()
        {
            Random rnd = new Random();
            int firstDice = rnd.Next(1, 6);
            int secondDice = rnd.Next(1, 6);
            int roll = firstDice + secondDice;
            return roll;
        }

        public bool IsDoubles(int first, int second)
        {
            if (first == second)
            {
                return true;
            }
            else { return false; }
        }

        public bool TripleDoubles() { return false; }
    }

    public interface IProperty 
    {
        /* Interface for the Property spaces*/
        public string Name { get; set; } /* The name of the space*/
        public string Type { get; set; }
        public int Cost { get; set; } /*The cost to buy the space*/
        public int Location { get; set; } /*The location of the space on the board*/
        public Array RentArray { get; set; } /*The rent according to houses, other RRs, or other utility card */
        public int Mortgage { get; set; } /* The value of the mortgage*/
        public bool IsOwned { get; set; } /* Is this property owned? True/False*/
        public bool IsMortgaged { get; set; } /* Is this property mortgaged? True/False*/
    }

    class RegularProperty : IProperty
    {
        /* Regular Properties (Mediterranean Avenue) see IProperty for generic variables*/
        public enum ColorGroup { DarkPurple, LightBlue, Violet, Orange, Red, Yellow, Green, DarkBlue, Tax }; /* Each of the colors that a property can be*/
        public string Name { get; set; }
        public string Type { get; set; }
        public int Cost { get; set; }
        public int Location { get; set; }
        public int HouseCost { get; set; } /* The cost of a house or hotel */ 
        public int NumOfHouses { get; set; }
        public Array RentArray { get; set; }
        public int Mortgage { get; set; }
        public bool IsOwned { get; set; }
        public Player Owner { get; set; }
        public bool IsMortgaged { get; set; }
        public ColorGroup Color { get; set; } /* The color of the property*/
        public bool Monopoly { get; set; } /* Is this part of a monopoly? True/False*/
        public RegularProperty(string name, int cost, int location, int houseCost, Array rentArray, ColorGroup color)
        {
            Name = name;
            Type = "Reg";
            Cost = cost;
            Location = location;
            HouseCost = houseCost;
            NumOfHouses = 0;
            RentArray = rentArray;
            Mortgage = cost/2;
            IsOwned = false;
            Owner = null;
            IsMortgaged = false;
            Color = color;
            Monopoly = false;
        }
    }

    class RailRoadProperty : IProperty
    {
        /* RR Properties (Reading RR) see IProperty for generic variables*/
        public string Name { get; set; }
        public string Type { get; set; }
        public int Cost { get; set; }
        public int Location { get; set; }
        public Array RentArray { get; set; }
        public int Mortgage { get; set; }
        public bool IsOwned { get; set; }
        public Player Owner { get; set; }
        public bool IsMortgaged { get; set; }
        public RailRoadProperty(string name, int location, Array rentArray)
        {
            Name = name;
            Type = "RR";
            Cost = 200; /* Cost is always 200 and mortgage is always 100 */
            Location = location;
            RentArray = rentArray;
            Mortgage = 100;
            IsOwned = false;
            Owner = null;
            IsMortgaged = false;
        }
    }

    class UtilityProperty : IProperty
    {
        /* Utility Properties (Electric Company) see IProperty for generic variables*/
        public string Name { get; set; }
        public string Type { get; set; }
        public int Cost { get; set; }
        public int Location { get; set; }
        public Array RentArray { get; set; }
        public int Mortgage { get; set; }
        public bool IsOwned { get; set; }
        public Player Owner { get; set; }
        public bool IsMortgaged { get; set; }
        public bool Monopoly { get; set; } /* Does the player have both utilities*/
        public DiceRoll Roll { get; set; } /* Rent is based on rolls and if you own one or both places*/
        public UtilityProperty(string name, int location, Array rentArray, DiceRoll roll)
        {
            Name = name;
            Type = "Util";
            Cost = 150; /* Cost is always 150, mortgage is always 75 */
            Location = location;
            RentArray = rentArray;
            Mortgage = 75;
            IsOwned = false;
            Owner = null;
            IsMortgaged = false;
            Roll = roll;
            Monopoly = false;
        }
    }

    class ChanceAndCommChest : IProperty 
    {
        /* Chance or Community Chest, see IProperty for generic variables*/
        public string Name { get; set; }
        public string Type { get; set; }
        public int Cost { get; set; }
        public int Location { get; set; }
        public Array RentArray { get; set; }
        public int Mortgage { get; set; }
        public bool IsOwned { get; set; }
        public bool IsMortgaged { get; set; }
        public int[] Deck { get; set; } /* The deck of instructions that is random integers */
        public ChanceAndCommChest(string name, int location, int[] deck)
        {
            Name = name;
            Type = "CC";
            Cost = 0;
            Location = location;
            RentArray = null;
            Mortgage = Cost / 2;
            IsOwned = true;
            IsMortgaged = false;
            Deck = deck;
        }
    }

    class MiscSpace : IProperty
    {
        /* Free Parking, Just Visiting, Go To Jail, and Go Space */
        public enum MiscType { Parking, Visiting, GoToJail, GO}
        public string Name { get; set; }
        public string Type { get; set; }
        public int Cost { get; set; }
        public int Location { get; set; }
        public Array RentArray { get; set; }
        public int Mortgage { get; set; }
        public bool IsOwned { get; set; }
        public bool IsMortgaged { get; set; }
        public MiscType SpaceType { get; set; } /* The type of space, in the misc category*/
        public MiscSpace(string name, int location, MiscType spaceType)
        {
            Name = name;
            Type = "Misc";
            Cost = 0;
            Location = location;
            RentArray = null;
            Mortgage = Cost / 2;
            IsOwned = true;
            IsMortgaged = false;
            SpaceType = spaceType;
        }
    }

    class Player
    {
        /* Each player has a token, name, location on the board, funds, property that is owned and property that is mortgaged*/
        public enum Token { Null, ScottishTerrier, Battleship, RaceCar, TopHat, Thimble, Shoe, WheelBarrow, Howitzer, Iron }
        public string Name { get; set; } /* Name of the player */
        public Token PlayerToken { get; set; } /* Token the player has */
        public int Location { get; set; } /* Location of the player on the board*/
        public int Funds { get; set; } /* Amount of money a player has*/
        public IProperty[] OwnedProperties { get; set; } /* The properties that the player owns*/
        public IProperty[] MortgagedProperties { get; set; } /* The properties that the player has mortgaged*/
        public Player(string name, Token token, int funds, IProperty[] ownedProperties)
        {
            Name = name;
            PlayerToken = token;
            Location = 0;
            Funds = funds;
            OwnedProperties = ownedProperties;
            MortgagedProperties = null;
        }
    }

    class HousesAndHotels 
    {
        /* The house and hotel supply that is finite on purpose*/
        public int Houses { get; set; }
        public int Hotels { get; set; }
        public HousesAndHotels()
        {
            Houses = 32;
            Hotels = 12;
        }
    }
}
