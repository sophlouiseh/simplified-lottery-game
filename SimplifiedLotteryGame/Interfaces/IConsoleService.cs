using SimplifiedLotteryGame.Models;

namespace SimplifiedLotteryGame.Interfaces
{
    public interface IConsoleService{
        void WriteWelcomeMessage();

        void WritePlayersMessage(Dictionary<int, Player> players);

        void WriteWinnersMessage(LotteryResult result);

        int GetRequestedNumberOfTickets();
    }
}