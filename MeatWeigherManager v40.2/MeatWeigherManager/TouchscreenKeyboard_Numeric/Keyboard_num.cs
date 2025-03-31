using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace KeyboardClassLibrarySjf
{
    public partial class Keyboardcontrol_Num : UserControl
    {
        const int LeftInitialOffsetPixels_X = 8;
        const int TopInitialOffsetPixels_Y = 11;
        const int SizePixelsKey_X = 107;
        const int SizePixelsKey_Y = 106;
         
        public Keyboardcontrol_Num()
        {
            InitializeComponent();
        }

        private string pvtKeyboardKeyPressed = "";

        [Category("Mouse"), Description("Return value of mouseclicked key")]
        public event KeyboardDelegate UserKeyPressed;

        protected virtual void OnUserKeyPressed(KeyboardEventArgs e)
        {
            if (UserKeyPressed != null)
                UserKeyPressed(this, e);
        }

        private void pictureBoxKeyboard_MouseClick(object sender, MouseEventArgs e)
        {
            Single xpos = e.X;
            Single ypos = e.Y;

            xpos = pictureBoxKeyboard.Image.Width /*444*/ * (xpos / pictureBoxKeyboard.Width);
            ypos = pictureBoxKeyboard.Image.Height /*452*/ * (ypos / pictureBoxKeyboard.Height);

            pvtKeyboardKeyPressed = HandleTheMouseClick(xpos, ypos);

            KeyboardEventArgs dea = new KeyboardEventArgs(pvtKeyboardKeyPressed);

            OnUserKeyPressed(dea);
        }


        private string HandleTheMouseClick(Single x, Single y)
        {
            string Keypressed = null;
            if (x >= LeftInitialOffsetPixels_X && x < pictureBoxKeyboard.Image.Width + LeftInitialOffsetPixels_X && 
                y >= TopInitialOffsetPixels_Y && y < pictureBoxKeyboard.Image.Height+TopInitialOffsetPixels_Y)         //  keyboard section
            {
                if (y < (TopInitialOffsetPixels_Y +SizePixelsKey_Y))
                {
                    if (x >= LeftInitialOffsetPixels_X && x < LeftInitialOffsetPixels_X+SizePixelsKey_X)
                        Keypressed = "1";
                    else if (x >= LeftInitialOffsetPixels_X + SizePixelsKey_X && x < (LeftInitialOffsetPixels_X + SizePixelsKey_X*2))
                        Keypressed = "2";
                    else if (x >= (LeftInitialOffsetPixels_X + SizePixelsKey_X * 2) && x < (LeftInitialOffsetPixels_X + SizePixelsKey_X * 3))
                        Keypressed = "3";
                    else if (x >= (LeftInitialOffsetPixels_X + SizePixelsKey_X * 3) && x < (LeftInitialOffsetPixels_X + SizePixelsKey_X * 4))
                        Keypressed = "4";
                    else Keypressed = null;
                }
                else if (y >= (TopInitialOffsetPixels_Y + SizePixelsKey_Y) && y < (TopInitialOffsetPixels_Y + (SizePixelsKey_Y*2)))
                {
                    if (x >= LeftInitialOffsetPixels_X && x < LeftInitialOffsetPixels_X + SizePixelsKey_X)
                        Keypressed = "5";
                    else if (x >= LeftInitialOffsetPixels_X + SizePixelsKey_X && x < (LeftInitialOffsetPixels_X + SizePixelsKey_X * 2))
                        Keypressed = "6";
                    else if (x >= (LeftInitialOffsetPixels_X + SizePixelsKey_X * 2) && x < (LeftInitialOffsetPixels_X + SizePixelsKey_X * 3))
                        Keypressed = "7";
                    else if (x >= (LeftInitialOffsetPixels_X + SizePixelsKey_X * 3) && x < (LeftInitialOffsetPixels_X + SizePixelsKey_X * 4))
                        Keypressed = "8";
                    else Keypressed = null;
                }
                else if (y >= (TopInitialOffsetPixels_Y + (SizePixelsKey_Y * 2)) && y < (TopInitialOffsetPixels_Y + (SizePixelsKey_Y * 3)))
                {
                    if (x >= LeftInitialOffsetPixels_X && x < LeftInitialOffsetPixels_X + SizePixelsKey_X)
                        Keypressed = "9";
                    else if (x >= LeftInitialOffsetPixels_X + SizePixelsKey_X && x < (LeftInitialOffsetPixels_X + SizePixelsKey_X * 2))
                        Keypressed = "0";
                    else if (x >= (LeftInitialOffsetPixels_X + SizePixelsKey_X * 2) && x < (LeftInitialOffsetPixels_X + SizePixelsKey_X * 3))
                        Keypressed = ",";
                    else if (x >= (LeftInitialOffsetPixels_X + SizePixelsKey_X * 3) && x < (LeftInitialOffsetPixels_X + SizePixelsKey_X * 4))
                        Keypressed = "{DELETE}";
                    else Keypressed = null;
                }
                else if (y >= (TopInitialOffsetPixels_Y + (SizePixelsKey_Y * 3)) && y < (TopInitialOffsetPixels_Y + (SizePixelsKey_Y * 4)))
                {
                    if (x >= LeftInitialOffsetPixels_X && x < LeftInitialOffsetPixels_X + SizePixelsKey_X*2)
                        Keypressed = "{BACKSPACE}";
                    else if (x >= LeftInitialOffsetPixels_X + SizePixelsKey_X * 2 && x < LeftInitialOffsetPixels_X + SizePixelsKey_X * 4)
                        Keypressed = "{ENTER}";
                    else Keypressed = null;
                }
            }
            if (Keypressed != null)
            {
                return Keypressed;
            }
            else
            {
                return null;
            }
        }

    }

    public delegate void KeyboardDelegate(object sender, KeyboardEventArgs e);

    public class KeyboardEventArgs : EventArgs
    {
        private readonly string pvtKeyboardKeyPressed;

        public KeyboardEventArgs(string KeyboardKeyPressed)
        {
            this.pvtKeyboardKeyPressed = KeyboardKeyPressed;
        }

        public string KeyboardKeyPressed
        {
            get
            {
                return pvtKeyboardKeyPressed;
            }
        }
    }
}

