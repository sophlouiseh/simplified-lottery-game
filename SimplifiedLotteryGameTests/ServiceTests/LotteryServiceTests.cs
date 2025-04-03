using Moq;
using SimplifiedLotteryGame.Configuration;
using SimplifiedLotteryGame.Interfaces;
using SimplifiedLotteryGame.Models;
using SimplifiedLotteryGame.Services;

namespace SimplifiedLotteryGameTests.ServiceTests {

    public class LotteryServiceTests
    {
        [Fact]
        public void RunLottery_WithPrizePercentOfWinnersGreaterThanZero_ShouldPickMultipleWinnersForPrize()
        {    
            var appSettings = new AppSettings
                {
                    Ticket = new TicketSettings { Price = 10.00m },
                    Game = new GameSettings
                    {
                        Prizes = new List<PrizeSettings>
                        {
                            new PrizeSettings { Name = "Second Tier", PercentOfWinners = 50, WinningsPercent = 10 }
                        }
                    }
                };   
            
            var lotteryService = new LotteryService(appSettings);
            var tickets = new List<Ticket>
            {
                new Ticket { PlayerNumber = 1 },
                new Ticket { PlayerNumber = 2 },
                new Ticket { PlayerNumber = 3 },
                new Ticket { PlayerNumber = 4 }
            };


            var result = lotteryService.RunLottery(tickets);


            Assert.Single(result.PrizeResults);
            Assert.Equal("Second Tier", result.PrizeResults[0].Name);
            Assert.Equal(2, result.PrizeResults[0].Winners.Count); // 2 winners
            Assert.Equal(2.00m, result.PrizeResults[0].Winnings); //10% of 40 % 2 winners
        }

        [Fact]
        public void RunLottery_ShouldCalculateHousePrizeCorrectly()
        {    
            var appSettings = new AppSettings
                {
                    Ticket = new TicketSettings { Price = 10.00m },
                    Game = new GameSettings
                    {
                        Prizes = new List<PrizeSettings>
                        {
                            new PrizeSettings { Name = "Grand Prize", PercentOfWinners = 0, WinningsPercent = 50 },
                            new PrizeSettings { Name = "Second Tier", PercentOfWinners = 50, WinningsPercent = 10 }

                        }
                    }
                };   
            
            var lotteryService = new LotteryService(appSettings);
            var tickets = new List<Ticket>
            {
                new Ticket { PlayerNumber = 1 },
                new Ticket { PlayerNumber = 2 },
                new Ticket { PlayerNumber = 3 },
                new Ticket { PlayerNumber = 4 }
            };


            var result = lotteryService.RunLottery(tickets);


            decimal expectedHousePrize = 16; // 40 - (50%: 20) - (10%: 4)
            Assert.Equal(expectedHousePrize, result.HousePrize);
        }

        [Fact]
        public void RunLottery_WithPrizePercentOfWinnersZero_ShouldPickSingleWinnerForPrize()
        {

            var appSettings = new AppSettings
                {
                    Ticket = new TicketSettings { Price = 10.00m },
                    Game = new GameSettings
                    {
                        Prizes = new List<PrizeSettings>
                        {
                            new PrizeSettings { Name = "Grand Prize", PercentOfWinners = 0, WinningsPercent = 50 }
                        }
                    }
                };

            var lotteryService = new LotteryService(appSettings);
            var tickets = new List<Ticket>
            {
                new Ticket { PlayerNumber = 1 },
                new Ticket { PlayerNumber = 2 }
            };

            var result = lotteryService.RunLottery(tickets);

            Assert.Single(result.PrizeResults);
            Assert.Equal("Grand Prize", result.PrizeResults[0].Name);
            Assert.Single(result.PrizeResults[0].Winners);
            Assert.Equal(10.00m, result.PrizeResults[0].Winnings); // 50% of 20
        }
    }
}