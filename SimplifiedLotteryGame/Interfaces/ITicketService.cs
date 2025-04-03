using SimplifiedLotteryGame.Models;

namespace SimplifiedLotteryGame.Interfaces
{
    public interface ITicketService {
        List<Ticket> GetTickets(int noOfTickets, int playerNumber);
    }
}