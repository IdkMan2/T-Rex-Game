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
    public partial class MainForm : Form
    {
        private Menu menu;
        private GameForm gameForm;
        private AboutGame aboutForm;
        public MainForm()
        {
            InitializeComponent();
            MaximizeBox = false;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            menu = new Menu(this, menuGroupBox);
        }

        private void onStartButtonClick(object sender, EventArgs e)
        {
            gameForm = new GameForm();
            gameForm.MdiParent = this;
            gameForm.FormClosed += onGameFormClosed;
            menu.Open = false;
            gameForm.Show();
        }

        private void onExitButtonClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void onAboutGameFormClosed(object sender, FormClosedEventArgs e)
        {
            aboutForm = null;
        }

        private void onGameFormClosed(object sender, FormClosedEventArgs e)
        {
            gameForm = null;
            menu.Open = true;
        }

        private void onAboutButtonClick(object sender, EventArgs e)
        {
            if(aboutForm != null)
            {
                aboutForm.Focus();
            } else
            {
                aboutForm = new AboutGame();
                aboutForm.FormClosed += onAboutGameFormClosed;
                aboutForm.Show();
            }
        }
    }
}
