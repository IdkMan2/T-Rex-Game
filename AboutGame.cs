using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T_Rex_Game
{
    public partial class AboutGame : Form
    {
        public AboutGame()
        {
            InitializeComponent();
            MaximizeBox = false;
            MinimizeBox = false;
        }

        private void AboutGame_Load(object sender, EventArgs e)
        {

        }

        private void onCloseBtnClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
