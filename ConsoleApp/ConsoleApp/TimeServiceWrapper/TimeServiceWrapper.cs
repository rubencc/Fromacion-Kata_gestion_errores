using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExternalService.Time;
using ExternalServiceInterface.Time;

namespace ConsoleApp.TimeWrapper
{
    public class TimeServiceWrapper : ITimeService
    {
        private readonly TimeService service;

        public TimeServiceWrapper()
        {
            this.service = new TimeService();
        }

        public DateTime GetDateTime()
        {
            try
            {
                return this.service.GetDateTime();
            }
            catch
            {
                DateTime date = new DateTime(this.service.GetTime());
                return date;
            }
        }

        public float[] GetLag(string[] sats)
        {
            try
            {
                return this.service.GetLag(sats);
            }
            catch
            {
                return this.GetLag(sats);
            }
        }

        public string GetTime(System.Globalization.CultureInfo culture)
        {
            CultureInfo tempCulture;

            if (culture.Name.Contains("en"))
            {
                tempCulture = new CultureInfo("es-ES");
            }
            else
            {
                tempCulture = culture;
            }

            string stringDate = this.service.GetTime(tempCulture);

            DateTime date = DateTime.Parse(stringDate, tempCulture);

            //DateTime newDate = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Millisecond);

            return date.ToString(culture);
        }

        public long GetTime()
        {
            return this.service.GetTime();
        }

        public bool ServiceAvailable()
        {
            return this.service.ServiceAvailable();
        }
    }
}
