using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Data;

namespace ATM.Interfaces
{
    public interface IProximityDetection
    {
        void CheckProximityDetection(List<ITrackData> trackDataList);
        //bool IsTracksInConflict(TrackData track1, TrackData track2); 

    }
}
