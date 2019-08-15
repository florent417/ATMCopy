using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Data;
using ATM.Interfaces;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TransponderReceiver;

namespace ATM.Test.Integration
{
    [TestFixture]
    class IT3_Parsing
    {
        private ITrackUpdate _trackUpdate;
        private ITrackRendition _trackRendition;
        private IFiltering _filtering;
        private ITrackData _trackData;
        private IProximityDetection _proximityDetection;
        private IParsing _parsing;
        private IEventRendition _eventRendition;
        private ITransponderReceiver _transponderReceiver;
        private IProximityDetectionData _proximityDetectionData;
        private RawTransponderDataEventArgs _dataEvent;
          

        private ITrackData _fakeTrackData;
        private List<ITrackData> _faketrackList;





        [SetUp]
        public void Setup()
        {

            _transponderReceiver = Substitute.For<ITransponderReceiver>();
            _trackRendition = Substitute.For<ITrackRendition>();
            _proximityDetectionData = new ProximityDetectionData();
            _eventRendition = new EventRendition();
            _proximityDetection = new ProximityDetection(_eventRendition, _proximityDetectionData);
            _trackUpdate = new TrackUpdate(_trackRendition, _proximityDetection);
            _filtering = new Filtering(_trackUpdate);
            _parsing = new Parsing(_transponderReceiver, _filtering);
            _trackData = new TrackData();
            _faketrackList = new List<ITrackData>();
            //_trackRendition = new TrackRendition();

            _dataEvent = new RawTransponderDataEventArgs(new List<string>()
                { "JAS001;12345;67890;12000;20160101100909111" });

            //_fakeTrackData = new TrackData
            //{
            //    Tag = "JAS001", 
            //    X = 12345,
            //    Y = 67890, 
            //    Altitude = 12000, 
            //    Course = 0, 
            //    Velocity = 0, 
            //    TimeStamp = DateTime.ParseExact("20160101100909111", "yyyyMMddHHmmssfff", System.Globalization.CultureInfo.InvariantCulture)
            //};
   

        }


        private void RaiseFakeEvent()
        {
            _transponderReceiver.TransponderDataReady += Raise.EventWith(_dataEvent);
        }


        [Test]
        public void RaisedEvent_ValidTracks_CorrectTrackCount()
        {
            RaiseFakeEvent();
            _trackRendition.Received().Print(Arg.Is<List<ITrackData>>(data => data.Count == 1));

        }



        [Test]
        public void RaisedEvent_ValidTracks_CorrectTagName()
        {
            
            RaiseFakeEvent();
            _trackRendition.Received().Print(Arg.Is<List<ITrackData>>(data => data[0].Tag == "JAS001"));

        }

        [Test]
        public void RaisedEvent_ValidTracks_CorrectXValueReceived()
        {

            
            RaiseFakeEvent();
            _trackRendition.Received().Print(Arg.Is<List<ITrackData>>(data => data[0].X == 12345));
        }


        [Test]
        public void RaisedEvent_ValidTracks_CorrectYValueReceived()
        {

            
            RaiseFakeEvent();
            _trackRendition.Received().Print(Arg.Is<List<ITrackData>>(data => data[0].Y == 67890));
        }

        [Test]
        public void RaisedEvent_ValidTracks_CorrectAltitudeReceived()
        {

            
            RaiseFakeEvent();
            _trackRendition.Received().Print(Arg.Is<List<ITrackData>>(data => data[0].Altitude == 12000));
        }


        [Test]
        public void RaisedEvent_ValidTracks_CorrectTimeStampYearReceived()
        {

            
            RaiseFakeEvent();
            _trackRendition.Received().Print(Arg.Is<List<ITrackData>>(data => data[0].TimeStamp.Year == 2016));
        }


        [Test]
        public void RaisedEvent_ValidTracks_CorrectTimeStampMonthReceived()
        {
            
            RaiseFakeEvent();
            _trackRendition.Received().Print(Arg.Is<List<ITrackData>>(data => data[0].TimeStamp.Month == 01));
        }

        [Test]
        public void RaisedEvent_ValidTracks_CorrectTimeStampDayReceived()
        {
           
            RaiseFakeEvent();
            _trackRendition.Received().Print(Arg.Is<List<ITrackData>>(data => data[0].TimeStamp.Day == 01));
        }

        [Test]
        public void RaisedEvent_ValidTracks_CorrectTimeStampHourReceived()
        {
            
            RaiseFakeEvent();
            _trackRendition.Received().Print(Arg.Is<List<ITrackData>>(data => data[0].TimeStamp.Hour == 10));
        }

        [Test]
        public void RaisedEvent_ValidTracks_CorrectTimeStampMinuteReceived()
        {
            RaiseFakeEvent();
            _trackRendition.Received().Print(Arg.Is<List<ITrackData>>(data => data[0].TimeStamp.Minute == 09));
        }


        [Test]
        public void RaisedEvent_ValidTracks_CorrectTimeStampSecondReceived()
        {
           
            RaiseFakeEvent();
            _trackRendition.Received().Print(Arg.Is<List<ITrackData>>(data => data[0].TimeStamp.Second == 09));
        }

        [Test]
        public void RaisedEvent_ValidTracks_CorrectTimeStampMillisecondsReceived()
        {
            
            RaiseFakeEvent();
            _trackRendition.Received().Print(Arg.Is<List<ITrackData>>(data => data[0].TimeStamp.Millisecond == 111));
        }

        [Test]
        public void RaisedEvent_ThreeValidTracks_CorrectCountReceived()
        {
            
           _dataEvent.TransponderData.Add("JAS002;12345;67890;12000;20160101100909111");
           _dataEvent.TransponderData.Add("JAS003;12345;67890;12000;20160101100909111");
           RaiseFakeEvent();
           _trackRendition.Received().Print(Arg.Is<List<ITrackData>>(data => data.Count == 3));

        }

        [Test]
        public void RaisedEvent_ThreeValidTracks_CorrectTagsReceived()
        {
            _faketrackList.Add(_fakeTrackData);
            _dataEvent.TransponderData.Add("JAS002;12345;67890;12000;20160101100909111");
            _dataEvent.TransponderData.Add("JAS003;12345;67890;12000;20160101100909111");
            RaiseFakeEvent(); 
            _trackRendition.Received().Print(Arg.Is<List<ITrackData>>(data => data[2].Tag == "JAS003"));


        }


    }
}
