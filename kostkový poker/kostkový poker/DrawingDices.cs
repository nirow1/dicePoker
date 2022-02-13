using System;
using System.Collections.Generic;
using System.Text;

namespace kostkový_poker
{
    class DrawingDices
    {
        Random r = new Random();
        public int posX = 0;
        public int posY = 0;
        private int dice = 0;
        private int Dv;

        public void Drawoutline(int a, int b, int c, int d, int e)
        {
            Dv = a;
            Choosedice();

            posY = posY - 6;
            Dv = b;
            Choosedice();

            posY = posY - 11;
            Dv = c;
            Choosedice();

            posY = posY - 8;
            Dv = d;
            Choosedice();

            posY = posY - 10;
            Dv = e;
            Choosedice();
            posX = 0;
            posY = posY + 3;
            Console.SetCursorPosition(posX, posY);

        }

        public void WriteLine(string sentence)
        {
            Console.SetCursorPosition(0, posY);
            Console.WriteLine(sentence);
            posY++;
        }

        private void DrawValues()
        {
            switch (dice)
            {
                case 1:
                    if(Dv==1||Dv==3||Dv==5)
                        PrintValue(7, 7);
                    if(Dv > 1)
                    {
                        PrintValue(5, 8);
                        PrintValue(9, 6);
                    }
                    if (Dv > 3)
                    {
                        PrintValue(11, 8);
                        PrintValue(3, 6);
                    }
                    if (Dv == 6)
                    {
                        PrintValue(4, 7);
                        PrintValue(10, 7);
                    }
                    break;

                case 2:
                    if (Dv == 1 || Dv == 3 || Dv == 5)
                        PrintValue(10, 7);
                    if (Dv > 1)
                    {
                        PrintValue(6, 8);
                        PrintValue(14, 6);
                    }
                    if (Dv > 3)
                    {
                        PrintValue(12, 8);
                        PrintValue(8, 6);
                    }
                    if (Dv == 6)
                    {
                        PrintValue(7, 7);
                        PrintValue(13, 7);
                    }
                    break;

                case 3:
                    if (Dv == 1 || Dv == 3 || Dv == 5)
                        PrintValue(7, 7);
                    if (Dv > 1)
                    {
                        PrintValue(3, 8);
                        PrintValue(11, 6);
                    }

                    if (Dv > 3)
                    {
                        PrintValue(9, 8);
                        PrintValue(5, 6);
                    }
                    if (Dv == 6)
                    {
                        PrintValue(4, 7);
                        PrintValue(10, 7);
                    }
                    break;
            }
        }

        private void PrintValue(int a, int b)
        {
            Console.SetCursorPosition(posX * 19 + a, posY - b);
            Console.WriteLine("o");
        }

        private void Choosedice()
        {
            int x = r.Next(1, 4);

            if (x == 1)
                DrawDice1();
            else if (x == 2)
                DrawDice2();
            else
                DrawDice3();
        }

        private void DrawDice1()
        {
            DiceLine("    ----------");
            DiceLine("   /         /|");
            DiceLine("  /         /o|");
            DiceLine(" /         / o|");
            DiceLine("'---------'ooo|");
            DiceLine("| o  o  o |o /");
            DiceLine("|    o    |o/");
            DiceLine("| o  o  o |/");
            DiceLine("'---------'");
            dice = 1;
            DrawValues();
            posX++;
        }
        private void DrawDice2()
        {
            DiceLine( "    ----------");
            DiceLine(@"   /\         \");
            DiceLine(@"  /o \         \");
            DiceLine(@" /  o \         \");
            DiceLine(@"'o o o '---------'");
            DiceLine(@" \ o   / o  o  o /");
            DiceLine(@"  \ o /    o    /");
            DiceLine(@"   \ / o  o  o /");
            DiceLine(@"    '---------'");
            dice = 2;
            DrawValues();
            posX++;
        }
        private void DrawDice3()
        {
            DiceLine( " ----------");
            DiceLine(@"|\         \");
            DiceLine(@"|o\         \");
            DiceLine(@"|o \         \");
            DiceLine(@"|ooo'---------'");
            DiceLine(@" \ o| o     o |");
            DiceLine(@"  \o| o  o  o |");
            DiceLine(@"   \| o     o |");
            DiceLine(@"    '---------'");
            dice = 3;
            DrawValues();
            posX++;
        }
        private void DiceLine(string diceline)
        {
            Console.SetCursorPosition(posX * 19, posY);
            Console.WriteLine(diceline);
            posY++;
        }
    }
}
