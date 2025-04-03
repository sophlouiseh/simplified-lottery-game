using Microsoft.Extensions.Options;
using SimplifiedLotteryGame.Configuration;
using SimplifiedLotteryGame.Interfaces;
using SimplifiedLotteryGame.Models;

namespace SimplifiedLotteryGame.Services
{
    public class LotteryService : ILotteryService
    {
        private readonly AppSettings _appSettings;

        public LotteryService(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }


        public LotteryResult RunLottery(List<Ticket> tickets)
        {
            var rand = new Random();

            int totalNoTickets = tickets.Count();
            decimal totalRevenue = totalNoTickets * _appSettings.Ticket.Price;
            decimal housePrize = totalRevenue;

            List<PrizeResult> prizeResults = [];

            //Pick Winners
            foreach (var prize in _appSettings.Game.Prizes)
            {
                if(prize.PercentOfWinners == 0){ //Assume that if no percentage of winners set it's the Grand Prize
                    decimal winnings = Utilities.Percentage(prize.WinningsPercent, totalRevenue);

                    int index = rand.Next(totalNoTickets);
                    var winner = tickets[index];
                    tickets.RemoveAt(index);

                    prizeResults.Add(new PrizeResult{ Name = prize.Name, Winners = [winner.PlayerNumber], Winnings = winnings });

                    housePrize -= winnings;
                } else {
                    int noOfWinners = Utilities.Percentage(prize.PercentOfWinners, totalNoTickets);
                    decimal winnings = Utilities.Percentage(prize.WinningsPercent, totalRevenue);
                    decimal winningsEach = Utilities.SplitWinnings(winnings, noOfWinners);

                    List<int> winners = [];
                    for (int i = 0; i < noOfWinners; i++)
                    {
                        int index = rand.Next(tickets.Count());
                        winners.Add(tickets[index].PlayerNumber);
                        tickets.RemoveAt(index);
                    }

                    prizeResults.Add(new PrizeResult{ Name = prize.Name, Winners = winners, Winnings = winningsEach });

                    housePrize -= winningsEach*noOfWinners;
                }
            }

            return new LotteryResult
            {
                HousePrize = housePrize,
                PrizeResults = prizeResults
            };
        }
    }
}