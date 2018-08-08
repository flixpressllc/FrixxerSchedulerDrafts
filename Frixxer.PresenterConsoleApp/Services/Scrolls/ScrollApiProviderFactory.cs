namespace Frixxer.PresenterConsoleApp.Services.Scrolls
{
    public class ScrollApiProviderFactory : IScrollApiProviderFactory
    {
        public IScrollApiProvider CreateScrrollApiProviderInstance(string scrollApiType)
        {
            if (scrollApiType == "CNN")
            {
                return new CnnScrollApiProvider();
            }
            else if (scrollApiType == "NPR")
            {
                return new NprScrollApiProvider();
            }
            else if (scrollApiType == "BBC")
            {
                return new BbcScrollApiProvider();
            }

            return null;
        }
    }
}
