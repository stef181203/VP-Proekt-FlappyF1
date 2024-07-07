using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyF1
{
    public class Player
    {
        private static int JumpForce = -10;
        private static int CAR_HEIGHT = 30;
        private static int CAR_WIDTH = 100;
        private static int PLAYER_HEIGHT = 60;
        private static int PLAYER_WIDTH = 60;
        private static Color COLOR = Color.Transparent;
        private static Image CAR_IMAGE = FlappyF1.Properties.Resources.Car;
        private static Image PLAYER_IMAGE = FlappyF1.Properties.Resources.HamiltonBezVrat;
        private int Velocity { get; set; }
        public Point Location { get; set; } 
        public Rectangle CarRect { get; set; }
        public Rectangle PlayerRect { get; set; }
        

        public Player(Point Location)
        {
            this.Location = Location;
            this.Velocity = 0;
        }

        public void Draw(Graphics g)
        {
            CarRect = new Rectangle(Location.X, Location.Y, CAR_WIDTH, CAR_HEIGHT);
            PlayerRect = new Rectangle(Location.X + 20, Location.Y - 41, PLAYER_WIDTH, PLAYER_HEIGHT);

            Pen p = new Pen(COLOR);

            g.DrawRectangle(p, CarRect);
            g.DrawImage(CAR_IMAGE, CarRect);

            g.DrawRectangle(p, PlayerRect);
            g.DrawImage(PLAYER_IMAGE, PlayerRect);

            p.Dispose();
        }

        public void Jump()
        {
            Velocity = JumpForce;
        }

        public void Move()
        {
            Location = new Point(Location.X, Location.Y + Velocity);
            if(Velocity < 20)
            {
                Velocity++;
            }
        }

        public bool IsColliding(Rectangle rectangle)
        {
            return this.CarRect.IntersectsWith(rectangle) || this.PlayerRect.IntersectsWith(rectangle);
        }

        public bool IsBelowTheScreen()
        {
            return this.PlayerRect.Location.Y > 720;
        }
    }
}