using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public class Deck
    {
        public Deck()
        {
            FillDeck();
        }

        public void FillDeck()
        {
            if (Cards.Count != 0)// if the count of cards is not 0 deletes remaining cards in deck 
                Cards.Clear();

            List<Card> tmpDeck = new List<Card>();
            //Can use a single loop utilising the mod operator % and Math.Floor
            //Using divition based on 13 cards in a suited
            for (int i = 0; i < 52; ++i)
            {
                Suites suite = (Suites)(Math.Floor((decimal)i / 13));
                //Add 2 to value as a cards start a 2
                int val = i % 13 + 2;
                for (int j = 0; j < 4; ++j)
                {
                    tmpDeck.Add(new Card(val, suite));
                }
            }

            //shuffling the cards
            Random rnd = new Random();

            //while the count of all cards in deck is bigger than 1
            int n = tmpDeck.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                Card value = tmpDeck[k];//takes card on random position <n+1> in temporal deck
                tmpDeck[k] = tmpDeck[n];//puts card n on position of the random card
                tmpDeck[n] = value;//changes card on position n
            }

            Cards = new Queue<Card>(tmpDeck);
        }

        public Card GetCard()
        {
            //if the count of all cards is less than 4, method filldeck is called, which fills up the deck with new cards
            if (Cards.Count < 4)
                FillDeck();

            return Cards.Dequeue();
        }

        private Queue<Card> Cards = new Queue<Card>();// queue  of all cards
    }
}
