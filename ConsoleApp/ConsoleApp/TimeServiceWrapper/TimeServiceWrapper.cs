using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Logger;
using ExternalService.Time;
using ExternalServiceInterface.Time;

namespace ConsoleApp.TimeWrapper
{
    public class TimeServiceWrapper : ITimeService
    {
        private readonly TimeService service;
        private readonly ILogger logger;

        public TimeServiceWrapper(ILogger logger)
        {
            this.service = new TimeService();
            this.logger = logger;
        }

        public DateTime GetDateTime()
        {
            try
            {
                return this.service.GetDateTime();
            }
            catch(Exception ex)
            {
                this.logger.WriteLogWarning("En el metodo GetDateTime", ex.Message == null ? String.Empty : ex.Message);
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
            catch(Exception ex)
            {
                this.logger.WriteLogWarning("En el metodo GetLag", ex.Message == null ? String.Empty : ex.Message);
                return this.GetLag(sats);
            }
        }

        public string GetTime(System.Globalization.CultureInfo culture)
        {
            CultureInfo tempCulture;

            if (culture.Name.Contains("en"))
            {
                this.logger.WriteLogInfo("Switch culture to es-ES");
                tempCulture = new CultureInfo("es-ES");
            }
            else
            {
                tempCulture = culture;
            }

            string stringDate = this.service.GetTime(tempCulture);

            DateTime date = DateTime.Parse(stringDate, tempCulture);
           
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
