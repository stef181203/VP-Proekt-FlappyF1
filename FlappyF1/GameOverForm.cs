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
    public partial class GameOverForm : Form
    {
        public GameForm GameForm { get; set; }
        private HomeScreenForm HomeScreenForm { get; set; }
        public GameOverForm(HomeScreenForm HomeScreenForm)
        {
            InitializeComponent();
            this.HomeScreenForm = HomeScreenForm;
        }

        private void btnPlayAgain_Click(object sender, EventArgs e)
        {
            GameForm = new GameForm();
            GameForm.GameOverForm = this;

            this.Hide();
            GameForm.Show();
        }

        private void btnMainMenu_Click(object sender, EventArgs e)
        {
            GameForm.Hide();
            this.Hide();
            HomeScreenForm.Show();
            HomeScreenForm.HomeScreenMediaPlayer.Ctlcontrols.play();
        }

        private void GameOverForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
