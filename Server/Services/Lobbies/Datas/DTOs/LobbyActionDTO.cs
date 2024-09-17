namespace Server.Services.Lobbies.Datas.DTOs
{
    using Server.Services.Lobbies.Enums;

    public class LobbyActionDTO
    {
        public LobbyActionType Type { get; set; }

        public object? Value { get; set; }
    }
}
