using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.IO.Ports;
using NAudio.Wave;
using NAudio.Utils;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System.Drawing;




namespace arduino_button
{
    public partial class MainWindow : Window
    {
        int time_play = 50;
        public string data;
        public string comPORT;

        SerialPort port = new SerialPort();
        Tab_Volume tab_vol = new Tab_Volume();

        bool state;

        public SeriesCollection MySeriesCollection { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public Func<double, string> XFormatter { get; set; }

        public int[] Labels { get; set; }
        public string[] HLTopValues { get; set; }

        int[] test_tab = { 1, 2, 3, 4, 5 };

        public MainWindow()
        {
            InitializeComponent();

            string[] ports = SerialPort.GetPortNames();
            comboBox.ItemsSource = ports;
            comboBox.SelectedIndex = 0;

            

            //bt_report.IsEnabled = true;

        }

        int counter_volume_250_l;
        int counter_volume_250_r;
        int counter_volume_500_l;
        int counter_volume_500_r;
        int counter_volume_1000_l;
        int counter_volume_1000_r;
        int counter_volume_2000_l;
        int counter_volume_2000_r;
        int counter_volume_4000_l;
        int counter_volume_4000_r;

        int dB_250_L;
        int dB_250_R;
        int dB_500_L;
        int dB_500_R;
        int dB_1000_L;
        int dB_1000_R;
        int dB_2000_L;
        int dB_2000_R;
        int dB_4000_L;
        int dB_4000_R;

        private async void L_250Hz(object sender, RoutedEventArgs e)
        {

            bt_250Hz_L.IsEnabled = false;
            int counter = 0;
            counter_volume_250_l = 0;
            port.Open();
            state = true;
            float volume = 0.000001f;
            string filename = "250hz_l.mp3";
            string source = System.IO.Path.Combine(Environment.CurrentDirectory, @"Sound\", filename);
            var reader = new Mp3FileReader(source);
            // or WaveOutEvent()
            var waveOut = new WaveOut();
            waveOut.Init(reader);
            waveOut.Play();
            waveOut.Volume = (float)volume;

            for (; ; )
            {
                try
                {
                    if (counter % time_play == 0)
                    {
                        counter_volume_250_l++;
                        volume = (float)tab_vol.tab_250Hz[counter_volume_250_l];

                    }
                    if (Check() == 1)
                    {
                       
                        break;
                    }
                    waveOut.Volume = volume;
                    await Task.Delay(100);
                    counter++;
                    if(bt_start.IsEnabled == true)
                    {
                        waveOut.Stop();
                        port.Close();
                        break;
                    }
                }
                catch (System.IndexOutOfRangeException)
                {
                    counter_volume_250_l = 20;
                    break;
                }
            }

            waveOut.Stop();
            port.Close();
            
            if (bt_start.IsEnabled == false)
            {
                bt_250Hz_R.IsEnabled = true;
            }
            
        }

        private async void R_250Hz(object sender, RoutedEventArgs e)
        {
            bt_250Hz_R.IsEnabled = false;
            port.Open();
            int counter = 0;
            counter_volume_250_r = 0;
            float volume = 0.000001f;
            string filename = "250hz_r.mp3";
            string source = System.IO.Path.Combine(Environment.CurrentDirectory, @"Sound\", filename);
            var reader = new Mp3FileReader(source);
            var waveOut = new WaveOut(); // or WaveOutEvent()
            waveOut.Init(reader);
            waveOut.Play();
            waveOut.Volume = (float)volume;


            for (; ; )
            {
                try
                {
                    if (counter % time_play == 0)
                    {
                        counter_volume_250_r++;
                        volume = (float)tab_vol.tab_250Hz[counter_volume_250_r];
                    }
                    if (Check() == 1)
                    {
                        break;
                    }
                    waveOut.Volume = volume;
                    await Task.Delay(100);
                    counter++;
                    if (bt_start.IsEnabled == true)
                    {
                        waveOut.Stop();
                        port.Close();
                        break;
                    }
                }
                catch (System.IndexOutOfRangeException)
                {
                    counter_volume_250_r = 20;
                    break;
                }
            }


            waveOut.Stop();
            port.Close();

            if (bt_start.IsEnabled == false)
            {
                bt_250Hz_L.Visibility = Visibility.Hidden;
                bt_250Hz_R.Visibility = Visibility.Hidden;
                bt_500Hz_L.Visibility = Visibility.Visible;
                bt_500Hz_R.Visibility = Visibility.Visible;
                bt_500Hz_R.IsEnabled = false;
            }
        }

        private async void L_500Hz(object sender, RoutedEventArgs e)
        {

            bt_500Hz_L.IsEnabled = false;
            port.Open();
            int counter = 0;
            counter_volume_500_l = 0;
            float volume = 0.000001f;
            string filename = "500hz_l.mp3";
            string source = System.IO.Path.Combine(Environment.CurrentDirectory, @"Sound\", filename);
            var reader = new Mp3FileReader(source);
            var waveOut = new WaveOut(); // or WaveOutEvent()
            waveOut.Init(reader);
            waveOut.Play();
            waveOut.Volume = (float)volume;


            for (; ; )
            {
                try
                {
                    if (counter % time_play == 0)
                    {
                        counter_volume_500_l++;
                        volume = (float)tab_vol.tab_500Hz[counter_volume_500_l];
                    }
                    if (Check() == 1)
                    {
                        break;
                    }
                    waveOut.Volume = volume;
                    await Task.Delay(100);
                    counter++;
                    if (bt_start.IsEnabled == true)
                    {
                        waveOut.Stop();
                        port.Close();
                        break;
                    }
                }
                catch (System.IndexOutOfRangeException)
                {
                    counter_volume_500_l = 20;
                    break;
                }
            }


            waveOut.Stop();
            port.Close();

            if (bt_start.IsEnabled == false)
            {
                bt_500Hz_R.IsEnabled = true;
            }
        }

        private async void R_500Hz(object sender, RoutedEventArgs e)
        {
            bt_500Hz_R.IsEnabled = false;
            port.Open();
            int counter = 0;
            counter_volume_500_r = 0;
            float volume = 0.000001f;
            string filename = "500hz_r.mp3";
            string source = System.IO.Path.Combine(Environment.CurrentDirectory, @"Sound\", filename);
            var reader = new Mp3FileReader(source);
            var waveOut = new WaveOut(); // or WaveOutEvent()
            waveOut.Init(reader);
            waveOut.Play();
            waveOut.Volume = (float)volume;


            for (; ; )
            {
                try
                {
                    if (counter % time_play == 0)
                    {
                        counter_volume_500_r++;
                        volume = (float)tab_vol.tab_500Hz[counter_volume_500_r];
                    }
                    if (Check() == 1)
                    {
                        break;
                    }
                    waveOut.Volume = volume;
                    await Task.Delay(100);
                    counter++;
                    if (bt_start.IsEnabled == true)
                    {
                        waveOut.Stop();
                        port.Close();
                        break;
                    }
                }
                catch (System.IndexOutOfRangeException)
                {
                    counter_volume_500_r = 20;
                    break;
                }
            }


            waveOut.Stop();
            port.Close();

            if (bt_start.IsEnabled == false)
            {
                bt_500Hz_L.Visibility = Visibility.Hidden;
                bt_500Hz_R.Visibility = Visibility.Hidden;
                bt_1000Hz_L.Visibility = Visibility.Visible;
                bt_1000Hz_R.Visibility = Visibility.Visible;
                bt_1000Hz_R.IsEnabled = false;
            }
        }

        private async void L_1000Hz(object sender, RoutedEventArgs e)
        {
            bt_1000Hz_L.IsEnabled = false;
            port.Open();
            int counter = 0;
            counter_volume_1000_l = 0;
            float volume = 0.000001f;
            string filename = "1000hz_l.mp3";
            string source = System.IO.Path.Combine(Environment.CurrentDirectory, @"Sound\", filename);
            var reader = new Mp3FileReader(source);
            var waveOut = new WaveOut(); // or WaveOutEvent()
            waveOut.Init(reader);
            waveOut.Play();
            waveOut.Volume = (float)volume;


            for (; ; )
            {
                try
                {
                    if (counter % time_play == 0)
                    {
                        counter_volume_1000_l++;
                        volume = (float)tab_vol.tab_1000Hz[counter_volume_1000_l];
                    }
                    if (Check() == 1)
                    {
                        break;
                    }
                    waveOut.Volume = volume;
                    await Task.Delay(100);
                    counter++;
                    if (bt_start.IsEnabled == true)
                    {
                        waveOut.Stop();
                        port.Close();
                        break;
                    }
                }
                catch (System.IndexOutOfRangeException)
                {
                    counter_volume_1000_l = 20;
                    break;
                }
            }


            waveOut.Stop();
            port.Close();

            if (bt_start.IsEnabled == false)
            {
                bt_1000Hz_R.IsEnabled = true;
            }
        }

        private async void R_1000Hz(object sender, RoutedEventArgs e)
        {
            bt_1000Hz_R.IsEnabled = false;
            port.Open();
            int counter = 0;
            counter_volume_1000_r = 0;
            float volume = 0.000001f;
            string filename = "1000hz_r.mp3";
            string source = System.IO.Path.Combine(Environment.CurrentDirectory, @"Sound\", filename);
            var reader = new Mp3FileReader(source);
            var waveOut = new WaveOut(); // or WaveOutEvent()
            waveOut.Init(reader);
            waveOut.Play();
            waveOut.Volume = (float)volume;


            for (; ; )
            {
                try
                {
                    if (counter % time_play == 0)
                    {
                        counter_volume_1000_r++;
                        volume = (float)tab_vol.tab_1000Hz[counter_volume_1000_r];
                    }
                    if (Check() == 1)
                    {
                        break;
                    }
                    waveOut.Volume = volume;
                    await Task.Delay(100);
                    counter++;
                    if (bt_start.IsEnabled == true)
                    {
                        waveOut.Stop();
                        port.Close();
                        break;
                    }
                }
                catch (System.IndexOutOfRangeException)
                {
                    counter_volume_1000_r = 20;
                    break;
                }
            }


            waveOut.Stop();
            port.Close();

            if (bt_start.IsEnabled == false)
            {
                bt_1000Hz_L.Visibility = Visibility.Hidden;
                bt_1000Hz_R.Visibility = Visibility.Hidden;
                bt_2000Hz_L.Visibility = Visibility.Visible;
                bt_2000Hz_R.Visibility = Visibility.Visible;
                bt_2000Hz_R.IsEnabled = false;
            }
        }

        private async void L_2000Hz(object sender, RoutedEventArgs e)
        {
            bt_2000Hz_L.IsEnabled = false;
            port.Open();
            int counter = 0;
            counter_volume_2000_l = 0;
            float volume = 0.000001f;
            string filename = "2000hz_l.mp3";
            string source = System.IO.Path.Combine(Environment.CurrentDirectory, @"Sound\", filename);
            var reader = new Mp3FileReader(source);
            var waveOut = new WaveOut(); // or WaveOutEvent()
            waveOut.Init(reader);
            waveOut.Play();
            waveOut.Volume = (float)volume;


            for (; ; )
            {
                try
                {
                    if (counter % time_play == 0)
                    {
                        counter_volume_2000_l++;
                        volume = (float)tab_vol.tab_2000Hz[counter_volume_2000_l];
                    }
                    if (Check() == 1)
                    {
                        break;
                    }
                    waveOut.Volume = volume;
                    await Task.Delay(100);
                    counter++;
                    if (bt_start.IsEnabled == true)
                    {
                        waveOut.Stop();
                        port.Close();
                        break;
                    }
                }
                catch (System.IndexOutOfRangeException)
                {
                    counter_volume_2000_l = 20;
                    break;
                }
            }


            waveOut.Stop();
            port.Close();

            if (bt_start.IsEnabled == false)
            {
                bt_2000Hz_R.IsEnabled = true;
            }
        }

        private async void R_2000Hz(object sender, RoutedEventArgs e)
        {
            bt_2000Hz_R.IsEnabled = false;
            port.Open();
            int counter = 0;
            counter_volume_2000_r = 0;
            float volume = 0.000001f;
            string filename = "2000hz_r.mp3";
            string source = System.IO.Path.Combine(Environment.CurrentDirectory, @"Sound\", filename);
            var reader = new Mp3FileReader(source);
            var waveOut = new WaveOut(); // or WaveOutEvent()
            waveOut.Init(reader);
            waveOut.Play();
            waveOut.Volume = (float)volume;


            for (; ; )
            {
                try
                {
                    if (counter % time_play == 0)
                    {
                        counter_volume_2000_r++;
                        volume = (float)tab_vol.tab_2000Hz[counter_volume_2000_r];
                    }
                    if (Check() == 1)
                    {
                        break;
                    }
                    waveOut.Volume = volume;
                    await Task.Delay(100);
                    counter++;
                    if (bt_start.IsEnabled == true)
                    {
                        waveOut.Stop();
                        port.Close();
                        break;
                    }
                }
                catch (System.IndexOutOfRangeException)
                {
                    counter_volume_2000_r = 20;
                    break;
                }
            }


            waveOut.Stop();
            port.Close();

            if (bt_start.IsEnabled == false)
            {
                bt_2000Hz_L.Visibility = Visibility.Hidden;
                bt_2000Hz_R.Visibility = Visibility.Hidden;
                bt_4000Hz_L.Visibility = Visibility.Visible;
                bt_4000Hz_R.Visibility = Visibility.Visible;
                bt_4000Hz_R.IsEnabled = false;
            }
        }

        private async void L_4000Hz(object sender, RoutedEventArgs e)
        {
            bt_4000Hz_L.IsEnabled = false;
            port.Open();
            int counter = 0;
            counter_volume_4000_l = 0;
            float volume = 0.000001f;
            string filename = "4000hz_l.mp3";
            string source = System.IO.Path.Combine(Environment.CurrentDirectory, @"Sound\", filename);
            var reader = new Mp3FileReader(source);
            var waveOut = new WaveOut(); // or WaveOutEvent()
            waveOut.Init(reader);
            waveOut.Play();
            waveOut.Volume = (float)volume;


            for (; ; )
            {
                try
                {
                    if (counter % time_play == 0)
                    {
                        counter_volume_4000_l++;
                        volume = (float)tab_vol.tab_4000Hz[counter_volume_4000_l];
                    }
                    if (Check() == 1)
                    {
                        break;
                    }
                    waveOut.Volume = volume;
                    await Task.Delay(100);
                    counter++;
                    if (bt_start.IsEnabled == true)
                    {
                        waveOut.Stop();
                        port.Close();
                        break;
                    }
                }
                catch (System.IndexOutOfRangeException)
                {
                    counter_volume_4000_l = 20;
                    break;
                }
            }


            waveOut.Stop();
            port.Close();

            if (bt_start.IsEnabled == false)
            {
                bt_4000Hz_R.IsEnabled = true;
            }
        }

        private async void R_4000Hz(object sender, RoutedEventArgs e)
        {
            bt_4000Hz_R.IsEnabled = false;
            port.Open();
            int counter = 0;
            counter_volume_4000_r = 0;
            float volume = 0.000001f;
            string filename = "4000hz_r.mp3";
            string source = System.IO.Path.Combine(Environment.CurrentDirectory, @"Sound\", filename);
            var reader = new Mp3FileReader(source);
            var waveOut = new WaveOut(); // or WaveOutEvent()
            waveOut.Init(reader);
            waveOut.Play();
            waveOut.Volume = (float)volume;


            for (; ; )
            {
                try
                {
                    if (counter % time_play == 0)
                    {
                        counter_volume_4000_r++;
                        volume = (float)tab_vol.tab_4000Hz[counter_volume_4000_r];
                    }
                    if (Check() == 1)
                    {
                        break;
                    }
                    waveOut.Volume = volume;
                    await Task.Delay(100);
                    counter++;
                    if (bt_start.IsEnabled == true)
                    {
                        waveOut.Stop();
                        port.Close();
                        break;
                    }
                }
                catch (System.IndexOutOfRangeException)
                {
                    counter_volume_4000_r = 20;
                    break;
                }
            }


            waveOut.Stop();
            port.Close();

            if (bt_start.IsEnabled == false)
            {
                bt_report.IsEnabled = true;
                bt_start.IsEnabled = true;
            }
        }

        public int Check()
        {
            int valueCheck = 0;
            if (state == true)
            {
                data = port.ReadLine();
                if (data == "1\r")
                {
                    valueCheck = 1;
                    port.DiscardInBuffer();
                    port.DiscardOutBuffer();
                }
           }


            return valueCheck;
        }

        private void bt_start_Click(object sender, RoutedEventArgs e)
        {
           try
            {
                comPORT = comboBox.SelectedItem.ToString();
                port.BaudRate = 9600;
                port.PortName = comPORT;
                port.DtrEnable = true;
                port.RtsEnable = true;

                bt_start.IsEnabled = false;
                bt_stop.IsEnabled = true;
                bt_250Hz_L.IsEnabled = true;
                bt_250Hz_L.Visibility = Visibility.Visible;
                bt_250Hz_R.Visibility = Visibility.Visible;

                bt_500Hz_L.Visibility = Visibility.Hidden;
                bt_500Hz_R.Visibility = Visibility.Hidden;
                bt_1000Hz_L.Visibility = Visibility.Hidden;
                bt_1000Hz_R.Visibility = Visibility.Hidden;
                bt_2000Hz_L.Visibility = Visibility.Hidden;
                bt_2000Hz_R.Visibility = Visibility.Hidden;
                bt_4000Hz_L.Visibility = Visibility.Hidden;
                bt_4000Hz_R.Visibility = Visibility.Hidden;
            }
            catch(System.NullReferenceException)
            {
                MessageBox.Show("Przycisk nie został podłączony");
            }
                

        }

        public void bt_stop_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Chcesz przerwać badanie ? Dotychczasowe wyniki zostaną utracone.", "Ostrzeżenie", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                //do no stuff
            }
            else
            {
                try
                {
                    bt_stop.IsEnabled = false;
                    bt_250Hz_L.IsEnabled = false;
                    bt_250Hz_R.IsEnabled = false;
                    bt_500Hz_L.IsEnabled = false;
                    bt_500Hz_R.IsEnabled = false;
                    bt_1000Hz_L.IsEnabled = false;
                    bt_1000Hz_R.IsEnabled = false;
                    bt_2000Hz_L.IsEnabled = false;
                    bt_2000Hz_R.IsEnabled = false;
                    bt_4000Hz_L.IsEnabled = false;
                    bt_4000Hz_R.IsEnabled = false;



                    counter_volume_250_l = 0;
                    counter_volume_250_r = 0;
                    counter_volume_500_l = 0;
                    counter_volume_500_r = 0;
                    counter_volume_1000_l = 0;
                    counter_volume_1000_r = 0;
                    counter_volume_2000_l = 0;
                    counter_volume_2000_r = 0;
                    counter_volume_4000_l = 0;
                    counter_volume_4000_r = 0;


                    bt_start.Content = "Wykonaj ponownie badanie";
                    bt_start.IsEnabled = true;
                    port.Close();
                    state = false;
                    
                }
                catch (System.InvalidOperationException)
                {

                }

            }

        }

        private void bt_exit_Click(object sender, RoutedEventArgs e)
        {

            if (MessageBox.Show("Zamknąć program ? Niezapisane dane zostaną usunięte.", "Ostrzeżenie", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                //do no stuff
            }
            else
            {
                System.Windows.Application.Current.Shutdown();
            }

        }

        private void bt_report_Click(object sender, RoutedEventArgs e)
        {

            double[] axis = { 250, 500, 1000, 2000, 4000 };

            dB_250_L = (counter_volume_250_l * 5) - 5;
            dB_250_R = (counter_volume_250_r * 5) - 5;
            dB_500_L = (counter_volume_500_l * 5) - 5;
            dB_500_R = (counter_volume_500_r * 5) - 5;
            dB_1000_L = (counter_volume_1000_l * 5) - 5;
            dB_1000_R = (counter_volume_1000_r * 5) - 5;
            dB_2000_L = (counter_volume_2000_l * 5) - 5;
            dB_2000_R = (counter_volume_2000_r * 5) - 5;
            dB_4000_L = (counter_volume_4000_l * 5) - 5;
            dB_4000_R = (counter_volume_4000_r * 5) - 5;

            int[] result_decibels_L = new int[] { dB_250_L, dB_500_L, dB_1000_L, dB_2000_L, dB_4000_L, };
            int[] result_decibels_R = new int[] { dB_250_R, dB_500_R, dB_1000_R, dB_2000_R, dB_4000_R };
            cartesianChart1.Visibility = Visibility.Visible;
            
            var invertedYMapper = LiveCharts.Configurations.Mappers.Xy<ObservablePoint>()
                .X(point => point.X)
            .Y(point => -point.Y);
            MySeriesCollection = new SeriesCollection
            {
                
                new LineSeries
                {
                    Title = "Ucho lewe",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(200,result_decibels_L[0]),
                        new ObservablePoint(400,result_decibels_L[1]),
                        new ObservablePoint(600,result_decibels_L[2]),
                        new ObservablePoint(800,result_decibels_L[3]),
                        new ObservablePoint(1000,result_decibels_L[4])

                        /*new ObservablePoint(0,10),
                        new ObservablePoint(1,50),
                        new ObservablePoint(2,10),
                        new ObservablePoint(3,20),
                        new ObservablePoint(4,40)*/
                    },
                    Configuration = invertedYMapper

                },



            new LineSeries
                {
                    Title = "Ucho prawe",

                    Values = new ChartValues<ObservablePoint>
                    {

                        new ObservablePoint(200,result_decibels_R[0]),
                        new ObservablePoint(400,result_decibels_R[1]),
                        new ObservablePoint(600,result_decibels_R[2]),
                        new ObservablePoint(800,result_decibels_R[3]),
                        new ObservablePoint(1000,result_decibels_R[4])

                        /*new ObservablePoint(0,15),
                        new ObservablePoint(1,40),
                        new ObservablePoint(2,12),
                        new ObservablePoint(3,30),
                        new ObservablePoint(4,50)*/


                    },
                    Configuration = invertedYMapper



                },





            };


            YFormatter = x => (x * -1).ToString();
            XFormatter = val => (axis[(int)val]).ToString();

            DataContext = this;

        }

        private void CartesianChart_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Axis_RangeChanged(LiveCharts.Events.RangeChangedEventArgs eventArgs)
        {

        }


        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    SaveToPng(cartesianChart1, "chart.png");
        //}

        //private void SaveToPng(FrameworkElement visual, string fileName)
        //{
        //    var encoder = new PngBitmapEncoder();
        //    EncodeVisual(visual, fileName, encoder);
        //}

        //private static void EncodeVisual(FrameworkElement visual, string fileName, BitmapEncoder encoder)
        //{
        //    var bitmap = new RenderTargetBitmap((int)visual.ActualWidth, (int)visual.ActualHeight, 0, 0, PixelFormats.Pbgra32);
        //    bitmap.Render(visual);
        //    var frame = BitmapFrame.Create(bitmap);
        //    encoder.Frames.Add(frame);
        //    using (var stream = File.Create(fileName)) encoder.Save(stream);
        //}
    }


}
