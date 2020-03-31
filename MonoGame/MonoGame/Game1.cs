using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Entity;
using MonoGame.Graph;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Graphics;
using System;
using System.Collections.Generic;
using MonoGame.Extended;

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

        private KeyboardState previousState = Keyboard.GetState();

        private TiledMap map;
        private TiledMapRenderer mapRenderer;

        public const float timeDelta = 0.8f;

        public EntityManager em = new EntityManager();

        public Graph.Graph navGraph;
        public bool showGraph = false;

        public Vector2 Target = new Vector2(100,100);

        int Width { get; set; }
        int Height { get; set; }
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1010;
            graphics.PreferredBackBufferHeight = 960;
            graphics.ApplyChanges();
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            camera = new Camera(graphics.GraphicsDevice.Viewport, new Vector2(0, 0));
            //InitWorld(w: GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, h: GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            this.IsMouseVisible = true;

            this.Window.AllowUserResizing = true;

            //Load the compiled map
            map = Content.Load<TiledMap>("Map/islandMap");

            // Make sure the graphicsdevice 
            GraphicsDevice.BlendState = BlendState.AlphaBlend;

            // Create the map renderer
            mapRenderer = new TiledMapRenderer(GraphicsDevice);

            // Initialize the entity manager
            em = new EntityManager();

            // Generate the graph with the map and static entities
            navGraph = new Graph.Graph(map, em.GetStaticEntities());

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            em.LoadContent(Content);
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

            var mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                Target = new Vector2(mouseState.X, mouseState.Y);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.G) && !previousState.IsKeyDown(Keys.G))
                showGraph = !showGraph;

            camera.UpdateCamera(graphics.GraphicsDevice.Viewport);

            mapRenderer.Update(map, gameTime);

            em.Update(gameTime);

            previousState = Keyboard.GetState();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            RenderWorld();

            if (showGraph)
                navGraph.Draw(spriteBatch);

            this.DrawTarget();

            em.Draw(spriteBatch);

            base.Draw(gameTime);
        }

        private void InitWorld(int w, int h)
        {
            Width = w;
            Height = h;
        }

        public void RenderWorld()
        {
            mapRenderer.Draw(map);
        }

        public Vector2 WrapAround(Vector2 position)
        {
            return new Vector2((position.X + Width) % Width, (position.Y + Height) % Height);
        }

        public Vector2 GetTarget()
        {
            return Target;
        }

        public void DrawTarget()
        {
            spriteBatch.Begin();
            spriteBatch.DrawCircle(Target, 5F, 12, Color.Red, 2F);
            spriteBatch.End();
        }
    }
}
