using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Data;

namespace ATM.Interfaces
{
    public interface ITrackRendition
    {
        void Print(List<ITrackData> trackDataList);
    }
}
