using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Entity;

namespace MonoGame
{
    public class Survivor : MovingEntity
    {
        public Color VColor { get; set; }

        public Survivor(Vector2 pos, EntityManager em) : base(pos, em)
        {
            Velocity = new Vector2(0, 0f);
            Scale = 5;
        }

        public override void Render(SpriteBatch sb)
        {
            double leftCorner = Pos.X - Scale;
            double rightCorner = Pos.Y - Scale;
            double size = Scale * 2;

            sb.Draw(em.survivorTexture, Pos, Color.White);
            sb.DrawLine(new Vector2((int)Pos.X, (int)Pos.Y), new Vector2((int)Pos.X + (int)(Velocity.X * 2), (int)Pos.Y + (int)(Velocity.Y * 2)), VColor, thickness: 2);

            base.Render(sb);
        }
    }
}
