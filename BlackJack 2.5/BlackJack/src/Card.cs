using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{


    public class Card
    {
        public Card(int Value, Suites Suite)
        {
            //checking if the cards have the right values
            if (Value <= 0 || Value > 14 || !Enum.IsDefined(typeof(Suites),Suite))
                throw new ArgumentException("Invalid class Card constructor argument");

            this.Value = Value;
            this.Suite = Suite;
        }

        public Card(Card card)
        {
            this.Value = card.Value;
            this.Suite = card.Suite;
        }


        public int Value { get; set; }// sets value of the card
        public Suites Suite { get; set; }// sets suite of the card

        //if you want to just get the named value
        public string NamedValue
        {
            get
            {
                string name = string.Empty;
                switch (Value)
                {
                    case (14):
                        name = "A";
                        break;
                    case (13):
                        name = "K";
                        break;
                    case (12):
                        name = "Q";
                        break;
                    case (11):
                        name = "J";
                        break;
                    default:
                        name = Value.ToString();
                        break;
                }

                return name;
            }
        }

        //blackjack card value
        public int IntValue
        {
            get
            {
                int black;
                switch (Value)
                {
                    case (14):
                        black = 11;
                        break;
                    case (13):
                        black = 10;
                        break;
                    case (12):
                        black = 10;
                        break;
                    case (11):
                        black = 10;
                        break;
                    default:
                        black = Value;
                        break;
                }

                return black;
            }
        }

        /// <summary>
        /// Override of operator ==
        /// </summary>
        /// <param value="obj1"></param>
        /// <param value="obj2"></param>
        /// <returns>True if instances are equal, false if not</returns>
        public static bool operator ==(Card obj1, Card obj2)
        {
            return (obj1.Equals(obj2));
        }

        // override operator !=
        public static bool operator !=(Card obj1, Card obj2)
        {
            return !(obj1.Equals(obj2));
        }

        // override Equals
        public override bool Equals(Object obj)
        {
            return this.Value == ((Card)obj).Value;
        }

        public override int GetHashCode()
        {
            return Guid.NewGuid().GetHashCode();
        }
    }
}
