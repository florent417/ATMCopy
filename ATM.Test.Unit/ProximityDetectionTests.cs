using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Data;
using ATM.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace ATM.Test.Unit
{
    [TestFixture]
    public class ProximityDetectionTests
    {
        private IProximityDetection _uut;
        private List<ITrackData> _trackDataList;

        private List<IProximityDetectionData> _proximityDetections;
        //private ITrackData _track1;
        //private ITrackData _track2;
        private ITrackData _track1, _track2, _track3, _track4;

        private IEventRendition _eventRendition;
        private IProximityDetectionData _proximityDetectionData;

        //Horizontal seperation less than 5000 meters
        //vertical seperation less than 300 meters

        [SetUp]
        public void SetUp()
        {
            _trackDataList = new List<ITrackData>();
            _proximityDetectionData = new ProximityDetectionData();
            _eventRendition = Substitute.For<IEventRendition>();
            _proximityDetections = Substitute.For<List<IProximityDetectionData>>();
            _uut = new ProximityDetection(_eventRendition, _proximityDetectionData);
           
            _track1 = new TrackData
            { 
                Tag = "ABC123",
                X = 10050,
                Y = 10050,
                Altitude = 10050,
            };

            _track2 = new TrackData
            {
                Tag = "123ABC",
                X = 10000,
                Y = 10000,
                Altitude = 10200,
            };
        }

        [Test]
        public void ValidTracks_CorrectTagPrinted()
        { 
            _trackDataList.Add(_track1);
            _trackDataList.Add(_track2);
            _uut.CheckProximityDetection(_trackDataList);
            _eventRendition.Received()
                .PrintEvent(Arg.Is<List<IProximityDetectionData>>(data => data[0].Tag1 == "123ABC" && data[0].Tag2== "ABC123"));
        }

        [Test]
        public void CheckProcximityDetection_SeperationValid_IsCorrect()
        {
            _trackDataList.Add(_track1);
            _trackDataList.Add(_track2);
            _uut.CheckProximityDetection(_trackDataList);
            
           _eventRendition.Received().LogToFile(Arg.Is<List<IProximityDetectionData>>(data => data[0].Tag1=="123ABC" && data[0].Tag2 == "ABC123"));

        }

        //[Test]
        //public void CheckProximityDetection_CollisionCountIsOne()
        //{


        //    _track1.Tag = "ART123";
        //    _track2.Tag = "THF334";
        //    _track1.X = 30000;
        //    _track2.X = 30050;
        //    _track1.Y = 50000;
        //    _track2.Y = 50050;
        //    _track1.Altitude = 5000;
        //    _track2.Altitude = 5050;

        //    _trackDataList.Add(_track1);
        //    _trackDataList.Add(_track2);


        //    _uut.CheckProximityDetection(_trackDataList);

        //    Assert.That(_proximityDetections.Count, Is.EqualTo(1));

        //}

        //[Test]
        //public void CheckProximityDetection_CollisionCountIsOne_withThreeTracks()
        //{

        //    _track1.Tag = "ART123";
        //    _track2.Tag = "THF334";
        //    _track1.X = 30000;
        //    _track2.X = 30050;
        //    _track1.Y = 50000;
        //    _track2.Y = 50050;
        //    _track1.Altitude = 5000;
        //    _track2.Altitude = 5050;
        //    _track3.X = 50000;
        //    _track3.Y = 70000;
        //    _track3.Altitude = 3000;

        //    _trackDataList.Add(_track1);
        //    _trackDataList.Add(_track2);
        //    _trackDataList.Add(_track3);

      
        //    _uut.CheckProximityDetection(_trackDataList);

        //    Assert.That(_proximityDetections.Count, Is.EqualTo(1));

        //}

    }
}
