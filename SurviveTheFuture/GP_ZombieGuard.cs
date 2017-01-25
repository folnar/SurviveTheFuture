using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SurviveTheFuture
{
    class GP_ZombieGuard : GamePiece
    {
        private static string spriteResLoc = @"graphics\gamePiece_ZombieGuard_01";

        private int[,] moveMatrixDef = new int[3, 3]
        {
            { 0, 0, 2},
            { 0, 0, 0},
            { 0, 0, 2}
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
        public GP_ZombieGuard(int row, int col, int tileWidth, int tileHeight, bool flipMoveMatrix) :
            base(spriteResLoc, row, col, tileWidth, tileHeight, flipMoveMatrix)
        {
        }
    }
}
