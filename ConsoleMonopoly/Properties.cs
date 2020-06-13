using System;

namespace ConsoleMonopoly
{
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
        public Player Owner { get; set; } /* The owner of the property */
        public bool IsMortgaged { get; set; } /* Is this property mortgaged? True/False*/
    }

    public class RegularProperty : IProperty
    {
        /* Regular Properties (Mediterranean Avenue) see IProperty for generic variables*/
        public enum ColorGroup { DarkPurple, LightBlue, Violet, Orange, Red, Yellow, Green, DarkBlue}; /* Each of the colors that a property can be*/
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
        public int NumOfRRs { get; set; }
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
            NumOfRRs = 0;
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
        public bool BothUtils { get; set; }
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
            BothUtils = false;
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
        public Player Owner { get; set; }
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
        public Player Owner { get; set; }
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
}
