using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SurviveTheFuture
{
    class GameBoardTile
    {
        #region Fields

        // board coordinates
        int boardRow;
        int boardCol;

        // drawing support
        Texture2D sprite;
        Texture2D spriteHighlight;
        Rectangle drawRectangle;
        int halfDrawRectangleWidth;
        int halfDrawRectangleHeight;
        Vector2 location;

        // click processing
        bool leftClickStarted = false;
        bool leftButtonReleased = true;
        bool rightClickStarted = false;
        bool rightButtonReleased = true;
        bool isSelected = false;

        // pieces contained on tile
        List<GamePiece> pieces = new List<GamePiece>();

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sprite">sprite for the tile texture</param>
        /// <param name="location">location of the center of the tile</param>
        public GameBoardTile(Texture2D sprite, Texture2D tileHighlight, Vector2 location, int x, int y)
        {
            this.sprite = sprite;
            spriteHighlight = tileHighlight;
            this.location = location;
            boardCol = x;
            boardRow = y;

            // Set draw rectangle so tile is centered on location
            drawRectangle = new Rectangle((int)location.X - sprite.Width / 2,
                (int)location.Y - sprite.Height / 2, sprite.Width, sprite.Height);

            // Set halfDrawRectangleWidth and halfDrawRectangleHeight for efficiency.
            halfDrawRectangleHeight = drawRectangle.Height / 2;
            halfDrawRectangleWidth = drawRectangle.Width / 2;

            if (x == 3 || x == 12)
            {
                pieces.Add(new GP_ArmyMan(boardRow, boardCol, drawRectangle.Width, drawRectangle.Height));
            }
        }

        #endregion

        #region Properties

        #endregion

        #region Public methods

        /// <summary>
        /// Updates the tile
        /// </summary>
        /// <param name="gameTime">game time</param>
        /// <param name="mouse">current mouse state</param>
        public void Update(GameTime gameTime, MouseState mouse)
        {
            // check for mouse over tile
            if (drawRectangle.Contains(mouse.X, mouse.Y))
            {
                // check for left click started on tile
                if (mouse.LeftButton == ButtonState.Pressed &&
                    leftButtonReleased)
                {
                    leftClickStarted = true;
                    leftButtonReleased = false;
                }
                else if (mouse.LeftButton == ButtonState.Released)
                {
                    leftButtonReleased = true;

                    // if click finished on tile, ...
                    if (leftClickStarted)
                    {
                        if (pieces.Count > 0)
                        {
                            isSelected = !isSelected;
                        }
                        else
                        {
                            pieces.Add(new GP_ArmyMan(boardRow, boardCol, drawRectangle.Width, drawRectangle.Height));
                        }
                        leftClickStarted = false;
                    }
                }
                // check for right click started on tile
                if (mouse.RightButton == ButtonState.Pressed &&
                    rightButtonReleased)
                {
                    rightClickStarted = true;
                    rightButtonReleased = false;
                }
                else if (mouse.RightButton == ButtonState.Released)
                {
                    rightButtonReleased = true;

                    // if click finished on tile, ...
                    if (rightClickStarted)
                    {
                        pieces.Clear();
                        isSelected = false;
                        rightClickStarted = false;
                    }
                }
            }
            else
            {
                // no clicking on tile
                leftClickStarted = false;
                leftButtonReleased = false;
                rightClickStarted = false;
                rightButtonReleased = false;
            }
        }

        /// <summary>
        /// Draws the tile
        /// </summary>
        /// <param name="spriteBatch">sprite batch</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Use the sprite batch to draw the tile.
            if (boardCol < 3)
            {
                spriteBatch.Draw(sprite, drawRectangle, Color.LightSkyBlue);

            }
            else if (boardCol > 12)
            {
                spriteBatch.Draw(sprite, drawRectangle, Color.LightGray);

            }
            else
            {
                spriteBatch.Draw(sprite, drawRectangle, Color.White);

            }
            if (isSelected)
            {
                spriteBatch.Draw(spriteHighlight, drawRectangle, Color.White);
            }

            // Draw the pieces in the this tile.
            foreach (GamePiece gp in pieces)
            {
                gp.Draw(spriteBatch);
            }
        }

        #endregion
    }
}
