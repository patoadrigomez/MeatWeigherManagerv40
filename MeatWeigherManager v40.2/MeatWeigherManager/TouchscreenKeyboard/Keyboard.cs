using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace KeyboardClassLibrary
{
    public partial class Keyboardcontrol : UserControl
    {
        private Boolean altGrindicator = false;
        private Boolean shiftindicator = false;
        private Boolean capslockindicator = false;
        private string pvtKeyboardKeyPressed = "";

        public Keyboardcontrol()
        {
            InitializeComponent();
            pictureBoxKeyboard.Image = KeyboardClassLibrary.Properties.Resources.keyboard_white;
            pictureBoxCapsLockDown.Image = KeyboardClassLibrary.Properties.Resources.caps_down_white;
            pictureBoxLeftShiftDown.Image = KeyboardClassLibrary.Properties.Resources.shift_down_white;
            pictureBoxRightShiftDown.Image = KeyboardClassLibrary.Properties.Resources.shift_down_white;
            pictureBox_AltGrKey.Image = KeyboardClassLibrary.Properties.Resources.altGr_down_white;
        }


        [Category("Mouse"), Description("Return value of mouseclicked key")]
        public event KeyboardDelegate UserKeyPressed;
        protected virtual void OnUserKeyPressed(KeyboardEventArgs e)
        {
            if (UserKeyPressed != null && e.KeyboardKeyPressed != null)
                UserKeyPressed(this, e);
        }

        private void pictureBoxKeyboard_MouseClick(object sender, MouseEventArgs e)
        {
            Single xpos = e.X;
            Single ypos = e.Y;

            xpos = 993 * (xpos / pictureBoxKeyboard.Width);
            ypos = 282 * (ypos / pictureBoxKeyboard.Height);

            pvtKeyboardKeyPressed = HandleTheMouseClick(xpos, ypos);

            KeyboardEventArgs dea = new KeyboardEventArgs(pvtKeyboardKeyPressed);

            OnUserKeyPressed(dea);
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            HandleAltGrClick();
        }
        private void pictureBoxLeftShiftState_MouseClick(object sender, MouseEventArgs e)
        {
            HandleShiftClick();
        }

        private void pictureBoxRightShiftState_MouseClick(object sender, MouseEventArgs e)
        {
            HandleShiftClick();
        }

        private void pictureBoxCapsLockState_MouseClick(object sender, MouseEventArgs e)
        {
            HandleCapsLock();
        }

        private string HandleTheMouseClick(Single x, Single y)
        {
            string Keypressed = null;
            if (x >= 4 && x < 815 && y >= 3 && y < 277)         //  keyboard section
            {
                if (y < 58)
                {
                    if (x >= 4 && x < 59) Keypressed = HandleShiftableKey("º");
                    else if (x >= 67 && x < 112) Keypressed = HandleShiftableKey("1");
                    else if (x >= 112 && x < 165) Keypressed = HandleShiftableKey("2");
                    else if (x >= 165 && x < 220) Keypressed = HandleShiftableKey("3");
                    else if (x >= 220 && x < 275) Keypressed = HandleShiftableKey("4");
                    else if (x >= 275 && x < 328) Keypressed = HandleShiftableKey("5");
                    else if (x >= 328 && x < 380) Keypressed = HandleShiftableKey("6");
                    else if (x >= 380 && x < 435) Keypressed = HandleShiftableKey("7");
                    else if (x >= 435 && x < 490) Keypressed = HandleShiftableKey("8");
                    else if (x >= 490 && x < 545) Keypressed = HandleShiftableKey("9");
                    else if (x >= 545 && x < 600) Keypressed = HandleShiftableKey("0");
                    else if (x >= 600 && x < 655) Keypressed = HandleShiftableKey("-");
                    else if (x >= 655 && x < 705) Keypressed = HandleShiftableKey("=");
                    else if (x >= 705 && x < 815) Keypressed = "{BACKSPACE}";
                    else Keypressed = null;
                }
                else if (y >= 58 && y < 114)
                {
                    if (x >= 5 && x < 70) HandleAltGrClick();
                    else if (x >= 85 && x < 140) Keypressed = HandleShiftableCaplockableKey("q");
                    else if (x >= 140 && x < 193) Keypressed = HandleShiftableCaplockableKey("w");
                    else if (x >= 193 && x < 247) Keypressed = HandleShiftableCaplockableKey("e");
                    else if (x >= 247 && x < 300) Keypressed = HandleShiftableCaplockableKey("r");
                    else if (x >= 300 && x < 355) Keypressed = HandleShiftableCaplockableKey("t");
                    else if (x >= 355 && x < 409) Keypressed = HandleShiftableCaplockableKey("y");
                    else if (x >= 409 && x < 463) Keypressed = HandleShiftableCaplockableKey("u");
                    else if (x >= 463 && x < 517) Keypressed = HandleShiftableCaplockableKey("i");
                    else if (x >= 517 && x < 571) Keypressed = HandleShiftableCaplockableKey("o");
                    else if (x >= 571 && x < 625) Keypressed = HandleShiftableCaplockableKey("p");
                    else if (x >= 625 && x < 680) Keypressed = HandleShiftableKey("[");
                    else if (x >= 680 && x < 733) Keypressed = HandleShiftableKey("]");
                    else if (x >= 733 && x < 786) Keypressed = HandleShiftableKey(";");
                    else Keypressed = null;
                }
                else if (y >= 114 && y < 168)
                {
                    if (x >= 4 && x < 113) HandleCapsLock();
                    else if (x >= 113 && x < 167) Keypressed = HandleShiftableCaplockableKey("a");
                    else if (x >= 167 && x < 221) Keypressed = HandleShiftableCaplockableKey("s");
                    else if (x >= 221 && x < 275) Keypressed = HandleShiftableCaplockableKey("d");
                    else if (x >= 275 && x < 330) Keypressed = HandleShiftableCaplockableKey("f");
                    else if (x >= 330 && x < 383) Keypressed = HandleShiftableCaplockableKey("g");
                    else if (x >= 383 && x < 437) Keypressed = HandleShiftableCaplockableKey("h");
                    else if (x >= 437 && x < 491) Keypressed = HandleShiftableCaplockableKey("j");
                    else if (x >= 491 && x < 545) Keypressed = HandleShiftableCaplockableKey("k");
                    else if (x >= 545 && x < 599) Keypressed = HandleShiftableCaplockableKey("l");
                    else if (x >= 599 && x < 653) Keypressed = HandleShiftableCaplockableKey("ñ");
                    else if (x >= 653 && x < 706) Keypressed = HandleShiftableKey("'");
                    else if (x >= 706 && x < 815) Keypressed = "{ENTER}";
                    else Keypressed = null;
                }
                else if (y >= 168 && y < 221)
                {
                    if (x >= 4 && x < 140) HandleShiftClick();
                    else if (x >= 140 && x < 194) Keypressed = HandleShiftableCaplockableKey("z");
                    else if (x >= 194 && x < 248) Keypressed = HandleShiftableCaplockableKey("x");
                    else if (x >= 248 && x < 302) Keypressed = HandleShiftableCaplockableKey("c");
                    else if (x >= 302 && x < 356) Keypressed = HandleShiftableCaplockableKey("v");
                    else if (x >= 356 && x < 410) Keypressed = HandleShiftableCaplockableKey("b");
                    else if (x >= 410 && x < 464) Keypressed = HandleShiftableCaplockableKey("n");
                    else if (x >= 464 && x < 518) Keypressed = HandleShiftableCaplockableKey("m");
                    else if (x >= 518 && x < 572) Keypressed = HandleShiftableKey(",");
                    else if (x >= 572 && x < 626) Keypressed = HandleShiftableKey(".");
                    else if (x >= 626 && x < 680) Keypressed = HandleShiftableKey("/");
                    else if (x >= 680 && x < 815) HandleShiftClick();
                    else Keypressed = null;
                }
                else if (y >= 221 && y < 277)
                {
                    if (x >= 218 && x < 597) Keypressed = " ";
                    else Keypressed = null;
                }
            }
            else if (x >= 827 && x < 989 && y >= 27 && y < 193)   //  cursor keys
            {
                if (y < 83)
                {
                    if (x < 880) Keypressed = "{INSERT}";
                    else if (x >= 880 && x < 934) Keypressed = "{UP}";
                    else if (x >= 934) Keypressed = HandleShiftableKey("{HOME}");
                    else Keypressed = null;
                }
                else if (y >= 83 && y < 137)
                {
                    if (x < 880) Keypressed = "{LEFT}";
                    else if (x >= 934) Keypressed = "{RIGHT}";
                    else Keypressed = null;
                }
                else if (y >= 137)
                {
                    if (x < 880) Keypressed = "{DELETE}";
                    else if (x >= 880 && x < 934) Keypressed = "{DOWN}";
                    else if (x >= 934) Keypressed = HandleShiftableKey("{END}");
                    else Keypressed = null;
                }
                else Keypressed = null;
            }
            if (Keypressed != null)
            {
                if (shiftindicator) HandleShiftClick();
                if (altGrindicator) HandleAltGrClick();
                return Keypressed;
            }
            else
            {
                return null;
            }
        }

        private string HandleShiftableKey(string theKey)
        {
            //Atencion:
            //especiales caracteres : "[+^%~()]" que deben ser acotados por "{ }"
            
            if (shiftindicator)
            {
                switch(theKey)
                {
                    case "º" :
                        theKey = "{~}";
                        break;
                    case "1" :
                        theKey = "!";
                        break;
                    case "2":
                        theKey = "@";
                        break;
                    case "3":
                        theKey = "#";
                        break;
                    case "4":
                        theKey = "$";
                        break;
                    case "5":
                        theKey = "{%}";
                        break;
                    case "6":
                        theKey = "{^}";
                        break;
                    case "7":
                        theKey = "&";
                        break;
                    case "8":
                        theKey = "*";
                        break;
                    case "9":
                        theKey = "{(}";
                        break;
                    case "0":
                        theKey = "{)}";
                        break;
                    case "-":
                        theKey = "_";
                        break;
                    case "=":
                        theKey = "{+}";
                        break;
                    case "[":
                        theKey = "{{}";
                        break;
                    case "]":
                        theKey = "{}}";
                        break;
                    case ";":
                        theKey = ":";
                        break;
                    case "´":
                        theKey = "\"";
                        break;
                    case ",":
                        theKey = "<";
                        break;
                    case ".":
                        theKey = ">";
                        break;
                    case "/":
                        theKey = "?";
                        break;
                }
            }
            else if (altGrindicator)
            {
                switch (theKey)
                {
                    case "º":
                        theKey = "\\";
                        break;
                    case "1":
                        theKey = "¿";
                        break;
                }
            }
            else
            {
                switch (theKey)
                {
                    case "[":
                        theKey = "{[}";
                        break;
                    case "]":
                        theKey = "{]}";
                        break;
                }
            }
            return theKey;
        }

        private string HandleShiftableCaplockableKey(string theKey)
        {
            if (capslockindicator || shiftindicator)
            {
                return theKey.ToUpper();
            }
            else
            {
                return theKey;
            }
        }

        private void HandleShiftClick()
        {
            if (shiftindicator)
            {
                shiftindicator = false;
                pictureBoxLeftShiftDown.Visible = false;
                pictureBoxRightShiftDown.Visible = false;
            }
            else
            {
                shiftindicator = true;
                pictureBoxLeftShiftDown.Visible = true;
                pictureBoxRightShiftDown.Visible = true;
            }
        }

        private void HandleAltGrClick()
        {
            if (altGrindicator)
            {
                altGrindicator = false;
                pictureBox_AltGrKey.Visible = false;
            }
            else
            {
                altGrindicator = true;
                pictureBox_AltGrKey.Visible = true;
            }
        }
        private void HandleCapsLock()
        {
            if (capslockindicator)
            {
                capslockindicator = false;
                pictureBoxCapsLockDown.Visible = false;
            }
            else
            {
                capslockindicator = true;
                pictureBoxCapsLockDown.Visible = true;
            }
        }

        private void pictureBoxKeyboard_SizeChanged(object sender, EventArgs e)
        {
            // position the capslock and shift down overlays
            pictureBox_AltGrKey.Left = Convert.ToInt16(pictureBoxKeyboard.Width * 5 / 993);
            pictureBox_AltGrKey.Top = Convert.ToInt16(pictureBoxKeyboard.Height * 59 / 282);
            pictureBoxCapsLockDown.Left = Convert.ToInt16(pictureBoxKeyboard.Width * 5 / 993);
            pictureBoxCapsLockDown.Top = Convert.ToInt16(pictureBoxKeyboard.Height * 115 / 282);
            pictureBoxLeftShiftDown.Left = Convert.ToInt16(pictureBoxKeyboard.Width * 5 / 993);
            pictureBoxLeftShiftDown.Top = Convert.ToInt16(pictureBoxKeyboard.Height * 169 / 282);
            pictureBoxRightShiftDown.Left = Convert.ToInt16(pictureBoxKeyboard.Width * 681 / 993);
            pictureBoxRightShiftDown.Top = pictureBoxLeftShiftDown.Top;

            // size the capslock and shift down overlays

            pictureBox_AltGrKey.Width = Convert.ToInt16(pictureBoxKeyboard.Width * 83 / 993);
            pictureBox_AltGrKey.Height = Convert.ToInt16(pictureBoxKeyboard.Height * 55 / 282);
            pictureBoxCapsLockDown.Width = Convert.ToInt16(pictureBoxKeyboard.Width * 110 / 993);
            pictureBoxCapsLockDown.Height = Convert.ToInt16(pictureBoxKeyboard.Height * 55 / 282);
            pictureBoxLeftShiftDown.Width = Convert.ToInt16(pictureBoxKeyboard.Width * 136 / 993);
            pictureBoxLeftShiftDown.Height = Convert.ToInt16(pictureBoxKeyboard.Height * 55 / 282);
            pictureBoxRightShiftDown.Width = Convert.ToInt16(pictureBoxKeyboard.Width * 135 / 993);
            pictureBoxRightShiftDown.Height = pictureBoxLeftShiftDown.Height;
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

