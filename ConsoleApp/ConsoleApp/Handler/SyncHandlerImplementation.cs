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
        public SyncHandlerImplementation(ITimeService service)
            : base(service)
        { }

        public override DateTime GetDateTimeObject()
        {
            return this.timeService.GetDateTime();
        }

        public string GetTimeForESA()
        {
            return string.Empty;
        }
    }
}
