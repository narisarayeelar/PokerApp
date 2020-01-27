using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poker
{
    public class Poker
    {
        private string name;
        private POKERHANDS pokerhand;
        private Card[] cards;
        private string[] suits;
        private int[] ranks;
        private string errorMessage="";
        private string strCard;
        private int[] specialRank ;

        public Poker(string name)
        {
            this.name = name;
        }

        public string Name { get => name; set => name = value; }
        public string ErrorMessage { get => errorMessage; set => errorMessage = value; }
        public POKERHANDS Pokerhand { get => pokerhand; set => pokerhand = value; }
        public Card[] Cards { get => cards; set => cards = value; }
        public int[] Ranks { get => ranks; set => ranks = value; }
        public string StrCard { get => strCard; set => strCard = value; }
        public int[] SpecialRank { get => specialRank; set => specialRank = value; }

        public bool SetCardsInHand(string strCard)
        {
            try
            {
                this.strCard = strCard;
                cards = new Card[Constants.NumCardsInHand];
                suits = new string[Constants.NumCardsInHand];
                ranks = new int[Constants.NumCardsInHand];

                var arrCard = strCard.Split(" ");

                if (arrCard.Length != Constants.NumCardsInHand)
                {
                    throw new Exception(string.Format("Incorrect number of cards in {0} hand", this.name));
                }

                if (arrCard.Distinct().Count() != arrCard.Count())
                {
                    throw new Exception("Duplication of cards. More than one card of the same type exist in one or more player hands.");
                }

                for (int i=0;i<arrCard.Length;i++)
                {
                    var str = arrCard[i];

                    if (str.Length != 2)
                    {
                        throw new Exception(string.Format("Card: {0} is invalid format.", str));
                    }

                    Card card = new Card(str[1].ToString(), str[0].ToString());

                    if (card.ValidCard == false)
                    {
                        throw new Exception(string.Format("Card: {0} is invalid format.", str));
                    }

                    this.cards[i] = card;
                }

                for(int i=0;i< this.cards.Length; i++)
                {
                    suits[i] = this.cards[i].Suit;

                    int rank = 0;
                    switch (this.cards[i].Rank) {
                        case "T":
                            rank = 10;
                            break;
                        case "J":
                            rank = 11;
                            break;
                        case "Q":
                            rank = 12;
                            break;
                        case "K":
                            rank = 13;
                            break;
                        case "A":
                            rank = 14;
                            break;
                        default:
                            rank = int.Parse(this.cards[i].Rank);
                            break;

                    }

                    ranks[i] = rank;
                }


                return true;
            }catch(Exception ex)
            {
                cards = null;
                suits = null;
                ranks = null;
                this.errorMessage = ex.Message;
                return false;
            }
        }

        public void CheckCardsInHand()
        {
            if (StraightFlush()) return;
            if (FourOfAKind()) return;
            if (FullHouse()) return;
            if (Flush()) return;
            if (Straight()) return;
            if (ThreeOfAKind()) return;
            if (TwoPair()) return;
            if (Pair()) return;

            //Default
            this.pokerhand = POKERHANDS.HighCard;
        }
        private bool CheckConsecutiveCards(int[] tmp)
        {
            Array.Sort(tmp);
            int preVal = tmp[0];
            for (int i =1; i< tmp.Length; i++)
            {   
                if (tmp[i] != preVal + 1)
                {
                    return false;
                }

                preVal++;

            }

            return true;
        }

        private bool StraightFlush()
        {
            //5 cards in rank orderm same suit
            bool isStraightFlush = true;

            if (this.suits.Distinct().Count() != 1) return isStraightFlush = false;

            int[] tmp = new int[Constants.NumCardsInHand];
            this.ranks.CopyTo(tmp,0);

            isStraightFlush = CheckConsecutiveCards(tmp);

            //IF HAVE "A" Check again
            if (isStraightFlush == false)
            {
                if (tmp[tmp.Length - 1] == 14) tmp[tmp.Length - 1] = 1;
                isStraightFlush = CheckConsecutiveCards(tmp);
            }

            if (isStraightFlush)
            {
                this.pokerhand = tmp[0] == 10 ? POKERHANDS.RoyalStraightFlush : POKERHANDS.StraightFlush;
                //this.specialRank = new int[1];
                //if (this.suits[0] == "C") this.specialRank[0] = 1000;
                //if (this.suits[0] == "D") this.specialRank[0] = 2000;
                //if (this.suits[0] == "H") this.specialRank[0] = 3000;
                //if (this.suits[0] == "H") this.specialRank[0] = 4000;
            }

            return isStraightFlush;
        }

        private bool FourOfAKind()
        {
            //4 Same rank
           var rankDistinct = this.ranks.Distinct();

            foreach (int val in rankDistinct)
            {
                if (this.ranks.Count(x => x == val) == 4)
                {
                    this.pokerhand = POKERHANDS.FourOfAKind;
                    this.specialRank = new int[1];
                    this.specialRank[0] = val;
                    return true;
                }
            }

            return false;

        }

        private bool FullHouse()
        {
            var rankDistinct = this.ranks.Distinct();
            bool hasThree = false;
            bool hasPair = false;
            int maxRank = 0;

            foreach (int val in rankDistinct)
            {
                var count = this.ranks.Count(x => x == val);
                if (count == 3)
                {
                    hasThree = true;
                    maxRank = val;
                }
                if(count == 2) hasPair =  true;
            }

            if(hasThree && hasPair)
            {
                this.pokerhand = POKERHANDS.FullHouse;
                this.specialRank = new int[1];
                this.specialRank[0] = maxRank;
                return true;
            }

            return false;
        }

        private bool Flush()
        {
            if(this.suits.Distinct().Count() == 1)
            {
                this.pokerhand = POKERHANDS.Flush;
                return true;
            }

            return false;
        }

        private bool Straight()
        {
            var isStraight = false;
            int[] tmp = new int[Constants.NumCardsInHand];
            this.ranks.CopyTo(tmp, 0);

            isStraight = CheckConsecutiveCards(tmp);

            //IF HAVE "A" Check again
            if (isStraight == false)
            {
                if (tmp[tmp.Length - 1] == 14) tmp[tmp.Length - 1] = 1;
                isStraight = CheckConsecutiveCards(tmp);
            }

            if (isStraight)
            {
                this.pokerhand = POKERHANDS.Straight;
            }
            return isStraight;
        }

        private bool ThreeOfAKind()
        {
            //3 Same rank
            var rankDistinct = this.ranks.Distinct();

            foreach (int val in rankDistinct)
            {
                if (this.ranks.Count(x => x == val) == 3)
                {
                    this.pokerhand = POKERHANDS.ThreeOfSKind;
                    this.specialRank = new int[1];
                    this.specialRank[0] = val;
                    return true;
                }
            }

            return false;
        }

        private bool TwoPair()
        {
            var rankDistinct = this.ranks.Distinct();
            int pairCount = 0;
            List<int> pairRank = new List<int>();
            foreach (int val in rankDistinct)
            {
                var count = this.ranks.Count(x => x == val);
                if (count == 2)
                {
                       pairRank.Add(val);
                       pairCount++;
                }
            }

            if (pairCount==2)
            {
                this.pokerhand = POKERHANDS.TwoPairs;
                this.specialRank = pairRank.ToArray();
                Array.Sort(this.specialRank);
                Array.Reverse(this.specialRank);
                return true;
            }

            return false;
        }

        private bool Pair()
        {
            var rankDistinct = this.ranks.Distinct();
            int pairCount = 0;
            int pairRank = 0;

            foreach (int val in rankDistinct)
            {
                var count = this.ranks.Count(x => x == val);
                if (count == 2) {
                    pairCount++;
                    pairRank = val;
                }
                
            }

            if (pairCount == 1)
            {
                this.pokerhand = POKERHANDS.Pair;
                this.specialRank = new int[1];
                this.specialRank[0] =  pairRank;
                return true;
            }


            return false;
        }



        public void Clear()
        {

           pokerhand = POKERHANDS.None;
           cards = null;
           suits = null;
           ranks = null;
           errorMessage = "";
           strCard = "";
           specialRank = null;
        }

       
    }
}
