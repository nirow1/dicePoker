using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public class GameRunner
    {
        // beggining of the game
        public GameRunner()
        {
            WriteLine("Press any key to start...");
            ReadKey();
        }

        //redefining WriteLine, ReadKey, and ReadLine to better fit my purpose
        public void WriteLine(string text)
        {
            Console.SetCursorPosition(0, ycoor);
            Console.WriteLine(text);
            ycoor++;
        }

        public void ReadKey()
        {
            Console.SetCursorPosition(0, ycoor);
            Console.ReadKey();
        }

        public string ReadLine()
        {
            Console.SetCursorPosition(0, ycoor);
            ycoor++;
            return Console.ReadLine();
        }

        public void DrawCard(ref Card card)
        {
            if (cardguiy == 0)
            {
                cardguiy = ycoor;
            }

            int x = cardguix * 15;//information of where to start drawing the card on x and y axis
            int y = cardguiy;


            Console.SetCursorPosition(x, y);
            Console.Write(" ____________\n"); //top of the card

            for (int i = 0; i < 11; i++)
            {
                Console.SetCursorPosition(x, y + 1 + i);

                if (i != 10)
                    Console.WriteLine("|            |");//mid section
                else
                    Console.WriteLine("|____________|");//bottom of the card
            }

            // colors of suites
            if (card.Suite == Suites.Diamonds || card.Suite == Suites.Hearts)
                Console.ForegroundColor = ConsoleColor.Red;
            else
                Console.ForegroundColor = ConsoleColor.White;

            //drawing of card value and suit in right place
            Console.SetCursorPosition(x + 2, y + 3);
            Console.Write(card.Suite.ToChar());
            Console.SetCursorPosition(x + 2, y + 2);
            Console.Write(card.NamedValue);
            Console.SetCursorPosition(x + 6, y + 6);
            Console.Write(card.Suite.ToChar());
            Console.SetCursorPosition(x + 11, y + 9);
            Console.Write(card.Suite.ToChar());
            Console.SetCursorPosition(x + 11, y + 10);
            Console.Write(card.NamedValue);
            Console.ForegroundColor = ConsoleColor.White;

            //if the number of cards is bigger then the number of cards that can be drawn in defined length of console x changes to beginning and y move 4 points down
            cardguix++;
            if (cardguix * 15 >= Console.WindowWidth)
            {
                cardguiy += 4;
                cardguix = 0;
            }
        }

        //void where the player is asked if he wants to play again 
        public void PlayGame()
        {
            bool nextgame = true;

            while (nextgame)
            {
                Console.Clear();
                ycoor = 0;

                PlayRound();

                if (balance <= 0 || !AskQuestion("Do you want to continue?"))
                {
                    nextgame = false;
                }
            }

            //end of the game because the player lost all of his money
            if (balance <= 0)
            {
                WriteLine("You have lost all the balance, press any key to exit");
                ReadKey();
            }
        }

        //simplification of the yes or no question 
        public bool AskQuestion(string Question)
        {
            WriteLine(Question);

            while (true)
            {
                string response = ReadLine();
                if (response.ToLower() == "y" || response.ToLower() == "yes")//takes lower or upper case and y or yes as answer
                    return true;
                if (response.ToLower() == "n" || response.ToLower() == "no")
                    return false;
            }
        }

        private void PlayRound()
        {
            int bet;
            int iter = 1;

            WriteLine($"How much do you want to bet? ($1-${balance})");

            // betting 
            while (true)
            {
                string betSt = ReadLine();
                if (int.TryParse(betSt, out bet) && bet > 0 && bet <= balance)
                {
                    WriteLine("You have bet: " + bet);
                    break;
                }
                else
                {
                    WriteLine("insufficient number");
                }
            }

            Hands.Clear();
            for (int i = 0; i < 3; i++)
            {
                Hands.Add(new Hand());
            }

            WriteLine("Player hand:");

            Card tmp = deck.GetCard();

            Hands[iter].AddCard(tmp);// takes card and put it in hand 
            DrawCard(ref tmp);// draw the taken card and repeats
            tmp = deck.GetCard();
            Hands[iter].AddCard(tmp);
            DrawCard(ref tmp);

            ycoor += 13;

            //check for pair
            bool isEqual = true;
            tmp = Hands[iter].Cards[0];
            foreach (var i in Hands[iter].Cards)
            {
                if (i != tmp)//if card2 isn't the same value as card1
                {
                    isEqual = false;
                    break;
                }
            }

            bool isSplit = false;
            bool cont;

            //conditions for splitting hand into two hands
            if (isEqual && bet * 2 <= balance && AskQuestion("You got lucky, do you want to split the hand?(y or n)"))
            {
                Hands[iter] = new Hand();
                Hands[iter].AddCard(tmp);
                Hands[iter + 1] = new Hand(Hands[iter]);

                isSplit = true;
                cont = true;
            }
            else if (Hands[iter].Value != 21 && AskQuestion($"Your card value: {Hands[iter].Value}. Do you want to hit?(y or n)"))
                cont = true;
            else
            {
                WriteLine($"Your card value: {Hands[iter].Value}");//BlackJack
                cont = false;
            }

            while (cont)
            {
                tmp = deck.GetCard();
                Hands[iter].AddCard(tmp);
                DrawCard(ref tmp);//adding card

                //if player busted the hand
                if (Hands[iter].Value > 21)
                {
                    WriteLine($"Your value is {Hands[iter].Value}. You have busted!");
                    Hands[iter].Value = int.MaxValue;
                    //if he wanted to split 
                    if (isSplit)
                    {
                        iter++;
                        continue;
                    }
                    else
                    {
                        
                        iter = 1;
                        cont = false;// ends the turn
                    }
                }
                else if (AskQuestion("Your card value: " + Hands[iter].Value + " Do you want to hit?(y or n)"))
                {
                    continue;
                }
                else if (isSplit)//moving to another hand if player splited hand
                {
                    iter++;
                    isSplit = false;
                    continue;
                }
                else
                {
                    iter = 1;
                    cont = false;
                }
            }

            cardguix = 0;
            cardguiy = 0;
            WriteLine("Dealers hand:");

            //dealer draws to 16 and stands on 17
            while (Hands[dealerIter].Value < 17)
            {
                tmp = deck.GetCard();
                Hands[dealerIter].AddCard(tmp);//adding to dealers hand
                DrawCard(ref tmp);
            }

            ycoor += 13;
            cardguix = 0;
            cardguiy = 0;
            WriteLine($"Dealers card value: {Hands[dealerIter].Value}");

            int updateBalance = 0;

            //results of the bet 
            for (int i = 1; i < iter + 2; i++)
            {
                // player did not play second hand
                if (Hands[i].Value == 0)
                    continue;

                if (Hands[iter].GetHandResult(Hands[dealerIter]) == GameResult.WIN)
                {
                    updateBalance += bet;
                }
                else if (Hands[iter].GetHandResult(Hands[dealerIter]) == GameResult.LOSE)
                {
                    updateBalance -= bet;
                }
            }

            // writen results
            if (updateBalance > 0)
                WriteLine($"You have won ${updateBalance}!");
            else if (updateBalance < 0)
                WriteLine($"You have lost ${Math.Abs(updateBalance)}!");
            else
                WriteLine($"You have pushed and lost nothing");

            balance += updateBalance;
        }

        int cardguiy = 0;

        int cardguix = 0;

        const int dealerIter = 0;

        Deck deck = new Deck();

        int balance = 5000;

        List<Hand> Hands = new List<Hand>();

        public int ycoor = 0;
    }

}
