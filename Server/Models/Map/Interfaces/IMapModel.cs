namespace Server.Models.Map.Interfaces
{
    using Server.Models.Figures.Interfaces;
    using Server.Models.Map.Datas;

    public interface IMapModel
    {
        public IFigureModel GetFigure(Vector2Int vector2);

        public void SetFigure(IFigureModel figure, Vector2Int vector2);
    }
}
