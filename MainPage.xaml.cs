using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;
using Windows.UI.ViewManagement;
using System.Text;


namespace Data_Collector_TWR
{
 //  C# 2 class term Data Collector project by Thomas W. Rodke  5/2/2018
    public sealed partial class MainPage : Page
    {
        private Timer timer;
        MeasureLengthDevice measurementDevice = null;
        MainDisplayData displayData = null;

        public MainPage()  // MainPage constructor
        {
            measurementDevice = new MeasureLengthDevice();

            this.InitializeComponent();
            ApplicationView.PreferredLaunchViewSize = new Size(550, 1100);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            timer = new Timer(timer_Tick, null, 
               (int)TimeSpan.FromSeconds(1).TotalMilliseconds, 
               (int)TimeSpan.FromSeconds(5).TotalMilliseconds);
            
            Debug.WriteLine("at timer in MainPage constructor");

            //  whatever you want binded to the display, you have to put in this displayData section,

            displayData = new MainDisplayData
            {     // default initializer for displayData?
                Measurement = measurementDevice.Measurement,
                History = measurementDevice.History,
                AltMeasurement = measurementDevice.MetricValue()
            };
        }

        private async void timer_Tick(object state)
        {
            //code to randomly generate a new value and update GetMeasurement
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () => {
                //    string Imp_measure = measurementDevice.ImperialValue().ToString();
                //    string Met_measure = measurementDevice.MetricValue().ToString();

                //    // check whether Metric or Imperial units are used
                if (optImperial.IsChecked == true)
                {
                    displayData.Measurement = measurementDevice.Measurement;
                    displayData.AltMeasurement = measurementDevice.Measurement * 2.54m;

                    MainUnitsLabel.Text = " in";
                    AltUnitsLabel.Text = " cm";

                    //txtHistory.Text += Imp_measure + '\n';

                }
                if (optMetric.IsChecked == true)
                {
                    //displayData.Measurement = measurementDevice.Measurement * 2.54m;
                    displayData.AltMeasurement = measurementDevice.Measurement;

                    MainUnitsLabel.Text = " cm";
                    AltUnitsLabel.Text =  " in";
                    //txtHistory.Text += Met_measure + '\n';
                }

                DateTimeStamp.Text = DateTime.Now.ToString();
                displayData.History = measurementDevice.History;

                this.DataContext = null;
                this.DataContext = displayData;

            });
        }

        public void btnStart_Click(object sender, RoutedEventArgs e)
        {
            measurementDevice.StartCollecting();
        }

        private void btnGetRawData_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder rawData = new StringBuilder();
            for (int i = 0; i < measurementDevice.GetRawData().Count(); i++)
            {
                rawData.Append(measurementDevice.GetRawData().ElementAt(i));
            }

            txtRawData.Text = rawData.ToString();

        }

        public void btnStop_Click(object sender, RoutedEventArgs e)
        {
            measurementDevice.StopCollecting();
            timer.Change(Timeout.Infinite, Timeout.Infinite);
        }
    }
}
