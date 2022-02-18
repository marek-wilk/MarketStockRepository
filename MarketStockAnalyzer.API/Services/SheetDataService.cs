using System.IO;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using System.Linq;
using MarketStockAnalyzer.API.Data;
using Google.Apis.Sheets.v4.Data;

namespace MarketStockAnalyzer.API.Services
{
    public class SheetDataService : ISheetDataService
    {
        private readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        private readonly string ApplicationName = "Stock Market";
        private readonly string SpreadsheetId = "1dmzzxs4-pmaDhnm0Oqpdogc9uIl04LQuujj-SGma_-g";
        private readonly string sheet = "Google-Stock";
        private SheetsService service;

        public void CreateCredentials()
        {
            GoogleCredential credential;
            using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream)
                    .CreateScoped(Scopes);
            }

            // Create Google Sheets API service.
            service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }

        public List<Tick> ReadEntries()
        {
            var entries = new List<Tick>();
            var range = $"{sheet}!A1:B100000";
            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(SpreadsheetId, range);

            var response = request.Execute();
            IList<IList<object>> values = response.Values;
            if (values != null && values.Count > 0)
            {
                foreach (var row in values)
                {
                    if (row.Count == 0) break;
                    if (!DateTime.TryParse(row[0].ToString(), out DateTime result)) continue;
                    entries.Add(new Tick
                    {
                        Date = result,
                        Price = double.Parse(row[1].ToString())
                    });
                }
            }
            else
            {
                Console.WriteLine("No data found.");
            }

            return entries;
        }

        public List<Tick> FilterEntriesByDates(List<Tick> entries, DateTime startDate, DateTime endDate)
        {
            return entries
                .Where(e => DateTime.Parse(e.Date.ToString()) > startDate && 
                    DateTime.Parse(e.Date.ToString()) < endDate)
                .ToList();
        }
    }
}
