using System.Collections.Generic;
using System.Linq;

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
        Color tileShading;

        // click processing
        bool leftClickStarted = false;
        bool leftButtonReleased = true;
        bool rightClickStarted = false;
        bool rightButtonReleased = true;
        bool isSelected = false;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sprite">sprite for the tile texture</param>
        /// <param name="location">location of the center of the tile</param>
        public GameBoardTile(Texture2D sprite, Texture2D tileHighlight, Vector2 location, int x, int y, Color tileShading)
        {
            this.sprite = sprite;
            spriteHighlight = tileHighlight;
            this.location = location;
            boardCol = x;
            boardRow = y;
            this.tileShading = tileShading;

            // Set draw rectangle so tile is centered on location
            drawRectangle = new Rectangle((int)location.X - sprite.Width / 2,
                (int)location.Y - sprite.Height / 2, sprite.Width, sprite.Height);

            // Set halfDrawRectangleWidth and halfDrawRectangleHeight for efficiency.
            halfDrawRectangleHeight = drawRectangle.Height / 2;
            halfDrawRectangleWidth = drawRectangle.Width / 2;
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
        public void Update(GameTime gameTime, MouseState mouse, List<GamePiece> pieces, List<GameBoardTile> boardArr)
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
                        if (pieces.Where(s => s.BoardCol == boardCol && s.BoardRow == boardRow).ToList().Count > 0)
                        {
                            isSelected = !isSelected;
                            // Unselect all pieces.
                            pieces.ForEach(u => u.IsSelected = false);
                            // Select the pieces in this tile.
                            pieces.Where(s => s.BoardCol == boardCol && s.BoardRow == boardRow).ToList().ForEach(s => s.IsSelected = !s.IsSelected);
                        }
                        else
                        {
                            boardArr.ForEach(u => u.isSelected = false);
                            //isSelected = true;
                            pieces.Where(s => s.IsSelected).ToList().ForEach(s => s.Move(boardRow, boardCol));
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
        public void Draw(SpriteBatch spriteBatch, List<GamePiece> pieces)
        {
            spriteBatch.Draw(sprite, drawRectangle, tileShading);

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
