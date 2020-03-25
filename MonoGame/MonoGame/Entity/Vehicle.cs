using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame
{
    public class Vehicle : MovingEntity
    {
        public Color VColor { get; set; }

        public Texture2D texture { get; set; }

        public Vehicle(Vector2D pos, Game1 w, GraphicsDeviceManager g) : base(pos, w, g)
        {
            Velocity = new Vector2D(0, 0);
            Scale = 5;

            texture = new Texture2D(g.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
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
