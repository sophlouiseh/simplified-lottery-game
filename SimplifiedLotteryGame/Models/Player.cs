namespace SimplifiedLotteryGame.Models
{
    public class Player
    {
        public int PlayerNumber { get; set; }

        public int NumberOfTickets { get; set; }

        public decimal Balance { get; set; }

        public List<Ticket> Tickets { get; set; }
    }
}