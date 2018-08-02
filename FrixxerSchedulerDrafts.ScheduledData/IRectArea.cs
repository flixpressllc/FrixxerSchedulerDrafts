using System;
using System.Collections.Generic;
using System.Text;

namespace FrixxerSchedulerDrafts.ScheduledData
{
    public interface IRectArea
    {
        /// <summary>
        /// This is NOT the unique id of the rect area among its siblings in the very content data. It is a reference
        /// to the rect area descriptor that allows the creation of the specific type of rect area to begin with.
        /// </summary>
        int Id { get; set; }
    }
}
