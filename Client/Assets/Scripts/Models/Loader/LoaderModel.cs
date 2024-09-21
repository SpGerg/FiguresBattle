using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

namespace Models.Loader
{
    using Models.Loader.Datas.DTOs;
    using Models.Loader.Interfaces;
    using Presenters.ChessBoard;

    public class LoaderModel : ILoaderModel
    {
        public LoaderModel(int id, ChessBoardPresenter chessBoardPresenter, string apiPath)
        {
            ChessGameId = id;
            ChessBoardPresenter = chessBoardPresenter;

            _apiPath = apiPath;
            _client = new HttpClient();
        }

        public int ChessGameId { get; set; }

        public ChessBoardPresenter ChessBoardPresenter { get; }

        private readonly string _apiPath;

        private readonly HttpClient _client;

        private async Task<ChessMoveDTO> WaitForChessMove()
        {
            using var response = await _client.GetAsync(_apiPath + $"/update?id={ChessGameId}");
            var serialized = await response.Content.ReadAsStringAsync();

            return JsonUtility.FromJson<ChessMoveDTO>(serialized);
        }
    }
}
