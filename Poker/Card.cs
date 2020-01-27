using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Poker
{
    public class Card
    {
        //C,D,H,S
        private string suit;
        //2,3,4,5,6,7,8,9,T,J,Q,K,A
        private string rank;

        private bool validCard;

        //Constructor
        public Card(string suit, string rank)
        {
            validCard = ValidateCard(suit, rank);

            if (validCard)
            {
                this.suit = suit;
                this.rank = rank;
            }
        }


        //Property
        public string Suit { get => suit; set => suit = value; }
        public string Rank { get => rank; set => rank = value; }
        public bool ValidCard { get => validCard; set => validCard = value; }

        //Private Method
        #region Private Method
        private bool ValidateCard(string suit, string rank) {
            var valid = false;
            string suitPattern = @"[^CDHS]";
            var regex = new Regex(suitPattern);
            var match = regex.Match(suit);
            valid = (match.Length > 0 ? false : true);

            if (valid)
            {
                string rankPattern = @"[^23456789TJQKA]";
                regex = new Regex(rankPattern);
                match = regex.Match(rank);
                valid = (match.Length > 0 ? false : true);
            }

            return valid;
        }

        #endregion
        //Public Method

    }
}
