using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Data;
using TransponderReceiver;

namespace ATM
{
    public interface IParsing
    {
        TrackData ConvertData(string data);
        void Data(object o, RawTransponderDataEventArgs args);
    }
}
