using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ATM.Data;
using ATM.Interfaces;

namespace ATM
{
   public class TrackUpdate : ITrackUpdate
    {
        public List<ITrackData> oldList { get; set; }
        private ITrackRendition _trackRendition;
        private IProximityDetection _proximityDetection;
       // private double x1 { get; set; }
        //private double x2 { get; set; }
        //private double y1 { get; set; }
        //private double y2 { get; set; }
        public double timespan { get; set; }
        public TrackUpdate(ITrackRendition trackRendition, IProximityDetection proximityDetection)
        {
            _trackRendition = trackRendition;
            _proximityDetection = proximityDetection;
            oldList = new List<ITrackData>();

        }
        //public List<IFiltering> newList { get; }
        //private readonly ITrackRendition _trackRendition;
        //private readonly IProximityDetection _proximityDetection;

        //public TrackUpdate()
        //{
        //    oldList= new List<ITrackData>();
        //}

        public void Update(List<ITrackData> newList)
        {
            foreach (var newTrack in newList)
            {
                if (!oldList.Any())
                    break;

                foreach (var oldTrack in oldList)
                {
                    if (newTrack.Tag == oldTrack.Tag)
                    {
                        newTrack.Velocity = CalVelocity(newTrack, oldTrack);
                        newTrack.Course = CalCourse(newTrack, oldTrack);
                    }


                }
            }

            oldList.Clear();

            foreach (var trackData in newList)
            {
                oldList.Add(trackData);
            }
            
      
            
            _trackRendition.Print(newList);
            _proximityDetection.CheckProximityDetection(newList);
            
        }

        public int CalVelocity(ITrackData track1, ITrackData track2)
        {

            // calculate velocity


            //Coordinates 
            //int x1 = track1.X;
            //int x2 = track2.X;
            //int y1 = track1.Y;
            //int y2 = track2.Y;

            ////Distance between the 2 tracks
            //double distance = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
            //if (distance<0)
            //{
            //    distance = distance * (-1);
            //}

            // //double time = track2.TimeStamp.Subtract(track1.TimeStamp).TotalSeconds;



            //TimeSpan time = track2.TimeStamp - track1.TimeStamp;
            //timespan = (time.TotalSeconds);
            TimeSpan time = track2.TimeStamp - track1.TimeStamp;
            timespan = (double)time.TotalSeconds;

            double deltaX = 0;
            double deltaY = 0;
            double velocity = 0;

            if (track1.X > track2.X)
            {
                deltaX = track1.X - track2.X;
            }
            else
            {
                deltaX = track2.X - track1.X;
            }

            if (track1.Y > track2.Y)
            {
                deltaY = track1.Y - track2.Y;
            }
            else
            {
                deltaY = track2.Y - track1.Y;
            }

            double distance = Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));

            if (timespan < 0)
            {
                timespan = timespan * -1;
                velocity = distance / timespan;
            }
            else if (timespan > 0)
            { velocity = distance / timespan; }

            return (int)velocity;

            //return (int)distance /(int)timespan;  //Updating speed
        }

        public int CalCourse(ITrackData track1, ITrackData track2)
        {
            double deltaX = track2.X - track1.X;
            double deltaY = track2.Y - track1.Y;

            double Degree = 0;

            if (deltaX == 0)
            {
                //if deltaY er større end 0
                // condition ? first_expression : second_expression;
                Degree = deltaY > 0 ? 0 : 180;
            }
            else
            {
                double radian = Math.Atan2(deltaY, deltaX);
                //Convert to degress 
                Degree = radian / Math.PI * 180;

                Degree = 90 - Degree;
                if (Degree < 0)
                {
                    Degree += 360;
                }
            }

            return (int)Degree;
        }



    }


}

