namespace ConsoleMonopoly
{
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
}
