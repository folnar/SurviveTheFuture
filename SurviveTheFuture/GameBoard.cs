using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace survive_the_future
{
    class GameBoard
    {
        #region Fields

        Texture2D tileSprite;
        int halfTileWidth;
        int halfTileHeight;

        List<GameBoardTile> boardArr = new List<GameBoardTile>();

        // board dimensions
        int boardWidth;
        int boardHeight;
        int halfBoardWidth;
        int halfBoardHeight;
        private int boardOffsetX;
        private int boardOffsetY;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tile">sprite for the tile texture</param>
        /// <param name="location">location of the center of the gameboard</param>
        /// <param name="numcols">number of columns of tiles on the gameboard</param>
        /// <param name="numrows">number of rows of tiles on the gameboard</param>
        public GameBoard(Texture2D tile, Vector2 location, int numcols, int numrows)
        {
            tileSprite = tile;

            // Set halfDrawRectangleWidth and halfDrawRectangleHeight for efficiency.
            halfTileHeight = tileSprite.Height / 2;
            halfTileWidth = tileSprite.Width / 2;

            // Set board dimensions.
            boardWidth = numcols * tileSprite.Width;
            boardHeight = numrows * tileSprite.Height;
            halfBoardWidth = boardWidth / 2;
            halfBoardHeight = boardHeight / 2;
            boardOffsetX = (int)location.X - halfBoardWidth;
            boardOffsetY = (int)location.Y - halfBoardHeight;

            for (int x = 0; x < numrows; x++)
            {
                for (int y = 0; y < numcols; y++)
                {
                    int translatedX = x * tile.Width + halfTileWidth + boardOffsetX;
                    int translatedY = y * tile.Height + halfTileHeight + boardOffsetY;
                    Vector2 currLocation = new Vector2(translatedY, translatedX);
                    boardArr.Add(new GameBoardTile(tileSprite, currLocation));
                }
            }
        }

        #endregion

        #region Properties

        #endregion

        #region Public methods

        /// <summary>
        /// Updates the gameboard
        /// </summary>
        /// <param name="gameTime">game time</param>
        /// <param name="mouse">current mouse state</param>
        public void Update(GameTime gameTime, MouseState mouse)
        {
        }

        /// <summary>
        /// Draws the gameboard
        /// </summary>
        /// <param name="spriteBatch">sprite batch</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (GameBoardTile currTile in boardArr)
            {
                currTile.Draw(spriteBatch);
            }
        }

        #endregion

    }
}
