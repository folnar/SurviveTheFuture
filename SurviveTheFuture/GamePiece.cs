﻿using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SurviveTheFuture
{
    public class GamePiece
    {
        #region Fields

        // board coordinates
        protected int boardRow;
        protected int boardCol;

        protected Texture2D sprite;
        protected Rectangle drawRectangle;

        protected int halfDrawRectangleWidth;
        protected int halfDrawRectangleHeight;

        protected int tileWidth;
        protected int tileHeight;
        protected int tileOffsetX;
        protected int tileOffsetY;

        //protected bool isInHand = false;
        //protected bool isOnBoard = false;

        #endregion

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public GamePiece()
        {
            // default constructor
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sprite">sprite for the gamepiece texture</param>
        /// <param name="row">index of the row on which the piece resides</param>
        /// <param name="col">index of the column on which the piece resides</param>
        /// <param name="tileWidth">width of the gameboard tile containing the piece</param>
        /// <param name="tileHeight">height of the gameboard tile containing the piece</param>
        public GamePiece(string spriteName, int row, int col, int tileWidth, int tileHeight)
        {
            if (ResourceRegistry.Registry.ContainsKey(spriteName))
            {
                sprite = ResourceRegistry.Registry[spriteName];
            }
            else
            {
                sprite = ResourceRegistry.CM.Load<Texture2D>(spriteName);
                ResourceRegistry.Registry[spriteName] = sprite;
            }

            _init(row, col, tileWidth, tileHeight);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Draws the tile
        /// </summary>
        /// <param name="spriteBatch">sprite batch</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Use the sprite batch to draw the tile.
            spriteBatch.Draw(sprite, drawRectangle, Color.White);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Sets the draw rectangle to be on the current gameboard tile.
        /// </summary>
        protected void setDrawRectangle()
        {
            int xpos = GameBoard.TranslateColumnToX(boardCol) + tileOffsetX;
            int ypos = GameBoard.TranslateColumnToY(boardRow) + tileOffsetX;
            drawRectangle = new Rectangle(xpos, ypos, sprite.Width, sprite.Height);
        }

        protected void _init(int row, int col, int tileWidth, int tileHeight)
        {
            boardRow = row;
            boardCol = col;
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;

            // Set halfDrawRectangleWidth and halfDrawRectangleHeight for efficiency.
            halfDrawRectangleHeight = sprite.Height / 2;
            halfDrawRectangleWidth = sprite.Width / 2;

            // Set tile offsets. This is the left and top margin basically for the piece in the tile.
            tileOffsetX = (int)Math.Floor((double)((tileWidth - sprite.Width) / 2));
            tileOffsetY = (int)Math.Floor((double)((tileHeight - sprite.Height) / 2));

            setDrawRectangle();
        }

        #endregion
    }
}