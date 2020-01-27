using NUnit.Framework;

namespace Poker.Test
{
    [TestFixture]
    public class CardTest
    {
        [TestCase("C","2")]
        public void Test_Create_Card(string suit, string rank)
        {
            var card = new Card(suit, rank);
            Assert.IsNotNull(card);
            Assert.AreEqual(suit, card.Suit);
            Assert.AreEqual(rank, card.Rank);
            Assert.IsTrue(card.ValidCard);
        }

        [TestCase("2", "C")]
        [TestCase("Z", "T")]
        public void Test_Create_Invalid_Card(string suit, string rank)
        {
            var card = new Card(suit, rank);
            Assert.IsFalse(card.ValidCard);
            Assert.IsNull(card.Suit);
            Assert.IsNull(card.Rank);
        }


    }
}
