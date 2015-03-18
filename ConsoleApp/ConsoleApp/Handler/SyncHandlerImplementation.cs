using System.Threading;
using ExternalServiceInterface.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Handler
{
    public class SyncHandlerImplementation : SyncApp.Handler.SyncAbstract
    {
        private string[] newSats;

        public SyncHandlerImplementation(ITimeService service)
            : base(service)
        {
            int count = this.sats.Length;
            this.newSats = new string[count - 1];
            int j = 0;
            for (int i = 0; i < count; i++)
            {
                if (!this.sats[i].Contains("hispasat"))
                {
                    newSats[j] = this.sats[i];
                    j++;
                }
            }
        }

        public override DateTime GetDateTimeObject()
        {
            return this.timeService.GetDateTime();
        }

        public override float GetLag()
        {
            return this.timeService.GetLag(newSats)[0];
        }
    }
}
