using NUnit.Framework;

namespace Poker.Test
{
    [TestFixture]
    public class PokerTest
    {
        [TestCase("2H 5S KD KS 4D")]
        [TestCase("QC AS TD QS 9D")]
        [TestCase("JC KC 3D 6S JH")]
        [TestCase("8S 7H TC 4C TD")]
        [TestCase("2H 5S KD KS 4D")]
        [TestCase("QC AS TD QS 9D")]
        [TestCase("JC KC 3D 6S JH")]
        [TestCase("8S 8H 8C 4C 4D")]
        public void Test_setCardsInHand_Valid(string strCard)
        {
            Poker poker = new Poker("Test");
            var success = poker.SetCardsInHand(strCard);

            Assert.IsTrue(success);
        }

        [TestCase("8S 8H 8C 4C")]
        [TestCase("QC AS TD QS 9D 8S")]
        [TestCase("QC AS")]
        [TestCase("JC")]
        public void Test_setCardsInHand_InValid_IncorrectNumberOfCards(string strCard)
        {
            Poker poker = new Poker("Test");
            var success = poker.SetCardsInHand(strCard);

            Assert.IsFalse(success);
            Assert.IsTrue(poker.ErrorMessage.Contains("Incorrect number of cards"));
        }

        [TestCase("QC QC TD QS 9D")]
        [TestCase("QC QC TD TD 9D")]
        public void Test_setCardsInHand_InValid_DuplicationOfCards(string strCard)
        {
            Poker poker = new Poker("Test");
            var success = poker.SetCardsInHand(strCard);

            Assert.IsFalse(success);
            Assert.IsTrue(poker.ErrorMessage.Contains("Duplication of cards"));
        }

        [TestCase("2C 3C 5C 4C 6C")]
        [TestCase("2C 3C 5C 4C AC")]
        [TestCase("9C TC KC QC JC")]
        public void Test_CheckCardsInHand_StraghtFlush(string strCard)
        {
            Poker poker = new Poker("Test");
            var success = poker.SetCardsInHand(strCard);
            poker.CheckCardsInHand();

            Assert.IsTrue(poker.Pokerhand == POKERHANDS.StraightFlush);
        }

        [TestCase("2C 3C 5C 7C 6C")]
        [TestCase("2C 3C 5C 4H AC")]
        [TestCase("9C AC KC QC JC")]
        public void Test_CheckCardsInHand_NOT_StraghtFlush(string strCard)
        {
            Poker poker = new Poker("Test");
            var success = poker.SetCardsInHand(strCard);
            poker.CheckCardsInHand();

            Assert.IsFalse(poker.Pokerhand == POKERHANDS.StraightFlush);
        }

        [TestCase("TC JC QC KC AC")]
        [TestCase("AC TC QC KC JC")]
        [TestCase("TD JD QD KD AD")]
        [TestCase("TH JH QH KH AH")]
        [TestCase("TS JS QS KS AS")]
        public void Test_CheckCardsInHand_RoyalStraghtFlush(string strCard)
        {
            Poker poker = new Poker("Test");
            var success = poker.SetCardsInHand(strCard);
            poker.CheckCardsInHand();

            Assert.IsTrue(poker.Pokerhand == POKERHANDS.RoyalStraightFlush);
        }

        [TestCase("6C 6D 6H 6S AC")]
        [TestCase("AC AD AH AS TS")]
        public void Test_CheckCardsInHand_Four_of_a_kind(string strCard)
        {
            Poker poker = new Poker("Test");
            var success = poker.SetCardsInHand(strCard);
            poker.CheckCardsInHand();

            Assert.IsTrue(poker.Pokerhand == POKERHANDS.FourOfAKind);
        }

        [TestCase("6C 6D 6H 5S 5C")]
        [TestCase("6C 6D 7H 5S 5C")]
        public void Test_CheckCardsInHand_NOT_Four_of_a_kind(string strCard)
        {
            Poker poker = new Poker("Test");
            var success = poker.SetCardsInHand(strCard);
            poker.CheckCardsInHand();

            Assert.IsFalse(poker.Pokerhand == POKERHANDS.FourOfAKind);
        }

        [TestCase("4S 4H 4C JH JS")]
        [TestCase("8H 8C 8D 4H 4C")]
        [TestCase("8S 8H 8C 4C 4D")]
        public void Test_CheckCardsInHand_FullHouse(string strCard)
        {
            Poker poker = new Poker("Test");
            var success = poker.SetCardsInHand(strCard);
            poker.CheckCardsInHand();

            Assert.IsTrue(poker.Pokerhand == POKERHANDS.FullHouse);
        }

        [TestCase("AH QH TH 5H 3H")]
        [TestCase("TD 8D 7D 6D 5D")]
        public void Test_CheckCardsInHand_Flush(string strCard)
        {
            Poker poker = new Poker("Test");
            var success = poker.SetCardsInHand(strCard);
            poker.CheckCardsInHand();

            Assert.IsTrue(poker.Pokerhand == POKERHANDS.Flush);
        }

        [TestCase("TC JC QC KC AD")]
        [TestCase("AC TC QC KC JH")]
        [TestCase("TD JD QD KD AH")]
        [TestCase("TH JH QS KH AH")]
        [TestCase("TD JS QS KS AS")]
        public void Test_CheckCardsInHand_RoyalStraght(string strCard)
        {
            Poker poker = new Poker("Test");
            var success = poker.SetCardsInHand(strCard);
            poker.CheckCardsInHand();

            Assert.IsTrue(poker.Pokerhand == POKERHANDS.Straight);
        }

        [TestCase("6C 6D 6H 7S AC")]
        public void Test_CheckCardsInHand_Three_of_a_kind(string strCard)
        {
            Poker poker = new Poker("Test");
            var success = poker.SetCardsInHand(strCard);
            poker.CheckCardsInHand();

            Assert.IsTrue(poker.Pokerhand == POKERHANDS.ThreeOfSKind);
        }

        [TestCase("6C 6D 7H 7S AC")]
        [TestCase("TC AD 2H AS TS")]
        public void Test_CheckCardsInHand_TwoPairs(string strCard)
        {
            Poker poker = new Poker("Test");
            var success = poker.SetCardsInHand(strCard);
            poker.CheckCardsInHand();

            Assert.IsTrue(poker.Pokerhand == POKERHANDS.TwoPairs);
        }

        [TestCase("6C 6D 7H 8S AC")]
        [TestCase("4C AD 2H AS TS")]
        public void Test_CheckCardsInHand_Pairs(string strCard)
        {
            Poker poker = new Poker("Test");
            var success = poker.SetCardsInHand(strCard);
            poker.CheckCardsInHand();

            Assert.IsTrue(poker.Pokerhand == POKERHANDS.Pair);
        }
        //10♣ 8♠ 7♠ 6♥ 4♦ 
        //Q♥ 10♦ 7♠ 5♠ 2♥
        [TestCase("TC 8S 7S 6H 4D")]
        [TestCase("QH TD 7S 5S 2H")]
        public void Test_CheckCardsInHand_HighCard(string strCard)
        {
            Poker poker = new Poker("Test");
            var success = poker.SetCardsInHand(strCard);
            poker.CheckCardsInHand();

            Assert.IsTrue(poker.Pokerhand == POKERHANDS.HighCard);
        }
    }
}
