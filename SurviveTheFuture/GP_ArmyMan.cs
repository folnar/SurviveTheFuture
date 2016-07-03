namespace SurviveTheFuture
{
    public class GP_ArmyMan : GamePiece
    {
        private static string spriteResLoc = @"graphics\gamePiece_ArmyMan_01";

        private int[,] moveMatrixDef = new int[3, 3]
        {
            { 0, 2, 1},
            { 1, 0, 3},
            { 0, 2, 1}
        };

        public override int[,] MoveMatrix
        {
            get { return moveMatrixDef; }
            set { moveMatrixDef = value; }
        }

        public GP_ArmyMan(int row, int col, int tileWidth, int tileHeight, bool flipMoveMatrix) :
            base(spriteResLoc, row, col, tileWidth, tileHeight, flipMoveMatrix)
        {
        }
    }
}
