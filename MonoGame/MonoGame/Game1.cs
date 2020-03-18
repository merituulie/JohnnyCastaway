using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MonoGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        World world;
        Camera camera;

        public const float timeDelta = 0.8f;
        float timeElapsed;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            camera = new Camera(graphics.GraphicsDevice.Viewport, new Vector2(0,0));
            world = new World(w: GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, h: GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height, graphics);
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

            timeElapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 18;

            // TODO: Add your update logic here
            var mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                world.Target.Pos = new Vector2D(mouseState.X, mouseState.Y);
            }

            //world.Update(timeElapsed);
            camera.UpdateCamera(graphics.GraphicsDevice.Viewport);

            foreach (MovingEntity me in world.entities)
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

            world.entities.ForEach(e => e.Render(spriteBatch));
            world.Target.Render(spriteBatch);

            spriteBatch.End();


            base.Draw(gameTime);
        }

        private void InitWorld()
        {

        }

        private void PopulateWorld()
        {
            Vehicle v = new Vehicle(new Vector2D(200, 200), world, graphics);
            v.VColor = Color.Blue;
            v.SB = new SeekBehaviour(v);
            world.entities.Add(v);

            //Vehicle vg = new Vehicle(new Vector2D(60, 60), this);
            //vg.VColor = Color.Green;
            //entities.Add(vg);

            world.Target = new Vehicle(new Vector2D(100, 60), world, graphics);
            world.Target.VColor = Color.DarkRed;
            world.Target.Pos = new Vector2D(100, 40);
        }
    }
}
