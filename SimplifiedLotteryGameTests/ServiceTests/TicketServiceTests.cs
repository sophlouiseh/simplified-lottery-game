using SimplifiedLotteryGame.Services;

namespace SimplifiedLotteryGameTests.ServiceTests {

    public class TicketServiceTests
    {
        [Theory]
        [InlineData(1, 2)]
        [InlineData(9, 16)]
        [InlineData(22, 1)]
        public void GetTickets_ReturnsTickets(int ticketCount, int playerNumber)
        {
            var ticketService = new TicketService();

            var results = ticketService.GetTickets(ticketCount, playerNumber);

            Assert.Equal(ticketCount, results.Count);

            Assert.All(results, result => Assert.Equal(playerNumber, result.PlayerNumber));
        }
    }
}