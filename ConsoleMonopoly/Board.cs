using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleMonopoly
{
    public class Board 
    {
        /*Create the board, each property, list of Chance/CC, player tokens, etc.*/
        public IProperty[] Properties;
        public HousesAndHotels HAndH;

        public Board(int NumberOfPlayers)
        {
            /* Creating all of the spaces on the board*/
            IProperty[] prop = new IProperty[40];
            prop[0] = new MiscSpace("GO", 0, MiscSpace.MiscType.GO);
            int[] MediteraneanRent = new int[6] { 2, 10, 30, 90, 160, 250 };
            prop[1] = new RegularProperty("Mediterranean Avenue", 60, 1, 50, MediteraneanRent, RegularProperty.ColorGroup.DarkPurple);
            int [] CommChestDeck = RandomDeck();
            prop[2] = new ChanceAndCommChest("Community Chest", "CC", 2, CommChestDeck);
            int[] BalticRent = new int[6] { 4, 20, 60, 180, 320, 500 };
            prop[3] = new RegularProperty("Baltic Avenue", 60, 3, 50, BalticRent, RegularProperty.ColorGroup.DarkPurple);
            prop[4] = new RegularProperty("Income Tax", 200, 4, 0, null, RegularProperty.ColorGroup.Tax);
            int[] RRRent = new int[4] { 25, 50, 100, 200 };
            prop[5] = new RailRoadProperty("Reading Railroad", 5, RRRent);
            int[] OriAndVerRent = new int[6] { 6, 30, 90, 270, 400, 550 };
            prop[6] = new RegularProperty("Oriental Avenue", 100, 6, 50, OriAndVerRent, RegularProperty.ColorGroup.LightBlue);
            int[] ChanceDeck = RandomDeck();
            prop[7] = new ChanceAndCommChest("Chance", "Chance", 7, ChanceDeck);
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
            prop[17] = new ChanceAndCommChest("Community Chest", "CC", 17, CommChestDeck);
            prop[18] = new RegularProperty("Tennessee Avenue", 180, 18, 100, JamesAndTennRent, RegularProperty.ColorGroup.Orange);
            int[] NYRent = new int[6] { 16, 80, 220, 600, 800, 100 };
            prop[19] = new RegularProperty("New York Avenue", 200, 19, 100, NYRent, RegularProperty.ColorGroup.Orange);
            prop[20] = new MiscSpace("Free Parking", 20, MiscSpace.MiscType.Parking);
            int[] KentAndIndiRent = new int[6] { 18, 90, 250, 700, 875, 1050 };
            prop[21] = new RegularProperty("Kentucky Avenue", 220, 21, 150, KentAndIndiRent, RegularProperty.ColorGroup.Red);
            prop[22] = new ChanceAndCommChest("Chance", "Chance", 22, ChanceDeck);
            prop[23] = new RegularProperty("Indiana Avenue", 220, 23, 150, KentAndIndiRent, RegularProperty.ColorGroup.Red);
            int[] IllRent = new int[6] { 20, 100, 300, 750, 925, 1100 };
            prop[24] = new RegularProperty("Illinois Avenue", 240, 24, 150, IllRent, RegularProperty.ColorGroup.Red);
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
            prop[33] = new ChanceAndCommChest("Community Chest", "CC", 33, CommChestDeck);
            int[] PennRent = new int[6] { 28, 150, 450, 100, 1200, 1400 };
            prop[34] = new RegularProperty("Pennsylvania Avenue", 320, 34, 200, PennRent, RegularProperty.ColorGroup.Green);
            prop[35] = new RailRoadProperty("Short Line", 35, RRRent);
            prop[36] = new ChanceAndCommChest("Chance", "Chance", 36, ChanceDeck);
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
    public class HousesAndHotels 
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
