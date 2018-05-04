using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using System.Diagnostics;

namespace Data_Collector_TWR
{
    class MeasureLengthDevice : IMeasuringDevice
    {
        public Timer timer;
        public Device myDevice;
        public FixedSizedQueue<int> myQueue;
        private int[] dataCaptured;
        private UnitsEnumeration unitsToUse;
        private int mostRecentMeasure;

        public MeasureLengthDevice()  // MeasureLengthDevice constructor
        {
            myDevice = new Device();
            myQueue = new FixedSizedQueue<int>();
            myQueue.Limit = 10;
            dataCaptured = new int[10];
            Debug.WriteLine("timer has been instantiated");
            mostRecentMeasure = Measurement;
        }
        public int Measurement
        {
            get { return this.mostRecentMeasure; }
        }

        //public int[] RawData
        //{
        //    get { return this.dataCaptured; }
        //}
        public string History => DisplayHistory(myQueue);
        private string DisplayHistory(FixedSizedQueue<int> myCollection)
        { 
            //code to return history from ConcurrentQueue
            StringBuilder myString = new StringBuilder();
            int y = 0;
            foreach (var i in myCollection.q)
            {
                myString.AppendLine(i.ToString() + "  " + DateTime.Now.ToString());
                dataCaptured[y] = i;
                y++;
            }
            return myString.ToString();
        }

        public int[] GetRawData()
        {
            //Device dataCaptured[] = new Device();


            //for (int i = 0; i < 10; i++)
            //{
            //    dataCaptured[i] = mostRecentMeasure;
            //}
            //return dataCaptured;

            return dataCaptured;
        }

        public decimal ImperialValue()
        {
            // RawData input is defined by Statement of Work to be Imperial (inches) units.
            return mostRecentMeasure;
        }

        public decimal MetricValue()
        {
            // write code to do conversion of value
            return mostRecentMeasure * 2.54m;
        }

        public void StartCollecting()
        {
            timer = new Timer(timer_Tick, null, 
                (int)TimeSpan.FromSeconds(1).TotalMilliseconds, 
                (int)TimeSpan.FromSeconds(5).TotalMilliseconds);
            Debug.WriteLine("at timer in StartCollecting() method in MeasureLengthDevice class");
        }
        private async void timer_Tick(object state)
        {
            //code to randomly generate a new value and update GetMeasurement
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () => {
                mostRecentMeasure = myDevice.GetMeasurement;
                myQueue.Enqueue(mostRecentMeasure);
            });
        }

        public void StopCollecting()
        {
            timer.Change(Timeout.Infinite,Timeout.Infinite);
        }
    }
}
