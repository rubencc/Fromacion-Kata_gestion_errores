using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncApp.Handler
{
    public interface ISync
    {
        bool SyncWithExternalClock();
        string GetTimeForРоскосмос();
        string GetTimeForJAXA();
        string GetTimeForNASA();
        string GetTimeForESA();
        decimal WeeksUntilFinishMission();
        float GetLag();
    }
}
