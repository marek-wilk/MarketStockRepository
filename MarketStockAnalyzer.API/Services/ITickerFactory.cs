using MarketStockAnalyzer.API.Data;

namespace MarketStockAnalyzer.API.Services
{
    public interface ITickerFactory
    {
        Ticker Create(List<Tick> Ticks, string errorMessage);
    }
}
