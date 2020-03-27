using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Entity;

namespace MonoGame
{
    public class Survivor : MovingEntity
    {
        public Color VColor { get; set; }

        public Texture2D texture { get; set; }

        public Survivor(Vector2 pos, Game1 w, EntityManager em) : base(pos, w, em)
        {
            Velocity = new Vector2(0, 0f);
            Scale = 5;
        }

        public override void Render(SpriteBatch s)
        {
            double leftCorner = Pos.X - Scale;
            double rightCorner = Pos.Y - Scale;
            double size = Scale * 2;

            s.Draw(texture, new Vector2((int)Pos.X,(int)Pos.Y));
            s.DrawLine(new Vector2((int)Pos.X, (int)Pos.Y), new Vector2((int)Pos.X + (int)(Velocity.X * 2), (int)Pos.Y + (int)(Velocity.Y * 2)), VColor, thickness: 2);
        }

        public void LoadTexture(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("preview_idle");
        }
    }
}
