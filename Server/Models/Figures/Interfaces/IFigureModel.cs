namespace Server.Models.Figures.Interfaces
{
    using Server.Models.Map.Datas;

    public interface IFigureModel
    {
        void MoveTo(Vector2Int vector2);
    }
}
