using Google.Apis.Sheets.v4;
using MarketStockAnalyzer.API.Data;

namespace MarketStockAnalyzer.API.Services
{
    public interface ISheetDataService
    {
        void CreateCredentials();
        List<Tick> ReadEntries();
        List<Tick> FilterEntriesByDates(List<Tick> entries, DateTime startDate, DateTime endDate);
    }
}
