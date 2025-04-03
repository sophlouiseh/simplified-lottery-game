using SimplifiedLotteryGame.Configuration;
using SimplifiedLotteryGame.Models;

namespace SimplifiedLotteryGame.Interfaces
{
    public interface IPlayerService{
        Dictionary<int, Player> GetPlayers(int playerOneNoOfTickets);
    }
}