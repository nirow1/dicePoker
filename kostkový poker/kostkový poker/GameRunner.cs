using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace kostkový_poker
{
    public class GameRunner
    {
        DrawingDices DD = new DrawingDices();
        Random r = new Random();
        Betting Bt = new Betting();
        public int diceA;
        public int diceB;
        public int diceC;
        public int diceD;
        public int diceE;
        public int A;
        public int B;
        public int C;
        public int D;
        public int E;
        public int F;
        public int finalSide = 0;
        public int firstSide = 0;
        public bool rerollA;
        public bool rerollB;
        public bool rerollC;
        public bool rerollD;
        public bool rerollE;
        int[] PlayerDices = new int[7];
        int[] OponentDices = new int[7];

        public GameRunner()
        {
            Console.WriteLine("Press any key to start...");  
            Console.ReadKey();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

        }
        public bool AskQuestion(string Question)
        {
            Console.WriteLine(Question);

            while (true)
            {
                string response = Console.ReadLine();
                if (response.ToLower() == "y" || response.ToLower() == "yes")//takes lower or upper case and y or yes as answer
                    return true;
                if (response.ToLower() == "n" || response.ToLower() == "no")
                    return false;
            }
        }

        public void rollDices()
        {
            diceA = r.Next(1, 7);
            diceB = r.Next(1, 7);
            diceC = r.Next(1, 7);
            diceD = r.Next(1, 7);
            diceE = r.Next(1, 7);
        }

        public void rerollDices()
        {
            if (rerollA == true)
                diceA = r.Next(1, 7);                
            
            if (rerollB == true)
                diceB = r.Next(1, 7);

            if (rerollC == true)
                diceC = r.Next(1, 7);

            if (rerollD == true)
                diceD = r.Next(1, 7);

            if (rerollE == true)
                diceE = r.Next(1, 7);
            
            DD.Drawoutline(diceA, diceB, diceC, diceD, diceE);
            restart();
        }

        private void restart()
        {
            rerollA = false;
            rerollB = false;
            rerollC = false;
            rerollD = false;
            rerollE = false;
        }

        public void pickDices()
        {
            bool wrongDices = true;

            while (wrongDices)
            {
                restart();

                if (AskQuestion("do you want to throw dice 1 again? y or n "))
                    rerollA = true;

                if (AskQuestion("do you want to throw dice 2 again? y or n "))
                    rerollB = true;

                if (AskQuestion("do you want to throw dice 3 again? y or n "))
                    rerollC = true;

                if (AskQuestion("do you want to throw dice 4 again? y or n "))
                    rerollD = true;

                if (AskQuestion("do you want to throw dice 5 again? y or n "))
                    rerollE = true;

                Console.WriteLine("You decided to reroll: ");
                if (rerollA)
                    Console.WriteLine("Dice 1");
                if (rerollB)
                    Console.WriteLine("Dice 2");
                if (rerollC)
                    Console.WriteLine("Dice 3");
                if (rerollD)
                    Console.WriteLine("Dice 4");
                if (rerollE)
                    Console.WriteLine("Dice 5");

                if (AskQuestion("Continue?"))
                    wrongDices = false;
            }
        }
        public int HandValue;

        public void EvaluateHand()
        {

            A = 0;
            B = 0;
            C = 0;
            D = 0;
            E = 0;
            F = 0;
            int side = 0;
            int sideValue = 0;
            bool two = false;
            bool first = true;


            int[] Dices = new int[] { diceA, diceB, diceC, diceD, diceE };

            for (int i = 0; i < 5; i++)
            {
                if (Dices[i] == 1)
                    A++;
                if (Dices[i] == 2)
                    B++;
                if (Dices[i] == 3)
                    C++;
                if (Dices[i] == 4)
                    D++;
                if (Dices[i] == 5)
                    E++;
                if (Dices[i] == 6)
                    F++;

            }

            if (A == 0)
                A = 6;
            if (B == 0)
                B = 7;
            if (C == 0)
                C = 8;
            if (D == 0)
                D = 9;
            if (E == 0)
                E = 10;
            if (F == 0)
                F = 11;

            if (A == 1)
                A = 12;
            if (B == 1)
                B = 13;
            if (C == 1)
                C = 14;
            if (D == 1)
                D = 15;
            if (E == 1)
                E = 16;
            if (F == 1)
                F = 17;

            int[] Values = new int[] { A, B, C, D, E, F };


            foreach (int value in Values)
            {
                side++;
                if (value >= sideValue && value >= 2 && value <= 5)
                {
                    if (first == true)
                    {
                        firstSide = side;
                        first = false;
                    }
                    sideValue = value;
                    finalSide = side;

                }
            }

            if (A == 2 || B == 2 || C == 2 || D == 2 || E == 2 || F == 2)
            {
                HandValue = 1;
                two = true;
                //DD.WriteLine("pair");
            }

            if (new[] { B, C, D, E, F }.Contains(A) || new[] { C, D, E, F }.Contains(B) || new[] { D, E, F }.Contains(C) || new[] { E, F }.Contains(D) || E == F)
            {
                HandValue = 2;
                //DD.WriteLine("Two pairs");
            }

            if (A == 3 || B == 3 || C == 3 || D == 3 || E == 3 || F == 3)
            {
                HandValue = 3;
                //DD.WriteLine("triple");
            }

            if (A == 12 && B == 13 && C == 14 && D == 15 && E == 16)
            {
                HandValue = 4;
                DD.WriteLine("small straight");
            }

            if (HandValue == 3 && two == true)
            {
                HandValue = 5;
                //DD.WriteLine("fullhouse");
            }

            if (A == 4 || B == 4 || C == 4 || D == 4 || E == 4 || F == 4)
            {
                HandValue = 6;
                //DD.WriteLine("four of the kind");
            }

            if (B == 13 && C == 14 && D == 15 && E == 16 && F == 17)
            {
                HandValue = 7;
                DD.WriteLine("straight");
            }

            if (A == 5 || B == 5 || C == 5 || D == 5 || E == 5 || F == 5)
            {
                HandValue = 8;
                //DD.WriteLine("five of the kind");
            }
        }

        public void OponentsAI()
        {
            int tactics = r.Next(1, 11);
            //tactics = 9;

            switch (OponentDices[5])
            {
                case 1:
                    DiceReroll();
                    break;

                case 2:
                    //if(B == 13 && C == 14 && D == 15 && E == 16)
                    if (PlayerDices[5]> OponentDices[5])
                        DiceReroll();
                    else
                    {
                        for (int i = 0; i < 5;)
                        {
                            if (OponentDices[i] != OponentDices[6])
                            {
                                if (OponentDices[i] != firstSide)
                                    OponentDices[i] = r.Next(1, 7);
                            }
                            i++;
                        }
                    }
                    break;

                case 3:
                    DiceReroll();
                    break;

                case 4:
                    if (OponentDices[5] < PlayerDices[5])
                    {
                        for (int i = 0; i < 5;)
                        {
                            if (OponentDices[i] == 1)
                                 OponentDices[i] = r.Next(1, 7);
                            i++;
                        }
                    }
                    break;

                case 5:
                    if (OponentDices[5] < PlayerDices[5])
                        DiceReroll();
                    break;

                case 6:
                    DiceReroll();
                    break;

                case 7:
                    break;

                case 8:
                    break;

                default:
                    rollDices();
                    break;
            }
        }

        public void DiceReroll()
        {
            for (int i = 0; i < 5;)
            {
                if (OponentDices[i] != OponentDices[6])
                {
                    OponentDices[i] = r.Next(1, 7);

                }
                i++;
            }
        }

        public void oponentsTurn()
        {

            DD.WriteLine(" " + OponentDices[0] + " " + OponentDices[1] + " " + OponentDices[2] + " " + OponentDices[3] + " " + OponentDices[4] + " ");
            OponentsAI();
            DD.WriteLine(" " + OponentDices[0] + " " + OponentDices[1] + " " + OponentDices[2] + " " + OponentDices[3] + " " + OponentDices[4] + " ");
            DD.WriteLine(" " + OponentDices[5]);
            diceA = OponentDices[0];
            diceB = OponentDices[1];
            diceC = OponentDices[2];
            diceD = OponentDices[3];
            diceE = OponentDices[4];
            DD.Drawoutline(diceA, diceB, diceC, diceD, diceE);
            EvaluateHand();
            OponentDices[0] = diceA;
            OponentDices[1] = diceB;
            OponentDices[2] = diceC;
            OponentDices[3] = diceD;
            OponentDices[4] = diceE;
            OponentDices[5] = HandValue;
            OponentDices[6] = finalSide;

        }

        public void Game()
        {
            bool anotherGame = true;
            while (anotherGame)
            {
                DD.posY = 0;
                Console.Clear();
                // players turn 
                DD.WriteLine("Your dices:");
                DD.posY++;
                rollDices();
                DD.Drawoutline(diceA, diceB, diceC, diceD, diceE);
                EvaluateHand();
                DD.WriteLine(" cislo strany " + finalSide);
                DD.posY = DD.posY + 2;
                //fist 5 values are dices 6st value represents 
                PlayerDices[0] = diceA;
                PlayerDices[1] = diceB;
                PlayerDices[2] = diceC;
                PlayerDices[3] = diceD;
                PlayerDices[4] = diceE;
                PlayerDices[5] = HandValue;
                PlayerDices[6] = finalSide;

                //oponents turn
                DD.WriteLine("Oponents dices:");
                DD.posY++;
                rollDices();
                DD.Drawoutline(diceA, diceB, diceC, diceD, diceE);
                EvaluateHand();
                DD.WriteLine(" cislo strany " + finalSide);
                OponentDices[0] = diceA;
                OponentDices[1] = diceB;
                OponentDices[2] = diceC;
                OponentDices[3] = diceD;
                OponentDices[4] = diceE;
                OponentDices[5] = HandValue;
                OponentDices[6] = finalSide;

                //pick dices to reroll
                diceA = PlayerDices[0];
                diceB = PlayerDices[1];
                diceC = PlayerDices[2];
                diceD = PlayerDices[3];
                diceE = PlayerDices[4];

                pickDices();
                Console.Clear();
                DD.posY = 0;
                DD.WriteLine("Your dices:");
                DD.posY++;
                rerollDices();
                EvaluateHand();
                PlayerDices[0] = diceA;
                PlayerDices[1] = diceB;
                PlayerDices[2] = diceC;
                PlayerDices[3] = diceD;
                PlayerDices[4] = diceE;
                PlayerDices[5] = HandValue;
                PlayerDices[6] = finalSide;
                Console.WriteLine(finalSide);
                DD.posY++;

                //computer responce
                DD.WriteLine("Oponents dices:");
                DD.posY++;
                oponentsTurn();
                Result();
                if (AskQuestion("do you want to play again?") == false)
                    anotherGame = false;
            }
        }

        public void Result()
        {
            if (OponentDices[5] > PlayerDices[5])
            {
                DD.WriteLine("Oponent wins");
            }
            else if (OponentDices[5] < PlayerDices[5])
            {
                DD.WriteLine("Player wins");
            }
            else
            {
                if(PlayerDices[6] < OponentDices[6])
                {
                    DD.WriteLine("Oponent wins");
                }
                else if (PlayerDices[6]> OponentDices[6])
                {
                    DD.WriteLine("Player wins");
                }
                else
                {
                    DD.WriteLine("Draw");
                }
            }
        }
        public void TakeDices()
        {
            PlayerDices[0] = diceA;
            PlayerDices[1] = diceB;
            PlayerDices[2] = diceC;
            PlayerDices[3] = diceD;
            PlayerDices[4] = diceE;
            PlayerDices[5] = HandValue;
            PlayerDices[6] = finalSide;
        }
        public void GiveDices()
        {
            diceA = PlayerDices[0];
            diceB = PlayerDices[1];
            diceC = PlayerDices[2];
            diceD = PlayerDices[3];
            diceE = PlayerDices[4];
        }

        //checking the propabilities
        public void posibility()
        {
            int tri = 0;
            diceA = 2;
            diceB = 5;
            diceC = 2;
            diceD = 5;
            diceE = 2;
            for (int i = 0; i < 10000; i++)
            {
                diceA = r.Next(1, 7);
                //diceB = r.Next(1, 7);
                diceC = r.Next(1, 7);
                //diceD = r.Next(1, 7);
                diceE = r.Next(1, 7);
                EvaluateHand();
                if (HandValue > 4)
                {
                    tri++;
                    Console.WriteLine(diceA + " " + diceB + " " + diceC +" "+ diceD + " " + diceE +" hand: "+ HandValue);
                }
            }

            Console.WriteLine(tri);
        }
        public void TryingAi()
        {

            bool Again = true;
            while (Again == true)
            {
                diceA = 2;
                diceB = 5;
                diceC = 2;
                diceD = 5;
                diceE = 6;

                EvaluateHand();
                OponentDices[0] = diceA;
                OponentDices[1] = diceB;
                OponentDices[2] = diceC;
                OponentDices[3] = diceD;
                OponentDices[4] = diceE;
                OponentDices[5] = HandValue;
                OponentDices[6] = finalSide;
                PlayerDices[5] = 3;

                DD.WriteLine(" " + OponentDices[0] + " " + OponentDices[1] + " " + OponentDices[2] + " " + OponentDices[3] + " " + OponentDices[4] + " ");
                OponentsAI();
                DD.WriteLine(" " + OponentDices[0] + " " + OponentDices[1] + " " + OponentDices[2] + " " + OponentDices[3] + " " + OponentDices[4] + " ");
                DD.WriteLine(" " + OponentDices[5]);
                if (AskQuestion("znova") == false)
                    Again = false;
            }
        }
    }
}
