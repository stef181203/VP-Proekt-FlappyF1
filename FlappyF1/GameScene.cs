using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyF1
{
    public class GameScene
    {
        private Player Player;
        private List<Tires> UpperTires;
        private List<Tires> LowerTires;
        private int TireSpeed { get; set; }
        public bool allowedToJump { get; set; }
         

        private static Random RANDOM = new Random();
        private static int WINDOW_HEIGHT = 160;
        private static int TIRES_HEIGHT = 360;
        private static int TIRES_WIDTH = 100;
        private static int TIRES_LOCATION_X = 970;
        private static Point PLAYER_STARTING_POSITION = new Point(160, 270);
        public GameScene()
        {
            UpperTires = new List<Tires>();
            LowerTires = new List<Tires>();
            allowedToJump = true;
            TireSpeed = -5;
        }

        public void Draw(Graphics g)
        {
            Player.Draw(g);

            for (int i = 0; i < UpperTires.Count; i++)
            {
                UpperTires[i].Draw(g);
                LowerTires[i].Draw(g);
            }
        }

        public void AddTires()
        {
            int locationUpperY = RANDOM.Next(-250, 0);
            int locationLowerY = (locationUpperY + TIRES_HEIGHT) + WINDOW_HEIGHT;

            UpperTires.Add(new Tires(true, new Point(TIRES_LOCATION_X, locationUpperY)));
            LowerTires.Add(new Tires(false, new Point(TIRES_LOCATION_X, locationLowerY)));
        }

        public void LoadObjects()
        {
            Player = new Player(PLAYER_STARTING_POSITION);
            AddTires();
        }

        public void MovePlayer()
        {
            Player.Move();
        }

        public void PlayerJump()
        {
            Player.Jump();
        }

        public void MoveTires(int width)
        {
            for (int i = 0; i < UpperTires.Count; i++)
            {
                UpperTires[i].Move(TireSpeed);
                LowerTires[i].Move(TireSpeed);

                if (UpperTires[i].Location.X < width && UpperTires[i].GenerateNew || LowerTires[i].Location.X < width && LowerTires [i].GenerateNew)
                {
                    UpperTires[i].GenerateNew = false;
                    LowerTires[i].GenerateNew = false;
                    AddTires();
                    break;
                }

                if (UpperTires[i].Location.X < -TIRES_WIDTH || LowerTires[i].Location.X < -TIRES_WIDTH)
                {
                    UpperTires.RemoveAt(i);
                    LowerTires.RemoveAt(i);
                    break;
                }
            }
        }

        public bool CheckCollision()
        {
            for(int i = 0; i < UpperTires.Count; i++)
            {
                if (Player.IsColliding(UpperTires[i].Rect) || Player.IsColliding(LowerTires[i].Rect) || Player.IsBelowTheScreen())
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckScore()
        {
            for (int i = 0; i < UpperTires.Count; i++)
            {
                if ((UpperTires[i].Location.X + TIRES_WIDTH) < PLAYER_STARTING_POSITION.X && !UpperTires[i].ScoreUpdated || (LowerTires[i].Location.X + TIRES_WIDTH) < PLAYER_STARTING_POSITION.X && !LowerTires[i].ScoreUpdated)
                {
                    UpperTires[i].ScoreUpdated = true;
                    LowerTires[i].ScoreUpdated = true; 
                    return true;
                }
            }
            return false;
        }

        public bool CheckScoreSound()
        {
            for (int i = 0; i < UpperTires.Count; i++)
            {
                if (UpperTires[i].Location.X < PLAYER_STARTING_POSITION.X && !UpperTires[i].ScoreSoundPassed || LowerTires[i].Location.X < PLAYER_STARTING_POSITION.X && !LowerTires[i].ScoreSoundPassed)
                {
                    UpperTires[i].ScoreSoundPassed = true;
                    LowerTires[i].ScoreSoundPassed = true;
                    return true;
                }
            }
            return false;
        }

        public void UpdateTireSpeed()
        {
            TireSpeed--;
        }
    }
}
