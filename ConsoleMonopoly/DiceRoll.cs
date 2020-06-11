using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleMonopoly
{
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
}
