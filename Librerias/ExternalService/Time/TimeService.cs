using ExternalService.Time;
using ExternalServiceInterface.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalService.Time
{
    public class TimeService : ITimeService
    {
        private static int count;

        public TimeService()
        {
            count = 8;
        }

        public bool ServiceAvailable()
        {
            Random rd = new Random();

            double value = rd.NextDouble();

            if(value > 0.99)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public long GetTime()
        {
            return DateTime.Now.Ticks;
        }

        public string GetTime(System.Globalization.CultureInfo culture)
        {
            return this.GetTimeAsString(culture);
        }

        public DateTime GetDateTime()
        {
            this.LaunchRandomException();
            return DateTime.Now;
        }

        private string GetTimeAsString(System.Globalization.CultureInfo culture)
        {
            if(culture.Name.Contains("en"))
            {
                throw new InvalidTimeZoneException();
            }

            return DateTime.Now.ToUniversalTime().ToString(culture);
        }

        private void LaunchRandomException()
        {

            int cond = count % 10;
            count++;
            switch (cond)
            {
                case 0:
                    throw new ArithmeticException();
                case 1:
                    throw new FormatException();
                case 2:
                    throw new InsufficientMemoryException();
                case 3:
                    throw new DivideByZeroException();
                case 4: 
                    throw new ArgumentNullException();
            }
        }


        public float[] GetLag(string[] sats)
        {
            if (sats.Contains("hispasat"))
                throw new UnauthorizedAccessException("No tiene autorizacion para acceder a los sistemas del satelite hispasat");

            if (sats.Length < 3)
                throw new InsuficientConstelationMembersException();

            int count = sats.Length;
            Random rd = new Random();
            float[] lag = new float[sats.Length];

            for (int i = 0; i < count; i++)
            {
                if (rd.NextDouble() < 1)
                    throw new InsufficientMemoryException();

                lag[i] = (float)rd.NextDouble() * 10;
            }

            return lag;
        }
    }
}
