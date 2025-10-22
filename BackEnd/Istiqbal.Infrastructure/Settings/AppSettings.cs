using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Infrastructure.Settings
{
    public class AppSettings
    {
        public int RoomStatusCheckFrequencyMinutes { get; set; }
        public int RoomCleaningDurationMinutes { get; set; }
        public int RoomMaintenanceFrequencyDays { get; set; }
        public int RoomMaintenanceDurationHours { get; set; }
    }
}
