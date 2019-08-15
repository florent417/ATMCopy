using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ATM.Data;
using ATM.Interfaces;

namespace ATM
{
    public class ProximityDetection : IProximityDetection
    {
        private int HorizontalSeperation = 5000; //Horizontal seperation less than 5000 meters
        private int VerticalSeperation = 300; //vertical seperation less than 300 meters

        //private readonly IEventRendition _eventRendition;
        private IEventRendition _eventRendition;
        private IProximityDetectionData _proximityDetectionData;
        private List<IProximityDetectionData> _proximityDetectionDatas;




        public ProximityDetection(IEventRendition eventRendition, IProximityDetectionData proximityDetectionData)
        {
            //Need to call LogToFile
            _eventRendition = eventRendition;
            _proximityDetectionData = proximityDetectionData;
            _proximityDetectionDatas = new List<IProximityDetectionData>();
        }

        public void CheckProximityDetection(List<ITrackData> trackDataList)
        {
            //Runs through the tracks in the trackDataList to see if any tracks are in collision distance
            foreach (var track1 in trackDataList)
            {
                foreach (var track2 in trackDataList)
                {
                    //Track data
                    //double x1 = track1.X;
                    //double x2 = track2.X;
                    //double y1 = track1.Y;
                    //double y2 = track2.Y;
                    //double alt1 = track1.Altitude;
                    //double alt2 = track1.Altitude;


                    //Formula: sqrt((x1-x2)^2+(y1-y2)^2)
                    //var horizantalDistance = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
                    double horizantalDistance = Math.Sqrt(Math.Pow(track1.X - track2.X,2) + Math.Pow(track1.Y - track2.Y,2));

                    //vertical seperation less than 300 meters
                    //var veritalDistance = Math.Abs(alt1 - alt2);
                    double veritalDistance = Math.Abs(track2.Altitude - track1.Altitude);

                    // If the to two planes tag is different and is conflicting with the minimum seperation.
                    //veritalDistance <= VerticalSeperation && horizantalDistance <= 5000 && track1.Tag != track2.Tag
                    if (horizantalDistance <= HorizontalSeperation && veritalDistance <= VerticalSeperation && track1.Tag != track2.Tag)
                    {
                        //_proximityDetectionData(track1.Tag, track2.Tag, DateTime.Now);
                        
                        
                        _proximityDetectionData.Tag1 = track1.Tag;
                        _proximityDetectionData.Tag2 = track2.Tag;
                        _proximityDetectionData.Timestamp = DateTime.Now;


                        //Sending data to EventRendition class to be log and printed on console application
                        //_eventRendition.LogToFile(_proximityDetectionData);
                        //_eventRendition.PrintEvent(_proximityDetectionData);

                        _proximityDetectionDatas.Add(_proximityDetectionData);

                        _eventRendition.PrintEvent(_proximityDetectionDatas);
                        _eventRendition.LogToFile(_proximityDetectionDatas);


                        //the time og the event needs to be logged to files or application
                        //track1.TimeStamp = DateTime.Now;

                    }

                }
            }
        }
    }
}
