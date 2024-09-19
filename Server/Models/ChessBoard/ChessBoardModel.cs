namespace Server.Models.ChessBoard
{
    using Server.Controllers.ChessGame.Datas.DTOs;
    using Server.Models.Figures.Datas;
    using Server.Models.Figures.Interfaces;
    using Server.Models.Map.Datas;
    using Server.Models.Map.Interfaces;
    using System.Collections.Generic;

    public abstract class ChessBoardModel : IChessBoardModel
    {
        public abstract IReadOnlyList<ChessMoveDTO[]> ChessMoves { get; protected set; }

        public abstract int ChessMoveCount { get; }

        public abstract IFigureModel[,] Map { get; }

        public abstract IFigureModel GetFigure(Vector2Int vector2);

        public abstract IFigureModel GetFigure(int x, int y);

        public abstract bool IsCanMoveTo(Direction[] directions, Vector2Int vector2Int);

        public abstract void MoveTo(Vector2Int[] oldPositions, Vector2Int[] newPositions);

        public abstract void MoveTo(Vector2Int oldPosition, Vector2Int newPosition);

        public abstract void SetFigure(IFigureModel figure, Vector2Int vector2);
    }
}
