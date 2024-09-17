namespace Server.Models.Map
{
    using Server.Controllers.ChessGame.Datas.DTOs;
    using Server.Models.Figures.Datas;
    using Server.Models.Figures.Interfaces;
    using Server.Models.Map.Datas;
    using Server.Models.Map.Interfaces;

    public class BasicMapModel : IMapModel
    {
        private const int Width = 8;

        private const int Height = 8;

        public BasicMapModel()
        {
            ChessMoves = _chessMoves;
        }

        private readonly IFigureModel[,] _map = new IFigureModel[Width, Height];

        public IFigureModel[,] Map => _map;

        public int ChessMoveCount => _chessMoveCount;

        public IReadOnlyList<ChessMoveDTO[]> ChessMoves { get; private set; }

        private readonly List<ChessMoveDTO[]> _chessMoves = [];

        private int _chessMoveCount;

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

        public IFigureModel GetFigure(Vector2Int vector2)
        {
            return _map[vector2.X, vector2.Y];
        }

        public IFigureModel GetFigure(int x, int y)
        {
            return _map[x, y];
        }

        public void SetFigure(IFigureModel figure, Vector2Int vector2)
        {
            _map[vector2.X, vector2.Y] = figure;
        }

        public bool IsCanMoveTo(Direction[] directions, Vector2Int vector2Int)
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

        public void MoveTo(Vector2Int[] oldPositions, Vector2Int[] newPositions)
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

        public void MoveTo(Vector2Int oldPosition, Vector2Int newPosition)
        {
            var figure = GetFigure(oldPosition);

            SetFigure(figure, newPosition);
        }
    }
}
