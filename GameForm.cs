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
    public partial class GameForm : Form
    {
        private GameManager gameManager;
        public GameManager GameManager { get; }
        public GameForm()
        {
            InitializeComponent();
            Dictionary<GameElement, Component> gameElements = new Dictionary<GameElement, Component> {
                { GameElement.FLOOR, floor },
                { GameElement.GAME_TIMER, gameTimer },
                { GameElement.OBSTACLE1, obstacle1 },
                { GameElement.OBSTACLE2, obstacle2 },
                { GameElement.SCORE_LABEL, scoreText },
                { GameElement.LEVEL_LABEL, levelLabel },
                { GameElement.TREX, trex }
            };
            gameManager = new GameManager(this, gameElements);
        }

        private void gameEvent(object sender, EventArgs e)
        {
            gameManager.onGameEvent(sender, e);
        }

        private void onKeyDown(object sender, KeyEventArgs e)
        {
            gameManager.onKeyDown(sender, e);
        }

        private void onKeyUp(object sender, KeyEventArgs e)
        {
            gameManager.onKeyUp(sender, e);
        }


        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void OnFormPaint(object sender, PaintEventArgs e)
        {
            int rectSide = 50, rectMargin = 15;
            Graphics g = e.Graphics;
            g.DrawRectangle(
                Pens.Gray,
                new Rectangle(
                    ClientSize.Width - rectMargin - rectSide, rectMargin,
                    rectSide, rectSide
                )
            );
        }
    }
}
