namespace Server.Models.Map
{
    using Server.Controllers.ChessGame.Datas.DTOs;
    using Server.Models.ChessBoard;
    using Server.Models.Figures.Datas;
    using Server.Models.Figures.Interfaces;
    using Server.Models.Map.Datas;

    public class BasicChessBoardModel : ChessBoardModel
    {
        private const int Width = 8;

        private const int Height = 8;

        public BasicChessBoardModel()
        {
            ChessMoves = _chessMoves;
        }

        private readonly IFigureModel[,] _map = new IFigureModel[Width, Height];

        private readonly List<ChessMoveDTO[]> _chessMoves = [];

        private int _chessMoveCount;

        public override IFigureModel[,] Map => _map;

        public override IReadOnlyList<ChessMoveDTO[]> ChessMoves { get; protected set; }

        public override int ChessMoveCount => _chessMoveCount;

        public IFigureModel this[Vector2Int vector2]
        {
            get
            {
                return GetFigure(vector2);
            }
            set
            {
                SetFigure(value, vector2);
            }
        }

        public override IFigureModel GetFigure(Vector2Int vector2)
        {
            return _map[vector2.X, vector2.Y];
        }

        public override IFigureModel GetFigure(int x, int y)
        {
            return _map[x, y];
        }

        public override void SetFigure(IFigureModel figure, Vector2Int vector2)
        {
            _map[vector2.X, vector2.Y] = figure;
        }

        public override bool IsCanMoveTo(Direction[] directions, Vector2Int vector2Int)
        {
            foreach (var direction in directions)
            {
                if (direction.IsToEnd)
                {
                    //Тут будет код в будущем.
                    //Беспонятия как пока это написать.
                    //IsToEnd это типо когда фигура идёт до конца доски (например король так ходит).

                    continue;
                }

                var position = direction.Position + vector2Int;

                if (position.X > Width || position.X < 0)
                {
                    return false;
                }

                if (position.Y > Height || position.Y < 0)
                {
                    return false;
                }

                return true;
            }

            return false;
        }

        public override void MoveTo(Vector2Int[] oldPositions, Vector2Int[] newPositions)
        {
            if (oldPositions.Length != newPositions.Length)
            {
                throw new Exception("Old positions and new positions must have same length");
            }

            _chessMoveCount++;

            for (var i = 0; i < oldPositions.Length; i++)
            {
                MoveTo(oldPositions[i], newPositions[i]);
            }
        }

        public override void MoveTo(Vector2Int oldPosition, Vector2Int newPosition)
        {
            var figure = GetFigure(oldPosition);

            SetFigure(figure, newPosition);
        }
    }
}
