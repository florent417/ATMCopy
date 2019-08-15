using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Interfaces;

namespace ATM
{
    public interface IEventRendition
    {
        void LogToFile(List<IProximityDetectionData> proximityDetectionDatas);
        void PrintEvent(List<IProximityDetectionData> proximityDetectionDatas);
        
    }
}
