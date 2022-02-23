using MarketStockAnalyzer.API.Data;
using MarketStockAnalyzer.API.Data.DTO;
using MarketStockAnalyzer.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace MarketStockAnalyzer.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TickerController : ControllerBase
    {
        private readonly ITickerFactory _tickerFactory;
        private readonly ITickRepository _tickRepository;
        private readonly ISheetDataService _sheetDataService;

        public TickerController (ITickerFactory tickerFactory, ITickRepository tickRepository, ISheetDataService sheetDataService)
        {
            _tickerFactory = tickerFactory;
            _tickRepository = tickRepository;
            _sheetDataService = sheetDataService;
        }

        [HttpPost]
        public async Task<List<Tick>> CreateTicker(PickedDatesDTO dto)
        {
            var ticks = await _tickRepository.Get(dto.startDate, dto.endDate);
            _sheetDataService.CreateCredentials();
            string errorMessage = "";
            try
            {
                if (!DateHelper.CompareDates(ticks.Last().Date, dto.endDate) && !DateHelper.CompareDates(ticks.First().Date, dto.startDate))
                    ticks = await CallForMissingEntries(ticks, dto.startDate, dto.endDate);
            }
            catch (Exception e)
            {
                ticks = await CallForMissingEntries(ticks, dto.startDate, dto.endDate);
                if (ticks.Count == 0)
                    errorMessage = "No records found";
                else if (!DateHelper.CompareDates(ticks.Last().Date, dto.endDate) && !DateHelper.CompareDates(ticks.First().Date, dto.startDate))
                    errorMessage = "Couldn't find all records from this range";
            }
            return ticks;
        }

        private async Task<List<Tick>> CallForMissingEntries(List<Tick> ticks, DateTime startDate, DateTime endDate)
        {
            var entries = _sheetDataService.ReadEntries();
            var filteredEntries = _sheetDataService.FilterEntriesByDates(entries, startDate, endDate);
            if (filteredEntries.Count > 0)
                await _tickRepository.Add(entries.Except(ticks).ToList());
            return filteredEntries;
        }
    }
}
