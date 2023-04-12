namespace Lmbtfy.Blazor.Services
{
    public class KeywordService : IKeyworkService
    {
        private readonly string[] _keywordList =
            { "nature", "city", "skyline", "island", "food", "airplanes", "technology" };

        public string GetDailyKeyword()
        {
            var weekDay = (int)DateTime.UtcNow.DayOfWeek;

            return _keywordList[weekDay];
        }
    }
}
