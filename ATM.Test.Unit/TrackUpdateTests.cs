using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Data;
using ATM.Interfaces;
using NSubstitute;
using NSubstitute.Core;
using NUnit.Framework;

namespace ATM.Test.Unit
{
    [TestFixture]
    public class TrackUpdateTests
    {
        //private List<ITrackData> _track;
        private ITrackData _track1;
        private ITrackData _track2;

        private List<ITrackData>_trackData;                      //List holds trackdataobjects
        //private ITrackUpdate _uut;
        private ITrackRendition _trackRendition;
        private IProximityDetection _proximityDetection;

        [SetUp]

        public void SetUp()
        {
            _trackRendition = Substitute.For<ITrackRendition>();
            _proximityDetection = Substitute.For<IProximityDetection>();

            _trackData = new List<ITrackData>();                //initial
            //_uut=new TrackUpdate(_trackData);

            _track1 = Substitute.For<ITrackData>();
            _track2 = Substitute.For<ITrackData>();

            _track1.TimeStamp.Returns(new DateTime(2018, 05, 13, 10, 50, 35));
            _track2.TimeStamp.Returns(new DateTime(2018, 05, 13, 10, 20, 31));

            
        }

        [Test]
        public void Update_TimeStampOldandNew_returnsEqual()
        {
            
            var uut = new TrackUpdate(_trackRendition, _proximityDetection);
            _trackData.Add(_track1);
            _trackData.Add(_track2);

            uut.Update(_trackData);                     //new list
            Assert.That(_trackData[0].TimeStamp, Is.EqualTo(uut.oldList[0].TimeStamp)); //New list is equal to oldList
        }

        [Test]
        public void Update_TimeStampOldandNew_returnsNotEqual()
        {

            var uut = new TrackUpdate(_trackRendition, _proximityDetection);
            _trackData.Add(_track1);
            _trackData.Add(_track2);

            uut.Update(_trackData);                    
            Assert.AreNotEqual(_trackData[1].TimeStamp, uut.oldList[0].TimeStamp);
        }

        [Test]
        public void Update_VelocityOldandNew_returnsEqual()
        {

            var uut = new TrackUpdate(_trackRendition, _proximityDetection);
            _trackData.Add(_track1);
            _trackData.Add(_track2);

            uut.Update(_trackData);                     //new list
            Assert.That(_trackData[0].Velocity, Is.EqualTo(uut.oldList[0].Velocity));
        }

        [Test]
        public void Update_VelocityOldandNew_returnsNotEqual()
        {
            
            var uut = new TrackUpdate(_trackRendition, _proximityDetection);
            _track1.TimeStamp.Returns(new DateTime(2018, 05, 13, 10, 50, 30));
            _track1.X.Returns(58000);
            _track1.Y.Returns(67000);
            _track1.Tag.Returns("SHN63");
            

            _trackData.Add(_track1);

            uut.Update(_trackData);         //new list SHN63 og old list SHN63

          // _trackData.Clear();

            _track2.TimeStamp.Returns(new DateTime(2018, 05, 13, 10, 50, 36));
            _track2.X.Returns(65000);
            _track2.Y.Returns(71000);
            _track2.Tag.Returns("SHN63");
            
            

            _trackData.Add(_track2);
            var vel = new TrackUpdate(_trackRendition, _proximityDetection).CalVelocity(_track1, _track2);   
            uut.Update(_trackData);
            
            Assert.AreNotEqual(vel, uut.oldList[0].Velocity);
        }

        [Test]
        public void Update_CourseOldandNew_returnsNotEqual()
        {
            var uut = new TrackUpdate(_trackRendition, _proximityDetection);
            //_track1.TimeStamp.Returns(new DateTime(2018, 05, 13, 10, 50, 30));
            _track1.X.Returns(58000);
            _track1.Y.Returns(67000);
            _track1.Tag.Returns("SHN63");


            _trackData.Add(_track1);

            uut.Update(_trackData);         //new list SHN63 og old list SHN63

            _trackData.Clear();

            //_track2.TimeStamp.Returns(new DateTime(2018, 05, 13, 10, 50, 31));
            _track2.X.Returns(65000);
            _track2.Y.Returns(71000);
            _track2.Tag.Returns("SHN63");



            _trackData.Add(_track2);
            var cor = new TrackUpdate(_trackRendition, _proximityDetection).CalCourse(_track1, _track2);
            uut.Update(_trackData);

            Assert.AreNotEqual(cor, uut.oldList[0].Course);
        }


        [Test]
        public void Update_CourseOldandNew_returnsEqual()
        {
            var uut= new TrackUpdate(_trackRendition, _proximityDetection);
            _trackData.Add(_track1);
            _trackData.Add(_track2);

            uut.Update(_trackData);
            Assert.That(_trackData[0].Course, Is.EqualTo(uut.oldList[0].Velocity));
        }



        [TestCase(50000, 50100,50000,50100,141)]
        [TestCase(85000, 60000, 90000, 60100, 38974)]
        [TestCase(50000, 50000, 50100, 50100, 0)]                    //No change in velocity
        [TestCase(50000, 90000, 50100, 90000, 56497)]
        public void CalVelocity_CalculateTrack1andTrack2_ReturnsVelocity(int x1, int x2, int y1, int y2, int result) //WORKS
        {
            var uut = new TrackUpdate(_trackRendition, _proximityDetection);
            _track1.TimeStamp.Returns(new DateTime(2018, 05, 13, 10, 50, 35));
            _track2.TimeStamp.Returns(new DateTime(2018, 05, 13, 10, 50, 36));
            _track1.X.Returns(x1);
            _track2.X.Returns(x2);
            _track1.Y.Returns(y1);
            _track2.Y.Returns(y2);

            _trackData.Add(_track1);
            _trackData.Add(_track2);

            Assert.That(uut.CalVelocity(_track1,_track2), Is.EqualTo(result));
        }


        [Test]
        [TestCase(20000, 10000, 20000, 10000, 225)]
        [TestCase(20000, 20000, 10000, 10000, 180)]
        [TestCase(50000, 87000, 90000, 50000, 137)]
        [TestCase(50000, 87000, 10000, 50000, 42)]
        [TestCase(80000, 77000, 10000, 50000, 355)]

        public void CalCourse_CalculateTrack1andTrack2_Returns(int x1, int x2, int y1, int y2, int result)
        {
            var uut = new TrackUpdate(_trackRendition, _proximityDetection);
            _track1.X.Returns(x1);
            _track2.X.Returns(x2);
            _track1.Y.Returns(y1);
            _track2.Y.Returns(y2);
            _trackData.Add(_track1);
            _trackData.Add(_track2);
            Assert.That(uut.CalCourse(_track1, _track2), Is.EqualTo(result));

        }

        [Test]
        public void TrackRendition_IsCalled_True()
        {
            var uut = new TrackUpdate(_trackRendition, _proximityDetection);
            uut.Update(_trackData);

            _trackRendition.Received().Print(_trackData);
        }

        [Test]
        public void ProximityDetection_IsCalled_True()
        {
            var uut = new TrackUpdate(_trackRendition, _proximityDetection);
            uut.Update(_trackData);

            _proximityDetection.Received().CheckProximityDetection(_trackData);
        }

    }
}
