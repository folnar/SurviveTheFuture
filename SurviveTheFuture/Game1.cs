using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace survive_the_future
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        const int WindowWidth = 1280;
        const int WindowHeight = 760;

        // click processing
        bool rightClickStarted = false;
        bool rightButtonReleased = true;

        GameBoard board;
        int numrows = 6;
        int numcols = 16;
        Vector2 boardCenter = new Vector2(WindowWidth / 2, WindowHeight / 2);

        Texture2D tileTexture;
        Rectangle drawRectangle;

        Texture2D armyMan;
        Rectangle gamePieceDrawRectangle;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = WindowWidth;
            graphics.PreferredBackBufferHeight = WindowHeight;

            IsMouseVisible = true;
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

            tileTexture = Content.Load<Texture2D>(@"graphics\gameboardTile_01");
            armyMan = Content.Load<Texture2D>(@"graphics\gamePiece_ArmyMan_01");

            board = new GameBoard(tileTexture, boardCenter, numcols, numrows);
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

            MouseState mouse = Mouse.GetState();

            if (mouse.RightButton == ButtonState.Pressed && rightButtonReleased)
            {
                rightClickStarted = true;
                rightButtonReleased = false;
            }
            else if (mouse.RightButton == ButtonState.Released)
            {
                rightButtonReleased = true;

                // if right click finished, add new pickup to list
                if (rightClickStarted)
                {
                    rightClickStarted = false;
                }
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

            spriteBatch.Begin();

            drawRectangle.Width = tileTexture.Width;
            drawRectangle.Height = tileTexture.Height;

            board.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
