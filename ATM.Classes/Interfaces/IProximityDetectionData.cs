using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Interfaces
{
    public interface IProximityDetectionData
    {
        string Tag1 { get; set; }
        string Tag2 { get; set; }
        DateTime Timestamp { get; set; }
    }
}
