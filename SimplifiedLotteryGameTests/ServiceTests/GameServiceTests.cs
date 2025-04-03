using Moq;
using SimplifiedLotteryGame.Configuration;
using SimplifiedLotteryGame.Interfaces;
using SimplifiedLotteryGame.Models;
using SimplifiedLotteryGame.Services;

namespace SimplifiedLotteryGameTests.ServiceTests {

    public class GameServiceTests
    {
        [Fact]
        public void RunGame_GetsPlayersAndRunsLottery()
        {
            var playerService = new Mock<IPlayerService>();
            var lotteryService = new Mock<ILotteryService>();
            var consoleService = new Mock<IConsoleService>();

            int userInput = 4;

            consoleService.Setup(c => c.GetRequestedNumberOfTickets())
                .Returns(userInput);

            playerService.Setup(p => p.GetPlayers(userInput)).Returns(new Dictionary<int, Player>{});

            var gameService = new GameService(lotteryService.Object, playerService.Object, consoleService.Object);

            gameService.RunGame();

            consoleService.Verify(c => c.WriteWelcomeMessage(), Times.Once);
            playerService.Verify(c => c.GetPlayers(userInput), Times.Once);
            consoleService.Verify(c => c.WritePlayersMessage(It.IsAny<Dictionary<int, Player>>()), Times.Once);
            lotteryService.Verify(c => c.RunLottery(It.IsAny<List<Ticket>>()), Times.Once);
            consoleService.Verify(c => c.WriteWinnersMessage(It.IsAny<LotteryResult>()), Times.Once);
        }
    }
}