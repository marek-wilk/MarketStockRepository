namespace MarketStockAnalyzer.API.Data
{
    public class Ticker
    {
        public List<Tick> Ticks { get; set; }
        public string ErrorMessage { get; set; }
    }
}
