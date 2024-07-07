using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyF1
{
    public class Tires
    {
        private static int HEIGHT = 360;
        private static int WIDTH = 100;
        private static Color COLOR = Color.Transparent;
        private static Image IMAGE_TOP = FlappyF1.Properties.Resources.TiresDoneFlipped;
        private static Image IMAGE_BOTTOM = FlappyF1.Properties.Resources.TiresDone2;
        public bool isUpperSetTires { get; set; }
        public Point Location { get; set; }
        public Rectangle Rect { get; set; }
        public bool GenerateNew { get; set; }
        public bool ScoreUpdated { get; set; }
        public bool ScoreSoundPassed { get; set; }


        public Tires(bool isUpperSetTires, Point Location)
        {
            this.isUpperSetTires = isUpperSetTires;
            this.Location = Location;
            this.GenerateNew = true;
            this.ScoreUpdated = false;
            this.ScoreSoundPassed = false;
        }

        public void Draw(Graphics g)
        {
            Rect = new Rectangle(Location.X, Location.Y, WIDTH, HEIGHT);
            Pen p = new Pen(COLOR);
            g.DrawRectangle(p, Rect);
            if(isUpperSetTires)
            {
                g.DrawImage(IMAGE_TOP, Rect);
            }
            else
            {
                g.DrawImage(IMAGE_BOTTOM, Rect);
            }
            p.Dispose();
        }

        public void Move(int Speed)
        {
            Location = new Point(Location.X + Speed, Location.Y);
        }
    }
}
