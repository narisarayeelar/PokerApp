using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Poker
{
    public class PokerManager
    {
        private List<Poker> pokers = new List<Poker>();
        private int numberOfPlayers;
        private string game = "";
        private bool gameErrorFlag = false;

        public List<Poker> Pokers { get => pokers; set => pokers = value; }
        public int NumberOfPlayers { get => numberOfPlayers; set => numberOfPlayers = value; }

        private string GetValue(string line)
        {
            try
            {
                return line.Substring(line.IndexOf(":") + 1);
            }catch(Exception ex)
            {
                return "";
            }
        }
        public void LoadData(string textFile)
        {
            string[] lines = File.ReadAllLines(textFile);
            
            foreach (string line in lines)
            {
                string value = GetValue(line);

                if (line.Contains("Number of players"))
                {
                    this.numberOfPlayers = value != ""? int.Parse(value): 0 ;
                }

                if (this.numberOfPlayers <= 0) return;

                for(int i= 1; i <= this.numberOfPlayers; i++)
                {
                    if (line.Contains("Player"+i.ToString()))
                    {
                        Poker poker = new Poker(value);
                        this.pokers.Add(poker);
                    }
                }


                
                if (line.Contains("Game"))
                {
                    //Play
                    ClearPokers();
                    //Console.WriteLine(line);
                    gameErrorFlag = false;
                    game = line;
                }

                foreach(Poker p in this.pokers)
                {
                    if (line.Contains(p.Name + " hand"))
                    {
                       bool success =  p.SetCardsInHand(value.Trim());
                        if (success == false && gameErrorFlag == false)
                        {
                            Console.WriteLine("{0} result: {1}", this.game, p.ErrorMessage);
                            gameErrorFlag = true;
                            break;
                        }
                    }
                }

                Play();

            }
                
        }


        private void ClearPokers()
        {
            foreach(Poker p in this.pokers)
            {
                p.Clear();
            }
        }

        public bool CheckAllPokerHaveCards()
        {
            if (this.pokers.Count < this.numberOfPlayers) return false;

            foreach(Poker p in this.pokers)
            {
                if (p.Cards == null) return false;
            }

            return true;
        }

        public void Play()
        {
            if (CheckAllPokerHaveCards() == false) return;
            if (CheckDuplicateCards() == false)
            {
                Console.WriteLine("{0} result: Duplication of cards. More than one card of the same type exist in one or more player hands.", this.game);
                //Console.WriteLine("----------------------------------------------------------------");
                return;
            }
            foreach (Poker p in this.pokers)
            {
                p.CheckCardsInHand();
               // string description = EnumExtensions.GetDescription(p.Pokerhand);
               // Console.WriteLine(" {0}  with {1} : {2}", p.Name, p.StrCard, description);
            }

            FindTheWinners(this.pokers);
            //Console.WriteLine("----------------------------------------------------------------");
        }

        public bool CheckDuplicateCards()
        {
            List<string> AllCard = new List<string>();

            foreach (Poker p in this.pokers)
            {
                foreach(Card c in p.Cards)
                {
                    AllCard.Add(c.Rank.ToString() + c.Suit);
                }
            }

            int countDistinct = AllCard.ToArray().Distinct().Count();
            if(countDistinct != AllCard.Count())
            {
                return false;
            }

            return true;
        }


        private void FindTheWinners(List<Poker> listPokers)
        {
            POKERHANDS pokerHand = listPokers.Max(x => x.Pokerhand);
            List<Poker> winnerList = listPokers.Where(p => p.Pokerhand == pokerHand).ToList();

            int index = 1;
            int maxIndex = 1;

            if (pokerHand == POKERHANDS.TwoPairs)
            {
                maxIndex = 2;
            }
            if (pokerHand == POKERHANDS.Flush || pokerHand == POKERHANDS.Straight || pokerHand == POKERHANDS.HighCard || pokerHand == POKERHANDS.StraightFlush)
            {
                maxIndex = 0;
            }

            
            while (index <= maxIndex && winnerList.Count>1)
            {
                if (winnerList.Count > 0 && index <= maxIndex)
                {
                    winnerList = winnerList.Where(p => p.SpecialRank[index - 1] == GetMaxSpeicalRank(winnerList, index - 1)).ToList();
                }
                index++;
            }
            

            if (winnerList.Count()> 1)
            {
                winnerList = FindTheWinnerList(winnerList);
            }

            //
            string winNames = "";
            foreach (Poker p in winnerList)
            {
                winNames = winNames + " " + p.Name;
            }

            string description = EnumExtensions.GetDescription(pokerHand);
            Console.WriteLine("{0} result: {1} {2} with {3}",this.game, winNames.Trim(), winnerList.Count == 1?"win":"tie", description);

        }

        private int GetMaxSpeicalRank(List<Poker> listPokers,int index)
        {
            int[] ranks = new int[listPokers.Count];
            int i = 0;
            foreach (Poker p in listPokers)
            {
                ranks[i] = p.SpecialRank[index];
                i++;
            }

            return ranks.Max();
        }

        private List<Poker> FindTheWinnerList( List<Poker> listPokers)
        {
            bool finishLoop = true;

            foreach(Poker p in listPokers)
            {
                if(p.Ranks.Max() > 0)
                {
                    finishLoop = false;
                    break;
                }
            }

            if (finishLoop)
            {
                return listPokers;
            }

            List<Poker> result = new List<Poker>();
            int[] ranks = new int[listPokers.Count];

            int i = 0;
            foreach(Poker p in listPokers)
            {
                Array.Sort(p.Ranks);
                ranks[i] = p.Ranks.Max();
                i++;
            }

            int maxRank = ranks.Max();

            result = listPokers.Where(a => a.Ranks.Max() == maxRank).ToList();

            foreach(Poker p in result)
            {
                for(int index = 0; index < p.Ranks.Length; index++)
                {
                    if (p.Ranks[index] == maxRank) p.Ranks[index] = 0;
                }
            }

            return FindTheWinnerList(result);

        }

    }


}
