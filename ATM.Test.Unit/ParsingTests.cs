using System;
using System.Collections.Generic;
using ATM.Data;
using ATM.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute; 
using NUnit.Framework;
using TransponderReceiver;

namespace ATM.Test.Unit
{
    [TestFixture]

    class ParsingTests
    {
        private Parsing _uut; 
        private ITransponderReceiver _receiver;
        private RawTransponderDataEventArgs _fakeTransponderDataEventArgs;
        private IFiltering _filtering;



        [SetUp]
        public void Setup()
        {
            _receiver = Substitute.For<ITransponderReceiver>();
            _filtering = Substitute.For<IFiltering>(); 
            _uut = new Parsing(_receiver, _filtering);
            _fakeTransponderDataEventArgs = new RawTransponderDataEventArgs(new List<string>()
                { "JAS001;12345;67890;12000;20160101100909111" });

        }

        private void RaiseFakeEvent()
        {
            _receiver.TransponderDataReady += Raise.EventWith(_fakeTransponderDataEventArgs);
        }

        [Test]
        public void OneTrackInList_CountCorrect()
        {
            RaiseFakeEvent();
            _filtering.Received().ValidateTracks(Arg.Is<List<ITrackData>>(x => x.Count == 1));

        }


        [Test]
        public void OneTrackInList_TagCorrect()
        {
            RaiseFakeEvent();
            _filtering.Received().ValidateTracks(Arg.Is<List<ITrackData>>(x => x[0].Tag == "JAS001"));

        }

        [Test]
        public void OneTrackInList_XCorrect()
        {
            RaiseFakeEvent();
            _filtering.Received().ValidateTracks(Arg.Is<List<ITrackData>>(x => x[0].X == 12345));
        }


        [Test]
        public void OneTrackInList_YCorrect()
        {
            RaiseFakeEvent();
            _filtering.Received().ValidateTracks(Arg.Is<List<ITrackData>>(x => x[0].Y == 67890));
        }

        [Test]
        public void OneTrackInList_AltitudeCorrect()
        {
            RaiseFakeEvent();
            _filtering.Received().ValidateTracks(Arg.Is<List<ITrackData>>(x => x[0].Altitude == 12000));
        }


        [Test]
        public void OneTrackInList_TimeStampYearCorrect()
        {
            RaiseFakeEvent();
            _filtering.Received().ValidateTracks(Arg.Is<List<ITrackData>>(x => x[0].TimeStamp.Year == 2016));
        }


        [Test]
        public void OneTrackInList_TimeStampMonthCorrect()
        {
            RaiseFakeEvent();
            _filtering.Received().ValidateTracks(Arg.Is<List<ITrackData>>(x => x[0].TimeStamp.Month == 01));
        }

        [Test]
        public void OneTrackInList_TimeStampDayCorrect()
        {
            RaiseFakeEvent();
            _filtering.Received().ValidateTracks(Arg.Is<List<ITrackData>>(x => x[0].TimeStamp.Day == 01));
        }

        [Test]
        public void OneTrackInList_TimeStampHourCorrect()
        {
            RaiseFakeEvent();
            _filtering.Received().ValidateTracks(Arg.Is<List<ITrackData>>(x => x[0].TimeStamp.Hour == 10));
        }

        [Test]
        public void OneTrackInList_TimeStampMinuteCorrect()
        {
            RaiseFakeEvent();
            _filtering.Received().ValidateTracks(Arg.Is<List<ITrackData>>(x => x[0].TimeStamp.Minute == 09 ));
        }


        [Test]
        public void OneTrackInList_TimeStampSecondCorrect()
        {
            RaiseFakeEvent();
            _filtering.Received().ValidateTracks(Arg.Is<List<ITrackData>>(x => x[0].TimeStamp.Second == 09));
        }

        [Test]
        public void OneTrackInList_TimeStampMsCorrect()
        {
            RaiseFakeEvent();
            _filtering.Received().ValidateTracks(Arg.Is<List<ITrackData>>(x => x[0].TimeStamp.Millisecond == 111));
        }

        [Test]
        public void ThreeTracksInList_CountCorrect()
        {
           _fakeTransponderDataEventArgs.TransponderData.Add("JAS002;12345;67890;12000;20160101100909111");
           _fakeTransponderDataEventArgs.TransponderData.Add("JAS002;12345;67890;12000;20160101100909111");
           RaiseFakeEvent();
            _filtering.Received().ValidateTracks(Arg.Is<List<ITrackData>>(x => x.Count == 3));

        }

        [Test]
        public void ThreeTracksInList_ThirdTagCorrect()
        {
           _fakeTransponderDataEventArgs.TransponderData.Add("JAS002;12345;67890;12000;20160101100909111");
           _fakeTransponderDataEventArgs.TransponderData.Add("JAS003;12345;67890;12000;20160101100909111");
            RaiseFakeEvent();
            _filtering.Received().ValidateTracks(Arg.Is<List<ITrackData>>(x => x[2].Tag == "JAS003"));


        }
    }
}
