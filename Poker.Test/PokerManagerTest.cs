using NUnit.Framework;

namespace Poker.Test
{
    [TestFixture]
    public class PokerManagerTest
    {
        [TestCase("2H 5S KD KS 4D", "2H 5S KD KS 4D", "2H 5S KD KS 4D", "2H 5S KD KS 4D")]
        public void Test_CheckAllPokerHaveCards(string s1, string s2, string s3, string s4)
        {
            PokerManager m = new PokerManager();
            Poker p1 = CreatePoker("P1", s1);
            Poker p2 = CreatePoker("P2", s2);
            Poker p3 = CreatePoker("P3", s3);
            Poker p4 = CreatePoker("P4", s4);
            m.NumberOfPlayers = 4;
            m.Pokers.Add(p1);
            m.Pokers.Add(p2);
            m.Pokers.Add(p3);
            m.Pokers.Add(p4);

            var result = m.CheckAllPokerHaveCards();
            Assert.IsTrue(result);

        }

        [TestCase("2H 5S KD KS 4D", "2H 5S KD KS 4D", "2H 5S KD KS 4D", "")]
        public void Test_CheckAllPokerHaveCards_False(string s1, string s2, string s3, string s4)
        {
            PokerManager m = new PokerManager();
            Poker p1 = CreatePoker("P1", s1);
            Poker p2 = CreatePoker("P2", s2);
            Poker p3 = CreatePoker("P3", s3);
            Poker p4 = CreatePoker("P4", s4);
            m.NumberOfPlayers = 4;
            m.Pokers.Add(p1);
            m.Pokers.Add(p2);
            m.Pokers.Add(p3);
            m.Pokers.Add(p4);

            var result = m.CheckAllPokerHaveCards();
            Assert.IsFalse(result);

        }

        [TestCase("2H 5S KD KS 4D", "QC AS TD QS 9D", "JC KC 3D 6S JH", "8S 7H TC 4C TS")]
        [TestCase("2H 5S KD KS 4D", "QC AS TD QS 9D", "JC KC 3D 6S JH", "8S 8H 8C 4C 4S")]
        public void Test_CheckDuplicateCards_Not_Duplicate(string s1, string s2, string s3, string s4)
        {
            PokerManager m = new PokerManager();
            Poker p1 = CreatePoker("P1", s1);
            Poker p2 = CreatePoker("P2", s2);
            Poker p3 = CreatePoker("P3", s3);
            Poker p4 = CreatePoker("P4", s4);
            m.NumberOfPlayers = 4;
            m.Pokers.Add(p1);
            m.Pokers.Add(p2);
            m.Pokers.Add(p3);
            m.Pokers.Add(p4);

            var result = m.CheckDuplicateCards();
            Assert.IsTrue(result);

        }

        [TestCase("2H 5S KD KS 4D", "QC 2H TD QS 9D", "JC KC 3D 6S JH", "8S 7H TC 4C TS")]
        [TestCase("2H 5S KD KS 4D", "QC AS 6S QS 9D", "JC KC 3D 6S JH", "8S 8H 8C 6S 4S")]
        public void Test_CheckDuplicateCards_Duplicate(string s1, string s2, string s3, string s4)
        {
            PokerManager m = new PokerManager();
            Poker p1 = CreatePoker("P1", s1);
            Poker p2 = CreatePoker("P2", s2);
            Poker p3 = CreatePoker("P3", s3);
            Poker p4 = CreatePoker("P4", s4);
            m.NumberOfPlayers = 4;
            m.Pokers.Add(p1);
            m.Pokers.Add(p2);
            m.Pokers.Add(p3);
            m.Pokers.Add(p4);

            var result = m.CheckDuplicateCards();
            Assert.IsFalse(result);

        }

        private Poker CreatePoker(string name,string strCard)
        {
            Poker p = new Poker(name);
            p.SetCardsInHand(strCard);
            return p;
        }
    }
}
