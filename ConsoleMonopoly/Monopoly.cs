﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleMonopoly
{
    class Monopoly
    {
        public enum Actions { EndTurn, Roll, Trade, Unmortgage, Funds, Properties, Houses };
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
            int chanceCard = 0;
            int commChestCard = 0;
            while (isGameStillGoing)
            {
                bool endTurn = true;
                while (endTurn)
                {
                    Console.WriteLine("It is {0}'s turn. What would you like to do?", listOfPlayers[turn]);
                    if (currentPlayer.isJail)
                    {
                        Console.WriteLine("You are currently in jail. Your actions are limited to");
                    }
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
                        case 0: /* End turn */
                            Console.WriteLine("You have chosen to end your turn");
                            endTurn = false;
                            break;
                        case 1: /* Roll */
                            max = dice.Roll();
                            currentPlayer.Location += max;
                            if(currentPlayer.Location > 39)
                            {
                                currentPlayer.Location -= 40;
                            }
                            IProperty currentProperty = board.Properties[currentPlayer.Location];
                            Console.WriteLine("You have moved forward {0} spaces and landed on {1}", max, currentProperty.Name);
                            /* Now that they have landed do something*/
                            int rentDue = RentCalculator(board, currentPlayer, max); /* Rent is due. Handled in the function */
                            switch(rentDue)
                            {
                                case 0: /* Mortgaged */
                                    Console.WriteLine("This property is mortgaged by {0}, no rent is due.", currentProperty.Owner.Name);
                                    break;
                                case -1: /* This property is unowned */
                                    Console.WriteLine("This property is unowned. Would you like to purchase it? Y/N");
                                    Console.WriteLine("The default answer is No.");
                                    if(currentPlayer.Funds > currentProperty.Cost || Console.ReadKey().Key == ConsoleKey.Y)
                                    {
                                        Console.WriteLine("Congratulations, you have purchased {0} for ${1}!");
                                        currentPlayer.OwnedProperties.Append(currentProperty);
                                        currentProperty.Owner = currentPlayer;
                                        currentProperty.IsOwned = true;
                                        if (MonopolyChecker(currentPlayer, currentProperty))
                                        {
                                            Console.WriteLine("Congratulations, you have made a monopoly and can start buying houses.");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("You either had insufficient funds or chose to not purchase the property. Time for an auction!");
                                        /* auction time */
                                    }
                                    break;
                                case -2: /* This is a misc property or CC/Chance */
                                    if(currentProperty.Type == "CC") /* Chance or CC*/
                                    {
                                        ChanceAndCommChest CC = (ChanceAndCommChest)currentProperty;
                                        int card = CC.Deck[commChestCard];
                                        CommChestCards(listOfPlayers, card, currentPlayer);
                                        commChestCard++;
                                        if(commChestCard > 15)
                                        {
                                            commChestCard = 0;
                                        }
                                    }
                                    else if(currentProperty.Type == "Chance")
                                    {
                                        ChanceAndCommChest Chance = (ChanceAndCommChest)currentProperty;
                                        int card = Chance.Deck[chanceCard];
                                        ChanceCards(listOfPlayers, card, currentPlayer);
                                        chanceCard++;
                                        if(chanceCard > 15)
                                        {
                                            chanceCard = 0;
                                        }
                                    }
                                    else /* Misc: Parking, Visiting, GO, Jail */
                                    {
                                        MiscSpace misc = (MiscSpace)currentProperty;
                                        switch (misc.SpaceType)
                                        {
                                            case MiscSpace.MiscType.Parking:
                                                Console.WriteLine("You have landed on Free Parking. Enjoy your free parking!");
                                                break;
                                            case MiscSpace.MiscType.Visiting:
                                                Console.WriteLine("You have landed on Visiting Jail. Say hi to the inmates.");
                                                break;
                                            case MiscSpace.MiscType.GO:
                                                Console.WriteLine("You have landed on GO. Here is your 200$.");
                                                currentPlayer.Funds += 200;
                                                break;
                                            case MiscSpace.MiscType.GoToJail:
                                                Console.WriteLine("Go To Jail. Go directly to Jail Do not pass GO, do not collect $200.");
                                                currentPlayer.isJail = true;
                                                break;
                                        }
                                        
                                    }
                                    break;
                                default: 
                                    Console.WriteLine("You payed ${0} to {1} for rent.", rentDue, currentProperty.Owner.Name);
                                    break;
                            }
                            break;
                        case 4: /* Funds */
                            Console.WriteLine("You currently have ${0} in your funds.", currentPlayer.Funds);
                            break;
                        case 5: /*Properties */
                            foreach (IProperty property in currentPlayer.OwnedProperties)
                            {
                                if (property.Type == "Util")
                                {
                                    Console.WriteLine("Name: {0}", property.Name);
                                }
                                else if(property.Type == "RR")
                                {
                                    RailRoadProperty RRproperty = (RailRoadProperty)property;
                                    Console.WriteLine("Name: {0} Rent: {1}", property.Name, (int)RRproperty.RentArray.GetValue(RRproperty.NumOfRRs));
                                }
                                else
                                {
                                    RegularProperty regularProperty = (RegularProperty)property;
                                    Console.WriteLine("Name: {0} Rent: {1} House Cost: {2}", regularProperty.Name, (int)regularProperty.RentArray.GetValue(regularProperty.NumOfHouses), regularProperty.HouseCost);
                                }
                            }
                            break;
                    }
                }
            }
        }

        static bool MonopolyChecker(Player player, IProperty property) /* Does the player have a monopoly?*/
        {
            if (property.Type.Equals("Reg"))
            {
                RegularProperty regular = (RegularProperty)property;
                RegularProperty[] colorCheck = new RegularProperty[3];
                foreach(RegularProperty prop1 in player.OwnedProperties)
                {
                    if(prop1.Color == regular.Color)
                    {
                        colorCheck.Append(prop1);
                    }
                }
                RegularProperty.ColorGroup[] twoColorProps = new RegularProperty.ColorGroup[] { RegularProperty.ColorGroup.DarkBlue, RegularProperty.ColorGroup.DarkPurple };
                if (twoColorProps.Contains(regular.Color))
                {
                    if(colorCheck.Length == 2)
                    {
                        foreach(RegularProperty reg1 in colorCheck)
                        {
                            reg1.Monopoly = true;
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if(colorCheck.Length == 3)
                    {
                        foreach(RegularProperty reg1 in colorCheck)
                        {
                            reg1.Monopoly = true;
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else if(property.Type.Equals("RR"))
            {
                int r = 0;
                foreach(RailRoadProperty rail1 in player.OwnedProperties)
                {
                    r++;
                }
                foreach (RailRoadProperty rail2 in player.OwnedProperties)
                {
                    rail2.NumOfRRs = r;
                }
                return false;
            }
            else if(property.Type.Equals("Util"))
            {
                int u = 0;
                foreach(UtilityProperty util1 in player.OwnedProperties)
                {
                    u++;
                }
                if(u == 2)
                {
                    foreach(UtilityProperty util2 in player.OwnedProperties)
                    {
                        util2.BothUtils = true;
                    }
                }
                return false;
            }
            else /* Misc or Chance/CC */
            {
                return false;
            }
        }

        static int RentCalculator(Board board, Player player, int roll)
        {
            string landedType = board.Properties[player.Location].Type;
            switch (landedType)
            {
                case "Reg":
                    var regProperty = (RegularProperty)board.Properties[player.Location];
                    if (regProperty.IsOwned) /* If the property is owned */
                    {
                        if (regProperty.IsMortgaged)
                        {
                            return 0;
                        }
                        else
                        {
                            int rentDue = (int)regProperty.RentArray.GetValue(regProperty.NumOfHouses);
                            if (regProperty.Monopoly && regProperty.NumOfHouses == 0)
                            {
                                rentDue *= 2;
                                player.Funds -= rentDue;
                                regProperty.Owner.Funds += rentDue;
                                return rentDue;
                            }
                            else
                            {
                                player.Funds -= rentDue;
                                regProperty.Owner.Funds += rentDue;
                                return rentDue;
                            }
                        }
                    }
                    else
                    {
                         return -1;
                    }
                case "RR":
                    var rrProperty = (RailRoadProperty)board.Properties[player.Location];
                    if (rrProperty.IsOwned) /* If the property is owned */
                    {
                        if (rrProperty.IsMortgaged)
                        {
                            return 0;
                        }
                        else
                        {
                            int rentDue = (int)rrProperty.RentArray.GetValue(rrProperty.NumOfRRs);
                            player.Funds -= rentDue;
                            rrProperty.Owner.Funds += rentDue;
                            return rentDue;
                        }
                    }
                    else
                    {
                        return -1;
                    }
                case "Util":
                    var utilProperty = (UtilityProperty)board.Properties[player.Location];
                    if (utilProperty.IsOwned)
                    {
                        if (utilProperty.IsMortgaged)
                        {
                            return 0;
                        }
                        else
                        {
                            int rentDue;
                            if (utilProperty.Monopoly) { rentDue = roll * 10; }
                            else { rentDue = roll * 4; }
                            player.Funds -= rentDue;
                            utilProperty.Owner.Funds += rentDue;
                            return rentDue;
                        }
                    }
                    else
                    {
                        return -1;
                    }
                default:
                    return -2;
            }
        }

        static void CommChestCards(Player[] listOfPlayers, int cardNumber, Player player)
        {
            switch (cardNumber)
            {
                case 0:
                    Console.WriteLine("Income Tax Refund. You have collected $20");
                    player.Funds += 20;
                    break;
                case 1:
                    Console.WriteLine("Grand Opera Opening. You collect $50 from every player");
                    foreach(Player iterPlayer in listOfPlayers)
                    {
                        iterPlayer.Funds -= 50;
                    }
                    player.Funds += (listOfPlayers.Length + 1) * 50;
                    break;
                case 2:
                    Console.WriteLine("You Payed The Hospital $100");
                    player.Funds -= 100;
                    break;
                case 3:
                    Console.WriteLine("Advance To GO. You have collected $200");
                    player.Location = 0;
                    player.Funds += 200;
                    break;
                case 4:
                    Console.WriteLine("Get Out Of Jail, Free");
                    player.JailFreeCard = true;
                    break;
                case 5:
                    Console.WriteLine("Xmas Fund Matures. You have collected $100");
                    player.Funds += 100;
                    break;
                case 6:
                    Console.WriteLine("From Sale of Stock. You have collected $45");
                    player.Funds += 45;
                    break;
                case 7:
                    Console.WriteLine("Life Insurance Matures. You have collected $100");
                    player.Funds += 100;
                    break;
                case 8:
                    Console.WriteLine("Receive $25 for Services.");
                    player.Funds += 25;
                    break;
                case 9:
                    Console.WriteLine("Go Directly To Jail Go directly to Jail Do not pass GO, Do not collect $200");
                    player.isJail = true;
                    break;
                case 10:
                    Console.WriteLine("You Have Won Second Prize In A Beauty Contest. You have collected $10");
                    player.Funds += 10;
                    break;
                case 11:
                    Console.WriteLine("Pay School Tax Of $150. You are helping the children.");
                    player.Funds -= 150;
                    break;
                case 12:
                    Console.WriteLine("Doctor's Fee Pay $50. I wish doctor visits were this cheap.");
                    player.Funds -= 50;
                    break;
                case 13:
                    Console.WriteLine("You Are Assessed For Street Repairs. You pay $40 per house and $115 per hotel");
                    foreach(RegularProperty regular in player.OwnedProperties)
                    {
                        /* You still need to finish this.*/
                    }
                    break;
                case 14:
                    Console.WriteLine("Bank Error In Your Favor. You have collected $200");
                    break;
                case 15:
                    Console.WriteLine("You Inherit $100, you lucky duck.");
                    player.Funds += 100;
                    break;
            }
        }
        static void ChanceCards(Player[] listOfPlayers, int cardNumber, Player player)
        {
            switch (cardNumber)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
                case 8:
                    break;
                case 9:
                    break;
                case 10:
                    break;
                case 11:
                    break;
                case 12:
                    break;
                case 13:
                    break;
                case 14:
                    break;
                case 15:
                    break;
            }
        }
    }
}
