using System.Collections.Generic;
using ATM.Data;

namespace ATM.Interfaces
{
    public interface IFiltering
    {
        int _minXCoordinate { get; set; }
        int _maxXCoordinate { get; set; }
        int _minYCoordinate { get; set; }
        int _maxYCoordinate { get; set; }
        int _minAltitude { get; set; }
        int _maxAltitude { get; set; }

        void ValidateTracks(List<ITrackData> trackData);
    }
}
