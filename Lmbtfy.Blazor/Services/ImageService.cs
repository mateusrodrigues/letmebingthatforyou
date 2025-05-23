namespace Lmbtfy.Blazor.Services
{
    public class ImageService : IImageService
    {
        public string GetSeed()
        {
            return $"{DateTime.Now:yyyyMMdd}";
        }
    }
}
