namespace Frixxer.PresenterConsoleApp.Services.Scrolls
{
    public class CnnScrollApiProvider : IScrollApiProvider
    {
        public string GetScrollText()
        {
            return "This is ticker news provided from CNN";
        }
    }
}
