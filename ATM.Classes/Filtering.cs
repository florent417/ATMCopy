using System.Collections.Generic;
using ATM.Data;
using ATM.Interfaces;

namespace ATM
{
    public class Filtering : Interfaces.IFiltering
    {
        private readonly ITrackUpdate _trackUpdate;
        public int _minXCoordinate { get; set; }
        public int _maxXCoordinate { get; set; }
        public int _minYCoordinate { get; set; }
        public int _maxYCoordinate { get; set; }
        public int _minAltitude { get; set; }
        public int _maxAltitude { get; set; }
        
        public Filtering(ITrackUpdate trackUpdate)
        {
            _minXCoordinate = 10000;
            _maxXCoordinate = 90000;
            _minYCoordinate = 10000;
            _maxYCoordinate = 90000;
            _minAltitude = 500;
            _maxAltitude = 20000;
            _trackUpdate = trackUpdate;
        }

        //Kan tilføjes, hvis det overvågede område skal ændres i størrelsen. 
        //public Filtering(int minXCoordinate, int maxXCoordinate, int minYCoordinate, 
        //    int maxYCoordinate, int minAltitude, int maxAltitude, ITrackUpdate trackUpdate)
        //{
        //    _minXCoordinate = minXCoordinate;
        //    _maxXCoordinate = maxXCoordinate;
        //    _minYCoordinate = minYCoordinate;
        //    _maxYCoordinate = maxYCoordinate;
        //    _minAltitude = minAltitude;
        //    _maxAltitude = maxAltitude;
        //    _trackUpdate = trackUpdate;
        //}

        public void ValidateTracks(List<ITrackData> trackData)
        {
            List<ITrackData> newTracks = new List<ITrackData>();

            foreach (var track in trackData)
            {
                if (track.X >= _minXCoordinate && track.X <= _maxXCoordinate
                                               && track.Y >= _minYCoordinate && track.Y <= _maxYCoordinate
                                               && track.Altitude >= _minAltitude && track.Altitude <= _maxAltitude)
                {
                    newTracks.Add(track);
                }
            }
            
            _trackUpdate.Update(newTracks);
        }
    }
}