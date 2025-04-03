namespace SimplifiedLotteryGame.Configuration
{
    public class GameSettings
    {
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public List<PrizeSettings> Prizes { get; set; }
    }
}