using AxWMPLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlappyF1
{
    public partial class GameForm : Form
    {
        // Form height = 649 -> 720 = 71
        // Form width = 1106 -> 1280 = 174
        // Passing window height = 160
        public GameOverForm GameOverForm { get; set; }
        private AxWindowsMediaPlayer GameStartMediaPlayer;
        private AxWindowsMediaPlayer GameScoreMediaPlayer;
        private AxWindowsMediaPlayer GameOverMediaPlayer;

        private GameScene GameScene;
        private int score = 0;
        public GameForm()
        {
            InitializeComponent();
            GameScene = new GameScene();

            PlayStartGameSound();
            timer1.Start();
            DoubleBuffered = true;
            lblScore.Text = $"Score: {score}";
        }

        private void PlayStartGameSound()
        {
            GameStartMediaPlayer = new AxWindowsMediaPlayer();
            GameStartMediaPlayer.CreateControl();
            GameStartMediaPlayer.URL = @".\Resources\Sounds\StartGame.mp3";
            GameStartMediaPlayer.settings.volume = 20;
            GameStartMediaPlayer.Ctlcontrols.play();
        }

        private void PlayScoreSound()
        {
            GameScoreMediaPlayer = new AxWindowsMediaPlayer();
            GameScoreMediaPlayer.CreateControl();
            GameScoreMediaPlayer.URL = @".\Resources\Sounds\Pass.mp3";
            GameScoreMediaPlayer.settings.volume = 20;
            GameScoreMediaPlayer.Ctlcontrols.play();
        }

        private void PlayGameOverSound()
        {
            GameOverMediaPlayer = new AxWindowsMediaPlayer();
            GameOverMediaPlayer.CreateControl();
            GameOverMediaPlayer.URL = @".\Resources\Sounds\IAmStupid.mp3";
            GameOverMediaPlayer.settings.volume = 100;
            GameOverMediaPlayer.Ctlcontrols.play();
        }

        public void UpdateScore()
        {
            score++;
            lblScore.Text = $"Score: {score}" ;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GameScene.LoadObjects();
            Invalidate();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                GameScene.allowedToJump = true;
            }
            Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && GameScene.allowedToJump)
            {
                GameScene.PlayerJump();
                GameScene.allowedToJump = false;
            }

            Invalidate();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (GameScene.CheckCollision())
            {
                timer1.Stop();
                GameStartMediaPlayer.Ctlcontrols.stop();
                GameOverForm.Show();
                PlayGameOverSound();
            }

            if(GameScene.CheckScore())
            {
                UpdateScore();
                if(score % 5  == 0)
                {
                    GameScene.UpdateTireSpeed();
                }
            }

            if(GameScene.CheckScoreSound())
            {
                PlayScoreSound();
            }
            
            GameScene.MoveTires(640);
            GameScene.MovePlayer();
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            GameScene.Draw(e.Graphics);
        }

        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
