﻿using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SurviveTheFuture
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class SurviveTheFuture : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        const int WindowWidth = 1280;
        const int WindowHeight = 760;

        // click processing
        //bool rightClickStarted = false;
        //bool rightButtonReleased = true;

        GameBoard board;
        int numrows = 6;
        int numcols = 16;
        Vector2 boardCenter = new Vector2(WindowWidth / 2, WindowHeight / 2);
        private Color[,] tileShadingMap = new Color[6, 16]
        {
            { Color.LightSkyBlue, Color.LightSkyBlue, Color.LightSkyBlue, Color.White, Color.White, Color.White, Color.White, Color.White,
                Color.White, Color.White, Color.White, Color.White, Color.White, Color.LightGray, Color.LightGray, Color.LightGray },
            { Color.LightSkyBlue, Color.LightSkyBlue, Color.LightSkyBlue, Color.White, Color.White, Color.White, Color.White, Color.White,
                Color.White, Color.White, Color.White, Color.White, Color.White, Color.LightGray, Color.LightGray, Color.LightGray },
            { Color.LightSkyBlue, Color.LightSkyBlue, Color.LightSkyBlue, Color.White, Color.White, Color.White, Color.White, Color.White,
                Color.White, Color.White, Color.White, Color.White, Color.White, Color.LightGray, Color.LightGray, Color.LightGray },
            { Color.LightSkyBlue, Color.LightSkyBlue, Color.LightSkyBlue, Color.White, Color.White, Color.White, Color.White, Color.White,
                Color.White, Color.White, Color.White, Color.White, Color.White, Color.LightGray, Color.LightGray, Color.LightGray },
            { Color.LightSkyBlue, Color.LightSkyBlue, Color.LightSkyBlue, Color.White, Color.White, Color.White, Color.White, Color.White,
                Color.White, Color.White, Color.White, Color.White, Color.White, Color.LightGray, Color.LightGray, Color.LightGray },
            { Color.LightSkyBlue, Color.LightSkyBlue, Color.LightSkyBlue, Color.White, Color.White, Color.White, Color.White, Color.White,
                Color.White, Color.White, Color.White, Color.White, Color.White, Color.LightGray, Color.LightGray, Color.LightGray },
        };

        Texture2D tileTexture;
        Texture2D tileTextureHighlight;
        Rectangle drawRectangle;

        // pieces contained on tile
        List<GamePiece> pieces = new List<GamePiece>();

        /// <summary>
        /// constructor for game (akin to a main() for the game)
        /// </summary>
        public SurviveTheFuture()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = WindowWidth;
            graphics.PreferredBackBufferHeight = WindowHeight;
            IsMouseVisible = true;

            ResourceRegistry.CM = Content;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
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
            ResourceRegistry.Registry.Add(@"graphics\gameboardTile_01", tileTexture);
            tileTextureHighlight = Content.Load<Texture2D>(@"graphics\gameboardTile_01_highlight");
            ResourceRegistry.Registry.Add(@"graphics\gameboardTile_01_highlight", tileTextureHighlight);

            board = new GameBoard(tileTexture, tileTextureHighlight, boardCenter, numcols, numrows, tileShadingMap);

            // Add initial army men surrounding base houses.
            for (int i = 0; i < numrows; i++)
            {
                pieces.Add(new GP_ArmyMan(i, 3, tileTexture.Width, tileTexture.Height, false));
                pieces.Add(new GP_ArmyMan(i, 12, tileTexture.Width, tileTexture.Height, true));
            }

            // Add zombie kings to chests.
            pieces.Add(new GP_ZombieKing(3, 1, tileTexture.Width, tileTexture.Height, false));
            pieces.Add(new GP_ZombieKing(3, 14, tileTexture.Width, tileTexture.Height, true));

            // Add zombie guards to hands.
            pieces.Add(new GP_ZombieGuard(3, 6, tileTexture.Width, tileTexture.Height, false));
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

            board.Update(gameTime, mouse, pieces);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.BurlyWood);

            spriteBatch.Begin();

            drawRectangle.Width = tileTexture.Width;
            drawRectangle.Height = tileTexture.Height;

            board.Draw(spriteBatch, pieces);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
