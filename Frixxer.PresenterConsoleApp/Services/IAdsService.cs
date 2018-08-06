using FrixxerSchedulerDrafts.ScheduledData.Presented;
using System.Collections.Generic;

namespace Frixxer.PresenterConsoleApp.Services
{
    public interface IAdsService
    {
        List<AdPresentation> GetAds(List<string> tags);
    }
}
