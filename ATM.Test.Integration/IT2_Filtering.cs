using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Data;
using ATM.Interfaces;
using NUnit.Framework;
using NUnit.Framework.Internal;
using NSubstitute;

namespace ATM.Test.Integration
{
    [TestFixture]
    class IT2_Filtering
    {
        
        private ITrackRendition _trackRendition;
        private ITrackUpdate _trackUpdate;
        private IEventRendition _eventRendition;
        private IProximityDetectionData _proximityDetectionData;
        private IProximityDetection _proximityDetection;
        private IFiltering _filtering;

        private ITrackData _fakeTrackDataValid1;
        private ITrackData _fakeTrackDataValid2;
        private ITrackData _fakeTrackDataValid3;
        private ITrackData _fakeTrackDataValid4;
        private List<ITrackData> _fakeTrackDataList;
        
        [SetUp]
        public void SetUp()
        {
            _trackRendition = Substitute.For<ITrackRendition>();
            _proximityDetectionData = new ProximityDetectionData();
            _eventRendition = new EventRendition();
            _proximityDetection = new ProximityDetection(_eventRendition, _proximityDetectionData);
            _trackUpdate = new TrackUpdate(_trackRendition, _proximityDetection);
            _filtering = new Filtering(_trackUpdate);

            _fakeTrackDataList = new List<ITrackData>();
            
            _fakeTrackDataValid1 = new TrackData
            {
                Tag = "JAS002",
                X = 50000,
                Y = 50000,
                Altitude = 12000,
                Course = 0,
                TimeStamp = new DateTime(2018, 05, 13, 10, 50, 35),
                Velocity = 0
            };
            
            _fakeTrackDataValid2 = new TrackData
            {
                Tag = "JAS002",
                X = 50100,
                Y = 50100,
                Altitude = 12000,
                Course = 0,
                TimeStamp = new DateTime(2018, 05, 13, 10, 50, 36),
                Velocity = 0
            };

           _fakeTrackDataValid3 = new TrackData
            {
                Tag = "JAS002",
                X = 20000,
                Y = 20000,
                Altitude = 12000,
                Course = 0,
                TimeStamp = DateTime.MinValue,
                Velocity = 0
            };

            _fakeTrackDataValid4 = new TrackData
            {
                Tag = "JAS002",
                X = 10000,
                Y = 10000,
                Altitude = 12000,
                Course = 0,
                TimeStamp = DateTime.MinValue,
                Velocity = 0
            };
        }

        [Test]
        public void ValidateTracks_ValidTracks_PrintsCalculatedVelocity()
        {
            //Adds fake data to list
            _fakeTrackDataList.Add(_fakeTrackDataValid1);
            _filtering.ValidateTracks(_fakeTrackDataList);

            _fakeTrackDataList.Clear();
            _fakeTrackDataList.Add(_fakeTrackDataValid2);

            _filtering.ValidateTracks(_fakeTrackDataList);

           _trackRendition.Received().Print(Arg.Is<List<ITrackData>>(data => data[0].Tag == "JAS002" && data[0].Velocity == (int)141));
        }

        [Test]
        public void ValidateTrack_ValidTracks_PrintsCalculatedCourse()
        {
            _fakeTrackDataList.Clear();
            _fakeTrackDataList.Add(_fakeTrackDataValid4);
            _filtering.ValidateTracks(_fakeTrackDataList);

            _fakeTrackDataList.Clear();
            _fakeTrackDataList.Add(_fakeTrackDataValid3);

            _filtering.ValidateTracks(_fakeTrackDataList);

            _trackRendition.Received().Print(Arg.Is<List<ITrackData>>(data => data[0].Tag == "JAS002" && data[0].Course == (int)225));
        }
    }
}
