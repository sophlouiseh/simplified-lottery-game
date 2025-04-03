using SimplifiedLotteryGame.Configuration;
using SimplifiedLotteryGame.Interfaces;
using SimplifiedLotteryGame.Models;

namespace SimplifiedLotteryGame.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly AppSettings _appSettings;
        private readonly ITicketService _ticketService;

        public PlayerService(AppSettings appSettings, ITicketService ticketService)
        {
            _appSettings = appSettings;
            _ticketService = ticketService;
        }

        public Dictionary<int, Player> GetPlayers(int playerOneNoOfTickets)
        {
            Dictionary<int, Player> players = [];

            Random rand = new Random();
            int noOfPlayers = rand.Next(_appSettings.Game.MinPlayers - 1, _appSettings.Game.MaxPlayers);

            //Store number of tickets per player in dictionary
            if (playerOneNoOfTickets * _appSettings.Ticket.Price > _appSettings.Player.StartingBalance)
            {
                //Not enough balance to purchase requested tickets - default to amount can afford
                playerOneNoOfTickets = (int)Math.Floor(_appSettings.Player.StartingBalance / _appSettings.Ticket.Price);
            }
            List<Ticket> playerTickets = _ticketService.GetTickets(playerOneNoOfTickets, 1);
            players.Add(1, new Player { PlayerNumber = 1, NumberOfTickets = playerOneNoOfTickets, Balance = _appSettings.Player.StartingBalance, Tickets = playerTickets });
            for (int playerNumber = 2; playerNumber <= noOfPlayers + 1; playerNumber++)
            {
                int noOfTickets = rand.Next(_appSettings.Player.MinTickets, _appSettings.Player.MaxTickets + 1);
                if (noOfTickets * _appSettings.Ticket.Price > _appSettings.Player.StartingBalance)
                {
                    //Not enough balance to purchase requested tickets - default to amount can afford
                    noOfTickets = (int)Math.Floor(_appSettings.Player.StartingBalance / _appSettings.Ticket.Price);
                }
                List<Ticket> tickets = _ticketService.GetTickets(noOfTickets, playerNumber);
                players.Add(playerNumber, new Player { PlayerNumber = playerNumber, NumberOfTickets = noOfTickets, Balance = _appSettings.Player.StartingBalance, Tickets = tickets });
            }

            return players;
        }
    }
}