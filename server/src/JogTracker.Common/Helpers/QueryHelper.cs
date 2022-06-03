namespace JogTracker.Common.Helpers
{
    public static class QueryHelper
    {
        public static bool IsMatch(string searchText, string source)
        {
            return source.ToLower().Contains(searchText.ToLower());
        }

        public static bool IsInteger(double number)
        {
            return number % 1 == 0;
        }
    }
}
