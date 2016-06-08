using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace survive_the_future
{
    class GameBoardTile
    {
        #region Fields

        // drawing support
        Texture2D sprite;
        Rectangle drawRectangle;
        int halfDrawRectangleWidth;
        int halfDrawRectangleHeight;
        Vector2 location;

        // click processing
        bool leftClickStarted = false;
        bool leftButtonReleased = true;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sprite">sprite for the tile texture</param>
        /// <param name="location">location of the center of the tile</param>
        public GameBoardTile(Texture2D sprite, Vector2 location)
        {
            this.sprite = sprite;
            this.location = location;

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
                        leftClickStarted = false;
                    }
                }
            }
            else
            {
                // no clicking on tile
                leftClickStarted = false;
                leftButtonReleased = false;
            }
        }

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
    }
}
