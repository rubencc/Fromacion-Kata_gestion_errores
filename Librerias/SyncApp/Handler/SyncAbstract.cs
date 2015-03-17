using ExternalServiceInterface.Time;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncApp.Handler
{
    public abstract class SyncAbstract : ISync
    {
        protected readonly ITimeService timeService;
        private readonly string[] cultures;
        protected string[] sats;
        private readonly DateTime finishMissionDate;
        public long LastSyncTime { get; set; }

        public SyncAbstract(ITimeService timeService)
        {
            this.timeService = timeService;
            this.cultures = new string[] { "ru-RU", "ja-JP", "en-US", "en-GB" };
            this.sats = new string[] { "XRT1033", "hispasat", "pegaso", "astra" };
            this.finishMissionDate = new DateTime(2066, 2, 28);
        }

        public bool SyncWithExternalClock()
        {
            bool syncronized = false;
            syncronized = this.Sync();
            return syncronized;
        }

        public string GetTimeForРоскосмос()
        {
            CultureInfo culture = new CultureInfo(this.cultures[0]);
            return this.timeService.GetTime(culture) + " +3h, +12h";
        }

        public string GetTimeForJAXA()
        {
            CultureInfo culture = new CultureInfo(this.cultures[1]);
            return this.timeService.GetTime(culture) + " +9h";
        }

        public string GetTimeForNASA()
        {
            CultureInfo culture = new CultureInfo(this.cultures[2]);
            return this.timeService.GetTime(culture) + " -10h, -5h";
        }

        public string GetTimeForESA()
        {
            CultureInfo culture = new CultureInfo(this.cultures[3]);
            return this.timeService.GetTime(culture) + " 0h, +3h";
        }

        public decimal WeeksUntilFinishMission()
        {
            DateTime date = this.GetDateTimeObject();

            return new TimeSpan(this.finishMissionDate.Ticks - date.Ticks).Days / 7;
        }

        private bool Sync()
        {
            bool result = true;
            long time = 0;
            for (int i = 0; i < 10; i++)
            {
                if (!this.timeService.ServiceAvailable())
                {
                    result = false;
                }
                else
                {
                    time = this.timeService.GetTime();
                }
            }

            if (result)
            {
                this.LastSyncTime = time;
            }

            return result;
        }

        public abstract DateTime GetDateTimeObject();
        public abstract float GetLag();
    }
}
