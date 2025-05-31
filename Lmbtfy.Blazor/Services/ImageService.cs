namespace Lmbtfy.Blazor.Services
{
    public class ImageService : IImageService
    {
        public int GetIndex()
        {
            return (int)DateTime.Today.DayOfWeek + 1;
        }
    }
}
