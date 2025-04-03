using Moq;
using SimplifiedLotteryGame.Configuration;
using SimplifiedLotteryGame.Interfaces;
using SimplifiedLotteryGame.Services;

namespace SimplifiedLotteryGameTests.ServiceTests {

    public class PlayerServiceTests
    {
        [Theory]
        [InlineData(1, 2)]
        [InlineData(9, 16)]
        [InlineData(3, 22)]
        public void GetPlayers_ReturnsPlayersWithTickets(int minPlayers, int maxPlayers)
        {
            var ticketService = new Mock<ITicketService>();

            var playerService = new PlayerService(GetMockAppSettings(minPlayers, maxPlayers, 10.00m), ticketService.Object);

            var results = playerService.GetPlayers(10);

            Assert.InRange(results.Count, minPlayers, maxPlayers);

            foreach (var item in results)
            {
                ticketService.Verify(t => t.GetTickets(item.Value.NumberOfTickets, item.Value.PlayerNumber), Times.Once);
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(9)]
        [InlineData(2)]
        public void GetPlayers_ReturnsRequestedNumberOfTicketsForPlayerOne(int noOfTickets)
        {
            var ticketService = new Mock<ITicketService>();

            var playerService = new PlayerService(GetMockAppSettings(10, 15, 10.00m), ticketService.Object);

            var results = playerService.GetPlayers(noOfTickets);

            var playerOne = results[1];

            Assert.Equal(noOfTickets, playerOne.NumberOfTickets);
            ticketService.Verify(t => t.GetTickets(noOfTickets, 1), Times.Once);
        }

        private AppSettings GetMockAppSettings(int minPlayers, int maxPlayers, decimal startingBalance){
            return new AppSettings {
                Game = new GameSettings {
                    MinPlayers = minPlayers,
                    MaxPlayers = maxPlayers
                },
                Player = new PlayerSettings {
                    StartingBalance = startingBalance,
                    MinTickets = 1,
                    MaxTickets = 10
                },
                Ticket = new TicketSettings {
                    Price = 1.00m
                }
            };
        }
    }
}