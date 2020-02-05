using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T_Rex_Game
{
    class Menu
    {
        private GroupBox menuGroupBox;
        private bool openState = true;

        public bool Open {
            get { return openState; }
            set {
                if (value == openState) return;
                if(value == true)
                {
                    menuGroupBox.Show();
                    openState = true;
                } else
                {
                    menuGroupBox.Hide();
                    openState = false;
                }
            }
        }
        public Menu(MainForm mainForm, GroupBox menuGroupBox)
        {
            this.menuGroupBox = menuGroupBox;
            adjustMenuPosition(mainForm.ClientSize);
        }

        private void adjustMenuPosition(Size clientSize)
        {
            menuGroupBox.Top = (clientSize.Height / 2) - (menuGroupBox.Height / 2) - 25;
            menuGroupBox.Left = (clientSize.Width / 2) - (menuGroupBox.Width / 2);
        }
    }
}
