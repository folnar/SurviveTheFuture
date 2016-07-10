namespace SurviveTheFuture
{
    /// <summary>
    /// 
    /// </summary>
    public class GP_ZombieKing : GamePiece
    {
        private static string spriteResLoc = @"graphics\gamePiece_ZombieKing_01";

        private int[,] moveMatrixDef = new int[3, 3]
        {
            { 1, 1, 1},
            { 1, 0, 1},
            { 1, 1, 1}
        };

        /// <summary>
        /// 
        /// </summary>
        public override int[,] MoveMatrix
        {
            get { return moveMatrixDef; }
            set { moveMatrixDef = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="tileWidth"></param>
        /// <param name="tileHeight"></param>
        /// <param name="flipMoveMatrix"></param>
        public GP_ZombieKing(int row, int col, int tileWidth, int tileHeight, bool flipMoveMatrix) :
            base(spriteResLoc, row, col, tileWidth, tileHeight, flipMoveMatrix)
        {
        }
    }
}
