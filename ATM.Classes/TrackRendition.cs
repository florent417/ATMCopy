using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Data;
using ATM.Interfaces;

namespace ATM
{
    public class TrackRendition : ITrackRendition
    {
        public TrackRendition()
        {
        }

        public void Print(List<ITrackData> trackDataList)
        {
            foreach (var track in trackDataList)
            {
                System.Console.WriteLine(track);
            }
        }
    }
}
