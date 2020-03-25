using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace MonoGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Camera camera;

        public const float timeDelta = 0.8f;
        float timeElapsed;

        List<MovingEntity> entities = new List<MovingEntity>();
        public Vehicle Target { get; set; }
        int Width { get; set; }
        int Height { get; set; }
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            camera = new Camera(graphics.GraphicsDevice.Viewport, new Vector2(0, 0));
            InitWorld(w: GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, h: GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
            
            //world = new World(w: GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, h: GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height, graphics);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.IsMouseVisible = true;
            this.Window.AllowUserResizing = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            var mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                Target.Pos = new Vector2D(mouseState.X, mouseState.Y);
            }

            camera.UpdateCamera(graphics.GraphicsDevice.Viewport);

            foreach (MovingEntity me in entities)
            {
                me.Update(timeElapsed);
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(transformMatrix: camera.Transform);
            // TODO: Add your drawing code here
            //world.Render(spriteBatch);
            spriteBatch.DrawLine(new Vector2(0, 0), new Vector2(100, 100), Color.Black, thickness: 10);
            RenderWorld(spriteBatch);

            spriteBatch.End();


            base.Draw(gameTime);
        }

        private void InitWorld(int w, int h)
        {
            Width = w;
            Height = h;
            PopulateWorld();
        }

        private void PopulateWorld()
        {

            Target = new Vehicle(new Vector2D(100, 60), this, graphics);
            Target.VColor = Color.DarkRed;
            Target.Pos = new Vector2D(100, 40);

            Vehicle v = new Vehicle(new Vector2D(200, 200), this, graphics);
            v.VColor = Color.Blue;
            v.SB = new SeekBehaviour(v);
            entities.Add(v);

            //Vehicle vg = new Vehicle(new Vector2D(60, 60), this);
            //vg.VColor = Color.Green;
            //entities.Add(vg);
        }

        public void RenderWorld(SpriteBatch s)
        {
            entities.ForEach(e => e.Render(s));
            Target.Render(s);
        }

        public Vector2D WrapAround(Vector2D position)
        {
            return new Vector2D((position.X + Width) % Width, (position.Y + Height) % Height);
        }
    }
}
