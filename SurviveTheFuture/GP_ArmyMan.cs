﻿namespace SurviveTheFuture
{
    /// <summary>
    /// 
    /// </summary>
    public class GP_ArmyMan : GamePiece
    {
        private static string spriteResLoc = @"graphics\gamePiece_ArmyMan_01";

        private int[,] moveMatrixDef = new int[3, 3]
        {
            { 0, 1, 0},
            { 1, 0, 1},
            { 0, 1, 0}
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
        public GP_ArmyMan(int row, int col, int tileWidth, int tileHeight, bool flipMoveMatrix) :
            base(spriteResLoc, row, col, tileWidth, tileHeight, flipMoveMatrix)
        {
        }
    }
}
