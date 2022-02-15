namespace MarketStockAnalyzer.API.Services
{
    public static class DateHelper
    {
        public static bool CompareDates(DateTime comparedDate, DateTime desiredDate)
        {
            if (!CompareDays(comparedDate.Day, desiredDate.Day)) return false;
            if (!CompareMonths(comparedDate.Month, desiredDate.Month)) return false;
            if (!CompareYears(comparedDate.Year, desiredDate.Year)) return false;
            return true;
        }

        private static bool CompareDays(int comparedDays, int desiredDays)
        {
            return comparedDays == desiredDays;
        }
        private static bool CompareMonths(int comparedMonths, int desiredMonths)
        {
            return comparedMonths == desiredMonths;
        }
        private static bool CompareYears(int comparedYears, int desiredYears)
        {
            return comparedYears == desiredYears;
        }
    }
}
