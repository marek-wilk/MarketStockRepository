namespace MarketStockAnalyzer.API.Data
{
    public interface ITickRepository
    {
        Task Add(Tick tick);
        Task Add(List<Tick> ticks);
        Task<List<Tick>> Get(DateTime startDate, DateTime endDate);
    }
}
