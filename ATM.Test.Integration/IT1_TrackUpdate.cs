using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ATM.Classes;
using ATM.Data;
using ATM.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NUnit.Framework;

namespace ATM.Test.Integration
{
    [TestFixture]
    class IT1_TrackUpdate
    {

        private List<ITrackData> _trackData;
        private IFiltering _filtering;
        private ITrackRendition _trackRendition;
        private IEventRendition _eventRendition;
        private IProximityDetection _proximityDetection;
        private IProximityDetectionData _proximityDetectionData;
        private ITrackUpdate _trackUpdate;
        private ITrackData _track1;
        private ITrackData _track2;
        private ITrackData _fakeTrackDataValid1;
        private ITrackData _fakeTrackDataValid2;
        private List<ITrackData> _fakeTrackDataList;

        [SetUp]
        public void SetUp()
        {
            _trackData = new List<ITrackData>();  //elementerne i listen skal subtitutes
            //_filtering = Substitute.For<IFiltering>();

            _trackRendition = Substitute.For<ITrackRendition>();
            _eventRendition = Substitute.For<IEventRendition>();
            _proximityDetectionData = Substitute.For<IProximityDetectionData>();
            _proximityDetection = new ProximityDetection(_eventRendition, _proximityDetectionData);
            //_proximityDetectionData = Substitute.For<IProximityDetectionData>();
            _trackUpdate = new TrackUpdate(_trackRendition, _proximityDetection);
            _track1 = Substitute.For<ITrackData>();
            _track2 = Substitute.For<ITrackData>();

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
                Tag = "J5S002",
                X = 50100,
                Y = 50100,
                Altitude = 12000,
                Course = 0,
                TimeStamp = new DateTime(2018, 05, 13, 10, 50, 35),
                Velocity = 0
            };



        }



        [Test]
        public void Update_SeperationEventTrue_SeperationDataPrinted()
        {
            _fakeTrackDataList.Add(_fakeTrackDataValid1);
            _fakeTrackDataList.Add(_fakeTrackDataValid2);
            _trackUpdate.Update(_fakeTrackDataList);
            
            _eventRendition.Received().PrintEvent(Arg.Is<List<IProximityDetectionData>>(data=>data[0].Tag1== "J5S002"));// && data[0].Tag2== _fakeTrackDataValid2.Tag));
        }

        [Test]
        public void Update_SeperationEventTrue_SeperationDataLoggedToFile()
        {
            _fakeTrackDataList.Add(_fakeTrackDataValid1);
            _fakeTrackDataList.Add(_fakeTrackDataValid2);
            _trackUpdate.Update(_fakeTrackDataList);

            _eventRendition.Received().LogToFile(Arg.Is<List<IProximityDetectionData>>(data=>data[0].Tag1=="J5S002"));
        }

     




    }
}
