using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Collector_TWR
{
    class MainDisplayData
    {
        private int measurement;
        private decimal altMeasurement;
        private string history;
        private int[] rawData;
        //private enum mainUnits { cm, inches };
        //private enum altUnits { cm, inches };
        public int Measurement
        {
            get { return this.measurement; }
            set { this.measurement = value; }
        }
        public decimal AltMeasurement
        {
            get { return this.altMeasurement; }
            set { this.altMeasurement = value; }
        }
        public string History
        {
            get { return this.history; }
            set { this.history = value; }
        }

        public enum MainUnits { cm, inches };
        public enum AltUnits { cm, inches };
    }
}
