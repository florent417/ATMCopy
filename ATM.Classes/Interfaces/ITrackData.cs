using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Interfaces
{
    public interface ITrackData
    {
         string Tag { get; set; }
         int X { get; set; }
         int Y { get; set; }
         int Altitude { get; set; }
         int Velocity { get; set; }
         int Course { get; set; }

         DateTime TimeStamp { get; set; }
        string ToString();
    }
}
