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
        private int boardRow;
        private int boardCol;

        // drawing support
        private Texture2D sprite;
        private Texture2D spriteHighlight;
        private Rectangle drawRectangle;
        private int halfDrawRectangleWidth;
        private int halfDrawRectangleHeight;
        private Vector2 location;
        private Color tileShading;

        // click processing
        private bool leftClickStarted = false;
        private bool leftButtonReleased = true;
        private bool rightClickStarted = false;
        private bool rightButtonReleased = true;
        private bool isSelected = false;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sprite">sprite for the tile texture</param>
        /// <param name="tileHighlight">overlay sprite for the highlighted tile</param>
        /// <param name="location">location of the center of the tile</param>
        /// <param name="x">the board column of this tile</param>
        /// <param name="y">the board row of this tile</param>
        /// <param name="tileShading">the color with which this tile is drawn (def: Color.White)</param>
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
        /// <param name="pieces">a list of current game pieces</param>
        /// <param name="boardArr">a list of game board tiles</param>
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
                        // This if() dictates that only board tiles which contain pieces may be highlighted.
                        if (pieces.Where(s => s.BoardCol == boardCol && s.BoardRow == boardRow).ToList().Count > 0)
                        {
                            // Toggle the selected state of this board tile.
                            isSelected = !isSelected;
                            
                            // Unselect all pieces.
                            pieces.ForEach(u => u.IsSelected = false);

                            // Select the pieces in this tile only if the tile is highlighted / selected.
                            if (isSelected)
                            {
                                boardArr.ForEach(s => s.isSelected = false);
                                pieces.Where(s => s.BoardCol == boardCol && s.BoardRow == boardRow)
                                      .ToList()
                                      .ForEach(s => s.IsSelected = !s.IsSelected);

                                // Highlight the squares to which the selected piece(s) can legally move.
                                // See Note 2-A.
                                List<int[,]> legalMoves = new List<int[,]>();
                                pieces.Where(s => s.BoardCol == boardCol && s.BoardRow == boardRow).ToList().ForEach(s => legalMoves.Add(s.MoveMatrix));

                                // NOTE: The following selection of legal moves code only considers the 0th indexed
                                //       piece on this tile. The case of multiple pieces needs to be handled at some
                                //       time in the future. It may end up that multiple pieces are disallowed.
                                // This selects legal moves to the left and right.
                                boardArr
                                    .Where(s => s.boardCol >= (boardCol - legalMoves[0][1, 0]) &&
                                                s.boardCol <= (boardCol + legalMoves[0][1, 2]) &&
                                                s.boardRow == boardRow)
                                    .ToList()
                                    .ForEach(s => s.isSelected = true);

                                // This selects legal moves up and down.
                                boardArr
                                    .Where(s => s.boardRow >= (boardRow - legalMoves[0][0, 1]) &&
                                                s.boardRow <= (boardRow + legalMoves[0][2, 1]) &&
                                                s.boardCol == boardCol)
                                    .ToList()
                                    .ForEach(s => s.isSelected = true);

                                // This selects legal moves forward up diagonal.
                                for (int i = 0; i <= legalMoves[0][0, 2]; i++)
                                {
                                    if (!((boardRow - i) < 0 || (boardRow + i) > boardArr.Count) &&
                                        )
                                    {
                                        boardArr
                                            .Where(s => s.boardRow == (boardRow - i) &&
                                                        s.boardCol == (boardCol + i))
                                            .Single(s => s.isSelected = true);
                                    }
                                }

                                // This selects legal moves forward down diagonal.
                                for (int i = 0; i <= legalMoves[0][2, 2]; i++)
                                {
                                    if (!((boardRow - i) < 0 || (boardRow + i) > boardArr.Count))
                                    {
                                        boardArr
                                            .Where(s => s.boardRow == (boardRow + i) &&
                                                    s.boardCol == (boardCol + i))
                                        .Single(s => s.isSelected = true);
                                    }
                                }

                                // This selects legal moves backward up diagonal.
                                for (int i = 0; i <= legalMoves[0][0, 0]; i++)
                                {
                                    boardArr
                                        .Where(s => s.boardRow == (boardRow - i) &&
                                                    s.boardCol == (boardCol - i))
                                        .Single(s => s.isSelected = true);
                                }

                                // This selects legal moves backward down diagonal.
                                for (int i = 0; i <= legalMoves[0][2, 0]; i++)
                                {
                                    boardArr
                                        .Where(s => s.boardRow == (boardRow + i) &&
                                                    s.boardCol == (boardCol - i))
                                        .Single(s => s.isSelected = true);
                                }
                            }
                            else
                            {
                                boardArr.ForEach(s => s.isSelected = false);
                            }
                        }
                        else
                        {
                            // Only allow a move if this tile is selected, i.e. is a legal move for the currently selected piece.
                            if (isSelected)
                            {
                                // Unselect all board tiles.
                                boardArr.ForEach(u => u.isSelected = false);
                                // Move the selected piece(s) to the new board tile.
                                pieces.Where(s => s.IsSelected).ToList().ForEach(s => s.Move(boardRow, boardCol));
                                // Unselect all pieces.
                                pieces.ForEach(u => u.IsSelected = false);
                            }
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
                        // Unselect all board tiles.
                        boardArr.ForEach(u => u.isSelected = false);
                        // Unselect all pieces.
                        pieces.ForEach(u => u.IsSelected = false);

                        // HERE IS WHERE WE NEED TO BUILD THE POPUP MENU. WE'LL NEED A POPUP
                        // MENU CLASS FOR OPTIONS AND SAVING AND WHATNOT AND AN OBJECT WITH
                        // HIDE() AND SHOW() METHODS AS WELL AS LOCATIONN PROPERTIES WHICH
                        // WILL BE SET BY WHERE THE CURSOR IS WHEN THIS RIGHT-CLICK OCCURS.

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
        /// <param name="pieces">a list of current game pieces</param>
        public void Draw(SpriteBatch spriteBatch, List<GamePiece> pieces)
        {
            // The tileShading of this tile is determined by the tileShadeMap in SurviveTheFuture.cs.
            spriteBatch.Draw(sprite, drawRectangle, tileShading);

            if (isSelected)
            {
                spriteBatch.Draw(spriteHighlight, drawRectangle, Color.White);
            }

            // Draw the pieces in this tile.
            pieces.Where(s => s.BoardCol == boardCol && s.BoardRow == boardRow).ToList().ForEach(s => s.Draw(spriteBatch));
        }

        #endregion
    }
}
