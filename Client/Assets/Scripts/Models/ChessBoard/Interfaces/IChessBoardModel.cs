using Datas;
using Models.Figures.Enums;

namespace Models.ChessBoard.Interfaces
{
    public interface IChessBoardModel
    {
        void SetFigure(FigureType figureType, Vector2Int vector2Int);
    }
}
