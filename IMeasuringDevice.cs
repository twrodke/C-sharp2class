using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Collector_TWR
{
    interface IMeasuringDevice  // an interface needs no constructor?
    {
        // The MetricValue() method will return a decimal that represents the metric value
        // of the most recent measurement that was captured.
        decimal MetricValue();

        // The ImperialValue() method will return a decimal that represents the imperial value
        // of the most recent measurement that was captured.
        decimal ImperialValue();

        // The StartCollecting() method will retrieve a copy of all of the recent data that the measuring device has captured.
        // The data will be returned as an array of integer values. It will begin collecting measurements and record them.
        void StartCollecting();

        // The StopCollecting() method will stop the device.  It will cease collecting measurements. 
        void StopCollecting();

        // The GetRawData() method starts the device running.
        int[] GetRawData();
    }
}
