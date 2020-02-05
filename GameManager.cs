using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace T_Rex_Game
{
    public class GameManager
    {
        internal static event GameTickEventHandler GameTickEvent;
        GameForm gameForm;
        Timer gameTimer;
        long ticks = 0;
        bool jumping = false;
        int jumpSpeed = 10;
        int force = 12;
        int obstacleSpeed = 8;
        int floorHeight;
        Random rand = new Random();
        PictureBox trex;
        private readonly List<PictureBox> obstacles = new List<PictureBox>();
        private ScoreController score;
        internal ScoreController Score { get { return score; } }

        internal GameManager(GameForm gameForm, Dictionary<GameElement, Component> gameElements)
        {
            this.gameForm = gameForm;
            this.gameTimer = (Timer) gameElements[GameElement.GAME_TIMER];
            this.trex = (PictureBox) gameElements[GameElement.TREX];
            Label scoreLabel = (Label)gameElements[GameElement.SCORE_LABEL];
            Label levelLabel = (Label)gameElements[GameElement.LEVEL_LABEL];
            ScoreController.LevelUpEvent += this.onLevelUp;
            score = new ScoreController(scoreLabel, levelLabel);
            score.attachListeners();

            PictureBox floor = (PictureBox) gameElements[GameElement.FLOOR];
            floorHeight = floor.Top + 10;

            var obstacles = gameElements.Where((gElem) => gElem.Key.ToString().StartsWith("OBSTACLE"));
            foreach (var obstacle in obstacles)
            {
                this.obstacles.Add((PictureBox) obstacle.Value);
            }

            resetGame();
        }

        ~GameManager()
        {
            ScoreController.LevelUpEvent -= this.onLevelUp;
            score.detachListeners();
        }

        public void onGameEvent(object sender, EventArgs e)
        {
            ticks++;
            GameManager.GameTickEvent(this, new GameTickEventArgs(ticks));

            trex.Top += jumpSpeed;

            if (jumping && force < 0)
            {
                jumping = false;
            }

            if (jumping)
            {
                jumpSpeed = -12;
                force -= 1;
            }
            else
            {
                jumpSpeed = 12;
            }

            obstacles.ForEach((x) => {
                x.Left -= obstacleSpeed;
                if (x.Left + x.Width < -120)
                {
                    // Relokacja przeszkód
                    x.Left = gameForm.ClientSize.Width + rand.Next(200, 800);
                    score.Add(50);
                }

                Rectangle newBounds = x.Bounds;
                newBounds.Width -= 2; newBounds.Height -= 2;
                newBounds.X += 1; newBounds.Y += 1;

                if (trex.Bounds.IntersectsWith(newBounds))
                {
                    gameTimer.Stop();
                    trex.Image = Properties.Resources.dead;
                    score.EndGameScore();
                }
            });

            if (trex.Top > 380 && !jumping)
            {
                force = 12;
                trex.Top = floorHeight - trex.Height;
                jumpSpeed = 0;
            }
        }

        public void onKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    {
                        if (!jumping)
                        {
                            jumping = true;
                        }
                        break;
                    }
                case Keys.Escape:
                    {
                        gameForm.Close();
                        break;
                    }
            }
        }

        public void onKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.R)
            {
                resetGame();
            }

            if (jumping)
            {
                jumping = false;
            }
        }

        internal void onLevelUp(object sender, LevelUpEventArgs e)
        {
            obstacleSpeed = 7 + (e.level * 2);
        }

        public void resetGame()
        {
            ticks = 0;
            force = 12;
            trex.Top = floorHeight - trex.Height;
            jumpSpeed = 0;
            jumping = false;
            obstacleSpeed = 8;
            score.Reset();
            trex.Image = Properties.Resources.running;

            // Ponowna relokacja przeszkód
            obstacles.ForEach((x) => {
                if (x is PictureBox && ((string)x.Tag) == "obstacle")
                {
                    x.Left = gameForm.ClientSize.Width + rand.Next(200, 800) + x.Width * 3;
                }
            });

            gameTimer.Start();
        }

    }
}
