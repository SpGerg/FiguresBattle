namespace Server.Models.Map.Interfaces
{
    using Server.Controllers.ChessGame.Datas.DTOs;
    using Server.Models.Figures.Datas;
    using Server.Models.Figures.Interfaces;
    using Server.Models.Map.Datas;

    public interface IMapModel
    {
        IReadOnlyList<ChessMoveDTO[]> ChessMoves { get; }

        int ChessMoveCount { get; }

        IFigureModel GetFigure(Vector2Int vector2);

        IFigureModel GetFigure(int x, int y);

        void SetFigure(IFigureModel figure, Vector2Int vector2);

        void MoveTo(Vector2Int[] oldPositions, Vector2Int[] newPositions);

        void MoveTo(Vector2Int oldPosition, Vector2Int newPosition);

        bool IsCanMoveTo(Direction[] directions, Vector2Int vector2Int);
    }
}
