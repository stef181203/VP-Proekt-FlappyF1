using AxWMPLib;
using FlappyF1.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace FlappyF1
{
    public partial class HomeScreenForm : Form
    {
        private GameForm GameForm;
        private GameOverForm GameOverForm;
        public AxWindowsMediaPlayer HomeScreenMediaPlayer;
        public HomeScreenForm()
        {
            InitializeComponent();

            HomeScreenMediaPlayer = new AxWindowsMediaPlayer();
            HomeScreenMediaPlayer.CreateControl();
            HomeScreenMediaPlayer.URL = @".\Resources\Sounds\ThemeSong.mp3";

            HomeScreenMediaPlayer.settings.volume = 20;
            HomeScreenMediaPlayer.Ctlcontrols.play();
        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            GameForm = new GameForm();
            GameOverForm = new GameOverForm(this);

            GameForm.GameOverForm = GameOverForm;
            GameOverForm.GameForm = GameForm;

            HomeScreenMediaPlayer.Ctlcontrols.stop();
            GameForm.Show();
            this.Hide();
        }
    }
}
