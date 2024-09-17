namespace Server.Models.Figures.Interfaces
{
    using Server.Models.Figures.Datas;
    using Server.Models.Map.Datas;

    public interface IFigureModel
    {
        Direction[] Directions { get; }

        bool MoveToIfCan(Vector2Int vector2);
    }
}
