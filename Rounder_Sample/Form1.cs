using System;

namespace RounderSample
{
    public partial class Form1 : Form
    {
        LinkedList<Point3d> RunningSamples = new LinkedList<Point3d>(); // Stores recent probe samples
        int MaxSamples = 100;  // Maximum number of samples to retain
        Probe3 _probe;         // Instance of the probe interface

        public Form1()
        {
            InitializeComponent();

            _probe = new Probe3();

            // Initialize text boxes to 0.0
            txtX.Text = txtY.Text = txtZ.Text = "0.0";
        }

        // Connect button event
        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (_probe.Connect())
            {
                _probe.TurnLEDOn();     // Turn on the probe's green LED
                timerRead.Enabled = true;  // Enable timer to read data periodically
            }
        }

        // Reference (zero) button event
        private void btnRef_Click(object sender, EventArgs e)
        {
            if (_probe.IsConnected)
                _probe.SetZero();  // Set current position as zero
            else
                MessageBox.Show("Not connected to IGEMS Rounder! Please connect first.");
        }

        // Periodic timer tick - fetches and updates data
        private void timerRead_Tick(object sender, EventArgs e)
        {
            GetNewRunningSample();

            if (RunningSamples.Count > 0)
            {
                // Display the most recent sample in the UI
                Point3d lastSample = RunningSamples.Last.Value;
                txtX.Text = lastSample.X.ToString("F3");
                txtY.Text = lastSample.Y.ToString("F3");
                txtZ.Text = lastSample.Z.ToString("F3");
            }  
        }

        // Gets a new probe sample and manages the sample list
        private void GetNewRunningSample()
        {
            Point3d s = _probe.GetMedianSample(3);  // Get median of 3 samples for noise reduction

            RunningSamples.AddLast(s);

            // Ensure the sample buffer does not exceed MaxSamples
            while (RunningSamples.Count > MaxSamples)
                RunningSamples.RemoveFirst();
        }
    }

    // Handles the connection and data communication with the probe
    internal class Probe3
    {
        private const int VendorId = 0x16C0;
        private const int ProductId = 0x0486;

        internal bool IsConnected = false;
        Point3d _offset;  // Stores the zero/reference offset

        public Probe3()
        {
            _offset = new Point3d(0, 0, 0); // Initialize offset to zero
        }

        // Establish connection to the USB probe
        public bool Connect()
        {
            int numopen = RawHID.Open(1, VendorId, ProductId, 0xffab, 0x0200);

            if (numopen < 1)
            {
                MessageBox.Show("No probe found.");
                IsConnected = false;
                return false;
            }

            IsConnected = true;
            return true;
        }

        // Turns on the probe's LED (green by default, red if specified)
        public void TurnLEDOn(bool red = false)
        {
            byte[] buff = new byte[64];

            buff[0] = (byte)(red ? 'r' : 'g');  // ASCII codes for red or green command

            RawHID.Send(0, buff, 64, 50);  // Send LED command
        }

        // Closes the connection
        public void Close()
        {
            IsConnected = false;
        }

        public void GetRawData(out byte[] data)
        {
            byte[] buffer = new byte[64];
            int brec = RawHID.Recieve(0, buffer, 1000);

            if (brec >= 64)
            {
                data = buffer;
            }
            else
                data = new byte[brec];
        }

        // Reads one sample from the probe and returns it as a Point3d (in mm)
        public Point3d GetOneSample()
        {
            double curx = 0.0, cury = 0.0, curz = 0.0;
            byte[] buffer = new byte[64];

            int brec = RawHID.Recieve(0, buffer, 1000);  // Waits up to 1000 ms for data

            if (brec >= 64)
            {
                // Convert raw byte data to double values
                curx = BitConverter.ToDouble(buffer, 8);
                cury = BitConverter.ToDouble(buffer, 16);
                curz = BitConverter.ToDouble(buffer, 24);
            }

            // Convert from micrometers to millimeters
            curx /= 1000.0;
            cury /= 1000.0;
            curz /= 1000.0;

            return new Point3d(curx, cury, curz);
        }

        // Returns a median sample from multiple measurements, corrected by the current offset
        public Point3d GetMedianSample(int NumberOfSamples)
        {
            List<double> samplesX = new List<double>();
            List<double> samplesY = new List<double>();
            List<double> samplesZ = new List<double>();

            for (int i = 0; i < NumberOfSamples; i++)
            {
                Point3d s = GetOneSample();
                samplesX.Add(s.X);
                samplesY.Add(s.Y);
                samplesZ.Add(s.Z);
            }

            double medianX = CalcMedian(samplesX);
            double medianY = CalcMedian(samplesY);
            double medianZ = CalcMedian(samplesZ);

            Point3d notOffseted = new Point3d(medianX, medianY, medianZ);

            return SubtractSample(notOffseted, _offset);  // Apply offset
        }

        // Sets the current median position as the new "zero"
        public void SetZero()
        {
            _offset = new Point3d(0, 0, 0);  // Temporarily reset offset
            _offset = GetMedianSample(101); // New zero based on average of 101 samples
        }

        // Calculates the median from a list of samples
        private double CalcMedian(List<double> samples)
        {
            samples.Sort();
            return samples[samples.Count / 2];
        }

        // Subtracts two 3D points (used for offset correction)
        private Point3d SubtractSample(Point3d s1, Point3d s2)
        {
            double x = s1.X - s2.X;
            double y = s1.Y - s2.Y;
            double z = s1.Z - s2.Z;

            return new Point3d(x, y, z);
        }
    }

    // A simple 3D point class
    public class Point3d
    {
        public readonly double X;
        public readonly double Y;
        public readonly double Z;

        public Point3d(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
