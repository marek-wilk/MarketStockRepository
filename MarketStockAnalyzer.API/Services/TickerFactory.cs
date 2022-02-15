using MarketStockAnalyzer.API.Data;

namespace MarketStockAnalyzer.API.Services
{
    public class TickerFactory : ITickerFactory
    {

        public Ticker Create(List<Tick> Ticks, string errorMessage)
        {
            var Ticker = new Ticker();
            Ticker.Ticks = Ticks;
            Ticker.ErrorMessage = errorMessage; 

            return Ticker;
        }
    }
}
