
namespace SurviveTheFuture
{
    public class GP_ArmyMan : GamePiece
    {
        private static string spriteResLoc = @"graphics\gamePiece_ArmyMan_01";

        public GP_ArmyMan(int row, int col, int tileWidth, int tileHeight) :
            base(spriteResLoc, row, col, tileWidth, tileHeight) { }
    }
}
