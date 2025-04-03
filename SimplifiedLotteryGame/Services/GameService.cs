using SimplifiedLotteryGame.Interfaces;
using SimplifiedLotteryGame.Models;

namespace SimplifiedLotteryGame.Services
{
    public class GameService : IGameService
    {
        private readonly IConsoleService _consoleService;
        private readonly ILotteryService _lotteryService;
        private readonly IPlayerService _playerService;

        public GameService(ILotteryService lotteryService, IPlayerService playerService, IConsoleService consoleService)
        {
            _lotteryService = lotteryService;
            _playerService = playerService;
            _consoleService = consoleService;
        }

        public void RunGame()
        {
            List<Ticket> tickets = [];

            _consoleService.WriteWelcomeMessage();

            int playerOneNoOfTickets = _consoleService.GetRequestedNumberOfTickets();

            var players = _playerService.GetPlayers((int)playerOneNoOfTickets);

            _consoleService.WritePlayersMessage(players);

            var result = _lotteryService.RunLottery(players.Values.SelectMany(x => x.Tickets).ToList());

            _consoleService.WriteWinnersMessage(result);
        }
    }
}