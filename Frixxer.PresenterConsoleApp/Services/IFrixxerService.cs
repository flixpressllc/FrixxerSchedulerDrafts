using Frixxer.EntityFramework;
using System;
using System.Collections.Generic;

namespace Frixxer.PresenterConsoleApp.Services
{
    public interface IFrixxerService
    {
        List<PresentationViewModel<Presentation>> GetPresentations(DateTime startTime, int durationInSeconds);
    }
}
