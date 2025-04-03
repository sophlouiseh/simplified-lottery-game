using System.Globalization;

public static class Utilities {
    public static string DisplayAsCurrency(decimal amount, string currencyCulture){
        return amount.ToString("C", new CultureInfo(currencyCulture));
    }

    public static int Percentage(decimal percentage, decimal total){
        return (int)Math.Round(percentage*total/100m, 0);
    }

    public static decimal PercentageDecimal(decimal percentage, decimal total){
        return Math.Round(percentage*total/100m, 2);
    }

    public static decimal SplitWinnings(decimal totalWinnings, int noOfWinners){
        return Math.Round(totalWinnings/noOfWinners, 2);
    }
}