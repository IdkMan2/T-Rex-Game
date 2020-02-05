using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T_Rex_Game
{
    class ScoreController
    {
        internal static event LevelUpEventHandler LevelUpEvent;
        private Label scoreLabel;
        private Label levelLabel;
        private int score = 0;
        private int level = 1;
        internal ScoreController(Label scoreLabel, Label levelLabel)
        {
            this.scoreLabel = scoreLabel;
            this.levelLabel = levelLabel;
            attachListeners();
            UpdateScore();
        }

        internal void attachListeners()
        {
            GameManager.GameTickEvent += onGameTickEvent;
        }

        internal void detachListeners()
        {
            GameManager.GameTickEvent -= onGameTickEvent;
        }

        private void onGameTickEvent(object sender, GameTickEventArgs e)
        {
            // 20pkt na sekunde, tak jak ma Chrome
            if (e.ticks % 5 == 0)
            {
                score++;
                UpdateScore();
            }
        }

        internal void Substract(int score)
        {
            this.score -= score;
            UpdateScore();
        }

        internal void Add(int score)
        {
            this.score += score;
            UpdateScore();
        }

        internal void Reset()
        {
            this.score = 0;
            this.level = 1;
            UpdateScore();
        } 

        internal void EndGameScore()
        {
            scoreLabel.Text += "\n'R' - restart gry\n'Esc' - powrót do menu";
        }

        private void UpdateScore()
        {
            int newLevel = (score / 500) + 1;
            if(newLevel != level)
            {
                level = newLevel;
                ScoreController.LevelUpEvent(this, new LevelUpEventArgs(level));
            }
            scoreLabel.Text = "Punkty: " + score;
            levelLabel.Text = level.ToString();
        }
    }
}
