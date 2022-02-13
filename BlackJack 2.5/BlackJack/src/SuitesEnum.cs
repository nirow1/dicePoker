using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{

    //enum of all posible suites
    public enum Suites
    {
        Hearts = 0,
        Diamonds = 1,
        Clubs = 2,
        Spades = 3
    }

    //gets character from encoder tab for every possible suit
    public static class SuitesExtensions
    {
        public static char ToChar(this Suites me)
        {
            switch (me)
            {
                case Suites.Hearts:
                    return Encoding.GetEncoding(437).GetChars(new byte[] { 3 })[0];

                case Suites.Diamonds:
                    return Encoding.GetEncoding(437).GetChars(new byte[] { 4 })[0];

                case Suites.Clubs:
                    return Encoding.GetEncoding(437).GetChars(new byte[] { 5 })[0];

                case Suites.Spades:
                    return Encoding.GetEncoding(437).GetChars(new byte[] { 6 })[0];
                default:
                    return '?';
            }
        }
    }
}
