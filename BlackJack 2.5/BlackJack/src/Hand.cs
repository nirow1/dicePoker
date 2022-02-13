using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public class Hand
    {
        //List of hands 
        public Hand()
        {
            Value = 0;
            Cards = new List<Card>();
        }

        //defining what list holds 
        public Hand(Hand obj)
        {
            this.Value = obj.Value;
            this.Cards = obj.Cards.ConvertAll(card => new Card(card));
        }

        public void AddCard(Card card)
        {
            //checking hand for ace if the hand value is over 21
            if (Value + card.IntValue > 21)
            {
                for (int i = 0; i < Cards.Count; i++)
                {
                    if (this.Cards[i].IntValue == 11)
                    {
                        this.Cards[i] = new Card(1, this.Cards[i].Suite);
                        Value -= 10;
                        break;
                    }
                }
            }

            if (card.IntValue == 11 && Value + card.IntValue > 21)
            {
                Cards.Add(new Card(1, card.Suite));
                Value++;
            }
            else
            {
                Cards.Add(card);
                Value += card.IntValue;
            }
        }

        //defining when did player lose, win or push 
        public GameResult GetHandResult(Hand dealerHand)
        {
            if ((Value > dealerHand.Value || dealerHand.Value > 21) && Value <= 21)
                return GameResult.WIN;
            else if (Value == dealerHand.Value)
                return GameResult.PUSH;
            else
                return GameResult.LOSE;
        }

        public int Value;

        public List<Card> Cards;
    }
}
