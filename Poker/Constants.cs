
using System;
using System.ComponentModel;
using System.Reflection;

namespace Poker
{
    static class Constants
    {
        public const int NumCardsInHand = 5;
    }

    public static class EnumExtensions
    {
        public static string GetDescription(POKERHANDS enumerationValue) 
        {
            var type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException($"{nameof(enumerationValue)} must be of Enum type", nameof(enumerationValue));
            }
            var memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo.Length > 0)
            {
                var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            return enumerationValue.ToString();
        }
    }

    public enum POKERHANDS
    {
        [Description("none")]
        None = 0,
        [Description("royal straight flush")]
        RoyalStraightFlush = 10,
        [Description("straight flush")]
        StraightFlush = 9,
        [Description("four of a kind")]
        FourOfAKind = 8,
        [Description("full house")]
        FullHouse = 7,
        [Description("flush")]
        Flush = 6,
        [Description("straight")]
        Straight = 5,
        [Description("three of a kind")]
        ThreeOfSKind = 4,
        [Description("two pairs")]
        TwoPairs = 3,
        [Description("pair")]
        Pair = 2,
        [Description("high card")]
        HighCard = 1
    }
}
