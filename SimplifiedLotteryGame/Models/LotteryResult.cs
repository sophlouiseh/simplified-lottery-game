public class LotteryResult {
    public decimal HousePrize { get; set; }

    public List<PrizeResult> PrizeResults { get; set; }
}


public class PrizeResult {
    public string Name { get; set; }

    public List<int> Winners { get; set; }

    public decimal Winnings { get; set; }
}