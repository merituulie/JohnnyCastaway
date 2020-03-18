using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.entity
{
    class Thief : MovingEntity
    {

        private Image thiefImage;

        public Thief(Vector2D pos, World w) : base(pos, w)
        {
            Velocity = new Vector2D(0, 0);
        }

        private void LoadResources()
        {
            thiefImage = Image.FromFile("C:\\Users\\Merit\\Documents\\School\\AI and Algorithms\\AIProject\\GameAI\\Ninja\\Run__000.png");
        }

        public override void Render(Graphics g)
        {
            LoadResources();
            g.DrawImage(thiefImage, new Point(0, 0));
        }
    }
}
