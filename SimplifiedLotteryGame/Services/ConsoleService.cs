using Microsoft.Extensions.Options;
using SimplifiedLotteryGame.Configuration;
using SimplifiedLotteryGame.Interfaces;
using SimplifiedLotteryGame.Models;

namespace SimplifiedLotteryGame.Services
{
    public class ConsoleService : IConsoleService
    {
        private readonly AppSettings _appSettings;

        public ConsoleService(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public void WriteWelcomeMessage()
        {
            Console.WriteLine($@"Welcome to the Bede Lottery, Player 1!

* Your digital balance: {Utilities.DisplayAsCurrency(_appSettings.Player.StartingBalance, _appSettings.Currency.Culture)}
* Ticket Price: {Utilities.DisplayAsCurrency(_appSettings.Ticket.Price, _appSettings.Currency.Culture)} each

How many tickets do you want to buy, Player 1?");
        }

        public void WritePlayersMessage(Dictionary<int, Player> players)
        {
            Console.WriteLine($@"
{players.Count() - 1} other CPU players have also purchased tickets:

* {string.Join("\n* ", players.Select(p => $"Player {p.Key}: {p.Value.NumberOfTickets} tickets"))}");
        }

        public void WriteWinnersMessage(LotteryResult result)
        {
            Console.WriteLine($@"
Ticket Draw Results:

* {string.Join("\n* ", result.PrizeResults.Select(t =>
            {
                return $"{(t.Winners.Count == 1 ? 
                $"{t.Name}: Player {t.Winners.First()} wins {Utilities.DisplayAsCurrency(t.Winnings, _appSettings.Currency.Culture)}!" 
                : $"{t.Name}: Players {string.Join(", ", t.Winners)} win {Utilities.DisplayAsCurrency(t.Winnings, _appSettings.Currency.Culture)} each!")}";
            }))}

Congratulations to the winners!

House Revenue: {Utilities.DisplayAsCurrency(result.HousePrize, _appSettings.Currency.Culture)}");
        }

        public int GetRequestedNumberOfTickets()
        {
            int? playerOneNoOfTickets = null;

            while (playerOneNoOfTickets is null)
            {
                
                string userInput = Console.ReadLine() ?? "";
                if (int.TryParse(userInput, out int parsedNumber))
                {
                    if (1 <= parsedNumber && parsedNumber <= 10)
                    {
                        playerOneNoOfTickets = parsedNumber;
                    }
                    else
                    {
                        Console.WriteLine($@"You can buy between 1 and 10 tickets.
                        How many tickets do you want to buy, Player 1?");
                    }
                }
                else
                {
                    Console.WriteLine($@"{userInput} is not a number.
                    How many tickets do you want to buy, Player 1?");
                }
            }

            return (int)playerOneNoOfTickets;
        }
    }
}