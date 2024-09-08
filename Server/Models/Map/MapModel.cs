namespace Server.Models.Map
{
    using Server.Models.Figures.Interfaces;
    using Server.Models.Map.Datas;

    public class MapModel
    {
        private const int Width = 8;

        private const int Height = 8;

        private readonly IFigureModel[,] _map = new IFigureModel[Width, Height];

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

        public void SetFigure(IFigureModel figure, Vector2Int vector2)
        {
            _map[vector2.X, vector2.Y] = figure;
        }
    }
}
