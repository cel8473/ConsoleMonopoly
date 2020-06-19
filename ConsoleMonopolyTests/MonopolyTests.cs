using NUnit.Framework;
using ConsoleMonopoly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using NUnit.Framework.Internal;

namespace ConsoleMonopoly.Tests
{
    [TestFixture()]
    public class MonopolyTests
    {
        [Test()]
        public void MonopolyCheckerTest()
        {
            int[] OriAndVerRent = new int[6] { 6, 30, 90, 270, 400, 550 };
            RegularProperty oriental = new RegularProperty("Oriental Avenue", 100, 6, 50, OriAndVerRent, RegularProperty.ColorGroup.LightBlue);
            RegularProperty vermont = new RegularProperty("Vermont Avenue", 100, 8, 50, OriAndVerRent, RegularProperty.ColorGroup.LightBlue);
            int[] ConnectRent = new int[6] { 8, 40, 100, 300, 450, 600 };
            RegularProperty connecticut = new RegularProperty("Connecticut Avenue", 120, 9, 50, ConnectRent, RegularProperty.ColorGroup.LightBlue);
            int[] PPRent = new int[6] { 35, 175, 500, 1100, 1300, 1500 };
            RegularProperty parkPlace = new RegularProperty("Park Place", 350, 37, 200, PPRent, RegularProperty.ColorGroup.DarkBlue);
            int[] BWRent = new int[6] { 50, 200, 600, 1400, 1700, 2000 };
            RegularProperty boardWalk = new RegularProperty("Boardwalk", 400, 39, 200, BWRent, RegularProperty.ColorGroup.DarkBlue);
            IProperty[] owned = new IProperty[] { oriental, vermont, parkPlace, boardWalk };
            Player testPlayer = new Player("Chris", Player.Token.RaceCar, 1500, owned);
            Assert.IsTrue(Monopoly.MonopolyChecker(testPlayer, boardWalk));
            Assert.IsFalse(Monopoly.MonopolyChecker(testPlayer, vermont));
            testPlayer.OwnedProperties.Append(connecticut);
            Assert.IsTrue(Monopoly.MonopolyChecker(testPlayer, connecticut));
        }

        [Test()]
        public void RentCalculatorTest()
        {
            Board board = new Board(1);
            int roll = 1;
            IProperty[] owned = null;
            Player testPlayer = new Player("Chris", Player.Token.RaceCar, 1500, owned);
            Player testOwner = new Player("Bob", Player.Token.Battleship, 1500, owned);
            /* Mediterranean Ave Not Owned */ 
            testPlayer.Location = 1;
            int rent = Monopoly.RentCalculator(board, testPlayer, roll);
            Assert.AreEqual(rent, -1);
            /* Owned */
            board.Properties[1].Owner = testOwner;
            rent = Monopoly.RentCalculator(board, testPlayer, roll);
            Assert.AreEqual(rent, 2);
            Assert.AreEqual(testOwner.Funds, 1502);
            Assert.AreEqual(testPlayer.Funds, 1498);
            /* Mortgaged Mediterranean*/
            board.Properties[1].IsMortgaged = true;
            rent = Monopoly.RentCalculator(board, testPlayer, roll);
            Assert.AreEqual(rent, 0);
            Assert.AreEqual(testOwner.Funds, 1500);
            Assert.AreEqual(testPlayer.Funds, 1500);
            /* Monopoly 0 Houses Medit*/
            RegularProperty mediterranean = (RegularProperty)board.Properties[1];
            mediterranean.Monopoly = true;
            rent = Monopoly.RentCalculator(board, testPlayer, roll);
            Assert.AreEqual(rent, 4);
            /* 1 House */
            mediterranean.NumOfHouses = 1;
            rent = Monopoly.RentCalculator(board, testPlayer, roll);
            Assert.AreEqual(rent, 10);
            testPlayer.Funds = 1500;
            testOwner.Funds = 1500;
            /* Community Chest */
            testPlayer.Location = 2;
            rent = Monopoly.RentCalculator(board, testPlayer, roll);
            Assert.AreEqual(rent, -2);
            /* Reading RR */
            testPlayer.Location = 5;
            board.Properties[5].Owner = testOwner;
            rent = Monopoly.RentCalculator(board, testPlayer, roll);
            Assert.AreEqual(rent, 25);
            Assert.AreEqual(testOwner.Funds, 1525);
            Assert.AreEqual(testPlayer.Funds, 1475);
            /* Second RR */
            RailRoadProperty reading = (RailRoadProperty)board.Properties[5];
            reading.NumOfRRs = 2;
            rent = Monopoly.RentCalculator(board, testPlayer, roll);
            Assert.AreEqual(rent, 50);
            testPlayer.Funds = 1500;
            testOwner.Funds = 1500;
            /* Electric Company */
            testPlayer.Location = 12;
            board.Properties[12].Owner = testOwner;
            rent = Monopoly.RentCalculator(board, testPlayer, roll);
            Assert.AreEqual(rent, 4);
            Assert.AreEqual(testOwner.Funds, 1504);
            Assert.AreEqual(testPlayer.Funds, 1496);
            testPlayer.Funds = 1500;
            testOwner.Funds = 1500;
            /* Both Utilities Owned By Own*/
            UtilityProperty electric = (UtilityProperty)board.Properties[12];
            electric.Monopoly = true;
            rent = Monopoly.RentCalculator(board, testPlayer, roll);
            Assert.AreEqual(rent, 10);
            Assert.AreEqual(testOwner.Funds, 1510);
            Assert.AreEqual(testPlayer.Funds, 1490);
            testPlayer.Funds = 1500;
            testOwner.Funds = 1500;
        }

        [Test()]
        public void CommChestCardsTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void ChanceCardsTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void PropertyCheckerTest()
        {
            Assert.Fail();
        }
    }
}