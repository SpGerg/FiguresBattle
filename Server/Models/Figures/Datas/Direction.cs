namespace Server.Models.Figures.Datas
{
    using Server.Models.Map.Datas;

    public class Direction
    {
        public required Vector2Int Position { get; init; }

        public bool IsToEnd { get; init; } 

        public bool IsEnemyRequired { get; init; }
    }
}
