﻿using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SurviveTheFuture
{
    class GameBoard
    {
        #region Fields

        static Texture2D tileSprite;
        private readonly int halfTileWidth;
        private readonly int halfTileHeight;

        List<GameBoardTile> boardArr = new List<GameBoardTile>();

        // board dimensions
        private readonly int boardWidth;
        private readonly int boardHeight;
        private readonly int halfBoardWidth;
        private readonly int halfBoardHeight;
        private static int boardOffsetX;
        private static int boardOffsetY;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tile">sprite for the tile texture</param>
        /// <param name="tileHighlight">sprite which overlays gameboard tile to highlight it</param>
        /// <param name="location">location of the center of the gameboard</param>
        /// <param name="numcols">number of columns of tiles on the gameboard</param>
        /// <param name="numrows">number of rows of tiles on the gameboard</param>
        /// <param name="tileShadingMap">a matrix of colors corresponding to the shading of the gameboard tiles</param>
        public GameBoard(Texture2D tile, Texture2D tileHighlight, Vector2 location, int numcols, int numrows, Color[,] tileShadingMap)
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

            for (int y = 0; y < numrows; y++)
            {
                for (int x = 0; x < numcols; x++)
                {
                    int translatedX = x * tile.Width + halfTileWidth + boardOffsetX;
                    int translatedY = y * tile.Height + halfTileHeight + boardOffsetY;
                    Vector2 currLocation = new Vector2(translatedX, translatedY);
                    boardArr.Add(new GameBoardTile(tileSprite, tileHighlight, currLocation, x, y, tileShadingMap[y,x]));
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
        public void Update(GameTime gameTime, MouseState mouse, List<GamePiece> pieces)
        {
            foreach (GameBoardTile currTile in boardArr)
            {
                currTile.Update(gameTime, mouse, pieces, boardArr);
            }
        }

        /// <summary>
        /// Draws the gameboard
        /// </summary>
        /// <param name="spriteBatch">sprite batch</param>
        public void Draw(SpriteBatch spriteBatch, List<GamePiece> pieces)
        {
            foreach (GameBoardTile currTile in boardArr)
            {
                currTile.Draw(spriteBatch, pieces);
            }
        }

        /// <summary>
        /// Translates a column index to the x-coordinate of the upper left-hand corner of a tile in that column.
        /// </summary>
        /// <param name="col">the index of the column</param>
        static public int TranslateColumnToX(int col)
        {
            return boardOffsetX + col * tileSprite.Width;
        }

        /// <summary>
        /// Translates a row index to the y-coordinate of the upper left-hand corner of a tile in that row.
        /// </summary>
        /// <param name="row">the index of the row</param>
        static public int TranslateColumnToY(int row)
        {
            return boardOffsetY + row * tileSprite.Height;
        }

        #endregion

    }
}
