using SimplifiedLotteryGame.Configuration;
using SimplifiedLotteryGame.Models;

namespace SimplifiedLotteryGame.Interfaces
{
    public interface ILotteryService{
        LotteryResult RunLottery(List<Ticket> tickets);
    }
}