using MongoDB.Driver;

namespace MarketStockAnalyzer.API.Data
{
    public class TickRepository : ITickRepository
    {
        private readonly TickContext _context;

        public TickRepository(TickContext context)
        {
            _context = context;
        }

        public async Task Add(List<Tick> ticks)
        {
            await _context.Ticks.InsertManyAsync(ticks);
        }

        public async Task Add(Tick tick)
        {
            await _context.Ticks.InsertOneAsync(tick);
        }

        public async Task<List<Tick>> Get(DateTime startDate, DateTime endDate)
        {
            return await _context.Ticks.Find(t => t.Date > startDate && t.Date < endDate).ToListAsync();
        }
    }
}
