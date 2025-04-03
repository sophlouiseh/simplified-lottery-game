namespace SimplifiedLotteryGame.Configuration
{
    public class AppSettings
    {
        public CurrencySettings Currency { get; set; }
        public TicketSettings Ticket { get; set; }
        public PlayerSettings Player { get; set; }
        public GameSettings Game { get; set; }
    }
}