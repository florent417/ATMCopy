////using System;
////using System.Collections.Generic;
////using System.IO;
////using System.Linq;
////using System.Text;
////using System.Threading.Tasks;

using System.Collections.Generic;
using System.IO;
using ATM.Interfaces;

namespace ATM
{
    public class EventRendition : IEventRendition
    {
        private IProximityDetectionData _proximityDetectionData;

        //public EventRendition(List<IProximityDetectionData> proximityDetectionData)
        //{
           
        //    proximityDetectionData = new List<IProximityDetectionData>();
        //}

        public EventRendition()
        {
        }



        public void LogToFile(List<IProximityDetectionData> proximityDetectionDatas)
        {

            using (StreamWriter outputFile = new StreamWriter(@"Logfile.txt", true))
            {
                foreach (var proximityDetectionData in proximityDetectionDatas)
                {
                    string text = "Planes in conflict: " + proximityDetectionData.Tag1 + " and " +
                                  proximityDetectionData.Tag2 +
                                  "\nTime of occurance: " + proximityDetectionData.Timestamp;
                    outputFile.WriteLine(text);
                }
            }
        }  

        public void PrintEvent(List<IProximityDetectionData> proximityDetectionDatas)
        {
            //Print the collision warning to the console
           foreach (var data in proximityDetectionDatas)
            {
                System.Console.WriteLine(data);
           }
        }
    }
}
