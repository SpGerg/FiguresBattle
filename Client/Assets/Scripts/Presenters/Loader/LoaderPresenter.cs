using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

namespace Presenters
{
    using Presenters.ChessBoard;
    using Presenters.Loader.Datas.DTOs;
    using System;

    public class LoaderPresenter : PresenterBase
    {
        [SerializeField]
        private ChessBoardPresenter _chessBoardPresenter;

        [SerializeField]
        private int _chessGameId;

        private readonly string _apiPath = Path.Combine(Application.streamingAssetsPath, "server.txt");

        private readonly HttpClient _httpClient = new();

        private string _chessMoveApi;

        public void Awake()
        {
            _chessGameId = PlayerPrefs.GetInt();

            //Не больше двух часов ждём ход противника.
            _httpClient.Timeout = TimeSpan.FromHours(2);

            _chessMoveApi = _apiPath + "/chessgame";
        }

        public void Restart()
        {

        }

        public async Task<ChessMoveDTO> WaitForChessMove()
        {
            using var response = await _httpClient.GetAsync(_chessMoveApi + $"/update?id={_chessGameId}");
            var serialized = await response.Content.ReadAsStringAsync();

            return JsonUtility.FromJson<ChessMoveDTO>(serialized);
        }
    }
}
