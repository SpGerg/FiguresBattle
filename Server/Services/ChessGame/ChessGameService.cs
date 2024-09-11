namespace Server.Services.Map
{
    using Server.Models.Figures.Interfaces;
    using Server.Models.Map.Datas;
    using Server.Models.Map.Interfaces;

    public class ChessGameService(IMapModel mapModel)
    {
        public void SetFigure(IFigureModel figure, Vector2Int vector2)
        {
            mapModel.SetFigure(figure, vector2);
        }

        public IFigureModel GetFigure(Vector2Int vector2)
        {
            return mapModel.GetFigure(vector2);
        }
    }
}
