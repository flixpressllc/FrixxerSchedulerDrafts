using FCore.Foundations;
using FrixxerSchedulerDrafts.ScheduledData;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace Frixxer
{
    public class PresentationViewModel<TPresentation> : IViewModel<TPresentation, long>
        where TPresentation : class, IPresentation
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ScheduledBlockData ScheduledBlockData { get; set; }

        public PresentationViewModel()
        {        
        }

        public PresentationViewModel(TPresentation presentation)
        {
            SetValues(presentation);
        }

        public void SetValues(TPresentation presentation)
        {
            Id = presentation.Id;
            Name = presentation.Name;
            ScheduledBlockData = JsonConvert.DeserializeObject<ScheduledBlockData>(presentation.Data, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DefaultValueHandling = DefaultValueHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        public void UpdateValues(TPresentation presentation)
        {
            throw new NotSupportedException();
        }
    }
}
