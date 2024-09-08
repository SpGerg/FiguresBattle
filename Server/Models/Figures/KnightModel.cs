namespace Server.Models.Figures
{
    using Server.Models.Enums;
    using Server.Models.Figures.Enums;
    using Server.Services.Accounts.Datas;

    public class KnightModel(User user, SideType side) : FigureModel(user, side)
    {
        public override FigureType FigureType => FigureType.Knight;
    }
}
