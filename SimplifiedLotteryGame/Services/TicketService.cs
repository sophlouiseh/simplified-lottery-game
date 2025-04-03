using SimplifiedLotteryGame.Interfaces;
using SimplifiedLotteryGame.Models;

namespace SimplifiedLotteryGame.Services
{
    public class TicketService : ITicketService
    {
        public TicketService()
        {
        }

        public List<Ticket> GetTickets(int noOfTickets, int playerNumber)
        {
            return Enumerable.Range(0, noOfTickets)
                    .Select(_ => new Ticket { PlayerNumber = playerNumber }).ToList();
        }
    }
}