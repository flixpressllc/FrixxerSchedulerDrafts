namespace Frixxer.PresenterConsoleApp.Services.Scrolls
{
    public interface IScrollApiProviderFactory
    {
        IScrollApiProvider CreateScrrollApiProviderInstance(string scrollApiType);
    }
}
